Public Class Frm_AlbNew2
    Private _Delivery As DTODelivery = Nothing
    Private _Customer As DTOCustomer
    Private _Ccx As DTOCustomer
    Private _Proveidor As DTOProveidor

    Private _Obs As String = ""
    Private _CustomerDocUrl As String = ""
    Private _CashCod As DTOCustomer.CashCodes
    Private _DirtyItems As Boolean = False
    Private _WarnAlbs As Boolean = False
    Private _ExemptIva As Boolean = False
    Private _ShowTot As Boolean = False
    Private _Diposit As Boolean = False
    Private _AllowEvents As Boolean = False
    Private _TabLoaded(10) As Boolean

    Property _PaymentTerms As DTOPaymentTerms

    Private Enum Tabs
        linies
        docfiles
        etiquetesTransport
        bultos
    End Enum

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)


    Public Sub New(oImportacio As DTOImportacio)
        MyBase.New()
        Me.InitializeComponent()

        Dim exs As New List(Of Exception)
        FEB.Importacio.Load(exs, oImportacio)
        _Proveidor = oImportacio.Proveidor
        _Delivery = FEB.Delivery.Factory(exs, _Proveidor, Current.Session.User, GlobalVariables.Emp.Mgz)
        _Delivery.Importacio = oImportacio
    End Sub


    Public Sub New(ByVal oDelivery As DTODelivery)
        MyBase.New()
        Me.InitializeComponent()

        _Delivery = oDelivery
    End Sub

    Private Async Sub Frm_AlbNew2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Delivery.IsNew Then
            _Delivery.items = Await FEB.DeliveryItems.Factory(exs, _Delivery.contact, _Delivery.cod, GlobalVariables.Emp.Mgz)


            If exs.Count = 0 Then
                Dim oBundleItem = _Delivery.items.Where(Function(x) x.bundle.Count > 0).ToList
                If _Delivery.items.Count = 0 Then
                    UIHelper.WarnError("Aquest client no té comandes pendents")
                    Me.Close()
                    Exit Sub
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            If FEB.Delivery.Load(_Delivery, exs) Then
            Else
                UIHelper.WarnError(exs)
            End If
        End If

        Select Case _Delivery.Cod
            Case DTOPurchaseOrder.Codis.proveidor
                _Proveidor = _Delivery.Proveidor
                If Not FEB.Proveidor.Load(_Proveidor, exs) Then
                    UIHelper.WarnError(exs)
                End If
                TextBoxObsTransp.Visible = False
                LabelObsTransp.Visible = False
            Case Else
                _Customer = _Delivery.Customer
                _Ccx = FEB.Customer.CcxOrMe(exs, _Customer)
                _Customer.Ccx = _Ccx
                'FEB.Customer.Load(_Ccx, exs)
                If _Delivery.IsNew Then _Delivery.ObsTransp = _Customer.HorarioEntregas
        End Select

        With ImageList1
            .Images.Add(Tabs.etiquetesTransport.ToString, My.Resources.label16)
        End With

        Await SetTitle()
        ValidateCustomer()
        ' Await LoadTrps()
        Await refresca()
        If _Delivery.IsNew Then SetDirty()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        With _Delivery
            Xl_ContactMgz.Contact = .Mgz
            DateTimePickerFch.Value = .Fch
            TextBoxNom.Text = IIf(.Nom = "", .Contact.NomComercialOrDefault(), .Nom)
            If .Address IsNot Nothing Then
                'TextBoxAdr.Text = .Address.Text
                TextBoxAdr.Text = .address.SingleLineText
                Xl_LookupZip1.Load(.address.Zip)
            End If
            TextBoxTel.Text = .Tel
            If (.Customer IsNot Nothing) Then
                TextBoxObsTransp.Text = .ObsTransp
            End If
            'SetTrp(.Transportista)
            CheckBoxValorat.Checked = .Valorado
            CheckBoxFacturable.Checked = .Facturable
            SetTransmisio()
            _Obs = .Obs
            _PaymentTerms = .PaymentTerms
            DisplayStatusObs()
            ButtonDel.Enabled = Not .IsNew

            Dim oIncoterms = Await FEB.Incoterms.All(exs)
            If exs.Count = 0 Then
                Xl_LookupIncoterm1.Load(oIncoterms)
            End If

            Select Case .Cod
                Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                    If Await FEB.Contact.IsImpagat(_Ccx, exs) Then
                        TextBoxNom.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                    End If
                    Xl_LookupIncoterm1.Value = .Customer.Incoterm
                Case DTOPurchaseOrder.Codis.proveidor
                    'ComboBoxTrp.Visible = False
                    Xl_LookupIncoterm1.Value = .Proveidor.IncoTerm
                    ComboBoxPorts.Visible = False
                    CheckBoxValorat.Visible = False
                    CheckBoxFacturable.Visible = False
                    LabelLastAlb.Visible = False

                    ToolStripButtonCustomDoc.Visible = False
                    ClientToolStripMenuItem.Text = "Procedencia"
            End Select

            If .IsNew Then
                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                        If _Delivery.CashCod = DTOCustomer.CashCodes.TransferenciaPrevia Or _Delivery.CashCod = DTOCustomer.CashCodes.Visa Then
                            'ComboBoxPorts.SelectedIndex = DTOCustomer.PortsCodes.Altres
                            ToolStripButtonFra.Visible = False
                        Else
                            If _Delivery.CashCod = DTOCustomer.CashCodes.credit Then
                                If Not Await FEB.Customer.EFrasEnabled(exs, _Delivery.Customer) Then
                                    Me.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                                    MsgBox("No té cap email habilitat per rebre les factures!")
                                End If
                            End If

                        End If
                    Case Else
                        ToolStripButtonFra.Visible = False
                End Select

                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Traspas
                        If _Customer.WarnAlbs > "" Or _Ccx.WarnAlbs > "" Then
                            Dim sMsg As New Text.StringBuilder
                            If _Customer.WarnAlbs > "" Then sMsg.AppendLine(_Customer.WarnAlbs)
                            If _Ccx.WarnAlbs > "" And _Ccx.WarnAlbs <> _Customer.WarnAlbs Then
                                sMsg.AppendLine(_Ccx.WarnAlbs)
                            End If
                            UIHelper.WarnError(sMsg.ToString())
                            _WarnAlbs = True
                        End If

                        If _Ccx.PrimaryNifValue().Trim = "" Then
                            UIHelper.WarnError("Falta NIF!")
                        End If
                End Select

                CheckBoxRecycle.Visible = True
                CheckBoxRecycle.Enabled = Await LoadRecicleNums()
                Await SetEmailLabel()
                ToolStripButtonFra.Visible = False
                Await SetLastAlb()
                Select Case _Delivery.Cod
                    Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                        If _CashCod = DTOCustomer.CashCodes.credit Then SetCreditDetails()
                End Select


                Dim oExportCod = DTOInvoice.ExportCods.nacional
                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.proveidor
                        oExportCod = DTOAddress.ExportCod(.Proveidor.Address)
                    Case Else
                        If .Platform Is Nothing Then
                            oExportCod = DTOAddress.ExportCod(.Address)
                        Else
                            If .Customer.Nifs IsNot Nothing AndAlso .Customer.Nifs.Count > 0 AndAlso .Customer.Nifs.First.Cod = DTONif.Cods.CR Then
                                oExportCod = DTOInvoice.ExportCods.nacional 'cas de Bebitus
                            Else
                                oExportCod = DTOAddress.ExportCod(.Customer.Address) 'cas de Zoalfer
                            End If
                        End If
                End Select

                Select Case oExportCod
                    Case DTOInvoice.ExportCods.notSet
                        CheckBoxExport.CheckState = CheckState.Indeterminate
                        CheckBoxCEE.Visible = False
                    Case DTOInvoice.ExportCods.nacional
                        Select Case _Delivery.PortsCod
                            Case DTOCustomer.PortsCodes.pagats
                                Xl_LookupIncoterm1.Value = DTOIncoterm.Factory("DAP")
                            Case DTOCustomer.PortsCodes.deguts
                                Xl_LookupIncoterm1.Value = DTOIncoterm.Factory("EXW")
                        End Select
                    Case DTOInvoice.ExportCods.intracomunitari
                        CheckBoxExport.Checked = True
                        CheckBoxCEE.Checked = True
                    Case DTOInvoice.ExportCods.extracomunitari
                        CheckBoxExport.Checked = True
                End Select

            Else
                If (_Delivery.Pallets.Count + _Delivery.Packages.Count) > 0 Then
                    Xl_Tree_DeliveryPackages1.Load(_Delivery)
                Else
                    TabControl1.TabPages.Remove(TabControl1.TabPages(Tabs.bultos))
                End If
                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                        If _PaymentTerms Is Nothing Then
                            ToolStripButtonFpg.Image = My.Resources.UnChecked13
                        Else
                            ToolStripButtonFpg.Image = My.Resources.Checked13
                        End If

                        If .Invoice Is Nothing Then
                            ToolStripButtonFra.Text = "pendent de facturar"
                        Else
                            ToolStripButtonFra.Text = DTOInvoice.CompactConcept(.Invoice, _Customer.Lang)
                        End If
                    Case DTOPurchaseOrder.Codis.Traspas
                        ToolStripButtonFra.Visible = False
                        _Diposit = True
                    Case Else
                        ToolStripButtonFra.Visible = False
                End Select

                LabelLastAlb.Visible = False
                LabelEmail.Visible = False

                'LoadTrps()
                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                        '      SetTrp(.Transportista)
                End Select

                If .EtiquetesTransport IsNot Nothing Then
                    TabControl1.TabPages(Tabs.etiquetesTransport).ImageKey = Tabs.etiquetesTransport.ToString
                    Xl_DocFileEtiquetesTransport.Load(.EtiquetesTransport)
                End If

                Select Case .ExportCod
                    Case DTOInvoice.ExportCods.notSet
                        CheckBoxExport.ForeColor = Me.BackColor
                        CheckBoxExport.BackColor = Color.Red
                        CheckBoxExport.CheckState = CheckState.Indeterminate
                        CheckBoxCEE.Visible = False
                    Case DTOInvoice.ExportCods.intracomunitari
                        CheckBoxExport.Checked = True
                        CheckBoxCEE.Visible = True
                        CheckBoxCEE.Checked = True
                    Case DTOInvoice.ExportCods.extracomunitari
                        CheckBoxExport.Checked = True
                        CheckBoxCEE.Visible = True
                        CheckBoxCEE.Checked = False
                End Select

                Xl_LookupIncoterm1.Value = .Incoterm
            End If



            If .Platform IsNot Nothing Then
                Await Xl_Contact2Platform.Load(exs, .Platform)
                Xl_Contact2Platform.Visible = True
                CheckBoxPlatform.Checked = True
            End If

            If .FacturarA IsNot Nothing Then
                CheckBoxFacturarA.Checked = True
                Await Xl_Contact2FacturarA.Load(exs, .FacturarA)
                Xl_Contact2FacturarA.Visible = True
                CheckBoxPlatform.Checked = True
            End If

            _CustomerDocUrl = .CustomerDocURL
            _ExemptIva = .IvaExempt
        End With

        ToolStripButtonDiposit.Image = IIf(_Diposit, My.Resources.Checked13, My.Resources.UnChecked13)

        Await Xl_DeliveryItems1.Load(_Delivery)
        SetKg()
        If SetTotals(exs) Then
            SetFormaDePago()
            Xl_UsrLog1.Load(_Delivery.UsrLog)
            ShowCredit()

            SetPorts()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

        Dim oContactMenu = Await FEB.ContactMenu.Find(exs, _Delivery.Contact.Guid)
        Dim oMenuClient As New Menu_Contact(_Delivery.Contact, oContactMenu)
        ClientToolStripMenuItem.DropDownItems.AddRange(oMenuClient.Range)

        Dim oMenuDelivery = New Menu_Delivery({_Delivery}.ToList())
        Dim arxiu As ToolStripMenuItem = MenuStrip1.Items(0)
        For Each item In oMenuDelivery.Range
            If item.Text <> "Zoom" Then
                arxiu.DropDownItems.Add(item)
            End If
        Next

        Application.DoEvents()
    End Function

    Private Sub SetTransmisio()
        With _Delivery
            If .IsNew Then
                ToolStripButtonTransmisio.Visible = False
            Else
                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.Client
                        If .Transmisio IsNot Nothing Then
                            ToolStripButtonTransmisio.Text = "transmisió " & .Transmisio.Id
                        Else
                            Select Case .RetencioCod
                                Case DTODelivery.RetencioCods.Free
                                    ToolStripButtonTransmisio.Text = "pendent de transmetre"
                                Case DTODelivery.RetencioCods.Transferencia
                                    ToolStripButtonTransmisio.Image = My.Resources.SandClock
                                    ToolStripButtonTransmisio.Text = "pendent de transferencia"
                                Case DTODelivery.RetencioCods.VISA
                                    ToolStripButtonTransmisio.Image = My.Resources.SandClock
                                    ToolStripButtonTransmisio.Text = "pendent de VISA"
                            End Select
                        End If
                    Case Else
                        ToolStripButtonTransmisio.Visible = False
                End Select
            End If
        End With
    End Sub

    Private Sub SetPorts()
        'Dim oPortsCod As DTOCustomer.PortsCodes = _Delivery.PortsCod
        Dim oPortsCod As DTOCustomer.PortsCodes = _Delivery.PortsCod
        If _Delivery.IsNew And _Delivery.Customer IsNot Nothing Then
            Select Case _Delivery.Customer.PortsCondicions.Cod
                Case DTOPortsCondicio.Cods.portsPagats, DTOPortsCondicio.Cods.carrecEnFactura
                    oPortsCod = DTOCustomer.PortsCodes.pagats
                Case DTOPortsCondicio.Cods.portsDeguts
                    oPortsCod = DTOCustomer.PortsCodes.deguts
                Case DTOPortsCondicio.Cods.reculliran
                    oPortsCod = DTOCustomer.PortsCodes.reculliran
            End Select
        End If
        If oPortsCod > ComboBoxPorts.Items.Count - 1 Then
            ComboBoxPorts.SelectedIndex = DTOCustomer.PortsCodes.altres
            MsgBox("codi ports " & oPortsCod & " no existent. Canviat a altres", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            ComboBoxPorts.SelectedIndex = oPortsCod
            SetPortsVisibility()
            If _Delivery.IsNew And oPortsCod = DTOCustomer.PortsCodes.Reculliran Then
                SetReculliran()
            End If
        End If
    End Sub

    Private Sub ValidateCustomer()
        If _Delivery.Cod = DTOPurchaseOrder.Codis.client And _Delivery.IsNew Then
            Dim oCustomer As DTOCustomer = _Delivery.Customer
            Dim exs As New List(Of Exception)
            If Not FEB.Customer.ValidateCustomer(oCustomer, exs) Then
                UIHelper.WarnError(exs, "Fitxa de client incomplerta")
                Me.Close()
            End If
        End If
    End Sub

    Private Async Function SetEmailLabel() As Task
        Dim exs As New List(Of Exception)
        Dim oRecipients = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.ConfirmacioEnviament, _Delivery.contact)
        If oRecipients.Count = 0 Then
            Dim oUsers = Await FEB.Users.All(exs, _Delivery.Contact)
            If exs.Count = 0 Then
                oUsers = oUsers.Where(Function(x) x.BadMail Is Nothing).ToList
                If oUsers.Count > 0 Then
                    LabelEmail.Text = oUsers.First.EmailAddress
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            LabelEmail.Text = oRecipients.First
        End If
    End Function

    Private Sub SetReculliran()
        'SetTrp(Nothing)
        TextBoxAdr.Text = _Customer.Lang.Tradueix("(recogerán en almacén)", "(reculliràn al magatzem)", "(pick up at warehouse)")
        Dim oMgz = Xl_ContactMgz.Mgz
        If oMgz.Address Is Nothing Then
            Dim exs As New List(Of Exception)
            If FEB.Contact.Load(oMgz, exs) Then
                Xl_LookupZip1.Load(Xl_ContactMgz.Mgz.Address.Zip)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            Xl_LookupZip1.Load(Xl_ContactMgz.Mgz.Address.Zip)
        End If
    End Sub

    Private Async Function SetTitle() As Task
        Dim exs As New List(Of Exception)
        Dim oCod As DTOPurchaseOrder.Codis = _Delivery.Cod
        Dim iNumeroDocument As Integer = _Delivery.Id
        Dim oContact = _Delivery.Contact
        Dim oImportacio As DTOImportacio = _Delivery.Importacio
        Dim s As String = ""

        If iNumeroDocument = 0 Then
            Select Case oCod
                Case DTOPurchaseOrder.Codis.Client
                    s = "NOU ALBARA DE " & oContact.FullNom
                Case DTOPurchaseOrder.Codis.Proveidor
                    s = "ENTRADA MERCANCIA DE " & oContact.FullNom
                    If oImportacio IsNot Nothing Then
                        s = s & " (" & oImportacio.FormattedId() & ") "
                    End If
                Case DTOPurchaseOrder.Codis.Reparacio
                    s = "REPARACIO SERVEI TECNIC " & oContact.FullNom
                Case DTOPurchaseOrder.Codis.Traspas
                    s = "NOTA DE TRASPAS DE MAGATZEM"
            End Select
        Else
            Select Case oCod
                Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                    s = "ALBARA " & iNumeroDocument & " DE " & oContact.FullNom
                Case DTOPurchaseOrder.Codis.Proveidor
                    s = "ENTRADA " & iNumeroDocument & " "
                    If oImportacio Is Nothing Then
                        oImportacio = Await FEB.Importacio.FromDelivery(exs, _Delivery)
                        If exs.Count > 0 Then
                            UIHelper.WarnError(exs)
                        End If
                    End If
                    If oImportacio IsNot Nothing Then
                        s = s & "(" & oImportacio.FormattedId() & ") "
                    End If

                    s = s & "DE " & oContact.FullNom
                Case DTOPurchaseOrder.Codis.Traspas
                    s = "NOTA " & iNumeroDocument & " DE TRASPAS DE MAGATZEM"
            End Select
        End If

        Me.Text = s
    End Function



    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        If _Delivery.IsNew Then
            If CheckBoxRecycle.Checked Then
                Dim RecycleId As Integer = Await RecycleNum(exs)
                If exs.Count = 0 Then
                    _Delivery.id = RecycleId
                Else
                    UIHelper.WarnError(exs)
                    Exit Sub
                End If
            End If
        End If

        LoadFromForm()
        LoadTransportFromForm()

        If Await FEB.Delivery.CheckValidationErrors(exs, GlobalVariables.Emp, _Delivery) Then
            Save(exs)
        Else
            Dim rc = MsgBox(ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.AbortRetryIgnore)
            If rc <> MsgBoxResult.Abort Then
                exs = New List(Of Exception)
                Save(exs)
            End If
        End If

    End Sub


    Private Sub LoadFromForm()
        With _Delivery
            Dim oSuma As DTOAmt = DTODeliveryItem.BaseImponible(Xl_DeliveryItems1.Items)
            If .IsNew Then
                'retenció per forma de pagament
                If oSuma.IsPositive Then
                    Select Case _CashCod
                        Case DTOCustomer.CashCodes.TransferenciaPrevia
                            .RetencioCod = DTODelivery.RetencioCods.Transferencia
                        Case DTOCustomer.CashCodes.Visa
                            .RetencioCod = DTODelivery.RetencioCods.VISA
                    End Select
                End If
            End If

            If oSuma.IsPositive Then
                Select Case _CashCod
                    Case DTOCustomer.CashCodes.TransferenciaPrevia, DTOCustomer.CashCodes.Visa
                    Case Else
                        .RetencioCod = DTODelivery.RetencioCods.Free
                End Select
            End If

            .Fch = DateTimePickerFch.Value
            .Nom = TextBoxNom.Text
            .Address = New DTOAddress()
            With .Address
                .Text = TextBoxAdr.Text
                .Zip = Xl_LookupZip1.Zip
            End With
            .Tel = TextBoxTel.Text
            .ExportCod = GetExportCod()
            .Incoterm = Xl_LookupIncoterm1.Value


            If _Diposit Then
                .Cod = DTOPurchaseOrder.Codis.Traspas
            Else
                .Facturable = CheckBoxFacturable.Checked
            End If

            .IvaExempt = _ExemptIva
            .Items = Xl_DeliveryItems1.Items
            .Import = .totalCash()
            .Valorado = CheckBoxValorat.Checked

            .Obs = ToolStripStatusLabelObs.Text
            .ObsTransp = TextBoxObsTransp.Text
            .Mgz = Xl_ContactMgz.Mgz
            .PortsCod = CurrentPorts()

            .PaymentTerms = _PaymentTerms

            If .IvaExempt Then
                '.IvaBaseQuotas = New MaxiSrvr.IvaBaseQuotas =========================================== TODO: canviar a DTO
            End If

            .CustomerDocURL = _CustomerDocUrl
            .CashCod = _CashCod

            If .Cod = DTOPurchaseOrder.Codis.client Or .Cod = DTOPurchaseOrder.Codis.proveidor Then
                If .Items.Select(Function(x) x.PurchaseOrderItem.PurchaseOrder).Distinct.Count = 1 Then
                    .EtiquetesTransport = .Items.First.PurchaseOrderItem.PurchaseOrder.EtiquetesTransport
                Else
                    .EtiquetesTransport = Nothing
                End If
            End If
            .UsrLog.FchLastEdited = DTO.GlobalVariables.Now()

            If CheckBoxFacturarA.Checked Then
                .FacturarA = .Customer
            Else
                .FacturarA = Xl_Contact2FacturarA.Customer
            End If

        End With
    End Sub

    Private Sub LoadTransportFromForm()

        With _Delivery
            .PortsCod = CurrentPorts()
            If CheckBoxPlatform.Checked And Xl_Contact2Platform.Contact IsNot Nothing Then
                .Platform = DTOCustomerPlatform.FromContact(Xl_Contact2Platform.Contact)
            Else
                .Platform = Nothing
            End If


            ' Select Case .Cod
            'Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.reparacio
            'If .Transportista Is Nothing Then
            ' Dim DcM3 As Decimal '= .Itms.M3 =========================================== TODO: canviar a DTO
            ' Dim oTrpCost As DTOTrpCost = FEB.Delivery.SuggestedTransport(Xl_LookupZip1.Zip, CurrentPorts, DcM3)
            ' If oTrpCost IsNot Nothing Then
            '.Transportista = oTrpCost.Parent.Transportista
            'End If
            'Else
            '.Transportista = ComboBoxTrp.SelectedItem
            'End If
            'End Select
        End With
    End Sub

    Private Async Sub Save(exs As List(Of Exception))
        Dim BlWasNew As Boolean = _Delivery.IsNew
        _Delivery.UsrLog.UsrLastEdited = Current.Session.User.ToGuidNom
        If BlWasNew And _Delivery.Importacio IsNot Nothing Then
            Await FEB.Importacio.Entrada(exs, _Delivery)
        Else
            Dim id = Await FEB.Delivery.Update(exs, _Delivery)
            If exs.Count = 0 Then
                _Delivery.Id = id
            Else
                UIHelper.WarnError(exs)
            End If
        End If


        If exs.Count = 0 Then

            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Delivery))

            If BlWasNew Then
                Select Case DirectCast(ComboBoxPorts.SelectedIndex, DTOCustomer.PortsCodes)
                    Case DTOCustomer.PortsCodes.Altres
                                'MailHelper.SendMail("victoria@matiasmasso.es", , , "ALBARA " & .Id & " EN ALTRES (" & .Nom & ")")
                    Case DTOCustomer.PortsCodes.Reculliran
                        'MailHelper.SendMail("victoria@matiasmasso.es", , , "ALBARA " & .Id & " EN RECULLIRAN (" & .Nom & ")")
                End Select

                'If CheckBoxEalbs.Checked Then
                ' mAlb.MailToSubscriptors()
                'End If


                Dim s As String = "albará nº " & _Delivery.Id
                If _Delivery.Cod = DTOPurchaseOrder.Codis.Client Then
                    Select Case _Delivery.PortsCod
                        Case DTOCustomer.PortsCodes.Reculliran
                            s = s & vbCrLf & "recullirán"
                        Case DTOCustomer.PortsCodes.Altres
                            s = s & " fora de transmisió"
                    End Select
                End If

                MsgBox(s, MsgBoxStyle.Information, "MAT.NET")
            End If

            Me.Close()
        Else
            If BlWasNew Then
                UIHelper.WarnError(exs, "error al desar el nou albarà:")
            Else
                UIHelper.WarnError(exs, "error al desar l'albarà " & _Delivery.Id & ":")
            End If
        End If

    End Sub



    Private Async Function RecycleNum(exs As List(Of Exception)) As Task(Of Integer)
        Dim retval As Integer = 0
        If CheckBoxRecycle.Checked And IsNumeric(ComboBoxNum.Text) Then
            Dim oDelivery As DTODelivery = Await FEB.Delivery.FromNum(Current.Session.Emp, DateTimePickerFch.Value.Year, ComboBoxNum.Text, exs)
            If exs.Count = 0 Then
                If oDelivery Is Nothing Then
                    retval = ComboBoxNum.Text
                Else
                    Dim msg = String.Format("el número {0} ja ha estat assignat a {1}." & vbCrLf & "Torna a seleccionar un altre", ComboBoxNum.Text, oDelivery.nom)
                    exs.Add(New Exception(msg))
                    If Await LoadRecicleNums() Then
                        ComboBoxNum.Visible = True
                    Else
                        CheckBoxRecycle.Checked = False
                        CheckBoxRecycle.Enabled = False
                        ComboBoxNum.Visible = False
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Private Sub Xl_DeliveryItems1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_DeliveryItems1.AfterUpdate
        Dim exs As New List(Of Exception)
        _DirtyItems = True
        SetDirty()
        SetKg()
        LoadFromForm()
        If SetTotals(exs) Then
            Select Case _Delivery.Cod
                Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                    If _CashCod = DTOCustomer.CashCodes.credit Then SetCreditDetails()
                    'SetTrp(Nothing)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Function GetExportCod() As DTOInvoice.ExportCods
        Dim retval = DTOInvoice.ExportCods.nacional
        If CheckBoxExport.Checked Then
            If CheckBoxCEE.Checked Then
                retval = DTOInvoice.ExportCods.intracomunitari
            Else
                retval = DTOInvoice.ExportCods.extracomunitari
            End If
        End If
        Return retval
    End Function

    Private Function SetTotals(exs As List(Of Exception)) As Boolean
        Dim oPaymentTerms As DTOPaymentTerms
        Dim totalsText = FEB.Delivery.TotalsText(_Delivery, Xl_DeliveryItems1.Items, GetExportCod(), _Ccx, _Delivery.Contact.Lang)
        If _Delivery.BaseImponible.IsZero Then
            TextBoxTotals.Text = totalsText
        Else
            Select Case _Delivery.Cod
                Case DTOPurchaseOrder.Codis.Proveidor
                    oPaymentTerms = _Delivery.Proveidor.paymentTerms
                    TextBoxTotals.Text = String.Format("{0}   {1}", totalsText, FEB.PaymentTerms.Text(oPaymentTerms, _Delivery.Contact.Lang))
                Case Else
                    oPaymentTerms = _Ccx.PaymentTerms
                    TextBoxTotals.Text = String.Format("{0}   {1}", totalsText, FEB.PaymentTerms.Text(oPaymentTerms, _Delivery.Contact.Lang))
            End Select
        End If
        Return exs.Count = 0
    End Function



    Private Sub SetDirty()
        Dim AllowSave As Boolean = True
        If _Delivery.IsNew Then
            If Xl_DeliveryItems1.Items.Count = 0 Then AllowSave = False
        End If
        ButtonOk.Enabled = AllowSave
    End Sub



    Private Sub DateTimePickerFch_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePickerFch.ValueChanged
        If _AllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub ToolStripButtonObs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonObs.Click
        Dim s As String = InputBox("Observacions: ", Me.Text, _Obs)
        If s <> _Obs Then
            _Obs = s
            DisplayStatusObs()
            SetDirty()
        End If
    End Sub

    Private Sub ToolStripButtonCustomDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonCustomDoc.Click
        Dim s As String = InputBox("adreça web:", "DOCUMENT PER EL CONSUMIDOR", _CustomerDocUrl)
        Select Case s
            Case ""
                'ha clicat CANCEL 
            Case _CustomerDocUrl
                'ha clicat ACCEPTAR sense canviar res
            Case Else
                _CustomerDocUrl = s
                DisplayStatusObs()
                SetDirty()
        End Select
    End Sub

    Private Sub DisplayStatusObs()
        ToolStripStatusLabelObs.Text = _Obs
        ToolStripStatusLabelObs.Visible = (_Obs.isNotEmpty())
        ToolStripStatusLabelCustDoc.Text = _CustomerDocUrl
        ToolStripStatusLabelCustDoc.Visible = _CustomerDocUrl > ""
        StatusStripObs.Visible = (_Obs > "" Or _CustomerDocUrl.isNotEmpty())
    End Sub

    Private Async Function LoadRecicleNums() As Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Dim DtFch As Date = DateTimePickerFch.Value
        Dim iNums = Await FEB.Deliveries.NumsToRecycle(exs, Current.Session.Emp, DtFch)
        If exs.Count = 0 Then
            ComboBoxNum.DataSource = iNums
            If iNums.Count > 0 Then
                retval = True
                ComboBoxNum.SelectedIndex = 0
            End If
        Else
            UIHelper.WarnError(exs)
        End If

        Return retval
    End Function


    Private Async Function SetLastAlb() As Task
        Dim exs As New List(Of Exception)
        Dim oDelivery = Await FEB.Deliveries.Last(exs, _Delivery.Contact)
        If exs.count = 0 Then
            If oDelivery Is Nothing Then
                With LabelLastAlb
                    .Text = "primer albará"
                    .BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                End With
            Else
                Dim DtFch As Date = oDelivery.Fch
                With LabelLastAlb
                    .Text = "ultim albará del " & DtFch.ToShortDateString
                    If DtFch.ToShortDateString = DateTimePickerFch.Value.ToShortDateString Then
                        .BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                    End If
                End With
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub CheckWarning()
        'warn(true) o false segons validacio
    End Sub

    Private Sub Warn(ByVal BlWarn As Boolean)
        If BlWarn Then
            ButtonOk.TextAlign = ContentAlignment.MiddleRight
            ButtonOk.ImageIndex = 0
        Else
            ButtonOk.TextAlign = ContentAlignment.MiddleCenter
            ButtonOk.ImageIndex = -1
        End If
    End Sub

    Private Async Sub LabelLastAlb_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelLastAlb.DoubleClick
        Dim exs As New List(Of Exception)
        Dim oDelivery = Await FEB.Deliveries.Last(exs, _Delivery.Contact)
        If exs.Count = 0 Then
            If oDelivery IsNot Nothing Then

                Dim oContact As DTOContact = _Delivery.contact
                If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oContact, DTOAlbBloqueig.Codis.ALB, exs) Then
                    Dim oFrm As New Frm_AlbNew2(oDelivery)
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If

            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Function CurrentPorts() As DTOCustomer.PortsCodes
        Return DirectCast(ComboBoxPorts.SelectedIndex, DTOCustomer.PortsCodes)
    End Function

    Private Sub ComboBoxPorts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPorts.SelectedIndexChanged
        If _AllowEvents Then
            SetPortsVisibility()
            CheckWarning()
            SetDirty()
        End If
    End Sub

    Private Sub SetPortsVisibility()
        Static oLastCod As DTOCustomer.PortsCodes
        Dim oNewCod As DTOCustomer.PortsCodes = CurrentPorts()
        Select Case CurrentPorts()
            Case DTOCustomer.PortsCodes.altres
                'SetTrp(Nothing)
                'ComboBoxTrp.Visible = False
                'ComboBoxTrp.SelectedIndex = 0
            Case DTOCustomer.PortsCodes.Reculliran
                If _Delivery.IsNew Then
                    SetReculliran()
                    ' ComboBoxTrp.Visible = False
                    'ComboBoxTrp.SelectedIndex = 0
                End If
            Case DTOCustomer.PortsCodes.entregatEnMa
                'ComboBoxTrp.Visible = False
                'ComboBoxTrp.SelectedIndex = 0
            Case Else
                'ComboBoxTrp.Visible = True
                'If oLastCod = DTOCustomer.PortsCodes.Reculliran Then
                'restaura adreça d'entrega

                If _Delivery.Address IsNot Nothing Then
                    TextBoxAdr.Text = _Delivery.address.SingleLineText
                    Xl_LookupZip1.Load(_Delivery.Address.Zip)
                End If
                'TextBoxTel.Text = mAlb.Tel
                'End If
                If _Delivery.IsNew And _Delivery.Transportista Is Nothing Then
                    'ComboBoxTrp.SelectedItem = DirectCast(ComboBoxTrp.DataSource, List(Of DTOTransportista)).FirstOrDefault(Function(x) x.Equals(DTOTransportista.Wellknown(DTOTransportista.Wellknowns.Tnt)))
                End If
        End Select
        oLastCod = oNewCod
    End Sub


    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem l'albará " & _Delivery.Id.ToString & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Delivery.Delete(exs, _Delivery) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "Aquest albará no es pot eliminar:")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Frm_PurchaseOrder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim exs As New List(Of Exception)
        If Not FEB.AlbBloqueig.BloqueigEnd(Current.Session.User, _Delivery.Contact, DTOAlbBloqueig.Codis.ALB, exs) Then
            e.Cancel = True
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ToolStripButtonTransmisio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonTransmisio.Click
        If _Delivery.Transmisio Is Nothing Then
            Dim oFrmNew As New Frm_TransmisioNew()
            oFrmNew.Show()
        Else
            Dim oTransmisio = _Delivery.Transmisio
            Dim oFrm As New Frm_Transmisio(oTransmisio)
            oFrm.Show()
        End If
    End Sub

    Private Async Sub ToolStripButtonFra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFra.Click
        If _Delivery.Invoice Is Nothing Then
            Dim oDeliveries As New List(Of DTODelivery)
            oDeliveries.Add(_Delivery)
            Dim exs As New List(Of Exception)
            Dim oInvoice = Await FEB.Invoice.Factory(exs, Current.Session.Emp, oDeliveries)
            Dim oFrm As New Frm_Invoice(oInvoice)
            oFrm.Show()
        Else
            Dim oFrm As New Frm_Invoice(_Delivery.Invoice)
            oFrm.Show()
        End If
    End Sub


