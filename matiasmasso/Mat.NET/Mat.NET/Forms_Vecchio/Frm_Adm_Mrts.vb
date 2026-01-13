

Public Class Frm_Adm_Mrts

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsCta As DataSet
    Private mDsItm As DataSet
    Private mAllowEvents As Boolean
    Private mLastYearAmortitzat As Integer

    Private Enum ColsMain
        PgcPlan
        Id
        Dsc
        Inmvovilitzat
        CurrentYea
        Amortitzat
        Pendent
        Itm
        Baixa
    End Enum

    Private Enum ColsDetail
        Id
        Ico
        Pdf
        Dsc
        Inmvovilitzat
        CurrentYea
        Amortitzat
        Pendent
        Itm
        Baixa
    End Enum

    Private Sub Frm_Adm_Mrts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetYeas()
        refresca()
    End Sub

    Private Sub refresca()
        mAllowEvents = False
        SetCtas(CurrentYea)
        LoadItms(CurrentYea, CurrentCta)
        mAllowEvents = True
        refrescaLastYear()
    End Sub


    Private Sub refrescaLastYear()
        mLastYearAmortitzat = LastYearAmortitzat()
        SetToolStripButtonAmortitzar()
        SetToolStripButtonRetroceder()
    End Sub

    Private Sub SetToolStripButtonRetroceder()
        With ToolStripButtonRetroceder
            .Text = "retrocedir " & mLastYearAmortitzat
            .Enabled = (mLastYearAmortitzat > BLL.BLLDefault.EmpValue(DTODefault.Codis.LastBlockedCcaYea))
        End With
    End Sub

    Private Sub SetToolStripButtonAmortitzar()
        ToolStripButtonAmortitzar.Text = "amortitzar " & mLastYearAmortitzat + 1
    End Sub

    Private Function LastYearAmortitzat() As Integer
        Dim iYea As Integer = Today.Year
        Dim oPlan As PgcPlan = PgcPlan.FromToday
        Dim SQL As String = "SELECT TOP 1 YEA FROM CCA WHERE " _
            & "EMP=" & mEmp.Id & " AND " _
            & "CCD=" & DTOCca.CcdEnum.Amortitzacions & " " _
            & "ORDER BY YEA DESC"

        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        If oDrd.Read Then
            iYea = oDrd("YEA")
        End If
        oDrd.Close()
        Return iYea
    End Function

    Private Function CurrentYea() As Integer
        Return ComboBoxYea.Text
    End Function

    Private Function CurrentCta() As PgcCta
        Dim oCta As PgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCtas.CurrentRow
        If oRow IsNot Nothing Then
            Dim sCta As String = oRow.Cells(ColsMain.Id).Value
            oCta = MaxiSrvr.PgcCta.FromNum(PgcPlan.FromYear(CurrentYea), sCta)
        End If
        Return oCta
    End Function

    Private Function CurrentItm() As Mrt
        Dim oMrt As Mrt = Nothing
        Dim oRow As DataGridViewRow = DataGridViewItms.CurrentRow
        If oRow IsNot Nothing Then
            Dim iMrt As Integer = oRow.Cells(ColsDetail.Itm).Value
            oMrt = New Mrt(mEmp, iMrt)
        End If
        Return oMrt
    End Function

    Private Sub SetYeas()
        Dim SQL As String = "SELECT TOP 1 FCH FROM MRT " _
        & "WHERE EMP=" & mEmp.Id & " " _
        & "ORDER BY FCH"

        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Dim iMinYea As Integer = Today.Year - 20
        If oDrd.Read Then
            Dim DtFch As Date = oDrd("FCH")
            Dim iYea As Integer = DtFch.Year
            If iYea > iMinYea Then iMinYea = iYea
        End If
        oDrd.Close()

        Dim i As Integer
        ComboBoxYea.Items.Clear()
        For i = Today.Year To iMinYea Step -1
            ComboBoxYea.Items.Add(i)
        Next
        ComboBoxYea.SelectedIndex = 1
    End Sub

    Private Sub SetCtas(ByVal IntYea As Integer)
        Dim SQL As String = "SELECT M1.PGCPLAN, M1.cta, '' as DSC, M1.INMOV, CURRENTYEA, M2.AMORT, " _
        & "(CASE WHEN M2.AMORT IS NULL THEN M1.INMOV ELSE(M1.INMOV-M2.AMORT) END) AS PENDENT,0 AS ITM,'' AS BAIXA FROM " _
        & "(SELECT PGCPLAN, Emp, cta, SUM(CASE WHEN (BAIXAYEA=0 OR BAIXAYEA>" & IntYea & ") THEN EUR ELSE 0 END) AS INMOV FROM MRT " _
        & "WHERE(Year(fch) <=" & IntYea & ") " _
        & "GROUP BY PGCPLAN, Emp, cta) M1 " _
        & "LEFT OUTER JOIN " _
        & "(SELECT  MRT.EMP, CTA, SUM(CASE WHEN MRT.BAIXACCA=0 AND Year(Mr2.Fch)=" & IntYea & " then MR2.eur else 0 end) AS CURRENTYEA, " _
        & "SUM(CASE WHEN MRT.BAIXACCA=0 THEN MR2.eur ELSE 0 END) AS AMORT FROM MRT INNER JOIN " _
        & "MR2 ON MRT.EMP = MR2.EMP AND MRT.ITM = MR2.ITM " _
        & "WHERE Year(Mr2.Fch) <=" & IntYea & " " _
        & "GROUP BY MRT.EMP, MRT.CTA) M2 ON M1.Emp = M2.EMP AND M1.cta = M2.CTA " _
        & "WHERE M1.EMP=" & mEmp.Id & " " _
        & "ORDER BY M1.cta"
        mDsCta = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsCta.Tables(0)
        Dim oRow As DataRow
        Dim oCta As PgcCta
        For Each oRow In oTb.Rows
            oCta = MaxiSrvr.PgcCta.FromNum(CInt(oRow("PGCPLAN")), oRow("CTA"))
            oRow("DSC") = oCta.Nom
        Next

        With DataGridViewCtas
            With .RowTemplate
                .Height = DataGridViewCtas.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(ColsMain.PgcPlan)
                .Visible = False
            End With
            With .Columns(ColsMain.Id)
                .HeaderText = "COMPTE"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsMain.Dsc)
                .HeaderText = "CONCEPTE"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsMain.Inmvovilitzat)
                .HeaderText = "INMOVILITZAT"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsMain.CurrentYea)
                .HeaderText = "AMORT." & IntYea
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsMain.Amortitzat)
                .HeaderText = "TOTAL AMORT."
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsMain.Pendent)
                .HeaderText = "PENDENT"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsMain.Itm)
                .Visible = False
            End With
            With .Columns(ColsMain.Baixa)
                .HeaderText = ""
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            If .Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(ColsMain.Dsc)
            End If
        End With

        'SetFooters(oTb, DataGridViewCtas)

    End Sub

    Private Sub LoadItms(ByVal IntYea As Integer, ByVal oCta As PgcCta)
        Dim SQL As String = "SELECT mrt.fch, " _
        & "(CASE WHEN ALTA.Hash IS NULL THEN 0 ELSE 1 END) AS PDF, " _
        & "DSC, MRT.Eur, M2.CURRENTYEA, M2.AMORT, (CASE WHEN M2.AMORT IS NULL THEN MRT.EUR ELSE (MRT.EUR-M2.AMORT) END) AS PENDENT, MRT.Itm, CCA.FCH AS BAIXA " _
        & "FROM MRT LEFT OUTER JOIN " _
        & "(SELECT  EMP, ITM, SUM(CASE WHEN Year(Fch)=" & IntYea & " then eur else 0 end) AS CURRENTYEA, SUM(eur) AS AMORT FROM MR2 " _
        & "WHERE EMP=" & mEmp.Id & " AND " _
        & "Year(Fch) <=" & IntYea & " " _
        & "GROUP BY EMP, ITM) M2 " _
        & "ON MRT.Emp = M2.EMP AND MRT.ITM=M2.ITM LEFT OUTER JOIN " _
        & "CCA ALTA ON MRT.EMP=ALTA.EMP AND MRT.AltaYea=ALTA.YEA AND MRT.AltaCca=ALTA.CCA LEFT OUTER JOIN " _
        & "CCA ON MRT.EMP=CCA.EMP AND MRT.BaixaYea=CCA.YEA AND MRT.BaixaCca=CCA.CCA " _
        & "WHERE MRT.CTA LIKE '" & oCta.Id & "' AND " _
        & "MRT.ALTAYEA<=" & IntYea & " " _
        & "ORDER BY (CASE WHEN MRT.BAIXACCA=0 THEN 0 ELSE 1 END), MRT.FCH"
        mDsItm = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsItm.Tables(0)

        'afegeix columna pdf
        Dim oCol As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColsDetail.Ico)

        With DataGridViewItms
            With .RowTemplate
                .Height = DataGridViewCtas.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(ColsDetail.Id)
                .Width = 70
                .HeaderText = "DATA"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsDetail.Pdf)
                .Visible = False
            End With
            With .Columns(ColsDetail.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsDetail.Dsc)
                .HeaderText = "PARTIDA"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsDetail.Inmvovilitzat)
                .HeaderText = "ADQUISICIO"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDetail.CurrentYea)
                .HeaderText = "AMORT." & IntYea
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDetail.Amortitzat)
                .HeaderText = "TOTAL AMORT."
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDetail.Pendent)
                .HeaderText = "INVENTARI"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDetail.Itm)
                .Visible = False
            End With
            With .Columns(ColsDetail.Baixa)
                .HeaderText = "BAIXA"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            'SetFooters(oTb, DataGridViewItms)

        End With

    End Sub


    Private Sub ComboBoxYea_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYea.SelectedValueChanged
        If mAllowEvents Then
            mAllowEvents = False
            SetCtas(CurrentYea)
            LoadItms(CurrentYea, CurrentCta)
            mAllowEvents = True
        End If
    End Sub


    Private Sub ToolStripButtonAmortitzar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonAmortitzar.Click
        Dim s As String = MaxiSrvr.Emp.FromDTOEmp(mEmp).Amortitza(mLastYearAmortitzat + 1)
        MsgBox(s, MsgBoxStyle.Information, "MAT.NET")
        refrescaLastYear()
    End Sub

    Private Sub DataGridViewCtas_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewCtas.SelectionChanged
        If mAllowEvents Then
            LoadItms(CurrentYea, CurrentCta)
        End If
    End Sub


    Private Sub SetContextMenuItms()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMrt As Mrt = CurrentItm()

        If oMrt IsNot Nothing Then
            Dim oMenu_Mrt As New Menu_Mrt(oMrt)
            AddHandler oMenu_Mrt.AfterUpdate, AddressOf RefreshRequestItms
            oContextMenu.Items.AddRange(oMenu_Mrt.Range)
        End If

        DataGridViewItms.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequestItms(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsDetail.Dsc
        Dim oGrid As DataGridView = DataGridViewItms


        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadItms(CurrentYea, CurrentCta)

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridViewItms_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewItms.CellFormatting
        Select Case e.ColumnIndex
            Case ColsDetail.Ico
                Dim oRow As DataGridViewRow = DataGridViewItms.Rows(e.RowIndex)
                If oRow.Cells(ColsDetail.Pdf).Value = 1 Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub DataGridViewItms_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewItms.DoubleClick
        Dim oMrt As Mrt = CurrentItm()
        If oMrt IsNot Nothing Then
            Dim oFrm As New Frm_Mrt
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestItms
            With oFrm
                .Mrt = oMrt
                .Show()
            End With
        End If
    End Sub


    Private Sub DataGridViewItms_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewItms.SelectionChanged
        SetContextMenuItms()
    End Sub

    Private Sub ToolStripButtonRetroceder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRetroceder.Click
        Dim rc As MsgBoxResult = MsgBox("Permís per retrocedir totes les amortitzacions" & vbCrLf & "de l'any " & mLastYearAmortitzat, MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)

            Dim SQL As String = "DELETE MR2 WHERE EMP=" & mEmp.Id & " AND YEAR(FCH)=" & mLastYearAmortitzat.ToString
            MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi)

            SQL = "SELECT YEA,CCA FROM CCA WHERE " _
            & "EMP=" & mEmp.Id & " AND " _
            & "CCD=" & DTOCca.CcdEnum.Amortitzacions & " AND " _
            & "YEA=" & mLastYearAmortitzat.ToString
            Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
            Dim oCca As Cca = Nothing
            Do While oDrd.Read
                oCca = MaxiSrvr.Cca.FromNum(mEmp, oDrd("YEA"), oDrd("CCA"))
                ToolStripStatusLabel1.Text = "retrocedin " & oCca.Id & " " & oCca.fch.ToShortDateString & " " & oCca.Txt
                Application.DoEvents()

                oCca.Delete( exs)
            Loop
            refrescaLastYear()
        End If
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        refresca()
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDsItm).Visible = True
    End Sub

    Private Sub ToolStripButtonAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonAddNew.Click
        Dim oMrt As New Mrt(mEmp, , CurrentCta)
        oMrt.Fch = Today
        'oMrt.Alta = Today
        oMrt.Amt = New Amt
        Dim oFrm As New Frm_Mrt
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestItms
        With oFrm
            .Mrt = oMrt
            .Show()
        End With
    End Sub


    Private Sub DataGridViewItms_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewItms.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridViewItms.Rows(e.RowIndex)
        Dim sBaixa As String = oRow.Cells(ColsDetail.Baixa).FormattedValue
        If sBaixa > "" Then
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

End Class
