Public Class Frm_SegSocialGrups
    Private _DefaultValue As DTOSegSocialGrup
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOSegSocialGrup = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_SegSocialGrups_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_SegSocialGrups1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_SegSocialGrups1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_SegSocialGrups1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_SegSocialGrups1.RequestToAddNew
        Dim oSegSocialGrup As New DTOSegSocialGrup
        Dim oFrm As New Frm_SegSocialGrup(oSegSocialGrup)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_SegSocialGrups1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SegSocialGrups1.RequestToRefresh
        Await refresca()
    End Sub
    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oSegSocialGrups = Await FEB2.SegSocialGrups.All(exs)
        If exs.Count = 0 Then
            Xl_SegSocialGrups1.Load(oSegSocialGrups, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class
