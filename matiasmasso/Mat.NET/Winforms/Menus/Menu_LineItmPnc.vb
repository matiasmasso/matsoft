

Public Class Menu_LineItmPnc

    Private mLineItmPnc As LineItmPnc

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oLineItmPnc As LineItmPnc)
        MyBase.New()
        mLineItmPnc = oLineItmPnc
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Art(), _
        MenuItem_Pdc(), _
        MenuItem_RepCom(), _
        MenuItem_Sortides()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Art() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Article..."
        'oMenuItem.Image = My.Resources.People_Blue
        oMenuItem.DropDownItems.AddRange(New Menu_Art(mLineItmPnc.Art).Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comanda..."
        If mLineItmPnc.Pdc.Id > 0 Then
            'oMenuItem.Image = My.Resources.People_Blue
            oMenuItem.DropDownItems.AddRange(New Menu_Pdc(mLineItmPnc.Pdc).Range)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_RepCom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                oMenuItem.Text = "Representant"
                oMenuItem.Image = My.Resources.People_Orange
                AddHandler oMenuItem.Click, AddressOf Do_RepCom
            Case Else
                oMenuItem.Visible = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_Sortides() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sortides"
        oMenuItem.Enabled = mLineItmPnc.SortidesExist
        'oMenuItem.Image = My.Resources.People_Blue
        oMenuItem.DropDownItems.AddRange(New Menu_Pdc(mLineItmPnc.Pdc).Range)
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    
    Private Sub Do_Sortides(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowPncSortides(mLineItmPnc)
    End Sub

    Private Sub Do_RepCom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PncRepCom
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .LineItmPnc = mLineItmPnc
            .Show()
        End With
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
