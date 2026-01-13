

Public Class Menu_Cce
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Private mCce As Cce

    Public Sub New(ByVal oCce As Cce)
        MyBase.New()
        mCce = oCce
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_SubComptes(), _
        MenuItem_Extracte(), _
        MenuItem_PgcCta(), _
        MenuItem_Web(), _
        MenuItem_CopyLink() _
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_SubComptes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "SubComptes"
        oMenuItem.Image = My.Resources.People_Blue
        AddHandler oMenuItem.Click, AddressOf Do_SubComptes
        Return oMenuItem
    End Function

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

    Private Sub Do_SubComptes(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCceCcds(New Cce(mCce.Emp, mCce.Cta, mCce.Yea), CDate("1/1/" & mCce.Yea), Today)
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCcd As New Ccd(mCce, Nothing)
        Dim oEmp as DTOEmp
        Dim oContact As Contact = oCcd.Contact
        If oContact Is Nothing Then
            oEmp =BLL.BLLApp.Emp
        Else
            oEmp = oContact.Emp
        End If
        Dim oCta As PgcCta = oCcd.Cta
        Dim oExercici As New Exercici(oEmp, oCcd.Yea)
        Dim oFrm As New Frm_CliCtas(oContact, oCta, oExercici)
        oFrm.Show()
    End Sub

    Private Sub Do_PgcCta(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PgcCta(mCce.Cta)
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mCce.Url, True)
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Process.Start("IExplore.exe", mCce.Url)
    End Sub

  
End Class
