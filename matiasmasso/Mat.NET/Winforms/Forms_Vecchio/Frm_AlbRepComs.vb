Public Class Frm_AlbRepComs

    Private _Delivery As DTODelivery

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New()
        Me.InitializeComponent()
        _Delivery = oDelivery
    End Sub

    Private Sub Frm_AlbRepComs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Delivery.Load(_Delivery, exs) Then
            Me.Text = "Comisions sobre albará " & _Delivery.Id & " del " & _Delivery.Fch.ToShortDateString & " a " & _Delivery.Contact.FullNom
            Xl_DeliveryRepComs1.Load(_Delivery)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class