Imports System.ComponentModel

Public Class Frm_Cca
    Private _Cca As DTOCca
    Private _Ctas As List(Of DTOPgcCta)

    Private mDs As DataSet
    Private mLastBlockedCcaYea As Integer
    Private mFchOriginal As Date = Date.MinValue
    Private mAllowUpdate As Boolean
    Private mLastValidatedObject As Object
    Private mDirtyCell As Boolean
    Private mDirtyFileDocument As Boolean
    Private mShowDivisas As Boolean
    Private _PgcPlan As DTOPgcPlan = DTOApp.Current.PgcPlan

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
        Pnd
    End Enum

    Public Sub New(oCca As DTOCca)
        MyBase.New()
        Me.InitializeComponent()
        _Cca = oCca
    End Sub


    Private Async Sub Frm_Cca_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Ctas = Await FEB2.PgcCtas.All(exs, _Cca.Fch.Year)
        If exs.Count = 0 Then
            If FEB2.Cca.Load(_Cca, exs) Then
                mFchOriginal = _Cca.Fch
                mLastBlockedCcaYea = Await FEB2.Default.EmpInteger(Current.Session.Emp, DTODefault.Codis.LastBlockedCcaYea, exs)
                If exs.Count = 0 Then
                    _AllowEvents = True
                    If AllowBrowse(_Cca) Then
                        UIHelper.LoadComboFromEnum(ComboBoxCcd, GetType(DTOCca.CcdEnum))
                        If Await Display(_Cca, exs) Then
                            RefrescaStatusBar(_Cca)
                            Refresca()
                            EnableButtons()
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        Await FEB2.MailMessage.MailAdmin(My.User.Name & " BROWSE ASSENTAMENT SENSIBLE " & Year(_Cca.fch) & "/" & _Cca.id)
                        MsgBox("Operació no autoritzada", MsgBoxStyle.Exclamation, "MAT.NET")
                        Me.Close()
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "Error al descarregar els comptes del pla comptable")
        End If
    End Sub

    Private Function AllowBrowse(oCca As DTOCca) As Boolean
        Dim oUser As DTOUser = Current.Session.User
        Dim retval As Boolean = oCca.Items.All(Function(x) DTOUser.AllowContactBrowse(oUser, x.Contact))
        Return retval
    End Function



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
            .Columns.Add("Pnd", System.Type.GetType("System.Guid"))
        End With
        Dim oRow As DataRow
        For Each item In _Cca.Items
            oRow = oTb.NewRow

            oRow(Cols.Cta) = item.Cta.Id
            oRow(Cols.CtaDsc) = item.Cta.Nom.Tradueix(Current.Session.Lang)
            oRow(Cols.CtaGuid) = item.Cta.Guid
            If item.Contact IsNot Nothing Then
                oRow(Cols.ContactGuid) = item.Contact.Guid
                oRow(Cols.Clx) = item.Contact.FullNom
            End If
            Select Case item.Dh
                Case DTOCcb.DhEnum.Debe
                    oRow(Cols.DebDiv) = item.Amt.Val
                    oRow(Cols.DebCur) = item.Amt.Cur.Tag
                    oRow(Cols.DebEur) = item.Amt.Eur
                Case DTOCcb.DhEnum.Haber
                    oRow(Cols.HabDiv) = item.Amt.Val
                    oRow(Cols.HabCur) = item.Amt.Cur.Tag
                    oRow(Cols.HabEur) = item.Amt.Eur
            End Select
            oTb.Rows.Add(oRow)
            If item.Amt.Cur IsNot Nothing Then
                If item.Amt.Cur.Tag <> DTOCur.Eur.Tag Then
                    mShowDivisas = True
                End If
            End If
            If item.Pnd IsNot Nothing Then
                oRow(Cols.Pnd) = item.Pnd.Guid
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
            With .Columns(Cols.Pnd)
                .Visible = False
            End With
            CheckDivisas()
        End With

        mDs = New DataSet
        mDs.Tables.Add(oTb)
    End Sub

    Private Async Function Display(oCca As DTOCca, exs As List(Of Exception)) As Task(Of Boolean)
        With oCca
            If oCca.Id = 0 Then
                Me.Text = "Nou assentament"
                DateTimePicker1.MinDate = "1/1/" & CStr(mLastBlockedCcaYea + 1)
                If .Ccd = DTOCca.CcdEnum.NotSet Then .Ccd = DTOCca.CcdEnum.Manual
                mAllowUpdate = True
            Else
                Me.Text = "Assentament Num." & .Id
                Dim isBlockedYear As Boolean = Await FEB2.Cca.IsBlockedYear(GlobalVariables.Emp, .Fch.Year, exs)
                mAllowUpdate = Not isBlockedYear
                ButtonDel.Enabled = Not isBlockedYear
                Select Case Current.Session.Rol.id
                    Case DTORol.Ids.Auditor
                        mAllowUpdate = False
                        ButtonDel.Enabled = False
                End Select

            End If
            ComboBoxCcd.SelectedValue = .Ccd
            TextBoxConcepte.Text = .Concept
            If .Fch > DateTimePicker1.MinDate Then
                DateTimePicker1.Value = .Fch
            End If
            If _Cca.Projecte IsNot Nothing Then
                CheckBoxProjecte.Checked = True
                Xl_LookupProjecte1.Visible = True
                Xl_LookupProjecte1.Projecte = _Cca.Projecte
            End If

            If .docFile IsNot Nothing Then
                .docFile.Nom = .concept
            End If
            Await Xl_DocFile1.Load(.docFile)
        End With
        Return (exs.Count = 0)
    End Function

    Private Sub RefrescaStatusBar(oCca As DTOCca)
        LabelUsr.Text = oCca.UsrLog.Text
    End Sub

    Private Sub DataChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
     Handles TextBoxConcepte.TextChanged,
      ComboBoxCcd.SelectedValueChanged,
        Xl_LookupProjecte1.AfterUpdate

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


    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Await CheckIvaFch() Then

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


            With _Cca
                .Concept = TextBoxConcepte.Text
                .Fch = DateTimePicker1.Value
                .Exercici = DTOExercici.FromYear(Current.Session.Emp, .Fch.Year)
                .Ccd = ComboBoxCcd.SelectedValue
                .Items = GetItemsFromGrid()
                .UsrLog.UsrLastEdited = Current.Session.User

                If CheckBoxProjecte.Checked Then
                    .Projecte = Xl_LookupProjecte1.Projecte
                Else
                    .Projecte = Nothing
                End If

                If Xl_DocFile1.IsDirty Then
                    .DocFile = Xl_DocFile1.Value
                End If

                Dim BlIsNew As Boolean = .IsNew


                Dim exs As New List(Of Exception)
                UIHelper.ToggleProggressBar(Panel1, True)
                _Cca.id = Await FEB2.Cca.Update(exs, _Cca)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count = 0 Then
                    If BlIsNew Then
                        MsgBox("registre nº " & _Cca.id, MsgBoxStyle.Information, "MAT.NET")
                    End If
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cca))
                    Me.Close()
                Else
                    MsgBox("error al desar l'assentament:" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
                End If

            End With

        End If
    End Sub

    Private Async Function CheckIvaFch() As Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim RetVal As Boolean = True
        Dim BlWarn As Boolean = False

        Dim fchUltimaDeclaracio = Await FEB2.Cca.IvaFchUltimaDeclaracio(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            If _Cca.IsNew Then
                If ExistQuotesIva(GetItemsFromGrid) Then
                    If IsAnteriorAUltimaDeclaracioIva(fchUltimaDeclaracio) Then BlWarn = True
                End If
            Else
                If Not _Cca.Ccd = DTOCca.CcdEnum.IVA Then
                    If AfectaAlIva() Then

                        If IvaJaDeclarat(fchUltimaDeclaracio) Then
                            If HaCanviatElPeriodeDeclaracioIva(fchUltimaDeclaracio) Then
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
        Else
            UIHelper.WarnError(exs, "Error al llegir la última declaració de Iva")
        End If


        Return RetVal
    End Function

    Private Function AfectaAlIva() As Boolean
        Dim oQuotasInPreviousQuotas = ExistQuotesIva(_Cca.Items)
        Dim oQuotasInUpdatedCcbs = ExistQuotesIva(GetItemsFromGrid())
        Dim retval = oQuotasInPreviousQuotas Or oQuotasInUpdatedCcbs
        Return retval
    End Function


    Private Function IsAnteriorAUltimaDeclaracioIva(fchUltimaDeclaracio As Date) As Boolean
        Dim currentFch As Date = DateTimePicker1.Value
        Dim retval As Boolean = (currentFch <= fchUltimaDeclaracio)
        Return retval
    End Function

    Private Function IvaJaDeclarat(fchUltimaDeclaracio As Date) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = False
        Dim currentFch As Date = DateTimePicker1.Value
        If exs.Count = 0 Then
            If fchUltimaDeclaracio >= currentFch Then retval = True
            If fchUltimaDeclaracio >= mFchOriginal Then retval = True
        Else
            UIHelper.WarnError(exs)
        End If

        Return retval
    End Function

    Private Function HaCanviatElPeriodeDeclaracioIva(fchUltimaDeclaracio As Date) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = False
        Dim currentFch As Date = DateTimePicker1.Value
        If exs.Count = 0 Then
            If _Cca.IsNew Then
                If currentFch <= fchUltimaDeclaracio Then retval = True
            Else
                Dim pertanyenAlMateixPeriode As Boolean = (currentFch.Year = mFchOriginal.Year And currentFch.Month = mFchOriginal.Month)
                If pertanyenAlMateixPeriode Then
                    retval = False
                Else
                    If fchUltimaDeclaracio < currentFch And fchUltimaDeclaracio < _Cca.Fch Then
                        'FES LA EXCEPCIÓ SI CAP DELS PERIODES A QUE PERTANYEN HA ESTAT DECLARAT ENCARA
                        retval = False
                    Else
                        retval = True
                    End If
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If

        Return retval
    End Function

    Private Function HanCanviatLesQuotesIva() As Boolean
        Dim previousQuotas As Decimal = SumaQuotesIva(_Cca.Items)
        Dim updatedQuotas As Decimal = SumaQuotesIva(GetItemsFromGrid())
        Dim retval = previousQuotas <> updatedQuotas
        Return retval
    End Function

    Private Function ExistQuotesIva(oCcbs As IEnumerable(Of DTOCcb)) As Boolean
        Return oCcbs.Any(Function(x) x.Cta.IsQuotaIva)
    End Function

    Private Function SumaQuotesIva(oCcbs As IEnumerable(Of DTOCcb)) As Decimal
        Dim oCcbsWithQuota = oCcbs.Where(Function(x) x.Cta.IsQuotaIva).ToList
        Dim retval As Decimal = oCcbsWithQuota.Sum(Function(y) IIf(y.Dh = DTOCcb.DhEnum.Debe, CDec(y.Amt.Eur), CDec(-y.Amt.Eur)))
        Return retval
    End Function

    Private Function GetItemsFromGrid() As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not oRow.IsNewRow Then
                Dim item = getItemFromDatagridViewRow(oRow)
                retval.Add(item)
            End If
        Next
        Return retval
    End Function


    Private Function getItemFromDatagridViewRow(ByVal oRow As DataGridViewRow) As DTOCcb
        Dim oDh As DTOCcb.DhEnum = DTOCcb.DhEnum.NotSet
        Dim oAmt As DTOAmt = Nothing
        Dim oDeb As DTOAmt = GetDebFromRow(oRow)
        If oDeb.IsZero Then
            oDh = DTOCcb.DhEnum.Haber
            oAmt = GetHabFromRow(oRow)
        Else
            oDh = DTOCcb.DhEnum.Debe
            oAmt = oDeb
        End If

        Dim oCcb = DTOCcb.Factory(_Cca, oAmt, GetPgcCtaFromRow(oRow), GetContactFromRow(oRow), oDh)
        If Not IsDBNull(oRow.Cells(Cols.Pnd).Value) Then
            Dim PndGuid As Guid = oRow.Cells(Cols.Pnd).Value
            Dim oPnd As New DTOPnd(PndGuid)
            oCcb.Pnd = oPnd
        End If
        Return oCcb
    End Function

    Private Function GetPgcCtaFromRow(ByVal oRow As DataGridViewRow) As DTOPgcCta
        Dim retval As DTOPgcCta = Nothing
        If Not IsDBNull(oRow.Cells(Cols.Cta).Value) Then
            Dim oGuid As Guid = oRow.Cells(Cols.CtaGuid).Value
            retval = _Ctas.FirstOrDefault(Function(x) x.Guid.Equals(oGuid))
        End If
        Return retval
    End Function


    Private Function GetContactFromRow(ByVal oRow As DataGridViewRow) As DTOContact
        Dim retval As DTOContact = Nothing
        If Not IsDBNull(oRow.Cells(Cols.ContactGuid).Value) Then
            Dim oGuid As Guid = oRow.Cells(Cols.ContactGuid).Value
            retval = New DTOContact(oGuid)
            retval.FullNom = oRow.Cells(Cols.Clx).Value
        End If
        Return retval
    End Function

    Private Function GetDebFromRow(ByVal oRow As DataGridViewRow) As DTOAmt
        Dim oAmt As DTOAmt = Nothing

        If IsDBNull(oRow.Cells(Cols.DebCur).Value) Then
            oAmt = DTOAmt.Empty
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
            Dim oCur = DTOCur.Factory(sCur)
            If oCur Is Nothing Then oCur = Current.Session.Cur

            oAmt = DTOAmt.Factory(DcEur, oCur.Tag, DcVal)
        End If

        Return oAmt
    End Function

    Private Function GetHabFromRow(ByVal oRow As DataGridViewRow) As DTOAmt
        Dim oAmt As DTOAmt = Nothing

        If IsDBNull(oRow.Cells(Cols.HabCur).Value) Then
            oAmt = DTOAmt.Empty
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
            Dim oCur As DTOCur = DTOCur.Factory(sCur)
            If Not oCur Is Nothing Then oCur = Current.Session.Cur

            oAmt = DTOAmt.Factory(DcEur, oCur.Tag, DcVal)
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

    Private Function CurrentContact() As DTOContact
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim retval As DTOContact = GetContactFromRow(oRow)
        Return retval
    End Function

    Private Function CurrentCcd() As DTOCcd
        Dim retval As DTOCcd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oContact As DTOContact = CurrentContact()
            Dim oCta As DTOPgcCta = GetPgcCtaFromRow(oRow)
            If oCta IsNot Nothing Then
                Dim iYea As Integer = DateTimePicker1.Value.Year
                Dim oExercici = DTOExercici.FromYear(Current.Session.Emp, iYea)
                retval = DTOCcd.Factory(oExercici, oCta, oContact)
            End If
        End If
        Return retval
    End Function



    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest assentament?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Cca.Delete(exs, _Cca) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cca))
                MsgBox("Assentament eliminat", MsgBoxStyle.Information, "M+O")
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar l'assentament")
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
                    Dim oCta As DTOPgcCta = DirectCast(mLastValidatedObject, DTOPgcCta)
                    oRow.Cells(Cols.CtaGuid).Value = oCta.Guid
                    oRow.Cells(Cols.Cta).Value = oCta.Id
                    oRow.Cells(Cols.CtaDsc).Value = oCta.Nom.Tradueix(Current.Session.Lang)
                    DataGridView1.SelectNextControl(DataGridView1, True, True, True, True)
                'DataGridView1.CurrentCell = DataGridView1.CurrentRow.Cells(Cols.Clx)
                Case Cols.Clx
                    Dim oContact As DTOContact = DirectCast(mLastValidatedObject, DTOContact)
                    If oContact Is Nothing Then
                        oRow.Cells(Cols.ContactGuid).Value = System.DBNull.Value
                        oRow.Cells(Cols.Clx).Value = ""
                    Else
                        oRow.Cells(Cols.ContactGuid).Value = oContact.Guid
                        oRow.Cells(Cols.Clx).Value = oContact.FullNom
                    End If
                Case Cols.DebCur, Cols.HabCur
                    Dim oCur As DTOCur = DirectCast(mLastValidatedObject, DTOCur)
                    Dim sFormat As String = ""
                    Select Case oCur.Tag
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
                        Dim exs As New List(Of Exception)
                        Dim oCta As DTOPgcCta = Finder.FindCta(_Ctas, sCtaId)
                        If exs.Count = 0 Then
                            If oCta Is Nothing Then
                                e.Cancel = True
                            Else
                                mLastValidatedObject = oCta
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    End If
                Case Cols.Clx
                    If e.FormattedValue = "" Then
                        mLastValidatedObject = Nothing
                    Else
                        Dim exs As New List(Of Exception)
                        Dim oContact = Finder.FindContact(exs, Current.Session.User, e.FormattedValue)
                        If exs.Count = 0 Then
                            If oContact Is Nothing Then
                                e.Cancel = True
                            Else
                                mLastValidatedObject = oContact
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    End If
                Case Cols.DebCur, Cols.HabCur
                    Dim s As String = e.FormattedValue
                    Dim oCur = DTOCur.Factory(s)
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
                    Dim oCur = DTOCur.Factory(sCur)
                    If oCur IsNot Nothing Then
                        DataGridView1.CurrentCell.Value = oCur.Tag
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
        Dim oSheet = MatHelperStd.ExcelHelper.Sheet.Factory(mDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            'salta les columnes ReadOnly al clicar-les amb el ratolí
            Dim oGrid As DataGridView = DirectCast(sender, DataGridView)
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
        Dim oCcd As DTOCcd = CurrentCcd()

        If oCcd IsNot Nothing Then
            Dim oMenu_Ccd As New Menu_Ccd(oCcd)
            'AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Ccd.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        'salta les columnes ReadOnly al passar amb el tabulador
        Dim oGrid As DataGridView = DirectCast(sender, DataGridView)
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
            Dim oControl As TextBox = DirectCast(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf DataGridView1_KeyPress
        End If
    End Sub


    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterUpdate
        If _AllowEvents Then
            mDirtyFileDocument = True
            EnableButtons()
        End If
    End Sub

    Private Sub CheckBoxProjecte_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProjecte.CheckedChanged
        If _AllowEvents Then
            Xl_LookupProjecte1.Visible = True
        End If
    End Sub

    Private Sub Xl_LookupProjecte1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupProjecte1.RequestToLookup
        Dim oFrm As New Frm_Projectes(_Cca.Projecte, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onProjectSelected
        oFrm.Show()
    End Sub

    Private Sub onProjectSelected(sender As Object, e As MatEventArgs)
        Xl_LookupProjecte1.Projecte = e.Argument
        EnableButtons()
    End Sub


    Private Sub DateTimePicker1_Validating(sender As Object, e As CancelEventArgs) Handles DateTimePicker1.Validating
        If _AllowEvents Then
            Dim iYea As Integer = DateTimePicker1.Value.Year
            Dim changedYear As Boolean = _Cca.Fch.Year <> iYea
            If changedYear Then
                If Not _Cca.IsNew And Not _Cca.Id <> 0 Then
                    Dim rc = MsgBox("El canvi d'any comportarà un canvi en el numero d'assentament", MsgBoxStyle.OkCancel)
                    If rc = MsgBoxResult.Ok Then
                        _Cca.Id = 0
                    Else
                        e.Cancel = True
                    End If
                End If
            End If
        End If

    End Sub
End Class