

Public Class Frm_Pdc_Proveidor
    Private mPdc As Pdc = Nothing
    Private _CurExchangeRate As DTOCurExchangeRate
    Private mDirtyPdd As Boolean = False
    Private mDirty As Boolean = False
    Private mAllowEvents As Boolean = False
    Private mFchMin As Date = Date.MinValue
    Private mObs As String = ""
    Private mCustDoc As String = ""
    Private mSortides As Xl_Pdc_Sortides = Nothing
    Private mAllowOpen As Boolean = True

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(oOrder As DTOPurchaseOrder)
        MyBase.New()
        Me.InitializeComponent()
        mPdc = MaxiSrvr.Pdc.FromProveidorDTO(oOrder)

        If mPdc.Id = 0 Then
            mPdc.UsrCreated = mPdc.UsrLastEdited
        End If

        refresca()

    End Sub
    Public Sub New(ByVal oPdc As Pdc)
        MyBase.New()
        Me.InitializeComponent()
        mPdc = oPdc
        mPdc.UsrLastEdited = BLLSession.Current.User

        If mPdc.Id = 0 Then
            mPdc.UsrCreated = mPdc.UsrLastEdited
        End If

        refresca()

    End Sub

    Public ReadOnly Property AllowOpen() As Boolean
        Get
            Return mAllowOpen
        End Get
    End Property

    Private Sub refresca()
        With mPdc
            Me.Text = GetTitle(.Cod, .Id, .Client.Clx)
            Xl_Cur1.Cur = mPdc.Cur
            DateTimePickerFch.Value = .Fch

            If .FchMin <> Nothing Then
                CheckBoxFchDelivery.Checked = True
                DateTimePickerDelivery.Visible = True
                DateTimePickerDelivery.Value = .FchMin
            End If

            Xl_Contact_Logo1.EnableChanges = True
            Xl_Contact_Logo1.Contact = .Client
            TextBoxPdd.Text = .Text
            TextBoxPdd.BackColor = .Client.BackColor

            ComboBoxDeliverTo.SelectedIndex = .DeliveryCod
            Dim oMgz As DTOMgz = .Mgz
            If oMgz Is Nothing Then oMgz = BLL.BLLApp.Mgz

            Xl_ContactMgz.Contact = New Contact(oMgz.Guid)
            Xl_ContactDeliverTo.Contact = .EntregarEn

            Xl_PdcSrc1.Load(.Source)
            Xl_Pdc_LineItems_Proveidor1.Pdc = mPdc
            Xl_DocFile1.Load(.DocFile)

            If mPdc.ExistPendents Then
                ButtonAlb.Enabled = True
            End If
            ButtonDel.Enabled = True

            mObs = .Obs
            mCustDoc = .CustomerDocURL
            DisplayStatusObs()

            If Not .Fpg.CliDefault Then
                ToolStripButtonFpg.Font = New Font(ToolStripButtonFpg.Font, FontStyle.Bold)
            End If

            Select Case .TotJunt
                Case True
                    'ToolStripButtonServirTotJunt.Checked = True
                    'ToolStripButtonServirTotJunt.Image = My.Resources.Checked13
                Case Else
                    'ToolStripButtonServirTotJunt.Checked = False
                    'ToolStripButtonServirTotJunt.Image = My.Resources.UnChecked13
            End Select

            Select Case .Pot
                Case True
                    'ToolStripButtonPot.Checked = True
                    'ToolStripButtonPot.Image = My.Resources.Checked13
                Case Else
                    'ToolStripButtonPot.Checked = False
                    'ToolStripButtonPot.Image = My.Resources.UnChecked13
            End Select

            mFchMin = .FchMin
            If mFchMin = Date.MinValue Then
                'ToolStripButtonFchMin.Text = "servei inmediat"
                'ToolStripButtonFchMin.Checked = False
            Else
                'ToolStripButtonFchMin.Text = "servei " & mFchMin.ToShortDateString
                'ToolStripButtonFchMin.Checked = True
            End If

            ButtonOk.Enabled = (.IsNew And .Itms.Count > 0)

        End With

    End Sub

    Private Function GetTitle(ByVal oCod As DTOPurchaseOrder.Codis, ByVal iNumeroDocument As Integer, ByVal sNom As String) As String
        Dim s As String = ""

        If iNumeroDocument = 0 Then
            Select Case oCod
                Case DTO.DTOPurchaseOrder.Codis.Client
                    s = "NOVA COMANDA DE " & sNom
                Case DTO.DTOPurchaseOrder.Codis.Proveidor
                    s = "NOVA COMANDA PER " & sNom
            End Select
        Else
            Select Case oCod
                Case DTO.DTOPurchaseOrder.Codis.Client
                    s = "COMANDA " & iNumeroDocument & " DE " & sNom
                Case DTO.DTOPurchaseOrder.Codis.Proveidor
                    s = "COMANDA " & iNumeroDocument & " A " & sNom
            End Select
        End If

        Return s
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Xl_Pdc_LineItems_Proveidor1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Pdc_LineItems_Proveidor1.AfterUpdate
        If mAllowEvents Then
            SetDirty()
            If Xl_Pdc_LineItems_Proveidor1.ExistPendents Then
                ButtonAlb.Enabled = True
            End If
        End If
    End Sub

    Private Sub TextBoxPdd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxPdd.KeyDown
        Dim s As String = TextBoxPdd.Text

        If mDirtyPdd Then
            Dim oClient As Client = mPdc.Client
            Dim oLang As DTOLang = oClient.Lang

            Select Case e.KeyCode
                Case Keys.Return, Keys.Tab
                    Dim oPdd As DTOPurchaseOrderConcepte = BLL.BLLPurchaseOrder.SearchConcepte(s)
                    If oPdd IsNot Nothing Then
                        TextBoxPdd.Text = oLang.Tradueix(oPdd.Esp, oPdd.Cat, oPdd.Eng)
                        TextBoxPdd.SelectionStart = TextBoxPdd.Text.Length
                        Xl_PdcSrc1.Load(oPdd.Src)
                    End If
            End Select
            mDirtyPdd = False
        End If

    End Sub

    Private Sub TextBoxPdd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxPdd.TextChanged

        If mAllowEvents Then
            If Xl_PdcSrc1.Source = DTO.DTOPurchaseOrder.Sources.no_Especificado Then
                Dim s As String = TextBoxPdd.Text.ToUpper
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
            mDirtyPdd = True
            If mPdc.Exists Then
                SetDirty()
            End If
        End If
    End Sub

    Private Sub ControlValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePickerFch.ValueChanged,
     Xl_PdcSrc1.AfterUpdate,
      Xl_Cur1.AfterUpdate,
        Xl_ContactDeliverTo.AfterUpdate

        If mAllowEvents Then
            If mPdc.Exists Then
                SetDirty()
            End If
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem la comanda?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
                Dim oPurchaseOrder As DTOPurchaseOrder = BLLPurchaseOrder.Find(mPdc.Guid)
                Dim exs As New List(Of Exception)
                If BLLPurchaseOrder.Delete(oPurchaseOrder, exs) Then
                    MsgBox("Comandes eliminades", MsgBoxStyle.Information, "M+O")
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(mPdc))
                    Me.Close()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)

        If Save(exs) Then
            If mPdc.IsNew Then
                RedactaPdf()
                Xl_DocFile1.Load(mPdc.DocFile)
                mPdc.Update(exs)
            Else
                Dim rc As MsgBoxResult = MsgBox("la comanda ha canviat. Tornem a generar el document pdf original?" & vbCrLf & "respondre SI quan li tonrnem a enviar al proveidor" & vbCrLf & "respondre NO si es tracta de un ajust posterior i volem conservar l'original tal com li vam enviar", MsgBoxStyle.YesNo, "MAT.NET")
                If rc = MsgBoxResult.Yes Then
                    RedactaPdf()
                    Xl_DocFile1.Load(mPdc.DocFile)
                    mPdc.Update(exs)
                End If
            End If

            RaiseEvent AfterUpdate(mPdc, MatEventArgs.Empty)
            MsgBox("Comanda " & mPdc.Id & " registrada correctament", MsgBoxStyle.Information, "MAT.NET")
            Me.Close()
        Else
            MsgBox("error al desar la comanda. Tornar a intentar-ho" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Function GetPdcFromForm(ByRef oPdc As Pdc, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Try
            Dim oCur As DTOCur = Xl_Cur1.Cur
            _CurExchangeRate = BLL.BLLCurExchangeRate.Closest(oCur)
            Dim oItms As LineItmPncs = Xl_Pdc_LineItems_Proveidor1.Itms
            If Not oCur.Equals(DTOCur.Eur) Then
                For Each oItm As LineItmPnc In oItms
                    If oItm.Preu Is Nothing Then
                        'exs.Add(New Exception(oItm.Art.Nom(Lang.CAT) & " no te preu"))
                    Else
                        Dim DcDivisa As Decimal = oItm.Preu.Eur
                        oItm.Preu = BLL.BLLCurExchangeRate.AmtFromDivisa(DcDivisa, _CurExchangeRate)
                    End If
                Next
            End If
            oPdc = mPdc
            With oPdc
                .Cur = Xl_Cur1.Cur
                .Fch = DateTimePickerFch.Value
                If CheckBoxFchDelivery.Checked Then
                    .FchMin = DateTimePickerDelivery.Value
                    If .IsNew Then
                        For Each item As LineItmPnc In oItms
                            item.FchConfirm = .FchMin
                        Next
                    End If
                Else
                    If .IsNew Then
                        exs.Add(New Exception("Cal especificar una data esperada de entrega"))
                    End If
                    .FchMin = Nothing
                End If
                .Source = Xl_PdcSrc1.Source
                .Text = TextBoxPdd.Text
                .Mgz = New DTOMgz(Xl_ContactMgz.Contact.Guid)
                .Obs = ToolStripStatusLabelObs.Text
                .DeliveryCod = CType(ComboBoxDeliverTo.SelectedIndex, Alb.DeliveryCods)
                .EntregarEn = Xl_ContactDeliverTo.Contact

                '.TotJunt = ToolStripButtonServirTotJunt.Checked
                '.Pot = ToolStripButtonPot.Checked
                .Itms = oItms
                .CustomerDocURL = mCustDoc
            End With

        Catch ex As Exception
            exs.Add(ex)
        End Try

        retval = exs.Count = 0
        Return retval
    End Function

    Private Function Save(exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = False

        If GetPdcFromForm(mPdc, exs) Then
            For Each oLine As LineItmPnc In mPdc.Itms
                If oLine.Qty = 0 Then
                    exs.Add(New Exception("quantitat zero detectada al demanar " & oLine.Art.Nom_ESP))
                End If
            Next

            If exs.Count > 0 Then
                UIHelper.WarnError(exs, "errors detectats al desar la comanda:")
            Else
                With mPdc
                    Dim BlIsNew As Boolean = .IsNew
                    .UsrLastEdited = BLLSession.Current.User


                    If Xl_DocFile1.IsDirty Then
                        .DocFile = Xl_DocFile1.Value
                    End If
                    .Update(exs)
                    retval = True
                End With

                retval = True
            End If

        Else
            UIHelper.WarnError(exs, "error al redactar la comanda")
        End If
        Return retval
    End Function

    Private Sub RedactaPdf()
        Dim exs As New List(Of exception)
        Dim oByteArray As Byte() = PdfStream(mPdc, mPdc.Cod = DTO.DTOPurchaseOrder.Codis.Proveidor)

        Dim oDocFile As DTODocFile = Nothing
        If BLLDocFile.LoadFromStream(oByteArray, oDocFile, exs) Then
            mPdc.DocFile = oDocFile
            If mPdc.Update(exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al redactar el document")
            End If
        Else
            UIHelper.WarnError(exs, "error al redactar el document")
        End If
    End Sub

    Public Function PdfStream(oPdc As Pdc, Optional ByVal BlSigned As Boolean = False, Optional ByVal BlProforma As Boolean = False) As Byte()
        Dim oPurchaseOrders As New List(Of DTOPurchaseOrder)
        Dim oPurchaseOrder As New DTOPurchaseOrder(oPdc.Guid)
        oPurchaseOrders.Add(oPurchaseOrder)
        Dim oPdf As New BLLPdfAlb(oPurchaseOrders, BlSigned, BlProforma)
        Dim retval As Byte() = oPdf.Stream
        Return retval
    End Function

    Private Sub ButtonAlb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAlb.Click
        If mDirty Then
            Dim exs As New List(Of Exception)
            If Save(exs) Then

                If mPdc.IsNew Then
                    RedactaPdf()
                    Xl_DocFile1.Load(mPdc.DocFile)
                    mPdc.Update(exs)
                Else
                    Dim rc As MsgBoxResult = MsgBox("la comanda ha canviat. Tornem a generar el document pdf original?" & vbCrLf & "respondre SI quan li tonrnem a enviar al proveidor" & vbCrLf & "respondre NO si es tracta de un ajust posterior i volem conservar l'original tal com li vam enviar", MsgBoxStyle.YesNo, "MAT.NET")
                    If rc = MsgBoxResult.Yes Then
                        RedactaPdf()
                        Xl_DocFile1.Load(mPdc.DocFile)
                        mPdc.Update(exs)
                    End If
                End If

                RaiseEvent AfterUpdate(mPdc, MatEventArgs.Empty)

                Dim oCustomer As DTOCustomer = mPdc.Client.ToDTO
                Dim oDelivery As DTODelivery = BLLDelivery.Factory(oCustomer, BLLSession.Current.User)
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                Me.Close()
                oFrm.Show()
            Else
                UIHelper.WarnError(exs, "error al desar la comanda:")
            End If
        End If

    End Sub


    Private Sub Frm_PdcNew_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim exs As New List(Of Exception)
        If BLL.BLLAlbBloqueig.BloqueigEnd(BLL.BLLSession.Current.User, New DTOContact(mPdc.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
        Else
            e.Cancel = True
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub SetDirty()
        mDirty = True
        ButtonOk.Enabled = True
    End Sub


    Private Sub Frm_PdcNew_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        mAllowEvents = True
        Xl_Pdc_LineItems_Proveidor1.AllowEvents = True

        TextBoxPdd.Focus()
    End Sub


    Private Sub ToolStripButtonObs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonObs.Click
        Dim s As String = InputBox("Observacions:", Me.Text, mObs)
        If s <> mObs Then
            mObs = s
            DisplayStatusObs()
            SetDirty()
        End If
    End Sub

    Private Sub ToolStripButtonCustomDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim s As String = InputBox("adreça web:", "DOCUMENT PER EL CONSUMIDOR", mCustDoc)
        Select Case s
            Case ""
                'ha clicat CANCEL 
            Case mCustDoc
                'ha clicat ACCEPTAR sense canviar res
            Case Else
                mCustDoc = s
                DisplayStatusObs()
                SetDirty()
        End Select
    End Sub

    Private Sub DisplayStatusObs()
        ToolStripStatusLabelObs.Text = mObs
        ToolStripStatusLabelObs.Visible = (mObs > "")
        ToolStripStatusLabelCustDoc.Text = mCustDoc
        ToolStripStatusLabelCustDoc.Visible = mCustDoc > ""
        StatusStripObs.Visible = (mObs > "" Or mCustDoc > "")
    End Sub


    Private Sub ToolStripButtonFchMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim DtDefault As Date = IIf(mFchMin = Date.MinValue, Today, mFchMin)
        Dim RetVal As String = ""
        Do Until IsDate(RetVal)
            RetVal = InputBox("No servir abans de:", Me.Text, DtDefault.ToShortDateString)
            If RetVal = "" Then Exit Do
        Loop

        If IsDate(RetVal) Then
            mFchMin = RetVal
            'ToolStripButtonFchMin.Text = "servei " & mFchMin.ToShortDateString
            'ToolStripButtonFchMin.Checked = True
        Else
            mFchMin = Date.MinValue
            'ToolStripButtonFchMin.Text = "servei inmediat"
            'ToolStripButtonFchMin.Checked = False
        End If
        SetDirty()
    End Sub


    Private Sub ToolStripButtonView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonView.Click
        With ToolStripButtonView
            Select Case .Checked
                Case False
                    .Text = "vista sortides"
                    Xl_Pdc_LineItems_Proveidor1.Visible = True
                    If mSortides IsNot Nothing Then
                        mSortides.Visible = False
                    End If
                Case True
                    .Text = "vista comanda"
                    Xl_Pdc_LineItems_Proveidor1.Visible = False
                    If mSortides Is Nothing Then
                        mSortides = New Xl_Pdc_Sortides
                        mSortides.Pdc = mPdc
                        PanelHost.Controls.Add(mSortides)
                        mSortides.Dock = DockStyle.Fill
                    End If

                    mSortides.Visible = True
            End Select
        End With
    End Sub

    Private Sub ToolStripButtonFpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFpg.Click
        Dim oFrm As New Frm_Fpg(mPdc.Fpg)
        AddHandler oFrm.AfterUpdate, AddressOf onFpgUpdate
        oFrm.Show()
    End Sub

    Private Sub onFpgUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFpg As fpg = sender

        mPdc.Fpg = oFpg
        Dim oFontStyle As FontStyle = IIf(oFpg Is Nothing, FontStyle.Regular, FontStyle.Bold)
        ToolStripButtonFpg.Font = New Font(ToolStripButtonFpg.Font, oFontStyle)
        SetDirty()
    End Sub

    Private Sub ButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExcel.Click
        Xl_Pdc_LineItems_Proveidor1.Excel()
    End Sub


    Private Sub ComboBoxDeliverTo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ComboBoxDeliverTo.SelectedIndexChanged
        If mAllowEvents Then
            Dim oDeliveryCod As Alb.DeliveryCods = CType(ComboBoxDeliverTo.SelectedIndex, Alb.DeliveryCods)
            Select Case oDeliveryCod
                Case Alb.DeliveryCods.Mgz
                    Xl_ContactDeliverTo.Contact = New Contact(mPdc.Mgz.Guid)
                Case Alb.DeliveryCods.Customer
                    Xl_ContactDeliverTo.Contact = mPdc.EntregarEn
            End Select

            If mPdc.Exists Then
                SetDirty()
            End If
        End If
    End Sub


    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        SetDirty()
    End Sub

    Private Sub CheckBoxFchDelivery_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchDelivery.CheckedChanged
        If mAllowEvents Then
            DateTimePickerDelivery.Visible = CheckBoxFchDelivery.Checked
            SetDirty()
        End If
    End Sub
End Class