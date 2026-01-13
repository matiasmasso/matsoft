Public Class Frm_Filters
    Private _DefaultValue As DTOFilter
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOFilter = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Filters_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Filters1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Filters1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Filters1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Filters1.RequestToAddNew
        Dim oFilter As New DTOFilter
        Dim oFrm As New Frm_Filter(oFilter)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Filters1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Filters1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oFilters = Await FEB2.Filters.All(exs)
        If exs.Count = 0 Then
            Xl_Filters1.Load(oFilters, _DefaultValue, _SelectionMode)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_Filters1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_Filters1.RequestToToggleProgressBar
        ProgressBar1.Visible = CBool(e.Argument)
    End Sub
End Class