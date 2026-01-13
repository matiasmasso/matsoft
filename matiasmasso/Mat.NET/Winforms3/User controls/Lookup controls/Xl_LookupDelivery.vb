Public Class Xl_LookupDelivery
    Inherits Xl_LookupTextboxButton

    Private _Delivery As DTODelivery

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToLookup(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Delivery() As DTODelivery
        Get
            Return _Delivery
        End Get
        Set(ByVal value As DTODelivery)
            _Delivery = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Delivery = Nothing
    End Sub

    Private Sub Xl_LookupDelivery_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        RaiseEvent RequestToLookup(Me, New MatEventArgs(_Delivery))
    End Sub

    Private Sub onDeliverySelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Delivery = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Delivery Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = String.Format("{0} del {1:dd/MM/yy}", _Delivery.Id, _Delivery.Fch)
            Dim oDeliveries As New List(Of DTODelivery)
            oDeliveries.Add(_Delivery)
            Dim oMenu_Delivery As New Menu_Delivery(oDeliveries)
            AddHandler oMenu_Delivery.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Delivery.Range)
        End If
    End Sub


End Class
