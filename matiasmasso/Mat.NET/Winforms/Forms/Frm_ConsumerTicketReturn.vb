Public Class Frm_ConsumerTicketReturn
    Private _TicketOriginal As DTOConsumerTicket

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Sub New(oTicketOriginal As DTOConsumerTicket)
        MyBase.New
        InitializeComponent()
        _TicketOriginal = oTicketOriginal
    End Sub

    Private Async Sub Frm_ConsumerTicketReturn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _TicketOriginal = Await FEB2.ConsumerTicket.Find(exs, _TicketOriginal.Guid)
        If exs.Count = 0 Then
            TextBoxOriginal.Text = _TicketOriginal.ToMultiline(Current.Session.Lang)
            DateTimePicker1.Value = Today
            Xl_TextBoxNumBaseImponible.Value = _TicketOriginal.Delivery.baseImponible.Eur
            If _TicketOriginal.MarketPlace IsNot Nothing Then
                If _TicketOriginal.MarketPlace.Equals(DTOMarketPlace.Wellknown(DTOMarketPlace.Wellknowns.AmazonSeller)) Then

                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
End Class