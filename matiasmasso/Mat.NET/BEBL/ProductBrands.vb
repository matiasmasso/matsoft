Public Class ProductBrand
    Shared Function Find(oGuid As Guid) As DTOProductBrand
        Dim retval As DTOProductBrand = ProductBrandLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNom(oEmp As DTOEmp, sNom As String) As DTOProductBrand
        Dim retval As DTOProductBrand = ProductBrandLoader.FromNom(oEmp, sNom)
        Return retval
    End Function

    Shared Function Load(ByRef oBrand As DTOProductBrand) As Boolean
        Dim retval As Boolean = ProductBrandLoader.Load(oBrand)
        Return retval
    End Function

    Shared Function Logo(oBrand As DTOProductBrand) As ImageMime
        Return ProductBrandLoader.Logo(oBrand)
    End Function


    Shared Function LogoDistribuidorOficial(oBrand As DTOProductBrand) As Byte()
        Return ProductBrandLoader.LogoDistribuidorOficial(oBrand)
    End Function

    Shared Function Update(oProductBrand As DTOProductBrand, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductBrandLoader.Update(oProductBrand, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Brands)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductUrls)
        Return retval
    End Function

    Shared Function Delete(oProductBrand As DTOProductBrand, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductBrandLoader.Delete(oProductBrand, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Brands)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductUrls)
        Return retval
    End Function

    Shared Function LogoUrl(oBrand As DTOProductBrand, AbsoluteUrl As Boolean) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.TpaLogo, oBrand.Guid, AbsoluteUrl)
    End Function

    Shared Function EPubUrl(oBrand As DTOProductBrand, AbsoluteUrl As Boolean) As String
        Return UrlHelper.Doc(AbsoluteUrl, DTODocFile.Cods.TpaEPubBook, oBrand.Guid.ToString())
    End Function
End Class

Public Class ProductBrands

    Shared Function RoutingConstraints(oEmp As DTOEmp) As List(Of String)
        Return ProductBrandsLoader.RoutingConstraints(oEmp)
    End Function

    Shared Function Tree(oEmp As DTOEmp) As List(Of DTOProductBrand)
        Return ProductBrandsLoader.Tree(oEmp)
    End Function

    Shared Function All(oEmp As DTOEmp, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = ProductBrandsLoader.All(oEmp, IncludeObsolets)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oUser As DTOUser, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        If oUser Is Nothing Then
            retval = ProductBrandsLoader.All(oEmp, IncludeObsolets)
        Else
            Select Case oUser.Rol.Id
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    retval = FromCustomer(oUser)
                Case DTORol.Ids.Rep
                    Dim oRep As DTORep = BEBL.User.GetRep(oUser)
                    retval = BEBL.ProductBrands.OfficiallyDistributed(oRep)
                Case DTORol.Ids.Comercial, DTORol.Ids.SalesManager, DTORol.Ids.Admin, DTORol.Ids.SuperUser, DTORol.Ids.Marketing, DTORol.Ids.LogisticManager, DTORol.Ids.Operadora, DTORol.Ids.Accounts
                    retval = ProductBrandsLoader.All(oEmp, IncludeObsolets)
                Case DTORol.Ids.Manufacturer
                    retval = FromProveidor(oUser)
                Case Else
                    retval = ProductBrandsLoader.All(oEmp, IncludeObsolets)
            End Select
        End If
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Dim oSkus As List(Of DTOProductSku) = ProductSkusLoader.All(oCustomer)
        Dim retval As List(Of DTOProductBrand) = oSkus.Select(Function(x) x.category.Brand).Distinct.ToList
        Return retval
    End Function


    Shared Function GuidNoms(oUser As DTOUser) As List(Of DTOGuidNom) 'for web api
        Dim osrc As List(Of DTOProductBrand) = ProductBrandsLoader.All(oUser.Emp, False)
        If oUser IsNot Nothing Then
            Select Case oUser.Rol.Id
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    Dim oCustomers As List(Of DTOCustomer) = BEBL.User.GetCustomers(oUser)
                    If oCustomers.Count > 0 Then
                        Dim oBlockedProducts As List(Of DTOCliProductBlocked) = BEBL.CliProductsBlocked.All(oCustomers(0))
                        For i As Integer = osrc.Count - 1 To 0 Step -1
                            If Not BEBL.CliProductsBlocked.IsAllowed(oBlockedProducts, osrc(i)) Then
                                osrc.RemoveAt(i)
                            End If
                        Next
                    End If
                Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                    Dim oRep As DTORep = BEBL.User.GetRep(oUser)
                    osrc = BEBL.ProductBrands.OfficiallyDistributed(oRep)
                Case DTORol.Ids.Manufacturer
                    osrc = FromProveidor(oUser)
            End Select
        End If

        Dim retval As New List(Of DTOGuidNom)
        Dim oVarios As DTOProductBrand = DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios)
        For Each oBrand As DTOProductBrand In osrc
            If oBrand.UnEquals(oVarios) Then
                retval.Add(New DTOGuidNom(oBrand.Guid, oBrand.nom.Esp))
            End If
        Next
        Return retval
    End Function

    Shared Function OfficiallyDistributed(oRep As DTORep) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = ProductBrandsLoader.OfficiallyDistributed(oRep)
        Return retval
    End Function

    Shared Function FromCustomer(oUser As DTOUser) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = ProductBrandsLoader.FromCustomer(oUser.Emp, oUser)
        Return retval
    End Function

    Shared Function FromCustomer(oCustomer As DTOContact) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = ProductBrandsLoader.FromCustomer(oCustomer)
        Return retval
    End Function

    Shared Function FromProveidor(oUser As DTOUser, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = ProductBrandsLoader.FromProveidor(oUser, IncludeObsolets)
        Return retval
    End Function

    Shared Function FromProveidor(oProveidor As DTOProveidor, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Dim retval As List(Of DTOProductBrand) = ProductBrandsLoader.FromProveidor(oProveidor, IncludeObsolets)
        Return retval
    End Function

End Class
