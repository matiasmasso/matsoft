Public Class Frm_Rols
    Private _DefaultValue As DTORol
    Private _SelectionMode As DTO.Defaults.SelectionModes

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTORol = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Rols_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oRols = Await FEB.Rols.All(exs)
        If exs.Count = 0 Then
            Xl_Rols1.Load(oRols, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Xl_Rols1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Rols1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class