Imports System.Web
Imports Common.Logging.Configuration

Public Class DTOBrowser
    Property IP As String
    Property OS As String
    Property Browser As String
    Property UserName As String
    Property SessionId As String
    Property Rol As String

    Property Cookies As Dictionary(Of String, String)
    Property Headers As Dictionary(Of String, String)

    Shared Function Factory(IP As String, BrowserName As String, BrowserVersion As String, oUser As DTOUser) As DTOBrowser
        Dim retval As New DTOBrowser
        With retval
            .IP = IP
            .Browser = String.Format("{0} {1}", BrowserName, BrowserVersion)
            If oUser IsNot Nothing Then
                .UserName = DTOUser.NicknameOrElse(oUser)
                .Rol = oUser.Rol.id.ToString
            End If
        End With
        Return retval
    End Function

    Shared Function EmailLink(oBrowser As DTOBrowser, BlScriptsEnabled As Boolean) As String
        Dim sb As New System.Text.StringBuilder
        For Each s As String In List(oBrowser, BlScriptsEnabled)
            sb.AppendLine(s)
        Next

        Dim src As String = sb.ToString
        Dim sBody As String = HttpUtility.UrlEncode(src.ToString())
        Dim sSubject As String = HttpUtility.UrlEncode("prueba de navegador de usuario")
        Dim sTo As String = "matias@matiasmasso.es"
        Dim retval As String = "mailto:" & sTo & "?subject=" & sSubject & "&body=" & sBody
        Return retval
    End Function

    Shared Function List(oBrowser As DTOBrowser, BlScriptsEnabled As Boolean) As List(Of String)
        Dim retval As New List(Of String)
        With oBrowser
            retval.Add("IP: " & .IP)
            'retval.Add("OS: " & .OS)
            retval.Add("Browser: " & .Browser)
            retval.Add("Scripts: " & If(BlScriptsEnabled, "SI", "NO"))
            retval.Add("User name: " & .UserName)
            retval.Add("Session: " & .SessionId)
            retval.Add("Rol: " & .Rol)
        End With
        Return retval
    End Function


End Class
