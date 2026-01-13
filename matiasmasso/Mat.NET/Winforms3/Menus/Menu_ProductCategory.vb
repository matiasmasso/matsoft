Public Class Menu_ProductCategory
    Inherits Menu_Base

    Private _ProductCategory As DTOProductCategory
    Private _ProductCategories As List(Of DTOProductCategory)

#Region "Constructors"
    Public Sub New(ByVal oProductCategory As DTOProductCategory)
        MyBase.New()
        _ProductCategory = oProductCategory
        _ProductCategories = New List(Of DTOProductCategory)
        _ProductCategories.Add(_ProductCategory)

        AddMenuItems()
    End Sub

    Public Sub New(ByVal oProductCategories As List(Of DTOProductCategory))
        MyBase.New()
        _ProductCategory = oProductCategories(0)
        _ProductCategories = oProductCategories

        AddMenuItems()
    End Sub


#End Region


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_LangText())
        MyBase.AddMenuItem(MenuItem_Atlas())
        MyBase.AddMenuItem(MenuItem_Wtbols())
        MyBase.AddMenuItem(MenuItem_Incidencies())
        MyBase.AddMenuItem(MenuItem_Repeticions())
        MyBase.AddMenuItem(MenuItem_RankingCustomers())
        MyBase.AddMenuItem(MenuItem_RankingProducts())
        MyBase.AddMenuItem(MenuItem_StoreLocator())
        MyBase.AddMenuItem(MenuItem_Sellout())
        MyBase.AddMenuItem(MenuItem_Forecast())
        MyBase.AddMenuItem(MenuItem_Historial())
        MyBase.AddMenuItem(MenuItem_Incentius())
        MyBase.AddMenuItem(MenuItem_Delete())
        MyBase.AddMenuItem(MenuItem_Web())
        MyBase.AddMenuItem(MenuItem_Copy())
        MyBase.AddMenuItem(MenuItem_Circulars())

        Select Case Current.Session.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.marketing, DTORol.Ids.operadora
                MyBase.AddMenuItem(MenuItem_Advanced())
        End Select
    End Sub

#Region "Items"

