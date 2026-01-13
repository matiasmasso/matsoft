
Imports Microsoft.Office.Interop

Public Class Menu_Alb
    Inherits Menu_Base

    Private mAlbs As Albs
    Private mEmp As DTOEmp

    Public Sub New(ByVal oAlb As Alb)
        MyBase.New()
        mAlbs = New Albs
        mAlbs.Add(oAlb)
        mEmp = oAlb.Emp
        'AddHandler oAlb.AfterUpdate, AddressOf mAlbs.RaiseEventRefreshRequest
    End Sub

    Public Sub New(ByVal oAlbs As Albs)
        MyBase.New()
        mAlbs = oAlbs
        If mAlbs.Count > 0 Then
            mEmp = mAlbs(0).Emp
        End If
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_ZoomOldFormat(), _
        MenuItem_Print(), _
        MenuItem_Pdf(), _
        MenuItem_CopyLink(), _
        MenuItem_Email(), _
        MenuItem_EmailConfirmationRequest(), _
        MenuItem_EmailSubscriptors(), _
        MenuItem_Proforma(), _
        MenuItem_Tpv(), _
        MenuItem_ComparaTransportistes(), _
        MenuItem_SeguimentTransportista(), _
        MenuItem_ShowRepComs(), _
        MenuItem_Factura(), _
        MenuItem_Facturar(), _
        MenuItem_SwitchClient(), _
        MenuItem_Justificante(), _
        MenuItem_Cobrar(), _
        MenuItem_Recollida(), _
        MenuItem_Avançats(), _
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
        AddHandler oMenuItem.Click, AddressOf Do_Tpv
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
        'oMenuItem.Image 
        oMenuItem.Enabled = False
        If mAlbs.Count = 1 Then
            Dim oAlb As Alb = mAlbs(0)
            If oAlb.Transmisio IsNot Nothing Then
                If oAlb.Transmisio.Id > 0 Then
                    If oAlb.UriTrpSeguiment IsNot Nothing Then
                        oMenuItem.Enabled = True
                    End If
                End If
            End If
        End If
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

    Private Function MenuItem_SwitchClient() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "canviar client"
        oMenuItem.Image = My.Resources.candau
        Select Case App.Current.emp.WinUsr.Rol.Id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
            Case Else
                oMenuItem.Visible = False
        End Select
        oMenuItem.Enabled = mAlbs.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_SwitchClient
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eliminar"
        oMenuItem.Image = My.Resources.DelText
        oMenuItem.Enabled = mAlbs.Count = 1
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
            Case Rol.Ids.SuperUser
                oMenuItem.DropDownItems.Add(MenuItem_RecuperaAlbPrvRoche)
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_RecuperaAlbPrvRoche() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "recupera alb prv Roche"
        AddHandler oMenuItem.Click, AddressOf Do_RecuperaAlbPrvRoche
        Return oMenuItem
    End Function


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
        Me.RefreshRequest(mAlbs, New System.EventArgs)
    End Sub

    Private Sub Do_TransferenciaPrevia()
        Dim oAlb As Alb = mAlbs(0)
        Dim oFrm As New Frm_Cobrament_TransferenciaPrevia(oAlb)
        AddHandler oFrm.AfterUpdate, AddressOf AfterCobroTransferencia
        oFrm.Show()
    End Sub

    Private Sub AfterCobroTransferencia(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As Frm_Cobrament_TransferenciaPrevia = sender
        BLL.MailHelper.SendMail("COBROS <info@matiasmasso.es>", , , oFrm.Cca.Txt, "albara " & oFrm.Cca.UsrLog.ToString(Lang.CAT, mAlbs(0).Client) & vbCrLf & "Missatge automatic enviat quan es registra el pagament de un albará per transferencia previa")
        RefreshRequest(Me, New MatEventArgs(mAlbs(0)))
    End Sub

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAlb As Alb = mAlbs(0)

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
        Dim oDelivery As DTODelivery = BLL_Delivery.Find(mAlbs(0).Guid)
        Dim sSubject As String = oDelivery.Contact.Lang.Tradueix("Solicitud de confirmación de envío #" & oDelivery.Id & " (requiere respuesta)", "Sol•licitut de confirmació d'enviament #" & oDelivery.Id & " (requereix resposta)", "Shipment confirmation request #" & oDelivery.Id & " (answer required)")
        Dim oRecipients As System.Net.Mail.MailAddressCollection = BLL.BLLSubscriptors.MailAddressCollection(DTOSubscription.Ids.ConfirmacioEnviament, oDelivery.Contact)
        If oRecipients.Count = 0 Then
            Dim oUser As DTOUser = BLL.BLLContact.DefaultUser(oDelivery.Contact)
            If oUser IsNot Nothing Then oRecipients.Add(oUser.EmailAddress)
        End If
        Dim url As String = BLL_Delivery.EmailConfirmationRequestUrl(oDelivery)
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
        Dim sFileName As String = "M+O " & mAlbs.ToString & ".pdf"

        Dim oDlg As New Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR ALBARANS"
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                mAlbs.Pdf(.FileName)
            End If
        End With
    End Sub


    Private Sub Do_Tpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = BLL_Tpv.url(mAlbs(0))
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Sub Do_ComparaTransportistes(ByVal sender As Object, ByVal e As System.EventArgs)
        'comparar transportistes
        root.ShowAlbTrps(mAlbs(0))
    End Sub

    Private Sub Do_SeguimentTransportista(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowHtml(mAlbs(0).UriTrpSeguiment.ToString)
    End Sub

    Private Sub Do_ShowRepComs()
        Dim oFrm As New Frm_AlbRepComs(mAlbs(0))
        oFrm.Show()
    End Sub

    Private Sub Do_Factura2(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ExeFacturacio(mAlbs)
    End Sub

    Private Sub Do_RecuperaAlbPrvRoche(sender As Object, e As System.EventArgs)
        eDiversa.RedoPrvAlb(mAlbs(0))
        RefreshRequest(sender, e)
        MsgBox("Ok")
    End Sub

    Private Sub Do_SwitchClient(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAlb As Alb = mAlbs(0)
        Dim oFra As Fra = oAlb.Fra

        If oFra Is Nothing Then
            Dim oContact As Contact = Nothing
            Dim sKey As String = InputBox("Nou client:", "CANVIAR ALB." & oAlb.Id & " DE CLIENT")
            If sKey > "" Then
                If IsNumeric(sKey) Then
                    Dim iCli As Integer = CInt(sKey)
                    oContact = MaxiSrvr.Contact.FromNum(mEmp, iCli)
                    If Not oContact.Exists Then
                        MsgBox("la fitxa " & oContact.Id & " esta buida", MsgBoxStyle.Exclamation, "MAT.NET")
                        oContact = Nothing
                    End If
                Else
                    Dim oKeys As MaxiSrvr.ContactKeys
                    oKeys = App.Current.emp.GetContactKeysFromSearchKey(sKey)
                    Select Case oKeys.Count
                        Case 0
                            MsgBox("No s'ha trobat cap contacte per" & sKey, MsgBoxStyle.Exclamation, "MAT.NET")
                            oContact = Nothing
                        Case 1
                            oContact = oKeys(0)
                        Case Else
                            Dim oFrm As New Frm_Contacts_Select_Old
                            With oFrm
                                .ContactKeys = oKeys
                                .ShowDialog()
                                oContact = .ContactKey
                            End With
                    End Select
                End If
            End If

            If oContact IsNot Nothing Then
                Dim rc As MsgBoxResult = MsgBox("canviem l'albará " & oAlb.Id & " del " & oAlb.Fch & vbCrLf & "de " & oAlb.Client.Clx & vbCrLf & "a " & oContact.Clx & "?", MsgBoxStyle.OkCancel, "MAT.NET")
                If rc = MsgBoxResult.Ok Then
                    oAlb.SetItm()
                    oAlb.Client = New Client(oContact.Guid)
                    oAlb.SetUser(BLL.BLLSession.Current.User)

                    Dim exs As New List(Of Exception)
                    If oAlb.Update(exs) Then
                        RefreshRequest(sender, e)
                        MsgBox("albará canviat de client", MsgBoxStyle.Information, "MAT.NET")
                    Else
                        MsgBox("error. Albará no grabat" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
                    End If
                Else
                    MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            Else
                MsgBox("Operació no realitzada", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("aquest albará no es pot canviar perque ja está facturat", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult
        Select Case mAlbs.Count
            Case 1
                rc = MsgBox("Eliminem l'albará " & mAlbs(0).Id & " de " & mAlbs(0).Nom & "?", MsgBoxStyle.OkCancel, "M+O")
            Case Else
                rc = MsgBox("Eliminem " & mAlbs.ToString & "?", MsgBoxStyle.OkCancel, "M+O")
        End Select
        If rc = MsgBoxResult.Ok Then
            mAlbs.Delete()
            MsgBox("Albarans eliminats", MsgBoxStyle.Information, "M+O")
            RefreshRequest(Me, MatEventArgs.Empty)
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If

    End Sub



    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAlb As Alb = mAlbs(0)
        Dim sUrl As String = oAlb.Url(True)
        Clipboard.SetDataObject(sUrl, True)
        MsgBox("Copiat enllaç al portapapers:" & vbCrLf & "albará " & oAlb.Id & " de " & oAlb.Client.Clx)
    End Sub

    Private Sub Do_Recollida(sender As Object, e As System.EventArgs)
        Dim oAlb As Alb = mAlbs(0)
        Dim oRecollida As Recollida = Recollida.NewFromAlb(oAlb)
        Dim oFrm As New Frm_Alb_Recollida(oRecollida)
        oFrm.Show()
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
                Dim oPdf As New PdfAlb(mAlbs, BlProforma)
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
        root.ShowPdf(mAlbs.PdfStream(BlProforma, True))
    End Sub

    Private Sub SavePdf(ByVal BlProforma As Boolean)
        Dim sFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        Dim sFileName As String = "M+O " & mAlbs.ToString(BlProforma) & ".pdf"
        Dim sTit As String = IIf(BlProforma, "PROFORMES", "COMANDES")
        Dim oDlg As New Windows.Forms.SaveFileDialog
        With oDlg
            .Title = "GUARDAR " & sTit
            .FileName = sFileName
            .Filter = "acrobat reader(*.pdf)|*.pdf"
            .FilterIndex = 1
            .DefaultExt = "pdf"
            .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            If .ShowDialog() = DialogResult.OK Then
                mAlbs.Pdf(False, oDlg.FileName)
            End If
        End With

    End Sub

End Class
