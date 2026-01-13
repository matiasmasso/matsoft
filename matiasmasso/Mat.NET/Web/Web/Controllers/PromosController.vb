Public Class PromosController
    Inherits _MatController


    Function Index() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oUser As DTOUser = GetSession.User
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Dim oPromos As List(Of DTOPromo) = BLL.BLLPromos.All(oUser, False)
            Select Case oUser.Rol.Id
                Case DTORol.Ids.Unregistered
                    retval = LoginOrView("Promos")
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Operadora, DTORol.Ids.Comercial, DTORol.Ids.Rep, DTORol.Ids.Cli, DTORol.Ids.Manufacturer
                    retval = View("Promos", oPromos)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Function Promo(guid As Guid) As ActionResult
        Dim retval As ActionResult = Nothing
        If guid <> Nothing Then
            Dim oPromo As DTOPromo = BLL.BLLPromo.Find(guid)
            Dim oBrand As DTOProductBrand = BLL.BLLProduct.Brand(oPromo.Product)
            Dim oUser As DTOUser = GetSession.User
            oPromo.Distributors = BLL.BLLProductDistributors.DistribuidorsOficials(oUser, oBrand, oPromo).FindAll(Function(x) x.Promo = True)
            retval = View(oPromo)
        Else
            retval = MyBase.UnauthorizedView()
        End If
        Return retval
    End Function

    Function FullPromo(guid As Guid) As ActionResult
        Dim retval As ActionResult = Nothing
        If guid <> Nothing Then
            Dim oPromo As DTOPromo = BLL.BLLPromo.Find(guid)
            Dim oUser As DTOUser = GetSession.User
            Dim oBrand As DTOProductBrand = BLL.BLLProduct.Brand(oPromo.Product)
            oPromo.Distributors = BLL.BLLProductDistributors.DistribuidorsOficials(oUser, oBrand, oPromo)
            ViewData("AllDist") = True
            retval = View("promo", oPromo)
        Else
            retval = MyBase.UnauthorizedView()
        End If
        Return retval
    End Function

    Function TrilogyColors() As ActionResult
        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep,
                                               DTORol.Ids.Cli})
            Case AuthResults.success

                Dim oSkus As New List(Of DTOProductSku)
                oSkus.Add(BLLProductSku.Find(New Guid("6F025F8D-F766-47A2-AEF1-65FF8FCD8340")))
                oSkus.Add(BLLProductSku.Find(New Guid("42C061DD-8BA2-4913-A6F7-AC8CEB6D6469")))
                oSkus.Add(BLLProductSku.Find(New Guid("92F3CA89-BB05-4FCA-B2C9-7BE7EBC3EF0D")))
                oSkus.Add(BLLProductSku.Find(New Guid("359826CF-FAD5-4285-86D0-04DEDF5D8533")))
                oSkus.Add(BLLProductSku.Find(New Guid("1FBB7D10-DDE5-49D6-AC2F-27D547DD1BAC")))
                retval = View(oSkus)
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Public Function TrilogyColorsUpdate(data As String) As String
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim o As Object = jss.Deserialize(Of Object)(data)
        Dim sb As New System.Text.StringBuilder
        Dim sCust As String = ""
        For i As Integer = 0 To o.length - 1
            sCust = o(i)("cli")
            Dim sGuid As String = o(i)("sku")
            Dim iQty As Integer = o(i)("qty")
            Dim oSku As DTOProductSku = BLLProductSku.Find(New Guid(sGuid))
            sb.AppendLine(iQty & " x " & oSku.NomLlarg)
        Next
        Dim oContact As DTOContact = BLLContact.Find(New Guid(sCust))
        MailHelper.SendMail(WellKnownRecipients.Info, "Promo Trilogy Colors " & oContact.FullNom, sb.ToString)
        Return ""
    End Function

    Function TrilogyColorsRepsContest() As ActionResult
        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.Manufacturer,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep})
            Case AuthResults.success
                Select Case MyBase.GetSession.Rol.Id
                    Case DTORol.Ids.Manufacturer
                        Dim oManufacturer As DTOProveidor = BLLUser.GetProveidor(MyBase.GetSession.User)
                        Dim oInglesina As DTOProveidor = BLLProveidor.WellKnown(DTOProveidor.WellKnown.Inglesina)
                        If oManufacturer.Equals(oInglesina) Then
                            retval = View()
                        Else
                            retval = MyBase.UnauthorizedView()
                        End If
                    Case Else
                        retval = View()
                End Select
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function


End Class