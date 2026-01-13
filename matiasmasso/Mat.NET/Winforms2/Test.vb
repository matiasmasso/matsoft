Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports LegacyHelper
Imports Newtonsoft.Json.Linq

Public Class Test
    Public Shared Async Sub Procesa()
        Dim exs As New List(Of Exception)
        Dim path = "C:\Users\matia\Documents\Nuria-nomina 2.Pdf"
        Dim pdfBytes = FileToByteArray(path)
        Dim thumb = Await FEB.Pdf2Jpg.Thumbnail(exs, pdfBytes)
        'Dim oFrm As New Frm_Test
        'oFrm.Show()
    End Sub

    Public Shared Function FileToByteArray(ByVal _FileName As String) As Byte()
        Dim _Buffer() As Byte = Nothing

        Try
            ' Open file for reading
            Dim _FileStream As New System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            ' attach filestream to binary reader
            Dim _BinaryReader As New System.IO.BinaryReader(_FileStream)

            ' get total byte length of the file
            Dim _TotalBytes As Long = New System.IO.FileInfo(_FileName).Length

            ' read entire file into buffer
            _Buffer = _BinaryReader.ReadBytes(CInt(Fix(_TotalBytes)))

            ' close file reader
            _FileStream.Close()
            _FileStream.Dispose()
            _BinaryReader.Close()
        Catch _Exception As Exception
            ' Error
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString())
        End Try

        Return _Buffer
    End Function
End Class