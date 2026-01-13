Public Class Xl_LookupStaffPos

    Inherits Xl_LookupTextboxButton

    Private _StaffPos As DTOStaffPos

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property StaffPos() As DTOStaffPos
        Get
            Return _StaffPos
        End Get
        Set(ByVal value As DTOStaffPos)
            _StaffPos = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.StaffPos = Nothing
    End Sub

    Private Sub Xl_LookupStaffPos_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _StaffPos IsNot Nothing Then
            If Not FEB2.StaffPos.Load(exs, _StaffPos) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If
        Dim oFrm As New Frm_StaffPoss(_StaffPos, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onStaffPosSelected
        oFrm.Show()
    End Sub

    Private Sub onStaffPosSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _StaffPos = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _StaffPos Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = DTOStaffPos.Nom(_StaffPos, MyBase.Lang)
            Dim oMenu_StaffPos As New Menu_StaffPos(_StaffPos)
            AddHandler oMenu_StaffPos.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_StaffPos.Range)
        End If
    End Sub

End Class


