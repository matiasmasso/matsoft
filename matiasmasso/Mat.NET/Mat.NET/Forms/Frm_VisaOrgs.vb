Public Class Frm_VisaOrgs
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private _DefaultValue As DTOVisaEmisor

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOVisaEmisor = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_VisaOrgs_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_VisaOrgs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_VisaOrgs1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_VisaOrgs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_VisaOrgs1.RequestToAddNew
        Dim oVisaOrg As New DTOVisaEmisor
        Dim oFrm As New Frm_VisaOrg(oVisaOrg)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_VisaOrgs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_VisaOrgs1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oVisaOrgs As List(Of DTOVisaEmisor) = BLL.BLLVisaOrgs.All
        Xl_VisaOrgs1.Load(oVisaOrgs, _DefaultValue, _SelectionMode)
    End Sub
End Class