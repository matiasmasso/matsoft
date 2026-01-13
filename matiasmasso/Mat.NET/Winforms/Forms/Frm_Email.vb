Public Class Frm_Email

    Private _Contact As DTOContact
    Private _User As DTOUser
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oUser As DTOUser)
        MyBase.New
        InitializeComponent()
        _User = oUser
        _Contact = oUser.Contact
    End Sub

    Private Async Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _User
            Xl_EmailAddress1.Load(.EmailAddress)
            TextBoxNickname.Text = .NickName
            TextBoxObs.Text = .Obs
            Await CheckEmail(.EmailAddress)

            Xl_Contacts1.ColumnHeadersVisible = False
            Xl_Contacts1.DefaultCellStyle.BackColor = Me.BackColor
        End With
        _AllowEvents = True
    End Sub

    Private Async Function CheckEmail(src) As Task
        Dim exs As New List(Of Exception)
        If src = "" Then
            Dim oContacts As New List(Of DTOContact)
            oContacts.Add(_Contact)
            Xl_Contacts1.Load(oContacts)
            Xl_Contacts1.CurrentCell = Nothing
            ButtonOk.Enabled = False
        Else
            Dim oUser = Await FEB2.User.FromEmail(exs, Current.Session.Emp, src)
            If exs.Count = 0 Then
                If oUser Is Nothing Then
                    _User.EmailAddress = Xl_EmailAddress1.Value
                Else
                    _User = oUser
                End If
                Dim oContacts As List(Of DTOContact) = _User.Contacts
                If Not _User.Contacts.Exists(Function(x) x.Equals(_Contact)) Then
                    oContacts.Insert(0, _Contact)
                End If
                Xl_Contacts1.Load(oContacts)
                Xl_Contacts1.CurrentCell = Nothing
                ButtonOk.Enabled = True
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Async Sub Xl_EmailAddress1_onValidation(sender As Object, e As MatEventArgs) Handles Xl_EmailAddress1.onValidation
        If _AllowEvents Then
            Await CheckEmail(e.Argument)
        End If
    End Sub

    Private Sub TextBoxObs_TextChanged(sender As Object, e As EventArgs) Handles TextBoxNickname.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _User
            .NickName = TextBoxNickname.Text
            .Obs = TextBoxObs.Text
        End With
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_User))
        Me.Close()
    End Sub

End Class