Public Class Frm_BookFrasCompact

    Private _Values As List(Of DTOBookFra)
    Private _DefaultValue As DTOBookFra
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(values As List(Of DTOBookFra), Optional oDefaultValue As DTOBookFra = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Values = values
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_BookFrasCompact_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_BookfrasCompact1.Load(_Values, _DefaultValue, _SelectionMode)
    End Sub

    Private Sub Xl_BookfrasCompact1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_BookfrasCompact1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub


End Class