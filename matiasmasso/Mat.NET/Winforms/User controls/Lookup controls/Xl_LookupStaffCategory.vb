Public Class Xl_LookupStaffCategory

    Inherits Xl_LookupTextboxButton

    Private _StaffCategory As DTOStaffCategory

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oStaffCategory As DTOStaffCategory)
        _StaffCategory = oStaffCategory
        Refresca()
    End Sub

    Public ReadOnly Property StaffCategory() As DTOStaffCategory
        Get
            Return _StaffCategory
        End Get
    End Property


    Public Sub Clear()
        _StaffCategory = Nothing
        Refresca()
    End Sub

    Private Sub Xl_LookupStaffCategory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_StaffCategory(_StaffCategory)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupStaffCategory_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_StaffCategories(_StaffCategory, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onStaffCategorySelected
        oFrm.Show()
    End Sub

    Private Sub onStaffCategorySelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _StaffCategory = e.Argument
        RaiseEvent AfterUpdate(Me, e)
        Refresca()
    End Sub

    Private Sub Refresca()
        If _StaffCategory Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = _StaffCategory.Nom
        End If
    End Sub

End Class