#Region "Toolbar"

    Private Sub SetKg()
        Dim oItems = Xl_DeliveryItems1.Items
        Dim iBultos As Integer = _Delivery.Bultos

        Dim sb As New System.Text.StringBuilder
        If iBultos > 0 Then sb.Append(iBultos.ToString & " bts ")
        Dim iKg As Integer = DTODeliveryItem.WeightKg(oItems)
        If iKg > 0 Then sb.Append(iKg & " Kg ")
        Dim m3 As Decimal = DTODeliveryItem.VolumeM3(oItems)
        If m3 > 0 Then sb.Append(Math.Round(m3, 3) & " m3 ")

        ToolStripLabelKg.Text = sb.ToString
    End Sub

    Private Sub ShowCredit()
        Select Case _Delivery.Cod
            Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                SetCredit(_Delivery.CashCod)
            Case Else
                ToolStripSplitButtonCredit.Visible = False
        End Select
    End Sub

    Private Sub SetCredit(ByVal oCashCod As DTOCustomer.CashCodes)
        _CashCod = oCashCod
        Select Case _CashCod
            Case DTOCustomer.CashCodes.credit
                SetCreditDetails()
            Case DTOCustomer.CashCodes.TransferenciaPrevia
                ToolStripSplitButtonCredit.Text = "transf.previa"
                ToolStripSplitButtonCredit.Image = My.Resources.cash
                If _Delivery.IsNew Then ButtonOk.Image = My.Resources.cash
                ToolStripButtonFpg.Visible = False
            Case DTOCustomer.CashCodes.Visa
                ToolStripSplitButtonCredit.Text = "VISA"
                ToolStripSplitButtonCredit.Image = My.Resources.cash
                If _Delivery.IsNew Then ButtonOk.Image = My.Resources.cash
                ToolStripButtonFpg.Visible = False
            Case DTOCustomer.CashCodes.Reembols
                ToolStripSplitButtonCredit.Text = "reembols"
                ToolStripSplitButtonCredit.Image = My.Resources.reembols
                If _Delivery.IsNew Then ButtonOk.Image = My.Resources.reembols
                ToolStripButtonFpg.Visible = False
        End Select
    End Sub



    Private Async Sub SetCreditDetails()
        Dim exs As New List(Of Exception)
        Dim BlShowCreditAvailable As Boolean = False
        If _Delivery.IsNew Then
            Select Case _Delivery.Cod
                Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                    BlShowCreditAvailable = True
            End Select
        End If

        If BlShowCreditAvailable Then

            Dim oTotal As DTOAmt = FEB.Delivery.TotalLiquid(exs, _Delivery)
            Dim oCreditDisponible = Await FEB.Risc.CreditDisponible(_Ccx, exs)
            If exs.Count = 0 Then
            Else
                UIHelper.WarnError(exs)
            End If
            oCreditDisponible = oCreditDisponible.Substract(oTotal)
            ToolStripSplitButtonCredit.Text = "credit " & DTOAmt.CurFormatted(oCreditDisponible)
            If oCreditDisponible.IsNegative Then
                ToolStripSplitButtonCredit.Image = My.Resources.warn
                'ButtonOk.Image = My.Resources.warn
                ButtonOk.BackColor = LegacyHelper.Defaults.COLOR_NOTOK
                ToolStripSplitButtonCredit.ForeColor = Color.Red
                ToolStripSplitButtonCredit.Font = New Font(ToolStripSplitButtonCredit.Font, FontStyle.Bold)
            Else
                'ButtonOk.Image = Nothing
                ButtonOk.BackColor = ButtonCancel.BackColor
                ToolStripSplitButtonCredit.Image = My.Resources.credit
                ToolStripSplitButtonCredit.ForeColor = Color.Navy
                ToolStripSplitButtonCredit.Font = New Font(ToolStripSplitButtonCredit.Font, FontStyle.Regular)
            End If

            ToolStripButtonFpg.Visible = True
        Else
            ToolStripSplitButtonCredit.Text = "credit"
            ToolStripSplitButtonCredit.Image = My.Resources.credit
            ToolStripButtonFpg.Visible = True
        End If
    End Sub

    Private Sub SetFormaDePago()
        Select Case _Delivery.Cod
            Case DTOPurchaseOrder.Codis.Client, DTOPurchaseOrder.Codis.Reparacio
                Select Case _Delivery.CashCod
                    Case DTOCustomer.CashCodes.credit
                        SetCreditDetails()
                        ToolStripButtonFpg.Visible = True

                        If _Delivery.IsNew Then

                            Dim sWarnFpgs As String = ""
                            Dim oFpgs As New List(Of DTOPaymentTerms)
                            If CheckComandesAmbDiferentsFormesDePagament(sWarnFpgs, oFpgs) Then
                                MsgBox(sWarnFpgs, vbExclamation)
                            Else
                                If oFpgs.Count > 0 Then
                                    _PaymentTerms = oFpgs.First
                                End If
                            End If
                        End If

                    Case Else
                        ToolStripButtonFpg.Visible = False
                End Select
        End Select
    End Sub

    Private Function CheckComandesAmbDiferentsFormesDePagament(ByRef sWarnText As String, ByRef oFpgs As List(Of DTOPaymentTerms)) As Boolean
        Dim exs As New List(Of Exception)
        Dim retVal As Boolean = False
        Dim sCurrentFpg As String = ""
        Dim oPaymentterms = _Ccx.PaymentTerms
        If oPaymentterms IsNot Nothing Then
            sCurrentFpg = DTOPaymentTerms.XMLEncoded(oPaymentterms)
        End If

        Dim oPdcs = Xl_DeliveryItems1.Items.Select(Function(x) x.PurchaseOrderItem.PurchaseOrder).Distinct
        For Each oPdc In oPdcs
            If oPdc.PaymentTerms IsNot Nothing Then
                If Not DTOPaymentTerms.Match(oPdc.PaymentTerms, _Ccx.PaymentTerms) Then
                    Dim BlFpgExists As Boolean = oFpgs.Any(Function(x) DTOPaymentTerms.Match(x, oPdc.PaymentTerms))

                    If Not BlFpgExists Then

                        oFpgs.Add(oPdc.PaymentTerms)
                        If sWarnText > "" Then
                            sWarnText = sWarnText & vbCrLf
                        Else
                            sWarnText = "formes de pago especials:" & vbCrLf
                        End If
                        sWarnText = sWarnText & "comanda " & oPdc.Num & ": " & FEB.PaymentTerms.Text(oPdc.PaymentTerms, Current.Session.Lang)
                    End If

                End If
            End If
        Next

        retVal = oFpgs.Count > 1
        Return retVal
    End Function


    Private Sub ToolStripButtonFpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFpg.Click
        Dim oFrm As New Frm_PaymentTerms(_PaymentTerms)
        AddHandler oFrm.AfterUpdate, AddressOf Paymentterms_AfterUpdate
        oFrm.Show()
    End Sub

    Private Sub ToolStripButtonExemptIva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExemptIva.Click
        SwitchCheckedToolStripButton(ToolStripButtonExemptIva, _ExemptIva)
    End Sub

    Private Sub Paymentterms_AfterUpdate(ByVal sender As System.Object, ByVal e As MatEventArgs)
        _PaymentTerms = e.Argument

        If _PaymentTerms.Cod = DTOPaymentTerms.CodsFormaDePago.NotSet Then
            ToolStripButtonFpg.Image = My.Resources.UnChecked13
        Else
            ToolStripButtonFpg.Image = My.Resources.Checked13
        End If
        SetDirty()
    End Sub


    Private Sub ToolStripMenuItemCredit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemCredit.Click
        SetCredit(DTOCustomer.CashCodes.credit)
    End Sub

    Private Sub ToolStripMenuItemReembols_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemReembols.Click
        SetCredit(DTOCustomer.CashCodes.Reembols)
    End Sub

    Private Sub ToolStripMenuItemTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemTransfer.Click
        SetCredit(DTOCustomer.CashCodes.TransferenciaPrevia)
    End Sub

