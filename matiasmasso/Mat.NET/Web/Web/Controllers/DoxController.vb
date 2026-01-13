Public Class DoxController
    Inherits _MatController

    Public Async Function Index(sBase64Params As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)

        Dim oParams As Dictionary(Of String, String) = CryptoHelper.FromUrlFriendlyBase64Json(sBase64Params)
        Dim oCod As DTODocFile.Cods = oParams.First.Value
        Select Case oCod
            Case DTODocFile.Cods.JSONSalePoints
                retval = Await JSONSalePoints(oParams)
            Case DTODocFile.Cods.XMLSalePoints
                retval = Await XMLSalePoints(oParams)
            Case DTODocFile.Cods.StocksExcel
                Dim oExcelSheet = Await FEB2.SkuStocks.Excel(exs, GlobalVariables.Emp, oParams)
                retval = MyBase.ExcelResult(oExcelSheet)
            Case DTODocFile.Cods.TarifaCsv
                'obsolet
            Case DTODocFile.Cods.rawdatalast12monthscsv
                Dim oUser = ContextHelper.GetUser()
                Dim oAuthResult As AuthResults = MyBase.Authorize({DTORol.Ids.Manufacturer})
                If oAuthResult = AuthResults.success Then
                    Dim oProveidor = Await FEB2.User.GetProveidor(oUser, exs)
                    If oProveidor.Equals(DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)) Then
                        Dim items = Await FEB2.SellOut.RawDataLast12Months(exs, oProveidor)
                        Dim oCsv As DTOCsv = DTOSellOut.RawDataLast12MonthsCsv(items)
                        retval = MyBase.CsvResult(oCsv)
                    Else
                        retval = MyBase.UnauthorizedView
                    End If
                Else
                    MyBase.LoginOrDeny(oAuthResult)
                End If
            Case DTODocFile.Cods.MaybornSalesExcel
                Dim oAuthResult As AuthResults = MyBase.Authorize({DTORol.Ids.Manufacturer})
                If oAuthResult = AuthResults.success Then
                    Dim oUser = ContextHelper.GetUser()
                    Dim oProveidor = Await FEB2.User.GetProveidor(oUser, exs)
                    If oProveidor.Equals(DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)) Then
                        Dim oCsv As DTOCsv = Await FEB2.Mayborn.SalesCsv(exs)
                        retval = MyBase.CsvResult(oCsv)
                    Else
                        retval = MyBase.UnauthorizedView
                    End If
                Else
                    MyBase.LoginOrDeny(oAuthResult)
                End If
            Case DTODocFile.Cods.IbanMandato
                If oParams.Count > 1 Then
                    Dim sGuid As String = oParams("guid")
                    If GuidHelper.IsGuid(sGuid) Then
                        Dim oGuid As New Guid(sGuid)
                        Dim oIban = Await FEB2.Iban.Find(oGuid, exs)
                        Dim oDocFile As DTODocFile = oIban.DocFile
                        If oDocFile Is Nothing Then
                            If FEB2.Contact.Load(oIban.titular, exs) Then
                                Dim sSwift = FEB2.Iban.Swift(oIban)
                                Dim oSepaTexts = Await FEB2.SepaTexts.All(exs)
                                oDocFile = LegacyHelper.LegacyDivers.SepaMandato(exs, GlobalVariables.Emp, oIban, sSwift, oSepaTexts, oIban.titular.lang)
                            End If
                        Else
                            FEB2.DocFile.Load(exs, oDocFile, LoadStream:=True)
                        End If
                        retval = MyBase.FileResult(oDocFile, "M+O Mandato Bancario.pdf")
                    End If
                End If
            Case DTODocFile.Cods.IbanMandatoManual
                Dim oLang As DTOLang = DTOLang.ESP
                If oParams.Count > 1 Then
                    oLang = DTOLang.Factory(oParams("Lang"))
                End If
                Dim oGuid As Guid = Guid.NewGuid
                Dim oSepaTexts = Await FEB2.SepaTexts.All(exs)
                Dim oMandato As New LegacyHelper.PdfSepaMandatoManual(GlobalVariables.Emp, oGuid, oLang)
                Dim oByteArray() As Byte = oMandato.Pdf(oSepaTexts)
                Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray)

                Dim sFilename As String = String.Format("M+O Mandato Bancario {0}.pdf", oGuid.ToString())
                retval = MyBase.FileResult(oDocFile, sFilename)

            Case DTODocFile.Cods.zipgallerydownloads
                If oParams.Count > 1 Then
                    Dim oProduct = Await FEB2.Product.Find(exs, New Guid(oParams("product")))
                    Dim items = Await FEB2.MediaResources.All(exs, oProduct)
                    Dim oStream As IO.MemoryStream = LegacyHelper.MediaResourceZip.Zip(items, exs)
                    If exs.Count = 0 Then
                        retval = MyBase.FileResult(oStream.ToArray, "MatiasMasso.Zip")
                    Else
                        retval = View("Error")
                    End If
                End If
            Case DTODocFile.Cods.Forecast
                Dim oAuthResult As AuthResults = MyBase.Authorize({DTORol.Ids.Manufacturer})
                If oAuthResult = AuthResults.success Then
                    Dim oCsv As DTOCsv = Await FEB2.Mayborn.SalesCsv(exs)
                    retval = MyBase.CsvResult(oCsv)
                Else
                    MyBase.LoginOrDeny(oAuthResult)
                End If
            Case DTODocFile.Cods.SellOutPerChannel
                If oParams.Count > 1 Then
                    Dim user As DTOUser = ContextHelper.GetUser
                    Dim year As Integer = oParams("year")
                    Dim oExcelBook As ExcelHelper.Book = Await FEB2.ProductDistributors.PerChannel(exs, user, year)
                    retval = LoginOrFile(oExcelBook)
                End If
            Case DTODocFile.Cods.ExcelSalePoints
                If oParams.Count > 1 Then
                    Dim oProveidor = Await FEB2.Proveidor.Find(New Guid(oParams("proveidor")), exs)
                    If exs.Count = 0 Then
                        Dim items As List(Of DTOProductDistributor) = Await FEB2.StoreLocator.Distributors(exs, oProveidor)
                        If exs.Count = 0 Then
                            Dim oExcelSheet As ExcelHelper.Sheet = DTOProductDistributor.Excel(items)
                            retval = ExcelResult(oExcelSheet)
                        End If

                    End If
                End If

        End Select
        Return retval
    End Function



    Private Async Function JSONSalePoints(oParams As Dictionary(Of String, String)) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oGuid As New Guid(oParams("brand"))
        Dim oBrand As New DTOProductBrand(oGuid)
        Dim oDistributors = Await FEB2.ProductDistributors.FromBrand(exs, oBrand)
        Dim oJSONArray As New List(Of Object)
        For Each oDistributor As DTOProductRetailer In oDistributors
            oJSONArray.Add(New With {
                           .id = oDistributor.Id,
                           .Country = oDistributor.Country,
                           .region = oDistributor.Region,
                           .location = oDistributor.Location,
                           .address = oDistributor.Address.Replace(vbCrLf, " - "),
                           .nom = oDistributor.Name,
                           .Zip = oDistributor.ZipCod,
                           .tel = oDistributor.Tel
                           })
        Next
        Dim retval As JsonResult = MyBase.Json(oJSONArray, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Private Async Function XMLSalePoints(oParams As Dictionary(Of String, String)) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oGuid As New Guid(oParams("brand"))
        Dim oBrand As New DTOProductBrand(oGuid)
        Dim oDistributors = Await FEB2.ProductDistributors.FromBrand(exs, oBrand)

        Dim xmlDoc As New System.Xml.XmlDocument
        Dim itemsElement As System.Xml.XmlElement = xmlDoc.CreateElement("items")
        xmlDoc.AppendChild(itemsElement)

        For Each oDistributor As DTOProductRetailer In oDistributors
            Dim itemElement As System.Xml.XmlElement = xmlDoc.CreateElement("item")
            itemsElement.AppendChild(itemElement)

            With oDistributor
                Dim idElement As System.Xml.XmlElement = xmlDoc.CreateElement("id")
                idElement.InnerText = oDistributor.Id
                itemElement.AppendChild(idElement)

                Dim countryElement As System.Xml.XmlElement = xmlDoc.CreateElement("country")
                countryElement.InnerText = oDistributor.Country
                itemElement.AppendChild(countryElement)

                Dim regionElement As System.Xml.XmlElement = xmlDoc.CreateElement("region")
                regionElement.InnerText = oDistributor.Region
                itemElement.AppendChild(regionElement)

                Dim locationElement As System.Xml.XmlElement = xmlDoc.CreateElement("location")
                locationElement.InnerText = oDistributor.Location
                itemElement.AppendChild(locationElement)

                Dim addressElement As System.Xml.XmlElement = xmlDoc.CreateElement("address")
                addressElement.InnerText = .Address.Replace(vbCrLf, " - ")
                itemElement.AppendChild(addressElement)

                Dim nomElement As System.Xml.XmlElement = xmlDoc.CreateElement("nom")
                nomElement.InnerText = oDistributor.Name
                itemElement.AppendChild(nomElement)

                Dim zipElement As System.Xml.XmlElement = xmlDoc.CreateElement("zip")
                zipElement.InnerText = .ZipCod
                itemElement.AppendChild(zipElement)

                Dim telElement As System.Xml.XmlElement = xmlDoc.CreateElement("tel")
                telElement.InnerText = oDistributor.Tel
                itemElement.AppendChild(telElement)

            End With

        Next
        Dim retval As New XmlResult(xmlDoc)
        Return retval
    End Function

End Class
