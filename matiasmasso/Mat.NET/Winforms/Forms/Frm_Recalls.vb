Public Class Frm_Recalls
    Private _DefaultValue As DTORecall
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTORecall = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Recalls_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Recalls1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Recalls1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Recalls1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Recalls1.RequestToAddNew
        Dim oRecall As New DTORecall
        Dim oFrm As New Frm_Recall(oRecall)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Recalls1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Recalls1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oRecalls = Await FEB2.Recalls.All(exs)
        If exs.Count = 0 Then
            Xl_Recalls1.Load(oRecalls, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class