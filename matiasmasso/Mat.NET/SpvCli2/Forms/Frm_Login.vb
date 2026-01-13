Public Class Frm_Login
    Public Property User As DTOUser
    Public Property Persist As Boolean
    Private _Emp As DTOEmp

    Public Enum Icons
        Info
        Warning
    End Enum

    Public Sub New(oEmpId As DTOEmp.Ids)
        MyBase.New
        InitializeComponent()
        _Emp = New DTOEmp(oEmpId)
    End Sub

    Private Sub Frm_Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        LabelVersion.Text = version()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim sEmail As String = TextBoxEmail.Text
        Dim sPassword As String = TextBoxPwd.Text
        Dim oUser As DTOUser = Nothing

        If sEmail = "" Then
            WarnUser(Icons.Warning, "si us plau omple la casella de correu")
        ElseIf sPassword = "" Then
            WarnUser(Icons.Info, "password enviat a " & sEmail)
        Else
            Dim exs As New List(Of Exception)
            _User = Await FEB2.User.Validate(_Emp, sEmail, sPassword, exs)
            If exs.Count = 0 Then
                If _User Is Nothing Then
                    WarnUser(Icons.Warning, "password incorrecte")
                Else
                    _Persist = CheckBoxPersist.Checked
                    Me.Close()
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If

    End Sub

    Public Function version() As String
        Dim retval As String = ""
        Try
            If My.Application.IsNetworkDeployed Then
                With My.Application.Deployment.CurrentVersion
                    retval = .Major & "." & .Minor & "." & .Revision
                End With
            Else
                retval = "(versió de desenvolupador)"
            End If
        Catch ex As Exception

        End Try
        Return retval
    End Function

    Private Sub WarnUser(oIcon As Icons, sMessage As String)
        PictureBoxWarn.Visible = True
        LabelWarnUser.Visible = True
        LabelWarnUser.Text = sMessage

        Select Case oIcon
            Case Icons.Info
                PictureBoxWarn.Image = My.Resources.info
                LabelWarnUser.ForeColor = Color.Navy
            Case Icons.Warning
                PictureBoxWarn.Image = My.Resources.warn
                LabelWarnUser.ForeColor = Color.Red
        End Select
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub LinkLabelRemindPassword_Click(sender As Object, e As EventArgs) Handles LinkLabelRemindPassword.Click
        Dim sEmail As String = TextBoxEmail.Text
        If sEmail = "" Then
            WarnUser(Icons.Warning, "si us plau omple la casella de correu")
        Else
            Dim exs As New List(Of Exception)
            Dim oUser = Await FEB2.User.FromEmail(exs, _Emp, sEmail)
            If exs.Count = 0 Then
                If Await FEB2.User.EmailPwd(_Emp, sEmail, exs) Then
                    WarnUser(Icons.Info, "password enviat a " & sEmail)
                Else
                    WarnUser(Icons.Warning, "email no registrat")
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub TextBoxEmail_TextChanged(sender As Object, e As EventArgs) Handles TextBoxEmail.TextChanged
        Dim value As String = TextBoxEmail.Text
        If DTOUser.CheckEmailSintaxis(value) Then
            TextBoxPwd.Enabled = True
            CheckBoxPersist.Enabled = True
            ButtonOk.Enabled = True
        Else
            TextBoxPwd.Enabled = False
            CheckBoxPersist.Enabled = False
            ButtonOk.Enabled = False
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        UIHelper.ShowHtml("http://www.matiasmasso.es")
    End Sub
End Class