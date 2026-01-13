

Public Class Menu_Adr
    Private mAdr As Adr

    Public Event AfterUpdateAdr(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event AfterUpdateCit(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oAdr As Adr)
        MyBase.New()
        mAdr = oAdr
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom() _
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



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Adr(mAdr)
        AddHandler oFrm.AfterUpdateAdr, AddressOf RefreshRequestAdr
        AddHandler oFrm.AfterUpdateCit, AddressOf RefreshRequestCit
        oFrm.Show()
    End Sub

    Private Sub RefreshRequestAdr(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdateAdr(sender, e)
    End Sub

    Private Sub RefreshRequestCit(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdateCit(sender, e)
    End Sub

End Class
