Public Class Menu_PremiumLine
    Inherits Menu_Base

    Private _PremiumLines As List(Of DTOPremiumLine)
    Private _PremiumLine As DTOPremiumLine

    Public Sub New(ByVal oPremiumLines As List(Of DTOPremiumLine))
        MyBase.New()
        _PremiumLines = oPremiumLines
        If _PremiumLines IsNot Nothing Then
            If _PremiumLines.Count > 0 Then
                _PremiumLine = _PremiumLines.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oPremiumLine As DTOPremiumLine)
        MyBase.New()
        _PremiumLine = oPremiumLine
        _PremiumLines = New List(Of DTOPremiumLine)
        If _PremiumLine IsNot Nothing Then
            _PremiumLines.Add(_PremiumLine)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_StoreLocator())
        MyBase.AddMenuItem(MenuItem_ExcelEmail())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _PremiumLines.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_StoreLocator() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Store locator"
        oMenuItem.Enabled = _PremiumLines.Count = 1
        Dim browse As ToolStripMenuItem = oMenuItem.DropDownItems.Add("Navegar")
        browse.DropDownItems.Add("ES", Nothing, AddressOf Do_BrowseStoreLocatorES)
        browse.DropDownItems.Add("PT", Nothing, AddressOf Do_BrowseStoreLocatorPT)
        Dim copyLink As ToolStripMenuItem = oMenuItem.DropDownItems.Add("Copiar url")
        copyLink.DropDownItems.Add("relativa", Nothing, AddressOf Do_CopyLinkToStoreLocator)
        copyLink.DropDownItems.Add("absoluta ES", Nothing, AddressOf Do_CopyLinkToStoreLocatorES)
        copyLink.DropDownItems.Add("absoluta PT", Nothing, AddressOf Do_CopyLinkToStoreLocatorPT)
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelEmail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel emails per circular"
        oMenuItem.Enabled = _PremiumLines.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ExcelEmail
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        If Not Current.Session.Rol.IsMainboard Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PremiumLine(_PremiumLine)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_BrowseStoreLocatorES(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url = _PremiumLine.UrlStoreLocator(DTOLang.ESP, True)
        UIHelper.ShowHtml(url)
    End Sub
    Private Sub Do_BrowseStoreLocatorPT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url = _PremiumLine.UrlStoreLocator(DTOLang.POR, True)
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_CopyLinkToStoreLocator(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url = _PremiumLine.UrlStoreLocator(, False)
        UIHelper.CopyLink(url)
    End Sub

    Private Sub Do_CopyLinkToStoreLocatorES(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url = _PremiumLine.UrlStoreLocator(DTOLang.ESP, True)
        UIHelper.CopyLink(url)
    End Sub

    Private Sub Do_CopyLinkToStoreLocatorPT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url = _PremiumLine.UrlStoreLocator(DTOLang.POR, True)
        UIHelper.CopyLink(url)
    End Sub

    Private Async Sub Do_ExcelEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB.PremiumLine.ExcelEmailRecipients(exs, _PremiumLine)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
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
            If Await FEB.PremiumLine.Delete(_PremiumLines.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


