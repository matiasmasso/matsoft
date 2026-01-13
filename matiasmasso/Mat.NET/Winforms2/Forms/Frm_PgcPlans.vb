Public Class Frm_PgcPlans
    Private _DefaultValue As DTOPgcPlan
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPgcPlan = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_PgcPlans_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_PgcPlans1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PgcPlans1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_PgcPlans1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PgcPlans1.RequestToAddNew
        Dim oPgcPlan As New DTOPgcPlan()
        Dim oFrm As New Frm_PgcPlan(oPgcPlan)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PgcPlans1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PgcPlans1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oPgcPlans = Await FEB.PgcPlans.All(exs)
        If exs.Count = 0 Then
            Xl_PgcPlans1.Load(oPgcPlans, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class