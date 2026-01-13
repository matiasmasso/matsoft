Public Class Frm_UserTaskIds
    Private _DefaultValue As DTOUserTaskId
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOUserTaskId = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_UserTaskIds_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_UserTaskIds1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_UserTaskIds1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Async Sub Xl_UserTaskIds1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_UserTaskIds1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oUserTaskIds = Await FEB2.UserTaskIds.All(exs)
        If exs.count = 0 Then
            Xl_UserTaskIds1.Load(oUserTaskIds, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class