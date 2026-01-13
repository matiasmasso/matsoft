Public Class Frm_ZipCods

    Private _Values As List(Of DTOZip)
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Selection

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oValues As List(Of DTOZip), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Selection)
        MyBase.New()
        _Values = oValues
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Zips_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_ZipCods1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ZipCods1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_ZipCods1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ZipCods1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Xl_ZipCods1.Load(_Values, Nothing, _SelectionMode)
    End Sub
End Class