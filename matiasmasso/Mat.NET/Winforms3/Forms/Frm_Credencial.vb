Public Class Frm_Credencial
    Private _Credencial As DTOCredencial
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOCredencial)
        MyBase.New()
        Me.InitializeComponent()
        _Credencial = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oAllRols = Await FEB.Rols.All(exs)
        If exs.Count = 0 Then
            If FEB.Credencial.Load(_Credencial, exs) Then
                With _Credencial
                    TextBoxReferencia.Text = .Referencia
                    TextBoxUrl.Text = .Url
                    TextBoxUsuari.Text = .Usuari
                    TextBoxPwd.Text = .Password
                    TextBoxObs.Text = .Obs
                    Xl_RolsAllowed1.Load(oAllRols, .Rols)
                    Xl_Users1.Load(.Owners)
                    ButtonOk.Enabled = .IsNew
                    ButtonDel.Enabled = Not .IsNew
                End With
                EnableOwnerButtons()
                _AllowEvent = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxReferencia.TextChanged,
        TextBoxUrl.TextChanged,
        TextBoxUsuari.TextChanged,
        TextBoxPwd.TextChanged,
        TextBoxObs.TextChanged,
        Xl_RolsAllowed1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Credencial
            .referencia = TextBoxReferencia.Text
            .url = TextBoxUrl.Text
            .usuari = TextBoxUsuari.Text
            .password = TextBoxPwd.Text
            .obs = TextBoxObs.Text
            .rols = Xl_RolsAllowed1.CheckedValues
            .owners = Xl_Users1.Values
            .UsrLog.usrLastEdited = Current.Session.User
        End With

        If Await FEB.Credencial.Update(_Credencial, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Credencial))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar les credencials")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click

        Dim exs As New List(Of Exception)
        Select Case _Credencial.Owners.Count
            Case 0
                exs.Add(New Exception("No figures com a propietari d'aquesta credencial"))
            Case 1
                Dim oOwner As DTOUser = _Credencial.Owners.First
                If oOwner.Equals(Current.Session.User) Then
                    Dim rc As MsgBoxResult = MsgBox("eliminem aquesta credencial?", MsgBoxStyle.OkCancel)
                    If rc = MsgBoxResult.Ok Then
                        If Await FEB.Credencial.Delete(_Credencial, exs) Then
                            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Credencial))
                            Me.Close()
                        Else
                            UIHelper.WarnError(exs, "error al eliminar la credencial")
                        End If
                    End If
                Else
                    exs.Add(New Exception("No figures com a propietari d'aquesta credencial"))
                End If
            Case Else
                If _Credencial.Owners.Exists(Function(x) x.Equals(Current.Session.User)) Then
                    exs.Add(New Exception("No ets l'unic propietari d'aquesta credencial" & vbCrLf & "assegura't que la resta no la necessiten i eliminal's primer de la llista"))
                Else
                    exs.Add(New Exception("No figures com a propietari d'aquesta credencial"))
                End If
        End Select

    End Sub

    Private Async Sub ButtonAddme_Click(sender As Object, e As EventArgs) Handles ButtonAddUser.Click
        Dim exs As New List(Of Exception)
        Dim oUser As DTOUser = Await FEB.User.FromEmail(exs, GlobalVariables.Emp, TextBoxEmailAddress.Text)
        If exs.Count = 0 Then
            If oUser Is Nothing Then
                UIHelper.WarnError("usuari no trobat")
            Else
                Dim oOwners As List(Of DTOUser) = Xl_Users1.Values
                If Not oOwners.Exists(Function(x) x.Equals(oUser)) Then
                    oOwners.Add(oUser)
                    Xl_Users1.Load(oOwners)
                    TextBoxEmailAddress.Clear()
                    ButtonOk.Enabled = True
                    EnableOwnerButtons()
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonRemoveMe_Click(sender As Object, e As EventArgs)
        Dim oOwners As List(Of DTOUser) = Xl_Users1.Values
        If oOwners.Count = 1 Then
            MsgBox("No es permés eliminar l'ultim propietari. Si la credencial no es válida, pots eliminar-la.", MsgBoxStyle.Exclamation)
        Else
            Dim oUser As DTOUser = oOwners.FirstOrDefault(Function(x) x.Equals(Current.Session.User))
            If oOwners.Remove(oUser) Then
                ButtonOk.Enabled = True
                Xl_Users1.Load(oOwners)
                EnableOwnerButtons()
            End If
        End If
    End Sub

    Private Sub EnableOwnerButtons()
        Dim oUser As DTOUser = Current.Session.User
        Dim oOwners As List(Of DTOUser) = Xl_Users1.Values
        If oOwners.Exists(Function(x) x.Equals(oUser)) Then
            ButtonAddUser.Enabled = False
            ButtonDel.Enabled = Not _Credencial.IsNew
        Else
            ButtonAddUser.Enabled = True
            ButtonDel.Enabled = False
        End If

    End Sub

    Private Sub Xl_Users1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Users1.RequestToAddNew
        Dim oFrm As New Frm_Leads(Mode:=DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onUserAdded
        oFrm.Show()
    End Sub

    Private Sub onUserAdded(sender As Object, e As MatEventArgs)
        Dim oOwners As List(Of DTOUser) = Xl_Users1.Values
        oOwners.Add(e.Argument)
        Xl_Users1.Load(oOwners)
        ButtonOk.Enabled = True
    End Sub

    Private Sub TextBoxEmailAddress_TextChanged(sender As Object, e As EventArgs) Handles TextBoxEmailAddress.TextChanged
        ButtonAddUser.Enabled = DTOUser.IsEmailNameAddressValid(TextBoxEmailAddress.Text)
    End Sub

    Private Sub BrowseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrowseToolStripMenuItem.Click
        Dim url = TextBoxUrl.Text
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub CopyUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyUserToolStripMenuItem.Click
        Dim usr = TextBoxUsuari.Text
        UIHelper.CopyToClipboard(usr)
    End Sub

    Private Sub CopyPwdToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyPwdToolStripMenuItem.Click
        Dim pwd = TextBoxPwd.Text
        UIHelper.CopyToClipboard(pwd)
    End Sub
End Class