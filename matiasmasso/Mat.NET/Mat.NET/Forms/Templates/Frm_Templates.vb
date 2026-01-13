Public Class Frm_Templates
    Private _DefaultValue As DTOTemplate
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOTemplate = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Templates_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Templates1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Templates1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Templates1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Templates1.RequestToAddNew
        Dim oTemplate As New DTOTemplate
        Dim oFrm As New Frm_Template(oTemplate)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Templates1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Templates1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oTemplates As List(Of DTOTemplate) = BLL.BLLTemplates.All
        Xl_Templates1.Load(oTemplates, _DefaultValue, _SelectionMode)
    End Sub
End Class