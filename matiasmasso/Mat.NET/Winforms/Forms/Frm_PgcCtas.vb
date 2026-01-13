Public Class Frm_PgcCtas
    Private _Plan As DTOPgcPlan
    Private _DefaultValue As DTOPgcCta
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPgcCta = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_PgcCtas_Load(sender As Object, e As EventArgs) Handles Me.Load
        _Plan = DTOApp.Current.PgcPlan
        Await refresca()
    End Sub

    Private Sub Xl_PgcCtas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PgcCtas1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_PgcCtas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PgcCtas1.RequestToAddNew
        Dim oPgcCta As New DTOPgcCta
        Dim oFrm As New Frm_PgcCta(oPgcCta)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PgcCtas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PgcCtas1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oPgcCtas = Await FEB2.PgcCtas.All(exs)
        If exs.Count = 0 Then
            Xl_PgcCtas1.Load(oPgcCtas, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class