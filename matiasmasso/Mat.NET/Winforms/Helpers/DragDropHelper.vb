Public Class DragDropHelper

    Public Class DroppedFile
        Property ByteArray As Byte()
        Property Extension As String
        Property filename As String
    End Class

    Shared Function GetDroppedFile(exs As List(Of Exception), ByVal e As System.Windows.Forms.DragEventArgs) As DroppedFile
        Dim retval As New DroppedFile

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
                retval = DroppedFilesFromFileSystem(e).First
            ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
                retval = DroppedFileFromMailMessage(e)
            Else
                exs.Add(New Exception("l'element arrossegat no ha estat identificat com a fitxer"))
            End If

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Shared Function GetDroppedImage(exs As List(Of Exception), ByVal e As System.Windows.Forms.DragEventArgs) As Image
        Dim retval As Image = Nothing
        Dim oDroppedFile = GetDroppedFile(exs, e)
        If exs.Count = 0 Then
            Dim oStream As New System.IO.MemoryStream(oDroppedFile.ByteArray)
            retval = Image.FromStream(oStream)
        End If
        Return retval
    End Function

    Shared Function GetDroppedMime(exs As List(Of Exception), ByVal e As System.Windows.Forms.DragEventArgs) As MimeCods
        Dim retval As MimeCods = MimeCods.NotSet
        Dim oDroppedFile = GetDroppedFile(exs, e)
        If exs.Count = 0 Then
            retval = MimeHelper.GetMimeFromExtension(oDroppedFile.Extension)
        End If
        Return retval
    End Function

    Private Shared Function DroppedFilesFromFileSystem(e As System.Windows.Forms.DragEventArgs) As List(Of DroppedFile)
        Dim filenames As String() = e.Data.GetData(DataFormats.FileDrop)
        Dim retval As New List(Of DroppedFile)
        For Each filename In filenames
            Dim oDroppedFile As New DroppedFile
            With oDroppedFile
                .filename = filename
                .Extension = System.IO.Path.GetExtension(filename)
                .ByteArray = System.IO.File.ReadAllBytes(filename)
            End With
            retval.Add(oDroppedFile)
        Next
        Return retval
    End Function

    Private Shared Function DroppedFileFromMailMessage(e As System.Windows.Forms.DragEventArgs) As DroppedFile
        Dim oFileDescriptor As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
        Dim oFileContent As System.IO.MemoryStream = e.Data.GetData("FileContents", True)

        Dim retval As New DroppedFile
        With retval
            .ByteArray = GetFileContentFromFileDescriptor(oFileContent)
            .filename = GetFileNameFromFileGroupDescriptorData(oFileDescriptor)
            .Extension = System.IO.Path.GetExtension(retval.filename)
        End With
        Return retval
    End Function

    Private Shared Function GetFileNameFromFileGroupDescriptorData(FileDescriptor As System.IO.MemoryStream) As String
        Dim oByteArray(512) As Byte
        FileDescriptor.Read(oByteArray, 0, 512)

        ' used to build the filename from the oByteArray block
        Dim retval As String = ""
        For i As Integer = 76 To 512
            If oByteArray(i) = 0 Then Exit For
            retval = retval & Convert.ToChar(oByteArray(i))
        Next
        FileDescriptor.Close()
        Return retval
    End Function

    Private Shared Function GetFileContentFromFileDescriptor(FileContent As System.IO.MemoryStream) As Byte()
        Dim oBinaryReader As New IO.BinaryReader(FileContent)
        Dim retval = oBinaryReader.ReadBytes(FileContent.Length)
        oBinaryReader.Close()
        Return retval
    End Function



    Shared Function GetDatagridDropDocFiles(sender As DataGridView, ByVal e As System.Windows.Forms.DragEventArgs, ByRef oDocFiles As List(Of DTODocFile), ByRef oTargetCell As DataGridViewCell, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oPoint = sender.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = sender.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            oTargetCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
        End If

        retval = GetDroppedDocFiles(e, oDocFiles, exs)
        Return retval
    End Function

    Shared Function GetDroppedDocFiles(ByVal e As System.Windows.Forms.DragEventArgs, ByRef oDocFiles As List(Of DTODocFile), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim o = e.GetType

        oDocFiles = New List(Of DTODocFile)

        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            Dim sFilenames As String() = e.Data.GetData(DataFormats.FileDrop)

            For Each sFilename As String In sFilenames
                Dim oDocfile = LegacyHelper.DocfileHelper.Factory(sFilename, exs)
                oDocFiles.Add(oDocfile)
            Next

            retval = exs.Count = 0

        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            '
            Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
            Dim fileGroupDescriptor(512) As Byte
            theStream.Read(fileGroupDescriptor, 0, 512)

            ' used to build the filename from the FileGroupDescriptor block
            Dim sFilename As String = ""
            For i As Integer = 76 To 512
                If fileGroupDescriptor(i) = 0 Then Exit For
                sFilename = sFilename & Convert.ToChar(fileGroupDescriptor(i))
            Next
            theStream.Close()
            '
            ' get the actual raw file into memory
            Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
            Dim oByteArray As Byte() = Nothing

            If sFilename.EndsWith(".csv") Or sFilename.EndsWith(".txt") Then
                Dim oStreamReader = New System.IO.StreamReader(oMemStream)
                Dim sText = oStreamReader.ReadToEnd()
                oByteArray = System.Text.Encoding.UTF8.GetBytes(sText)
            Else
                ' allocate enough bytes to hold the raw data
                Dim oBinaryReader As New IO.BinaryReader(oMemStream)
                oByteArray = oBinaryReader.ReadBytes(oMemStream.Length)
                oBinaryReader.Close()
            End If

            Dim oMimeCod = MimeHelper.GetMimeFromExtension(sFilename)
            Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray, oMimeCod)
            If exs.Count = 0 Then
                oDocFile.Filename = sFilename
                oDocFiles.Add(oDocFile)
                retval = True
            End If

        Else
            exs.Add(New Exception("l'element arrossegat no ha estat identificat com a fitxer"))
            retval = False
        End If

        Return retval
    End Function

    Shared Function DragEnterFilePresentEffect(ByRef e As DragEventArgs) As DragDropEffects
        Dim retval As DragDropEffects = DragDropEffects.None
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            retval = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            retval = DragDropEffects.Copy
        End If
        Return retval
    End Function

    Shared Function GetDroppedMediaResources(ByVal e As System.Windows.Forms.DragEventArgs, ByRef oMediaResources As List(Of DTOMediaResource), oUser As DTOUser, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim o = e.GetType

        oMediaResources = New List(Of DTOMediaResource)

        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            Dim sFilenames As String() = e.Data.GetData(DataFormats.FileDrop)

            For Each sFilename As String In sFilenames
                Dim oMediaResource = MediaResourcesHelper.Factory(oUser, sFilename, exs)
                oMediaResources.Add(oMediaResource)
            Next

            retval = exs.Count = 0

        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            '
            Dim theStream As System.IO.MemoryStream = e.Data.GetData("FileGroupDescriptor")
            Dim fileGroupDescriptor(512) As Byte
            theStream.Read(fileGroupDescriptor, 0, 512)

            ' used to build the filename from the FileGroupDescriptor block
            Dim sFilename As String = ""
            For i As Integer = 76 To 512
                If fileGroupDescriptor(i) = 0 Then Exit For
                sFilename = sFilename & Convert.ToChar(fileGroupDescriptor(i))
            Next
            theStream.Close()
            '
            ' get the actual raw file into memory
            Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)

            ' allocate enough bytes to hold the raw data
            Dim oBinaryReader As New IO.BinaryReader(oMemStream)
            Dim oStream As Byte() = oBinaryReader.ReadBytes(oMemStream.Length)
            oBinaryReader.Close()

            Dim oMediaResource As DTOMediaResource = Nothing
            If MediaResourcesHelper.LoadFromStream(oUser, oStream, oMediaResource, exs, sFilename) Then
                oMediaResource.Filename = sFilename
                oMediaResources.Add(oMediaResource)
                retval = True
            End If

        Else
            exs.Add(New Exception("l'element arrossegat no ha estat identificat com a fitxer"))
            retval = False
        End If

        Return retval
    End Function


End Class
