Public Class Frm_Deliveries
    Private _Deliveries As List(Of DTODelivery)
    Private _Mode As Modes = Modes.All
    Private _Purpose As Xl_Deliveries.Purposes = Xl_Deliveries.Purposes.MultipleCustomers
    Private _Codis As List(Of DTOPurchaseOrder.Codis) = Nothing
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    'TODO: New(contact) per poder refrescar-ho per exemple despres de facturar un albarà

    Private Enum Modes
        All
        Some
    End Enum

    Public Sub New(oPurpose As Xl_Deliveries.Purposes, oDeliveries As List(Of DTODelivery), sCaption As String, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Purpose = oPurpose
        _Deliveries = oDeliveries
        _Mode = Modes.Some
        _SelectionMode = oSelectionMode
        Xl_Years1.Visible = False

        Me.Text = sCaption

    End Sub

    Public Sub New(Optional Codis As List(Of DTOPurchaseOrder.Codis) = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Mode = Modes.All
        _Codis = Codis
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Deliveries_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Select Case _Purpose
            Case Xl_Deliveries.Purposes.SingleCustomer
                Me.Size = New Size(550, Me.Height) 'short width version
        End Select

        If _Mode = Modes.All Then
            Dim iYeas = Await FEB2.Deliveries.Years(exs, Current.Session.Emp)
            If exs.Count = 0 Then
                Xl_Years1.Load(iYeas)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        If _Mode = Modes.All Then
            ProgressBar1.Visible = True
            _Deliveries = Await FEB2.Deliveries.Headers(exs, Current.Session.Emp, Xl_Years1.Exercici.Year)
        End If
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Deliveries1.Load(_Deliveries, _Purpose, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        refresca()
    End Sub

    Private Sub Xl_Deliveries1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Deliveries1.RequestToRefresh
        refresca()
    End Sub

    Private Sub Xl_Deliveries1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Deliveries1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Deliveries1.Filter = e.Argument
    End Sub
End Class