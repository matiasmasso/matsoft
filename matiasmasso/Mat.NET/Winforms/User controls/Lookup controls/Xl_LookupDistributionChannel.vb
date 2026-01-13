Public Class Xl_LookupDistributionChannel
    Inherits Xl_LookupTextboxButton

    Private _DistributionChannel As DTODistributionChannel

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property DistributionChannel() As DTODistributionChannel
        Get
            Return _DistributionChannel
        End Get
        Set(ByVal value As DTODistributionChannel)
            _DistributionChannel = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.DistributionChannel = Nothing
    End Sub

    Private Sub Xl_LookupDistributionChannel_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_DistributionChannels(_DistributionChannel, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.ItemSelected, AddressOf onDistributionChannelSelected
        oFrm.Show()
    End Sub

    Private Sub onDistributionChannelSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _DistributionChannel = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _DistributionChannel Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = DTODistributionChannel.Nom(_DistributionChannel, Current.Session.Lang)
            Dim oMenu_DistributionChannel As New Menu_DistributionChannel(_DistributionChannel)
            AddHandler oMenu_DistributionChannel.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_DistributionChannel.Range)
        End If
    End Sub


End Class