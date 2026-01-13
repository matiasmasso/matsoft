Public Class Frm_FairGuests
    Private _Evento As DTOEvento
    Private _Allowevents As Boolean

    Public Sub New()
        MyBase.New
        InitializeComponent()
        _Evento = BLL.BLLEventos.LastEventoWithGuests

    End Sub

    Private Sub Frm_FairGuests_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_LookupEvento1.EventoValue = _Evento
        refresca()
        _Allowevents = True
    End Sub

    Private Sub Xl_FairGuests1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_FairGuests1.RequestToAddNew
        Dim oFairGuest As New DTOFairGuest(_Evento)
        Dim oFrm As New Frm_FairGuest(oFairGuest)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_FairGuests1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_FairGuests1.RequestToRefresh
        If _Allowevents Then
            refresca()
        End If
    End Sub

    Private Sub refresca()
        Dim oFairGuests As List(Of DTOFairGuest) = BLL.BLLFairGuests.All(_Evento)
        Xl_FairGuests1.Load(oFairGuests)
        LabelCount.Text = String.Format("{0:#,#} visitants registrats", Xl_FairGuests1.Count)
    End Sub


    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        If _Allowevents Then Xl_FairGuests1.Filter = e.Argument
    End Sub

    Private Sub Xl_LookupEvento1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupEvento1.AfterUpdate
        If _Allowevents Then
            _Evento = e.Argument
            refresca()
        End If
    End Sub
End Class