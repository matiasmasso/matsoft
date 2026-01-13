Public Class Frm_Templates
    Private _DefaultValue As DTOTemplate
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOTemplate = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Templates_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Templates1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Templates1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Templates1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Templates1.RequestToAddNew
        Dim oTemplate As New DTOTemplate
        Dim oFrm As New Frm_Template(oTemplate)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Templates1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Templates1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oTemplates = Await FEB.Templates.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Templates1.Load(oTemplates, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class