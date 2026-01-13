

Public Class Frm_PqHdr
    Private mPqHdr As PqHdr
    Private mEmp as DTOEmp

    Private Enum Cols
        Alb
        Fch
        Bultos
        Kg
        Eur
        Dest
    End Enum

    Public WriteOnly Property PqHdr() As PqHdr
        Set(ByVal Value As PqHdr)
            mPqHdr = Value
            With mPqHdr
                mEmp = .Mgz.Emp
                Me.Text = "REMESA TRANSPORTISTA " & .Id
                LabelFch.Text = .Fch
                LabelMgz.Text = .Mgz.Nom_o_NomComercial
                LabelTrp.Text = .Trp.Nom_o_NomComercial
            End With
            LoadGrid()
        End Set
    End Property


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ALB,FCH,BULTOS,KGS,PQ_PORTS,CLX FROM ALB INNER JOIN " _
        & "CLX ON ALB.CliGuid=CLX.Guid " _
        & "WHERE " _
        & "ALB.Emp =" & mEmp.Id & " AND " _
        & "ALB.Pq_Yea =" & mPqHdr.Yea & " AND " _
        & "ALB.Pq_Id =" & mPqHdr.Id & " AND " _
        & "ALB.mgz =" & mPqHdr.Mgz.Id & " " _
        & "ORDER BY alb.YEA, alb.ALB"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
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

            With .Columns(Cols.Alb)
                .Width = 50
                .HeaderText = "albará"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .Width = 70
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Bultos)
                .HeaderText = "bultos"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Kg)
                .HeaderText = "Kg"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "despeses"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Dest)
                .HeaderText = "destinació"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = CDate(oRow.Cells(Cols.Fch).Value).Year
            Dim LngId As Long = oRow.Cells(Cols.Alb).Value
            oAlb = MaxiSrvr.Alb.FromNum(BLL.BLLApp.Emp, iYea, LngId)
        End If
        Return oAlb
    End Function

    Private Function CurrentAlbs() As Albs
        Dim oAlbs As New Albs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim iYea As Integer
            Dim LngId As Integer
            Dim oAlb As Alb
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                iYea = CDate(oRow.Cells(Cols.Fch).Value).Year
                LngId = oRow.Cells(Cols.Alb).Value
                oAlb = MaxiSrvr.Alb.FromNum(mEmp, iYea, LngId)
                oAlbs.Add(oAlb)
            Next
            oAlbs.Sort()
        Else
            Dim oAlb As Alb = CurrentAlb()
            If oAlb IsNot Nothing Then
                oAlbs.Add(CurrentAlb)
            End If
        End If
        Return oAlbs
    End Function

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlbs As Albs = CurrentAlbs()

        If oAlbs.Count > 0 Then
            Dim oMenu_Alb As New Menu_Alb(oAlbs)
            AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Alb.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Alb
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowAlb()
    End Sub

    Private Sub ShowAlb()
        Dim oFrm As New Frm_AlbNew2(CurrentAlb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowAlb()
            e.Handled = True
        End If
    End Sub


End Class
