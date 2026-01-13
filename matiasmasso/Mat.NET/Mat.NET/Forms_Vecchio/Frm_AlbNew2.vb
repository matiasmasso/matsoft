

Public Class Frm_AlbNew2
    Private mAlb As Alb = Nothing
    Private mImportacio As Importacio = Nothing
    Private mObs As String = ""
    Private mCustDoc As String = ""
    Private mFpg As fpg = Nothing
    Private mMgz As Mgz = Nothing
    Private mTrpDataSource As DataTable
    Private mCashCod As DTO.DTOCustomer.CashCodes
    Private mDirtyItems As Boolean = False
    Private mWarnAlbs As Boolean = False
    Private mExemptIva As Boolean = False
    Private mShowTot As Boolean = False
    Private mDiposit As Boolean = False
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        LinCod
        Pnc
        Art
        Dsc
        Ico
        Out
        Pvp
        Dto
        Amt
        Stk
        Pn2
        Prv
    End Enum

    Public Sub New(oImportacio As Importacio)
        MyBase.New()
        Me.InitializeComponent()
        mImportacio = oImportacio
        mAlb = mImportacio.Proveidor.NewAlb
        Me.Text = GetTitle(mAlb.Cod, mAlb.Id, mAlb.Client.Clx, mImportacio)
        refresca()
    End Sub

    Public Sub New(ByVal oAlb As Alb)
        MyBase.New()
        Me.InitializeComponent()
        mAlb = oAlb
        Me.Text = GetTitle(mAlb.Cod, mAlb.Id, mAlb.Client.Clx)


        refresca()
    End Sub

    Private Sub refresca()
        With mAlb
            Xl_Contact_Logo1.Contact = .Client
            If .AllowDelete Then
                ButtonDel.Enabled = True
            End If

            mFpg = .Fpg
            mMgz = .Mgz

            mObs = .Obs
            mCustDoc = .CustomerDocURL
            DisplayStatusObs()

            Xl_Alb_LineItems1.Alb = mAlb

            Select Case .Cod
                Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.traspas
                    If .Client.Impagat Then TextBoxNom.BackColor = maxisrvr.COLOR_NOTOK
                Case DTOPurchaseOrder.Codis.proveidor
                    ComboBoxTrp.Visible = False
                    ComboBoxPorts.Visible = False
                    CheckBoxValorat.Visible = False
                    CheckBoxFacturable.Visible = False
                    LabelLastAlb.Visible = False

                    ToolStripButtonCustomDoc.Visible = False
            End Select

            If .IsNew Then
                If Not Xl_Alb_LineItems1.IsEmpty Then
                    mDirtyItems = True
                    ButtonOk.Enabled = True
                End If

                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                        If mAlb.Client.CashCod = DTO.DTOCustomer.CashCodes.TransferenciaPrevia Or mAlb.Client.CashCod = DTO.DTOCustomer.CashCodes.Visa Then
                            'ComboBoxPorts.SelectedIndex = DTO.DTOCustomer.PortsCodes.Altres
                            ToolStripButtonFra.Visible = False
                        Else
                            If mAlb.CashCod = DTO.DTOCustomer.CashCodes.credit Then
                                If Not mAlb.Client.EFrasEnabled Then
                                    Me.BackColor = maxisrvr.COLOR_NOTOK
                                    MsgBox("No té cap email habilitat per rebre les factures!")
                                End If
                            End If

                        End If
                    Case Else
                        ToolStripButtonFra.Visible = False
                End Select

                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.traspas
                        If .Client.WarnAlbs > "" Then
                            MsgBox(.Client.WarnAlbs, MsgBoxStyle.Information, "MAT.NET")
                            mWarnAlbs = True
                        End If

                        If .Client.CcxOrMe.NIF.Trim = "" Then
                            MsgBox("Falta NIF!", MsgBoxStyle.Exclamation, "MAT.NET")
                        End If


                        'If .SubscriptorsExist Then
                        ' With CheckBoxEalbs
                        ' .Visible = True
                        ' .Checked = (mAlb.Id = 0)
                        ' End With
                        ' End If
                End Select


                CheckBoxRecycle.Visible = True
                CheckBoxRecycle.Enabled = LoadRecicleNums()
                SetEmailLabel()
                ToolStripButtonTransmisio.Visible = False


                SetLastAlb()
            Else
                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.client
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

                Select Case .Cod
                    Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                        If mFpg.Modalitat = fpg.Modalitats.NotSet Then
                            ToolStripButtonFpg.Image = My.Resources.UnChecked13
                        Else
                            ToolStripButtonFpg.Image = My.Resources.Checked13
                        End If

                        If mAlb.Fra Is Nothing Then
                            ToolStripButtonFra.Text = "pendent de facturar"
                        Else
                            ToolStripButtonFra.Text = "fra " & mAlb.Fra.Id & " del " & mAlb.Fra.Fch.ToShortDateString
                        End If
                    Case DTOPurchaseOrder.Codis.traspas
                        ToolStripButtonFra.Visible = False
                        mDiposit = True
                    Case Else
                        ToolStripButtonFra.Visible = False
                End Select

                LabelLastAlb.Visible = False
                LabelEmail.Visible = False

            End If

            mExemptIva = .IvaExempt
            ToolStripButtonDiposit.Image = IIf(mDiposit, My.Resources.Checked13, My.Resources.UnChecked13)

            SetFormaDePago()
            ShowCredit()

            DateTimePickerFch.Value = .Fch

            If mMgz IsNot Nothing Then
                If Not mMgz.Guid.Equals(System.Guid.Empty) Then 'to deprecate quan ho fitri a alb.setitm
                    Xl_ContactMgz.Contact = mMgz
                End If
            End If

            If .PortsCod > ComboBoxPorts.Items.Count - 1 Then
                ComboBoxPorts.SelectedIndex = DTO.DTOCustomer.PortsCodes.Altres
                MsgBox("codi ports " & .PortsCod & " no existent. Canviat a altres", MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                ComboBoxPorts.SelectedIndex = .PortsCod
                SetPortsVisibility()
                If .IsNew And .PortsCod = DTO.DTOCustomer.PortsCodes.Reculliran Then
                    SetReculliran()
                End If

            End If

            CheckBoxValorat.Checked = .Valorado
            CheckBoxFacturable.Checked = .Facturable

            TextBoxNom.Text = .Nom
            TextBoxAdr.Text = .Adr
            Xl_CitPais1.Zip = .Zip
            TextBoxTel.Text = .Tel

            LoadTrps()
            Select Case .Cod
                Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                    SetTrp(.Transportista)
            End Select

        End With

        mAllowEvents = True

    End Sub

    Private Sub SetEmailLabel()
        Dim oEmails As Emails = mAlb.Client.GetSubscribedEmails(DTOSubscription.Ids.ConfirmacioEnviament)
        If oEmails.Count = 0 Then
            oEmails = mAlb.Client.Emails
        End If
        If oEmails.Count > 0 Then
            LabelEmail.Text = oEmails(0).Adr
            If oEmails(0).BadMail Then LabelEmail.BackColor = maxisrvr.COLOR_NOTOK
        End If
    End Sub

    Private Sub SetReculliran()
        SetTrp(Nothing)
        TextBoxAdr.Text = "recogerán en almacén"
        Xl_CitPais1.Zip = Xl_ContactMgz.Contact.Adr.Zip
    End Sub

    Private Function GetTitle(ByVal oCod As DTOPurchaseOrder.Codis, ByVal iNumeroDocument As Integer, ByVal sNom As String, Optional oImportacio As Importacio = Nothing) As String
        Dim s As String = ""

        If iNumeroDocument = 0 Then
            Select Case oCod
                Case DTO.DTOPurchaseOrder.Codis.client
                    s = "NOU ALBARA DE " & sNom
                Case DTO.DTOPurchaseOrder.Codis.proveidor
                    s = "ENTRADA MERCANCIA DE " & sNom
                    If oImportacio IsNot Nothing Then
                        s = s & " (" & oImportacio.Id.ToString & "/" & oImportacio.Yea.ToString & ") "
                    End If
                Case DTOPurchaseOrder.Codis.reparacio
                    s = "REPARACIO SERVEI TECNIC " & sNom
                Case DTOPurchaseOrder.Codis.traspas
                    s = "NOTA DE TRASPAS DE MAGATZEM"
            End Select
        Else
            Select Case oCod
                Case DTO.DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                    s = "ALBARA " & iNumeroDocument & " DE " & sNom
                Case DTO.DTOPurchaseOrder.Codis.proveidor
                    s = "ENTRADA " & iNumeroDocument & " "
                    If oImportacio Is Nothing Then
                        oImportacio = ImportacioLoader.FromAlb(mAlb)
                    End If
                    If oImportacio IsNot Nothing Then
                        s = s & "(" & oImportacio.Id.ToString & "/" & oImportacio.Yea.ToString & ") "
                    End If

                    s = s & "DE " & sNom
                Case DTOPurchaseOrder.Codis.traspas
                    s = "NOTA " & iNumeroDocument & " DE TRASPAS DE MAGATZEM"
            End Select
        End If

        Return s
    End Function


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mAlb

            If .IsNew Then
                If CheckBoxRecycle.Checked Then
                    If Not mAlb.RecycleId(RecycleNum(), DateTimePickerFch.Value) Then
                        MsgBox("el numero " & RecycleNum().ToString & " ja está ocupat" & vbCrLf & "cal escullir-ne un altre o desactivar el reciclatje de numero", MsgBoxStyle.Exclamation, "MAT.NET")
                        Exit Sub
                    End If
                End If

                'retenció per forma de pagament
                Dim oSuma As maxisrvr.Amt = Xl_Alb_LineItems1.Items.Suma
                If oSuma.IsPositive Then
                    Select Case mCashCod
                        Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                            .RetencioCod = DTODelivery.RetencioCods.Transferencia
                        Case DTO.DTOCustomer.CashCodes.Visa
                            .RetencioCod = DTODelivery.RetencioCods.VISA
                    End Select
                End If
            End If

            .Fch = DateTimePickerFch.Value
            .Nom = TextBoxNom.Text
            .Adr = TextBoxAdr.Text
            .Zip = Xl_CitPais1.Zip
            .Tel = TextBoxTel.Text

            If mDiposit Then
                .Cod = DTOPurchaseOrder.Codis.traspas
            Else
                .Facturable = CheckBoxFacturable.Checked
            End If

            Select Case .Cod
                Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                    .Transportista = CurrentTrp()
            End Select

            .IvaExempt = mExemptIva

            .Valorado = CheckBoxValorat.Checked

            .Obs = ToolStripStatusLabelObs.Text
            If Xl_ContactMgz.Contact IsNot Nothing Then
                .Mgz = New Mgz(Xl_ContactMgz.Contact.Guid)
            End If
            .PortsCod = CurrentPorts()

            .Fpg = mFpg

            If .IvaExempt Then
                .IvaBaseQuotas = New maxisrvr.IvaBaseQuotas
            End If

            .CustomerDocURL = mCustDoc

            '.IvaStdPct = Xl_Totals1.IVA
            '.IvaReqPct = Xl_Totals1.Req
            '.Recarrec = Xl_Totals1.Suplemento

            .CashCod = mCashCod
            .PortsCod = CurrentPorts()
            .Plataforma = .Client.DeliveryPlatform

            ' If CheckBoxCobrat.Checked Then
            '.FchCobroReembolso = DateTimePickerCobrat.Value
            'Else
            '.FchCobroReembolso = Date.MinValue
            'End If

            .Itms = Xl_Alb_LineItems1.Items


            'Load transport
            Select Case .Cod
                Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                    If .Transportista Is Nothing Then
                        Dim DcM3 As Decimal = .Itms.M3
                        Dim oZona As Zona = .DeliveryZip.Location.Zona
                        .Transportista = App.Current.emp.GetTransportista(DcM3, oZona, CurrentPorts, mCashCod)
                    Else
                        .Transportista = CurrentTrp()
                    End If
            End Select


            If CheckValidationErrs() Then

                Dim BlWasNew As Boolean = .IsNew
                Dim exs as New List(Of exception)
                If BlWasNew And mImportacio IsNot Nothing Then
                    .SetUser(BLL.BLLSession.Current.User)
                    mImportacio.UpdateAlbaraDeEntrada(mAlb, exs)
                Else
                    .SetUser(BLL.BLLSession.Current.User)
                    .Update(exs)
                End If

                If exs.Count = 0 Then

                    RaiseEvent AfterUpdate(mAlb, MatEventArgs.Empty)

                    If BlWasNew Then
                        Select Case CType(ComboBoxPorts.SelectedIndex, DTO.DTOCustomer.PortsCodes)
                            Case DTO.DTOCustomer.PortsCodes.Altres
                                'MailHelper.SendMail("victoria@matiasmasso.es", , , "ALBARA " & .Id & " EN ALTRES (" & .Nom & ")")
                            Case DTO.DTOCustomer.PortsCodes.Reculliran
                                'MailHelper.SendMail("victoria@matiasmasso.es", , , "ALBARA " & .Id & " EN RECULLIRAN (" & .Nom & ")")
                        End Select

                        'If CheckBoxEalbs.Checked Then
                        ' mAlb.MailToSubscriptors()
                        'End If

                        SendUsrEvts()

                        Dim s As String = "albará nº " & .Id
                        If mAlb.Cod = DTOPurchaseOrder.Codis.client Then
                            Select Case .PortsCod
                                Case DTO.DTOCustomer.PortsCodes.Reculliran
                                    s = s & vbCrLf & "recullirán"
                                Case DTO.DTOCustomer.PortsCodes.Altres
                                    s = s & " fora de transmisió"
                                Case Else
                                    If .Transportista Is Nothing Then
                                        s = s & vbCrLf & " no s'ha pogut assignar transportista"
                                    Else
                                        s = s & " enviat per " & vbCrLf & .Transportista.Abr
                                    End If
                            End Select
                        End If

                        MsgBox(s, MsgBoxStyle.Information, "MAT.NET")
                    End If
                    Me.Close()
                Else
                    MsgBox("error al desar l'albará " & mAlb.Id & ":" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            End If
        End With
    End Sub


    Private Function CheckValidationErrs() As Boolean
        Dim RetVal As Boolean = False

        mAlb.ValidationErrors = mAlb.CheckValidationErrors
        If mAlb.ValidationErrors.Count = 0 Then
            RetVal = True
        Else
            RetVal = UIHelper.WarnOrPassError(mAlb.ValidationErrors)
        End If
        Return RetVal
    End Function

    Private Function RecycleNum() As Long
        Dim LngId As Long
        If CheckBoxRecycle.Checked And IsNumeric(ComboBoxNum.Text) Then
            LngId = ComboBoxNum.Text
            Dim SQL As String = "SELECT NOM FROM ALB WHERE " _
            & "EMP=" & mAlb.Emp.Id & " AND " _
            & "YEA=" & DateTimePickerFch.Value.Year & " AND " _
            & "ALB=" & LngId
            Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
            If oDrd.Read Then
                MsgBox("el número " & LngId & " ja ha estat assignat a " & oDrd("NOM") & "." & vbCrLf & "Torna a seleccionar un altre", MsgBoxStyle.Exclamation, "MAT.NET NOU ALBARÁ")
                If LoadRecicleNums() Then
                    ComboBoxNum.Visible = True
                Else
                    LngId = 0
                    CheckBoxRecycle.Checked = False
                    CheckBoxRecycle.Enabled = False
                    ComboBoxNum.Visible = False
                End If
            End If
            oDrd.Close()
        End If
        Return LngId
    End Function

    Private Sub Xl_Alb_LineItems1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Alb_LineItems1.AfterUpdate
        If mAllowEvents Then
            mDirtyItems = True
            SetDirty()
            SetKg()

            If mAlb.IsNew Then
                Select Case mAlb.Cod
                    Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                        If mCashCod = DTO.DTOCustomer.CashCodes.credit Then SetCreditDetails()
                End Select
            End If
        End If

    End Sub

    Private Sub SetDirty()
        Dim AllowSave As Boolean = True
        If mAlb.IsNew Then
            If Xl_Alb_LineItems1.IsEmpty Then AllowSave = False
        End If
        ButtonOk.Enabled = AllowSave
    End Sub

    Private Sub Control_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePickerFch.ValueChanged
        SetDirty()
        Xl_Alb_LineItems1.SetFch(DateTimePickerFch.Value)
    End Sub

    Private Sub ToolStripButtonObs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonObs.Click
        Dim s As String = InputBox("Observacions:", Me.Text, mObs)
        If s <> mObs Then
            mObs = s
            DisplayStatusObs()
            SetDirty()
        End If
    End Sub

    Private Sub ToolStripButtonCustomDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonCustomDoc.Click
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

    Private Function LoadRecicleNums() As Boolean
        Dim retval As Boolean
        Dim DtFch As Date = DateTimePickerFch.Value
        Dim SQL As String = "SELECT TOP 200 ALB,FCH,TRANSM FROM ALB WHERE EMP=@EMP ORDER BY YEA DESC, ALB DESC"
        Dim i As Long
        ComboBoxNum.Items.Clear()
        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@EMP", mAlb.Emp.Id)
        Do While oDrd.Read
            If oDrd("TRANSM") > 0 Then Exit Do
            Dim tmpFch As Date = CDate(oDrd("FCH"))
            If Not (tmpFch.Year = DtFch.Year And tmpFch.DayOfYear = DtFch.DayOfYear) Then Exit Do

            If i = 0 Then
                i = oDrd("ALB")
            Else
                i = i - 1
                Do While oDrd("ALB") < i
                    ComboBoxNum.Items.Add(i)
                    i = i - 1
                Loop
            End If
        Loop
        oDrd.Close()

        If ComboBoxNum.Items.Count > 0 Then
            ComboBoxNum.SelectedIndex = 0
            retval = True
        End If
        Return retval
    End Function

    Private Function GetLastAlb() As Alb
        Dim retval As Alb = Nothing

        Dim SQL As String = "SELECT TOP 1 Guid,YEA,ALB,FCH FROM ALB WHERE " _
        & "CliGuid = @CliGuid AND " _
        & "COD=" & DTOPurchaseOrder.Codis.client & " " _
        & "ORDER BY YEA DESC, ALB DESC"

        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@CliGuid", mAlb.Client.Guid.ToString)
        If oDrd.Read Then
            Dim oGuid As Guid = oDrd("Guid")
            retval = New Alb(oGuid)
        End If
        oDrd.Close()

        Return retval
    End Function

    Private Sub SetLastAlb()
        Dim oLastAlb As Alb = GetLastAlb()
        With LabelLastAlb
            If oLastAlb Is Nothing Then
                .Text = "primer albará"
                .BackColor = maxisrvr.COLOR_NOTOK
            Else
                Dim DtFch As Date = oLastAlb.Fch
                .Text = "ultim albará del " & DtFch.ToShortDateString
                If DtFch.ToShortDateString = DateTimePickerFch.Value.ToShortDateString Then
                    .BackColor = maxisrvr.COLOR_NOTOK
                End If
            End If

        End With
    End Sub

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

    Private Sub LabelLastAlb_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelLastAlb.DoubleClick
        Dim oFrm As New Frm_AlbNew2(GetLastAlb)
        oFrm.Show()
    End Sub


    Private Function CurrentPorts() As DTO.DTOCustomer.PortsCodes
        Return CType(ComboBoxPorts.SelectedIndex, DTO.DTOCustomer.PortsCodes)
    End Function

    Private Sub ComboBoxPorts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPorts.SelectedIndexChanged
        If mAllowEvents Then
            SetPortsVisibility()
            CheckWarning()
            SetDirty()
        End If
    End Sub

    Private Sub SetPortsVisibility()
        Static oLastCod As DTO.DTOCustomer.PortsCodes
        Dim oNewCod As DTO.DTOCustomer.PortsCodes = CurrentPorts()
        Select Case CurrentPorts()
            Case DTO.DTOCustomer.PortsCodes.Altres
                SetTrp(Nothing)
                ComboBoxTrp.Visible = False
                ComboBoxTrp.SelectedIndex = 0
            Case DTO.DTOCustomer.PortsCodes.Reculliran
                SetReculliran()
                ComboBoxTrp.Visible = False
                ComboBoxTrp.SelectedIndex = 0
            Case DTO.DTOCustomer.PortsCodes.EntregatEnMa
                ComboBoxTrp.Visible = False
                ComboBoxTrp.SelectedIndex = 0
            Case Else
                ComboBoxTrp.Visible = True
                If oLastCod = DTO.DTOCustomer.PortsCodes.Reculliran Then
                    'restaura adreça d'entrega
                    TextBoxAdr.Text = mAlb.Adr
                    Xl_CitPais1.Zip = mAlb.Zip
                    TextBoxTel.Text = mAlb.Tel
                End If
        End Select
        oLastCod = oNewCod
    End Sub

    Private Sub LoadTrps()
        Dim BlOnlyActive As Boolean = (mAlb.Id = 0)
        Dim oTransportistas As List(Of DTOTransportista) = BLL_Transportistas.All(BLL.BLLApp.Emp, BlOnlyActive)
        Dim oNullTrp As New DTOTransportista
        With oNullTrp
            .Abr = "(transportista)"
        End With
        oTransportistas.Insert(0, oNullTrp)

        With ComboBoxTrp
            .DataSource = oTransportistas
            .DisplayMember = "Abr"
            '.ValueMember = "Guid"
        End With
    End Sub

    Private Function CurrentTrp() As Transportista
        Dim retval As Transportista = Nothing
        If ComboBoxTrp.SelectedIndex > 0 Then
            Dim oTransportista As DTOTransportista = ComboBoxTrp.SelectedItem
            retval = New Transportista(oTransportista.Guid)
            retval.Abr = oTransportista.Abr
        End If
        Return retval
    End Function

    Private Sub SetTrp(ByVal oTrp As Transportista)
        If ComboBoxTrp.Items.Count = 0 Then LoadTrps()
        If oTrp Is Nothing Then
            ComboBoxTrp.SelectedIndex = 0
        Else
            'ComboBoxTrp.SelectedValue = oTrp.Guid
            Dim oTransportistas As List(Of DTOTransportista) = ComboBoxTrp.DataSource
            ComboBoxTrp.SelectedItem = oTransportistas.Find(Function(x) x.Guid.Equals(oTrp.Guid))
        End If
    End Sub


    Private Sub SendUsrEvts()
        Dim oUsr As Contact
        Dim SQL As String = "SELECT USR FROM USREVT WHERE " _
        & "EMP=" & mAlb.Emp.Id & " AND " _
        & "ID=" & mAlb.Client.Id
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Do While oDrd.Read
            oUsr = MaxiSrvr.Contact.FromNum(mAlb.Emp, oDrd("USR"))
            BLL.MailHelper.SendMail(oUsr.Email, , , "AVIS DE ALBARA A " & mAlb.Client.Clx, "albará " & mAlb.Id & mAlb.UserText(BLL.BLLSession.Current.User.Lang))
        Loop
        oDrd.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem l'albará " & mAlb.Id.ToString & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim sErr As String = mAlb.Delete()
            If sErr > "" Then
                MsgBox("Aquest albará no es pot eliminar:" & vbCrLf & sErr, MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Frm_PurchaseOrder_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim exs As New List(Of Exception)
        Dim oCustomer As New DTOCustomer(mAlb.Client.Guid)
        If BLL.BLLAlbBloqueig.BloqueigEnd(BLL.BLLSession.Current.User, oCustomer, DTOAlbBloqueig.Codis.ALB, exs) Then
        Else
            e.Cancel = True
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ToolStripButtonTransmisio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonTransmisio.Click
        If mAlb.Transmisio.Id = 0 Then
            Dim oFrmNew As New Frm_Transmisio_New(mAlb.Mgz)
            oFrmNew.Show()
        Else
            Dim oFrm As New Frm_Transmisio_New(mAlb.Transmisio)
            oFrm.Show()
        End If
    End Sub

    Private Sub ToolStripButtonFra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFra.Click
        If mAlb.Fra Is Nothing Then
            Dim oAlbs As New Albs
            oAlbs.Add(mAlb)
            root.ExeFacturacio(oAlbs)
        Else
            Dim oFrm As New Frm_Fra(mAlb.Fra)
            oFrm.Show()
        End If
    End Sub


#Region "Toolbar"

    Private Sub SetKg()
        Dim oItems As LineItmArcs = Xl_Alb_LineItems1.Items
        Dim iBultos As Integer = mAlb.Bultos

        Dim sb As New System.Text.StringBuilder
        If iBultos > 0 Then sb.Append(iBultos.ToString & " bts ")
        If oItems.Kg > 0 Then sb.Append(oItems.Kg & " Kg ")
        If oItems.M3 > 0 Then sb.Append(oItems.M3 & " m3 ")

        ToolStripLabelKg.Text = sb.ToString
    End Sub

    Private Sub ShowCredit()
        Select Case mAlb.Cod
            Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                SetCredit(mAlb.CashCod)
            Case Else
                ToolStripSplitButtonCredit.Visible = False
        End Select
    End Sub

    Private Sub SetCredit(ByVal oCashCod As DTO.DTOCustomer.CashCodes)
        mCashCod = oCashCod
        Select Case mCashCod
            Case DTO.DTOCustomer.CashCodes.credit
                SetCreditDetails()
            Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                ToolStripSplitButtonCredit.Text = "transf.previa"
                ToolStripSplitButtonCredit.Image = My.Resources.cash
                If mAlb.IsNew Then ButtonOk.Image = My.Resources.cash
                ToolStripButtonFpg.Visible = False
            Case DTO.DTOCustomer.CashCodes.Visa
                ToolStripSplitButtonCredit.Text = "VISA"
                ToolStripSplitButtonCredit.Image = My.Resources.cash
                If mAlb.IsNew Then ButtonOk.Image = My.Resources.cash
                ToolStripButtonFpg.Visible = False
            Case DTO.DTOCustomer.CashCodes.Reembols
                ToolStripSplitButtonCredit.Text = "reembols"
                ToolStripSplitButtonCredit.Image = My.Resources.reembols
                If mAlb.IsNew Then ButtonOk.Image = My.Resources.reembols
                ToolStripButtonFpg.Visible = False
        End Select
    End Sub

    Private Sub SetCreditDetails()
        Dim BlShowCreditAvailable As Boolean = False
        If mAlb.IsNew Then
            Select Case mAlb.Cod
                Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                    BlShowCreditAvailable = True
            End Select
        End If

        If BlShowCreditAvailable Then
            Dim oCcx As Client = mAlb.Client.CcxOrMe
            Dim oItems As LineItmArcs = Xl_Alb_LineItems1.Items

            Dim oTotal As MaxiSrvr.Amt = oItems.Total(mAlb.DtoPct, mAlb.DppPct, mAlb.Export, oCcx.REQ, mAlb.Fch)
            Dim oCreditDisponible As MaxiSrvr.Amt = oCcx.CreditDisponible
            oCreditDisponible.Substract(oTotal)

            ToolStripSplitButtonCredit.Text = "credit " & oCreditDisponible.CurFormat
            If oCreditDisponible.IsNegative Then
                ToolStripSplitButtonCredit.Image = My.Resources.warn
                'ButtonOk.Image = My.Resources.warn
                ButtonOk.BackColor = MaxiSrvr.COLOR_NOTOK
                ToolStripSplitButtonCredit.ForeColor = Color.Red
                ToolStripSplitButtonCredit.Font = New Font(ToolStripSplitButtonCredit.Font, FontStyle.Bold)
            Else
                'ButtonOk.Image = Nothing
                ButtonOk.BackColor = ButtonCancel.BackColor
                ToolStripSplitButtonCredit.Image = My.Resources.CreditCard
                ToolStripSplitButtonCredit.ForeColor = Color.Navy
                ToolStripSplitButtonCredit.Font = New Font(ToolStripSplitButtonCredit.Font, FontStyle.Regular)
            End If

            ToolStripButtonFpg.Visible = True
        Else
            ToolStripSplitButtonCredit.Text = "credit"
            ToolStripSplitButtonCredit.Image = My.Resources.CreditCard
            ToolStripButtonFpg.Visible = True
        End If
    End Sub

    Private Sub SetFormaDePago()
        Select Case mAlb.Cod
            Case DTOPurchaseOrder.Codis.client, DTOPurchaseOrder.Codis.reparacio
                Select Case mCashCod
                    Case DTO.DTOCustomer.CashCodes.credit
                        SetCreditDetails()
                        ToolStripButtonFpg.Visible = True

                        If mAlb.IsNew Then

                            Dim sWarnFpgs As String = ""
                            Dim oFpgs As New ArrayList
                            If CheckComandesAmbDiferentsFormesDePagament(sWarnFpgs, oFpgs) Then
                                MsgBox(sWarnFpgs, vbExclamation)
                            End If

                            If oFpgs.Count = 1 Then
                                If Not CType(oFpgs(0), fpg).CliDefault Then
                                    mFpg = oFpgs(0)
                                    Fpg_AfterUpdate(mFpg, EventArgs.Empty)
                                End If
                            End If
                        End If

                    Case Else
                        ToolStripButtonFpg.Visible = False
                End Select
        End Select
    End Sub

    Private Function CheckComandesAmbDiferentsFormesDePagament(ByRef sWarnText As String, ByRef oFpgs As ArrayList) As Boolean
        Dim retVal As Boolean = False
        Dim oPdc As Pdc = Nothing
        For Each oItem As LineItmArc In Xl_Alb_LineItems1.Items
            If Not oItem.Pnc.Pdc.Equals(oPdc) Then
                If oItem.Qty <> 0 Then
                    oPdc = oItem.Pnc.Pdc
                    Dim BlFpgExists As Boolean = False
                    For Each oFpg As fpg In oFpgs
                        If oFpg.Equals(oPdc.Fpg) Then
                            BlFpgExists = True
                            Exit For
                        End If
                    Next

                    If Not BlFpgExists Then
                        oFpgs.Add(oPdc.Fpg)
                        If Not oPdc.Fpg.CliDefault Then
                            If sWarnText > "" Then
                                sWarnText = sWarnText & vbCrLf
                            Else
                                sWarnText = "formes de pago especials:" & vbCrLf
                            End If
                            sWarnText = sWarnText & "comanda " & oPdc.Id & ": " & oPdc.Fpg.Text
                        End If
                    End If

                End If
            End If
        Next

        retVal = oFpgs.Count > 1
        Return retVal
    End Function


    Private Sub ToolStripButtonFpg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFpg.Click
        Dim oFrm As New Frm_Fpg(mFpg)
        AddHandler oFrm.AfterUpdate, AddressOf Fpg_AfterUpdate
        oFrm.Show()
    End Sub

    Private Sub ToolStripButtonExemptIva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExemptIva.Click
        SwitchCheckedToolStripButton(ToolStripButtonExemptIva, mExemptIva)
        Xl_Alb_LineItems1.SetIvaExempt(mExemptIva)
    End Sub

    Private Sub Fpg_AfterUpdate(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mFpg = sender

        If mFpg.Modalitat = fpg.Modalitats.NotSet Then
            ToolStripButtonFpg.Image = My.Resources.UnChecked13
        Else
            ToolStripButtonFpg.Image = My.Resources.Checked13
        End If
        SetDirty()
    End Sub


    Private Sub ToolStripMenuItemCredit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemCredit.Click
        SetCredit(DTO.DTOCustomer.CashCodes.credit)
    End Sub

    Private Sub ToolStripMenuItemReembols_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemReembols.Click
        SetCredit(DTO.DTOCustomer.CashCodes.Reembols)
    End Sub

    Private Sub ToolStripMenuItemTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemTransfer.Click
        SetCredit(DTO.DTOCustomer.CashCodes.TransferenciaPrevia)
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
        Dim oContactMgz As Contact = Xl_ContactMgz.Contact
        mMgz = New MaxiSrvr.Mgz(oContactMgz.Guid)
    End Sub

    Private Sub ToolStripButtonDiposit_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButtonDiposit.Click
        SwitchCheckedToolStripButton(ToolStripButtonDiposit, mDiposit)
    End Sub

    Private Sub SwitchCheckedToolStripButton(oToolStripButton As ToolStripButton, ByRef Value As Boolean)
        Value = Not Value
        oToolStripButton.Image = IIf(Value, My.Resources.Checked13, My.Resources.UnChecked13)
        SetDirty()
    End Sub

 
End Class