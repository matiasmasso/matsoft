Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports BEBL

Public Class WtbolController
    Inherits _BaseController

    <HttpGet>
    <Route("api/wtbol/model/{user}")>
    Public Function Model(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim value = BEBL.Wtbol.Model(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el model de Wtbol")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/wtbol/baskets/{user}")>
    Public Function baskets(user As Guid) As List(Of DTOWtbolBasket)
        Dim retval As New List(Of DTOWtbolBasket)
        Dim oUser = BEBL.User.Find(user)
        If oUser IsNot Nothing AndAlso oUser.Rol.id = DTORol.Ids.superUser Then
            retval = BEBL.WtbolBaskets.All
        End If
        Return retval
    End Function

    <HttpGet>
    <Route("api/wtbol/stocks/{site}")>
    Public Function downloadStocks(site As String) As FileContentResult
        Dim oSite As DTOWtbolSite = BEBL.Wtbolsite.FromMerchantId(site)
        Dim src As String = BEBL.WtbolStocks.Xml(oSite)
        Dim oStream As Byte() = System.Text.Encoding.UTF8.GetBytes(src)
        'Dim retval As New FileContentResult(oStream, "text/xml")
        Dim retval As New FileContentResult(oStream, "text/xml")
        Return retval
    End Function

    <HttpPost>
    <Route("api/wtbol/upload/landingpages")>
    Public Function uploadLandingPages(value As DTOWtbolInputLandingPages) As DTOTaskResult
        Dim exs As New List(Of Exception)
        Dim retval As DTOTaskResult
        Dim removedCount As Integer
        Dim addedCount As Integer
        Dim msg As String = ""
        Dim oSite As DTOWtbolSite = Nothing

        If value.Site Is Nothing Then
            exs.Add(New Exception("Comercio sin identificar"))
            msg = String.Format("error al subir las landing pages")
        ElseIf value.Site.Guid = Nothing Then
            exs.Add(New Exception("El parámetro Guid de identificador de comercio está vacío"))
            msg = String.Format("error al subir las landing pages")
        ElseIf value.Items Is Nothing Then
            exs.Add(New Exception("Falta la lista de productos"))
            msg = String.Format("error al subir las landing pages")
        ElseIf value.Items.Count = 0 Then
            exs.Add(New Exception("La lista de productos está vacía"))
            msg = String.Format("error al subir las landing pages")
        Else
            oSite = BEBL.Wtbolsite.Find(value.Site.Guid)
            If oSite Is Nothing Then
                msg = String.Format("error al subir las landing pages")
                Dim sMsg As String = String.Format("MerchantId '{0}' no reconocido", value.Site.MerchantId)
                exs.Add(New Exception(sMsg))
            Else
                Dim DtFch As Date = DTO.GlobalVariables.Now()
                Dim oSkus = BEBL.ProductCatalog.Refs()
                Dim oEanSkus = oSkus.Where(Function(x) x.ean13 IsNot Nothing).ToList

                Dim oLandingPages As New List(Of DTOWtbolLandingPage)

                Dim iEmptyItems As Integer
                Dim iDuplicates As Integer
                For Each item In value.Items
                    If item.Sku = "" And item.Url = "" Then
                        iEmptyItems += 1
                    ElseIf item.Sku = "" Then
                        Dim sMsg As String = String.Format("Producto sin código Ean en Landing Page '{0}'", item.Url)
                        exs.Add(New Exception(sMsg))
                    ElseIf item.Url = "" Then
                        Dim sMsg As String = String.Format("Producto con Ean '{0}' sin Landing Page", item.Sku)
                        exs.Add(New Exception(sMsg))
                    Else
                        Dim oSku As DTOProductSku = oEanSkus.FirstOrDefault(Function(x) x.ean13.value = item.Sku)
                        If oSku Is Nothing Then
                            Dim sMsg As String = String.Format("Ean '{0}' fuera de catálogo", item.Sku)
                            exs.Add(New Exception(sMsg))
                        Else
                            If oLandingPages.Any(Function(x) x.Product.Equals(oSku)) Then
                                iDuplicates += 1
                            Else
                                Dim oLandingPage As New DTOWtbolLandingPage
                                With oLandingPage
                                    .Product = oSku
                                    .Uri = New Uri(item.Url)
                                    .Site = oSite
                                    .FchCreated = DtFch
                                End With
                                oLandingPages.Add(oLandingPage)
                            End If
                        End If
                    End If
                Next

                msg = String.Format("{0} productos con sus landing page registrados correctamente", oLandingPages.Count)

                If iEmptyItems > 0 Then
                    Dim sMsg As String = String.Format("{0} productos vacíos (sin código Ean ni Landing Page)", iEmptyItems)
                    exs.Add(New Exception(sMsg))
                End If
                If iDuplicates > 0 Then
                    Dim sMsg As String = String.Format("{0} productos duplicados no han sido registrados", iDuplicates)
                    exs.Add(New Exception(sMsg))
                End If


                Dim oSiteBrandLandingPages = oSite.LandingPages.Where(Function(x) TypeOf x.Product Is DTOProductBrand).ToList
                Dim oSiteCategoryLandingPages = oSite.LandingPages.Where(Function(x) TypeOf x.Product Is DTOProductCategory).ToList
                Dim oSiteSkuLandingPages = oSite.LandingPages.Where(Function(x) TypeOf x.Product Is DTOProductSku).ToList

                'remove site.landingpages not present in submitted file
                For i = oSiteSkuLandingPages.Count - 1 To 0 Step -1
                    Dim oSiteSkuLandingPage = oSiteSkuLandingPages(i)
                    If Not oLandingPages.Any(Function(x) x.Product.guid.Equals(oSiteSkuLandingPage.Product.Guid) And x.Uri.ToString = oSiteSkuLandingPage.Uri.ToString) Then
                        oSiteSkuLandingPages.RemoveAt(i)
                        removedCount += 1
                    End If
                Next

                'add submitted landing pages not present in site
                For Each oLandingPage In oLandingPages
                    If Not oSite.HasLandingPage(oLandingPage.Product, oLandingPage.Uri.ToString()) Then
                        oSiteSkuLandingPages.Add(oLandingPage)
                        addedCount += 1
                    End If
                Next

                oSite.LandingPages = oSiteBrandLandingPages
                oSite.LandingPages.AddRange(oSiteCategoryLandingPages)
                oSite.LandingPages.AddRange(oSiteSkuLandingPages)
                BEBL.Wtbolsite.Update(oSite, exs)
            End If
        End If

        Dim s = JsonHelper.Serialize(value, exs)
        MailMessageHelper.MailAdmin(New DTOEmp(1), String.Format("Uploaded Landing Pages. Added={0} Removed={1}", addedCount, removedCount), msg & vbCrLf & s, exs)

        If exs.Count = 0 Then
            retval = DTOTaskResult.SuccessResult(msg)
        Else
            retval = DTOTaskResult.FailResult(exs, msg)
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/wtbol/upload/stocks")>
    Public Function uploadStocks(value As DTOWtbolInputStocks) As DTOTaskResult
        Dim exs As New List(Of Exception)
        Dim retval As DTOTaskResult
        Dim msg As String = ""
        Dim oSite As DTOWtbolSite = Nothing

        If value.Site Is Nothing Then
            msg = String.Format("error al subir los stocks")
            exs.Add(New Exception("Comercio sin identificar"))
        ElseIf value.Site.Guid = Nothing Then
            msg = String.Format("error al subir los stocks")
            exs.Add(New Exception("El parámetro Guid de identificador de comercio está vacío"))
        ElseIf value.Items Is Nothing Then
            msg = String.Format("error al subir los stocks")
            exs.Add(New Exception("Falta la lista de productos"))
        ElseIf value.Items.Count = 0 Then
            msg = String.Format("error al subir los stocks")
            exs.Add(New Exception("La lista de productos está vacía"))
        Else
            oSite = BEBL.WtbolSite.Find(value.Site.Guid)
            If oSite Is Nothing Then
                msg = String.Format("error al subir los stocks")
                Dim sMsg As String = String.Format("MerchantId '{0}' no reconocido", value.Site.MerchantId)
                exs.Add(New Exception(sMsg))
            Else
                Dim DtFch As Date = DTO.GlobalVariables.Now()
                Dim oSkus = BEBL.ProductCatalog.Refs()
                Dim oEanSkus = oSkus.Where(Function(x) x.Ean13 IsNot Nothing).ToList
                Dim iEmptyItems As Integer
                Dim iDuplicates As Integer

                oSite.Stocks = New List(Of DTOWtbolStock)
                For Each item In value.Items
                    If item.Sku = "" Then
                        iEmptyItems += 1
                    ElseIf item.Stock = 0 Then
                        'ignorar productes sense stock
                    Else
                        Dim oSku As DTOProductSku = oEanSkus.FirstOrDefault(Function(x) x.Ean13.Value = item.Sku)
                        If oSku Is Nothing Then
                            Dim sMsg As String = String.Format("Ean '{0}' fuera de catálogo", item.Sku)
                            exs.Add(New Exception(sMsg))
                        Else
                            If oSite.Stocks.Any(Function(x) x.Sku.Equals(oSku)) Then
                                iDuplicates += 1
                            Else
                                Dim oStock As New DTOWtbolStock
                                With oStock
                                    .Sku = oSku
                                    .Stock = item.Stock
                                    .Price = DTOAmt.Factory(item.Price)
                                    .Site = oSite
                                    .FchCreated = DtFch
                                End With

                                oSite.Stocks.Add(oStock)
                            End If
                        End If
                    End If
                Next

                msg = String.Format("{0} productos en stock registrados correctamente", oSite.LandingPages.Count)


                If iEmptyItems > 0 Then
                    Dim sMsg As String = String.Format("{0} productos sin código Ean", iEmptyItems)
                    exs.Add(New Exception(sMsg))
                End If
                If iDuplicates > 0 Then
                    Dim sMsg As String = String.Format("{0} productos duplicados no han sido registrados", iDuplicates)
                    exs.Add(New Exception(sMsg))
                End If

                BEBL.WtbolStocks.Update(oSite, exs)
            End If
        End If

        If exs.Count = 0 Then
            retval = DTOTaskResult.SuccessResult(msg)
        Else
            retval = DTOTaskResult.FailResult(exs, msg)
        End If
        Return retval

    End Function

    <HttpGet>
    <Route("api/wtbol/pixel/{MerchantId}/{*Catchall}")>
    Public Sub pixel(<FromUri> MerchantId As String, Catchall As String)
        Dim oSite As DTOWtbolSite = BEBL.WtbolSite.FromMerchantId(MerchantId)
        If oSite IsNot Nothing Then
            BEBL.WtbolSite.Load(oSite)
            Dim oBasket As DTOWtbolBasket = DTOWtbolBasket.Factory(oSite)
            Dim params As String() = Catchall.Split("/")
            Dim j As Integer
            Do While j < params.Length
                Dim ean As String = params(j)
                If IsNumeric(ean) Then
                    Dim oEan = DTOEan.Factory(ean)
                    Dim oSku As DTOProductSku = BEBL.ProductSku.FromEan(oEan)
                    If oSku IsNot Nothing Then
                        Dim qty As Integer = params(j + 1)
                        Dim price As Decimal = params(j + 2) / 100
                        oBasket.AddItem(oSku, qty, price)
                    End If
                End If
                j += 3
            Loop
            Dim oCookie As HttpCookie = HttpContext.Current.Request.Cookies(DTOSession.CookieSessionName)

            Dim exs As New List(Of Exception)
            BEBL.WtbolBasket.Update(oBasket, exs)
        End If
    End Sub


End Class
