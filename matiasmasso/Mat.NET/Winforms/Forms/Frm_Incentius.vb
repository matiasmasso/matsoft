Public Class Frm_Incentius
    Private _DefaultValue As DTOIncentiu
    Private _Product As DTOProduct
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oProduct As DTOProduct = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse, Optional BlIncludeObsolets As Boolean = False, Optional BlIncludeFutureIncentius As Boolean = False)
        MyBase.New()
        _Product = oProduct
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Incentius_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Incentius1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Incentius1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Incentius1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Incentius1.RequestToAddNew
        Dim oIncentiu As New DTOIncentiu
        Dim oFrm As New Frm_Incentiu(oIncentiu)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Incentius1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Incentius1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim BlDisplayObsolets As Boolean = Xl_Incentius1.DisplayObsolets
        Dim oUser As DTOUser = Current.Session.User
        Dim oIncentius = Await FEB2.Incentius.Headers(exs, oUser, BlIncludeObsolets:=BlDisplayObsolets, BlIncludeFutureIncentius:=BlDisplayObsolets)
        If exs.Count = 0 Then
            Xl_Incentius1.Load(oIncentius, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class