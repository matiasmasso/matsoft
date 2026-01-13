
Imports System.data
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Fiscal_IVA

    Private mEmp As DTOEmp = BLL.BLLApp.Emp
    Private mPlan As PgcPlan = PgcPlan.FromToday
    Private mDs As DataSet
    Private mDsLog As DataSet
    Private mAllowEvents As Boolean
    Private mFirstFch As Date
    Private mLastFch As Date
    Private mRedondeig As Boolean = False
    Private mIvaStandard As Decimal
    Private mIvaReduit As Decimal
    Private mIvaSuperReduit As Decimal
    Private mReqStandard As Decimal
    Private mReqReduit As Decimal
    Private mReqSuperReduit As Decimal
    'Private mIvaReq As Decimal
    Private mIvaSoportat As Decimal
    Private mIvaImportacio As Decimal

    Private Enum Bkms
        IvaStandard
        RecarrecStandard
        IvaReduit
        RecarrecReduit
        IvaSuperReduit
        RecarrecSuperReduit
        IvaComunitariRepercutit
        TotRepercutit
        Soportat
        Importacio
        IvaComunitariSoportat
        TotSoportat
        Diferencia
        Compensar
        Total
    End Enum

    Private Enum Cols
        CodLin
        Txt
        Tipus
        Base
        Quota
        Calc
        Dif
        Sdo
    End Enum

    Private Enum Cols2
        LineCod
        Warn
        IcoWarn
        Text
    End Enum

    Private Enum LogLineCods
        _NotSet
        Redondeig
    End Enum

    Private Enum Signes
        NotSet
        Repercutit
        Soportat
    End Enum

    Private Enum Logs
        Ok
        Warn
        Err
    End Enum

    Private Sub Frm_Fiscal_IVA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadYeas()
        LoadBancs()
        SetDefaults()
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        mIvaStandard = 0
        mIvaReduit = 0
        mIvaSuperReduit = 0
        'mIvaReq = 0

        CreateLogSource()
        SetFchs()
        LoadGrid()
        CheckOrdreCronologic()
        CheckAlreadyRegistered()
    End Sub


    Private Sub LoadYeas()
        Dim iYears As New List(Of Integer)
        For i As Integer = Today.Year To 1985 Step -1
            iYears.Add(i)
        Next
        With ComboBoxYeas
            .DataSource = iYears
        End With
    End Sub



    Private Sub SetDefaults()
        Dim DtFch As Date = Today.AddMonths(-1)
        ComboBoxYeas.SelectedValue = DtFch.Year
        ComboBoxPeriods.SelectedIndex = DtFch.Month - 1
    End Sub

    Private Sub SetFchs()
        Dim iYea As Integer = CurrentYea()
        Dim iMes As Integer = CurrentMes()
        mFirstFch = New Date(iYea, iMes, 1)
        mLastFch = New Date(iYea, iMes, Date.DaysInMonth(iYea, iMes))
        LabelFchs.Text = "del " & mFirstFch.ToShortDateString & " al " & mLastFch.ToShortDateString
        Select Case iMes
            Case 12
                DateTimePickerPay.Value = mLastFch.AddDays(30)
            Case Else
                DateTimePickerPay.Value = mLastFch.AddDays(20)
        End Select
    End Sub

    Private Sub LoadGrid()
        mDs = CreateDataSource()
        mRedondeig = False

        AddIvaRepercutit(Bkms.IvaStandard, DTOPgcPlan.Ctas.IvaRepercutitNacional, "IvaStdPct", "IvaStdBase", "IvaStdAmt")
        AddIvaRepercutit(Bkms.RecarrecStandard, DTOPgcPlan.ctas.IvaRecarrecEquivalencia, "ReqStdPct", "IvaStdBase", "ReqStdAmt")

        AddIvaRepercutit(Bkms.IvaReduit, DTOPgcPlan.ctas.IvaReduit, "IvaRedPct", "IvaRedBase", "IvaRedAmt")
        AddIvaRepercutit(Bkms.RecarrecReduit, DTOPgcPlan.ctas.IvaRecarrecReduit, "ReqRedPct", "IvaRedBase", "ReqRedAmt")

        AddIvaRepercutit(Bkms.IvaSuperReduit, DTOPgcPlan.ctas.IvaSuperReduit, "IvaSuperRedPct", "IvaSuperRedBase", "IvaSuperRedAmt")
        AddIvaRepercutit(Bkms.RecarrecSuperReduit, DTOPgcPlan.ctas.IvaRecarrecSuperReduit, "ReqSuperRedPct", "IvaSuperRedBase", "ReqSuperRedAmt")

        'AddIvaRepercutit(Bkms.IvaReq, DTOPgcPlan.ctas.IvaRecarrecEquivalencia, "IVAREQPCT", "IVAREQBASE", "IVAREQAMT")
        If mRedondeig Then
            Log(LogLineCods.Redondeig, Logs.Warn, "Cal fer assentament de redondeig per quadrar les quotes amb l'IVA sobre les bases")
        Else
            Log(LogLineCods._NotSet, Logs.Ok, "Les quotes quadren amb l'IVA sobre les bases")
        End If
        AddIntraComunitari(Signes.Repercutit)
        AddSumaIvaRepercutit()
        AddIvaSoportat()
        AddIvaImportacio()
        AddIntraComunitari(Signes.Soportat)
        AddSumaIvaSoportat()

        AddDiferencia()

        Dim oTb As DataTable = mDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.CodLin)
                .Visible = False
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Tipus)
                .HeaderText = "tipus"
                .Width = 35
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Base)
                .HeaderText = "Suma de bases"
                .Width = 110
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Quota)
                .HeaderText = "Suma de quotes"
                .Width = 110
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Calc)
                .HeaderText = "tipus s/bases"
                .Width = 95
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Dif)
                .HeaderText = "redondeig"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Sdo)
                .HeaderText = "saldo compte"
                .Width = 95
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Sub AddDiferencia()
        Dim oTb As DataTable = mDs.Tables(0)
        Dim DecRepercutit As Decimal = 0
        Dim DecSoportat As Decimal = 0
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            Select Case CType(oRow(Cols.CodLin), Bkms)
                Case Bkms.IvaStandard,
                     Bkms.RecarrecStandard,
                     Bkms.IvaReduit,
                     Bkms.RecarrecReduit,
                     Bkms.IvaSuperReduit,
                     Bkms.RecarrecSuperReduit,
                     Bkms.IvaComunitariRepercutit
                    DecRepercutit = DecRepercutit + oRow(Cols.Sdo)
                Case Bkms.Soportat, Bkms.Importacio, Bkms.IvaComunitariSoportat
                    DecSoportat = DecSoportat + oRow(Cols.Sdo)

            End Select
        Next
        AddRow(Bkms.Diferencia, "diferencia", , , , , DecRepercutit - DecSoportat)
    End Sub


    Private Sub AddIvaSoportat()

        Dim iYea As Integer = CurrentYea()
        Dim oCta As DTOPgcCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.IvaRepercutitNacional)
        Dim DtFchStart As Date = New Date(mLastFch.Year, mLastFch.Month, 1)
        Dim sFchStart As String = Format(DtFchStart, "yyyyMMdd")
        Dim sFchEnd As String = Format(mLastFch, "yyyyMMdd")

        Dim oExercici As DTOExercici = CurrentExercici()
        Dim oBookFras As List(Of DTOBookFra) = BLLBookFras.All(DTOBookFra.Modes.OnlyIva, oExercici, mLastFch.Month)
        Dim DcQuota As Decimal = oBookFras.Sum(Function(x) x.IvaBaseQuotas.Sum(Function(y) y.Quota.Eur))
        Dim DcBase As Decimal = oBookFras.Sum(Function(x) x.IvaBaseQuotas.Sum(Function(y) y.Base.Eur))

        'Dim oCcd As New Ccd(mEmp, iYea, oCta)
        'Dim DecSdo As Decimal = oCcd.Saldo(mLastFch).Eur
        'mIvaSoportat = DecSdo

        mIvaSoportat = BLLPgcSaldo.FromCtaCod(DTOPgcPlan.Ctas.IvaRepercutitNacional, Nothing, mLastFch)
        AddRow(Bkms.Soportat, BLLPgcCta.FullNom(oCta, DTOLang.CAT), 0, DcBase, DcQuota, DcQuota, mIvaSoportat)
    End Sub

    Private Sub AddIntraComunitari(ByVal oSigne As Signes)
        Dim DcIvaPercent As Decimal = maxisrvr.Iva.Standard(mLastFch).Tipus
        Dim dcBase As Decimal = 0
        Dim dcQuota As Decimal = 0
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT SUM(case when CCB.DH=1 THEN CCB.EUR ELSE -CCB.EUR END) As Amt ")
        sb.AppendLine("FROM INTRASTAT ")
        sb.AppendLine("INNER JOIN IMPORTDTL ON INTRASTAT.GUID = IMPORTDTL.INTRASTAT ")
        sb.AppendLine("INNER JOIN CCA ON IMPORTDTL.GUID = CCA.GUID ")
        sb.AppendLine("INNER JOIN CCB ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta on ccb.CtaGuid = PgcCta.Guid AND PgcCta.Id LIKE '6%' ")
        sb.AppendLine("WHERE CCB.EMP=@EMP AND INTRASTAT.YEA=@YEA AND INTRASTAT.MES=@MES ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL, "@EMP", mEmp.Id, "@YEA", mLastFch.Year, "@MES", mLastFch.Month)
        oDrd.Read()

        If Not IsDBNull(oDrd("AMT")) Then
            dcBase = CDec(oDrd("AMT"))
            dcQuota = Math.Round(dcBase * DcIvaPercent / 100, 2, MidpointRounding.AwayFromZero)
        End If

        Select Case oSigne
            Case Signes.Repercutit
                AddRow(Bkms.IvaComunitariRepercutit, "INTRA COMUNITARI", DcIvaPercent, dcBase, dcQuota, dcQuota, dcQuota)
            Case Signes.Soportat
                AddRow(Bkms.IvaComunitariSoportat, "INTRA COMUNITARI", DcIvaPercent, dcBase, dcQuota, dcQuota, dcQuota)
        End Select
    End Sub

    Private Sub AddIvaImportacio()
        Dim DcIvaPercent As Decimal = maxisrvr.Iva.Standard(mLastFch).Tipus
        Dim oCta As PgcCta = mPlan.Cta(DTOPgcPlan.Ctas.IvaSoportatImportacio)
        Dim DecSdo As Decimal = BLLPgcSaldo.FromCtaCod(DTOPgcPlan.Ctas.IvaSoportatImportacio, Nothing)

        mIvaImportacio = DecSdo
        AddRow(Bkms.Importacio, oCta.FullNom, DcIvaPercent, 0, DecSdo, DecSdo, DecSdo)
    End Sub

    Private Sub AddSumaIvaRepercutit()
        Dim oTb As DataTable = mDs.Tables(0)
        Dim DecQuot As Decimal = 0
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            Select Case CType(oRow(Cols.CodLin), Bkms)
                Case Bkms.IvaStandard,
                    Bkms.RecarrecStandard,
                    Bkms.IvaReduit,
                    Bkms.RecarrecReduit,
                    Bkms.IvaSuperReduit,
                    Bkms.RecarrecSuperReduit,
                    Bkms.IvaComunitariRepercutit
                    DecQuot = DecQuot + CDec(oRow(Cols.Sdo))
            End Select
        Next

        AddRow(Bkms.Diferencia, "total repercutit", , , , , DecQuot)
    End Sub

    Private Sub AddSumaIvaSoportat()
        Dim oTb As DataTable = mDs.Tables(0)
        Dim DecQuot As Decimal = 0
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            Select Case CType(oRow(Cols.CodLin), Bkms)
                Case Bkms.Soportat, Bkms.Importacio, Bkms.IvaComunitariSoportat
                    DecQuot = DecQuot + CDec(oRow(Cols.Sdo))
            End Select
        Next

        AddRow(Bkms.Diferencia, "total soportat", , , , , DecQuot)
    End Sub

    Private Sub AddIvaRepercutit(ByVal oBkm As Bkms, ByVal oCtaCod As DTOPgcPlan.Ctas, ByVal sFldPct As String, ByVal sFldBas As String, ByVal sFldAmt As String)
        '        AddIvaRepercutit(Bkms.IvaStandard, DTOPgcPlan.ctas.IvaRepercutit, "IvaStdPct", "IvaStdBase", "IvaStdAmt")

        Dim iYea As Integer = CurrentYea()
        Dim oExercici = BLLExercici.FromYear(iYea)
        Dim oCta As DTOPgcCta = BLLPgcCta.FromCod(oCtaCod, oExercici)
        Dim DecSdo As Decimal = BLLPgcSaldo.FromCtaCod(oCtaCod, Nothing, mLastFch)

        Select Case oBkm
            Case Bkms.IvaStandard
                mIvaStandard = DecSdo
            Case Bkms.RecarrecStandard
                mReqStandard = DecSdo
            Case Bkms.IvaReduit
                mIvaReduit = DecSdo
            Case Bkms.RecarrecReduit
                mReqReduit = DecSdo
            Case Bkms.IvaSuperReduit
                mIvaSuperReduit = DecSdo
            Case Bkms.RecarrecSuperReduit
                mReqSuperReduit = DecSdo
        End Select

        Dim SQL As String = "SELECT " _
        & sFldPct & ", " _
        & "SUM(" & sFldBas & ") AS " & sFldBas & ", " _
        & "SUM(" & sFldAmt & ") AS " & sFldAmt & " " _
        & "FROM FRA " _
        & "WHERE Emp =" & mEmp.Id & " AND " _
        & "yea =" & iYea & " AND " _
        & "DATEPART(MONTH, fch) =" & CurrentMes() & " AND " _
        & sFldPct & "<>0 " _
        & "GROUP BY " & sFldPct & " " _
        & "ORDER BY " & sFldPct & " "

        Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL)
        Dim SngPct As Decimal
        Dim DecBas As Decimal
        Do While oDrd.Read
            SngPct = oDrd(sFldPct)
            DecBas = oDrd(sFldBas)
            AddRow(oBkm, BLLPgcCta.FullNom(oCta, BLLSession.Current.Lang), SngPct, DecBas, oDrd(sFldAmt), Math.Round(DecBas * SngPct / 100, 2, MidpointRounding.AwayFromZero), DecSdo)
        Loop

    End Sub

    Private Sub AddRow(ByVal oBkm As Bkms, ByVal sTxt As String, Optional ByVal SngPct As Decimal = 0, Optional ByVal DecBas As Decimal = 0, Optional ByVal DecQuot As Decimal = 0, Optional ByVal DecCalc As Decimal = 0, Optional ByVal DecSdo As Decimal = 0)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow(Cols.CodLin) = oBkm
        oRow(Cols.Txt) = sTxt
        oRow(Cols.Tipus) = SngPct
        oRow(Cols.Base) = DecBas
        oRow(Cols.Quota) = DecQuot
        oRow(Cols.Calc) = DecCalc
        oRow(Cols.Dif) = 0
        Select Case oBkm
            Case Bkms.IvaStandard, Bkms.RecarrecStandard, Bkms.IvaReduit, Bkms.RecarrecReduit, Bkms.IvaSuperReduit, Bkms.RecarrecSuperReduit
                If DecSdo <> DecCalc Then
                    'oRow(Cols.Dif) = Math.Round(DecCalc - DecQuot, 2, MidpointRounding.AwayFromZero)
                    oRow(Cols.Dif) = Math.Round(DecCalc - DecSdo, 2, MidpointRounding.AwayFromZero)
                    If DecSdo <> 0 Then
                        mRedondeig = True
                    End If
                End If
        End Select
        oRow(Cols.Sdo) = DecSdo
        oTb.Rows.Add(oRow)
    End Sub

    Private Function CreateDataSource() As DataSet
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("CodLin", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("Dsc", System.Type.GetType("System.String")))
            .Add(New DataColumn("Tipus", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Base", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Quota", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Calc", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Diff", System.Type.GetType("System.Decimal")))
            .Add(New DataColumn("Sdo", System.Type.GetType("System.Decimal")))
        End With

        Dim oDs As New DataSet
        oDs.Tables.Add(oTb)
        Return oDs
    End Function

    Private Sub CreateLogSource()
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("LogCodLin", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("Warn", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("WarnIco", System.Type.GetType("System.Byte[]")))
            .Add(New DataColumn("Txt", System.Type.GetType("System.String")))
        End With
        mDsLog = New DataSet
        mDsLog.Tables.Add(oTb)

        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowsDefaultCellStyle.SelectionBackColor = .RowsDefaultCellStyle.BackColor
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols2.LineCod)
                .Visible = False
            End With
            With .Columns(Cols2.Warn)
                .Visible = False
            End With
            With .Columns(Cols2.IcoWarn)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols2.Text)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentExercici() As DTOExercici
        Dim retval As DTOExercici = BLL.BLLExercici.FromYear(CurrentYea)
        Return retval
    End Function

    Private Function CurrentYea() As Integer
        Return ComboBoxYeas.SelectedValue
    End Function

    Private Function CurrentMes() As Integer
        Return ComboBoxPeriods.SelectedIndex + 1
    End Function

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oCodLin As Bkms = CType(oRow.Cells(Cols.CodLin).Value, Bkms)
        Select Case oCodLin
            Case Bkms.TotRepercutit, Bkms.TotSoportat, Bkms.Diferencia
                oRow.DefaultCellStyle.BackColor = Me.BackColor
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub


    Private Sub ExtracteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComboBoxYeas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYeas.SelectedIndexChanged
        If mAllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub ComboBoxQuarters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPeriods.SelectedIndexChanged
        If mAllowEvents Then
            refresca()
        End If

    End Sub

    Private Sub Do_Redondeig(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oEmptyContact As Contact = Nothing
        Dim DecDif As Decimal
        Dim DecIvaStandard As Decimal = 0
        Dim DecIvaReduit As Decimal = 0
        Dim DecIvaSuperReduit As Decimal = 0
        Dim DecReqStandard As Decimal = 0
        Dim DecReqReduit As Decimal = 0
        Dim DecReqSuperReduit As Decimal = 0
        Dim iYea As Integer = CurrentYea()

        Dim oCca As Cca
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            DecDif = oRow(Cols.Dif)
            If DecDif <> 0 Then
                Select Case CType(oRow(Cols.CodLin), Bkms)
                    Case Bkms.IvaStandard
                        DecIvaStandard = DecIvaStandard + DecDif
                    Case Bkms.RecarrecStandard
                        DecReqStandard = DecReqStandard + DecDif
                    Case Bkms.IvaReduit
                        DecIvaReduit = DecIvaReduit + DecDif
                    Case Bkms.RecarrecReduit
                        DecReqReduit = DecReqReduit + DecDif
                    Case Bkms.IvaSuperReduit
                        DecIvaSuperReduit = DecIvaSuperReduit + DecDif
                    Case Bkms.RecarrecSuperReduit
                        DecReqSuperReduit = DecReqSuperReduit + DecDif
                End Select

            End If
        Next

        If DecIvaStandard = 0 And DecIvaReduit = 0 And DecIvaSuperReduit = 0 And DecReqStandard = 0 And DecReqReduit = 0 And DecReqSuperReduit = 0 Then Exit Sub

        oCca = New Cca()
        With oCca
            .Ccd = DTOCca.CcdEnum.Manual
            .fch = mLastFch
            .Txt = "redondeig Ivas " & ComboBoxPeriods.Text & "/" & CurrentYea()
            AddCcb(oCca, DTOPgcPlan.Ctas.IvaRepercutitNacional, DecIvaStandard)
            AddCcb(oCca, DTOPgcPlan.ctas.IvaRecarrecEquivalencia, DecReqStandard)
            AddCcb(oCca, DTOPgcPlan.ctas.IvaReduit, DecIvaReduit)
            AddCcb(oCca, DTOPgcPlan.ctas.IvaRecarrecReduit, DecReqReduit)
            AddCcb(oCca, DTOPgcPlan.ctas.IvaSuperReduit, DecIvaSuperReduit)
            AddCcb(oCca, DTOPgcPlan.ctas.IvaRecarrecSuperReduit, DecReqSuperReduit)
            .RestoEnviarA(mPlan.Cta(DTOPgcPlan.ctas.Redondeos), oEmptyContact)
            Dim exs As New List(Of exception)
            If Not .Update(exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With
        refresca()
    End Sub

    Private Sub AddCcb(ByRef oCca As Cca, ByVal oCtaCod As DTOPgcPlan.ctas, ByVal DcQuota As Decimal)
        If DcQuota <> 0 Then
            Dim oEmptyContact As Contact = Nothing
            Dim oCcb As New Ccb(mPlan.Cta(oCtaCod), oEmptyContact, BLLApp.GetAmt(Math.Abs(DcQuota)), IIf(DcQuota < 0, DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber))
            oCca.ccbs.Add(oCcb)
        End If
    End Sub

    Private Sub CheckOrdreCronologic()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CCA.cdn, CCB.fch, CCB.eur ")
        sb.AppendLine("FROM CCA ")
        sb.AppendLine("INNER JOIN CCB ON Ccb.CcaGuid = Cca.Guid ")
        sb.AppendLine("INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid ")
        sb.AppendLine("WHERE CCA.Emp =" & mEmp.Id & " ")
        sb.AppendLine("AND CCB.yea =" & CurrentYea() & " ")
        sb.AppendLine("AND (CCB.fch BETWEEN '" & Format(mFirstFch, "yyyyMMdd") & "' AND '" & Format(mLastFch, "yyyyMMdd") & "') ")
        sb.AppendLine("AND CCA.ccd =" & DTOCca.CcdEnum.FacturaNostre & " ")
        sb.AppendLine("AND PgcCta.Id LIKE '70%' ")
        sb.AppendLine("ORDER BY CCA.cdn")
        Dim iFirstFra As Integer = 0
        Dim iLastFra As Integer = 0
        Dim DtLastFch As Date
        Dim BlFirstRec As Boolean = True
        Dim BlErrs As Boolean = False
        Dim iCount As Integer = 0

        Dim sql As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(sql)
        Do While oDrd.Read
            If BlFirstRec Then
                BlFirstRec = False
                iFirstFra = oDrd("CDN")
                CheckFirstFra(iFirstFra, oDrd("FCH"))
            Else
                If oDrd("CDN") <> (iLastFra + 1) Then
                    Log(LogLineCods._NotSet, Logs.Err, "Salt entre factures " & iLastFra & " i " & oDrd("CDN"))
                    BlErrs = True
                End If
                If oDrd("FCH") < DtLastFch Then
                    Log(LogLineCods._NotSet, Logs.Err, "Fra." & oDrd("CDN") & " de data posterior a fra." & iLastFra)
                    BlErrs = True
                End If
            End If
            iLastFra = oDrd("CDN")
            DtLastFch = oDrd("FCH")
            iCount += 1
        Loop
        If BlFirstRec Then
            Log(LogLineCods._NotSet, Logs.Warn, "No hi ha cap factura registrada a aquest trimestre")
        Else
            If Not BlErrs Then
                Log(LogLineCods._NotSet, Logs.Ok, iCount & " factures consecutives i en ordre. De la " & iFirstFra & " a la " & iLastFra)
            End If
        End If
        oDrd.Close()
    End Sub

    Private Sub CheckFirstFra(ByVal iFirstFraNum As Integer, ByVal DtFch As Date)
        Dim eLog As Logs
        Dim iLastFraLastQuarter As Integer = 0
        Dim DtLastFraLastQuarter As Date
        Dim sTxt As String
        If CurrentMes() = 1 Then
            sTxt = "Primera factura de l'any num." & iFirstFraNum
            If iFirstFraNum = 1 Then
                eLog = Logs.Ok
            Else
                eLog = Logs.Err
            End If
        Else
            Dim SQL As String = "SELECT TOP 1 CCA.cdn, Cca.Fch " _
            & "FROM CCA " _
            & "INNER JOIN CCB ON Ccb.CcaGuid = Cca.Guid " _
            & "INNER JOIN PgcCta ON Ccb.CtaGuid = PgcCta.Guid " _
            & "WHERE CCA.ccd = 10 And " _
            & "CCA.emp =" & mEmp.Id & " And " _
            & "CCA.yea =" & CurrentYea() & " And " _
            & "PgcCta.Id LIKE '70%' AND " _
            & "CCA.fch < '" & Format(DtFch, "yyyyMMdd") & "' " _
            & "ORDER BY CCA.cdn DESC"
            Dim oDrd As SqlClient.SqlDataReader = Dal.SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                iLastFraLastQuarter = oDrd("Cdn")
                DtLastFraLastQuarter = oDrd("Fch")
            End If
            oDrd.Close()
            sTxt = "Primera fra " & iFirstFraNum & " del " & DtFch.ToShortDateString & ". Fra. anterior " & iLastFraLastQuarter & " del " & DtLastFraLastQuarter
            If iFirstFraNum = iLastFraLastQuarter + 1 Then
                eLog = Logs.Ok
            Else
                eLog = Logs.Err
            End If
        End If
        Log(LogLineCods._NotSet, eLog, sTxt)
    End Sub

    Private Sub Log(ByVal oLogLineCod As LogLineCods, ByVal e As Logs, ByVal sTxt As String)
        Dim oTb As DataTable = mDsLog.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow(Cols2.LineCod) = oLogLineCod
        oRow(Cols2.Warn) = e
        oRow(Cols2.Text) = sTxt
        oTb.Rows.Add(oRow)
    End Sub

    Private Sub LoadBancs()
        With ComboBoxBancs
            .DisplayMember = "Abr"
            .DataSource = BLLBancs.All()
        End With
    End Sub

    Private Sub ButtonPayNow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPayNow.Click
        Dim iYea As Integer = CurrentYea()
        Dim iMes As Integer = CurrentMes()
        Dim oEmptyContact As Contact = Nothing
        Dim oCtaIva As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.IvaDeutor)
        Dim DcLiquid As Decimal = mIvaStandard + mIvaReduit + mIvaSuperReduit + mReqStandard + mReqReduit + mReqSuperReduit - mIvaSoportat
        Dim oAmt As DTOAmt
        Dim oCta As PgcCta
        Dim oContact As Contact

        Dim oCca As New Cca()
        With oCca
            .Txt = "Hisenda mod.303 declaració IVA " & ComboBoxPeriods.Text & " " & iYea.ToString
            .fch = mLastFch
            .Ccd = DTOCca.CcdEnum.IVA
            .Cdn = GetCdn()


            'addRepercutit
            If mIvaStandard <> 0 Then
                oAmt = BLLApp.GetAmt(mIvaStandard)
                oCta = mPlan.Cta(DTOPgcPlan.Ctas.IvaRepercutitNacional)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Debe))
            End If

            If mReqStandard <> 0 Then
                oAmt = BLLApp.GetAmt(mReqStandard)
                oCta = mPlan.Cta(DTOPgcPlan.ctas.IvaRecarrecEquivalencia)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Debe))
            End If

            'addReduit
            If mIvaReduit <> 0 Then
                oAmt = BLLApp.GetAmt(mIvaReduit)
                oCta = mPlan.Cta(DTOPgcPlan.ctas.IvaReduit)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Debe))
            End If

            If mReqReduit <> 0 Then
                oAmt = BLLApp.GetAmt(mReqReduit)
                oCta = mPlan.Cta(DTOPgcPlan.ctas.IvaRecarrecReduit)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Debe))
            End If

            'addSuperReduit
            If mIvaSuperReduit <> 0 Then
                oAmt = BLLApp.GetAmt(mIvaSuperReduit)
                oCta = mPlan.Cta(DTOPgcPlan.ctas.IvaSuperReduit)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Debe))
            End If

            If mReqSuperReduit <> 0 Then
                oAmt = BLLApp.GetAmt(mReqSuperReduit)
                oCta = mPlan.Cta(DTOPgcPlan.ctas.IvaRecarrecSuperReduit)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Debe))
            End If

            'addSoportat
            If mIvaSoportat <> 0 Then
                oAmt = BLLApp.GetAmt(mIvaSoportat)
                oCta = mPlan.Cta(DTOPgcPlan.Ctas.IvaRepercutitNacional)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Haber))
            End If

            'addImportacio
            If mIvaImportacio <> 0 Then
                oAmt = BLLApp.GetAmt(mIvaImportacio)
                oCta = mPlan.Cta(DTOPgcPlan.Ctas.IvaSoportatImportacio)
                .ccbs.Add(New Ccb(oCta, oEmptyContact, oAmt, DTOCcb.DhEnum.Haber))
            End If

            'a pagar
            If DcLiquid <> 0 Then
                .RestoEnviarA(oCtaIva, oEmptyContact)
            End If

            Dim exs As New List(Of exception)
            If Not .Update(exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With

        If DcLiquid > 0 Then
            oAmt = BLLApp.GetAmt(DcLiquid)
            oCca = New Cca()
            With oCca
                .Txt = SelectedBanc.Abr & "-liquidació IVA " & ComboBoxPeriods.Text & " " & iYea.ToString
                .fch = DateTimePickerPay.Value
                .Ccd = DTOCca.CcdEnum.Pagament

                'banc
                oCta = mPlan.Cta(DTOPgcPlan.ctas.bancs)
                oContact = New Contact(SelectedBanc.Guid)
                .ccbs.Add(New Ccb(oCta, oContact, oAmt, DTOCcb.DhEnum.Haber))

                'Iva
                .ccbs.Add(New Ccb(oCtaIva, oEmptyContact, oAmt, DTOCcb.DhEnum.Debe))

                Dim exs As New List(Of exception)
                If .Update(exs) Then
                    MsgBox("pagament registrat", MsgBoxStyle.Information, "MAT.NET")
                    Me.Close()
                Else
                    MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If
            End With
        End If
    End Sub

    Private Function SelectedBanc() As DTOBanc
        Dim retval As DTOBanc = ComboBoxBancs.SelectedItem
        Return retval
    End Function

    Private Function GetCdn() As Long
        Return 100 * CurrentYea() + CurrentMes()
    End Function

    Private Function CheckAlreadyRegistered() As Boolean
        Dim retval As Boolean
        Dim oCca As DTOCca = BLLCca.FromCdn(BLLApp.Emp, CurrentYea, DTOCca.CcdEnum.IVA, GetCdn)
        If oCca Is Nothing Then
            LabelPaid.Visible = False
        Else
            BLLCca.Load(oCca)
            ButtonPayNow.Enabled = False
            ComboBoxBancs.Enabled = False
            DateTimePickerPay.Enabled = False
            LabelPaid.Text = "registrat al assentament " & oCca.Id
            retval = True
        End If
        Return retval
    End Function

    Private Sub ToolStripButtonXls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonXls.Click

        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo =
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture =
            New System.Globalization.CultureInfo("en-US")


        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet


        Dim row As Long

        With oSheet
            .Columns(2).ColumnWidth = 45
            row = 1
            .Cells(row, 1) = "MATIAS MASSO, S.A. - DECLARACIO IVA " & ComboBoxPeriods.SelectedText & "." & ComboBoxYeas.SelectedValue
            row = 3
            .Cells(row, 2) = "CONCEPTE"
            .Cells(row, 3) = "BASE"
            .Cells(row, 4) = "TIPUS"
            .Cells(row, 5) = "CUOTA"
            .Range("D3:E3").HorizontalAlignment = Excel.Constants.xlRight


            row = row + 2
            Dim iFirstRowDevengat As Integer = row
            Dim oTb As DataTable = mDs.Tables(0)
            Dim DecQuot As Decimal = 0
            Dim oRow As DataRow
            For Each oRow In oTb.Rows
                Select Case CType(oRow(Cols.CodLin), Bkms)
                    Case Bkms.IvaStandard,
                         Bkms.RecarrecStandard,
                         Bkms.IvaReduit,
                         Bkms.RecarrecReduit,
                         Bkms.IvaSuperReduit,
                         Bkms.RecarrecSuperReduit,
                         Bkms.IvaComunitariRepercutit

                        .Cells(row, 2) = oRow(Cols.Txt)
                        .Cells(row, 3) = oRow(Cols.Base)
                        .Cells(row, 4) = oRow(Cols.Tipus) / 100
                        '.Cells(row, 5).FormulaR1C1 = "=REDONDEAR(RC[-2]*RC[-1];2)"
                        .Cells(row, 5).Formula = Math.Round(oRow(Cols.Base) * oRow(Cols.Tipus) / 100, 2, MidpointRounding.AwayFromZero)

                        row = row + 1
                End Select
            Next

            'INTRASTAT
            'Dim iRowIntrastat As Integer = row
            '.Cells(row, 2) = "INTRACOMUNITARI"
            '.Cells(row, 3) = 0
            '.Cells(row, 4) = maxisrvr.Root.IvaPercent(maxisrvr.Iva.Codis.Standard) / 100
            '.Cells(row, 5).Formula = "=RC[-2]*RC[-1]"

            'row = row + 1

            'DEVENGAT
            Dim iRowTotDevengat As Integer = row
            .Cells(row, 2) = "total devengat"
            .Cells(row, 3) = 0
            .Cells(row, 4) = 0
            .Cells(row, 5).Formula = "=SUM(R[" & iFirstRowDevengat - row & "]C:R[-1]C)"

            row = row + 2

            Dim iFirstRowSoportat As Integer = row
            For Each oRow In oTb.Rows
                Select Case CType(oRow(Cols.CodLin), Bkms)
                    Case Bkms.Soportat, Bkms.Importacio, Bkms.IvaComunitariSoportat
                        .Cells(row, 2) = oRow(Cols.Txt)
                        .Cells(row, 3) = oRow(Cols.Base)
                        .Cells(row, 4) = oRow(Cols.Tipus) / 100
                        '.Cells(row, 5).Formula = "=ROUND(RC[-2]*RC[-1];2)"
                        .Cells(row, 5) = oRow(Cols.Quota)

                        '.Cells(row, 5) = oRow(Cols.Calc)
                        row = row + 1
                End Select
            Next

            'Dim iFirstRowImportacio As Integer = row
            'For Each oRow In oTb.Rows
            ' Select Case CType(oRow(Cols.CodLin), Bkms)
            '     Case Bkms.Importacio
            ' .Cells(row, 2) = oRow(Cols.Txt)
            ' '.CElls(row, 3).FormulaR1C1Local = "=F(0)C(2)/F(0)C(1)"
            ' '.Cells(row, 4) = oRow(Cols.Tipus) / 100
            ' .Cells(row, 5) = oRow(Cols.Calc)
            ' row = row + 1
            'End Select
            'Next

            'INTRASTAT
            '.Cells(row, 2) = "INTRACOMUNITARI"
            '.Cells(row, 3) = "=R[" & iRowIntrastat - row & "]C"
            '.Cells(row, 4) = "=R[" & iRowIntrastat - row & "]C"
            '.Cells(row, 5).Formula = "=R[" & iRowIntrastat - row & "]C"

            'row = row + 1

            'DEDUIBLE
            Dim iRowTotSoportat As Integer = row
            .Cells(row, 2) = "total a deduir"
            .Cells(row, 3) = 0
            .Cells(row, 4) = 0
            .Cells(row, 5).Formula = "=SUM(R[" & iFirstRowSoportat - row & "]C:R[-1]C)"

            row = row + 2

            'DEDUIBLE
            .Cells(row, 2) = "a liquidar"
            .Cells(row, 3) = 0
            .Cells(row, 4) = 0
            .Cells(row, 5).Formula = "=R[" & iRowTotDevengat - row & "]C-R[" & iRowTotSoportat - row & "]C"


            .Range("B:E").NumberFormat = "#,##0.00€;-#,##0.00€;#"
            .Range("D:D").NumberFormat = "0.0%;-0.0%;#"

        End With

        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

    End Sub

    Private Sub DataGridView2_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        Select Case e.ColumnIndex
            Case Cols2.IcoWarn
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Dim oLog As Logs = CType(oRow.Cells(Cols2.Warn).Value, Logs)
                Select Case oLog
                    Case Logs.Ok
                        e.Value = My.Resources.info
                    Case Logs.Warn
                        e.Value = My.Resources.warn
                    Case Logs.Err
                        e.Value = My.Resources.wrong
                End Select
        End Select
    End Sub

    Private Sub ToolStripButtonFitxer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFitxer.Click
        Dim oFitxer As New MaxiSrvr.MatFileAEAT303(CurrentYea, Format(CurrentMes, "00"), BLLApp.Org.Nif, BLLApp.Org.Nom)

        Dim oTb As DataTable = mDs.Tables(0)
        Dim DecQuot As Decimal = 0
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            Select Case CType(oRow(Cols.CodLin), Bkms)
                Case Bkms.IvaStandard
                    oFitxer.SetIVADevengat1(oRow(Cols.Base), oRow(Cols.Tipus), oRow(Cols.Calc))
                Case Bkms.RecarrecStandard
                    oFitxer.SetIVADevengat2(oRow(Cols.Base), oRow(Cols.Tipus), oRow(Cols.Calc))
                Case Bkms.IvaReduit
                    oFitxer.SetIVADevengat3(oRow(Cols.Base), oRow(Cols.Tipus), oRow(Cols.Calc))
                    'Case Bkms.RecarrecReduit
                    'oFitxer.SetIVADevengat4(oRow(Cols.Base), oRow(Cols.Tipus), oRow(Cols.Calc))
                    'Case Bkms.IvaSuperReduit
                    'oFitxer.SetIVADevengat5(oRow(Cols.Base), oRow(Cols.Tipus), oRow(Cols.Calc))
                    'Case Bkms.RecarrecSuperReduit
                    '  oFitxer.SetIVADevengat6(oRow(Cols.Base), oRow(Cols.Tipus), oRow(Cols.Calc))
                    'Case Bkms.IvaReq
                    '   oFitxer.SetRecarrecEquivalencia(oRow(Cols.Base), oRow(Cols.Tipus), oRow(Cols.Calc))
                Case Bkms.Soportat
                    oFitxer.SetIVASoportat(oRow(Cols.Base), oRow(Cols.Sdo))
                Case Bkms.Importacio
                    oFitxer.SetIVAImportacio(oRow(Cols.Base), oRow(Cols.Calc))
                Case Bkms.IvaComunitariRepercutit
                    oFitxer.SetIntraComunitari(oRow(Cols.Base), oRow(Cols.Quota))
            End Select
        Next


        Dim oFrm As New Frm_FileAEAT303
        With oFrm
            .File = oFitxer
            .Show()
        End With
    End Sub

    Private Sub DataGridView1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellChanged
        SetContextMenuMain()
    End Sub

    Private Sub SetContextMenuMain()
        Dim oContextMenu As New ContextMenuStrip
        If DataGridView1.CurrentRow IsNot Nothing Then
            Select Case CType(DataGridView1.CurrentRow.Cells(Cols.CodLin).Value, Bkms)
                Case Bkms.IvaStandard
                    Dim oCta As DTOPgcCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.IvaRepercutitNacional)
                    Dim oCcd As New DTOCcd(CurrentExercici, oCta, Nothing)
                    Dim oMenuCcd As New Menu_Ccd(oCcd)
                    oContextMenu.Items.AddRange(oMenuCcd.Range)
                Case Bkms.RecarrecStandard
                    Dim oCta As DTOPgcCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.IvaRecarrecEquivalencia)
                    Dim oCcd As New DTOCcd(CurrentExercici, oCta, Nothing)
                    Dim oMenuCcd As New Menu_Ccd(oCcd)
                    oContextMenu.Items.AddRange(oMenuCcd.Range)
                Case Bkms.Soportat
                    Dim oCta As DTOPgcCta = BLLPgcCta.FromCod(DTOPgcPlan.Ctas.IvaRepercutitNacional)
                    Dim oCcd As New DTOCcd(CurrentExercici, oCta, Nothing)
                    Dim oMenuCcd As New Menu_Ccd(oCcd)
                    oContextMenu.Items.AddRange(oMenuCcd.Range)
            End Select
        End If
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView2_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.CurrentCellChanged
        SetContextMenuLog()
    End Sub

    Private Sub SetContextMenuLog()
        Dim oContextMenu As New ContextMenuStrip
        If DataGridView2.CurrentRow IsNot Nothing Then
            Select Case CType(DataGridView2.CurrentRow.Cells(Cols2.LineCod).Value, LogLineCods)
                Case LogLineCods.Redondeig
                    oContextMenu.Items.Add("fer assentament", My.Resources.Gears, AddressOf Do_Redondeig)
            End Select
        End If
        DataGridView2.ContextMenuStrip = oContextMenu
    End Sub

End Class