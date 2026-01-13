Public Class Frm_VisaCards
    Private _DefaultValue As DTOVisaCard
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOVisaCard = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_VisaCards_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
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

    Private Sub onAfterUpdate(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
        refresca()
    End Sub

    Private Sub refresca()
        Dim oVisaCards As List(Of DTOVisaCard) = BLL.BLLVisaCards.All(, False)
        Xl_VisaCards1.Load(oVisaCards, _DefaultValue, _SelectionMode)
    End Sub
End Class