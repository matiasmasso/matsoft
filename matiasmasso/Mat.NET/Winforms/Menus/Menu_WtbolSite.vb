Imports Newtonsoft.Json.Linq

Public Class Menu_WtbolSite
    Inherits Menu_Base

    Private _WtbolSites As List(Of DTOWtbolSite)
    Private _WtbolSite As DTOWtbolSite

    Public Sub New(ByVal oWtbolSites As List(Of DTOWtbolSite))
        MyBase.New()
        _WtbolSites = oWtbolSites
        If _WtbolSites IsNot Nothing Then
            If _WtbolSites.Count > 0 Then
                _WtbolSite = _WtbolSites.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oWtbolSite As DTOWtbolSite)
        MyBase.New()
        _WtbolSite = oWtbolSite
        _WtbolSites = New List(Of DTOWtbolSite)
        If _WtbolSite IsNot Nothing Then
            _WtbolSites.Add(_WtbolSite)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Clicks())
        MyBase.AddMenuItem(MenuItem_CopyGuid())
        MyBase.AddMenuItem(Menuitem_ImportLandingPages)
        MyBase.AddMenuItem(Menuitem_ImportStocks)
        MyBase.AddMenuItem(MenuItem_CopyLinkExcel)
        MyBase.AddMenuItem(MenuItem_BrowseHatchFeed)
        MyBase.AddMenuItem(MenuItem_Test())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Clicks() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Clics"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Clicks
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyGuid() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Guid"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuid
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLinkExcel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç a Excel"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLinkExcel
        Return oMenuItem
    End Function

    Private Function MenuItem_BrowseHatchFeed() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Hatch feed"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_BrowseHatchFeed
        Return oMenuItem
    End Function

    '

    Private Function MenuItem_Test() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Proves"
        oMenuItem.DropDownItems.Add(Menuitem_TestUploadLandingPages)
        oMenuItem.DropDownItems.Add(Menuitem_TestUploadStocks)
        Return oMenuItem
    End Function

    Private Function Menuitem_TestUploadLandingPages() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pujar Landing pages"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_TestUploadLandingPages
        Return oMenuItem
    End Function
    Private Function Menuitem_ImportLandingPages() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar ean/Landing pages"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ImportLandingPages
        Return oMenuItem
    End Function

    Private Function Menuitem_ImportStocks() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar ean/stock"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ImportStocks
        Return oMenuItem
    End Function

    Private Function Menuitem_TestUploadStocks() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pujar Stocks"
        oMenuItem.Enabled = _WtbolSites.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_TestUploadStocks
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_WtbolSite(_WtbolSite)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Clicks(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_WtbolSite(_WtbolSite, Frm_WtbolSite.Tabs.Clicks)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyGuid(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sGuid As String = _WtbolSite.Guid.ToString
        Clipboard.SetDataObject(sGuid, True)
        MsgBox("Guid copiat a clipboard:" & vbCrLf & sGuid)
    End Sub

    Private Async Sub Do_ImportLandingPages()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Excel EAN/url sense headers"
            .Filter = "fitxers Excel|*.xlsx;*.xls|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim exs As New List(Of Exception)
                Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
                If LegacyHelper.ExcelHelper.ExcelSheetFromFilename(.FileName, oSheet, exs) Then

                    If Await FEB2.WtbolLandingPages.Upload(_WtbolSite, oSheet, exs) Then
                        MsgBox("Landing pages uploaded", MsgBoxStyle.Information)
                    Else
                        UIHelper.WarnError(exs)
                    End If

                Else
                    UIHelper.WarnError(exs)
                End If

            End If
        End With
    End Sub


    Private Async Sub Do_ImportStocks()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Excel EAN/stock/preu sense headers"
            .Filter = "fitxers Excel|*.xlsx;*.xls|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim exs As New List(Of Exception)
                Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
                If LegacyHelper.ExcelHelper.ExcelSheetFromFilename(.FileName, oSheet, exs) Then

                    If Await FEB2.WtbolStocks.Upload(_WtbolSite, oSheet, exs) Then
                        MsgBox("Stocks uploaded", , MsgBoxStyle.Information)
                    Else
                        UIHelper.WarnError(exs)
                    End If

                Else
                    UIHelper.WarnError(exs)
                End If

            End If
        End With
    End Sub



    Private Sub Do_TestUploadLandingPages()
        Dim exs As New List(Of Exception)
        If FEB2.WtbolSite.Load(_WtbolSite, exs) Then
            Dim jSite = New JObject()
            jSite.Add("MerchantId", _WtbolSite.MerchantId)

            Dim jItems = New JArray

            Dim jObj = New JObject
            jObj.Add("site", jSite)
            jObj.Add("items", jItems)

            If _WtbolSite.Equals(DTOWtbolSite.Wellknown(DTOWtbolSite.Wellknowns.algatec)) Then

                Dim jItem = New JObject
                jItem.Add("sku", "4000984159011")
                jItem.Add("url", "https://www.sillasauto.com/es/silla_de_coche_romer_dualfix_i_size.php")
                jItems.Add(jItem)

                jItem = New JObject
                jItem.Add("sku", "4000984159028")
                jItem.Add("url", "https://www.sillasauto.com/es/silla_de_coche_romer_dualfix_i_size.php")
                jItems.Add(jItem)

                jItem = New JObject
                jItem.Add("sku", "4000984159080")
                jItem.Add("url", "https://www.sillasauto.com/es/silla_de_coche_romer_swingfix_i_size.php")
                jItems.Add(jItem)

                jItem = New JObject
                jItem.Add("sku", "4000984159097")
                jItem.Add("url", "https://www.sillasauto.com/es/silla_de_coche_romer_swingfix_i_size.php")
                jItems.Add(jItem)

                jItem = New JObject
                jItem.Add("sku", "4000984194784")
                jItem.Add("url", "https://www.sillasauto.com/es/silla_de_coche_romer_dualfix_m_i_size.php")
                jItems.Add(jItem)

                jItem = New JObject
                jItem.Add("sku", "4000984194807")
                jItem.Add("url", "https://www.sillasauto.com/es/silla_de_coche_romer_dualfix_m_i_size.php")
                jItems.Add(jItem)
            End If

            Dim sJsonString As String = jObj.ToString
            Dim UrlProduction As String = "https://matiasmasso-api.azurewebsites.net/"
            Dim urlDeveloper As String = "http://localhost:55836/"
            Dim apiUrl As String = urlDeveloper  'UrlProduction
            Dim sUrl As String = apiUrl & "api/wtbol/upload/landingpages"
            Dim jsonOutputString As String = ""
            If FEB2.Api.SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs) Then
                MsgBox(jsonOutputString)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_TestUploadStocks()
        Dim exs As New List(Of Exception)
        If FEB2.WtbolSite.Load(_WtbolSite, exs) Then
            Dim jSite = New JObject()
            jSite.Add("MerchantId", _WtbolSite.MerchantId)

            Dim jItems = New JArray

            Dim jObj = New JObject
            jObj.Add("site", jSite)
            jObj.Add("items", jItems)

            Dim jItem = New JObject
            jItem.Add("sku", "4000984159011")
            jItem.Add("stock", 6)
            jItem.Add("price", 574)
            jItems.Add(jItem)

            jItem = New JObject
            jItem.Add("sku", "4000984159028")
            jItem.Add("stock", 4)
            jItem.Add("price", 574)
            jItems.Add(jItem)

            jItem = New JObject
            jItem.Add("sku", "4000984159080")
            jItem.Add("stock", 2)
            jItem.Add("price", 719)
            jItems.Add(jItem)

            jItem = New JObject
            jItem.Add("sku", "4000984159097")
            jItem.Add("stock", 2)
            jItem.Add("price", 719)
            jItems.Add(jItem)

            jItem = New JObject
            jItem.Add("sku", "4000984194784")
            jItem.Add("stock", 6)
            jItem.Add("price", 488)
            jItems.Add(jItem)

            jItem = New JObject
            jItem.Add("sku", "4000984194807")
            jItem.Add("stock", 6)
            jItem.Add("price", 488)
            jItems.Add(jItem)

            Dim sJsonString As String = jObj.ToString
            Dim UrlProduction As String = "https://matiasmasso-api.azurewebsites.net/"
            Dim urlDeveloper As String = "http://localhost:55836/"
            Dim sUrl As String = urlDeveloper & "api/wtbol/upload/stocks"
            Dim jsonOutputString As String = ""
            If FEB2.Api.SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs) Then
                MsgBox(jsonOutputString)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.WtbolSite.Delete(_WtbolSites.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_CopyLinkExcel()
        Dim url = FEB2.UrlHelper.Factory(True, "wtbol", "excel", _WtbolSite.Guid.ToString())
        UIHelper.CopyLink(url)
    End Sub

    Private Sub Do_BrowseHatchFeed()
        Dim exs As New List(Of Exception)
        If FEB2.WtbolSite.Load(_WtbolSite, exs) Then
            Dim url = FEB2.WtbolSite.HatchFeedUrl(_WtbolSite, True)
            UIHelper.ShowHtml(url)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class

