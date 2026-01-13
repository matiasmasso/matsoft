

Public Class Menu_Csb

    Private mCsb As Csb
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCsb As Csb)
        MyBase.New()
        mCsb = oCsb
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Vto(), _
        MenuItem_MailVenciment(), _
        MenuItem_MailImpagat(), _
        MenuItem_CopyTpv(), _
        MenuItem_Impagat(), _
        MenuItem_Rebut(), _
        MenuItem_Reclamar(), _
        MenuItem_Advance()})
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

    Private Function MenuItem_Reclamar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Reclamar"
        oMenuItem.Image = My.Resources.UNDO
        AddHandler oMenuItem.Click, AddressOf Do_reclamar
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
        oMenuItem.Enabled = Not mCsb.VtoCca Is Nothing
        'oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Vto
        Return oMenuItem
    End Function

    Private Function MenuItem_MailImpagat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email impagat"
        oMenuItem.Enabled = mCsb.Impagat
        oMenuItem.Image = My.Resources.MailSobreGroc
        AddHandler oMenuItem.Click, AddressOf Do_MailImpagat
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyTpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "enllaç a Tpv"
        oMenuItem.Enabled = mCsb.Impagat
        'oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_CopyTpv
        Return oMenuItem
    End Function

    Private Function MenuItem_Impagat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "entrar impagat"
        oMenuItem.Enabled = Not mCsb.Impagat
        'oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Impagat
        Return oMenuItem
    End Function

    Private Function MenuItem_MailVenciment() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avis de venciment"
        oMenuItem.Image = My.Resources.Outlook_16
        AddHandler oMenuItem.Click, AddressOf Do_MailVenciment
        Return oMenuItem
    End Function

    Private Function MenuItem_Advance() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avançades"
        If BLL.BLLSession.Current.User.Rol.IsAdmin Then
            oMenuItem.DropDownItems.Add(SubMenuItem_CreateVto)
            oMenuItem.DropDownItems.Add(SubMenuItem_Pnd)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CreateVto() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_CreateVto
        oMenuItem.Text = "registra venciment"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Pnd() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = Not mCsb.VtoCca.Exists
        AddHandler oMenuItem.Click, AddressOf Do_ShowPnd
        oMenuItem.Text = "partida pendent"
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCsb(mCsb)
    End Sub

    Private Sub Do_Rebut(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowRebut(mCsb.Rebut)
    End Sub

    Private Sub Do_Vto(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCca(mCsb.VtoCca)
    End Sub

    Private Sub Do_ShowPnd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPnd As Pnd = mCsb.Pnd
        Dim oFrm As New Frm_Contact_Pnd(oPnd.ToDTO)
        oFrm.Show()
    End Sub

    Private Sub Do_CreateVto(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as New List(Of exception)
        Dim oCca As Cca = mCsb.SaveVto( exs)
        If oCca Is Nothing Then
            MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        Else
            MsgBox("venciment registrat a l'assentament " & oCca.Id & vbCrLf & oCca.Txt, MsgBoxStyle.Information, "MAT.NET")
        End If
    End Sub


    Private Sub Do_Reclamar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("reclamem aquest efecte?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mCsb.Reclama( exs) Then
                Dim oMail As New Mail(mCsb.Csa.emp, Today)
                With oMail
                    .Contacts.Add(mCsb.Csa.Banc)
                    .Cod = DTO.DTOCorrespondencia.Cods.Enviat
                    .Subject = "RECLAMACIO EFECTE " & mCsb.Document
                    Dim oDoc As New PdfBancReclamacioEfecte(mCsb, oMail)

                    Dim oDocFile As DTODocFile = Nothing
                    If BLL_DocFile.LoadFromStream(oDoc.Stream, oDocFile, exs) Then
                        .DocFile = oDocFile
                        .DocFile.PendingOp = DTODocFile.PendingOps.Update
                    End If

                End With

                If oMail.Update( exs) Then
                    RefreshRequest(mCsb, EventArgs.Empty)
                    UIHelper.ShowStream(oMail.DocFile)
                Else
                    UIHelper.WarnError( exs, "error al desar el document a correspondencia")
                End If

            Else
                UIHelper.WarnError( exs, "error al reclamar l'efecte")
            End If
        Else
            UIHelper.WarnError("Operació cancelada per l'usuari")
        End If
    End Sub

    Private Sub Do_MailVenciment()
        Dim oCsb As MaxiSrvr.DTOCsb = BLL_Csb.Find(mCsb.Guid)
        Dim oLang As DTOLang = oCsb.Contact.Lang


        Dim oSsc As New DTOSubscription(DTOSubscription.Ids.Facturacio)
        Dim oSubscriptors As List(Of DTOSubscriptor) = BLL.BLLSubscriptors.All(oSsc, oCsb.Contact)

        Dim sb As New System.Text.StringBuilder
        For Each oSubscriptor As DTOSubscriptor In oSubscriptors
            sb.Append(oSubscriptor.EmailAddress & ";")
        Next

        Dim sSubject As String = "M+O: " & String.Format(oLang.Tradueix("Aviso de vencimiento por {0} el próximo {1}", "Avis de venciment {0} el proper {1}", "Due date reminder {0} next {1}"), BLL_Csb.AmtFormated(oCsb), BLL_Csb.Venciment(oCsb))
        Dim sBodyUrl As String = BLL.BLLMailing.BodyUrl(BLL.BLLMailing.Templates.MailVenciment, oCsb.Guid.ToString)
        Dim exs as New List(Of exception)
        If Not MatOutlook.NewMessage(sb.ToString, "", , sSubject, , sBodyUrl, , exs) Then
            UIHelper.WarnError( exs, "error al redactar missatge")
        End If
    End Sub


    Private Sub Do_Impagat()
        'Dim oImpagats As New Impagats
        'oImpagats.Add(New Impagat(mCsb))

        'Dim oFrm As New Frm_Banc_Impago
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'With oFrm
        ' .Impagats = oImpagats
        ' .Show()
        ' End With
    End Sub

    Private Sub Do_MailImpagat(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oCsb As DTO.DTOCsb = BLLCsb.find(mCsb.Guid)
        Dim oImpagat As DTOImpagat = BLLImpagat.FromCsb(oCsb)
        Dim oMsg As New ImpagatMsg(oImpagat)
        If oMsg.MailTo > "" Then
            Dim exs As New List(Of Exception)
            If Not MatOutlook.NewMessage(oMsg.MailTo, oMsg.MailCc, "", oMsg.Subject, , oMsg.Body, , exs) Then
                UIHelper.WarnError(exs, "error al redactar missatge")
            End If
        Else
            MsgBox("aquest client no te cap adreça de email registrada" & vbCrLf & "cal imprimir el missatge i enviar-lo per fax", MsgBoxStyle.Exclamation, "MAT.NET")
            Dim sMailTo As String = "cuentas@matiasmasso.es"
            Dim exs As New List(Of Exception)
            If Not MatOutlook.NewMessage(sMailTo, oMsg.MailCc, "", oMsg.Subject, , oMsg.Body, , exs) Then
                UIHelper.WarnError(exs, "error al redactar missatge")
            End If
        End If
    End Sub

    Private Sub Do_CopyTpv()
        Dim oImpagat As New Impagat(mCsb)
        Dim url As String = BLL_Tpv.url(oImpagat)
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class

