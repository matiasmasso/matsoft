Public Class Frm_DistributionChannels

    Private _DefaultValue As DTODistributionChannel
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTODistributionChannel = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_DistributionChannels_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_DistributionChannels1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_DistributionChannels1.onItemSelected
        RaiseEvent ItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_DistributionChannels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DistributionChannels1.RequestToAddNew
        Dim oDistributionChannel As New DTODistributionChannel
        Dim oFrm As New Frm_DistributionChannel(oDistributionChannel)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_DistributionChannels1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DistributionChannels1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oDistributionChannels = Await FEB.DistributionChannels.Headers(GlobalVariables.Emp, Current.Session.Lang, exs)
        If exs.Count = 0 Then
            Xl_DistributionChannels1.Load(oDistributionChannels, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class