Public Class Menu_EdiversaRemadvItem
    Private _EdiversaRemadvItem As DTOEdiversaRemadvItem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEdiversaRemadvItem As DTOEdiversaRemadvItem)
        MyBase.New()
        _EdiversaRemadvItem = oEdiversaRemadvItem
    End Sub

    Public Async Function Range() As Task(Of ToolStripMenuItem())
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        Await MenuItem_Assign()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = _EdiversaRemadvItem.Pnd IsNot Nothing
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Async Function MenuItem_Assign() As Task(Of ToolStripMenuItem)
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        If _EdiversaRemadvItem.Pnd Is Nothing Then
            oMenuItem.Text = "Asignar a..."
            Dim oContact As DTOContact = _EdiversaRemadvItem.Parent.EmisorPago
            Dim oPnds = Await FEB.Pnds.All(exs, oContact, _EdiversaRemadvItem.Num, _EdiversaRemadvItem.Fch)
            For Each oPnd As DTOPnd In oPnds
                Dim oDropdownItem As ToolStripMenuItem = oMenuItem.DropDownItems.Add("fra " & oPnd.FraNum & " del " & oPnd.Fch.ToShortDateString & " per " & oPnd.Amt.Formatted)
                oDropdownItem.Tag = oPnd
                AddHandler oDropdownItem.Click, AddressOf Do_AssignFra
            Next

        Else
            oMenuItem.Visible = False
        End If

        Return oMenuItem
    End Function





    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As EventArgs)
        Dim oPnd As DTOPnd = _EdiversaRemadvItem.Pnd
        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_AssignFra(ByVal sender As Object, ByVal e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oPnd As DTOPnd = oMenuItem.Tag
        _EdiversaRemadvItem.Pnd = oPnd
        Dim exs As New List(Of Exception)
        If Await FEB.EdiversaRemadv.Update(_EdiversaRemadvItem.Parent, exs) Then
            RefreshRequest(sender, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs, "error al desar la remesa")
        End If
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
