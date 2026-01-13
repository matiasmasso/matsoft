Public Class Xl_LookupCustomersxChannel
    Inherits Xl_LookupTextboxButton

    Private _AllItems As IEnumerable(Of DTOContact)
    Private _SelectedItems As IEnumerable(Of DTOContact)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub load(oAllItems As IEnumerable(Of DTOContact), oSelectedItems As IEnumerable(Of DTOContact))
        _AllItems = oAllItems
        If oSelectedItems Is Nothing Then oSelectedItems = New List(Of DTOContact)
        _SelectedItems = oSelectedItems
        If _SelectedItems.Count = 0 Then
            MyBase.Text = ""
        Else
            Dim oChannels = DTOCustomer.DistributionChannels(oAllItems)
            MyBase.Text = DTODistributionChannel.Caption(oChannels, Current.Session.Lang)
        End If
    End Sub

    Public Shadows ReadOnly Property SelectedItems As IEnumerable(Of DTOContact)
        Get
            Return _SelectedItems
        End Get
    End Property

    Public Sub Clear()
        load(_AllItems, New List(Of DTOContact))
    End Sub

    Private Sub Xl_LookupAtlas_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_FilterCustomersXChannel(_AllItems, _SelectedItems)
        AddHandler oFrm.AfterUpdate, AddressOf refreshRequest
        oFrm.Show()
    End Sub

    Private Sub refreshRequest(sender As Object, e As MatEventArgs)
        load(_AllItems, e.Argument)
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
