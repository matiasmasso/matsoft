Public Class Frm_About

    Private Sub Frm_About_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s As String = ""
        s = s & "version: " & version() & vbCrLf
        s = s & "working set: " & My.Application.Info.WorkingSet.ToString & vbCrLf
        's = s & "servidor: " & ServerName & vbCrLf
        s = s & "usuari: " & Session.User.EmailAddress & vbCrLf '  BLL.BLLSession.Current.User.EmailAddress & vbCrLf
        s = s & "rol: " & Session.User.Rol.Id.ToString 'BLL.BLLSession.Current.User.Rol.Id.ToString

        TextBox1.Text = s
    End Sub

    Public Function version() As String
        Dim s As String = ""
        Try
            With My.Application.Deployment.CurrentVersion
                s = .Major & "." & .Minor & "." & .Revision
            End With
        Catch ex As Exception
        End Try

        Return s
    End Function
End Class