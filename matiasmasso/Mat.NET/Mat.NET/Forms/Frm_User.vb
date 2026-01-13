Public Class Frm_User
    Private _User As DTOUser
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Contacts
        Raffles
    End Enum

    Public Sub New(value As DTOUser)
        MyBase.New()
        Me.InitializeComponent()
        _User = value
        BLL.BLLUser.Load(_User)
    End Sub

    Private Sub Frm_User_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _User
            TextBoxEmail.Text = .EmailAddress
            TextBoxNom.Text = .Nom
            TextBoxCognoms.Text = .Cognoms
            TextBoxNickName.Text = .NickName
            Xl_Sex1.Sex = .Sex
            Xl_Country1.Country = .Country
            Xl_ZipCod1.Load(.Country, .ZipCod)
            TextBoxTelefon.Text = .Tel
            If .Rol.IsStaff Then
                TextBoxPassword.UseSystemPasswordChar = True
                TextBoxPassword.Enabled = False
                Xl_LookupRol1.Enabled = False
            End If
            TextBoxPassword.Text = .Password
            Xl_Yea1.Yea = .BirthYear
            Xl_Langs1.Value = .Lang
            Xl_LookupRol1.Rol = .Rol

            UIHelper.LoadComboFromEnum(ComboBoxSource, GetType(DTOUser.Sources), "(seleccionar font)", DTO.DTOUser.Sources.Manual)
            ComboBoxSource.SelectedValue = .Source

            If .FchCreated = Nothing Then
                DateTimePickerFchCreated.Value = Now
            Else
                DateTimePickerFchCreated.Value = .FchCreated
            End If

            If .FchActivated > DateTimePickerFchActivated.MinDate Then
                CheckBoxActivated.Checked = True
                DateTimePickerFchActivated.Visible = True
                DateTimePickerFchActivated.Value = .FchActivated
            End If

            If .FchDeleted > DateTimePickerFchDeleted.MinDate Then
                CheckBoxDeleted.Checked = True
                DateTimePickerFchDeleted.Visible = True
                DateTimePickerFchDeleted.Value = .FchDeleted
            End If

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
       Xl_Sex1.AfterUpdate, _
          Xl_Country1.AfterUpdate, _
           Xl_ZipCod1.AfterUpdate, _
            TextBoxTelefon.TextChanged, _
             TextBoxPassword.TextChanged, _
              Xl_Yea1.AfterUpdate, _
               Xl_Langs1.AfterUpdate, _
                Xl_LookupRol1.AfterUpdate, _
                 ComboBoxSource.SelectedIndexChanged, _
                  DateTimePickerFchCreated.ValueChanged, _
                   DateTimePickerFchActivated.ValueChanged, _
                      DateTimePickerFchDeleted.ValueChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of exception)
        With _User
            .EmailAddress = TextBoxEmail.Text.Trim
            .Nom = TextBoxNom.Text
            .Cognoms = TextBoxCognoms.Text
            .NickName = TextBoxNickName.Text
            .Sex = Xl_Sex1.Sex
            .Country = Xl_Country1.Country
            .ZipCod = Xl_ZipCod1.ZipCod
            .Tel = TextBoxTelefon.Text
            .Password = TextBoxPassword.Text
            .BirthYear = Xl_Yea1.Yea
            .Lang = Xl_Langs1.Value
            .Rol = Xl_LookupRol1.Rol
            .Source = ComboBoxSource.SelectedValue
            .FchCreated = DateTimePickerFchCreated.Value
            If CheckBoxActivated.Checked Then
                .FchActivated = DateTimePickerFchActivated.Value
            Else
                .FchActivated = Nothing
            End If
            If CheckBoxDeleted.Checked Then
                .FchDeleted = DateTimePickerFchDeleted.Value
            Else
                .FchDeleted = Nothing
            End If
        End With

        If BLL.BLLUser.Update(_User, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_User))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLUser.Delete(_User, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_User))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Raffles
                'Dim oParticipants As List(Of DTORaffleParticipant) = RaffleParticipantsLoader.FromUser(_User)
                'Xl_RaffleParticipants1.Load(oParticipants, Xl_RaffleParticipants.Modes.FromUser)
            Case Tabs.Contacts
                Dim oContacts As List(Of DTOContact) = BLL.BLLUser.Contacts(_User)
                Xl_Contacts1.Load(oContacts)
        End Select
    End Sub

    Private Sub CheckBoxActivated_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxActivated.CheckedChanged
        If _AllowEvent Then
            DateTimePickerFchActivated.Visible = CheckBoxActivated.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxDeleted_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDeleted.CheckedChanged
        If _AllowEvent Then
            DateTimePickerFchDeleted.Visible = CheckBoxDeleted.Checked
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub Xl_Contact_Add1_RequestToAdd(sender As Object, e As MatEventArgs) Handles Xl_Contact_Add1.RequestToAdd
        Dim oContact As DTOContact = e.Argument
        Dim exs As New List(Of exception)
        'BLL_User.AddContact(_User, oContact, exs)
        'Dim oContacts As List(Of DTOContact) = BLL_User.Contacts(_User)
        'Xl_Contacts1.Load(oContacts)
    End Sub
End Class