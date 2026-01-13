Public Class Frm_Menus
    Private _AllowEvents As Boolean

    Private Sub Frm_Menus_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadApps()
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Dim oApp As DTOApp.AppTypes = ComboBox1.SelectedIndex
        Xl_Menus1.Load(oApp, Current.Session.Lang)
    End Sub


    Private Sub LoadApps()
        UIHelper.LoadComboFromEnum(ComboBox1, GetType(DTOApp.AppTypes), , 1)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _allowevents Then
            refresca()
        End If
    End Sub

    Private Sub Xl_Menus1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Menus1.RequestToRefresh
        refresca()
    End Sub
End Class