#End Region

    Private Sub CheckBoxRecycle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRecycle.CheckedChanged
        ComboBoxNum.Visible = CheckBoxRecycle.Checked
    End Sub


    Private Sub ToolStripSplitButtonCredit_Click(sender As Object, e As System.EventArgs) Handles ToolStripSplitButtonCredit.Click
        'ToolStripButtonTransmisio.Text = "pendent de transmetre"
        'mAlb.RetencioCod = DTODelivery.RetencioCods.Free
    End Sub

    Private Sub Xl_ContactMgz_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_ContactMgz.AfterUpdate
        SetDirty()
    End Sub

    Private Sub ToolStripButtonDiposit_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonDiposit.Click
        SwitchCheckedToolStripButton(ToolStripButtonDiposit, _Diposit)
    End Sub

    Private Sub SwitchCheckedToolStripButton(oToolStripButton As ToolStripButton, ByRef Value As Boolean)
        Value = Not Value
        oToolStripButton.Image = IIf(Value, My.Resources.Checked13, My.Resources.UnChecked13)
        SetDirty()
    End Sub



    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
            TextBoxNom.TextChanged,
             TextBoxAdr.TextChanged,
              Xl_LookupZip1.AfterUpdate,
               CheckBoxPlatform.CheckedChanged,
                Xl_Contact2Platform.AfterUpdate,
                 CheckBoxRecycle.CheckedChanged,
                  ComboBoxNum.SelectedIndexChanged,
                   CheckBoxValorat.CheckedChanged,
                    CheckBoxFacturable.CheckedChanged,
                     TextBoxTel.TextChanged,
                      DateTimePickerFch.ValueChanged,
                       ComboBoxPorts.SelectedIndexChanged,
                        CheckBoxExport.CheckedChanged,
                         CheckBoxCEE.CheckedChanged,
        Xl_Contact2Platform.AfterUpdate,
       Xl_LookupZip1.AfterUpdate,
        Xl_LookupIncoterm1.AfterUpdate,
         TextBoxObsTransp.TextChanged

        If _AllowEvents Then
            SetDirty()
        End If

    End Sub



    Private Sub Xl_DocFileEtiquetesTransport_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFileEtiquetesTransport.AfterFileDropped
        If Xl_DocFileEtiquetesTransport.Value Is Nothing Then
            TabControl1.TabPages(Tabs.etiquetesTransport).ImageKey = ""
        Else
            TabControl1.TabPages(Tabs.etiquetesTransport).ImageKey = Tabs.etiquetesTransport.ToString
        End If

    End Sub

    Private Sub CheckBoxExport_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxExport.CheckedChanged
        'CheckBoxExport.ForeColor = Me.BackColor
        Dim exs As New List(Of Exception)
        CheckBoxCEE.Visible = CheckBoxExport.Checked
        If _AllowEvents Then
            CheckBoxExport.ForeColor = Me.ForeColor
            CheckBoxExport.BackColor = Me.BackColor
            If Not SetTotals(exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub CheckBoxPlatform_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPlatform.CheckedChanged
        If _AllowEvents Then
            Xl_Contact2Platform.Visible = CheckBoxPlatform.Checked
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxFacturarA_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFacturarA.CheckedChanged
        If _AllowEvents Then
            Xl_Contact2FacturarA.Visible = CheckBoxFacturarA.Checked
            SetDirty()
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)

        Dim oBook = FEB.Delivery.Excel(exs, _Delivery)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oBook, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_DeliveryItems1_ToggleProgressBarRequest(visible As Boolean) Handles Xl_DeliveryItems1.ToggleProgressBarRequest
        UIHelper.ToggleProggressBar(Panel1, visible)
    End Sub

    Private Async Sub Xl_DeliveryItems1_RequestToReload(sender As Object, e As MatEventArgs) Handles Xl_DeliveryItems1.RequestToReload
        Dim exs As New List(Of Exception)
        If FEB.Delivery.Load(_Delivery, exs) Then
            Await Xl_DeliveryItems1.Load(_Delivery)
            SetKg()
            If SetTotals(exs) Then
                SetFormaDePago()
                Xl_UsrLog1.Load(_Delivery.UsrLog)
                ShowCredit()

                SetPorts()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If Not _TabLoaded(TabControl1.SelectedIndex) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.docfiles
                    Await RefrescaDocfiles()
            End Select
        End If
    End Sub

    Private Async Sub RefrescaDocfiles(sender As Object, e As MatEventArgs)
        Await RefrescaDocfiles()
    End Sub

    Private Async Function RefrescaDocfiles() As Task
        Dim exs As New List(Of Exception)
        Dim values = Await FEB.DocFileSrcs.All(_Delivery, exs)
        If exs.Count = 0 Then
            Dim specificCodes As New List(Of DTODocFile.Cods)
            specificCodes.Add(DTODocFile.Cods.dua)
            specificCodes.Add(DTODocFile.Cods.transportLabels)
            Xl_DocfileSrcs1.Load(values,,, specificCodes)
            _TabLoaded(TabControl1.SelectedIndex) = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Sub Xl_DocfileSrcs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToAddNew
        Dim oDocfileSrc As New DTODocFileSrc()
        With oDocfileSrc
            If TypeOf e.Argument Is DTODocFile.Cods Then
                .Cod = e.Argument
            Else
                .Cod = DTODocFile.Cods.download
            End If
            Select Case .Cod
                Case DTODocFile.Cods.dua
                    .Nom = "DUA"
                Case DTODocFile.Cods.transportLabels
                    .Nom = "Etiquetes de transport"
            End Select
            .Src = _Delivery
        End With
        Dim oFrm As New Frm_DocfileSrc(oDocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaDocfiles
        oFrm.Show()
    End Sub

    Private Async Sub Xl_DocfileSrcs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToRefresh
        Await RefrescaDocfiles()
    End Sub


End Class