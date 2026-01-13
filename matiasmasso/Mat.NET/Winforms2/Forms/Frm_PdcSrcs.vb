Public Class Frm_PdcSrcs
    Private _DefaultValue As DTOPurchaseOrder.Sources
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPurchaseOrder.Sources = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_Templates_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_PdcSrcs1.Load(_DefaultValue, _SelectionMode)
    End Sub

    Private Sub Xl_PdcSrcs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PdcSrcs1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub


    Private Sub Xl_PdcSrcs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PdcSrcs1.RequestToRefresh
        Xl_PdcSrcs1.Load(_DefaultValue, _SelectionMode)
    End Sub

End Class