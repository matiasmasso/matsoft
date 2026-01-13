Public Class Menu_Rep
    Inherits Menu_Base

    Private _Rep As DTORep
    Private mRep As Rep
    Private mWithContact As Boolean


    Public Sub New(ByVal oRep As DTORep)
        MyBase.New()
        _Rep = oRep
        mRep = New Rep(_Rep.Guid)
    End Sub

    Public Sub New(ByVal oRep As Rep, Optional ByVal BlWithContact As Boolean = False)
        MyBase.New()
        mRep = oRep
        mWithContact = BlWithContact
        _Rep = New DTORep(mRep.Guid)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
            MenuItem_Extracte(), _
            MenuItem_RepProducts(), _
            MenuItem_Excepcions(), _
            MenuItem_LastRepLiqs(), _
            MenuItem_Transfer(), _
            MenuItem_Retencions(), _
            MenuItem_MakeRetencions(), _
            MenuItem_Mail(), _
            MenuItem_Web(), _
            MenuItem_Profile(), _
            MenuItem_LastPdcs(), _
            MenuItem_CountPdcs(), _
            MenuItem_TablaComisions(), _
            MenuItem_Rutas(), _
            MenuItem_Baixa(), _
            MenuItem_FollowUp(), _
            MenuItem_Contact()})
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

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Extracte"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_RepProducts() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "cartera de productes"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_RepProducts
        Return oMenuItem
    End Function

    Private Function MenuItem_Excepcions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excepcions"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Excepcions
        Return oMenuItem
    End Function

    Private Function MenuItem_LastRepLiqs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ultimes liquidacions"
        oMenuItem.Image = My.Resources.OpenZoom
        AddHandler oMenuItem.Click, AddressOf Do_LastRepLiqs
        Return oMenuItem
    End Function

    Private Function MenuItem_Transfer() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Transferències"
        oMenuItem.Image = My.Resources.SquareArrowTurquesa
        AddHandler oMenuItem.Click, AddressOf Do_Transfer
        Return oMenuItem
    End Function

    Private Function MenuItem_Retencions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retencions"
        'oMenuItem.Image = My.Resources.SquareArrowTurquesa
        AddHandler oMenuItem.Click, AddressOf Do_Retencions
        Return oMenuItem
    End Function

    Private Function MenuItem_MakeRetencions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "redacta Retencions"
        'oMenuItem.Image = My.Resources.SquareArrowTurquesa
        AddHandler oMenuItem.Click, AddressOf Do_MakeRetencions
        Return oMenuItem
    End Function

    Private Function MenuItem_Mail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "circular a reps"
        'oMenuItem.Image = My.Resources.SquareArrowTurquesa
        AddHandler oMenuItem.Click, AddressOf Do_MailAll
        Return oMenuItem
    End Function

    Private Function MenuItem_LastPdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ultimes comandes"
        'oMenuItem.Image = My.Resources.iExplorer
        'oMenuItem.ShortcutKeys = Shortcut.CtrlW
        AddHandler oMenuItem.Click, AddressOf Do_LastPdcs
        Return oMenuItem
    End Function

    Private Function MenuItem_CountPdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "recompte comandes"
        'oMenuItem.Image = My.Resources.iExplorer
        'oMenuItem.ShortcutKeys = Shortcut.CtrlW
        AddHandler oMenuItem.Click, AddressOf Do_CountPdcs
        Return oMenuItem
    End Function

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "web"
        oMenuItem.Image = My.Resources.iExplorer
        oMenuItem.ShortcutKeys = Shortcut.CtrlW
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_Profile() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "perfil"
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_Profile
        Return oMenuItem
    End Function

    Private Function MenuItem_TablaComisions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "tabla comisions"
        'oMenuItem.Image = My.Resources.iExplorer
        'oMenuItem.ShortcutKeys = Shortcut.CtrlW
        oMenuItem.Visible = BLL.BLLSession.Current.User.Rol.Id = Rol.Ids.SuperUser
        AddHandler oMenuItem.Click, AddressOf Do_TablaComisions
        Return oMenuItem
    End Function

    Private Function MenuItem_Rutas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "rutes"
        'oMenuItem.Image = My.Resources.iExplorer
        'oMenuItem.ShortcutKeys = Shortcut.CtrlW
        AddHandler oMenuItem.Click, AddressOf Do_Rutas
        Return oMenuItem
    End Function

    Private Function MenuItem_Baixa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "donar de baixa"
        oMenuItem.Image = My.Resources.del
        'oMenuItem.ShortcutKeys = Shortcut.CtrlW
        oMenuItem.Visible = BLL.BLLSession.Current.User.Rol.IsMainboard
        AddHandler oMenuItem.Click, AddressOf Do_Baixa
        Return oMenuItem
    End Function


    Private Function MenuItem_Contact() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Contacte"
        If mWithContact Then
            oMenuItem.Image = My.Resources.UsrMan
            oMenuItem.DropDownItems.AddRange(New Menu_Contact(mRep).Range)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function


    Private Function MenuItem_FollowUp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "seguiment"
        AddHandler oMenuItem.Click, AddressOf Do_FollowUp
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact(mRep)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContact As Contact = mRep.RazonSocialFacturacio
        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.Ctas.proveidorsEur)
        Dim oFrm As New Frm_CliCtas(oContact, oCta)
        oFrm.Show()
    End Sub

    Private Sub Do_RepProducts(sender As Object, e As System.EventArgs)
        Dim oRep As New DTORep(mRep.Guid)
        Dim oFrm As New Frm_RepProducts(oRep)
        oFrm.Show()
    End Sub

    Private Sub Do_LastRepLiqs(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRep As New DTORep(mRep.Guid)
        Dim oFrm As New Frm_RepLiqs(oRep)
        oFrm.Show()
    End Sub

    Private Sub Do_Transfer(ByVal sender As System.Object, ByVal e As System.EventArgs)
        root.NewRepTransfer()
    End Sub

    Private Sub Do_MailAll()
        Dim oRecipients As New List(Of String)
        oRecipients.Add("a todos los representantes <info@matiasmasso.es>")

        Dim oBccs As New List(Of String)
        Dim oReps As List(Of DTORep) = BLL.BLLReps.All(True)
        For Each oRep In oReps
            Dim oEmails As List(Of DTOEmail) = BLL.BLLEmails.All(oRep)
            If oEmails.Count > 0 Then
                oBccs.Add(oEmails(0).EmailAddress)
            End If
        Next

        Dim sSubject As String = "Circular a toda la red comercial"
        OutlookHelper.NewMessage(oRecipients, , oBccs, sSubject)
    End Sub

    Private Sub Do_Retencions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepRetencions
        With oFrm
            .Rep = mRep
            .Show()
        End With
    End Sub

    Private Sub Do_LastPdcs(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Last_Pdc_Clients(mRep)
        oFrm.Show()
    End Sub

    Private Sub Do_CountPdcs(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepPdcs()
        oFrm.Show()
    End Sub

    Private Sub Do_MakeRetencions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'root.ShowRepRetencions(mRep)
        MsgBox("REIMPLEMENTAR per pas de Rpr a Repliq", MsgBoxStyle.Exclamation)
        Dim iYea As Integer
        Dim iQ As Integer
        MaxiSrvr.GetLastQuarter(iYea, iQ)
        Do
            Dim s As String = InputBox("any i trimestre (YYYYQ)", "CERTIFICATS DE RETENCIO", iYea.ToString & iQ.ToString)
            If s.Length = 5 And IsNumeric(s) Then
                iYea = s.Substring(0, 4)
                iQ = s.Substring(4, 1)
                Dim iRepsCount As Integer = root.RepRetencionsSave(iYea, iQ)
                MsgBox("redactats " & iRepsCount & " certificats", MsgBoxStyle.Information, "CERTIFICATS TRIMESTRALS DE RETENCIONS A REPRESENTANTS")
                Exit Do
            Else
                If s = "" Then Exit Do
            End If
        Loop


    End Sub

    Private Sub Do_TablaComisions(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = ".csv"
            .Filter = "fitxers Csv|*.csv|tots els fitxers|*.*"
            .AddExtension = True
            .FileName = "reps.csv"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "tabla de representants, zones i comisions"
            Dim Rc As DialogResult = .ShowDialog()
            If Rc = Windows.Forms.DialogResult.OK Then
                Dim sb As System.Text.StringBuilder = Reps.CsvTablaZonasComisions()
                Dim s As String = sb.ToString
                System.IO.File.WriteAllText(.FileName, s, System.Text.Encoding.Default)
            End If
        End With
    End Sub

    Private Sub Do_Baixa(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepBaixa(mRep)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = mRep.UrlPro(root.Usuari)
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub Do_Profile(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = mRep.UrlProfile
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Private Sub Do_Rutas()
        'Dim oFrm As New Frm_RepRutas(mRep)
        'oFrm.Show()
    End Sub

    Private Sub Do_Excepcions()
        Dim oFrm As New Frm_RepCliComs(mRep)
        oFrm.Show()
    End Sub

    Private Sub Do_CuadraComisions()
        Dim oFrm As New Frm_Reps_CheckCuadreComisions
        oFrm.Show()
    End Sub

    Private Sub Do_FollowUp()
        Dim oFrm As New Frm_Rep(mRep)
        oFrm.Show()
    End Sub
End Class

