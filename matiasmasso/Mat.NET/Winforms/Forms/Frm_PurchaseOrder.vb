Public Class Frm_PurchaseOrder
    Private _PurchaseOrder As DTOPurchaseOrder
    Private _Ccx As DTOCustomer
    Private _Obs As String
    Private _CustomerDocUrl As String
    Private _FchMin As Date
    Private _Dirty As Boolean
    Private _DirtyPdd As Boolean
    Private _ViewSortides As Xl_PurchaseOrderDeliveryItems = Nothing
    Private _PaymentTerms As DTOPaymentTerms


    Private _AllowEvents As Boolean

    'TODO: Imports tarifa 50%, Bayon 10%

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        linies
        doc
        etiquetesTransport
    End Enum

    Public Sub New(oPurchaseOrder As DTOPurchaseOrder)
        MyBase.New()
        Me.InitializeComponent()
        _PurchaseOrder = oPurchaseOrder
    End Sub


    Private Async Sub Frm_PurchaseOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        'UIHelper.ToggleProggressBar(Panel1, True)
        If _PurchaseOrder.IsNew Then
            If Not FEB2.Customer.ValidateCustomer(_PurchaseOrder.Customer, exs) Then
                UIHelper.WarnError(exs, "Error o Fitxa de client incomplerta:")
                Me.Close()
            End If
        Else
            If Not FEB2.PurchaseOrder.Load(exs, _PurchaseOrder, GlobalVariables.Emp.Mgz) Then
                UIHelper.WarnError(exs)
            End If
        End If

        _Ccx = _PurchaseOrder.Customer.CcxOrMe
        Xl_PurchaseOrderItems1.CustomerTarifaDtos = Await FEB2.CustomerTarifaDtos.Active(exs, _Ccx)
        Xl_PurchaseOrderItems1.CliProductDtos = Await FEB2.CliProductDtos.All(_Ccx, exs)
        Xl_PurchaseOrderItems1.CustomCosts = Await FEB2.PriceListItemsCustomer.Active(exs, _Ccx, DateTimePicker1.Value)

        With ImageListTabHeaders
            .Images.Add(Tabs.doc.ToString, My.Resources.star_green)
            .Images.Add(Tabs.etiquetesTransport.ToString, My.Resources.label16)
        End With

        Await refresca()
        _AllowEvents = True

    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        With _PurchaseOrder
            _PaymentTerms = .PaymentTerms

            If .IsNew Then
                Me.Text = "Nova comanda de " & .Contact.FullNom
                'ValidateNif(exs) (ja hem validat el customer)
                Await ValidateCreditEmail(exs)
            Else
                Me.Text = "Comanda " & _PurchaseOrder.Num & " de " & .Contact.FullNom
            End If


            TextBoxConcept.Text = .concept
            TextBoxConcept.BackColor = LegacyHelper.ImageHelper.Converter(Await FEB2.Contact.BackColor(GlobalVariables.Emp, _Ccx, exs))
            Xl_PdcSrc1.Load(.Source)
            DateTimePicker1.Value = .Fch


            If .Incentiu IsNot Nothing Then
                CheckBoxPromo.Checked = True
                Xl_LookupPromo1.Visible = True
                Xl_LookupPromo1.Incentiu = .Incentiu
            End If

            If .Platform IsNot Nothing Then
                FEB2.Contact.Load(.Platform, exs)
                Xl_ContactPlatform.Contact = .Platform
            End If

            If .DocFile IsNot Nothing Then
                TabControl1.TabPages(Tabs.doc).ImageKey = Tabs.doc.ToString
                Await Xl_DocFileDoc.Load(.DocFile)
            End If

            If .EtiquetesTransport IsNot Nothing Then
                TabControl1.TabPages(Tabs.etiquetesTransport).ImageKey = Tabs.etiquetesTransport.ToString
                Await Xl_DocFileEtiquetesTransport.Load(.EtiquetesTransport)
            End If

            Dim existPendents As Boolean = .Items.Exists(Function(x) x.Pending <> 0)
            If existPendents Then ButtonAlb.Enabled = True

            If Not .IsNew Then
                Dim oDeliveryItems = Await FEB2.DeliveryItems.All(exs, _PurchaseOrder)
                ButtonDel.Enabled = oDeliveryItems.Count > 0
            End If

            _Obs = .Obs
            _CustomerDocUrl = .CustomerDocUrl
            DisplayStatusObs()

            Select Case .TotJunt
                Case True
                    ToolStripButtonServirTotJunt.Checked = True
                    ToolStripButtonServirTotJunt.Image = My.Resources.Checked13
                Case Else
                    ToolStripButtonServirTotJunt.Checked = False
                    ToolStripButtonServirTotJunt.Image = My.Resources.UnChecked13
            End Select

            Select Case .Pot
                Case True
                    ToolStripButtonPot.Checked = True
                    ToolStripButtonPot.Image = My.Resources.Checked13
                Case Else
                    ToolStripButtonPot.Checked = False
                    ToolStripButtonPot.Image = My.Resources.UnChecked13
            End Select

            Select Case .BlockStock
                Case True
                    ToolStripButtonBlockStock.Checked = True
                    ToolStripButtonBlockStock.Image = My.Resources.Checked13
                Case Else
                    ToolStripButtonBlockStock.Checked = False
                    ToolStripButtonBlockStock.Image = My.Resources.UnChecked13
            End Select

            _FchMin = .fchDeliveryMin
            RefrescaFchMin()

            RefrescaStatusBar()

            'With _Ccx
            '.TarifaDtos = Await FEB2.CustomerTarifaDtos.Active(exs, _Ccx)
            '.ProductDtos = Await FEB2.CliProductDtos.All(_Ccx, exs)
            'End With

            If .Source <> DTOPurchaseOrder.Sources.ExcelMayborn Then
                ValidateTarifaCost(exs)
            End If

            If exs.Count = 0 Then
                ButtonOk.Enabled = (.IsNew And .Items.Count > 0)
            End If


            Dim oMenuClient As New Menu_Contact(.contact)
            ClientToolStripMenuItem.DropDownItems.AddRange(oMenuClient.Range)

        End With

        Await Xl_PurchaseOrderItems1.Load(_PurchaseOrder, _Ccx)
        TextBoxTotal.Text = String.Format("total:  {0}", DTOAmt.CurFormatted(Xl_PurchaseOrderItems1.GetTotals))

        Application.DoEvents()


        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If


    End Function

    Private Sub RefrescaFchMin()
        If _FchMin = Date.MinValue Then
            ToolStripButtonFchMin.Text = "servei inmediat"
            ToolStripButtonFchMin.Checked = False
        Else
            ToolStripButtonFchMin.Text = "servei " & _FchMin.ToShortDateString
            ToolStripButtonFchMin.Checked = True
        End If
    End Sub

    Private Sub ToolStripButtonObs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonObs.Click
        Dim s As String = InputBox("Observacions:", Me.Text, _Obs)
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

    Private Sub SetDirty()
        _Dirty = True
        ButtonOk.Enabled = True
    End Sub

    Private Sub DisplayStatusObs()
        ToolStripStatusLabelObs.Text = _Obs
        ToolStripStatusLabelObs.Visible = (_Obs.isNotEmpty())
        ToolStripStatusLabelCustDoc.Text = _CustomerDocUrl
        ToolStripStatusLabelCustDoc.Visible = _CustomerDocUrl > ""
        StatusStripObs.Visible = (_Obs > "" Or _CustomerDocUrl.isNotEmpty())
    End Sub


    Private Sub ValidateTarifaCost(exs As List(Of Exception))
        If _Ccx IsNot Nothing Then
            If _Ccx.TarifaDtos IsNot Nothing Then
                If _Ccx.TarifaDtos.Count = 0 Then
                    exs.Add(New Exception("client sense tarifa, sortira amb preus PVP"))
                End If
            End If
        End If
    End Sub

    Private Async Function ValidateCreditEmail(exs As List(Of Exception)) As Task(Of Boolean)
        If _Ccx.CashCod = DTOCustomer.CashCodes.credit Then
            If _Ccx.FraPrintMode = DTOCustomer.FraPrintModes.Email Then
                If Not Await FEB2.Customer.EFrasEnabled(exs, _Ccx) Then
                    exs.Add(New Exception("No té cap email habilitat per rebre les factures!"))
                End If
            End If
        End If
        Return exs.Count = 0
    End Function


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_PurchaseOrderItems1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderItems1.AfterUpdate
        If _AllowEvents Then
            SetDirty()
            Dim oBase As DTOAmt = Xl_PurchaseOrderItems1.GetTotals()
            If oBase Is Nothing Then
                TextBoxTotal.Clear()
            Else
                TextBoxTotal.Text = String.Format("total: {0}", DTOAmt.CurFormatted(oBase))
                ButtonOk.Enabled = True
            End If
            Dim existPendents As Boolean = Xl_PurchaseOrderItems1.Items.Exists(Function(x) x.Pending <> 0)
            If existPendents Then
                ButtonAlb.Enabled = True
            End If
        End If
    End Sub

    Private Sub CheckBoxPromo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxPromo.CheckedChanged
        If _AllowEvents Then
            Xl_LookupPromo1.Visible = CheckBoxPromo.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Frm_PurchaseOrder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim exs As New List(Of Exception)
        If Not FEB2.AlbBloqueig.BloqueigEnd(Current.Session.User, _PurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
            e.Cancel = True
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        'For Each item As DTOPurchaseOrderItem In _PurchaseOrder.Items : item.PurchaseOrder = _PurchaseOrder : Next
        If Await Save(_PurchaseOrder, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
            UIHelper.ToggleProggressBar(Panel1, False)
            MsgBox("Comanda " & _PurchaseOrder.num & " registrada correctament", MsgBoxStyle.Information, "MAT.NET")
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al grabar la comanda")
        End If
    End Sub

    Private Function ReadFromForm(exs As List(Of Exception)) As Boolean
        With _PurchaseOrder
            .emp = GlobalVariables.Emp.trimmed
            .fch = DateTimePicker1.Value
            .source = Xl_PdcSrc1.Source
            .concept = TextBoxConcept.Text
            .obs = ToolStripStatusLabelObs.Text
            .totJunt = ToolStripButtonServirTotJunt.Checked
            .pot = ToolStripButtonPot.Checked
            .blockStock = ToolStripButtonBlockStock.Checked
            .fchDeliveryMin = _FchMin
            .items = Xl_PurchaseOrderItems1.Items
            .paymentTerms = _PaymentTerms
            .customerDocUrl = _CustomerDocUrl

            If Xl_ContactPlatform.Contact Is Nothing Then
                .platform = Nothing
            Else
                .platform = New DTOCustomerPlatform(Xl_ContactPlatform.Contact.Guid)
            End If

            If CheckBoxPromo.Checked Then
                .incentiu = Xl_LookupPromo1.Incentiu
            Else
                .incentiu = Nothing
            End If

            If Xl_DocFileDoc.IsDirty Then
                .docFile = Xl_DocFileDoc.Value
            End If

            If Xl_DocFileEtiquetesTransport.IsDirty Then
                .etiquetesTransport = Xl_DocFileEtiquetesTransport.Value
            End If

            If .IsNew Then
                .UsrLog = DTOUsrLog.Factory(Current.Session.User)
            Else
                .UsrLog.usrLastEdited = Current.Session.User
            End If

            Dim iEmptyQties As Integer = Xl_PurchaseOrderItems1.EmptyQties
            Select Case iEmptyQties
                Case 0
                Case 1
                    exs.Add(New Exception("Detectada 1 linia sense quantitat"))
                Case Else
                    exs.Add(New Exception(String.Format("Detectades {0} linies sense quantitat", iEmptyQties)))
            End Select
        End With
        Return exs.Count = 0
    End Function

    Private Async Function Save(oPurchaseOrder As DTOPurchaseOrder, exs As List(Of Exception)) As Task(Of Boolean)
        If ReadFromForm(exs) Then
            If exs.Count = 0 Then
                Dim pPurchaseOrder = Await FEB2.PurchaseOrder.Update(exs, oPurchaseOrder)
                If exs.Count = 0 Then
                    _PurchaseOrder = pPurchaseOrder
                End If
            End If
        End If
        Return exs.Count = 0
    End Function

    Private Async Sub ButtonAlb_Click(sender As Object, e As EventArgs) Handles ButtonAlb.Click
        If _Dirty Or _PurchaseOrder.IsNew Then
            Dim exs As New List(Of Exception)
            If Await Save(_PurchaseOrder, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
                Do_NewAlb()
            Else
                UIHelper.WarnError(exs, "error al grabar la comanda")
            End If
        Else
            Do_NewAlb()
        End If
    End Sub

    Private Async Sub Do_NewAlb()
        Dim exs As New List(Of Exception)
        Dim oCustomer As DTOCustomer = _PurchaseOrder.Contact
        If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
            FEB2.Contact.Load(oCustomer, exs)
            Dim oDelivery As DTODelivery = FEB2.Delivery.Factory(exs, _PurchaseOrder.Customer, Current.Session.User, GlobalVariables.Emp.Mgz)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                oFrm.Show()
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem la comanda " & _PurchaseOrder.Num & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.PurchaseOrder.Delete(exs, _PurchaseOrder) Then
                MsgBox("Comanda eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la comanda")
            End If
        End If
    End Sub

    Private Async Sub TextBoxConcept_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxConcept.KeyDown
        Dim exs As New List(Of Exception)
        Dim s As String = TextBoxConcept.Text

        If _DirtyPdd Then
            Dim oLang As DTOLang = _PurchaseOrder.Contact.Lang

            Select Case e.KeyCode
                Case Keys.Return, Keys.Tab
                    Dim oPdd = Await FEB2.PurchaseOrder.SearchConcepte(exs, s)
                    If exs.Count = 0 Then
                        If oPdd IsNot Nothing Then
                            TextBoxConcept.Text = oLang.Tradueix(oPdd.Esp, oPdd.Cat, oPdd.Eng)
                            TextBoxConcept.SelectionStart = TextBoxConcept.Text.Length
                            Xl_PdcSrc1.Load(oPdd.Src)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select
            _DirtyPdd = False
        End If

    End Sub


    Private Sub TextBoxConcept_TextChanged(sender As Object, e As EventArgs) Handles TextBoxConcept.TextChanged
        If _AllowEvents Then

            If Xl_PdcSrc1.Source = DTOPurchaseOrder.Sources.no_Especificado Then
                Dim s As String = TextBoxConcept.Text.ToUpper
                If Microsoft.VisualBasic.Left(s, 2) = "SR" Then
                    Xl_PdcSrc1.Load(DTOPurchaseOrder.Sources.Telefonico)
                ElseIf s.IndexOf("FAX") >= 0 Then
                    Xl_PdcSrc1.Load(DTOPurchaseOrder.Sources.Fax)
                ElseIf s.IndexOf("MAIL") >= 0 Then
                    Xl_PdcSrc1.Load(DTOPurchaseOrder.Sources.eMail)
                ElseIf s.IndexOf("REPRES") >= 0 Then
                    Xl_PdcSrc1.Load(DTOPurchaseOrder.Sources.representante)
                ElseIf s.IndexOf("FERIA") >= 0 Then
                    Xl_PdcSrc1.Load(DTOPurchaseOrder.Sources.fira)
                ElseIf s.IndexOf("FIRA") >= 0 Then
                    Xl_PdcSrc1.Load(DTOPurchaseOrder.Sources.fira)
                End If
            End If
            _DirtyPdd = True

            If Not _PurchaseOrder.IsNew Then
                SetDirty()
            End If
        End If

    End Sub

    Private Sub TextBoxStatus_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxStatus.DoubleClick
        Dim oUser As DTOUser = _PurchaseOrder.UsrLog.UsrCreated
        If FEB2.User.IsAllowedToRead(Current.Session.User, oUser) Then
            Dim oFrm As New Frm_User(oUser)
            AddHandler oFrm.AfterUpdate, AddressOf RefrescaStatusBar
            oFrm.Show()
        Else
            MsgBox("Aquest usuari es d'accés restringit per la Llei de Protecció de Dades")
        End If
    End Sub

    Private Sub RefrescaStatusBar()
        TextBoxStatus.Text = _PurchaseOrder.UsrLog.Text()
    End Sub

    Private Async Sub ToolStripButtonView_Click(sender As Object, e As EventArgs) Handles ToolStripButtonView.Click
        Dim exs As New List(Of Exception)
        With ToolStripButtonView
            Select Case .Checked
                Case False
                    .Text = "vista sortides"
                    Xl_PurchaseOrderItems1.Visible = True
                    If _ViewSortides IsNot Nothing Then
                        _ViewSortides.Visible = False
                    End If
                Case True
                    .Text = "vista comanda"
                    Xl_PurchaseOrderItems1.Visible = False
                    If _ViewSortides Is Nothing Then
                        Dim items = Await FEB2.DeliveryItems.All(exs, _PurchaseOrder)
                        If exs.Count = 0 Then
                            _ViewSortides = New Xl_PurchaseOrderDeliveryItems
                            _ViewSortides.Load(items)
                            PanelItems.Controls.Add(_ViewSortides)
                            _ViewSortides.BringToFront()
                            _ViewSortides.Dock = DockStyle.Fill
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    End If

                    _ViewSortides.Visible = True
            End Select
        End With
    End Sub

    Private Sub ToolStripButtonFchMin_Click(sender As Object, e As EventArgs) Handles ToolStripButtonFchMin.Click
        Dim oFrm As New Frm_Date(_FchMin)
        AddHandler oFrm.AfterUpdate, AddressOf onFchMinChanged
        oFrm.Show()
    End Sub

    Private Sub onFchMinChanged(sender As Object, e As MatEventArgs)
        _FchMin = e.Argument
        RefrescaFchMin()
        If Not _PurchaseOrder.IsNew Then SetDirty()
    End Sub

    Private Sub ToolStripButtonServirTotJunt_Click(sender As Object, e As EventArgs) Handles ToolStripButtonServirTotJunt.Click
        If _AllowEvents Then
            If ToolStripButtonServirTotJunt.Checked Then
                ToolStripButtonServirTotJunt.Image = My.Resources.Checked13
            Else
                ToolStripButtonServirTotJunt.Image = My.Resources.UnChecked13
            End If
            If Not _PurchaseOrder.IsNew Then SetDirty()
        End If

    End Sub

    Private Sub ToolStripButtonPot_Click(sender As Object, e As EventArgs) Handles ToolStripButtonPot.Click
        If _AllowEvents Then
            If ToolStripButtonPot.Checked Then
                ToolStripButtonPot.Image = My.Resources.Checked13
            Else
                ToolStripButtonPot.Image = My.Resources.UnChecked13
            End If
            If Not _PurchaseOrder.IsNew Then SetDirty()
        End If
    End Sub

    Private Sub ToolStripButtonBlockStock_Click(sender As Object, e As EventArgs) Handles ToolStripButtonBlockStock.Click
        If _AllowEvents Then
            If ToolStripButtonBlockStock.Checked Then
                ToolStripButtonBlockStock.Image = My.Resources.Checked13
            Else
                ToolStripButtonBlockStock.Image = My.Resources.UnChecked13
            End If
            If Not _PurchaseOrder.IsNew Then SetDirty()
        End If
    End Sub

    Private Sub ButtonExcel_Click(sender As Object, e As EventArgs) Handles ButtonExcel.Click
        Dim exs As New List(Of Exception)
        If ReadFromForm(exs) Then
            Dim oSheet = FEB2.PurchaseOrder.Excel(_PurchaseOrder)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            Xl_PurchaseOrderItems1.Fch = DateTimePicker1.Value
            If Not _PurchaseOrder.IsNew Then SetDirty()
        End If
    End Sub


    Private Sub ControlChanged(sender As Object, e As MatEventArgs) Handles Xl_LookupPromo1.AfterUpdate, Xl_ContactPlatform.AfterUpdate, Xl_DocFileDoc.AfterUpdate, Xl_DocFileEtiquetesTransport.AfterUpdate
        If _AllowEvents Then
            If Not _PurchaseOrder.IsNew Then SetDirty()
        End If
    End Sub

    Private Sub ToolStripButtonFpg_Click(sender As Object, e As EventArgs) Handles ToolStripButtonFpg.Click
        Dim oFrm As New Frm_PaymentTerms(_PaymentTerms)
        AddHandler oFrm.AfterUpdate, AddressOf onPaymentTermsUpdated
        oFrm.Show()
    End Sub

    Private Sub onPaymentTermsUpdated(sender As Object, e As MatEventArgs)
        _PaymentTerms = e.Argument
        SetDirty()
    End Sub

    Private Sub Xl_PurchaseOrderItems1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderItems1.RequestToToggleProgressBar
        UIHelper.ToggleProggressBar(Panel1, e.Argument)
    End Sub



    'Private Sub Xl_PurchaseOrderItems1_PriceListLoaded(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderItems1.PriceListLoaded
    'UIHelper.ToggleProggressBar(Panel1, False)
    'End Sub
End Class