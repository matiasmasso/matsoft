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
        FEB2.Customer.Load(_Customer, exs)
        FEB2.Contact.Load(_Customer, exs)
        Me.Text = "Tarifa " & _Customer.Nom
        DateTimePicker1.Value = Now
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
        _Tarifa = Await FEB2.CustomerTarifa.Load(exs, _Customer, DateTimePicker1.Value)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
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
        Dim oDtos = Await FEB2.CustomerTarifaDtos.All(exs, _Customer)
        _CliProductDtos = Await FEB2.CliProductDtos.All(_Customer, exs)
        If exs.Count = 0 Then
            Xl_CustomerDtos1.Load(oDtos)

            Dim items As List(Of DTOCliProductDto) = _CliProductDtos

            If _Customer.GlobalDto > 0 Then
                RadioButtonDtoGlobal.Checked = True
                Xl_PercentDtoGlobal.Visible = True
                Xl_CliProductDtos1.Visible = False
                Xl_PercentDtoGlobal.Value = _Customer.GlobalDto
            ElseIf items Is Nothing Then
                items = New List(Of DTOCliProductDto)
            ElseIf items.Count > 0 Then
                RadioButtonDtoTpa.Checked = True
                Xl_PercentDtoGlobal.Visible = False
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
        If Await FEB2.CliProductDtos.Update(_Customer, exs) Then
            _CliProductDtos = Await FEB2.CliProductDtos.All(_Customer, exs)
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
        Dim oDtos = Await FEB2.CliProductsBlocked.All(exs, _Customer)
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

    Private Sub RadioButtonDtoTpa_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonDtoTpa.CheckedChanged
        Xl_CliProductDtos1.Visible = RadioButtonDtoTpa.Checked
    End Sub

    Private Async Sub RadioButtonDtoGlobal_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonDtoGlobal.CheckedChanged
        Xl_PercentDtoGlobal.Visible = RadioButtonDtoGlobal.Checked
        If _AllowEvents Then
            If RadioButtonDtoGlobal.Checked Then
            Else
                Dim exs As New List(Of Exception)
                _Customer.GlobalDto = 0
                If Not Await FEB2.Customer.Update(exs, _Customer) Then
                    UIHelper.WarnError(exs)
                End If
            End If
        End If
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
        If Await FEB2.CliProductDtos.Update(_Customer, exs) Then
            Dim items = Await FEB2.CliProductDtos.All(_Customer, exs)
            If exs.Count = 0 Then
                Xl_CliProductDtos1.Load(items)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Xl_PercentDtoGlobal_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PercentDtoGlobal.AfterUpdate
        If _AllowEvents Then
            Dim DcGlobalDto As Decimal = Xl_PercentDtoGlobal.Value
            Dim exs As New List(Of Exception)
            If Not Await FEB2.Customer.UpdateGlobalDto(exs, _Customer, Xl_PercentDtoGlobal.Value) Then
                UIHelper.WarnError(exs, "error al actualitzar el descompte global")
            End If
        End If
    End Sub

    Private Async Sub Xl_CliProductDtos1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_CliProductDtos1.AfterUpdate
        If _AllowEvents Then
            _Customer.ProductDtos = Xl_CliProductDtos1.Values
            Dim exs As New List(Of Exception)
            If Not Await FEB2.CliProductDtos.Update(_Customer, exs) Then
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
        Dim oPremiumCustomers = Await FEB2.premiumCustomers.All(exs, _Customer)
        If exs.Count = 0 Then
            Xl_PremiumLinesXCustomer1.Load(oPremiumCustomers)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


#End Region


End Class