

Public Class frm_Banc_Saldos
    Private mEmp as DTOEmp = Nothing
    Private mNow As Date = Now
    Private mAllowEvents As Boolean = False

    Private Enum Cols
        Nom
        Sdo
        Fch
        Cli
    End Enum

    Public Sub New(ByVal oEmp As DTOEmp)
        MyBase.New()
        Me.InitializeComponent()
        mEmp = oEmp
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim         SQL As String = "SELECT CLIBNC.ABR, CAST(0 AS DECIMAL) as SDO, CAST(NULL AS DATE) AS FCH, CLIBNC.CLI FROM CLIBNC " _
        & "WHERE CLIBNC.EMP=@EMP AND CLIBNC.ACTIU=1 ORDER BY (CASE WHEN ISOPAIS LIKE 'ES' THEN '__' ELSE ISOPAIS END), CLIBNC.ORD, CLIBNC.ABR"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            SQL = "SELECT TOP 1 FCH,SDO FROM BANCSDO WHERE EMP=@EMP AND BANC=@BANC ORDER BY FCH DESC"
            Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@BANC", CInt(oRow("CLI")))
            If oDrd.Read Then
                oRow("SDO") = oDrd("SDO")
                oRow("FCH") = Format(CDate(oDrd("FCH")), "dd/MM/yy HH:mm")
            End If
            oDrd.Close()
        Next

        With DataGridView1
            .DataSource = oTb
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(Cols.Sdo)
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Cli)
                .Visible = False
            End With
        End With
        SetContextMenu()
    End Sub

    Private Function CurrentItm() As Banc
        Dim oBanc As Banc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oBanc = MaxiSrvr.Banc.FromNum(mEmp, CInt(oRow.Cells(Cols.Cli).Value))
        End If
        Return oBanc
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oBanc As Banc = CurrentItm()

        If oBanc IsNot Nothing Then
            Dim oMenu_Banc As New Menu_Banc(oBanc)
            oContextMenu.Items.AddRange(oMenu_Banc.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        oRow.Cells(Cols.Fch).Value = mNow
        ButtonOk.Enabled = True
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Extracte(New DTOContact(CurrentItm.Guid))
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        'ho crida DataGridView1_EditingControlShowing
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Sdo
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


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then SetContextMenu()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If IsDBNull(oRow.Cells(Cols.Fch).Value) Then
            Else
                If oRow.Cells(Cols.Fch).Value = mNow Then
                    Dim SQL As String = "INSERT INTO BANCSDO (EMP,BANC,SDO) VALUES (@EMP,@BANC,@SDO)"
                    maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@BANC", oRow.Cells(Cols.Cli).Value, "@SDO", oRow.Cells(Cols.Sdo).Value.ToString.Replace(",", "."))
                End If
            End If
        Next
        Me.Close()
    End Sub



    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub
End Class