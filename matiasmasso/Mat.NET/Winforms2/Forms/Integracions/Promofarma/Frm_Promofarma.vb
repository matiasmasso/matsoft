Public Class Frm_Promofarma
    Private _feed As DTO.Integracions.Promofarma.Feed
    Private _IsLoaded(10) As Boolean
    Private Enum Tabs
        Orders
        Catalog
    End Enum

    Private Sub Frm_Promofarma_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        TabControl1_SelectedIndexChanged(sender, e)
    End Sub

    Private Async Sub Xl_PromofarmaFeed1_RequestToRefresh(sender As Object, e As MatEventArgs)
        Await refrescaCatalog()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_MarketplaceSkus1.Filter = e.Argument
    End Sub
    Private Sub ShowDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles ShowDisabled.CheckedChanged
        Xl_MarketplaceSkus1.ShowDisabled(ShowDisabled.Checked)
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        If Not _IsLoaded(TabControl1.SelectedIndex) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Orders
                    Dim response = Await FEB.PromofarmaOrders.Fetch(exs)
                    If exs.Count = 0 Then
                        Xl_PromofarmaOrders1.Load(response.orders)
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                        Me.Close()
                    End If
                Case Tabs.Catalog
                    Await refrescaCatalog()

                    Xl_TextboxSearch1.Visible = True
                    'Case Tabs.Csv
                    '    Xl_TextboxSearch1.Visible = False
            End Select

            _IsLoaded(TabControl1.SelectedIndex) = True
        End If
    End Sub

    Private Async Function refrescaCatalog() As Task
        Dim exs As New List(Of Exception)
        Dim oCache = Await FEB.Cache.Fetch(exs, Current.Session.User)
        If exs.Count = 0 Then
            ProgressBar1.Visible = True
            Dim oMarketPlace = DTOMarketPlace.Wellknown(DTOMarketPlace.Wellknowns.Promofarma)
            oMarketPlace.Catalog = Await FEB.MarketPlace.Catalog(exs, Current.Session.User, oMarketPlace)
            If exs.Count = 0 Then
                Xl_MarketplaceSkus1.Load(oMarketPlace, oCache)
                ProgressBar1.Visible = False
            Else
                ProgressBar1.Visible = False
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    'Private Sub Xl_PromofarmaFeed1_AfterUpdate(sender As Object, e As MatEventArgs)
    '    TextBoxCsv.Text = _feed.Csv.ToString()
    'End Sub


End Class