Public Class Frm_StaffPoss
    Private _DefaultValue As DTOStaffPos
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOStaffPos = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_StaffPoss_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_StaffPoss1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_StaffPoss1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_StaffPoss1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_StaffPoss1.RequestToAddNew
        Dim oStaffPos As New DTOStaffPos
        Dim oFrm As New Frm_StaffPos(oStaffPos)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_StaffPoss1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_StaffPoss1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oStaffPoss = Await FEB.StaffPoss.All(exs)
        If exs.Count = 0 Then
            Xl_StaffPoss1.Load(oStaffPoss, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class