Imports System.ComponentModel

Public Class Frm_Facturacio
    'Private _MinTransmisioFch As DateTimeOffset
    Private _MinTransmisioFch As DateTime
    Private _LastInvoicesEachSerie As List(Of DTOInvoice)
    Private _LastRectificativa As DTOInvoice
    Private _LastSimplificada As DTOInvoice
    Private _Transmisions As List(Of DTOTransmisio)
    Private _Deliveries As List(Of DTODelivery)
    Private _DeliveriesToInvoice As List(Of DTODelivery)
    Private _Invoices As List(Of DTOInvoice)
    Private _ExportDeliveries As New List(Of DTODelivery)
    Private _PendingDeliveries As New List(Of DTODelivery)

    Public Sub New(Optional oTransmisions As List(Of DTOTransmisio) = Nothing)
        MyBase.New
        InitializeComponent()
        _Transmisions = oTransmisions
    End Sub

    Private Async Sub Frm_Facturacio_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        ButtonOk.Enabled = False
        ButtonCancel.Enabled = True
        Xl_ProgressBar2.ShowMarquee("llegint els albarans pendents de transmetre")

        If _Transmisions Is Nothing Then
            TabControl1.TabPages.Remove(TabPage2)
            TabControl1.TabPages.Remove(TabPage1)
            _PendingDeliveries = Await FEB2.Deliveries.PendentsDeFacturar(exs, Current.Session.Emp)
            If exs.Count = 0 Then
                Xl_Deliveries3.Load(_PendingDeliveries, Xl_Deliveries.Purposes.Facturacio)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            Await Procesa()
            refresca()
            ButtonOk.Enabled = True
        End If
    End Sub



    Private Async Function Procesa() As Task
        Dim exs As New List(Of Exception)
        _DeliveriesToInvoice = New List(Of DTODelivery)
        _ExportDeliveries = New List(Of DTODelivery)
        _PendingDeliveries = New List(Of DTODelivery)
        _MinTransmisioFch = _Transmisions.Min(Function(x) x.fch).Date

        Dim oExercici As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, _MinTransmisioFch.Year)
        _LastInvoicesEachSerie = Await FEB2.Invoices.LastEachSerie(exs, oExercici)

        'fes una copia estàtica que no modifiqui l'original
        Dim oLastInvoicesEachSerie = New List(Of DTOInvoice)
        For Each oInvoice In _LastInvoicesEachSerie
            oLastInvoicesEachSerie.Add(oInvoice)
        Next

        _Deliveries = Await FEB2.Deliveries.PendentsDeFacturar(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            For Each item As DTODelivery In _Deliveries

                Dim BlInTransmisio As Boolean = False
                If item.Transmisio IsNot Nothing Then
                    If _Transmisions.Any(Function(x) x.Equals(item.Transmisio)) Then
                        BlInTransmisio = True
                    End If
                End If

                If BlInTransmisio Then
                    _DeliveriesToInvoice.Add(item)
                ElseIf item.ExportCod <> DTOInvoice.ExportCods.Nacional Then
                    _ExportDeliveries.Add(item)
                Else
                    _PendingDeliveries.Add(item)
                End If

            Next

            _Invoices = New List(Of DTOInvoice)
            If _DeliveriesToInvoice.Count = 0 Then
            Else
                Application.DoEvents()
                _Invoices = Await FEB2.Invoice.Factory(exs, Current.Session.Emp, _DeliveriesToInvoice, _MinTransmisioFch, oLastInvoicesEachSerie, AddressOf Xl_ProgressBar2.ShowProgress)
            End If
            Xl_ProgressBar2.Visible = False
            Application.DoEvents()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub refresca()
        Xl_Facturacio2.Load(_Invoices, _LastInvoicesEachSerie, _MinTransmisioFch)
        Xl_Deliveries2.Load(_ExportDeliveries, Xl_Deliveries.Purposes.Facturacio)
        Xl_Deliveries3.Load(_PendingDeliveries, Xl_Deliveries.Purposes.Facturacio)
    End Sub

    Private Function TransmDeliveries() As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        Dim transmitted As List(Of DTODelivery) = _Deliveries.Where(Function(x) x.Transmisio IsNot Nothing).ToList
        For Each item As DTODelivery In transmitted
            If _Transmisions.Any(Function(x) x.Equals(item.Transmisio)) Then
                retval.Add(item)
            End If
        Next
        Return retval
    End Function


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        ButtonOk.Visible = False
        ButtonCancel.Visible = False
        Xl_Facturacio2.Enabled = False
        Application.DoEvents()

        Xl_ProgressBar2.Visible = True
        Dim oInvoices As List(Of DTOInvoice) = Xl_Facturacio2.Invoices
        Dim exs As New List(Of Exception)
        If Await InvoiceUpdaterHelper.Update(exs, oInvoices, Current.Session.User, AddressOf ShowProgress) Then
            Xl_ProgressBar2.ShowEndSortida(String.Format("{0:N0} factures registrades", oInvoices.Count))
        Else
            UIHelper.WarnError(exs)
            'Xl_ProgressBar2.Visible = True
        End If
    End Sub

    Private Sub ShowProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        Xl_ProgressBar2.ShowProgress(min, max, value, label, CancelRequest)
    End Sub

    Private Sub Xl_Facturacio2_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Facturacio2.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub Xl_Deliveries2_RequestToInvoice(sender As Object, e As MatEventArgs) Handles Xl_Deliveries2.RequestToInvoice
        Dim exs As New List(Of Exception)
        Dim oDeliveries As List(Of DTODelivery) = e.Argument
        Dim oAllExports As List(Of DTODelivery) = Xl_Deliveries2.Values
        Dim itemsToRemove = New HashSet(Of DTODelivery)(oDeliveries)
        oAllExports.RemoveAll(Function(x) itemsToRemove.Contains(x))
        Xl_Deliveries2.Load(oAllExports, Xl_Deliveries.Purposes.Facturacio)

        _DeliveriesToInvoice.AddRange(oAllExports)
        Dim oSortedDeliveries As List(Of DTODelivery) = _DeliveriesToInvoice.OrderBy(Function(x) x.Fch)
        Dim oInvoices As New List(Of DTOInvoice)
        If oSortedDeliveries.Count > 0 Then
            oInvoices = Await FEB2.Invoice.Factory(exs, Current.Session.Emp, oSortedDeliveries, _MinTransmisioFch, _LastInvoicesEachSerie, AddressOf Xl_ProgressBar2.ShowProgress)
        End If
        Xl_Facturacio2.Load(oInvoices, _LastInvoicesEachSerie, _MinTransmisioFch)

    End Sub

    Private Sub Xl_ProgressBar2_RequestToExit(sender As Object, e As MatEventArgs) Handles Xl_ProgressBar2.RequestToExit
        Me.Close()
    End Sub

End Class