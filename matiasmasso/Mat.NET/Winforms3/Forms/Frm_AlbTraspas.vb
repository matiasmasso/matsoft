Public Class Frm_AlbTraspas
    Private _Delivery As DTODelivery

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New
        InitializeComponent()
        _Delivery = oDelivery
    End Sub

    Private Async Sub Frm_AlbTraspas_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Delivery.Load(_Delivery, exs) Then
            DateTimePicker1.Value = _Delivery.Fch
            Dim oItemFrom = _Delivery.Items.Where(Function(x) x.Cod = DTODeliveryItem.Cods.TraspasSortida).FirstOrDefault
            If oItemFrom IsNot Nothing Then
                Xl_Contact2From.Contact = oItemFrom.Mgz
            End If
            Dim oItemTo = _Delivery.Items.Where(Function(x) x.Cod = DTODeliveryItem.Cods.TraspasEntrada).FirstOrDefault
            If oItemTo IsNot Nothing Then
                Xl_Contact2To.Contact = oItemTo.Mgz
            End If
            Await Xl_DeliveryItems1.Load(_Delivery)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class