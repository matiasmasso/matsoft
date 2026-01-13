Public Class Frm_ProductGallery
    Private _Product As DTOProduct

    Public Sub New(oProduct As DTOProduct)
        MyBase.New()
        Me.InitializeComponent()
        _Product = oProduct
        BLL.BLLProduct.load(oProduct)
        Me.Text = "Galería " & BLL.BLLProduct.FullNom(oProduct)
        refresca()
    End Sub

    Private Sub Xl_ProductHighResImages1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductHighResImages1.RequestToAddNew
        Dim item As New DTOHighResImage
        Dim oFrm As New Frm_HighResImage(item)

    End Sub

    Private Sub Xl_ProductHighResImages1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductHighResImages1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim items As List(Of DTOHighResImage) = BLL.BLLHighResImages.FromProduct(_Product)
        Xl_ProductHighResImages1.Load(items)
    End Sub
End Class