Imports System.Data
Imports System.Data.SqlClient



Public Class Frm_Intrastats
    Private _emp as DTOEmp
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        mes
        remeses
        Amt
    End Enum

    Private Sub Frm_Intrastats_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _Emp =BLL.BLLApp.Emp
        LoadYeas()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim iYea As Integer = CurrentYea()
        Dim SQL As String = "SELECT INTRASTAT.GUID, INTRASTAT.MES, COUNT(DISTINCT IMPORTDTL.ID), SUM(CCB.EUR) AS AMT " _
        & "FROM INTRASTAT INNER JOIN IMPORTDTL ON INTRASTAT.GUID = IMPORTDTL.INTRASTAT INNER JOIN " _
        & "CCA ON IMPORTDTL.GUID = CCA.GUID INNER JOIN " _
        & "CCB ON Ccb.CcaGuid = Cca.Guid AND CCB.CTA LIKE '6%' " _
        & "WHERE INTRASTAT.EMP=@emp AND INTRASTAT.YEA=@yea " _
        & "GROUP BY INTRASTAT.MES,INTRASTAT.GUID " _
        & "ORDER BY INTRASTAT.MES DESC"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@emp", CInt(_Emp.Id).ToString, "@yea", iYea.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        ZoomToolStripMenuItem.Enabled = (oTb.Rows.Count > 0)

        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                .Height = 15 ' DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.mes)
                .HeaderText = "mes"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.remeses)
                .HeaderText = "partides"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With


    End Sub

    Private Sub LoadYeas()
        Dim SQL As String = "SELECT YEA FROM INTRASTAT WHERE EMP=@EMP GROUP BY YEA ORDER BY YEA DESC"
        Dim oDrd As SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@EMP", CInt(_Emp.Id).ToString)
        Do While oDrd.Read
            ComboBoxYea.Items.Add(oDrd("YEA"))
        Loop
        ComboBoxYea.SelectedIndex = 0
        oDrd.Close()
    End Sub

    Private Function CurrentYea() As Integer
        Dim RetVal As Integer = 0
        If ComboBoxYea.Items.Count > 0 Then
            If IsNumeric(ComboBoxYea.SelectedItem) Then
                RetVal = CInt(ComboBoxYea.SelectedItem)
            End If
        End If
        Return RetVal
    End Function

    Private Function CurrentItem() As Intrastat
        Dim RetVal As Intrastat = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            RetVal = New Intrastat(oGuid)
        End If
        Return RetVal
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If CurrentItem() IsNot Nothing Then
            Zoom()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        ZoomToolStripMenuItem.Enabled = (CurrentItem() IsNot Nothing)
    End Sub

    Private Sub ZoomToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZoomToolStripMenuItem.Click
        Zoom()
    End Sub

    Private Sub Zoom()
        Dim oIntrastat As Intrastat = CurrentItem()
        Dim oFrm As New Frm_Intrastat(oIntrastat)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub NouIntrastatToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NouIntrastatToolStripMenuItem.Click
        Dim oIntrastat As New Intrastat(_Emp)
        Dim oFrm As New Frm_Intrastat(oIntrastat)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.mes
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Dim iYea As Integer = CurrentYea()
        LoadYeas()
        If iYea > 0 Then ComboBoxYea.SelectedValue = iYea

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

    Private Sub ComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub FitxerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FitxerToolStripMenuItem.Click
        Dim oIntrastat As Intrastat = CurrentItem()
        IntrastatLoader.Load(oIntrastat)

        Dim oDlg As New SaveFileDialog
        With oDlg
            .FileName = oIntrastat.DefaultFileName
            .DefaultExt = ".TXT"
            .Filter = "fitxers ASCII (*.TXT)|*.TXT|(tots els fitxers)|*.*"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim exs As New List(Of Exception)
                If Not oIntrastat.SaveAs(.FileName, exs) Then
                    UIHelper.WarnError(exs)
                End If
            End If

        End With
    End Sub

End Class