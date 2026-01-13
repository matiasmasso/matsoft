Public Class Frm_MarketPlace
    Private _MarketPlace As DTOMarketPlace
    Private _AllowEvents As Boolean
    Private _Cache As Models.ClientCache

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Catalog
        Tickets
    End Enum

    Public Sub New(value As DTOMarketPlace)
        MyBase.New()
        Me.InitializeComponent()
        _MarketPlace = value
    End Sub

    Private Async Sub Frm_MarketPlace_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.MarketPlace.Load(exs, _MarketPlace) Then
            With _MarketPlace
                Await Xl_Contact21.Load(exs, .Contact())
                If .IsNew Then
                    Xl_Contact21.Enabled = True
                Else
                    Me.Text = "Marketplace " & .Nom
                End If
                TextBoxNom.Text = .Nom
                Xl_Percent1.Value = .Commission
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With

            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
             TextBoxNom.TextAlignChanged,
              Xl_Percent1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If _MarketPlace.IsNew Then
            _MarketPlace = New DTOMarketPlace(Xl_Contact21.Contact.Guid)
        End If

        With _MarketPlace
            .Nom = TextBoxNom.Text
            .Commission = Xl_Percent1.Value
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.MarketPlace.Update(exs, _MarketPlace) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_MarketPlace))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.MarketPlace.Delete(exs, _MarketPlace) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_MarketPlace))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        ShowDisabled.Visible = False
        ExcelToolStripMenuItem.Visible = False
        Select Case TabControl1.SelectedIndex
            Case Tabs.Catalog
                ShowDisabled.Visible = True
                ExcelToolStripMenuItem.Visible = True
                Await refrescaCatalog()
            Case Tabs.Tickets
                Await refrescaTickets()
        End Select
    End Sub

    Private Async Function refrescaCatalog() As Task
        Dim exs As New List(Of Exception)
        _Cache = Await FEB.Cache.Fetch(exs, Current.Session.User)
        If exs.Count = 0 Then
            ProgressBarCatalog.Visible = True
            _MarketPlace.Catalog = Await FEB.MarketPlace.Catalog(exs, Current.Session.User, _MarketPlace)
            If exs.Count = 0 Then
                Xl_MarketplaceSkus1.Load(_MarketPlace, _Cache)
                ProgressBarCatalog.Visible = False
            Else
                ProgressBarCatalog.Visible = False
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBarCatalog.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function refrescaTickets() As Task
        Dim exs As New List(Of Exception)
        ProgressBarTickets.Visible = True
        Dim values = Await FEB.ConsumerTickets.All(exs, _MarketPlace, DTO.GlobalVariables.Today().Year)
        If exs.Count = 0 Then
            Xl_ConsumerTickets1.Load(values)
            ProgressBarTickets.Visible = False
        Else
            ProgressBarTickets.Visible = False
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Async Sub Xl_ConsumerTickets1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ConsumerTickets1.RequestToRefresh
        Await refrescaTickets()
    End Sub

    Private Sub ShowDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles ShowDisabled.CheckedChanged
        Xl_MarketplaceSkus1.ShowDisabled(ShowDisabled.Checked)
    End Sub

    Private Sub Control_Changed(sender As Object, e As MatEventArgs) Handles Xl_Percent1.AfterUpdate, TextBoxNom.TextAlignChanged

    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oSheet As New MatHelper.Excel.Sheet("Catalogo " & _MarketPlace.Nom)
        With oSheet
            .AddColumn("sku")
            .AddColumn("product-id")
            .AddColumn("product-id-Type")
            .AddColumn("price")
            .AddColumn("quantity")
            .AddColumn("state") 'Csv => 11
            .AddColumn("leadtime-to-ship")
            .AddColumn("price[channel=WRT_ES_ONLINE]")
            .AddColumn("price[channel=WRT_PT_ONLINE]")
        End With
        For Each item In _MarketPlace.Catalog
            Dim oRow = oSheet.AddRow
            Dim oSku = _Cache.FindSku(item.Sku.Guid)
            Dim dcRrpp = _Cache.RetailPrice(item.Sku.Guid)
            Dim oStk = _Cache.SkuStock(item.Sku.Guid)
            oRow.AddCell(oSku.Guid.ToString())
            oRow.AddCell(DTOEan.eanValue(oSku.Ean13))
            oRow.AddCell("EAN")
            oRow.AddCell(dcRrpp)
            If oStk Is Nothing Then
                oRow.AddCell(0)
            Else
                oRow.AddCell(oStk.StockAvailable)
            End If
            oRow.AddCell(11)
            oRow.AddCell(2)
            oRow.AddCell(dcRrpp)
            oRow.AddCell(dcRrpp)
        Next

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class


