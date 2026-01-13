

Public Class Menu_LineItmArc
    Private mLineItmArc As LineItmArc

    Public Sub New(ByVal oLineItmArc As LineItmArc)
        MyBase.New()
        mLineItmArc = oLineItmArc
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Art(), MenuItem_Pdc(), MenuItem_Spv()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Art() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Article..."
        'oMenuItem.Image = My.Resources.People_Blue
        oMenuItem.DropDownItems.AddRange(New Menu_Art(mLineItmArc.Art).Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comanda..."
        Dim oPnc As LineItmPnc = mLineItmArc.Pnc
        If oPnc Is Nothing Then
            oMenuItem.Visible = False
        Else
            If mLineItmArc.Pnc.Pdc.Id > 0 Then
                'oMenuItem.Image = My.Resources.People_Blue
                oMenuItem.DropDownItems.AddRange(New Menu_Pdc(mLineItmArc.Pnc.Pdc).Range)
            Else
                oMenuItem.Visible = False
            End If
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Spv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Reparació..."
        Dim oSpv As spv = mLineItmArc.Spv
        If oSpv Is Nothing Then
            oMenuItem.Visible = False
        Else
            If mLineItmArc.Spv.Id > 0 Then
                'oMenuItem.Image = My.Resources.People_Blue
                oMenuItem.DropDownItems.AddRange(New Menu_Spv(mLineItmArc.Spv).Range)
            Else
                oMenuItem.Visible = False
            End If
        End If
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'root.ShowSpv(mSpv)
    End Sub



End Class

