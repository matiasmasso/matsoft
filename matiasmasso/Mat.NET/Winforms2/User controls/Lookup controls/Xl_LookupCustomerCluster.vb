Public Class Xl_LookupCustomerCluster

    Inherits Xl_LookupTextboxButton

    Private _CustomerCluster As DTOCustomerCluster

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property CustomerCluster() As DTOCustomerCluster
        Get
            Return _CustomerCluster
        End Get
        Set(ByVal value As DTOCustomerCluster)
            _CustomerCluster = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.CustomerCluster = Nothing
    End Sub

    Private Sub onCustomerClusterSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _CustomerCluster = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _CustomerCluster Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _CustomerCluster.Nom
            Dim oMenu_CustomerCluster As New Menu_CustomerCluster(_CustomerCluster)
            AddHandler oMenu_CustomerCluster.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_CustomerCluster.Range)
        End If
    End Sub


End Class



