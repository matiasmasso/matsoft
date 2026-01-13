Public Class Frm_Worten
    Public isLoaded(10) As Boolean
    Public Enum Tabs
        General
        Oferta
        Comandes
        Categories
    End Enum

    Private Async Sub Frm_Worten_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oShop = Await FEB.Worten.Shop(exs)
        If exs.Count = 0 Then
            With oShop
                TextBox_shop_id.Text = .shop_id
                TextBox_shop_name.Text = .shop_name
            End With
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub LoadOferta(sender As Object, e As System.EventArgs)
        Await LoadOferta()
    End Sub

    Private Async Function LoadOferta() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oShopOffer = Await FEB.Worten.ShopOffer(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            Xl_WortenOffers1.Load(oShopOffer)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadOrders() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oOrderList = Await FEB.Worten.Orders(exs)
        If exs.Count = 0 Then
            Xl_WortenOrders1.Load(oOrderList.Orders)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadCategories() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oCatalog = Await FEB.Worten.Catalog(exs)
        If exs.Count = 0 Then
            Xl_WortenProductCategories1.Load(oCatalog)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If Not isLoaded(TabControl1.SelectedIndex) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Oferta
                    Await LoadOferta()
                Case Tabs.Comandes
                    Await LoadOrders()
                Case Tabs.Categories
                    Await LoadCategories()
            End Select
            isLoaded(TabControl1.SelectedIndex) = True
        End If
    End Sub


    Private Async Sub Xl_WortenOffers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WortenOffers1.RequestToRefresh
        Await LoadOferta()
    End Sub

    Private Async Sub ActualitzarStocksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualitzarStocksToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oTaskLog = Await FEB.Worten.UpdateStocks(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            MsgBox(oTaskLog.ResultMsg, MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub



    Private Sub Xl_WortenOrders1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_WortenOrders1.RequestToToggleProgressBar
        ProgressBar1.Visible = e.Argument
    End Sub

    Private Sub Xl_WortenOffers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_WortenOffers1.RequestToAddNew
        Dim value As New DTO.Integracions.Worten.Offer()
        Dim oFrm As New Frm_WortenOffer(value)
        AddHandler oFrm.AfterUpdate, AddressOf LoadOferta
        oFrm.Show()
    End Sub
End Class