

Public Class Menu_Previsio
    Private mPrevisio As Previsio

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPrevisio As Previsio)
        MyBase.New()
        mPrevisio = oPrevisio
    End Sub

    Public Function Range() As ToolStripMenuItem()
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
        Dim oFrm As New Frm_PrvPrevisions
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Previsio = mPrevisio
            .Show()
        End With
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
