Public Class WebPageHelper
    Shared Function ReadHtmlPage(ByVal url As String, exs As List(Of Exception)) As String
        Dim objResponse As System.Net.WebResponse
        Dim objRequest As System.Net.WebRequest
        Dim result As String = ""

        Try
            objRequest = System.Net.HttpWebRequest.Create(url)
            objResponse = objRequest.GetResponse()
            Dim sr As New System.IO.StreamReader(objResponse.GetResponseStream())
            result = sr.ReadToEnd()

            'clean up StreamReader
            sr.Close()
        Catch ex As Exception
            exs.Add(New Exception("No s'ha pogut descarregar el document de internet"))
            exs.Add(ex)
        End Try

        Return result
    End Function

End Class
