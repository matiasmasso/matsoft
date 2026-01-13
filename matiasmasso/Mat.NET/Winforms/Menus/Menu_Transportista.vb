

Public Class Menu_Transportista
    Private _Transportista As DTOTransportista

    Public Sub New(ByVal oTransportista As DTOTransportista)
        MyBase.New()
        _Transportista = oTransportista
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Tarifas()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Tarifas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "tarifes"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Tarifas
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Tarifas()
        Dim oFrm As New Frm_TrpZons(_Transportista)
        oFrm.Show()
    End Sub



End Class
