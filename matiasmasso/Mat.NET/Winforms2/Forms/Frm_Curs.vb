Public Class Frm_Curs
    Private _DefaultCur As DTOCur
    Private _Mode As DTO.Defaults.SelectionModes
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultCur As DTOCur = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New
        InitializeComponent()

        _DefaultCur = oDefaultCur
        _Mode = oSelectionMode

    End Sub

    Private Sub Frm_CurExchangeRates_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_CurExchangeRates1.Load(DTOApp.Current.Curs, _DefaultCur, _Mode)
    End Sub

    Private Sub Xl_CurExchangeRates1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CurExchangeRates1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class