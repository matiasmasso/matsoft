Public Class Frm_Reps

    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _DefaultRep As DTORep
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultRep As DTORep = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
        _DefaultRep = oDefaultRep
    End Sub

    Private Async Sub Frm_Reps_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Sub Xl_Reps1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Reps1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Async Sub Xl_Reps1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Reps1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oReps = Await FEB.Reps.AllActive(Current.Session.User, exs)
        If exs.Count = 0 Then
            Xl_Reps1.Load(oReps, _DefaultRep, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class