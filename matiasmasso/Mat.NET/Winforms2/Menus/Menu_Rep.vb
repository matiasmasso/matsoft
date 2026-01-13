Public Class Menu_Rep
    Inherits Menu_Base

    Private _Rep As DTORep
    Private _ContactMenu As DTOContactMenu
    Private mWithContact As Boolean


    Public Sub New(ByVal oRep As DTORep, oContactMenu As DTOContactMenu)
        MyBase.New()
        _Rep = oRep
        _ContactMenu = oContactMenu
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Extracte())
        MyBase.AddMenuItem(MenuItem_RepProducts())
        MyBase.AddMenuItem(MenuItem_Excepcions())
        MyBase.AddMenuItem(MenuItem_LastRepLiqs())
        MyBase.AddMenuItem(MenuItem_Transfer())
        MyBase.AddMenuItem(MenuItem_Retencions())
        MyBase.AddMenuItem(MenuItem_Mail())
        MyBase.AddMenuItem(MenuItem_Profile())
        MyBase.AddMenuItem(MenuItem_LastPdcs())
        MyBase.AddMenuItem(MenuItem_Contracts())
        MyBase.AddMenuItem(MenuItem_Rutas())
        MyBase.AddMenuItem(MenuItem_Baixa())
        MyBase.AddMenuItem(MenuItem_FollowUp())
        MyBase.AddMenuItem(MenuItem_Contact())
    End Sub


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


    Private Function MenuItem_Profile() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "perfil"
        oMenuItem.Image = My.Resources.iExplorer
        AddHandler oMenuItem.Click, AddressOf Do_Profile
        Return oMenuItem
    End Function


    Private Function MenuItem_Contracts() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "contractes"
        AddHandler oMenuItem.Click, AddressOf Do_Contracts
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
        oMenuItem.Visible = Current.Session.User.Rol.IsMainboard
        AddHandler oMenuItem.Click, AddressOf Do_Baixa
        Return oMenuItem
    End Function


    Private Function MenuItem_Contact() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Contacte"
        If mWithContact Then
            oMenuItem.Image = My.Resources.UsrMan
            oMenuItem.DropDownItems.AddRange(New Menu_Contact(_Rep, _ContactMenu).Range)
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
        Dim oFrm As New Frm_Contact(_Rep)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = FEB.Rep.RaoSocialFacturacio(_Rep)
        Dim oCta = Await FEB.PgcCta.FromCod(DTOPgcPlan.Ctas.ProveidorsEur, Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Extracte(oContact, oCta)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_RepProducts(sender As Object, e As System.EventArgs)
        Dim oFrm As New Frm_RepProducts(_Rep)
        oFrm.Show()
    End Sub

    Private Sub Do_LastRepLiqs(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepLiqs(_Rep)
        oFrm.Show()
    End Sub

    Private Sub Do_Transfer(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BancTransferFactory(Frm_BancTransferFactory.Modes.Reps)
        oFrm.Show()

        'Dim oFrm As New Frm_Rep_Transfers
        'With oFrm
        ' .NewTransferCod = Frm_Rep_Transfers.NewTransferCods.Reps
        ' .Show()
        ' End With
    End Sub

    Private Async Sub Do_MailAll()
        Dim exs As New List(Of Exception)
        Dim oRepEmails = Await FEB.Reps.Emails(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            Dim oMailMessage = DTOMailMessage.Factory("info@matiasmasso.es")
            With oMailMessage
                .Bcc = oRepEmails.Select(Function(x) x.EmailAddress).ToList
                .Subject = "Circular a toda la red comercial"
            End With

            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Retencions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Rep(_Rep, Frm_Rep.Tabs.Retencions)
        oFrm.Show()
    End Sub

    Private Sub Do_LastPdcs(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PurchaseOrders(_Rep)
        oFrm.Show()
    End Sub



    Private Async Sub Do_MakeRetencions(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'root.ShowRepRetencions(mRep)
        MsgBox("REIMPLEMENTAR per pas de Rpr a Repliq", MsgBoxStyle.Exclamation)
        Dim iYea As Integer
        Dim iQ As Integer
        TimeHelper.GetLastQuarter(iYea, iQ, DTO.GlobalVariables.Today())
        Do
            Dim s As String = InputBox("any i trimestre (YYYYQ)", "CERTIFICATS DE RETENCIO", iYea.ToString & iQ.ToString())
            If s.Length = 5 And IsNumeric(s) Then
                iYea = s.Substring(0, 4)
                iQ = s.Substring(4, 1)

                Dim exs As New List(Of Exception)
                If Await root.RepRetencionsSave(iYea, iQ, exs) Then
                    MsgBox("Certificats redactats satisfactoriament", MsgBoxStyle.Information, "CERTIFICATS TRIMESTRALS DE RETENCIONS A REPRESENTANTS")
                    Exit Do
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                If s = "" Then Exit Do
            End If
        Loop


    End Sub

    Private Sub Do_Baixa(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_RepBaixa(_Rep)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Do_Profile(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim sUrl = DTOWebDomain.Default().Url("rep/perfil", _Rep.Guid.ToString())
        Process.Start(sUrl)
    End Sub

    Private Sub Do_Contracts()
        Dim oFrm As New Frm_RepContractes(_Rep)
        oFrm.Show()
    End Sub

    Private Sub Do_Rutas()
        'Dim oFrm As New Frm_RepRutas(mRep)
        'oFrm.Show()
    End Sub

    Private Sub Do_Excepcions()
        Dim exs As New List(Of Exception)
        If FEB.Rep.Load(exs, _Rep) Then
            Dim oFrm As New Frm_RepCliComs(_Rep)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Do_FollowUp()
        Dim oFrm As New Frm_Rep(_Rep, Frm_Rep.Tabs.RepcomFollowUp)
        oFrm.Show()
    End Sub
End Class

