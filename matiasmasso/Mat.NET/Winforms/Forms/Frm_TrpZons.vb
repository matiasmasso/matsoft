Public Class Frm_TrpZons

    Private _Transportista As DTOTransportista = Nothing

    Public Sub New(oTransportista As DTOTransportista)
        MyBase.New
        Me.InitializeComponent()
        _Transportista = oTransportista
    End Sub

    Private Async Sub Frm_TrpZons_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Transportista.Load(_Transportista, exs) Then
            Me.Text = String.Format("Tarifes transport {0}", _Transportista.Abr)
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TrpZons1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_TrpZons1.RequestToAddNew
        Dim oTrpZon As New DTOTrpZon
        With oTrpZon
            .Transportista = _Transportista
            .Zonas = New List(Of DTOZona)
            .Costs = New List(Of DTOTrpCost)
        End With
        Dim oFrm As New Frm_TrpZon(oTrpZon)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_TrpZons1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_TrpZons1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oTrpZons = Await FEB2.TrpZons.All(exs, _Transportista)
        If exs.Count = 0 Then
            Xl_TrpZons1.Load(oTrpZons)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class