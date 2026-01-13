Public Class Frm_PremiumLines
    Private _DefaultValue As DTOPremiumLine
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPremiumLine = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_PremiumLines_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_PremiumLines1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PremiumLines1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_PremiumLines1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PremiumLines1.RequestToAddNew
        Dim oPremiumLine As New DTOPremiumLine
        oPremiumLine.fch = DTO.GlobalVariables.Today()
        Dim oFrm As New Frm_PremiumLine(oPremiumLine)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PremiumLines1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PremiumLines1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oPremiumLines = Await FEB.PremiumLines.All(exs)
        If exs.Count = 0 Then
            Xl_PremiumLines1.Load(oPremiumLines, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class