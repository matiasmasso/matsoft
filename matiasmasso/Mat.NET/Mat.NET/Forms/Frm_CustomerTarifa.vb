Public Class Frm_CustomerTarifa
    Private _Customer As DTOCustomer
    Private _AllowEvents As Boolean

    Private Enum Tabs
        Tarifa
        Dto
        Exclusivas
    End Enum

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _Customer = oCustomer
    End Sub


    Private Sub Frm_Tarifas_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLL.BLLCustomer.Load(_Customer)
        Me.Text = "Tarifa " & _Customer.Nom
        DateTimePicker1.Value = Today
        refrescaTarifa()
        _AllowEvents = True
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Dto
                LoadDtos()
            Case Tabs.Exclusivas
                LoadExclusivas()
        End Select
    End Sub


#Region "Tarifa"
    Private Sub ButtonRefresh_Click(sender As Object, e As EventArgs) Handles ButtonRefresh.Click
        ButtonRefresh.Enabled = False
        refrescaTarifa()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ButtonRefresh.Enabled = True
            Xl_Tarifa1.Disable()
        End If
    End Sub

    Private Sub refrescaTarifa()
        Dim oTarifa As DTOCustomerTarifa = BLL.BLLCustomerTarifa.FromCustomer(_Customer, DateTimePicker1.Value)
        Xl_Tarifa1.Load(oTarifa)
    End Sub

    Private Sub Xl_Tarifa1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Tarifa1.RequestToRefresh
        refrescaTarifa()
    End Sub
#End Region

#Region "Dtos"
    Private Sub LoadDtos()
        Dim oDtos As List(Of DTOCustomerTarifaDto) = BLL.BLLCustomerTarifaDtos.All(_Customer)
        Xl_CustomerDtos1.Load(oDtos)
    End Sub

    Private Sub RefrescaDtos()
        LoadDtos()
        refrescaTarifa()
    End Sub

    Private Sub Xl_CustomerDtos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CustomerDtos1.RequestToAddNew
        Dim oCustomerDto As New DTOCustomerTarifaDto(_Customer)
        Dim oFrm As New Frm_CustomerDto(oCustomerDto)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaDtos
        oFrm.Show()
    End Sub

    Private Sub Xl_CustomerDtos1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CustomerDtos1.RequestToRefresh
        RefrescaDtos()
    End Sub
#End Region

#Region "Exclusivas"
    Private Sub LoadExclusivas()
        Dim oDtos As List(Of DTO.DTOCliProductBlocked) = BLL.BLLCliProductsBlocked.All(_Customer)
        Xl_CliProductsBlocked1.Load(oDtos)
    End Sub

    Private Sub RefrescaExclusivas()
        LoadExclusivas()
        refrescaTarifa()
    End Sub

    Private Sub Xl_CliProductsBlocked1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CliProductsBlocked1.RequestToAddNew
        Dim oItem As DTO.DTOCliProductBlocked = BLL.BLLCliProductBlocked.NewFrom(_Customer, Nothing)
        Dim oFrm As New Frm_CliProductBlocked(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaExclusivas
        oFrm.Show()
    End Sub

    Private Sub Xl_CliProductsBlocked1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliProductsBlocked1.RequestToRefresh
        RefrescaExclusivas()
    End Sub
#End Region


End Class