

Public Class Frm_Cli_Stat_StpMes
    Private mClient As Client
    Private mAllowEvents As Boolean
    Private mDs As DataSet = Nothing
    Private mTotRowTitle = "totals"

    Private Enum Cols
        TpaId
        TpaNom
        StpId
        StpNom
        Qty3
        Eur3
        Qty2
        Eur2
        Qty1
        Eur1
        LinCod
    End Enum

    Private Enum LinCods
        Standard
        Totals
    End Enum

    Public WriteOnly Property Client() As Client
        Set(ByVal Value As Client)
            If Not Value Is Nothing Then
                mClient = Value
                Me.Text = mClient.Nom
                LoadYeas()
                loadgrid()
                mAllowEvents = True
            End If
        End Set
    End Property

    Private Function NewTotRow(ByVal oSrcRow As DataRow) As DataRow
        Dim oRowTot As DataRow = oSrcRow.Table.NewRow
        oRowTot(Cols.TpaId) = oSrcRow(Cols.TpaId)
        oRowTot(Cols.TpaNom) = oSrcRow(Cols.TpaNom)
        oRowTot(Cols.StpNom) = mTotRowTitle
        oRowTot(Cols.Eur3) = 0
        oRowTot(Cols.Eur2) = 0
        oRowTot(Cols.Eur1) = 0
        Return oRowTot
    End Function

    Private Sub LoadGrid()
        Dim iYea As Integer = CurrentYea()
        mDs = mClient.StatYeasArcDataset(iYea)
        Dim oTb As DataTable = mDs.Tables(0)
        If oTb.Rows.Count > 0 Then


            Dim iRow As Integer = 0
            Dim oRowTot As DataRow = Nothing

            oRowTot = NewTotRow(oTb.Rows(iRow))
            oTb.Rows.InsertAt(oRowTot, iRow)
            Do
                If oRowTot(Cols.TpaId) <> oTb.Rows(iRow)(Cols.TpaId) Then
                    oRowTot = NewTotRow(oTb.Rows(iRow))
                    oTb.Rows.InsertAt(oRowTot, iRow)
                    iRow += 1
                End If
                If Not IsDBNull(oTb.Rows(iRow)(Cols.Eur3)) Then
                    oRowTot(Cols.Eur3) += oTb.Rows(iRow)(Cols.Eur3)
                End If
                If Not IsDBNull(oTb.Rows(iRow)(Cols.Eur3)) Then
                    oRowTot(Cols.Eur2) += oTb.Rows(iRow)(Cols.Eur2)
                End If
                If Not IsDBNull(oTb.Rows(iRow)(Cols.Eur3)) Then
                    oRowTot(Cols.Eur1) += oTb.Rows(iRow)(Cols.Eur1)
                End If
                iRow += 1
                If iRow = oTb.Rows.Count Then Exit Do
            Loop

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

                With .Columns(Cols.TpaId)
                    .Visible = False
                End With
                With .Columns(Cols.TpaNom)
                    .HeaderText = "marca"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With
                With .Columns(Cols.StpId)
                    .Visible = False
                End With
                With .Columns(Cols.StpNom)
                    .HeaderText = "producte"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
                With .Columns(Cols.Qty3)
                    .HeaderText = "uds " & iYea.ToString
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.Eur3)
                    .HeaderText = "eur " & iYea.ToString
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.Qty2)
                    .HeaderText = "uds " & (iYea - 1).ToString
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.Eur2)
                    .HeaderText = "eur " & (iYea - 1).ToString
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.Qty1)
                    .HeaderText = "uds " & (iYea - 2).ToString
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols.Eur1)
                    .HeaderText = "eur " & (iYea - 2).ToString
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With

            End With
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        Select Case oRow.Cells(Cols.StpNom).Value
            Case mTotRowTitle
                oRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue
            Case Else
                oRow.DefaultCellStyle.BackColor = System.Drawing.Color.White
        End Select

    End Sub

    Private Function CurrentYea() As Integer
        Dim retval As Integer
        If IsNumeric(ComboBoxYea.Text) Then
            retval = CInt(ComboBoxYea.Text)
        End If
        Return retval
    End Function

    Private Sub LoadYeas()
        Dim oTb As DataTable = mClient.StatYeas.Tables(0)
        With ComboBoxYea
            .DataSource = oTb
            .ValueMember = "yea"
            .DisplayMember = "yea"
            If oTb.Rows.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With
    End Sub

    Private Function CurrentStp() As Stp
        Dim oStp As Stp = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oTpa As Tpa = Tpa.FromNum(BLL.BLLApp.Emp, oRow.Cells(Cols.TpaId).Value)
            Dim StpId As Integer = oRow.Cells(Cols.StpId).Value
            oStp = New Stp(oTpa, StpId)
        End If
        Return oStp
    End Function

    Private Sub ComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub
End Class
