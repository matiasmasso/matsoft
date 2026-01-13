

Public Class Frm_RepRetencions
    Private mRep As rep

    Private Enum Cols
        yea
        trimestre
        fras
        com
        irpf
        iva
    End Enum

    Public WriteOnly Property Rep() As Rep
        Set(ByVal value As Rep)
            mRep = value
            LabelRepNom.Text = mRep.Abr
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT  yea AS [any], " _
        & "'T' + CAST(DATEPART(Q, Fch) AS VARCHAR) AS trimestre, " _
        & "SUM(BaseFras) AS facturas, " _
        & "SUM(ComisioEur) AS comisions, " _
        & "SUM(ComisioEur * IRPFPct / 100) AS IRPF, " _
        & "SUM(ComisioEur * IVAPct / 100) AS IVA " _
        & "FROM RepLiq " _
        & "WHERE RepGuid = @RepGuid " _
        & "GROUP BY yea, DATEPART(Q, fch) " _
        & "ORDER BY yea DESC, DATEPART(Q, fch) DESC"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@RepGuid", mRep.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False


            With .Columns(Cols.yea)
                .HeaderText = "any"
                .Width = 50
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.trimestre)
                .HeaderText = "trimestre"
                .Width = 50
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.fras)
                .HeaderText = "facturat"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.com)
                .HeaderText = "comisions"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.irpf)
                .HeaderText = "IRPF"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.iva)
                .HeaderText = "IVA"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub



    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        ShowPdf()
    End Sub

    Private Function CurrentPdf() As PdfRepRetencions
        Dim oPdf As PdfRepRetencions = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = oRow.Cells(Cols.yea).Value
            Dim iQuarter As Integer = CStr(oRow.Cells(Cols.trimestre).Value).Substring(1, 1)
            oPdf = New MaxiSrvr.PdfRepRetencions(mRep, iYea, iQuarter)
        End If
        Return oPdf
    End Function

    Private Sub ShowPdf()
        Dim oPdf As PdfRepRetencions = CurrentPdf()
        If oPdf IsNot Nothing Then
            Dim oStream As Byte() = oPdf.Stream
            root.ShowPdf(oStream)
        End If
    End Sub

    Private Sub ToolStripButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonSave.Click
        Dim oPdf As PdfRepRetencions = CurrentPdf()
        If oPdf IsNot Nothing Then
            Dim oDlg As New SaveFileDialog
            With oDlg
                .DefaultExt = "pdf"
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    oPdf.Save(.FileName)
                End If
            End With
        End If
    End Sub
End Class