

Public Class Frm_AlbTrps
    Private _Delivery As DTODelivery

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New
        InitializeComponent()
        _Delivery = oDelivery
    End Sub

    Private Async Sub Frm_AlbTrps_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Delivery.Load(_Delivery, exs) Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        TextBoxAlb.Text = String.Format("{0:dd/MM/yy} alb.{1} de {2}", _Delivery.Fch, _Delivery.Id, _Delivery.Nom)
        TextBoxZona.Text = DTOLocation.FullNom(_Delivery.Address.Zip.Location)
        TextBoxM3.Text = DTODelivery.VolumeM3(_Delivery.Items)
        TextBoxKg.Text = DTODelivery.WeightKg(_Delivery.Items)
        Await Xl_DeliveryTrps1.Load(_Delivery)

        Dim oTotal = Await FEB2.Delivery.Total(exs, _Delivery)
        If exs.Count = 0 Then
            TextBoxEur.Text = DTOAmt.CurFormatted(oTotal)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class
