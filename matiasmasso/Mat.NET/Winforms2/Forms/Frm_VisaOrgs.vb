Public Class Frm_VisaOrgs
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _DefaultValue As DTOVisaEmisor

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOVisaEmisor = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_VisaOrgs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
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

    Private Async Sub Xl_VisaOrgs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_VisaOrgs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oVisaOrgs = Await FEB.VisaEmisors.All(exs)
        If exs.Count = 0 Then
            Xl_VisaOrgs1.Load(oVisaOrgs, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class