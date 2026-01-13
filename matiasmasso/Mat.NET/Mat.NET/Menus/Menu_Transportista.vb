

Public Class Menu_Transportista
    Private mTransportista As Transportista

    Public Sub New(ByVal oTransportista As Transportista)
        MyBase.New()
        mTransportista = oTransportista
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_CobroReembolsos(), MenuItem_LastFras(), MenuItem_NewFra()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_LastFras() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ultimes factures"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_LastFras
        Return oMenuItem
    End Function

    Private Function MenuItem_NewFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "nova factura"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_NewFra
        Return oMenuItem
    End Function

    Private Function MenuItem_CobroReembolsos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "CobroReembolsos"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_CobroReembolsos
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_CobroReembolsos(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowReembolsos(mTransportista) '(mTransportista)
    End Sub


    Private Sub Do_LastFras(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowTrpFras(mTransportista)
    End Sub

    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oTrpFra As New MaxiSrvr.TrpFra(mTransportista)
        oTrpFra.Fch = Today
        root.ShowTrpFra(oTrpFra)
    End Sub


End Class
