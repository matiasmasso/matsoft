

Public Class Menu_Cce
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Private _Cce As DTOCce



    Public Sub New(ByVal oCce As DTOCce)
        MyBase.New()
        _Cce = oCce
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Extracte(),
        MenuItem_PgcCta(),
        MenuItem_Web(),
        MenuItem_CopyLink()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================



    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extracte"
        'oMenuItem.Image = My.Resources.
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_PgcCta() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pla comptable"
        'oMenuItem.Image = My.Resources.
        AddHandler oMenuItem.Click, AddressOf Do_PgcCta
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "web"
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================


    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Extracte(Nothing, _Cce.Cta, _Cce.Exercici)
        oFrm.Show()
    End Sub

    Private Sub Do_PgcCta(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PgcCta(_Cce.Cta)
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(FEB2.Cce.Url(_Cce), True)
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Process.Start(FEB2.Cce.Url(_Cce, True))
    End Sub

  
End Class
