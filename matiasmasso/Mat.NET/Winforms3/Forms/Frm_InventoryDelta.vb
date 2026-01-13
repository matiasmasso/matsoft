Public Class Frm_InventoryDelta
    Private _Stocks As List(Of DTOProductSku)
    Private _Values As List(Of DTODeliveryItem)
    Private _LoadedHistoric As Boolean
    Private _CurrentFch As Date
    Private _AllowEvents As Boolean

    Private Enum Tabs
        General
        Historic
    End Enum

    Private Async Sub Frm_InventoryDelta_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If Await LoadMgzs(exs) Then
            LoadGeneral()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub LoadGeneral()
        _AllowEvents = False
        DateTimePicker1.Value = DTO.GlobalVariables.Today()
        Await refresca()
        _AllowEvents = True
    End Sub


    Private Async Function refresca() As Task
        ProgressBar1.Visible = True
        Dim exs As New List(Of Exception)
        Dim oMgz As DTOMgz = CurrentMgz()
        _Stocks = Await FEB.Mgz.Inventory(exs, oMgz, Current.Session.User, _CurrentFch)
        ProgressBar1.Visible = False
        Me.Cursor = Cursors.Default
        If exs.Count = 0 Then
            LoadBrands()
            LoadCategories()
            LoadSkus()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Function LoadHistoric() As Task
        _AllowEvents = False
        Cursor = Cursors.WaitCursor
        Dim exs As New List(Of Exception)
        Dim oMgz As DTOMgz = CurrentMgz()
        Dim iYear As Integer = CurrentHistoricYear()
        _Values = Await FEB.Mgz.DeliveryItems(oMgz, iYear, exs)
        Cursor = Cursors.Default
        If exs.Count = 0 Then
            LoadMonths()
            LoadDays()
            LoadDeliveries()
            LoadItems()
            _AllowEvents = True
        Else
            _AllowEvents = True
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.General
            Case Tabs.Historic
                If Not _LoadedHistoric Then
                    LoadYears()
                    Await LoadHistoric()
                End If
                _LoadedHistoric = True
        End Select
    End Sub


#Region "General"

    Private Function CurrentFch() As Date
        Dim retval = DateTimePicker1.Value
        Return retval
    End Function

    Private Sub LoadBrands()
        Dim items As List(Of DTOProductSku) = _Stocks
        Xl_Inventory1.Load(Xl_Inventory.Modes.Brands, items)
    End Sub

    Private Sub LoadCategories()
        Dim oBrand As DTOProductBrand = Xl_Inventory1.Value
        Dim items As List(Of DTOProductSku) = _Stocks.Where(Function(x) x.Category.Brand.Equals(oBrand)).ToList
        Xl_Inventory2.Load(Xl_Inventory.Modes.Categories, items)
    End Sub

    Private Sub LoadSkus()
        Dim oCategory As DTOProductCategory = Xl_Inventory2.Value
        Dim items As List(Of DTOProductSku) = _Stocks.Where(Function(x) x.Category.Equals(oCategory)).ToList
        Xl_Inventory3.Load(Xl_Inventory.Modes.Skus, items)
    End Sub

    Private Async Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            DateTimePicker1.Enabled = False
            _AllowEvents = False
            _CurrentFch = DateTimePicker1.Value
            Await refresca()
            DateTimePicker1.Enabled = True
        End If
    End Sub

    Private Sub DateTimePicker1_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles DateTimePicker1.DropDown
        'to avoid valuechanged when browsing months
        RemoveHandler DateTimePicker1.ValueChanged, AddressOf DateTimePicker1_ValueChanged
    End Sub

    Private Sub DateTimePicker1_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.CloseUp
        'to avoid valuechanged when browsing months
        AddHandler DateTimePicker1.ValueChanged, AddressOf DateTimePicker1_ValueChanged
        Call DateTimePicker1_ValueChanged(sender, EventArgs.Empty)
    End Sub

    Private Sub Xl_Inventory1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Inventory1.ValueChanged
        If _AllowEvents Then
            LoadCategories()
            LoadSkus()
        End If
    End Sub

    Private Sub Xl_Inventory2_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Inventory2.ValueChanged
        If _AllowEvents Then
            LoadSkus()
        End If
    End Sub


#End Region