#End Region

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_LangText() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = Current.Session.Tradueix("nombres y descripciones", "noms i descripcions", "names & descriptions")
        AddHandler oMenuItem.Click, AddressOf Do_LangText
        Return oMenuItem
    End Function

    Private Function MenuItem_Atlas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Atlas"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Atlas
        Return oMenuItem
    End Function

    Private Function MenuItem_Wtbols() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "botigues online"
        oMenuItem.Enabled = _ProductCategories.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Wtbols
        Return oMenuItem
    End Function

    Private Function MenuItem_Incidencies() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Incidencies"
        oMenuItem.Image = My.Resources.Spv
        AddHandler oMenuItem.Click, AddressOf Do_Incidencies
        Return oMenuItem
    End Function

    Private Function MenuItem_Repeticions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Repeticions"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Repeticions
        Return oMenuItem
    End Function

    Private Function MenuItem_RankingCustomers() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ranking clients"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_RankingCustomers
        Return oMenuItem
    End Function

    Private Function MenuItem_RankingProducts() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ranking productes"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ProductRanking
        Return oMenuItem
    End Function

    Private Function MenuItem_StoreLocator() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Store Locator"
        oMenuItem.Enabled = _ProductCategories.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_StoreLocator
        Return oMenuItem
    End Function

    Private Function MenuItem_Sellout() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sellout"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Sellout
        Return oMenuItem
    End Function

    Private Function MenuItem_Forecast() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Forecast"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Forecast
        Return oMenuItem
    End Function

    Private Function MenuItem_Historial() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Historial"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Historial
        Return oMenuItem
    End Function

    Private Function MenuItem_Incentius() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "incentius"
        AddHandler oMenuItem.Click, AddressOf Do_Incentius
        Return oMenuItem
    End Function


    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        oMenuItem.Image = My.Resources.browse
        With oMenuItem.DropDownItems
            .Add(MenuItem_Browse())
            .Add(MenuItem_Browse(DTOLang.ESP))
            .Add(MenuItem_Browse(DTOLang.CAT))
            .Add(MenuItem_Browse(DTOLang.ENG))
            .Add(MenuItem_Browse(DTOLang.POR))
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Copy() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar"
        oMenuItem.Image = My.Resources.Copy
        With oMenuItem.DropDownItems
            .Add(MenuItem_CopyLink)
            .Add(MenuItem_CopyUrlBuilderEmails())
            .Add(MenuItem_CopyUrlCompatibility())
            .Add(MenuItem_UrlBuilderBlogComments())
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse(Optional oLang As DTOLang = Nothing) As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If oLang Is Nothing Then
            oMenuItem.Text = "*"
        Else
            oMenuItem.Text = oLang.Tag
        End If
        oMenuItem.Tag = oLang
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Enllaç a landing page"
        With oMenuItem.DropDownItems
            .Add(MenuItem_CopyLink("*"))
            .Add(MenuItem_CopyLink("ESP", DTOLang.ESP()))
            .Add(MenuItem_CopyLink("CAT", DTOLang.CAT()))
            .Add(MenuItem_CopyLink("ENG", DTOLang.ENG()))
            .Add(MenuItem_CopyLink("POR", DTOLang.POR()))
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink(caption As String, Optional oLang As DTOLang = Nothing) As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = caption
        oMenuItem.Image = My.Resources.Copy
        oMenuItem.Tag = oLang
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyUrlBuilderEmails() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç per email"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_UrlBuilderEmails
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyUrlCompatibility() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a llista de compatibilitat"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyUrlCompatibility
        Return oMenuItem
    End Function


    Private Function MenuItem_UrlBuilderBlogComments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç per comentari blog"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_UrlBuilderBlogComments
        Return oMenuItem
    End Function


    Private Function MenuItem_Circulars() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Circulars..."
        oMenuItem.Image = My.Resources.MailSobreGroc
        Select Case Current.Session.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.admin
                ' oMenuItem.DropDownItems.Add(MenuItem_ExcelClientsSortits)
        End Select
        Return oMenuItem
    End Function





    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avançat"
        With oMenuItem.DropDownItems
            .Add(SubMenuItem_CopyGuid)
            .Add(SubMenuItem_SortSkus)
            .Add(SubMenuItem_Plugins)
        End With
        Return oMenuItem
    End Function

    Private Function SubMenuItem_SortSkus() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ordre dels productes"
        AddHandler oMenuItem.Click, AddressOf Do_SkusSort
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Plugins() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "plugins"
        With oMenuItem.DropDownItems
            .Add(SubMenuItem_PluginCollection)
            .Add(SubMenuItem_PluginAccessories)
            .Add(SubMenuItem_PluginStoreLocator)
        End With

        Return oMenuItem
    End Function

    Private Function SubMenuItem_PluginAccessories() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "accessoris"
        AddHandler oMenuItem.Click, AddressOf Do_CopyPluginAccessories
        Return oMenuItem
    End Function

    Private Function SubMenuItem_PluginCollection() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "col·lecció"
        AddHandler oMenuItem.Click, AddressOf Do_CopyPluginCollection
        Return oMenuItem
    End Function


    Private Function SubMenuItem_PluginStoreLocator() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "store locator"
        AddHandler oMenuItem.Click, AddressOf Do_CopyPluginStoreLocator
        Return oMenuItem
    End Function


    Private Function SubMenuItem_CopyGuid() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar Guid"
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuid
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Stp(_ProductCategory, DTO.Defaults.SelectionModes.Browse)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _ProductCategory.nom.Tradueix(Current.Session.Lang) & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.ProductCategory.Delete(_ProductCategory, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar la categoría")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub



    Private Sub Do_Incidencies()
        Dim oFrm As New Frm_Last_Incidencies(_ProductCategory)
        oFrm.Show()
    End Sub


    Public Sub Do_Repeticions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductRepeticions(_ProductCategories.First)
        oFrm.Show()
    End Sub

    Private Async Sub Do_RankingCustomers()
        Dim exs As New List(Of Exception)
        Dim oRanking = Await FEB.Ranking.CustomerRanking(exs, Current.Session.User, _ProductCategories.First)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Ranking(oRanking)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Historial(sender As Object, e As System.EventArgs)
        Dim oFrm As New Frm_Product_Historial(_ProductCategories.First)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Atlas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB.SellOut.Factory(exs, Current.Session.User, , DTOSellOut.ConceptTypes.Geo, DTOSellOut.Formats.Units)
        If exs.Count = 0 Then
            Dim oValues = DTOProductCategory.ToGuidNoms(_ProductCategories)
            oSellout.AddFilter(DTOSellOut.Filter.Cods.Product, oValues)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Wtbols(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductWtbols(_ProductCategories.First)
        oFrm.Show()
    End Sub

    Private Sub Do_StoreLocator()
        Dim oFrm As New Frm_StoreLocator(_ProductCategories.First())
        oFrm.Show()
    End Sub

    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB.SellOut.Factory(exs, Current.Session.User, , DTOSellOut.ConceptTypes.product, DTOSellOut.Formats.units)
        If exs.Count = 0 Then
            Dim oValues = DTOProductCategory.ToGuidNoms(_ProductCategories)
            oSellout.AddFilter(DTOSellOut.Filter.Cods.Product, oValues)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_ProductRanking(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductCategoryStats(_ProductCategories.First)
        oFrm.Show()
    End Sub

    Private Sub Do_Forecast(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductSkuForecast(_ProductCategories.First)
        oFrm.Show()
    End Sub



    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oLang As DTOLang = oMenuItem.Tag
        Dim url = IIf(oLang Is Nothing, _ProductCategory.getUrl(AbsoluteUrl:=True), _ProductCategory.UrlCanonicas.CanonicalUrl(oLang))
        Process.Start(url)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oLang As DTOLang = oMenuItem.Tag
        Dim url = IIf(oLang Is Nothing, _ProductCategory.getUrl(AbsoluteUrl:=True), _ProductCategory.UrlCanonicas.CanonicalUrl(oLang))
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Sub Do_UrlBuilderBlogComments(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = FEB.ProductCategory.UrlBuilderForBlogComments(_ProductCategory, True)
        Clipboard.SetDataObject(sUrl)
    End Sub

    Private Sub Do_CopyUrlCompatibility()
        Dim sUrl = FEB.ApiUrl("ProductDownloads/LastCompatibilityReport", _ProductCategory.Guid.ToString())
        Clipboard.SetDataObject(sUrl)
    End Sub

    Private Sub Do_UrlBuilderEmails(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = FEB.ProductCategory.UrlBuilderForEmails(_ProductCategory, True)
        Clipboard.SetDataObject(sUrl)
    End Sub


    Private Sub Do_Incentius()
        Dim oFrm As New Frm_Incentius(_ProductCategory)
        oFrm.Show()
    End Sub

    Private Sub Do_LangText()
        Dim oFrm As New Frm_ProductDescription(_ProductCategory)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_SkusSort()
        Dim oFrm As New Frm_ProductSkusSort(_ProductCategory)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyPluginCollection()
        Dim src As String = _ProductCategory.PluginCollectionMarkup(DTOProductPlugin.Modes.Collection)
        Clipboard.SetDataObject(src, True)
        MsgBox("Plugin copiat al portapapers", MsgBoxStyle.Information, "Mat.Net")
    End Sub

    Private Sub Do_CopyPluginAccessories()
        Dim src As String = _ProductCategory.PluginCollectionMarkup(DTOProductPlugin.Modes.Accessories)
        Clipboard.SetDataObject(src, True)
        MsgBox("Plugin copiat al portapapers", MsgBoxStyle.Information, "Mat.Net")
    End Sub

    Private Sub Do_CopyPluginStoreLocator()
        Dim src As String = _ProductCategory.PluginStoreLocator()
        Clipboard.SetDataObject(src, True)
        MsgBox("Plugin copiat al portapapers", MsgBoxStyle.Information, "Mat.Net")
    End Sub

    Private Sub Do_CopyGuid(sender As Object, e As System.EventArgs)
        Clipboard.SetDataObject(_ProductCategory.Guid.ToString, True)
    End Sub

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================






End Class

