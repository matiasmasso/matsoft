

Public Class Xl_Lookup_WebmenuGroup

    Inherits Xl_LookupTextboxButton

    Private mGroup As DTOWebMenuGroup

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Group() As DTOWebMenuGroup
        Get
            Return mGroup
        End Get
        Set(ByVal value As DTOWebMenuGroup)
            mGroup = value
            If mGroup Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mGroup.Esp
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Group = Nothing
    End Sub

    Private Sub Xl_Lookup_WebmenuGroup_Doubleclick(sender As Object, e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_WebmenuGroup(mGroup)
        AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
        oFrm.Show()
    End Sub

    Private Sub Xl_Lookup_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_WebMenuGroups(BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.AfterSelect, AddressOf onItemSelected
        oFrm.Show()
    End Sub

    Private Sub onItemSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mGroup = sender
        RefreshRequest()
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub

    Private Sub RefreshRequest()
        MyBase.Text = BLL.BLLWebMenuGroup.Nom(mGroup, BLL.BLLApp.Lang)
    End Sub
End Class

