

Public Class Menu_Adr
    Private _Address As DTOAddress

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oAddress As DTOAddress)
        MyBase.New()
        _Address = oAddress
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
            MenuItem_Zoom(),
            MenuItem_Copy()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Copy() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_Copy
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Address(_Address)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Copy(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetText(DTOAddress.MultiLine(_Address))
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Address))
    End Sub


End Class
