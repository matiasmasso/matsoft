

Public Class Frm_Cyc

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsHdr As DataSet
    Private mDsDtl As DataSet
    Private mAllowEvents As Boolean

    Private ColorCash As System.Drawing.Color = Color.FromArgb(230, 245, 255)
    Private ColorCred As System.Drawing.Color = Color.FromArgb(255, 255, 220)
    Private ColorD30 As System.Drawing.Color = Color.FromArgb(245, 245, 245)
    Private ColorD60 As System.Drawing.Color = Color.FromArgb(240, 240, 240)
    Private ColorD90 As System.Drawing.Color = Color.FromArgb(235, 235, 235)
    Private ColorRest As System.Drawing.Color = Color.FromArgb(230, 230, 230)

    Private Enum ColsHdr
        mes
        cash
        credit
        D30
        D60
        D90
        rest
    End Enum

    Private Enum ColsDtl
        fra
        fch
        vto
        cash
        dias
        d30
        d60
        d90
        rest
        clx
    End Enum

    Private Sub Frm_Cyc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBoxMargin.Text = "20"
        LoadYeas()
        LoadGridHdr()
    End Sub

    Private Sub LoadGridHdr()
        Dim SQL As String = "SELECT MONTH(FCH) AS MES, " _
        & "SUM(CASE WHEN FRA.CFP = 3 THEN FRA.PTS ELSE 0 END) AS CASH, " _
        & "SUM(CASE WHEN FRA.CFP <>3 THEN FRA.PTS ELSE 0 END) AS CASH, " _
        & "SUM(CASE WHEN (((VTO - FCH) < " & (31 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D30, " _
        & "SUM(CASE WHEN (((VTO - FCH) BETWEEN " & (31 + CurrentMargin()) & " AND " & (60 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D60, " _
        & "SUM(CASE WHEN (((VTO - FCH) BETWEEN " & (61 + CurrentMargin()) & " AND " & (90 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D90, " _
        & "SUM(CASE WHEN (((VTO - FCH) > " & (90 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS RESTO " _
        & "FROM FRA " _
        & "WHERE EMP=" & mEmp.Id & " AND " _
        & "YEA=" & CurrentYea() & " " _
        & "GROUP BY MONTH(FCH) " _
        & "ORDER BY MONTH(FCH) DESC"

        'ComboBoxYeas.SelectedText & " " _
        mDsHdr = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsHdr.Tables(0)
        With DataGridViewHdr
            With .RowTemplate
                .Height = DataGridViewHdr.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(ColsHdr.mes)
                .HeaderText = "mes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsHdr.cash)
                .HeaderText = "Cash"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.credit)
                .HeaderText = "Crèdit"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.D30)
                .HeaderText = "30 dies"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.D60)
                .HeaderText = "60 dies"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.D90)
                .HeaderText = "90 dies"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.rest)
                .HeaderText = "mès de 90"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            mAllowEvents = True
            If oTb.Rows.Count > 0 Then
                .Rows(0).Selected = True
            End If
        End With
    End Sub

    Private Sub LoadGridPaisos()
        Dim SQL As String = "SELECT CIT.ISOPAIS , " _
        & "SUM(CASE WHEN FRA.CFP = 3 THEN FRA.PTS ELSE 0 END) AS CASH, " _
        & "SUM(CASE WHEN FRA.CFP <>3 THEN FRA.PTS ELSE 0 END) AS CASH, " _
        & "SUM(CASE WHEN (((VTO - FCH) < " & (31 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D30, " _
        & "SUM(CASE WHEN (((VTO - FCH) BETWEEN " & (31 + CurrentMargin()) & " AND " & (60 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D60, " _
        & "SUM(CASE WHEN (((VTO - FCH) BETWEEN " & (61 + CurrentMargin()) & " AND " & (90 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D90, " _
        & "SUM(CASE WHEN (((VTO - FCH) > " & (90 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS RESTO " _
        & "FROM FRA INNER JOIN " _
        & "CliAdr ON FRA.Emp = CliAdr.emp AND FRA.cli = CliAdr.cli AND CliAdr.cod = 1 INNER JOIN " _
        & "CIT ON CliAdr.CitNum = CIT.Id " _
        & "WHERE FRA.EMP=" & mEmp.Id & " AND " _
        & "FRA.YEA=" & CurrentYea() & " AND " _
        & "Month(FRA.FCH) =" & CurrentMes() & " " _
        & "GROUP BY CIT.ISOPAIS " _
        & "ORDER BY CIT.ISOPAIS"

        'ComboBoxYeas.SelectedText & " " _
        mDsHdr = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsHdr.Tables(0)
        With DataGridViewPaisos
            With .RowTemplate
                .Height = DataGridViewPaisos.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(ColsHdr.mes)
                .HeaderText = "pais"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsHdr.cash)
                .HeaderText = "Cash"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.credit)
                .HeaderText = "Crèdit"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.D30)
                .HeaderText = "30 dies"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.D60)
                .HeaderText = "60 dies"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.D90)
                .HeaderText = "90 dies"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.rest)
                .HeaderText = "mès de 90"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            If oTb.Rows.Count > 0 Then
                .Rows(0).Selected = True
            End If
        End With
    End Sub

    Private Sub LoadGridDtl()
        Dim SQL As String = "SELECT fra, fch, vto, " _
        & "(CASE WHEN FRA.CFP = 3 THEN FRA.PTS ELSE 0 END) AS CASH, " _
        & "datediff(dd, fch, vto), " _
        & "(CASE WHEN (((VTO - FCH) < " & (31 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D30, " _
        & "(CASE WHEN (((VTO - FCH) BETWEEN " & (31 + CurrentMargin()) & " AND " & (60 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D60, " _
        & "(CASE WHEN (((VTO - FCH) BETWEEN " & (61 + CurrentMargin()) & " AND " & (90 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS D90, " _
        & "(CASE WHEN (((VTO - FCH) > " & (90 + CurrentMargin()) & ") AND FRA.CFP <> 3) THEN FRA.PTS ELSE 0 END) AS RESTO, " _
        & "CLX.CLX " _
        & "FROM FRA INNER JOIN " _
        & "clx ON fra.emp = clx.emp AND fra.cli = clx.cli INNER JOIN " _
        & "CliAdr ON FRA.Emp = CliAdr.emp AND FRA.cli = CliAdr.cli AND CliAdr.cod = 1 INNER JOIN " _
        & "CIT ON CliAdr.CitNum = CIT.Id " _
        & "WHERE Fra.Emp =" & mEmp.Id & " And " _
        & "Fra.Yea = " & CurrentYea() & " And " _
        & "Month(Fra.Fch) =" & CurrentMes() & " AND " _
        & "CIT.ISOPAIS LIKE '" & CurrentCountry.ISO & "' " _
        & "ORDER BY fra.fra"
        mDsDtl = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsDtl.Tables(0)
        With DataGridViewDtl
            With .RowTemplate
                .Height = DataGridViewDtl.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(ColsDtl.fra)
                .HeaderText = "Factura"
                .Width = 50
            End With
            With .Columns(ColsDtl.fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsDtl.vto)
                .HeaderText = "venciment"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsDtl.cash)
                .HeaderText = "Cash"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.dias)
                .HeaderText = "dies"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.d30)
                .HeaderText = "30 dies"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.d60)
                .HeaderText = "60 dies"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.d90)
                .HeaderText = "90 dies"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.rest)
                .HeaderText = "mès de 90"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentYea() As Integer
        Return ComboBoxYeas.SelectedValue
    End Function

    Private Function CurrentMes() As Integer
        Dim iMes As Integer
        Dim oRow As DataGridViewRow = DataGridViewHdr.CurrentRow
        If oRow IsNot Nothing Then
            iMes = oRow.Cells(ColsHdr.mes).Value
        End If
        Return iMes
    End Function

    Private Function CurrentCountry() As Country
        Dim oCountry As Country = Nothing
        Dim oRow As DataGridViewRow = DataGridViewPaisos.CurrentRow
        If oRow IsNot Nothing Then
            Dim sPais As String = oRow.Cells(ColsHdr.mes).Value
            oCountry = New Country(sPais)
        End If
        Return oCountry
    End Function

    Private Function CurrentMargin() As Integer
        Return TextBoxMargin.Text
    End Function

    Private Sub LoadYeas()
        Dim SQL As String = "SELECT cast(YEA as varchar) AS YEA FROM FRA WHERE EMP=1 GROUP BY YEA ORDER BY YEA DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxYeas
            .DataSource = oDs.Tables(0)
            .DisplayMember = "YEA"
            .ValueMember = "YEA"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub ComboBoxYeas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxYeas.SelectedIndexChanged
        If mAllowEvents Then
            LoadGridHdr()
        End If
    End Sub


    Private Function CurrentFra() As Fra
        Dim oFra As Fra = Nothing
        Dim oRow As DataGridViewRow = DataGridViewDtl.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = oRow.Cells(ColsDtl.fra).Value
            oFra = Fra.FromNum(BLL.BLLApp.Emp, CurrentYea, LngId)
        End If
        Return oFra
    End Function

    Private Function SelectedFras() As Fras
        Dim oFras As New Fras

        If DataGridViewDtl.SelectedRows.Count > 0 Then
            Dim IntYea As Integer = CurrentYea()
            Dim LngId As Integer
            Dim oFra As Fra
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridViewDtl.SelectedRows
                LngId = oRow.Cells(ColsDtl.fra).Value
                oFra = Fra.FromNum(mEmp, IntYea, LngId)
                oFras.Add(oFra)
            Next
            oFras.Sort()
        Else
            Dim oFra As Fra = CurrentFra()
            If oFra IsNot Nothing Then
                oFras.Add(CurrentFra)
            End If
        End If
        Return oFras
    End Function



    Private Sub TextBoxMargin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxMargin.TextChanged
        If mAllowEvents Then
            ButtonMarginRefresh.Enabled = True
            DataGridViewHdr.Visible = False
            DataGridViewDtl.Visible = False
        End If
    End Sub

    Private Sub ButtonMarginRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonMarginRefresh.Click
        DataGridViewHdr.Visible = True
        DataGridViewDtl.Visible = True
        ButtonMarginRefresh.Enabled = False
        LoadGridHdr()
    End Sub


    Private Sub PictureBoxLink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxLink.Click
        Dim oCli As Contact = MaxiSrvr.Contact.FromNum(mEmp, 4991)
        Dim URL As String = oCli.WebSite
        Process.Start("IExplore.exe", URL)
    End Sub

    Private Sub DataGridViewHdr_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewHdr.CellFormatting
        Select Case e.ColumnIndex
            Case ColsHdr.mes
                Dim iMes As Integer = e.Value
                e.Value = App.Current.emp.WinUsr.Lang.MesAbr(iMes)
        End Select
    End Sub

    Private Sub DataGridViewHdr_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewHdr.SelectionChanged
        If mAllowEvents Then
            LoadGridPaisos()
        End If
    End Sub


    Private Sub DataGridViewPaisos_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewPaisos.CellFormatting
        Select Case e.ColumnIndex
            Case ColsHdr.mes
                Dim sPais As String = e.Value
                Dim oCountry As New Country(sPais)
                e.Value = oCountry.Nom(App.Current.Emp.WinUsr.Lang)
        End Select
    End Sub

    Private Sub DataGridViewPaisos_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewPaisos.SelectionChanged
        If mAllowEvents Then
            LoadGridDtl()
        End If
    End Sub
End Class
