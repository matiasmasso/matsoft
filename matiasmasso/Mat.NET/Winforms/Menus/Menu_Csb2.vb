Public Class Menu_Csb2
    Inherits Menu_Base

    Private _Csb As DTOCsb


    Public Sub New(ByVal oCsb As DTOCsb)
        MyBase.New()
        _Csb = oCsb
        Dim exs As New List(Of Exception)
        FEB2.Csb.Load(_Csb, exs)
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
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Csa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Remesa"
        AddHandler oMenuItem.Click, AddressOf Do_Csa
        Return oMenuItem
    End Function

    Private Function MenuItem_Lliurat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Lliurat"
        Dim oMenuContact As New Menu_Contact(_Csb.Contact)
        oMenuItem.DropDownItems.AddRange(oMenuContact.Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Ccc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "compte bancari"
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
        If _Csb.Iban Is Nothing Then
            oMenuItem.Enabled = False
        ElseIf _Csb.Iban.BankBranch Is Nothing Then
            oMenuItem.Enabled = False
        ElseIf _Csb.Iban.BankBranch.Bank Is Nothing Then
            oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Bank
        Return oMenuItem
    End Function

    Private Function MenuItem_BankBranch() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Oficina bancaria"
        If _Csb.Iban Is Nothing Then
            oMenuItem.Enabled = False
        ElseIf _Csb.Iban.BankBranch Is Nothing Then
            oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_BankBranch
        Return oMenuItem
    End Function

    Private Function MenuItem_Mandate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Mandato bancari"
        If _Csb.Iban Is Nothing Then
            oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Mandate
        Return oMenuItem
    End Function

    Private Function MenuItem_Reclamar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Reclamar"
        oMenuItem.Enabled = _Csb.Result <> DTOCsb.Results.Reclamat
        AddHandler oMenuItem.Click, AddressOf Do_Reclamar
        Return oMenuItem
    End Function

    Private Function MenuItem_RetrocedirReclamacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedir reclamació"
        oMenuItem.Visible = _Csb.Result = DTOCsb.Results.Reclamat
        AddHandler oMenuItem.Click, AddressOf Do_RetrocedirReclamacio
        Return oMenuItem
    End Function

    Private Function MenuItem_Rebut() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Rebut"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Rebut
        Return oMenuItem
    End Function

    Private Function MenuItem_Vto() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "veure assentament venciment"
        'oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Vto
        Return oMenuItem
    End Function


    Private Function MenuItem_Impagat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "entrar impagat"
        'oMenuItem.Enabled = Not _Csb.Impagat
        'oMenuItem.Image = My.Resources.pdf
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
        'oMenuItem.Enabled = Not _Csb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_CreateVto
        oMenuItem.Text = "registra venciment"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Pnd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not _Csb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_ShowPnd
        oMenuItem.Text = "partida pendent"
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Csb(_Csb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Csa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Csa(_Csb.Csa)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Bank()
        Dim oFrm As New Frm_Bank(_Csb.Iban.BankBranch.Bank)
        AddHandler oFrm.AfterUpdate, AddressOf OnBankUpdated
        oFrm.Show()
    End Sub
    Private Sub Do_BankBranch()
        Dim oFrm As New Frm_BankBranch(_Csb.Iban.BankBranch)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
    Private Sub Do_Mandate()
        Dim oFrm As New Frm_Iban(_Csb.Iban)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub OnBankUpdated(sender As Object, e As MatEventArgs)
        _Csb.Iban.BankBranch.Bank = e.Argument
        If _Csb.Csa IsNot Nothing Then
            DTOCsb.Validate(_Csb, _Csb.Csa.FileFormat)
        End If
        RefreshRequest(Me, New MatEventArgs(_Csb))
    End Sub

    Private Sub Do_Rebut(ByVal sender As Object, ByVal e As System.EventArgs)
        'root.ShowRebut(_Csb.Rebut)
    End Sub

    Private Sub Do_Vto(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As DTOCca = _Csb.ResultCca
        If oCca IsNot Nothing Then
            Dim oFrm As New Frm_Cca(oCca)
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_ShowPnd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPnd As DTOPnd = _Csb.Pnd
        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        oFrm.Show()
    End Sub

    Private Async Sub Do_CreateVto(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oUser As DTOUser = Current.Session.User
        Dim exs As New List(Of Exception)
        If Await FEB2.Csb.SaveVto(exs, _Csb, oUser) Then
            MsgBox("venciment registrat a l'assentament " & _Csb.ResultCca.Id & vbCrLf & _Csb.ResultCca.Concept, MsgBoxStyle.Information, "MAT.NET")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Do_Reclamar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("reclamem aquest efecte?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim oCca = Await FEB2.Csb.Reclama(exs, Current.Session.User, _Csb)
            If exs.Count = 0 Then
                Dim oMail = DTOCorrespondencia.Factory(Current.Session.User, _Csb.Csa.Banc, DTOCorrespondencia.Cods.Enviat)
                With oMail
                    .Subject = "RECLAMACIO EFECTE " & _Csb.FormattedId()
                    FEB2.Csb.Load(_Csb, exs)
                    FEB2.Banc.Load(_Csb.Csa.Banc, exs)
                    FEB2.Contact.Load(_Csb.Csa.Banc, exs)
                    Dim oDoc As New LegacyHelper.PdfBancReclamacioEfecte(_Csb, oMail)
                    .docFile = LegacyHelper.DocfileHelper.Factory(exs, oDoc.Stream(exs), MimeCods.Pdf)
                    If Await FEB2.Correspondencia.Upload(oMail, exs) Then
                        RefreshRequest(Me, New MatEventArgs(_Csb))
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
        Dim rc As MsgBoxResult = MsgBox("Tornem a deixar l'efecte com abans de reclamar-lo?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Csb.RetrocedeixReclamacio(Current.Session.User, _Csb, exs) Then
                RefreshRequest(Me, New MatEventArgs(_Csb))
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
        If FEB2.Csb.Load(_Csb, exs) Then
            If FEB2.Contact.Load(_Csb.Contact, exs) Then
                Dim oLang As DTOLang = _Csb.Contact.lang
                Dim sRecipients = Await FEB2.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.Facturacio, _Csb.contact)

                Dim oMailMessage = DTOMailMessage.Factory(sRecipients)
                With oMailMessage
                    .Bcc = {"matias@matiasmasso.es"}.ToList
                    .Subject = "M+O: " & String.Format(oLang.Tradueix("Aviso de vencimiento por {0} el próximo {1}", "Avis de venciment {0} el proper {1}", "Due date reminder {0} next {1}"), DTOAmt.CurFormatted(_Csb.Amt), _Csb.Vto.ToShortDateString)
                    .BodyUrl = FEB2.Mailing.BodyUrl(DTODefault.MailingTemplates.MailVenciment, _Csb.Guid.ToString())
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
        If FEB2.Csb.Load(_Csb, exs) Then
            Dim oImpagats As New List(Of DTOImpagat)
            Dim oImpagat As New DTOImpagat
                With oImpagat
                    .Csb = _Csb
                    .Gastos = Await FEB2.Impagat.CalculaGastos(Current.Session.Emp, oImpagat, exs)
                End With

                If exs.Count = 0 Then
                    oImpagats.Add(oImpagat)

                    Dim oFrm As New Frm_Banc_Impago(oImpagats)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If

            Else
                UIHelper.WarnError(exs)
        End If

    End Sub



End Class

