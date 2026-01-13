
Imports Microsoft.Office.Interop

Public Class Menu_Alb
    Inherits Menu_Base

    Private _Deliveries As List(Of DTODelivery)
    Private mAlbs As Albs
    Private mEmp As DTOEmp

    Public Sub New(ByVal oAlb As Alb)
        MyBase.New()
        mAlbs = New Albs
        mAlbs.Add(oAlb)
        mEmp = BLLApp.Emp
        'AddHandler oAlb.AfterUpdate, AddressOf mAlbs.RaiseEventRefreshRequest
        _Deliveries = New List(Of DTODelivery)
        _Deliveries.Add(New DTODelivery(oAlb.Guid))
    End Sub

    Public Sub New(ByVal oAlbs As Albs)
        MyBase.New()
        mAlbs = oAlbs
        If mAlbs.Count > 0 Then
            mEmp = mAlbs(0).Emp
        End If

        _Deliveries = New List(Of DTODelivery)
        For Each oAlb As Alb In oAlbs
            _Deliveries.Add(New DTODelivery(oAlb.Guid))
        Next
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_ZoomOldFormat(),
        MenuItem_Print(),
        MenuItem_Pdf(),
        MenuItem_CopyLink(),
        MenuItem_Email(),
        MenuItem_EmailConfirmationRequest(),
        MenuItem_EmailSubscriptors(),
        MenuItem_Proforma(),
        MenuItem_Tpv(),
        MenuItem_ComparaTransportistes(),
        MenuItem_SeguimentTransportista(),
        MenuItem_ShowRepComs(),
        MenuItem_Factura(),
        MenuItem_Facturar(),
        MenuItem_Justificante(),
        MenuItem_Cobrar(),
        MenuItem_Recollida(),
        MenuItem_Avançats(),
        MenuItem_Del()})
    End Function
    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If mAlbs.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_ZoomOldFormat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom (antic format)"
        oMenuItem.Image = My.Resources.prismatics
        If mAlbs.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_ZoomOldFormat
        Return oMenuItem
    End Function

    Private Function MenuItem_Print() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Imprimir"
        oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_AlbPrint
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_AlbPdf
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        oMenuItem.Enabled = mAlbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Email() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email..."
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_AlbEmail
        Return oMenuItem
    End Function

    Private Function MenuItem_EmailConfirmationRequest() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email confirmació enviament"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_EmailConfirmationRequest
        Return oMenuItem
    End Function



    Private Function MenuItem_EmailSubscriptors() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email a subscriptors"
        oMenuItem.Enabled = False
        Try
            If mAlbs.Count = 1 Then
                If mAlbs(0).SubscriptorsExist Then
                    oMenuItem.Enabled = True
                End If
            End If
        Catch ex As Exception
        End Try

        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_AlbEmailSubscriptors
        Return oMenuItem
    End Function

    Private Function MenuItem_Proforma() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Proforma"
        With oMenuItem.DropDownItems
            .Add(MenuItem_ProformaPrint)
            .Add(MenuItem_ProformaPdf)
            .Add(MenuItem_ProformaEmail)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaPrint() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Imprimir"
        oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_ProformaPrint
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaPdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_ProformaPdf
        Return oMenuItem
    End Function

    Private Function MenuItem_ProformaEmail() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email"
        oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_ProformaEmail
        Return oMenuItem
    End Function

    Private Function MenuItem_Tpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar enllaç a Tpv"
        'oMenuItem.Image = My.Resources.MailSobreObert
        AddHandler oMenuItem.Click, AddressOf Do_CopyTpv
        Return oMenuItem
    End Function

    Private Function MenuItem_ComparaTransportistes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compara Transportistes"
        'oMenuItem.Image = My.Resources.deltext
        oMenuItem.Enabled = mAlbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ComparaTransportistes
        Return oMenuItem
    End Function

    Private Function MenuItem_SeguimentTransportista() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "seguiment transportista"
        AddHandler oMenuItem.Click, AddressOf Do_SeguimentTransportista
        Return oMenuItem
    End Function


    Private Function MenuItem_ShowRepComs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "comisions"
        oMenuItem.Visible = BLL.BLLSession.Current.User.Rol.IsMainboard
        AddHandler oMenuItem.Click, AddressOf Do_ShowRepComs
        Return oMenuItem
    End Function

    Private Function MenuItem_Facturar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Facturar"
        oMenuItem.Image = My.Resources.Gears
        oMenuItem.Enabled = AllowFrx()
        AddHandler oMenuItem.Click, AddressOf Do_Factura2
        Return oMenuItem
    End Function

    Private Function MenuItem_Factura() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Factura"
        oMenuItem.Image = My.Resources.notepad
        If mAlbs.Count = 1 Then
            If mAlbs(0).Fra Is Nothing Then
                oMenuItem.Enabled = False
            Else
                oMenuItem.Enabled = mAlbs(0).Fra.Id > 0
            End If
        Else
            oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Factura2
        Return oMenuItem
    End Function


    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eliminar"
        oMenuItem.Image = My.Resources.DelText
        If Not mAlbs(0).AllowDelete Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function

    Private Function MenuItem_Justificante() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "justificante"
        With oMenuItem.DropDownItems
            .Add(MenuItem_JustificanteNone)
            .Add(MenuItem_JustificanteSolicitado)
            .Add(MenuItem_JustificanteRecibido)
        End With
        Return oMenuItem
    End Function

    Private Function MenuItem_Cobrar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case mAlbs(0).CashCod
            Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa
                oMenuItem.Text = "cobrar transf.previa"
                oMenuItem.Image = My.Resources.DollarOrange2
                oMenuItem.Enabled = mAlbs.Count = 1
                AddHandler oMenuItem.Click, AddressOf Do_TransferenciaPrevia
            Case Else
                oMenuItem.Visible = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_JustificanteNone() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "no disponible"
        oMenuItem.Checked = mAlbs(0).Justificante = Alb.JustificanteCodes.None
        'oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_JustificanteNone
        Return oMenuItem
    End Function

    Private Function MenuItem_JustificanteSolicitado() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "sol.licitat"
        oMenuItem.Checked = mAlbs(0).Justificante = Alb.JustificanteCodes.Solicitado
        'oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_JustificanteSolicitado
        Return oMenuItem
    End Function

    Private Function MenuItem_JustificanteRecibido() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "rebut"
        oMenuItem.Checked = mAlbs(0).Justificante = Alb.JustificanteCodes.Recibido
        'oMenuItem.Image = My.Resources.printer
        AddHandler oMenuItem.Click, AddressOf Do_JustificanteRecibido
        Return oMenuItem
    End Function

    Private Function MenuItem_Recollida() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If mAlbs(0).Cod = DTOPurchaseOrder.Codis.Client Then
            oMenuItem.Text = "recollida"
            oMenuItem.Enabled = mAlbs.Count = 1
            AddHandler oMenuItem.Click, AddressOf Do_Recollida
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Avançats() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançats..."
        Select Case BLL.BLLSession.Current.User.Rol.Id
            Case DTORol.Ids.SuperUser
                oMenuItem.DropDownItems.Add(MenuItem_Abona)
                oMenuItem.DropDownItems.Add(MenuItem_CurExchange)
            Case DTORol.Ids.Admin, DTORol.Ids.Accounts
                oMenuItem.DropDownItems.Add(MenuItem_CurExchange)
        End Select
        oMenuItem.DropDownItems.Add(MenuItem_SonaeLabels)
        Return oMenuItem
    End Function

    Private Function MenuItem_Abona() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "abona"
        oMenuItem.Enabled = mAlbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Abona
        Return oMenuItem
    End Function

    Private Function MenuItem_CurExchange() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "aplica canvi de divisa"
        oMenuItem.Enabled = mAlbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_CurExchange
        Return oMenuItem
    End Function
    Private Function MenuItem_SonaeLabels() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "etiquetes Sonae"
        oMenuItem.Enabled = mAlbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SonaeLabels
        Return oMenuItem
    End Function

    Private Sub Do_CurExchange()
        Dim oFrm As New Frm_CurExchange(_Deliveries.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Do_JustificanteNone()
        mAlbs(0).UpdateJustificante(Alb.JustificanteCodes.None)
        Me.RefreshRequest(mAlbs, New System.EventArgs)
    End Sub

    Private Sub Do_JustificanteSolicitado()
        mAlbs(0).UpdateJustificante(Alb.JustificanteCodes.Solicitado)
        Me.RefreshRequest(mAlbs, New System.EventArgs)
    End Sub

    Private Sub Do_JustificanteRecibido()
        mAlbs(0).UpdateJustificante(Alb.JustificanteCodes.Recibido)
        Me.RefreshRequest(mAlbs, New MatEventArgs(mAlbs(0)))
    End Sub

    Private Sub Do_TransferenciaPrevia()
        Dim oAlb As Alb = mAlbs(0)
        Dim oFrm As New Frm_Cobrament_TransferenciaPrevia(oAlb)
        AddHandler oFrm.AfterUpdate, AddressOf AfterCobroTransferencia
        oFrm.Show()
    End Sub

    Private Sub AfterCobroTransferencia(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oFrm As Frm_Cobrament_TransferenciaPrevia = sender
        Dim oCca As Cca = oFrm.Cca
        Dim sLogText As String = BLLUser.LogText(oCca.UserCreated, oCca.UserLastEdited, oCca.FchCreated, oCca.FchLastEdited)
        BLL.MailHelper.SendMail(WellKnownRecipients.Info, oCca.Txt, sLogText & vbCrLf & "Missatge automatic enviat quan es registra el pagament de un albará per transferencia previa")
        RefreshRequest(Me, New MatEventArgs(mAlbs(0)))
    End Sub

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAlb As Alb = mAlbs(0)
        oAlb.SetItm()

        Select Case oAlb.Cod
            Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Proveidor, DTOPurchaseOrder.Codis.Reparacio
                Dim exs As New List(Of Exception)
                Dim oCustomer As New DTOCustomer(oAlb.Client.Guid)
                BLL.BLLContact.Load(oCustomer)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_AlbNew2(oAlb)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If
            Case DTOPurchaseOrder.Codis.Traspas
                Dim oDelivery As DTODelivery = _Deliveries.First
                Dim oFrm As New Frm_TraspasDelivery(oDelivery)
                oFrm.Show()
            Case Else
        End Select

    End Sub

    Private Sub Do_ZoomOldFormat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_AlbNew2(mAlbs(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub Do_AlbPrint(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintAlbs(mAlbs)
    End Sub

    Private Sub Do_ProformaPrint(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintProforma(mAlbs)
    End Sub

    Private Sub Do_AlbPdf(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowPdf(False)
    End Sub

    Private Sub Do_ProformaPdf(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowPdf(True)
    End Sub

    Private Sub Do_AlbEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Email(False)
    End Sub

    Private Sub Do_EmailConfirmationRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDelivery As DTODelivery = BLLDelivery.Find(mAlbs(0).Guid)
        Dim sSubject As String = ""
        Select Case oDelivery.CashCod
            Case DTOCustomer.CashCodes.TransferenciaPrevia, DTOCustomer.CashCodes.Visa
                sSubject = oDelivery.Customer.Lang.Tradueix("Envío pendiente de transferencia #" & oDelivery.Id & " (requiere acción)", "Enviament pendent de transferencia #" & oDelivery.Id & " (requereix acció)", "Transfer pending shipment #" & oDelivery.Id & " (action required)")
            Case Else
                If BLL.BLLContactClass.WellKnown(DTO.DTOContactClass.Wellknowns.Farmacia).Equals(oDelivery.Customer.ContactClass) Then
                    sSubject = oDelivery.Customer.Lang.Tradueix("Aviso de envío #" & oDelivery.Id, "Avis d'enviament #" & oDelivery.Id, "Shipment advice #" & oDelivery.Id)
                Else
                    sSubject = oDelivery.Customer.Lang.Tradueix("Solicitud de confirmación de envío #" & oDelivery.Id & " (requiere respuesta)", "Sol•licitut de confirmació d'enviament #" & oDelivery.Id & " (requereix resposta)", "Shipment confirmation request #" & oDelivery.Id & " (answer required)")
                End If
        End Select
        Dim oRecipients As System.Net.Mail.MailAddressCollection = BLL.BLLSubscriptors.MailAddressCollection(DTOSubscription.Ids.ConfirmacioEnviament, oDelivery.Customer)
        If oRecipients.Count = 0 Then
            Dim oUser As DTOUser = BLL.BLLContact.DefaultUser(oDelivery.Customer)
            If oUser IsNot Nothing Then oRecipients.Add(oUser.EmailAddress)
        End If
        Dim url As String = BLLDelivery.EmailConfirmationRequestUrl(oDelivery)
        Dim exs As New List(Of Exception)
        If Not MatOutlook.NewMessage(oRecipients.ToString.Replace(",", ";"), "", , sSubject, , url, , exs) Then
            UIHelper.WarnError(exs, "error al redactar missatge. Verificar " & url)
        End If
    End Sub

    Private Sub Do_AlbEmailSubscriptors(ByVal sender As Object, ByVal e As System.EventArgs)
        mAlbs(0).MailToSubscriptors()
    End Sub

    Private Sub Do_ProformaEmail(ByVal sender As Object, ByVal e As System.EventArgs)
        Email(True)
    End Sub


    Private Sub Do_Print(ByVal sender As Object, ByVal e As System.EventArgs)
        root.PrintAlbs(mAlbs)
    End Sub

    Private Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFileName As String = "M+O " & BLLDeliveries.ToString(_Deliveries, BLLSession.Current.Lang) & ".pdf"

        Dim oDlg As New System.Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "Guardar albarans"
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                Dim oPdf As New BLLPdfAlb(_Deliveries, BlProforma:=False)
                oPdf.Save(.FileName)
            End If
        End With
    End Sub


    Private Sub Do_CopyTpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDelivery As New DTODelivery(mAlbs(0).Guid)
        Dim url As String = BLLDelivery.UrlTpv(oDelivery, True)
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Sub Do_ComparaTransportistes(ByVal sender As Object, ByVal e As System.EventArgs)
        'comparar transportistes
        Dim oFrm As New Frm_AlbTrps(_Deliveries.First)
        oFrm.Show()
    End Sub

    Private Sub Do_SeguimentTransportista(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDelivery As New DTODelivery(mAlbs(0).Guid)
        Dim sUrl As String = BLLDelivery.UrlAlbSeguiment(oDelivery)
        UIHelper.ShowHtml(mAlbs(0).UriTrpSeguiment.ToString)
    End Sub

    Private Sub Do_ShowRepComs()
        Dim oFrm As New Frm_AlbRepComs(mAlbs(0))
        oFrm.Show()
    End Sub

    Private Sub Do_Factura2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Facturacio_Old(mAlbs)
        oFrm.Show()
    End Sub

    Private Sub Do_Abona(sender As Object, e As System.EventArgs)
        Dim oAbono As DTODelivery = BLLDelivery.Abonar(_Deliveries.First, BLLSession.Current.User)
        Dim exs As New List(Of Exception)
        If BLLDelivery.Update(oAbono, exs) Then
            MsgBox(String.Format("Abonament registrat amb el num.", oAbono.Id))
            MyBase.RefreshRequest(Me, New MatEventArgs(oAbono))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult
        Select Case _Deliveries.Count
            Case 1
                rc = MsgBox("Eliminem l'albará " & _Deliveries(0).Id & " de " & _Deliveries(0).Nom & "?", MsgBoxStyle.OkCancel, "M+O")
            Case Else
                rc = MsgBox("Eliminem " & _Deliveries.ToString & "?", MsgBoxStyle.OkCancel, "M+O")
        End Select
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLLDeliveries.Delete(_Deliveries, exs) Then
                MsgBox("Albarans eliminats", MsgBoxStyle.Information, "M+O")
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If

    End Sub



    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAlb As Alb = mAlbs(0)
        Dim oDelivery As New DTODelivery(oAlb.Guid)
        Dim sUrl As String = BLL.BLLDelivery.Url(oDelivery, True) ' oAlb.Url(True)
        Clipboard.SetDataObject(sUrl, True)
        MsgBox("Copiat enllaç al portapapers:" & vbCrLf & "albará " & oAlb.Id & " de " & oAlb.Client.Clx)
    End Sub

    Private Sub Do_Recollida(sender As Object, e As System.EventArgs)
        Dim oAlb As Alb = mAlbs(0)
        Dim oRecollida As Recollida = Recollida.NewFromAlb(oAlb)
        Dim oFrm As New Frm_Alb_Recollida(oRecollida)
        oFrm.Show()
    End Sub

    Private Sub Do_SonaeLabels()
        Dim oPdf As New PdfSonaeLogisticLabel(_Deliveries.First)
        Dim oByteArray() As Byte = oPdf.Stream
        Dim exs As New List(Of Exception)
        Dim oDocFile As DTODocFile = BLLDocFile.FromStream(oByteArray, exs)
        If oPdf.exs.Count > 0 Then
            UIHelper.WarnError(oPdf.exs)
        End If
        UIHelper.ShowStream(oDocFile)

    End Sub


    '==========================================================================
    '                               AUXILIARS
    '==========================================================================
    Private Function AllowFrx() As Boolean
        Dim retval As Boolean = True
        For Each oAlb As Alb In mAlbs
            If oAlb.Fra IsNot Nothing Then
                If oAlb.Fra.Id > 0 Then
                    retval = False
                    Exit For
                ElseIf oAlb.Facturable = False Then
                    retval = False
                    Exit For
                End If
            End If
        Next
        Return retval
    End Function

    Private Sub Email(ByVal BlProforma As Boolean)
        'si tots els albarans son del mateix client
        If mAlbs.SameClient Then
            Dim oFirstAlb As Alb = mAlbs(0)
            Dim oClient As Client = oFirstAlb.Client()
            Dim oGuid As Guid = oClient.Guid

            Dim sRecipient As String = oClient.Email
            If sRecipient > "" Then
                Dim sSubject As String = mAlbs.Text(BlProforma).ToUpper
                Dim sDisplayName As String = "M+O " & sSubject & ".pdf"
                Dim sFileName As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal) & "\" & sDisplayName

                'mAlbs.Pdf(BlProforma, sFileName)
                'Dim oPdf As New PdfAlb(mAlbs, BlProforma)
                'oPdf.Save(sFileName)

                'Dim oPdf As New BLLPdfAlb(_Deliveries, BlProforma)
                'oPdf.Save(sFileName)


                Dim oPdf As New PdfDelivery(_Deliveries, BlProforma)
                oPdf.Print(PdfDelivery.Valoracio.Inherit, Nothing)
                oPdf.Save(sFileName)

                Dim oOlApp As New Outlook.Application
                Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
                With oNewMail
                    .Recipients.Add(sRecipient)
                    .Subject = sSubject
                    .Attachments.Add(sFileName, , , sDisplayName)
                    .Display()
                End With
            Else
                MsgBox("no hi ha cap correu registrat a la fitxa d'aquest client", MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("cal triar albarans del mateix client per enviar-los per correu", MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Sub ShowPdf(ByVal BlProforma As Boolean)
        'root.ShowPdf(mAlbs.PdfStream(BlProforma, True))
        Dim oPdf As New PdfDelivery(_Deliveries, BlProforma)
        oPdf.Print(PdfDelivery.Valoracio.Inherit, Nothing)
        UIHelper.ShowPdfStream(oPdf)
    End Sub

    Private Sub SavePdf(ByVal BlProforma As Boolean)
        Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Dim sFileName As String = "M+O " & BLLDeliveries.ToString(_Deliveries, BLLSession.Current.Lang, BlProforma) & ".pdf"
        Dim sTit As String = IIf(BlProforma, "PROFORMES", "COMANDES")
        Dim oDlg As New System.Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR " & sTit
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

            If .ShowDialog() = DialogResult.OK Then
                Dim oPdf As New BLLPdfAlb(_Deliveries, BlProforma)
                oPdf.Save(.FileName)
            End If

        End With

    End Sub



End Class
