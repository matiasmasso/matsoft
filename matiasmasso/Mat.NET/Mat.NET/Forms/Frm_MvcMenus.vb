Public Class Frm_MvcMenus

    Private Sub Frm_MvcMenus_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim oMvcMenus As List(Of DTO.DTOMenu) = BLL.BLLMvcMenus.All()
        Xl_MvcMenus1.Load(oMvcMenus)
    End Sub


    Private Sub Xl_MvcMenus1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_MvcMenus1.RequestToAddNew
        Dim item As DTO.DTOMenu = BLL.BLLMvcMenu.NewMvcMenu

        Dim oFrm As New Frm_MvcMenu(item)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_MvcMenus1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_MvcMenus1.RequestToRefresh
        refresca()
    End Sub
End Class