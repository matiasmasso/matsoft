

Public Class Frm_Nominas
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDirtyCell As Boolean

    Private Enum Cols
        Cli
        Nom
        Devengat
        Dietas
        SegSoc
        Irpf
        LiqStored
        MatchWarn
        IcoMatchWarn
        LiqCalc
    End Enum

    Private Sub Frm_Nominas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DtFch As Date = Today
        DtFch = DtFch.AddDays(-DtFch.Day).AddMonths(1) 'final de mes
        DateTimePicker1.Value = DtFch
        Dim sMes As String = MaxiSrvr.Emp.FromDTOEmp(memp).Org.Lang.Mes(DtFch.Month)
        Dim sText As String = "Nómina " & sMes.ToUpper
        TextBoxConcepte.Text = sText
        LoadGrid()
    End Sub

    Private Function CalcLiq(ByVal DblDevengat As Decimal, ByVal DblSegSoc As Decimal, ByVal DblIrpf As Decimal) As Decimal
        Dim DblLiq As Decimal = DblDevengat - DblSegSoc - DblIrpf
        Return Math.Round(DblLiq, 2)
    End Function

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CliStaff.cli, " _
        & "CliStaff.Abr, " _
        & "CliStaff.Brut, " _
        & "CliStaff.diet, " _
        & "CliStaff.SS, " _
        & "CliStaff.IRPF, " _
        & "CliStaff.transfer, " _
        & "0 AS WARN, 0.0 as LIQ " _
        & "FROM CliStaff " _
        & "WHERE CliStaff.EMP=" & BLLApp.Emp.Id & " AND " _
        & "(CliStaff.Baja < CliStaff.Alta) " _
        & "ORDER BY ABR"
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.IcoMatchWarn)

        Dim oRow As DataRow
        Dim DblStored As Decimal
        Dim DblCalculated As Decimal
        For Each oRow In oTb.Rows
            DblStored = CDbl(oRow(Cols.LiqStored))
            DblCalculated = CalcLiq(oRow(Cols.Devengat), oRow(Cols.SegSoc), oRow(Cols.Irpf))
            oRow(Cols.LiqCalc) = DblCalculated
            oRow(Cols.MatchWarn) = IIf(DblStored = DblCalculated, 0, 1)
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
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(Cols.Devengat)
                .HeaderText = "devengat"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Dietas)
                .HeaderText = "dietes"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.SegSoc)
                .HeaderText = "Seg.Social"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Irpf)
                .HeaderText = "IRPF"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.LiqStored)
                .Visible = False
            End With
            With .Columns(Cols.MatchWarn)
                .Visible = False
            End With
            With .Columns(Cols.IcoMatchWarn)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
                .ReadOnly = True
            End With
            With .Columns(Cols.LiqCalc)
                .HeaderText = "liquid"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                .ReadOnly = True
            End With
        End With
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoMatchWarn
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If Not IsDBNull(oRow.Cells(Cols.MatchWarn).Value) Then
                    Dim iCod As Integer = CInt(oRow.Cells(Cols.MatchWarn).Value)
                    Select Case iCod
                        Case 1
                            e.Value = My.Resources.warn
                        Case DTO.DTOCustomer.CashCodes.credit
                            e.Value = My.Resources.empty
                    End Select
                End If
        End Select
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        LoadGrid()
    End Sub

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        mDirtyCell = True
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If mDirtyCell Then
            Select Case e.ColumnIndex
                Case Cols.Devengat, Cols.Dietas, Cols.Irpf, Cols.SegSoc
                    If Not IsNumeric(e.FormattedValue) Then
                        MsgBox("només s'accepten valors numerics", MsgBoxStyle.Exclamation, "MAT.NET")
                        e.Cancel = True
                    End If
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If mDirtyCell Then
            Select Case e.ColumnIndex
                Case Cols.Devengat, Cols.SegSoc, Cols.Irpf
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim DblDevengat As Decimal = oRow.Cells(Cols.Devengat).Value
                    Dim DblSegSoc As Decimal = oRow.Cells(Cols.SegSoc).Value
                    Dim DblIrpf As Decimal = oRow.Cells(Cols.Irpf).Value
                    Dim DblLiquid As Decimal = Math.Round(DblDevengat - DblSegSoc - DblIrpf, 2)
                    oRow.Cells(Cols.LiqCalc).Value = DblLiquid
                    Dim DblStored As Decimal = CDbl(oRow.Cells(Cols.LiqStored).Value)
                    Dim iWarn As Integer = IIf(DblLiquid = DblStored, 0, 1)
                    oRow.Cells(Cols.MatchWarn).Value = iWarn
                    DataGridView1.Refresh()
            End Select
            mDirtyCell = False
        End If
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            If oRow.Cells(Cols.Cli).Value IsNot Nothing Then
                'evita la ultima fila append en blanc
                SaveCca(oRow)
            End If
        Next
        Me.Close()
    End Sub

    Private Sub SaveCca(ByVal oRow As DataGridViewRow)
        Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mEmp, oRow.Cells(Cols.Cli).Value)
        Dim oPlan As PgcPlan = PgcPlan.FromYear(DateTimePicker1.Value.Year)
        Dim oCtaDevengo As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.Nomina)
        Dim oCtaDietas As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.Dietas)
        Dim oCtaSegSoc As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.SegSocialDevengo)
        Dim oCtaIrpf As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.IrpfTreballadors)
        Dim oCtaLiq As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.PagasTreballadors)

        Dim oAmtDevengo As DTOAmt = BLLApp.GetAmt(CDec(oRow.Cells(Cols.Devengat).Value - oRow.Cells(Cols.Dietas).Value))
        Dim oAmtDietas As DTOAmt = BLLApp.GetAmt(CDec(oRow.Cells(Cols.Dietas).Value))
        Dim oAmtSegSoc As DTOAmt = BLLApp.GetAmt(CDec(oRow.Cells(Cols.SegSoc).Value))
        Dim oAmtIrpf As DTOAmt = BLLApp.GetAmt(CDec(oRow.Cells(Cols.Irpf).Value))
        Dim oAmtLiq As DTOAmt = BLLApp.GetAmt(CDec(oRow.Cells(Cols.LiqCalc).Value))

        Dim sText As String = TextBoxConcepte.Text
        Dim sNom As String = oRow.Cells(Cols.Nom).Value
        Dim iLenText As Integer = sText.Length
        Dim iLenNom As Integer = sNom.Length
        Dim iLenField As Integer = 60
        If iLenNom + iLenText + 1 > iLenField Then
            sNom = sNom.Substring(0, iLenField - iLenText - 1)
        End If
        sText = sNom & "-" & sText

        Dim oCca As new cca(BLL.BLLApp.emp)
        With oCca
            .Ccd = DTOCca.CcdEnum.Nomina
            .fch = DateTimePicker1.Value
            .Txt = sText
            If oAmtDevengo.Eur <> 0 Then
                .ccbs.Add(New Ccb(oCtaDevengo, oContact, oAmtDevengo, DTOCcb.DhEnum.Debe))
            End If
            If oAmtDietas.Eur <> 0 Then
                .ccbs.Add(New Ccb(oCtaDietas, oContact, oAmtDietas, DTOCcb.DhEnum.Debe))
            End If
            If oAmtSegSoc.Eur <> 0 Then
                .ccbs.Add(New Ccb(oCtaSegSoc, Nothing, oAmtSegSoc, DTOCcb.DhEnum.Haber))
            End If
            If oAmtIrpf.Eur <> 0 Then
                .ccbs.Add(New Ccb(oCtaIrpf, oContact, oAmtIrpf, DTOCcb.DhEnum.Haber))
            End If
            If oAmtLiq.Eur <> 0 Then
                .ccbs.Add(New Ccb(oCtaLiq, oContact, oAmtLiq, DTOCcb.DhEnum.Haber))
            End If
            If .ccbs.Count > 0 Then
                Dim exs as New List(Of exception)
                If Not .Update( exs) Then
                    MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'ho crida DataGridView1_EditingControlShowing
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Nom
            Case Else
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
End Class