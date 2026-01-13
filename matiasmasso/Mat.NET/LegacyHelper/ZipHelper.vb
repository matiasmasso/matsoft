Imports Ionic.Zip

Public Class ZipHelper
    Shared Function Filenames(oByteArray As Byte()) As List(Of String)
        Dim retval As New List(Of String)
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Dim oZipStream As New ZipInputStream(oStream)

        Dim e As ZipEntry = oZipStream.GetNextEntry()
        Do While e IsNot Nothing
            Dim sFilename As String = e.FileName
            retval.Add(sFilename)
            e = oZipStream.GetNextEntry()
        Loop
        Return retval
    End Function

    Shared Function Extract(oByteArray As Byte()) As List(Of File)
        Dim retval As New List(Of File)
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        Dim oZipStream As New Ionic.Zip.ZipInputStream(oStream)

        Using zip1 As ZipFile = ZipFile.Read(oStream)
            For Each e As ZipEntry In zip1
                Using snar As New System.IO.MemoryStream
                    e.Extract(snar)
                    snar.Seek(0, System.IO.SeekOrigin.Begin)
                    Dim oBuffer As Byte() = FileSystemHelper.GetByteArrayFromStream(snar)
                    Dim oItem = File.Factory(e.FileName, oBuffer)
                    'Dim exs As New List(Of Exception)
                    'Dim oItem = FEBL.DocFile.Factory(exs, oBuffer)
                    'oItem.Filename = e.FileName
                    retval.Add(oItem)
                End Using
            Next
        End Using
        Return retval
    End Function

    Shared Function Zip(items As List(Of ZipHelper.File), exs As List(Of Exception)) As IO.Stream
        Dim retval As New IO.MemoryStream
        Try
            Using zip1 As ZipFile = New ZipFile
                For Each item In items
                    zip1.AddEntry(item.Filename, item.ByteArray)
                Next
                zip1.Save(retval)
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Public Class File
        Property Filename As String
        Property ByteArray As Byte()

        Shared Function Factory(sFilename As String, oByteArray As Byte()) As File
            Dim retval As New File
            retval.Filename = sFilename
            retval.ByteArray = oByteArray
            Return retval
        End Function
    End Class


End Class

