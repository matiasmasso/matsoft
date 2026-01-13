Public Class Frm_CustomerTarifa
    Private _Customer As DTOCustomer
    Private _Tarifa As DTOCustomerTarifa
    Private _CliProductDtos As List(Of DTOCliProductDto)
    Private _Tab As Tabs
    Private _AllowEvents As Boolean

    Public Enum Tabs
        Tarifa
        Dto
        Exclusivas
        PremiumLines
        CustomPreus
    End Enum

    Public Sub New(oCustomer As DTOCustomer, Optional oTab As Tabs = Tabs.Tarifa)
        MyBase.New()
        Me.InitializeComponent()
        _Customer = oCustomer
        MargesToolStripMenuItem.Visible = Current.Session.User.Rol.IsAdmin
        _Tab = oTab
    End Sub


    Private Async Sub Frm_Tarifas_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        FEB.Customer.Load(_Customer, exs)
        FEB.Contact.Load(_Customer, exs)
        Me.Text = "Tarifa " & _Customer.Nom
        DateTimePicker1.Value = DTO.GlobalVariables.Now()
        Await refrescaTarifa()
        TabControl1.SelectedIndex = _Tab
    End Sub


    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Dto
                Await LoadDtos()
            Case Tabs.Exclusivas
                Await LoadExclusivas()
            Case Tabs.PremiumLines
                Await refrescaPremiumLines()
            Case Tabs.CustomPreus
                Await LoadCustomPreus()
        End Select
        _AllowEvents = True
    End Sub


#Region "Tarifa"
    Private Async Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        ButtonRefresh.Enabled = False
        Await refrescaTarifa()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonRefresh.Enabled = True
            Xl_Tarifa1.Disable()
        End If
    End Sub

    Private Async Function refrescaTarifa() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Tarifa = Await FEB.CustomerTarifa.Load(exs, _Customer, DateTimePicker1.Value)
        Dim oCustomProducts As List(Of DTOCustomerProduct.Compact) = Await FEB.CustomerProducts.Compact(exs, _Customer)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            If oCustomProducts.Count > 0 Then
                For Each oSku In _Tarifa.Skus
                    Dim oCustomProduct = oCustomProducts.FirstOrDefault(Function(x) x.Guid.Equals(oSku.Guid))
                    If oCustomProduct IsNot Nothing Then
                        oSku.RefCustomer = oCustomProduct.Ref
                    End If
                Next
            End If
            Xl_Tarifa1.Load(_Tarifa)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Tarifa1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Tarifa1.RequestToRefresh
        Await refrescaTarifa()
    End Sub
#End Region

#Region "Dtos"
    Private Async Function LoadDtos() As Task
        Dim exs As New List(Of Exception)
        Dim oDtos = Await FEB.CustomerTarifaDtos.All(exs, _Customer)
        _CliProductDtos = Await FEB.CliProductDtos.All(_Customer, exs)
        If exs.Count = 0 Then
            Xl_CustomerDtos1.Load(oDtos)

            Dim items As List(Of DTOCliProductDto) = _CliProductDtos

            If items Is Nothing Then
                items = New List(Of DTOCliProductDto)
            ElseIf items.Count > 0 Then
                Xl_CliProductDtos1.Visible = True
            End If

            Xl_CliProductDtos1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If

    End Function


    Private Async Sub RefrescaDtos(sender As Object, e As MatEventArgs)
        Await RefrescaDtos()
    End Sub

    Private Async Function RefrescaDtos() As Task
        Await LoadDtos()
        Await refrescaTarifa()
    End Function

    Private Sub Xl_CustomerDtos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CustomerDtos1.RequestToAddNew
        Dim oCustomerDto = DTOCustomerTarifaDto.Factory(_Customer)
        Dim oFrm As New Frm_CustomerDto(oCustomerDto)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaDtos
        oFrm.Show()
    End Sub

    Private Async Sub Xl_CliProductDtos1_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_CliProductDtos1.RequestToRemove
        Dim oDtos As List(Of DTOCliProductDto) = _CliProductDtos
        Dim oDto As DTOCliProductDto = e.Argument
        oDtos.Remove(oDto)
        _Customer.ProductDtos = oDtos
        Dim exs As New List(Of Exception)
        If Await FEB.CliProductDtos.Update(_Customer, exs) Then
            _CliProductDtos = Await FEB.CliProductDtos.All(_Customer, exs)
            If exs.Count = 0 Then
                Xl_CliProductDtos1.Load(_CliProductDtos)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_CustomerDtos1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CustomerDtos1.RequestToRefresh
        Await RefrescaDtos()
    End Sub
#End Region

