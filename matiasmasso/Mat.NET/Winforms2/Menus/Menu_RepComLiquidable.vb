Public Class Menu_RepComLiquidable
    Private mRepComsLiquidables As List(Of DTORepComLiquidable)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oRepComsLiquidables As List(Of DTORepComLiquidable))
        MyBase.New()
        mRepComsLiquidables = oRepComsLiquidables
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
            MenuItem_Client()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "factura"
        oMenuItem.Enabled = mRepComsLiquidables.Count = 1
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Client() As ToolStripMenuItem
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "client"
        oMenuItem.Enabled = mRepComsLiquidables.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom

        Dim oContactMenu = FEB.ContactMenu.FindSync(exs, mRepComsLiquidables(0).Fra.Customer)
        Dim oMenu_Contact As New Menu_Contact(mRepComsLiquidables(0).Fra.Customer, oContactMenu)
        oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepComLiquidable(mRepComsLiquidables(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
