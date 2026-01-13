Public Class Frm_VivaceTrace
    Private _Delivery As DTODelivery
    Private _Trace As DTO.Integracions.Vivace.Trace

    Public Sub New(oDelivery As DTODelivery)
        MyBase.New
        InitializeComponent()
        _Delivery = oDelivery
    End Sub

    Private Async Sub Frm_VivaceTrace_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Delivery = Await FEB.Delivery.DeliveryWithTracking(exs, _Delivery)
        If exs.Count = 0 Then
            _Trace = _Delivery.Trace
            ButtonBrowse.Visible = _Trace.MoreInfoAvailable
            Xl_VivaceTrace1.Load(_Trace.Items)
            ProgressBar1.Visible = False
            TextBoxAlb.Text = String.Format("albarà {0} del {1:dd/MM/yy} per {2}", _Delivery.Id, _Delivery.Fch, _Delivery.Contact.FullNom)
            If _Delivery.Transportista IsNot Nothing Then
                TextBoxTrp.Text = _Delivery.Transportista.Nom
            End If
            TextBox3Tracking.Text = _Delivery.Tracking
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        Dim exs As New List(Of Exception)
        Dim sUrl As String = FEB.Delivery.UrlAlbSeguiment(exs, _Delivery)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub BrowseVivaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BrowseVivaceToolStripMenuItem.Click
        Dim url = DTO.Integracions.Vivace.Vivace.TrackingUrl(_Delivery)
        UIHelper.ShowHtml(url)
    End Sub
End Class