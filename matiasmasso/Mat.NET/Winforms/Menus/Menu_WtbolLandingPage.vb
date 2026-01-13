Public Class Menu_WtbolLandingPage

    Inherits Menu_Base

    Private _WtbolLandingPages As List(Of DTOWtbolLandingPage)
    Private _WtbolLandingPage As DTOWtbolLandingPage

    Public Sub New(ByVal oWtbolLandingPages As List(Of DTOWtbolLandingPage))
        MyBase.New()
        _WtbolLandingPages = oWtbolLandingPages
        If _WtbolLandingPages IsNot Nothing Then
            If _WtbolLandingPages.Count > 0 Then
                _WtbolLandingPage = _WtbolLandingPages.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oWtbolLandingPage As DTOWtbolLandingPage)
        MyBase.New()
        _WtbolLandingPage = oWtbolLandingPage
        _WtbolLandingPages = New List(Of DTOWtbolLandingPage)
        If _WtbolLandingPage IsNot Nothing Then
            _WtbolLandingPages.Add(_WtbolLandingPage)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Navegar())
        MyBase.AddMenuItem(MenuItem_Producte())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _WtbolLandingPages.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function
    Private Function MenuItem_Navegar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Navegar"
        oMenuItem.Enabled = _WtbolLandingPages.Count = 1
        oMenuItem.DropDownItems.Add(MenuItem_MMOLandings)
        oMenuItem.DropDownItems.Add(MenuItem_MerchantLandingPage)
        Return oMenuItem
    End Function
    Private Function MenuItem_MMOLandings() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Landings del producte"
        oMenuItem.Enabled = _WtbolLandingPages.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Navigate_MMOLandings
        Return oMenuItem
    End Function
    Private Function MenuItem_MerchantLandingPage() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Landing page del comerç"
        oMenuItem.Enabled = _WtbolLandingPages.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Navigate_MerchantLandingPage
        Return oMenuItem
    End Function
    Private Function MenuItem_Producte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Producte"
        oMenuItem.Enabled = _WtbolLandingPages.Count = 1
        If _WtbolLandingPage.Product IsNot Nothing Then
            Dim oMenu As New Menu_Product(CType(_WtbolLandingPage.Product, DTOProduct))
            oMenuItem.DropDownItems.AddRange(oMenu.Range)
        End If
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
        Dim oFrm As New Frm_WtbolLandingPage(_WtbolLandingPage)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Navigate_MMOLandings()
        Dim oProduct = CType(_WtbolLandingPage.Product, DTOProduct)
        Dim url As String = oProduct.GetUrl(Current.Session.Lang, DTOProduct.Tabs.distribuidores, True)
        UIHelper.ShowHtml(url)
    End Sub
    Private Sub Do_Navigate_MerchantLandingPage()
        Dim url As String = _WtbolLandingPage.Uri.AbsoluteUri
        UIHelper.ShowHtml(url)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.WtbolLandingPage.Delete(exs, _WtbolLandingPages.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

