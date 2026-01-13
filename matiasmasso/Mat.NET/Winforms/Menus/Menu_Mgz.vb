

Public Class Menu_Mgz
    Private mMgz As Mgz

    Public Sub New(ByVal oMgz As Mgz)
        MyBase.New()
        mMgz = oMgz
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Fra() _
        })
    End Function

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

      Private Function MenuItem_Fra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewFra
        oMenuItem.Text = "Entrar factura"
        oMenuItem.ShowShortcutKeys = True
        oMenuItem.ShortcutKeys = Shortcut.CtrlF
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================
  
    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oTrpFra As New TrpFra( )
        'Dim oPqHdr As New MaxiSrvr.PqHdr(mMgz)
        'oPqHdr.Trp = New Transportista(mMgz)
        'oPqHdr.Fch = Today
        'root.ShowTrpFra(oPqHdr)
    End Sub

 

End Class
