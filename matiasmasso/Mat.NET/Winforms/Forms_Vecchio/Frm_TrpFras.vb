

Public Class Frm_TrpFras
    Private mTrp As Transportista

    Private Enum Cols
        Yea
        Id
        Fch
        Fra
        Albs
        Valor
        Bts
        M3
        Kgs
        Ports
    End Enum

    Public WriteOnly Property Trp() As Transportista
        Set(ByVal value As Transportista)
            If value IsNot Nothing Then
                mTrp = value
                PictureBoxTrpLogo.Image = mTrp.Img48
                Me.Text = Me.Text & " " & mTrp.Nom
                LoadGrid()
            End If
        End Set
    End Property


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT TrpFra.yea, TrpFra.Id, TrpFra.fch, TrpFra.num, COUNT(ALB.alb) AS ALBS, SUM(ALB.eur) AS EUR, " _
        & "SUM(ALB.Bultos) AS BULTOS, SUM(ALB.M3) AS M3, " _
        & "SUM(ALB.Kgs) AS KGS, SUM(ALB.Pq_ports) AS PORTS " _
        & "FROM TrpFra INNER JOIN " _
        & "ALB ON TrpFra.Emp = ALB.Emp AND TrpFra.yea = ALB.Pq_TrpFra_yea AND TrpFra.Id = ALB.Pq_TrpFra_Id " _
        & "WHERE TRPFRA.EMP=" & BLLApp.Emp.Id & " AND " _
        & "TRPFRA.TRP=" & mTrp.Id & " " _
        & "GROUP BY TrpFra.yea, TrpFra.Id, TrpFra.fch, TrpFra.num " _
        & "ORDER BY TrpFra.fch DESC, TrpFra.num DESC"
        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Albs)
                .HeaderText = "albarans"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Valor)
                .HeaderText = "valor"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Bts)
                .HeaderText = "bultos"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.M3)
                .HeaderText = "volum"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,### m3"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Kgs)
                .HeaderText = "pes"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,### Kg"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Ports)
                .HeaderText = "ports"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub


    Private Function CurrentTrpFra() As TrpFra
        Dim oTrpFra As TrpFra = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = oRow.Cells(Cols.Yea).Value
            Dim iId As Integer = oRow.Cells(Cols.Id).Value
            oTrpFra = New TrpFra(iYea, iId)
        End If
        Return oTrpFra
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oTrpFra As TrpFra = CurrentTrpFra()
        Zoom(oTrpFra)
    End Sub

    Private Sub ToolStripButtonZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonZoom.Click
        Dim oTrpFra As TrpFra = CurrentTrpFra()
        Zoom(oTrpFra)
    End Sub

    Private Sub Zoom(ByVal oTrpFra As TrpFra)
        'Dim oFrm As New Frm_TrpFra
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'With oFrm
        '.TrpFra = oTrpFra
        '.Show()
        'End With
    End Sub

    Private Sub ToolStripButtonNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonNew.Click
        Dim oTrpFra As New TrpFra(mTrp)
        With oTrpFra
            .Fch = Today
        End With
        Zoom(oTrpFra)
    End Sub

    Private Sub Del_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ToolStripButtonDel.Click
        Dim oTrpFra As TrpFra = CurrentTrpFra()
        Dim rc As MsgBoxResult = MsgBox("eliminem fra." & oTrpFra.FraNum & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            oTrpFra.Delete()
            MsgBox("Factura " & oTrpFra.FraNum & " eliminada", MsgBoxStyle.Information, "MAT.NET")
            RefreshRequest(sender, e)
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Information, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

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

End Class