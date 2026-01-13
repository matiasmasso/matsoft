Public Class Frm_About

    Private Sub Frm_About_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s As String = ""
        s = s & "version: " & version() & vbCrLf
        s = s & "Version (alt.): " & GetVersion() & vbCrLf
        s = s & "working set: " & My.Application.Info.WorkingSet.ToString & vbCrLf
        's = s & "servidor: " & ServerName & vbCrLf
        s = s & "usuari: " & Current.Session.User.EmailAddress & vbCrLf '  Current.Session.User.EmailAddress & vbCrLf
        s = s & "rol: " & Current.Session.Rol.Id.ToString 'Current.Session.User.Rol.Id.ToString

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

    Public Function GetVersion() As String
        If (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) Then
            Dim ver As Version
            ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
            Return String.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision)
        Else
            Return "Versió no descarregada "
        End If
    End Function
End Class