#Region "Historic"



    Private Function CurrentMgz() As DTOMgz
        Dim retval As DTOMgz = ComboBoxMgz.SelectedItem
        Return retval
    End Function

    Private Function CurrentHistoricYear() As Integer
        Dim retval As Integer = ComboBoxYear.SelectedItem
        Return retval
    End Function

    Private Function CurrentHistoricMonth() As Integer
        Dim retval As Integer = Xl_InventoryDelta1.Value
        Return retval
    End Function

    Private Function CurrentHistoricFch() As Date
        Dim retval As Date = Xl_InventoryDelta2.Value
        Return retval
    End Function

    Private Function CurrentDelivery() As Guid
        Dim retval As Guid = Xl_InventoryDelta3.Value
        Return retval
    End Function


    Private Async Function LoadMgzs(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oMgzs = Await FEB.Mgzs.Actius(Current.Session.Emp, DTO.GlobalVariables.Today(), exs)
        If exs.Count = 0 Then
            With ComboBoxMgz
                .DataSource = oMgzs
                .SelectedItem = oMgzs.Find(Function(x) x.Equals(GlobalVariables.Emp.Mgz))
                .DisplayMember = "Nom"
            End With
        End If
        Return exs.Count = 0
    End Function

    Private Sub LoadYears()
        Dim iYears As New List(Of Integer)
        For iYear As Integer = DTO.GlobalVariables.Today().Year To 1985 Step -1
            iYears.Add(iYear)
        Next

        With ComboBoxYear
            .DataSource = iYears
        End With
    End Sub

    Private Async Sub ActualitzarPreuMigDeCostToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualitzarPreuMigDeCostToolStripMenuItem.Click
        Await ActualitzarPreusMigDeCost(CurrentMgz)
    End Sub

    Shared Async Function ActualitzarPreusMigDeCost(oMgz As DTOMgz) As Task(Of Boolean)
        Dim exs As New List(Of Exception)

        Dim BlCancel As Boolean
        Dim oFrm As New Frm_Progress("Preu mig de cost", "calcula el preu mig de cost dels articles de un magatzem")
        oFrm.Show()

        Dim oSkus = Await FEB.Mgz.Skus(oMgz, DTO.GlobalVariables.Today(), exs)
        Dim idx As Integer
        For Each oSku In oSkus
            idx += 1
            oFrm.ShowProgress(1, oSkus.Count, idx, DTOProductSku.FullNom(oSku), BlCancel)
            If Not Await FEB.Mgz.SetPrecioMedioCoste(oSku, oMgz, exs) Then
                UIHelper.WarnError(exs)
            End If

            If BlCancel Then Exit For
        Next

        MsgBox(String.Format("{0:N0} productes amb preu mig actualitzat", oSkus.Count), MsgBoxStyle.Information, "Mat.NET")
        oFrm.Close()
        Dim retval As Boolean = True
        Return retval
    End Function

    Private Sub Xl_InventoryDelta1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_InventoryDelta1.ValueChanged
        If _AllowEvents Then
            LoadDays()
            LoadDeliveries()
            LoadItems()
        End If
    End Sub
    Private Sub Xl_InventoryDelta2_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_InventoryDelta2.ValueChanged
        If _AllowEvents Then
            LoadDeliveries()
            LoadItems()
        End If
    End Sub

    Private Sub Xl_InventoryDelta3_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_InventoryDelta3.ValueChanged
        If _AllowEvents Then
            LoadItems()
        End If
    End Sub

    Private Sub LoadMonths()
        Dim iYear As Integer = CurrentHistoricYear
        Dim items As List(Of DTODeliveryItem) = _Values.Where(Function(x) x.Delivery.Fch.Year = iYear).ToList
        Xl_InventoryDelta1.Load(Xl_InventoryDelta.Modes.Months, items)
    End Sub

    Private Sub LoadDays()
        Dim iYear As Integer = CurrentHistoricYear
        If iYear > 0 Then
            Dim iMonth As Integer = CurrentHistoricMonth()
            Dim items As List(Of DTODeliveryItem) = _Values.Where(Function(x) x.Delivery.Fch.Year = iYear And x.Delivery.Fch.Month = iMonth).ToList
            If items.Count > 0 Then
                Xl_InventoryDelta2.Load(Xl_InventoryDelta.Modes.Days, items)
            End If
        End If
    End Sub

    Private Sub LoadDeliveries()
        Dim DtFch As Date = CurrentHistoricFch()
        Dim items As New List(Of DTODeliveryItem)
        If DtFch <> Nothing Then items = _Values.Where(Function(x) x.Delivery.Fch = DtFch).ToList
        If items.Count > 0 Then
            Xl_InventoryDelta3.Load(Xl_InventoryDelta.Modes.Deliveries, items)
        End If
    End Sub

    Private Sub LoadItems()
        Dim oGuid As Guid = CurrentDelivery()
        Dim items As List(Of DTODeliveryItem) = _Values.Where(Function(x) x.Delivery.Guid.Equals(oGuid)).ToList
        If items.Count > 0 Then
            Xl_InventoryDelta4.Load(Xl_InventoryDelta.Modes.Items, items)
        End If
    End Sub

    Private Async Sub ComboBoxYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxYear.SelectedIndexChanged
        If _AllowEvents Then
            Await LoadHistoric()
        End If
    End Sub

    Private Async Sub ComboBoxMgz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMgz.SelectedIndexChanged
        If _AllowEvents Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.General
                    Await refresca()
                Case Tabs.Historic
                    Await LoadHistoric()
            End Select
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim title = String.Format("M+O Stocks a {0:yyyy.MM.dd}", _CurrentFch)
        Dim defaultFilename = title & ".xlsx"
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = title
            .Filter = "fitxers Excel (*.xlsx)|*.xlsx|tots els fitxers (*.*)|*.*"
            .FileName = defaultFilename
            If .ShowDialog = DialogResult.OK Then
                Dim oSheet As New MatHelper.Excel.Sheet(, .FileName)
                With oSheet
                    .addColumn("ref.M+O")
                    .addColumn("ref.fabricante")
                    .addColumn("marca comercial")
                    .addColumn("categoria")
                    .addColumn("producto")
                    .addColumn("stock", MatHelper.Excel.Cell.NumberFormats.Integer)
                End With
                For Each oSku In _Stocks
                    Dim oRow = oSheet.addRow()
                    oRow.addCell(oSku.id)
                    oRow.addCell(oSku.refProveidor)
                    oRow.addCell(oSku.brandNom())
                    oRow.addCell(oSku.categoryNom())
                    oRow.addCell(oSku.nomCurtOrNom(), oSku.GetUrl(Current.Session.Lang, AbsoluteUrl:=True))
                    oRow.addCell(oSku.stock)
                Next
                If Not UIHelper.ShowExcel(oSheet, exs) Then
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub


#End Region

End Class