#Region "Exclusivas"
    Private Async Function LoadExclusivas() As Task
        Dim exs As New List(Of Exception)
        Dim oDtos = Await FEB.CliProductsBlocked.All(exs, _Customer)
        If exs.Count = 0 Then
            Xl_CliProductsBlocked1.Load(oDtos)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub RefrescaExclusivas(sender As Object, e As MatEventArgs)
        Await RefrescaExclusivas()
    End Sub

    Private Async Function RefrescaExclusivas() As Task
        Await LoadExclusivas()
        Await refrescaTarifa()
    End Function

    Private Sub Xl_CliProductsBlocked1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CliProductsBlocked1.RequestToAddNew
        Dim oItem = DTOCliProductBlocked.Factory(_Customer)
        Dim oFrm As New Frm_CliProductBlocked(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaExclusivas
        oFrm.Show()
    End Sub

    Private Async Sub Xl_CliProductsBlocked1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliProductsBlocked1.RequestToRefresh
        Await RefrescaExclusivas()
    End Sub

    Private Sub MargesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MargesToolStripMenuItem.Click
        Me.Width = Me.Width + 180
        Xl_Tarifa1.DisplayCostColumns(True)
    End Sub

    Private Sub Xl_CliProductDtos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CliProductDtos1.RequestToAddNew
        Dim oCliProductDto As New DTOCliProductDto
        oCliProductDto.Customer = _Customer

        Dim oFrm As New Frm_CliProductDto(oCliProductDto)
        AddHandler oFrm.AfterUpdate, AddressOf onCliProductDtoAddNew
        oFrm.Show()
    End Sub

    Private Async Sub onCliProductDtoAddNew(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        _Customer.ProductDtos = Xl_CliProductDtos1.Values
        _Customer.ProductDtos.Add(e.Argument)
        If Await FEB.CliProductDtos.Update(_Customer, exs) Then
            Dim items = Await FEB.CliProductDtos.All(_Customer, exs)
            If exs.Count = 0 Then
                Xl_CliProductDtos1.Load(items)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_CliProductDtos1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CliProductDtos1.AfterUpdate
        If _AllowEvents Then
            _Customer.ProductDtos = Xl_CliProductDtos1.Values
            Dim exs As New List(Of Exception)
            If Not Await FEB.CliProductDtos.Update(_Customer, exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Xl_PremiumLinesXCustomer1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PremiumLinesXCustomer1.RequestToAddNew
        Dim oPremiumCustomer As New DTOPremiumCustomer
        With oPremiumCustomer
            .Customer = _Customer
            .UsrLog.UsrLastEdited = Current.Session.User
        End With
        Dim oFrm As New Frm_PremiumCustomer(oPremiumCustomer)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaPremiumLines
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PremiumLinesXCustomer1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PremiumLinesXCustomer1.RequestToRefresh
        Await refrescaPremiumLines()
        Await refrescaTarifa()
    End Sub

    Private Async Sub refrescaPremiumLines(sender As Object, e As MatEventArgs)
        Await refrescaPremiumLines()
    End Sub
    Private Async Function refrescaPremiumLines() As Task
        Dim exs As New List(Of Exception)
        Dim oPremiumCustomers = Await FEB.premiumCustomers.All(exs, _Customer)
        If exs.Count = 0 Then
            Xl_PremiumLinesXCustomer1.Load(oPremiumCustomers)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub ImportarTarifaCustomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarTarifaCustomToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Excel amb tarifa personalitzada de client"
            .Filter = "Excel (*.xls,*.xlsx)|*.xls;*.xlsx|documents csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim sFields = {"Ean", "Cost"}
                Dim oFrm As New Frm_ExcelColumsMapping(sFields, .FileName)
                AddHandler oFrm.AfterUpdate, AddressOf onImportTarifaCustom
                oFrm.Show()
            End If
        End With
    End Sub

    Private Async Sub onImportTarifaCustom(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet As MatHelper.Excel.Sheet = e.Argument
        Dim oTarifa = Await CustomTarifa(exs, oSheet)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_PriceList_Customer(oTarifa)
            AddHandler oFrm.AfterUpdate, AddressOf refreshCustomTarifa
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refreshCustomTarifa()
        Stop
    End Sub

    Private Async Function CustomTarifa(exs As List(Of Exception), oSheet As MatHelper.Excel.Sheet) As Task(Of DTOPricelistCustomer)
        Dim retval = DTOPricelistCustomer.Factory(_Customer)

        Dim oEans As New List(Of DTOEan)
        For Each oRow In oSheet.Rows
            If oSheet.ColumnHeadersOnFirstRow AndAlso oRow.Equals(oSheet.Rows.First) Then
                'skip oSheet header row
            Else
                Dim sEan As String = oRow.Cells(0).Content
                Dim oEan = DTOEan.Factory(sEan)
                If DTOEan.isValid(oEan) Then
                    oEans.Add(oEan)
                End If
            End If
        Next

        Dim oSkus = Await FEB.ProductSkus.Search(exs, oEans, _Customer, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then

            For Each oRow In oSheet.Rows
                Dim sEan As String = oRow.Cells(0).Content
                Dim sCost As String = oRow.Cells(1).Content
                Dim oEan = DTOEan.Factory(sEan)
                If DTOEan.isValid(oEan) Then
                    Dim dcCost As Decimal
                    If Decimal.TryParse(sCost, dcCost) Then
                        Dim item As New DTOPricelistItemCustomer(retval)
                        item.Sku = oSkus.FirstOrDefault(Function(x) x.Ean13.Equals(oEan))
                        If item.Sku Is Nothing Then
                            exs.Add(New Exception(String.Format("Ean {0} no trobat", sEan)))
                        Else
                            item.Retail = DTOAmt.Factory(dcCost)
                            retval.Items.Add(item)
                        End If
                    Else
                        exs.Add(New Exception(String.Format("preu {0} invalid a Ean {1}", sCost, sEan)))
                    End If
                End If
            Next
        End If

        Return retval
    End Function


#End Region

#Region "CustomPreus"

    Private Async Sub LoadCustomPreus(sender As Object, e As EventArgs)
        Await LoadCustomPreus()
    End Sub

    Private Async Function LoadCustomPreus() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim values = Await FEB.PriceListsCustomer.All(exs, _Customer)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_PriceLists_Customers1.Load(values, Defaults.SelectionModes.browse)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_PriceLists_Customers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Customers1.RequestToAddNew
        Dim value = DTOPricelistCustomer.Factory(_Customer)
        Dim oFrm As New Frm_PriceList_Customer(value)
        AddHandler oFrm.AfterUpdate, AddressOf LoadCustomPreus
        oFrm.Show()
    End Sub
#End Region
End Class