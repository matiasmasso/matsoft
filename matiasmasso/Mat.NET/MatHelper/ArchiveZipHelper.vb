Imports System.IO.Compression
Imports System.IO
Public Class ArchiveZipHelper
    ' Used to specify what our overwrite policy is for files we are extracting.
    Shared Function ZipEm(fileList As List(Of String), nzfName As String) As Boolean
        Try
            If File.Exists(nzfName) Then File.Delete(nzfName)
            Using newZipFile As ZipArchive = ZipFile.Open(nzfName, ZipArchiveMode.Create)
                For Each pfn As String In fileList
                    newZipFile.CreateEntryFromFile(pfn, Path.GetFileName(pfn))
                Next
            End Using
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


    Shared Function ZipEm(oStreams As List(Of Byte()), nzfName As String) As Boolean
        Using compressedFileStream = New MemoryStream()
            'Create an archive and store the stream in memory.

            Using zipArchive = New ZipArchive(compressedFileStream, ZipArchiveMode.Update, False)
                Dim i As Integer
                For Each oStream As Byte() In oStreams
                    'Create a zip entry for each attachment
                    i += 1
                    Dim sFilename As String = "filename " & i.ToString & ".jpg"
                    Dim zipEntry = zipArchive.CreateEntry(sFilename)

                    'Get the stream of the attachment
                    Using originalFileStream = New MemoryStream(oStream)
                        Using zipEntryStream = zipEntry.Open()
                            'Copy the attachment stream to the zip entry stream
                            originalFileStream.CopyTo(zipEntryStream)
                        End Using
                    End Using

                Next

                System.IO.File.WriteAllBytes(nzfName, compressedFileStream.ToArray)
            End Using

        End Using

        Return True
    End Function
End Class
