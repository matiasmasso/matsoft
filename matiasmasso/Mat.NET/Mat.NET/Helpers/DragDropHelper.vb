Public Class DragDropHelper


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
                Dim oDocFile As DTODocFile = BLL_DocFile.FromFile(sFilename, exs)
                oDocFiles.Add(oDocFile)
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

            Dim oDocFile As DTODocFile = Nothing
            If BLL_DocFile.LoadFromStream(oStream, oDocFile, exs, sFilename) Then
                oDocFile.PendingOp = DTODocFile.PendingOps.Update
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

    Shared Function GetFileDropMediaObjects(ByVal e As System.Windows.Forms.DragEventArgs, ByRef oMediaObjects As MediaObjects, ByRef exs as List(Of exception)) As Boolean
        Dim retval As Boolean
        oMediaObjects = New MediaObjects

        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            Dim sFilenames As String() = e.Data.GetData(DataFormats.FileDrop)

            For Each sFilename As String In sFilenames
                Dim oMediaObject As MediaObject = MediaObject.FromFile(sFilename, exs)
                oMediaObjects.Add(oMediaObject)
            Next
            retval = exs.Count = 0

        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            '
            ' the first step here is to get the filename
            ' of the attachment and
            ' build a full-path name so we can store it 
            ' in the temporary folder
            '
            ' set up to obtain the FileGroupDescriptor 
            ' and extract the file name
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
            ' Second step:  we have the file name.  
            ' Now we need to get the actual raw
            ' data for the attached file .
            '
            ' get the actual raw file into memory
            Dim oMemStream As System.IO.MemoryStream = e.Data.GetData("FileContents", True)
            ' allocate enough bytes to hold the raw data
            Dim oBinaryReader As New IO.BinaryReader(oMemStream)
            Dim oStream As Byte() = oBinaryReader.ReadBytes(oMemStream.Length)
            oBinaryReader.Close()

            Dim oMediaObject As New MediaObject(oStream, sFilename)
            oMediaObjects.Add(oMediaObject)
            retval = exs.Count = 0
        Else
            exs.Add(New Exception("l'element arrossegat no ha estat identificat com a fitxer"))
            retval = False
        End If

        Return retval
    End Function

    Shared Function GetStreamFromDatagridFiledDrop(oGrid As DataGridView, ByVal e As System.Windows.Forms.DragEventArgs, ByRef oTargetCell As DataGridViewCell, ByRef oMediaObjects As MediaObjects, ByRef exs as List(Of exception)) As Boolean
        Dim retval As Boolean

        Dim oPoint = oGrid.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = oGrid.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            oTargetCell = oGrid.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
        End If

        retval = GetFileDropMediaObjects(e, oMediaObjects, exs)
        Return retval
    End Function
End Class
