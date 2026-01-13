Public Class Frm_PurchaseOrder
    Private _PurchaseOrder As DTOPurchaseOrder
    Private _Obs As String
    Private _CustomerDocUrl As String
    Private _FchMin As Date
    Private _Dirty As Boolean
    Private _DirtyPdd As Boolean
    Private _ViewSortides As Xl_PurchaseOrderDeliveryItems = Nothing

    Private _AllowEvents As Boolean

    'TODO: Imports tarifa 50%, Bayon 10%

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oPurchaseOrder As DTOPurchaseOrder)
        MyBase.New()
        Me.InitializeComponent()
        _PurchaseOrder = oPurchaseOrder
        BLL.BLLPurchaseOrder.Load(_PurchaseOrder)

        With _PurchaseOrder
            BLL.BLLContact.Load(.Customer)
            If .IsNew Then
                Me.Text = "Nova comanda de " & .Customer.FullNom
            Else
                BLL.BLLPurchaseOrder.Load(_PurchaseOrder, BLL.BLLApp.Mgz)
                Me.Text = "Comanda " & _PurchaseOrder.Id & " de " & .Customer.FullNom
            End If

            _PurchaseOrder.Customer.Ccx = BLL.BLLCustomer.CcxOrMe(.Customer)
            BLL.BLLContact.Load(.Customer.Ccx) 'per el NIF
            .Customer.Ccx.TarifaDtos = BLL.BLLCustomerTarifaDtos.All(.Customer.Ccx)
            .Customer.Ccx.ProductDtos = BLL.BLLCliProductDtos.All(.Customer.Ccx)

            If .IsNew Then
                Dim exs As New List(Of Exception)
                ValidateNif(exs)
                ValidateCreditEmail(exs)
                ValidateTarifaCost(exs)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                End If
            End If

            Xl_Contact_Logo1.Load(.Customer)
            TextBoxConcept.Text = .Concept
            TextBoxConcept.BackColor = BLL.BLLCustomer.BackColor(.Customer.Ccx)
            Xl_PdcSrc1.Load(.Source)
            DateTimePicker1.Value = .Fch

            If .Promo IsNot Nothing Then
                CheckBoxPromo.Checked = True
                Xl_LookupPromo1.Visible = True
                Xl_LookupPromo1.Promo = .Promo
            End If

            Xl_DocFile1.Load(.DocFile)

            Dim existPendents As Boolean = .Items.Exists(Function(x) x.Pending > 0)
            If existPendents Then ButtonAlb.Enabled = True

            If Not .IsNew Then
                Dim existSortides As Boolean = BLL.BLLDeliveryItems.FromPurchaseOrder(_PurchaseOrder).Count > 0
                ButtonDel.Enabled = Not existSortides
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

            _FchMin = .FchMin
            RefrescaFchMin()

            RefrescaStatusBar()

            ButtonOk.Enabled = (.IsNew And .Items.Count > 0)

        End With

        Xl_PurchaseOrderItems1.Load(_PurchaseOrder)
        TextBoxTotal.Text = String.Format("total: {0}", Xl_PurchaseOrderItems1.GetTotals.CurFormatted)

        _AllowEvents = True
    End Sub

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
        ToolStripStatusLabelObs.Visible = (_Obs > "")
        ToolStripStatusLabelCustDoc.Text = _CustomerDocUrl
        ToolStripStatusLabelCustDoc.Visible = _CustomerDocUrl > ""
        StatusStripObs.Visible = (_Obs > "" Or _CustomerDocUrl > "")
    End Sub


    Private Sub ValidateTarifaCost(exs As List(Of Exception))
        If _PurchaseOrder.Customer.Ccx.TarifaDtos.Count = 0 Then
            exs.Add(New Exception("client sense tarifa, sortira amb preus PVP"))
        End If
    End Sub

    Private Sub ValidateCreditEmail(exs As List(Of Exception))
        If _PurchaseOrder.Customer.Ccx.CashCod = DTO.DTOCustomer.CashCodes.credit Then
            If Not BLL.BLLCustomer.EFrasEnabled(_PurchaseOrder.Customer.Ccx) Then
                exs.Add(New Exception("No té cap email habilitat per rebre les factures!"))
            End If
        End If
    End Sub

    Private Sub ValidateNif(exs As List(Of Exception))
        If _PurchaseOrder.Customer.Ccx.Nif = "" Then
            exs.Add(New Exception("falta el NIF!"))
        End If
    End Sub

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
                TextBoxTotal.Text = String.Format("total: {0}", oBase.CurFormatted)
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
        If BLL.BLLAlbBloqueig.BloqueigEnd(BLL.BLLSession.Current.User, _PurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
        Else
            e.Cancel = True
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        If Save(_PurchaseOrder, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
            MsgBox("Comanda " & _PurchaseOrder.Id & " registrada correctament", MsgBoxStyle.Information, "MAT.NET")
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al grabar la comanda")
        End If
    End Sub

    Private Function Save(oPurchaseOrder As DTOPurchaseOrder, exs As List(Of Exception)) As Boolean
        With oPurchaseOrder
            .Fch = DateTimePicker1.Value
            .Source = Xl_PdcSrc1.Source
            .Concept = TextBoxConcept.Text
            .Obs = ToolStripStatusLabelObs.Text
            .TotJunt = ToolStripButtonServirTotJunt.Checked
            .Pot = ToolStripButtonPot.Checked
            .FchMin = _FchMin
            .Items = Xl_PurchaseOrderItems1.Items
            .CustomerDocUrl = _CustomerDocUrl

            If CheckBoxPromo.Checked Then
                .Promo = Xl_LookupPromo1.Promo
            Else
                .Promo = Nothing
            End If



            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If

            If .IsNew Then
                .UsrCreated = BLL.BLLSession.Current.User
            Else
                .UsrLastEdited = BLL.BLLSession.Current.User
            End If
        End With

        Dim retval As Boolean = BLL.BLLPurchaseOrder.Update(oPurchaseOrder, exs)
        Return retval
    End Function

    Private Sub ButtonAlb_Click(sender As Object, e As EventArgs) Handles ButtonAlb.Click
        If _Dirty Then
            Dim exs As New List(Of Exception)
            If Save(_PurchaseOrder, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
                Do_NewAlb()
            Else
                UIHelper.WarnError(exs, "error al grabar la comanda")
            End If
        Else
            Do_NewAlb()
        End If
    End Sub

    Private Sub Do_NewAlb()
        Dim exs As New List(Of Exception)
        Dim oCustomer As DTOCustomer = _PurchaseOrder.Customer
        BLL.BLLContact.Load(oCustomer)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oClient As New Client(_PurchaseOrder.Customer.Guid)
            Dim oAlb As Alb = oClient.NewAlb()
            Dim oFrm As New Frm_AlbNew2(oAlb)
            oFrm.Show()
            Me.Close()
        End If
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem la comanda " & _PurchaseOrder.Id & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLPurchaseOrder.Delete(_PurchaseOrder, exs) Then
                MsgBox("Comanda eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la comanda")
            End If
        End If
    End Sub

    Private Sub TextBoxConcept_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxConcept.KeyDown
        Dim s As String = TextBoxConcept.Text

        If _DirtyPdd Then
            Dim oLang As DTOLang = _PurchaseOrder.Customer.Lang

            Select Case e.KeyCode
                Case Keys.Return, Keys.Tab
                    Dim oPdd As DTOPurchaseOrderConcepte = BLL.BLLPurchaseOrder.SearchConcepte(s)
                    If oPdd IsNot Nothing Then
                        TextBoxConcept.Text = oLang.Tradueix(oPdd.Esp, oPdd.Cat, oPdd.Eng)
                        TextBoxConcept.SelectionStart = TextBoxConcept.Text.Length
                        Xl_PdcSrc1.Load(oPdd.Src)
                    End If
            End Select
            _DirtyPdd = False
        End If

    End Sub


    Private Sub TextBoxConcept_TextChanged(sender As Object, e As EventArgs) Handles TextBoxConcept.TextChanged
        If _AllowEvents Then

             If Xl_PdcSrc1.Source = DTO.DTOPurchaseOrder.Sources.no_Especificado Then
                Dim s As String = TextBoxConcept.Text.ToUpper
                If Microsoft.VisualBasic.Left(s, 2) = "SR" Then
                    Xl_PdcSrc1.Load(DTO.DTOPurchaseOrder.Sources.Telefonico)
                ElseIf s.IndexOf("FAX") >= 0 Then
                    Xl_PdcSrc1.Load(DTO.DTOPurchaseOrder.Sources.Fax)
                ElseIf s.IndexOf("MAIL") >= 0 Then
                    Xl_PdcSrc1.Load(DTO.DTOPurchaseOrder.Sources.eMail)
                ElseIf s.IndexOf("REPRES") >= 0 Then
                    Xl_PdcSrc1.Load(DTO.DTOPurchaseOrder.Sources.representante)
                ElseIf s.IndexOf("FERIA") >= 0 Then
                    Xl_PdcSrc1.Load(DTO.DTOPurchaseOrder.Sources.fira)
                ElseIf s.IndexOf("FIRA") >= 0 Then
                    Xl_PdcSrc1.Load(DTO.DTOPurchaseOrder.Sources.fira)
                End If
            End If
            _DirtyPdd = True

            If Not _PurchaseOrder.IsNew Then
                SetDirty()
            End If
        End If

    End Sub

    Private Sub TextBoxStatus_DoubleClick(sender As Object, e As EventArgs) Handles TextBoxStatus.DoubleClick
        Dim oUser As DTOUser = _PurchaseOrder.UsrCreated
        If BLL.BLLUser.IsAllowedToRead(BLL.BLLSession.Current.User, oUser) Then
            Dim oFrm As New Frm_User(oUser)
            AddHandler oFrm.AfterUpdate, AddressOf RefrescaStatusBar
            oFrm.Show()
        Else
            MsgBox("Aquest usuari es d'accés restringit per la Llei de Protecció de Dades")
        End If
    End Sub

    Private Sub RefrescaStatusBar()
        With _PurchaseOrder
            If .IsNew Then
                TextBoxStatus.Text = String.Format("Registrat per {0}", BLL.BLLUser.NicknameOrElse(.UsrCreated))
            Else
                Dim Edited As Boolean = .FchLastEdited <> Nothing And (.FchCreated <> .FchLastEdited)
                If Edited Then
                    TextBoxStatus.Text = String.Format("Registrat per {0} el {1} i modificat per {2} el {3}", BLL.BLLUser.NicknameOrElse(.UsrCreated), Format(.FchCreated, "dd/MM/yy HH:mm"), BLL.BLLUser.NicknameOrElse(.UsrLastEdited), Format(.FchLastEdited, "dd/MM/yy HH:mm"))
                Else
                    TextBoxStatus.Text = String.Format("Registrat per {0} el {1}", BLL.BLLUser.NicknameOrElse(.UsrCreated), Format(.FchCreated, "dd/MM/yy HH:mm"))
                End If
            End If
        End With
    End Sub

    Private Sub ToolStripButtonView_Click(sender As Object, e As EventArgs) Handles ToolStripButtonView.Click
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
                        Dim items As List(Of DTODeliveryItem) = BLL.BLLDeliveryItems.FromPurchaseOrder(_PurchaseOrder)
                        _ViewSortides = New Xl_PurchaseOrderDeliveryItems
                        _ViewSortides.Load(items)
                        PanelItems.Controls.Add(_ViewSortides)
                        _ViewSortides.Dock = DockStyle.Fill
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
    End Sub

    Private Sub ToolStripButtonServirTotJunt_Click(sender As Object, e As EventArgs) Handles ToolStripButtonServirTotJunt.Click
        If _AllowEvents Then
            If ToolStripButtonServirTotJunt.Checked Then
                ToolStripButtonServirTotJunt.Image = My.Resources.Checked13
            Else
                ToolStripButtonServirTotJunt.Image = My.Resources.UnChecked13
            End If
        End If

    End Sub

    Private Sub ToolStripButtonPot_Click(sender As Object, e As EventArgs) Handles ToolStripButtonPot.Click
        If _AllowEvents Then
            If ToolStripButtonPot.Checked Then
                ToolStripButtonPot.Image = My.Resources.Checked13
            Else
                ToolStripButtonPot.Image = My.Resources.UnChecked13
            End If
        End If
    End Sub

    Private Sub ButtonExcel_Click(sender As Object, e As EventArgs) Handles ButtonExcel.Click
        Dim oLang As DTOLang = _PurchaseOrder.Customer.Lang
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Filter = "Excel (*.xlsx)|*.xlsx"
            .DefaultExt = ".xlsx"
            .FileName = oLang.Tradueix("Pedido ", "Comanda ", "Order ") & " " & _PurchaseOrder.Id & " de " & _PurchaseOrder.Customer.FullNom & ".xlsx"
            If .ShowDialog Then
                If .FileName > "" Then
                    Try
                        Dim oWorkbook As ClosedXML.Excel.XLWorkbook = BLL.BLLPurchaseOrder.Excel(_PurchaseOrder)
                        oWorkbook.SaveAs(.FileName)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End If
        End With
    End Sub
End Class