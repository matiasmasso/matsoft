Public Class Frm_StaffCategories
    Private _DefaultValue As DTOStaffCategory
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOStaffCategory = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_StaffCategories_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_StaffCategories1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_StaffCategories1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_StaffCategories1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_StaffCategories1.RequestToAddNew
        Dim oStaffCategory As New DTOStaffCategory
        Dim oFrm As New Frm_StaffCategory(oStaffCategory)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_StaffCategories1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_StaffCategories1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oStaffCategories = Await FEB2.StaffCategories.All(exs)
        If exs.Count = 0 Then
            Xl_StaffCategories1.Load(oStaffCategories, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class
