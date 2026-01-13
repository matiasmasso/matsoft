Public Class Frm_Reps

    Private _SelectionMode As BLL.Defaults.SelectionModes
    Private _DefaultRep As DTORep
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultRep As DTORep = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _DefaultRep = oDefaultRep
    End Sub

    Private Sub Frm_Reps_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub Xl_Reps1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Reps1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Reps1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Reps1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oReps As List(Of DTORep) = BLL.BLLReps.All()
        Xl_Reps1.Load(oReps, _DefaultRep, _SelectionMode)
    End Sub
End Class