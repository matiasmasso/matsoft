Public Class Frm_Holdings
    Private _DefaultValue As DTOHolding
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOHolding = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Holdings_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Holdings1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Holdings1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Holdings1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Holdings1.RequestToAddNew
        Dim oHolding = DTOHolding.Factory(GlobalVariables.Emp, "(nou holding)")
        Dim oFrm As New Frm_Holding(oHolding)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Holdings1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Holdings1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oHoldings = Await FEB.Holdings.All(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            Xl_Holdings1.Load(oHoldings, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class