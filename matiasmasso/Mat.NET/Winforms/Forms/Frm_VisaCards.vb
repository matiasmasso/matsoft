Public Class Frm_VisaCards
    Private _DefaultValue As DTOVisaCard
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOVisaCard = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_VisaCards_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_VisaCards1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_VisaCards1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_VisaCards1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_VisaCards1.RequestToAddNew
        Dim oVisaCard As New DTOVisaCard
        Dim oFrm As New Frm_VisaCard(oVisaCard)
        AddHandler oFrm.AfterUpdate, AddressOf onAfterUpdate
        oFrm.Show()
    End Sub

    Private Sub Xl_VisaCards1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_VisaCards1.RequestToRefresh
        onAfterUpdate(Me, e)
    End Sub

    Private Async Sub onAfterUpdate(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oVisaCards = Await FEB2.VisaCards.All(exs, Current.Session.Emp,, False)
        Xl_VisaCards1.Load(oVisaCards, _DefaultValue, _SelectionMode)
    End Function
End Class