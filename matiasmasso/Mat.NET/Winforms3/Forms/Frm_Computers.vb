Public Class Frm_Computers
    Private _DefaultValue As DTOComputer
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOComputer = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Computers_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Computers1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Computers1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Computers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Computers1.RequestToAddNew
        Dim oComputer As New DTOComputer
        Dim oFrm As New Frm_Computer(oComputer)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Computers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Computers1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oComputers = Await FEB.Computers.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Computers1.Load(oComputers, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class