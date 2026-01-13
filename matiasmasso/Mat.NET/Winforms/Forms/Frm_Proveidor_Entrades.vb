Public Class Frm_Proveidor_Entrades
    Private _Proveidor As DTOProveidor

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New
        InitializeComponent()
        _Proveidor = oProveidor
    End Sub

    Private Async Sub Frm_Proveidor_Entrades_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Proveidor, exs) Then
            Me.Text = _Proveidor.FullNom
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Proveidor_Entrades1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Proveidor_Entrades1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await FEB2.Deliveries.Entrades(exs, _Proveidor)
        If exs.Count = 0 Then
            Xl_Proveidor_Entrades1.Load(oDeliveries)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class