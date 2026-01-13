Public Class WebMenuGroup
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid, oEmp As DTOEmp, oUser As DTOUser, olang As DTOLang) As Task(Of DTOWebMenuGroup)
        Dim retval As DTOWebMenuGroup = Nothing
        Select Case oGuid.ToString
            Case DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Langs).ToString
                retval = DTOWebMenuGroup.LangsGroup(olang)
            Case DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Products).ToString
                retval = Await FEB2.WebMenuGroup.ProductsGroup(exs, oUser, olang)
            Case Else
                Dim oRolId As DTORol.Ids = DTORol.Ids.NotSet
                If oUser IsNot Nothing Then oRolId = oUser.Rol.Id
                retval = Await Api.Fetch(Of DTOWebMenuGroup)(exs, "WebMenuGroup", oGuid.ToString, oRolId)
        End Select
        Return retval
    End Function

    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid, oEmp As DTOEmp, oUser As DTOUser, olang As DTOLang) As DTOWebMenuGroup
        Dim retval As DTOWebMenuGroup = Nothing
        Select Case oGuid.ToString
            Case DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Langs).ToString
                retval = DTOWebMenuGroup.LangsGroup(olang)
            Case DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Products).ToString
                retval = FEB2.WebMenuGroup.ProductsGroupSync(exs, oUser, olang)
            Case Else
                Dim oRolId As DTORol.Ids = DTORol.Ids.NotSet
                If oUser IsNot Nothing Then oRolId = oUser.Rol.Id
                retval = Api.FetchSync(Of DTOWebMenuGroup)(exs, "WebMenuGroup", oGuid.ToString, oRolId)
        End Select
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWebMenuGroup As DTOWebMenuGroup) As Boolean
        If Not oWebMenuGroup.IsLoaded And Not oWebMenuGroup.IsNew Then
            Dim pWebMenuGroup = Api.FetchSync(Of DTOWebMenuGroup)(exs, "WebMenuGroup", oWebMenuGroup.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWebMenuGroup)(pWebMenuGroup, oWebMenuGroup, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oWebMenuGroup As DTOWebMenuGroup) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWebMenuGroup)(oWebMenuGroup, exs, "WebMenuGroup")
        oWebMenuGroup.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oWebMenuGroup As DTOWebMenuGroup) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWebMenuGroup)(oWebMenuGroup, exs, "WebMenuGroup")
    End Function

    Shared Async Function ProductsGroup(exs As List(Of Exception), oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As Task(Of DTOWebMenuGroup)

        Dim retval As New DTOWebMenuGroup(DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Products))
        retval.LangText = New DTOLangText("Productos", "Productes", "Products", "Produtos")

        Dim oCurrentBrand As DTOProductBrand = Nothing
        If oProduct IsNot Nothing Then
            oCurrentBrand = DTOProduct.Brand(oProduct)
        End If

        Dim oBrands = Await FEB2.ProductBrands.All(exs, oUser)

        'For Each oBrand As ProductBrand In oSession.Catalog.Brands.FindAll(Function(x) x.EnabledxConsumer = True)
        For Each oBrand As DTOProductBrand In oBrands.FindAll(Function(x) x.EnabledxConsumer = True)
            Dim skip As Boolean = False
            Select Case oLang.Id
                Case DTOLang.Ids.POR
                    If oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FisherPrice)) Then
                        skip = True
                    End If
                Case Else
            End Select

            If Not skip Then
                DTOWebMenuGroup.AddItem(retval, oBrand.Nom.Tradueix(oLang), "", "", "", oBrand.GetUrl(oLang, DTOProduct.Tabs.general), oBrand.Equals(oCurrentBrand))
            End If
        Next

        Return retval
    End Function
    Shared Function ProductsGroupSync(exs As List(Of Exception), oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As DTOWebMenuGroup

        Dim retval As New DTOWebMenuGroup(DTOWebMenuGroup.Wellknown(DTOWebMenuGroup.Wellknowns.Products))
        retval.LangText = New DTOLangText("Productos", "Productes", "Products", "Produtos")

        Dim oCurrentBrand As DTOProductBrand = Nothing
        If oProduct IsNot Nothing Then
            oCurrentBrand = DTOProduct.Brand(oProduct)
        End If

        Dim oBrands = FEB2.ProductBrands.AllSync(exs, oUser)

        'For Each oBrand As ProductBrand In oSession.Catalog.Brands.FindAll(Function(x) x.EnabledxConsumer = True)
        For Each oBrand As DTOProductBrand In oBrands.FindAll(Function(x) x.EnabledxConsumer = True)
            Dim skip As Boolean = False
            Select Case oLang.Id
                Case DTOLang.Ids.POR
                    If oBrand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FisherPrice)) Then
                        skip = True
                    End If
                Case Else
            End Select

            If Not skip Then
                DTOWebMenuGroup.AddItem(retval, oBrand.Nom.Tradueix(oLang), "", "", "", oBrand.GetUrl(oLang, DTOProduct.Tabs.general), oBrand.Equals(oCurrentBrand))
            End If
        Next

        Return retval
    End Function

    Shared Function Url(value As DTOWebMenuGroup, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If value.Url = "" Then
            retval = UrlHelper.Factory(AbsoluteUrl, "menu", value.Guid.ToString())
        Else
            retval = value.Url
        End If
        Return retval
    End Function
End Class

Public Class WebMenuGroups
    Inherits _FeblBase

    Shared Function Test(oLang As DTOLang) As List(Of DTOWebMenuGroup)
        Dim retval As New List(Of DTOWebMenuGroup)
        retval.Add(DTOWebMenuGroup.LangsGroup(oLang))
        retval.Add(DTOWebMenuGroup.LangsGroup(oLang))
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), Optional oUser As DTOUser = Nothing, Optional JustActiveItems As Boolean = False) As Task(Of List(Of DTOWebMenuGroup))
        Return Await Api.Fetch(Of List(Of DTOWebMenuGroup))(exs, "WebMenuGroups", OpcionalGuid(oUser), OpcionalBool(JustActiveItems))
    End Function
    Shared Function AllSync(exs As List(Of Exception), Optional oUser As DTOUser = Nothing, Optional JustActiveItems As Boolean = False) As List(Of DTOWebMenuGroup)
        Return Api.FetchSync(Of List(Of DTOWebMenuGroup))(exs, "WebMenuGroups", OpcionalGuid(oUser), OpcionalBool(JustActiveItems))
    End Function

    Shared Async Function ForWebSite(oEmp As DTOEmp, oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As Task(Of List(Of DTOWebMenuGroup))
        Dim exs As New List(Of Exception)
        Dim retval As New List(Of DTOWebMenuGroup)

        Dim oProductGroup = Await FEB2.WebMenuGroup.ProductsGroup(exs, oUser, oLang, oProduct)
        retval.Add(oProductGroup)

        If oUser IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
            If oUser.Rol.IsProfesional Then
                Dim oProfessionalRange = Await FEB2.WebMenuGroups.All(exs, oUser, JustActiveItems:=True)
                retval.AddRange(oProfessionalRange)
            Else
                If Await FEB2.Raffles.RafflesCount(exs, oLang) > 0 Then
                    retval.Add(DTOWebMenuGroup.SorteosGroup())
                End If
                retval.Add(DTOWebMenuGroup.NoticiasGroup())
                retval.AddRange(AllSync(exs, oUser, JustActiveItems:=True))
            End If
        Else
            If Await FEB2.Raffles.RafflesCount(exs, oLang) > 0 Then
                retval.Add(DTOWebMenuGroup.SorteosGroup())
            End If
            retval.Add(DTOWebMenuGroup.NoticiasGroup())
            retval.Add(DTOWebMenuGroup.LangsGroup(oLang))
            retval.Add(DTOWebMenuGroup.SignUpMenuGroup())
            retval.Add(DTOWebMenuGroup.LoginGroup())
        End If


        Return retval
    End Function

    Shared Function ForWebSiteSync(oEmp As DTOEmp, oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As List(Of DTOWebMenuGroup)
        Dim retval As New List(Of DTOWebMenuGroup)
        Dim exs As New List(Of Exception)
        retval.Add(FEB2.WebMenuGroup.ProductsGroupSync(exs, oUser, oLang, oProduct))

        If oUser IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
            If oUser.Rol.IsProfesional Then
                retval.AddRange(AllSync(exs, oUser, JustActiveItems:=True))
            Else
                If FEB2.Raffles.RafflesCountSync(exs, oLang) > 0 Then
                    retval.Add(DTOWebMenuGroup.SorteosGroup())
                End If
                retval.Add(DTOWebMenuGroup.NoticiasGroup())
                retval.AddRange(AllSync(exs, oUser, JustActiveItems:=True))
            End If
        Else
            If FEB2.Raffles.RafflesCountSync(exs, oLang) > 0 Then
                retval.Add(DTOWebMenuGroup.SorteosGroup())
            End If
            retval.Add(DTOWebMenuGroup.NoticiasGroup())
            retval.Add(DTOWebMenuGroup.LangsGroup(oLang))
            retval.Add(DTOWebMenuGroup.SignUpMenuGroup())
            retval.Add(DTOWebMenuGroup.LoginGroup())
        End If

        If Debugger.IsAttached Then
            Dim oDeveolperGroup As New DTOWebMenuGroup()
            oDeveolperGroup.LangText = New DTOLangText("Desarrollador", "Desenvolupador", "Developer")
            retval.Add(oDeveolperGroup)

            Dim oUseLocalApi As New DTOWebMenuItem
            oDeveolperGroup.Items.Add(oUseLocalApi)
            If FEB2.Api.UseLocalApi Then
                oUseLocalApi.LangText = New DTOLangText("cambia a Api remota", "canvia a Api remota", "use remote Api")
                oUseLocalApi.Url = "/uselocalapi/0"
            Else
                oUseLocalApi.LangText = New DTOLangText("cambia a Api local", "cambia a Api local", "use local Api")
                oUseLocalApi.Url = "/uselocalapi/1"
            End If
        End If


        Return retval
    End Function

End Class
