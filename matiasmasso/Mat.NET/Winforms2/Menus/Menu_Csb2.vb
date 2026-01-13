Public Class Menu_Csb2
    Inherits Menu_Base

    Private _Csbs As List(Of DTOCsb)


    Public Sub New(ByVal oCsbs As List(Of DTOCsb))
        MyBase.New()
        _Csbs = oCsbs
    End Sub

    Public Sub New(ByVal oCsb As DTOCsb)
        MyBase.New()
        _Csbs = {oCsb}.ToList()
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Csa(),
        MenuItem_Ccc(),
        MenuItem_Lliurat(),
        MenuItem_Vto(),
        MenuItem_MailVenciment(),
        MenuItem_Impagat(),
        MenuItem_Rebut(),
        MenuItem_Reclamar(),
        MenuItem_RetrocedirReclamacio(),
        MenuItem_Advance()
})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Csbs.Count = 1
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Csa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Remesa"
        oMenuItem.Enabled = _Csbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Csa
        Return oMenuItem
    End Function

    Private Function MenuItem_Lliurat() As ToolStripMenuItem
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Lliurat"
        oMenuItem.Enabled = _Csbs.Count = 1
        Dim oContactMenu = FEB.ContactMenu.FindSync(exs, _Csbs.First().Contact)
        Dim oMenuContact As New Menu_Contact(_Csbs.First().Contact, oContactMenu)
        oMenuItem.DropDownItems.AddRange(oMenuContact.Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Ccc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "compte bancari"
        oMenuItem.Enabled = _Csbs.Count = 1
        With oMenuItem.DropDownItems
            .Add(MenuItem_Bank)
            .Add(MenuItem_BankBranch)
            .Add(MenuItem_Mandate)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Bank() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Entitat bancaria"
        AddHandler oMenuItem.Click, AddressOf Do_Bank
        Return oMenuItem
    End Function

    Private Function MenuItem_BankBranch() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Oficina bancaria"
        AddHandler oMenuItem.Click, AddressOf Do_BankBranch
        Return oMenuItem
    End Function

    Private Function MenuItem_Mandate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Mandato bancari"
        AddHandler oMenuItem.Click, AddressOf Do_Mandate
        Return oMenuItem
    End Function

    Private Function MenuItem_Reclamar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Reclamar"
        oMenuItem.Enabled = _Csbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Reclamar
        Return oMenuItem
    End Function

    Private Function MenuItem_RetrocedirReclamacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir reclamació"
        oMenuItem.Enabled = _Csbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_RetrocedirReclamacio
        Return oMenuItem
    End Function

    Private Function MenuItem_Rebut() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Rebut"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = _Csbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Rebut
        Return oMenuItem
    End Function

    Private Function MenuItem_Vto() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "veure assentament venciment"
        oMenuItem.Enabled = _Csbs.Count = 1
        'oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Vto
        Return oMenuItem
    End Function


    Private Function MenuItem_Impagat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = IIf(_Csbs.Count = 1, "entrar impagat", String.Format("entrar els {0} impagats", _Csbs.Count))
        AddHandler oMenuItem.Click, AddressOf Do_Impagat
        Return oMenuItem
    End Function

    Private Function MenuItem_MailVenciment() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avis de venciment (Outlook)"
        oMenuItem.Image = My.Resources.Outlook_16
        AddHandler oMenuItem.Click, AddressOf Do_MailVenciment
        Return oMenuItem
    End Function

    Private Function MenuItem_Advance() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avançades"
        If Current.Session.User.Rol.IsAdmin Then
            oMenuItem.DropDownItems.Add(SubMenuItem_CreateVto)
            oMenuItem.DropDownItems.Add(SubMenuItem_Pnd)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CreateVto() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CreateVto
        oMenuItem.Text = "registra venciment"
        oMenuItem.Enabled = _Csbs.Count = 1
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Pnd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ShowPnd
        oMenuItem.Text = "partida pendent"
        oMenuItem.Enabled = _Csbs.Count = 1
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Csb(_Csbs.First())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Csa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Csa(_Csbs.First().Csa)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Bank()
        Dim exs As New List(Of Exception)
        If FEB.Csb.Load(_Csbs.First, exs) Then
            Dim oFrm As New Frm_Bank(_Csbs.First().Iban.BankBranch.Bank)
            AddHandler oFrm.AfterUpdate, AddressOf OnBankUpdated
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
    Private Sub Do_BankBranch()
        Dim exs As New List(Of Exception)
        If FEB.Csb.Load(_Csbs.First, exs) Then
            Dim oFrm As New Frm_BankBranch(_Csbs.First().Iban.BankBranch)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
    Private Sub Do_Mandate()
        Dim exs As New List(Of Exception)
        If FEB.Csb.Load(_Csbs.First, exs) Then
            Dim oFrm As New Frm_Iban(_Csbs.First().Iban)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub OnBankUpdated(sender As Object, e As MatEventArgs)
        _Csbs.First.Iban.BankBranch.Bank = e.Argument
        If _Csbs.First.Csa IsNot Nothing Then
            DTOCsb.validate(_Csbs.First, _Csbs.First.Csa.fileFormat)
        End If
        RefreshRequest(Me, New MatEventArgs(_Csbs.First))
    End Sub

    Private Sub Do_Rebut(ByVal sender As Object, ByVal e As System.EventArgs)
        'root.ShowRebut(_Csb.Rebut)
    End Sub

    Private Sub Do_Vto(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = _Csbs.First.ResultCca
        If oCca IsNot Nothing Then
            Dim oFrm As New Frm_Cca(oCca)
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_ShowPnd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPnd As DTOPnd = _Csbs.First.Pnd
        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        oFrm.Show()
    End Sub

    Private Async Sub Do_CreateVto(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oUser As DTOUser = Current.Session.User
        Dim exs As New List(Of Exception)
        If Await FEB.Csb.SaveVto(exs, _Csbs.First, oUser) Then
            MsgBox("venciment registrat a l'assentament " & _Csbs.First.ResultCca.Id & vbCrLf & _Csbs.First.ResultCca.Concept, MsgBoxStyle.Information, "MAT.NET")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_Reclamar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCsb = _Csbs.First
        Dim rc As MsgBoxResult = MsgBox("reclamem aquest efecte?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim oCca = Await FEB.Csb.Reclama(exs, Current.Session.User, oCsb)
            If exs.Count = 0 Then
                Dim oMail = DTOCorrespondencia.Factory(Current.Session.User, oCsb.Csa.banc, DTOCorrespondencia.Cods.Enviat)
                With oMail
                    .Subject = "RECLAMACIO EFECTE " & oCsb.FormattedId()
                    FEB.Csb.Load(oCsb, exs)
                    FEB.Banc.Load(oCsb.Csa.banc, exs)
                    FEB.Contact.Load(oCsb.Csa.banc, exs)
                    Dim oDoc As New LegacyHelper.PdfBancReclamacioEfecte(oCsb, oMail)
                    .docFile = LegacyHelper.DocfileHelper.Factory(exs, oDoc.Stream(exs), MimeCods.Pdf)
                    If Await FEB.Correspondencia.Upload(oMail, exs) Then
                        RefreshRequest(Me, New MatEventArgs(oCsb))
                        If Not Await UIHelper.ShowStreamAsync(exs, oMail.DocFile) Then
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs, "error al desar el document a correspondencia")
                    End If
                End With
            Else
                UIHelper.WarnError(exs, "error al reclamar l'efecte")
            End If
        Else
            UIHelper.WarnError("Operació cancelada per l'usuari")
        End If

    End Sub

    Private Async Sub Do_RetrocedirReclamacio()
        Dim oCsb = _Csbs.First
        Dim rc As MsgBoxResult = MsgBox("Tornem a deixar l'efecte com abans de reclamar-lo?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Csb.RetrocedeixReclamacio(Current.Session.User, oCsb, exs) Then
                RefreshRequest(Me, New MatEventArgs(oCsb))
                MsgBox("reclamació retrocedida satisfactoriament")
            Else
                UIHelper.WarnError(exs, "error al reclamar l'efecte")
            End If
        Else
            UIHelper.WarnError("Operació cancelada per l'usuari")
        End If
    End Sub

    Private Async Sub Do_MailVenciment()
        Dim exs As New List(Of Exception)
        Dim oCsb = _Csbs.First
        If FEB.Csb.Load(oCsb, exs) Then
            If FEB.Contact.Load(oCsb.Contact, exs) Then
                Dim oLang As DTOLang = oCsb.Contact.Lang
                Dim sRecipients = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.Facturacio, oCsb.Contact)

                Dim oMailMessage = DTOMailMessage.Factory(sRecipients)
                With oMailMessage
                    .bcc = {"matias@matiasmasso.es"}.ToList
                    .subject = "M+O: " & String.Format(oLang.Tradueix("Aviso de vencimiento por {0} el próximo {1}", "Avis de venciment {0} el proper {1}", "Due date reminder {0} next {1}"), DTOAmt.CurFormatted(oCsb.Amt), oCsb.Vto.ToShortDateString)
                    .bodyUrl = FEB.Mailing.BodyUrl(DTODefault.MailingTemplates.mailVenciment, oCsb.Guid.ToString())
                End With

                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_Impagat()
        Dim exs As New List(Of Exception)
        Dim oMinimGastos As DTOAmt = Await FEB.Default.EmpAmt(Current.Session.Emp, DTODefault.Codis.despesesImpagoMinim, exs)
        If exs.Count = 0 Then
            Dim DcGastosPercentage As Decimal = Await FEB.Default.EmpDecimal(Current.Session.Emp, DTODefault.Codis.despesesImpago, exs)
            If exs.Count = 0 Then
                Dim oImpagats As New List(Of DTOImpagat)
                For Each oCsb In _Csbs
                    Dim oGastos = oCsb.Amt.Percent(DcGastosPercentage)
                    If oMinimGastos.IsGreaterThan(oGastos) Then
                        oGastos = oMinimGastos
                    End If

                    Dim oImpagat As New DTOImpagat
                    With oImpagat
                        .Csb = oCsb
                        .Gastos = oGastos
                    End With
                    oImpagats.Add(oImpagat)
                Next

                Dim oFrm As New Frm_Banc_Impago(oImpagats)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub



End Class

