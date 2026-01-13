Public Class Frm_Eci_PlantillaModif
    Private _deliveries As List(Of DTODelivery)
    Private _results As List(Of DTODelivery)
    Private _AllowEvents As Boolean

    Public Sub New(oDeliveries As List(Of DTODelivery))
        MyBase.New
        InitializeComponent()
        _deliveries = oDeliveries
    End Sub

    Private Sub Frm_Eci_PlantillaModif_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.Value = DTO.GlobalVariables.Today()
        NumericUpDown1.Value = 5
        refresca()
        ButtonOk.Enabled = True
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        _results = Xl_PlantillaModif1.Load(_deliveries, NumericUpDown1.Value, DateTimePicker1.Value)
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            NumericUpDown1.ValueChanged,
             DateTimePicker1.ValueChanged

        If _AllowEvents Then
            refresca()
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oDeliveryItems = _results.SelectMany(Function(x) x.Items).ToList()
        If Xl_PlantillaModif1.SelectedValues.Count > 0 Then
            oDeliveryItems = Xl_PlantillaModif1.SelectedValues.SelectMany(Function(x) x.Items).ToList()
        End If

        Dim oOrders = oDeliveryItems.GroupBy(Function(x) x.PurchaseOrderItem.PurchaseOrder.Guid).Select(Function(y) y.First.PurchaseOrderItem.PurchaseOrder).ToList()

        'reverse order asked from Victoria 1st march 2021
        Dim oSortedOrders = New List(Of DTOPurchaseOrder)
        For Each oOrder In oOrders
            oSortedOrders.Insert(0, oOrder)
        Next

        Dim ElCorteIngles = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles)
        Dim oPlantilla = DTO.Integracions.ElCorteIngles.PlantillaModificacion.Factory(DateTimePicker1.Value, oSortedOrders)

        Dim oCsv = oPlantilla.Csv
        UIHelper.ShowCsv(oCsv)
        'Dim oSheet = oPlantilla.ExcelSheet()
        'If Not UIHelper.ShowExcel(oSheet, exs) Then
        '    UIHelper.WarnError(exs)
        'End If
    End Sub


End Class