Public Class Frm_RepKpis

    Private _DefaultValue As DTORepKpi
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTORepKpi = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_RepKpis_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_RepKpis1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_RepKpis1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_RepKpis1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepKpis1.RequestToAddNew
        Dim oRepKpi As New DTORepKpi(DTORepKpi.Ids._NotSet)
        Dim oFrm As New Frm_RepKpi(oRepKpi)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_RepKpis1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepKpis1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oRepKpis As List(Of DTORepKpi) = BLL.BLLRepKpis.All
        Xl_RepKpis1.Load(oRepKpis, _DefaultValue, _SelectionMode)
    End Sub
End Class