Public Class Frm_Mgzs
    Private _DefaultValue As DTOMgz
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOMgz = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Mgzs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Mgzs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Mgzs1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Async Sub Xl_Mgzs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Mgzs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oMgzs = Await FEB.Mgzs.All(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Xl_Mgzs1.Load(oMgzs, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class