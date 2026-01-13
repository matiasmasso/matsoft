

Public Class Frm_Transmisions_Old
    Private mAllowEvents As Boolean
    Private mTransmisio As Transmisio

    Private Enum Cols
        Guid
        Id
        Fch
        Albs
        Lins
        Uds
        Eur
    End Enum

    Public WriteOnly Property Yea() As Integer
        Set(ByVal Value As Integer)
            Xl_Yea1.Yea = Value
            LoadGrid()
        End Set
    End Property


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Transm.Guid, TRANSM.TRANSM, TRANSM.FCH, TRANSM.ALBS, SUM(ARCS.LINEAS) AS LINEAS, SUM(ARCS.UDS) AS UDS, TRANSM.VAL " _
        & "FROM TRANSM INNER JOIN " _
        & "ALB ON Alb.TransmGuid=Transm.Guid INNER JOIN " _
        & "(SELECT ARC.AlbGuid, COUNT(LIN) AS LINEAS, SUM(QTY) AS UDS FROM ARC GROUP BY AlbGuid) AS ARCS ON ALB.Guid=ARCS.AlbGuid " _
        & "WHERE " _
        & "TRANSM.EMP=@Emp AND TRANSM.YEA=@Yea " _
        & "GROUP BY Transm.Guid, TRANSM.TRANSM, TRANSM.FCH, TRANSM.ALBS, TRANSM.VAL " _
        & "ORDER BY TRANSM.TRANSM DESC"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Emp", App.Current.Emp.Id, "@Yea", Xl_Yea1.Yea)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Num"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 110
                .DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Albs)
                .HeaderText = "Albarans"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Lins)
                .HeaderText = "Linies"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(Cols.Uds)
                .HeaderText = "Unitats"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

        mAllowEvents = True
    End Sub


    Private Function CurrentTransm() As Transmisio
        Dim oTransm As Transmisio = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
            oTransm = TransmisioLoader.Find(oGuid)
        End If
        Return oTransm
    End Function


    Private Function CurrentYea() As Integer
        Return Xl_Yea1.Yea
    End Function

    Private Sub Xl_Yea1_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_Yea1.AfterUpdate
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub AnysegüentToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnysegüentToolStripButton.Click
        Xl_Yea1.Yea = Xl_Yea1.Yea + 1
        LoadGrid()
    End Sub

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Xl_Yea1.Yea = Xl_Yea1.Yea - 1
        LoadGrid()
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oTransm As Transmisio = CurrentTransm()

        If oTransm IsNot Nothing Then

            Dim oMenu_Transm As New Menu_Transmisio_Old(oTransm)
            AddHandler oMenu_Transm.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Transm.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        LoadGrid()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oTransmisio As New DTOTransmisio(CurrentTransm.Guid)
        Dim oFrm As New Frm_Transmisio(oTransmisio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Id

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripButtonAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonAddNew.Click
        'Dim oTransm As New Transmisio(App.Current.Emp.DefaultMgz)
        'oTransm.Mgz =BLL.BLLApp.Emp.DefaultMgz
        'oTransm.Fch = Now
        Dim oFrm As New Frm_Transmisio_New(New Mgz(BLL.BLLApp.Mgz.Guid))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
End Class