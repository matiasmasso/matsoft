

Public Class Menu_PrRevista
    Private mRevistas As PrRevistas

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oRevista As PrRevista)
        MyBase.New()
        mRevistas = New PrRevistas
        mRevistas.Add(oRevista)
    End Sub

    Public Sub New(ByVal oRevistas As PrRevistas)
        MyBase.New()
        mRevistas = oRevistas
    End Sub

    Public Function Range() As ToolStripMenuItem()
        'Return (New ToolStripMenuItem() {MenuItem_Zoom(), MenuItem_Change()})
        Return (New ToolStripMenuItem() {MenuItem_Zoom()})
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
        Dim oFrm As New Frm_PrRevista(BLL.Defaults.SelectionModes.Browse, mRevistas(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
