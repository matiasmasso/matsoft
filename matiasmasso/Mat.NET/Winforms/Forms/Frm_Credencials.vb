Public Class Frm_Credencials
    Private _Owner As DTOUser

    Private Sub Frm_Credencials_Load(sender As Object, e As EventArgs) Handles Me.Load
        _Owner = Current.Session.User
        refresca()
    End Sub

    Private Sub Xl_Credencials1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Credencials1.RequestToAddNew
        Dim oCredencial = DTOCredencial.Factory(Current.Session.User)
        Dim oFrm As New Frm_Credencial(oCredencial)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Credencials1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Credencials1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oCredencials = Await FEB2.Credencials.All(_Owner, exs)
        If exs.Count = 0 Then
            Xl_Credencials1.Load(oCredencials)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Credencials1.Filter = e.Argument
    End Sub

    Private Async Sub Xl_Credencials1_RequestToSwitchOwner(sender As Object, e As MatEventArgs) Handles Xl_Credencials1.RequestToSwitchOwner
        Dim exs As New List(Of Exception)
        Dim oOwners = Await FEB2.Credencials.Owners(GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Leads(oOwners, Mode:=DTO.Defaults.SelectionModes.Selection)
            AddHandler oFrm.onItemSelected, AddressOf onOwnerSwitched
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub onOwnerSwitched(sender As Object, e As MatEventArgs)
        _Owner = e.Argument
        If _Owner.Equals(Current.Session.User) Then
            Me.Text = "Credencials"
        Else
            Me.Text = "Credencials de " & _Owner.EmailAddress
        End If
        refresca()
    End Sub
End Class