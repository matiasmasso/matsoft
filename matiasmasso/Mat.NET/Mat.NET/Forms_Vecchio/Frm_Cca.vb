Public Class Frm_Cca
    Private _Cca As Cca

    Private mDs As DataSet
    Private mLastBlockedCcaYea As Integer
    Private mFchOriginal As Date = Date.MinValue
    Private mAllowUpdate As Boolean
    Private mLastValidatedObject As Object
    Private mDirtyCell As Boolean
    Private mFileDocument As FileDocument
    Private mDirtyFileDocument As Boolean
    Private mShowDivisas As Boolean
    Private mPgcPlan As PgcPlan = PgcPlan.FromToday

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        Cta
        CtaDsc
        CtaGuid
        ContactGuid
        Clx
        DebDiv
        DebCur
        DebEur
        HabDiv
        HabCur
        HabEur
    End Enum

    Public Sub New(oCca As DTOCca)
        MyBase.New()
        Me.InitializeComponent()

        _Cca = Cca.FromDTO(oCca)
        mFchOriginal = _Cca.fch
        Dim sLastBlockedCcaYea As String = BLL.BLLDefault.EmpValue(DTODefault.Codis.LastBlockedCcaYea)
        If IsNumeric(sLastBlockedCcaYea) Then
            mLastBlockedCcaYea = CInt(sLastBlockedCcaYea)
        End If
        UIHelper.LoadComboFromEnum(ComboBoxCcd, GetType(DTOCca.CcdEnum))
        Display()
        Refresca()
    End Sub

    Public Sub New(ByVal oCca As Cca)
        MyBase.New()
        Me.InitializeComponent()

        _Cca = oCca
        mFchOriginal = _Cca.fch
        Dim sLastBlockedCcaYea As String = BLL.BLLDefault.EmpValue(DTODefault.Codis.LastBlockedCcaYea)
        If IsNumeric(sLastBlockedCcaYea) Then
            mLastBlockedCcaYea = CInt(sLastBlockedCcaYea)
        End If
        UIHelper.LoadComboFromEnum(ComboBoxCcd, GetType(DTOCca.CcdEnum))
        Display()
        Refresca()
    End Sub

    Private Sub Frm_Cca_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        LoadGrid()
        CheckSum()
    End Sub

    Private Sub LoadGrid()
        Dim oTb As New DataTable
        With oTb
            .Columns.Add("CTA", System.Type.GetType("System.String"))
            .Columns.Add("CTADSC", System.Type.GetType("System.String"))
            .Columns.Add("CtaGuid", System.Type.GetType("System.Guid"))
            .Columns.Add("ContactGuid", System.Type.GetType("System.Guid"))
            .Columns.Add("CLX", System.Type.GetType("System.String"))
            .Columns.Add("DEBDIV", System.Type.GetType("System.Decimal"))
            .Columns.Add("DEBCUR", System.Type.GetType("System.String"))
            .Columns.Add("DEBEUR", System.Type.GetType("System.Decimal"))
            .Columns.Add("HABDIV", System.Type.GetType("System.Decimal"))
            .Columns.Add("HABCUR", System.Type.GetType("System.String"))
            .Columns.Add("HABEUR", System.Type.GetType("System.Decimal"))
        End With
        Dim oCcb As Ccb
        Dim oRow As DataRow
        For Each oCcb In _Cca.ccbs
            oRow = oTb.NewRow
            oRow(Cols.Cta) = oCcb.Cta.Id
            oRow(Cols.CtaDsc) = oCcb.Cta.Nom
            oRow(Cols.CtaGuid) = oCcb.Cta.Guid
            If oCcb.Contact IsNot Nothing Then
                oRow(Cols.ContactGuid) = oCcb.Contact.Guid
                oRow(Cols.Clx) = oCcb.Contact.Clx
            End If
            Select Case oCcb.Dh
                Case DTOCcb.DhEnum.Debe
                    oRow(Cols.DebDiv) = oCcb.Amt.Val
                    oRow(Cols.DebCur) = oCcb.Amt.Cur.Id
                    oRow(Cols.DebEur) = oCcb.Amt.Eur
                Case DTOCcb.DhEnum.Haber
                    oRow(Cols.HabDiv) = oCcb.Amt.Val
                    oRow(Cols.HabCur) = oCcb.Amt.Cur.Id
                    oRow(Cols.HabEur) = oCcb.Amt.Eur
            End Select
            oTb.Rows.Add(oRow)
            If oCcb.Amt.Cur.Id <> MaxiSrvr.Cur.Eur.Id And oCcb.Amt.Cur.Id <> "0" Then
                mShowDivisas = True
            End If
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.Cta)
                .HeaderText = "compte"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.CtaDsc)
                .HeaderText = ""
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DividerWidth = 0
                .ReadOnly = True
                '.tabstop = False
            End With
            With .Columns(Cols.CtaGuid)
                .Visible = False
            End With
            With .Columns(Cols.ContactGuid)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "subcompte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.DebDiv)
                .HeaderText = "debe"
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00;-#,###0.00;#"
            End With
            With .Columns(Cols.HabDiv)
                .HeaderText = "haber"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00;-#,###0.00;#"
            End With
            With .Columns(Cols.DebEur)
                .HeaderText = "debe"
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.HabEur)
                .HeaderText = "haber"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            CheckDivisas()
        End With

        mDs = New DataSet
        mDs.Tables.Add(oTb)
    End Sub

    Private Sub Display()
        With _Cca
            If _Cca.Id = 0 Then
                Me.Text = "NOU ASSENTAMENT"
                LabelUsr.Visible = False
                DateTimePicker1.MinDate = "1/1/" & CStr(mLastBlockedCcaYea + 1)
                ComboBoxCcd.SelectedValue = DTOCca.CcdEnum.Unknown
                mAllowUpdate = True
            Else
                Me.Text = "ASSENTAMENT Num." & .Id
                If _Cca.AuxCca > 0 Then
                    Me.Text = Me.Text & " (num." & _Cca.AuxCca & " del llibre diari)"
                End If
                LabelUsr.Text = _Cca.UsrTxt
                mAllowUpdate = Not _Cca.IsBlockedYear
                ButtonDel.Enabled = Not _Cca.IsBlockedYear
            End If
            ComboBoxCcd.SelectedValue = _Cca.Ccd
            TextBoxConcepte.Text = .Txt
            If .fch > DateTimePicker1.MinDate Then
                DateTimePicker1.Value = .fch
            End If
            If _Cca.ccbs.Count > 0 Then
                'Xl_Cur1.Cur = _Cca.ccbs(0).Amt.Cur
            Else
                'Xl_Cur1.Cur = maxisrvr.DefaultCur
            End If

            Xl_DocFile1.Load(.DocFile)
        End With

    End Sub

    Private Sub DataChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
     Handles TextBoxConcepte.TextChanged, _
     DateTimePicker1.TextChanged, _
      ComboBoxCcd.SelectedValueChanged

        If _AllowEvents Then
            EnableButtons()
        End If
    End Sub

    Private Sub EnableButtons()
        If _AllowEvents Then
            Dim BlEnable As Boolean = True
            If TextBoxConcepte.Text = "" Then BlEnable = False
            If TextBoxDif.Text <> "" Then BlEnable = False
            If DataGridView1.Rows.Count < 1 Then BlEnable = False
            If Not mAllowUpdate Then
                BlEnable = False
                ButtonDel.Enabled = False
            End If
            ButtonOk.Enabled = BlEnable
        End If
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If CheckIvaFch() Then

            Dim oTb As DataTable = mDs.Tables(0)
            Dim oRow As DataRow

            If Not mShowDivisas Then
                For Each oRow In oTb.Rows
                    oRow(Cols.DebCur) = "EUR"
                    oRow(Cols.DebDiv) = oRow(Cols.DebEur)
                    oRow(Cols.HabCur) = "EUR"
                    oRow(Cols.HabDiv) = oRow(Cols.HabEur)
                Next
            Else

                For Each oRow In oTb.Rows
                    If Not IsDBNull(oRow(Cols.DebEur)) Then
                        If IsDBNull(oRow(Cols.DebCur)) Then oRow(Cols.DebCur) = ""
                        If oRow(Cols.DebCur) = "" And oRow(Cols.DebEur) <> 0 Then
                            oRow(Cols.DebCur) = "EUR"
                            oRow(Cols.DebDiv) = oRow(Cols.DebEur)
                        End If
                    End If

                    If Not IsDBNull(oRow(Cols.HabEur)) Then
                        If IsDBNull(oRow(Cols.HabCur)) Then oRow(Cols.HabCur) = ""
                        If oRow(Cols.HabCur) = "" And oRow(Cols.HabEur) <> 0 Then
                            oRow(Cols.HabCur) = "EUR"
                            oRow(Cols.HabDiv) = oRow(Cols.HabEur)
                        End If

                    End If
                Next

            End If




            Dim oCcd As Ccd
            Dim oCcbBlock As CcbBlock
            Dim Str As String = ""
            Dim oPlan As PgcPlan = PgcPlan.FromYear(DateTimePicker1.Value.Year)
            Dim iYear As Integer = DateTimePicker1.Value.Year
            If iYear <= mLastBlockedCcaYea Then Str = "anys anteriors a " & mLastBlockedCcaYea + 1 & " bloqueijats"
            For Each oRow In oTb.Rows
                Dim oCtaGuid As Guid = oRow(Cols.CtaGuid)
                Dim oCta As PgcCta = New PgcCta(oCtaGuid)
                Dim oContactGuid As Guid
                Dim oContact As Contact = Nothing
                If IsDBNull(oRow(Cols.ContactGuid)) Then
                    oCcd = New Ccd(_Cca.emp, iYear, oCta)
                Else
                    oContactGuid = oRow(Cols.ContactGuid)
                    oContact = New Contact(oContactGuid)
                    oCcd = New Ccd(oContact, iYear, oCta)

                End If
                oCcbBlock = New CcbBlock(oCcd)
                If oCcbBlock.IsBlocked(DateTimePicker1.Value) Then
                    If Str > "" Then Str = Str & vbCrLf
                    Str = Str & "compte " & oCcd.Cta.FullNom & " bloqueijat fins a data " & oCcbBlock.Fch
                End If
            Next
            If Str > "" Then
                MsgBox(Str, MsgBoxStyle.Exclamation, "MAT.NET")
                Exit Sub
            End If

            With _Cca
                .Txt = TextBoxConcepte.Text
                .fch = DateTimePicker1.Value
                .Ccd = ComboBoxCcd.SelectedValue
                .ccbs = GetCcbsFromGrid()

                If Xl_DocFile1.IsDirty Then
                    .DocFile = Xl_DocFile1.Value
                End If

                Dim BlIsNew As Boolean = .IsNew
                Dim exs as New List(Of exception)
                If .Update( exs, root.Usuari) Then
                    If BlIsNew Then
                        MsgBox("registre nº " & .Id, MsgBoxStyle.Information, "MAT.NET")
                    End If
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cca))
                    Me.Close()
                Else
                    MsgBox("error al desar l'assentament:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If

            End With

        End If
    End Sub

    Private Function CheckIvaFch() As Boolean
        Dim RetVal As Boolean = True
        Dim BlWarn As Boolean = False

        If _Cca.IsNew Then
            If ExistQuotesIva() Then
                If IsAnteriorAUltimaDeclaracioIva() Then BlWarn = True
            End If
        Else
            If Not _Cca.Ccd = DTOCca.CcdEnum.IVA Then
                If AfectaAlIva() Then

                    If IvaJaDeclarat() Then
                        If HaCanviatElPeriodeDeclaracioIva() Then
                            BlWarn = True
                        ElseIf HanCanviatLesQuotesIva() Then
                            BlWarn = True
                        End If
                    End If
                End If
            End If
            End If

            If BlWarn Then
                MsgBox("No es permés de canviar l'Iva un cop liquidat", MsgBoxStyle.Exclamation, "MAT.NET")
                RetVal = False
            End If

            Return RetVal
    End Function

    Private Function AfectaAlIva() As Boolean
        Dim retval As Boolean = False

        Dim oPreviousCcbs As Ccbs = _Cca.ccbs
        Dim oUpdatedCcbs As Ccbs = GetCcbsFromGrid()

        If Not oPreviousCcbs.Equals(oUpdatedCcbs) Then
            Dim afectaAlNouAssentament As Boolean = False
            For Each oCcb As Ccb In oUpdatedCcbs
                If oCcb.Cta.IsQuotaIva Then
                    afectaAlNouAssentament = True
                    Exit For
                End If
            Next

            If afectaAlNouAssentament Then
                retval = True
            Else
                Dim afectaAlVellAssentament As Boolean = False
                For Each oCcb As Ccb In oPreviousCcbs
                    If oCcb.Cta.IsQuotaIva Then
                        afectaAlVellAssentament = True
                        Exit For
                    End If
                Next
                If afectaAlVellAssentament Then
                    retval = True
                End If
            End If

        End If


        Return retval
    End Function

    Private Function ExistQuotesIva() As Boolean
        Dim retval As Boolean = False
        For Each oCcb As Ccb In GetCcbsFromGrid()
            If oCcb.Cta.IsQuotaIva Then
                retval = True
                Exit For
            End If
        Next
        Return retval
    End Function

    Private Function IsAnteriorAUltimaDeclaracioIva() As Boolean
        Dim fchUltimaDeclaracio As Date = App.Current.emp.IvaFchUltimaDeclaracio
        Dim currentFch As Date = DateTimePicker1.Value
        Dim retval As Boolean = (currentFch <= fchUltimaDeclaracio)
        Return retval
    End Function

    Private Function IvaJaDeclarat() As Boolean
        Dim retval As Boolean = False
        Dim currentFch As Date = DateTimePicker1.Value
        Dim DtIvaFchUltimaDeclaracio As Date = App.Current.emp.IvaFchUltimaDeclaracio

        If DtIvaFchUltimaDeclaracio >= currentFch Then retval = True
        If DtIvaFchUltimaDeclaracio >= mFchOriginal Then retval = True
        Return retval
    End Function

    Private Function HaCanviatElPeriodeDeclaracioIva() As Boolean
        Dim retval As Boolean = False
        Dim currentFch As Date = DateTimePicker1.Value
        Dim DtIvaFchUltimaDeclaracio As Date = App.Current.emp.IvaFchUltimaDeclaracio

        If _Cca.IsNew Then
            If currentFch <= DtIvaFchUltimaDeclaracio Then retval = True
        Else
            Dim pertanyenAlMateixPeriode As Boolean = (currentFch.Year = mFchOriginal.Year And currentFch.Month = mFchOriginal.Month)
            If pertanyenAlMateixPeriode Then
            Else
                If DtIvaFchUltimaDeclaracio < currentFch And DtIvaFchUltimaDeclaracio < _Cca.fch Then
                    'FES LA EXCEPCIÓ SI CAP DELS PERIODES A QUE PERTANYEN HA ESTAT DECLARAT ENCARA
                Else
                    retval = True
                End If
            End If
        End If

        Return retval
    End Function

    Private Function HanCanviatLesQuotesIva() As Boolean
        'oOld correspon a l'albará original abans de rectificar-lo
        Dim oOldCcb As Ccb = Nothing
        Dim oOldIvaCcbs As New Ccbs
        For Each oOldCcb In _Cca.ccbs
            If oOldCcb.Cta.IsQuotaIva Then oOldIvaCcbs.Add(oOldCcb)
        Next

        'oNew correspon a l'albará tal com surt a la parrilla despres de acceptar les modificacions
        Dim oNewCcb As Ccb = Nothing
        Dim oNewIvaCcbs As New Ccbs
        For Each oNewCcb In GetCcbsFromGrid()
            If oNewCcb.Cta.IsQuotaIva Then oNewIvaCcbs.Add(oNewCcb)
        Next

        Dim retval As Boolean = Not (oNewIvaCcbs.Equals(oOldIvaCcbs))
        Return retval
    End Function


    Private Function GetCcbsFromGrid() As Ccbs
        Dim oCcbs As New Ccbs
        Dim oCcb As Ccb = Nothing
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not oRow.IsNewRow Then
                oCcb = getCcbFromDatagridViewRow(oRow)
                oCcbs.Add(oCcb)
            End If
        Next
        Return oCcbs
    End Function

    Private Function getCcbFromDatagridViewRow(ByVal oRow As DataGridViewRow) As Ccb
        Dim oDh As DTOCcb.DhEnum = DTOCcb.DhEnum.NotSet
        Dim oAmt As MaxiSrvr.Amt = Nothing
        Dim oDeb As MaxiSrvr.Amt = GetDebFromRow(oRow)
        If oDeb.IsZero Then
            oDh = DTOCcb.DhEnum.Haber
            oAmt = GetHabFromRow(oRow)
        Else
            oDh = DTOCcb.DhEnum.Debe
            oAmt = oDeb
        End If

        Dim oCcb As Ccb = New Ccb(_Cca, GetCtaFromRow(oRow), GetContactFromRow(oRow), oAmt, oDh)
        Return oCcb
    End Function

    Private Function GetCtaFromRow(ByVal oRow As DataGridViewRow) As PgcCta
        Dim oCta As PgcCta = Nothing
        If Not IsDBNull(oRow.Cells(Cols.Cta).Value) Then
            Dim oGuid As Guid = oRow.Cells(Cols.CtaGuid).Value
            oCta = New PgcCta(oGuid)
        End If
        Return oCta
    End Function

    Private Function GetContactFromRow(ByVal oRow As DataGridViewRow) As Contact
        Dim oContact As Contact = Nothing
        If Not IsDBNull(oRow.Cells(Cols.ContactGuid).Value) Then
            Dim oGuid As Guid = oRow.Cells(Cols.ContactGuid).Value
            oContact = New Contact(oGuid)
        End If
        Return oContact
    End Function

    Private Function GetDebFromRow(ByVal oRow As DataGridViewRow) As MaxiSrvr.Amt
        Dim oAmt As MaxiSrvr.Amt = Nothing

        If IsDBNull(oRow.Cells(Cols.DebCur).Value) Then
            oAmt = New MaxiSrvr.Amt
        Else
            Dim DcVal As Decimal = 0
            If IsNumeric(oRow.Cells(Cols.DebDiv).Value) Then
                DcVal = CDec(oRow.Cells(Cols.DebDiv).Value)
            End If

            Dim DcEur As Decimal = 0
            If IsNumeric(oRow.Cells(Cols.DebEur).Value) Then
                DcEur = CDec(oRow.Cells(Cols.DebEur).Value)
            End If

            Dim sCur As String = oRow.Cells(Cols.DebCur).Value
            Dim oCur As Cur = Current.Cur(sCur)
            If oCur Is Nothing Then oCur = MaxiSrvr.DefaultCur

            oAmt = New MaxiSrvr.Amt(DcEur, oCur, DcVal)
        End If

        Return oAmt
    End Function

    Private Function GetHabFromRow(ByVal oRow As DataGridViewRow) As MaxiSrvr.Amt
        Dim oAmt As MaxiSrvr.Amt = Nothing

        If IsDBNull(oRow.Cells(Cols.HabCur).Value) Then
            oAmt = New MaxiSrvr.Amt
        Else
            Dim DcVal As Decimal = 0
            If IsNumeric(oRow.Cells(Cols.HabDiv).Value) Then
                DcVal = CDec(oRow.Cells(Cols.HabDiv).Value)
            End If

            Dim DcEur As Decimal = 0
            If IsNumeric(oRow.Cells(Cols.HabEur).Value) Then
                DcEur = CDec(oRow.Cells(Cols.HabEur).Value)
            End If

            Dim sCur As String = oRow.Cells(Cols.HabCur).Value
            Dim oCur As Cur = Current.Cur(sCur)
            If Not oCur Is Nothing Then oCur = MaxiSrvr.DefaultCur

            oAmt = New MaxiSrvr.Amt(DcEur, oCur, DcVal)
        End If

        Return oAmt
    End Function


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub CheckSum()
        Dim DcEurDeb As Decimal = 0
        Dim DcEurHab As Decimal = 0
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            'If oRow.Cells(Cols.HabCur).Value = "GBP" Then Stop
            If Not IsDBNull(oRow.Cells(Cols.DebEur).Value) Then
                DcEurDeb += oRow.Cells(Cols.DebEur).Value
            End If
            If Not IsDBNull(oRow.Cells(Cols.HabEur).Value) Then
                DcEurHab += oRow.Cells(Cols.HabEur).Value
            End If
        Next
        TextBoxDeb.Text = Format(DcEurDeb, "#,###0.00 €;-#,###0.00 €;#")
        TextBoxHab.Text = Format(DcEurHab, "#,###0.00 €;-#,###0.00 €;#")
        TextBoxDif.Text = IIf(DcEurDeb = DcEurHab, "", Format(Math.Abs(DcEurDeb - DcEurHab), "#,###0.00 €;-#,###0.00 €;#"))
    End Sub

    Private Function CurrentContact() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim retval As Contact = GetContactFromRow(oRow)
        Return retval
    End Function

    Private Function CurrentCcd() As Ccd
        Dim oCcd As Ccd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oContact As Contact = CurrentContact()
            Dim oCta As PgcCta = GetCtaFromRow(oRow)
            If oCta IsNot Nothing Then
                Dim iYea As Integer = DateTimePicker1.Value.Year
                If oContact Is Nothing Then
                    oCcd = New Ccd(_Cca.emp, iYea, oCta)
                Else
                    oCcd = New Ccd(oContact, iYea, oCta)
                End If
            End If
        End If
        Return oCcd
    End Function



    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest assentament?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If _Cca.Delete( exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cca))
                MsgBox("Assentament eliminat", MsgBoxStyle.Information, "M+O")
                Me.Close()
            Else
                MsgBox("error al eliminar l'assentament" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        mDirtyCell = True
        Select Case e.ColumnIndex
            Case Cols.DebCur, Cols.HabCur
                e.Cancel = True
        End Select
    End Sub


    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case Cols.Cta
                    Dim oCta As PgcCta = CType(mLastValidatedObject, PgcCta)
                    oRow.Cells(Cols.CtaGuid).Value = oCta.Guid
                    oRow.Cells(Cols.Cta).Value = oCta.Id
                    oRow.Cells(Cols.CtaDsc).Value = oCta.Nom
                    DataGridView1.SelectNextControl(DataGridView1, True, True, True, True)
                    'DataGridView1.CurrentCell = DataGridView1.CurrentRow.Cells(Cols.Clx)
                Case Cols.Clx
                    Dim oContact As Contact = CType(mLastValidatedObject, Contact)
                    If oContact Is Nothing Then
                        oRow.Cells(Cols.ContactGuid).Value = System.DBNull.Value
                        oRow.Cells(Cols.Clx).Value = ""
                    Else
                        oRow.Cells(Cols.ContactGuid).Value = oContact.Guid
                        oRow.Cells(Cols.Clx).Value = oContact.Clx
                    End If
                Case Cols.DebCur, Cols.HabCur
                    Dim oCur As MaxiSrvr.Cur = CType(mLastValidatedObject, MaxiSrvr.Cur)
                    Dim sFormat As String = ""
                    Select Case oCur.Id
                        Case "EUR"
                            sFormat = "#,###0.00 €;-#,###0.00 €;#"
                        Case "GBP"
                            sFormat = "£ #,###0.00;£ -#,###0.00;#"
                        Case "USD"
                            sFormat = "$ #,###0.00;$ -#,###0.00;#"
                        Case Else
                            sFormat = "#,###0.00;-#,###0.00;#"
                    End Select
                    oRow.Cells(e.ColumnIndex - 1).Style.Format = sFormat
                Case Cols.DebEur
                    If Not IsDBNull(oRow.Cells(e.ColumnIndex).Value) Then
                        Dim sCellText As String = oRow.Cells(e.ColumnIndex).Value
                        If CDbl(sCellText) <> 0 Then
                            If Not mShowDivisas Then
                                oRow.Cells(Cols.DebDiv).Value = oRow.Cells(Cols.DebEur).Value
                            End If
                            oRow.Cells(Cols.HabEur).Value = 0
                            oRow.Cells(Cols.HabDiv).Value = 0
                        End If
                        CheckSum()
                    End If
                Case Cols.HabEur
                    If Not IsDBNull(oRow.Cells(e.ColumnIndex).Value) Then
                        Dim sCellText As String = oRow.Cells(e.ColumnIndex).Value
                        If CDbl(sCellText) <> 0 Then
                            If Not mShowDivisas Then
                                oRow.Cells(Cols.HabDiv).Value = oRow.Cells(Cols.HabEur).Value
                            End If
                            oRow.Cells(Cols.DebEur).Value = 0
                            oRow.Cells(Cols.DebDiv).Value = 0
                        End If
                        CheckSum()
                    End If
            End Select

            EnableButtons()
            mDirtyCell = False
        End If
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Select Case e.ColumnIndex
                Case Cols.Cta
                    Dim sCtaId As String = e.FormattedValue
                    If sCtaId = "" Then
                        mLastValidatedObject = Nothing
                    Else
                        Dim oCta As PgcCta = Finder.FindCta(mPgcPlan, sCtaId)
                        If oCta Is Nothing Then
                            e.Cancel = True
                        Else
                            mLastValidatedObject = oCta
                        End If
                    End If
                Case Cols.Clx
                    If e.FormattedValue = "" Then
                        mLastValidatedObject = Nothing
                    Else
                        Dim oContact As Contact = Finder.FindContact(_Cca.emp, e.FormattedValue)
                        If oContact Is Nothing Then
                            e.Cancel = True
                        Else
                            mLastValidatedObject = oContact
                        End If
                    End If
                Case Cols.DebCur, Cols.HabCur
                    Dim s As String = e.FormattedValue
                    Dim oCur As Cur = Current.Cur(s)
                    If oCur IsNot Nothing Then
                        mLastValidatedObject = oCur
                    Else
                        MsgBox("divisa '" & s & "' desconeguda")
                        e.Cancel = True
                    End If
                Case Cols.DebEur
                    Dim s As String = e.FormattedValue
                    If IsNumeric(s) Then
                        If CDbl(s) <> 0 Then
                            oRow.Cells(Cols.HabEur).Value = 0
                        Else
                            oRow.Cells(Cols.DebEur).Value = 0
                        End If
                    End If
            End Select
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim iCol As Integer = DataGridView1.CurrentCell.ColumnIndex
        Select Case iCol
            Case Cols.DebCur, Cols.HabCur
                Dim sCur As String = InputBox("nova divisa:", "MAT.NET", "EUR")
                If sCur > "" Then
                    Dim oCur As Cur = Current.Cur(sCur)
                    If oCur IsNot Nothing Then
                        DataGridView1.CurrentCell.Value = oCur.Id
                        EnableButtons()
                    Else
                        MsgBox("No s'ha trobat cap divisa amb aquest codi." & vbCrLf & "El sistema només accepta divises de tres lletres segons la norma internacional ISO 4217", MsgBoxStyle.Exclamation, "MAT.NET")
                    End If
                End If

        End Select
    End Sub

    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        If _AllowEvents Then
            CheckSum()
            EnableButtons()
        End If
    End Sub




    Private Sub ToolStripButtonExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExport.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub ButtonCancel_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            'salta les columnes ReadOnly al clicar-les amb el ratolí
            Dim oGrid As DataGridView = CType(sender, DataGridView)
            If oGrid.CurrentCell Is Nothing Then
                DataGridView1.ContextMenuStrip = Nothing
            Else
                If oGrid.CurrentCell.ReadOnly Then
                    For i As Integer = oGrid.CurrentCell.ColumnIndex + 1 To oGrid.Columns.Count - 1
                        If oGrid.Columns(i).Visible And Not oGrid.Columns(i).ReadOnly Then
                            oGrid.CurrentCell = oGrid.CurrentRow.Cells(i)
                            Exit For
                        End If
                    Next
                End If
                Select Case DataGridView1.CurrentCell.ColumnIndex
                    Case Cols.Cta, Cols.Clx
                        SetContextMenu()
                End Select
            End If
        End If

    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcd As Ccd = CurrentCcd()

        If oCcd IsNot Nothing Then
            Dim oMenu_Ccd As New Menu_Ccd(oCcd, _Cca.emp)
            'AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Ccd.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        'salta les columnes ReadOnly al passar amb el tabulador
        Dim oGrid As DataGridView = CType(sender, DataGridView)
        If oGrid.CurrentCell IsNot Nothing Then
            If oGrid.CurrentCell.ReadOnly Then
                For i As Integer = oGrid.CurrentCell.ColumnIndex + 1 To oGrid.Columns.Count - 1
                    If oGrid.Columns(i).Visible And Not oGrid.Columns(i).ReadOnly Then
                        oGrid.CurrentCell = oGrid.CurrentRow.Cells(i)
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub ToolStripButtonCur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonCur.Click
        mShowDivisas = Not mShowDivisas
        CheckDivisas()
    End Sub

    Private Sub CheckDivisas()
        With DataGridView1
            If mShowDivisas Then
                Me.Width = 1030
                With .Columns(Cols.DebDiv)
                    .HeaderText = "debe"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 70
                    .DefaultCellStyle.BackColor = Color.LightYellow
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(Cols.DebCur)
                    .HeaderText = ""
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 40
                    .DefaultCellStyle.BackColor = Color.LightYellow
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.NullValue = "EUR"
                    .Visible = True
                    .ReadOnly = False
                End With
                With .Columns(Cols.HabDiv)
                    .HeaderText = "haber"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 70
                    .DefaultCellStyle.BackColor = Color.LightYellow
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(Cols.HabCur)
                    .HeaderText = ""
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 40
                    .DefaultCellStyle.BackColor = Color.LightYellow
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.NullValue = "EUR"
                    .Visible = True
                    .ReadOnly = False
                End With
            Else
                Me.Width = 1028
                With .Columns(Cols.DebDiv)
                    .Visible = False
                End With
                With .Columns(Cols.DebCur)
                    .Visible = False
                End With
                With .Columns(Cols.HabDiv)
                    .Visible = False
                End With
                With .Columns(Cols.HabCur)
                    .Visible = False
                End With
            End If

        End With
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                e.SuppressKeyPress = True
            Case Keys.Escape
                If ButtonOk.Enabled Then
                    ButtonOk.Focus()
                Else
                    ButtonCancel.Focus()
                End If
        End Select
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'ho crida DataGridView1_EditingControlShowing
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.DebDiv, Cols.DebEur, Cols.HabDiv, Cols.HabEur
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        'fa que funcioni KeyPress per DataGridViews
        If TypeOf e.Control Is TextBox Then
            Dim oControl As TextBox = CType(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf DataGridView1_KeyPress
        End If
    End Sub


    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        If _AllowEvents Then
            mDirtyFileDocument = True
            EnableButtons()
        End If
    End Sub
End Class