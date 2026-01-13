

Public Class Menu_ClientGroup
    Private mClient As Client

    Public Sub New(ByVal oClient As Client)
        MyBase.New()
        mClient = oClient
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Ccxs(), MenuItem_LastAlbs(), MenuItem_Stat()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


 
    Private Function MenuItem_Ccxs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Ccx
        oMenuItem.Text = "centres"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastPdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastPdcs
        oMenuItem.Text = "ultimes comandes"
        oMenuItem.Enabled = False 'provisional
        Return oMenuItem
    End Function

    Private Function MenuItem_LastAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastAlbs
        oMenuItem.Text = "ultims albarans"
        Return oMenuItem
    End Function

 
    Private Function MenuItem_Stat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Stat
        oMenuItem.Text = "estadística"
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================



    Private Sub Do_Ccx(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Client_Ccx(mClient)
        oFrm.Show()
    End Sub

    Private Sub Do_LastPdcs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowClientPdcs(mClient)
    End Sub

    Private Sub Do_LastAlbs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowClientGroupAlbs(mClient)
    End Sub

    Private Sub Do_Stat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Grup_Stat
        With oFrm
            .Client = mClient
            .Show()
        End With
    End Sub

End Class

