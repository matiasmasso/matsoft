Public Class Frm_Banners

    Private Sub Frm_Banners_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oBanners As List(Of DTOBanner) = BannersLoader.All
        Xl_Banners1.Load(oBanners)
    End Sub

    Private Sub Xl_Banners1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Banners1.RequestToAddNew
        Dim oBanner As DTOBanner = BLL_Banner.NewBanner(Today)
        Dim oFrm As New Frm_Banner(oBanner)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest()
        Dim oBanners As List(Of DTOBanner) = BannersLoader.All
        Xl_Banners1.Load(oBanners)
    End Sub
End Class