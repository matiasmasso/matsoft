Public Class ProductController
    Inherits _BaseController

    Public Sub New()

    End Sub

    <HttpGet>
    <Route("api/cataleg/{userguid}")>
    Public Function cataleg(userguid As String) As List(Of DUI.Brand)
        Dim retval As New List(Of DUI.Brand)
        Dim oUser As DTOUser = BLLUser.Find(New Guid(userguid))
        If oUser IsNot Nothing Then
            Dim oTarifa As DTOCustomerTarifa = BLLCustomerTarifa.Load(oUser)
            For Each oBrand As DTOProductBrand In oTarifa.Brands
                Dim DuiBrand As New DUI.Brand
                With DuiBrand
                    .Guid = oBrand.Guid
                    .Nom = oBrand.Nom
                    .Categories = New List(Of DUI.Category)
                End With
                retval.Add(DuiBrand)
                For Each oCategory As DTOProductCategory In oBrand.Categories
                    Dim DuiCategory As New DUI.Category
                    With DuiCategory
                        .Guid = oCategory.Guid
                        .Nom = oCategory.Nom
                        .Skus = New List(Of DUI.Sku)
                    End With
                    DuiBrand.Categories.Add(DuiCategory)
                    For Each oSku As DTOProductSku In oCategory.Skus
                        Dim DuiSku As New DUI.Sku
                        With DuiSku
                            .Guid = oSku.Guid
                            .Nom = oSku.NomCurt
                            .Stock = oSku.Stock - oSku.Clients - oSku.ClientsAlPot - oSku.ClientsEnProgramacio
                            .Moq = IIf(oSku.HeredaDimensions, IIf(oCategory.ForzarInnerPack, oCategory.InnerPack, 0), IIf(oSku.ForzarInnerPack, oSku.InnerPack, 0))
                            .Price = BLLAmt.Eur(oSku.Price)
                            .RRPP = BLLAmt.Eur(oSku.RRPP)
                        End With
                        DuiCategory.Skus.Add(DuiSku)
                    Next
                Next
            Next
        End If

        Return retval
    End Function


    <HttpPost>
    <Route("api/products/sprite")>
    Public Function sprite(data As DUI.Sprite) As System.Net.Http.HttpResponseMessage
        Dim exs As New List(Of Exception)

        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sJsonRequest As String = jss.Serialize(data)
        Dim sHash As String = CryptoHelper.GetSha256Hash(sJsonRequest)

        Dim oSprite As DTOSprite = BLLSprite.Find(sHash)
        If oSprite Is Nothing Then
            Dim oSkus As New List(Of DTOProductSku)
            For Each oGuid As Guid In data.guids
                oSkus.Add(New DTOProductSku(oGuid))
            Next
            oSprite = BLLSprite.Factory(oSkus, data.itemWidth, sHash, exs)
        End If

        Dim oBytes() As Byte = BLL.ImageHelper.GetByteArrayFromImg(oSprite.Image)
        Dim MS As New System.IO.MemoryStream(oBytes)
        Dim retval As New System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
        With retval
            .Content = New System.Net.Http.StreamContent(MS)
            .Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg")
        End With
        Return retval
    End Function


End Class

