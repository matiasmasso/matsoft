Public Class Frm_ConsumerTicket
    Private _ConsumerTicket As DTOConsumerTicket
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOConsumerTicket)
        MyBase.New()
        Me.InitializeComponent()
        _ConsumerTicket = value
    End Sub

    Private Async Sub Frm_ConsumerTicket_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.ConsumerTicket.Load(exs, _ConsumerTicket) Then
            With _ConsumerTicket
                TextBoxId.Text = IIf(.IsNew, "(nou ticket)", .Id)
                DateTimePicker1.Value = .Fch
                Xl_LookupMarketplace1.Marketplace = .MarketPlace
                TextBoxOrderId.Text = .OrderId
                TextBoxNom.Text = .Nom
                TextBoxCognom1.Text = .Cognom1
                TextBoxCognom2.Text = .Cognom2
                Xl_Address1.Load(.Address)
                TextBoxEmail.Text = .EmailAddress
                TextBoxTel.Text = .Tel
                Await Xl_DocFileTicket.Load(.PurchaseOrder.DocFile)
                Xl_LookupPurchaseOrder1.PurchaseOrder = .PurchaseOrder

                If .Delivery IsNot Nothing Then
                    If .Delivery.Invoice IsNot Nothing Then
                        TextBoxFra.Text = .Delivery.Invoice.NumeroYSerie()
                        DateTimePickerFchFra.Value = .Delivery.Invoice.Fch
                        Await Xl_DocFileFra.Load(.Delivery.Invoice.DocFile)
                    End If
                    Xl_LookupDelivery1.Delivery = .Delivery
                End If

                TextBoxFraNom.Text = .FraNom
                Xl_AddressFra.Load(.FraAddress)
                TextBoxNif.Text = .Nif

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew

                If .PurchaseOrder IsNot Nothing Then
                    Xl_ConsumerTicketItems1.Load(.PurchaseOrder.Items)
                    TextBoxTotals.Text = .PurchaseOrder.TotalsText()
                End If


                If .Delivery IsNot Nothing Then
                    If .Delivery.Transportista IsNot Nothing Then
                        TextBoxTrpNom.Text = .Delivery.Transportista.Nom
                    End If
                    TextBoxTracking.Text = .Delivery.Tracking
                    If .FchTrackingNotified <> Nothing Then
                        CheckBoxUsrTrackingNotified.Checked = True
                        TextBoxUsrTrackingNotified.Visible = True
                        TextBoxUsrTrackingNotified.Text = String.Format("Notificat per {0} el {1:dd/MM/yy} a les {1:hh:mm}", .UsrTrackingNotified.Nom, .FchTrackingNotified)
                    End If

                    If .FchDelivered <> Nothing Then
                        CheckBoxDelivered.Checked = True
                        DateTimePickerDelivered.Visible = True
                        TextBoxUsrDelivered.Visible = True
                        DateTimePickerDelivered.Value = .FchDelivered
                        TextBoxUsrDelivered.Text = String.Format("Registrat per {0}", .UsrDelivered.Nom)
                    End If

                    If .FchReviewRequest <> Nothing Then
                        CheckBoxReviewRequest.Checked = True
                        TextBoxUsrReviewRequest.Visible = True
                        TextBoxUsrReviewRequest.Text = String.Format("Sol.licitat per {0} el {1:dd/MM/yy} a les {1:hh:mm}", .UsrReviewRequest.Nom, .FchReviewRequest)
                    End If
                End If
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub



    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxCognom1.TextChanged,
          TextBoxCognom2.TextChanged,
           TextBoxEmail.TextChanged,
            TextBoxTel.TextChanged,
             TextBoxNif.TextChanged,
              TextBoxFraNom.TextChanged,
               Xl_Address1.AfterUpdate,
                Xl_AddressFra.AfterUpdate,
                 DateTimePickerDelivered.ValueChanged,
                  Xl_LookupMarketplace1.AfterUpdate,
                   TextBoxOrderId.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If TextBoxTel.Text.Length > 15 Then
            Dim ex As New Exception("El telefon no pot tenir mes de 15 xifres")
            UIHelper.WarnError(ex)
        Else
            With _ConsumerTicket
                .Fch = DateTimePicker1.Value
                .OrderId = TextBoxOrderId.Text
                .MarketPlace = Xl_LookupMarketplace1.Marketplace
                .Nom = TextBoxNom.Text
                .Cognom1 = TextBoxCognom1.Text
                .Cognom2 = TextBoxCognom2.Text
                .Address = Xl_Address1.Address
                .EmailAddress = TextBoxEmail.Text
                .Tel = TextBoxTel.Text
                .PurchaseOrder = Xl_LookupPurchaseOrder1.PurchaseOrder
                .Delivery = Xl_LookupDelivery1.Delivery
                .FraNom = TextBoxFraNom.Text
                .FraAddress = Xl_AddressFra.Address
                .Nif = TextBoxNif.Text
                .FchDelivered = IIf(CheckBoxDelivered.Checked, DateTimePickerDelivered.Value, Nothing)
            End With

            Dim DirtyFra = invoiceHasChanged()

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.ConsumerTicket.Update(exs, _ConsumerTicket) Then
                If DirtyFra Then
                    Await RedactaDeNouLaFactura()
                End If
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ConsumerTicket))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al desar")
            End If
        End If
    End Sub

    Private Function invoiceHasChanged() As Boolean
        Dim retval As Boolean = False
        If TextBoxFraNom.Text <> _ConsumerTicket.FraNom Then retval = True
        If Not (Xl_AddressFra.Address Is Nothing And _ConsumerTicket.Address Is Nothing) Then
            If Xl_AddressFra.Address Is Nothing Or _ConsumerTicket.Address Is Nothing Then
                retval = True
            ElseIf Xl_AddressFra.Address.UnEquals(_ConsumerTicket.Address) Then
                retval = True
            End If
        End If
        If TextBoxNif.Text <> _ConsumerTicket.Nif Then retval = True
        Return retval
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.ConsumerTicket.Delete(exs, _ConsumerTicket) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ConsumerTicket))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Sub TextBoxNif_Validated(sender As Object, e As EventArgs) Handles TextBoxNif.Validated
        Dim src = Replace(TextBoxNif.Text, "-", "")
        src = Replace(src, " ", "")
        TextBoxNif.Text = src
    End Sub

    Private Async Function RedactaDeNouLaFactura() As Task
        Dim exs As New List(Of Exception)
        Dim oInvoice As DTOInvoice = Nothing
        If _ConsumerTicket.Delivery IsNot Nothing AndAlso _ConsumerTicket.Delivery.Invoice IsNot Nothing Then
            oInvoice = _ConsumerTicket.Delivery.Invoice
            If FEB.Invoice.Load(oInvoice, exs) Then
                Dim oPdf As New LegacyHelper.PdfAlb(oInvoice)
                Dim oCert = Await FEB.Cert.Find(GlobalVariables.Emp.Org, exs)
                Dim oByteArray = oPdf.Stream(exs, oCert)
                Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oByteArray, MimeCods.Pdf)
                If exs.Count = 0 Then
                    Dim oCca As DTOCca = oInvoice.Cca
                    oCca.UsrLog.usrLastEdited = Current.Session.User
                    If FEB.Cca.Load(oCca, exs) Then
                        oCca.docFile = oDocFile
                        oCca.id = Await FEB.Cca.Update(exs, oCca)
                        If exs.Count = 0 Then
                            Await Xl_DocFileFra.Load(oDocFile)
                        Else
                            exs.Add(New Exception("error al regenerar pdf " & DTOInvoice.FullConcept(oInvoice)))
                        End If
                    End If
                End If
            End If
        End If
        If exs.Count = 0 Then
            If oInvoice IsNot Nothing AndAlso oInvoice.DocFile IsNot Nothing Then
                Await Xl_DocFileFra.Load(oInvoice.DocFile)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub RedactaDeNouLaFacturaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedactaDeNouLaFacturaToolStripMenuItem.Click
        Await RedactaDeNouLaFactura()
    End Sub

    Private Sub CheckBoxUsrTrackingNotified_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxUsrTrackingNotified.CheckedChanged
        If _AllowEvents Then
            If CheckBoxUsrTrackingNotified.Checked Then
                _ConsumerTicket.UsrTrackingNotified = Current.Session.User.ToGuidNom
                _ConsumerTicket.FchTrackingNotified = DTO.GlobalVariables.Now()
                TextBoxUsrTrackingNotified.Visible = True
                TextBoxUsrTrackingNotified.Text = String.Format("Notificat per {0} el {1:dd/MM/yy} a les {1:hh:mm}", _ConsumerTicket.UsrTrackingNotified.Nom, _ConsumerTicket.FchTrackingNotified)
            Else
                _ConsumerTicket.UsrTrackingNotified = Nothing
                _ConsumerTicket.FchTrackingNotified = Nothing
                TextBoxUsrTrackingNotified.Visible = False
            End If
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxDelivered_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDelivered.CheckedChanged
        If _AllowEvents Then
            If CheckBoxDelivered.Checked Then
                DateTimePickerDelivered.Visible = True
                TextBoxUsrDelivered.Visible = True
                TextBoxUsrDelivered.Text = String.Format("Registrat per {0}", Current.Session.User.ToGuidNom.Nom)
                _ConsumerTicket.UsrDelivered = Current.Session.User.ToGuidNom
            Else
                DateTimePickerDelivered.Visible = False
                TextBoxUsrDelivered.Visible = False
                TextBoxUsrDelivered.Text = ""
            End If
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxReviewRequest_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxReviewRequest.CheckedChanged
        If _AllowEvents Then
            If CheckBoxReviewRequest.Checked Then
                _ConsumerTicket.UsrReviewRequest = Current.Session.User.ToGuidNom
                _ConsumerTicket.FchReviewRequest = DTO.GlobalVariables.Now()
                TextBoxUsrReviewRequest.Visible = True
                TextBoxUsrReviewRequest.Text = String.Format("Sol.licitat per {0} el {1:dd/MM/yy} a les {1:hh:mm}", _ConsumerTicket.UsrReviewRequest.Nom, _ConsumerTicket.FchReviewRequest)
            Else
                TextBoxUsrReviewRequest.Visible = False
                TextBoxUsrReviewRequest.Text = ""
                _ConsumerTicket.UsrReviewRequest = Nothing
                _ConsumerTicket.FchReviewRequest = Nothing
            End If
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub AfegirPortsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfegirPortsToolStripMenuItem.Click
        Dim oFrm As New Frm_ConsumerTicketPorts(_ConsumerTicket)
        AddHandler oFrm.AfterUpdate, AddressOf onPortsAdded
        oFrm.Show()
    End Sub

    Private Sub onPortsAdded(sender As Object, e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_ConsumerTicket))
    End Sub

    Private Sub Xl_LookupDelivery1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupDelivery1.RequestToLookup
        Dim oFrm As New Frm_Deliveries({DTOPurchaseOrder.Codis.client}.ToList(), Defaults.SelectionModes.selection)
        AddHandler oFrm.onItemSelected, AddressOf DeliverySelected
        oFrm.Show()
    End Sub

    Private Sub DeliverySelected(sender As Object, e As MatEventArgs)
        Xl_LookupDelivery1.Delivery = e.Argument
        ButtonOk.Enabled = True
    End Sub
End Class


