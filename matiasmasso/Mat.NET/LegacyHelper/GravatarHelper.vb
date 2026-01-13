
Public Class GravatarHelper

    Shared Function Url(emailaddress As String, Optional defaultImageCod As String = "", Optional width As Integer = 80) As String
        Dim encoder = New System.Text.UTF8Encoding()
        Dim md5 = New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim hashedBytes = md5.ComputeHash(encoder.GetBytes(emailaddress.ToLower()))
        Dim sb = New System.Text.StringBuilder(hashedBytes.Length * 2)

        For i = 0 To hashedBytes.Length - 1
            sb.Append(hashedBytes(i).ToString("X2"))
        Next


        'Dim MD5Hash = CryptoHelper.HashMD5(emailaddress.ToLower.Trim)
        Dim retval As String = String.Format("https://www.gravatar.com/avatar/{0}", sb.ToString) ' MD5Hash)
        'https://s.gravatar.com/avatar/2326d54196390a4b59757ee508d59530?s=80
        UrlHelper.addParam(retval, "s", width.ToString())
        UrlHelper.addParam(retval, "d", "mp") 'sets default avatar to "mistery man"
        Return retval
    End Function
End Class
