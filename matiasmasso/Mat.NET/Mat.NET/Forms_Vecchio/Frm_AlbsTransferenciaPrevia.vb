

Public Class Frm_AlbsTransferenciaPrevia
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsYeas As DataSet
    Private mAllowEvents As Boolean
    Private mRetencioCod As DTODelivery.RetencioCods = DTODelivery.RetencioCods.NotSet
    Private mCod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.NotSet

    Private Enum Cols
        Yea
        Id
        Fch
        Clx
        Eur
        Usr
    End Enum

    Public Sub New(oRetencioCod As DTODelivery.RetencioCods)
        MyBase.New()
        Me.InitializeComponent()
        mRetencioCod = oRetencioCod
    End Sub

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Refresca()
    End Sub

    Private Sub Refresca()
        Select Case mRetencioCod
            Case DTODelivery.RetencioCods.Transferencia
                Me.Text = "Albarans pendents de transferencia previa"
            Case DTODelivery.RetencioCods.VISA
                Me.Text = "Albarans pendents de VISA"
        End Select
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()


        Dim SQL As String = "SELECT YEA,ALB,FCH,CLX,EUR+PT2, " _
        & "(CASE WHEN USR.LOGIN IS NULL THEN CAST(USRCREATED AS VARCHAR) ELSE USR.LOGIN END) AS USR " _
        & "FROM alb INNER JOIN " _
        & "CLX ON ALB.CliGuid=CLX.Guid LEFT JOIN " _
        & "TRP ON ALB.TrpGuid=TRP.Guid LEFT OUTER JOIN " _
        & "EMPUSR ON ALB.UsrCreatedGuid=EmpUsr.ContactGuid LEFT OUTER JOIN " _
        & "USR ON EmpUsr.UsrGuid = Usr.Guid " _
        & "WHERE ALB.EMP=@EMP AND YEA>2006 AND (ALB.COD=2 or alb.cod=" & CInt(DTOPurchaseOrder.Codis.reparacio) & ") AND " _
        & "RETENCIOCOD=" & CInt(mRetencioCod) & " AND " _
        & "EUR>0 " _
        & "ORDER BY YEA DESC, ALB DESC"
        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Emp", mEmp.Id)
        Dim oTb As DataTable = mDs.Tables(0)

        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
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
                .HeaderText = "Albará"
                .Width = 45
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        mAllowEvents = True

    End Sub


    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = oRow.Cells(Cols.Yea).Value
            Dim iId As Integer = oRow.Cells(Cols.Id).Value
            oAlb = MaxiSrvr.Alb.FromNum(BLL.BLLApp.Emp, iYea, iId)
        End If
        Return oAlb
    End Function

    Private Function CurrentAlbs() As Albs
        Dim oAlbs As New Albs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim iYea As Integer
            Dim iId As Integer
            Dim oAlb As Alb
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                iYea = oRow.Cells(Cols.Yea).Value
                iId = oRow.Cells(Cols.Id).Value
                oAlb = MaxiSrvr.Alb.FromNum(mEmp, iYea, iId)
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
        If mAllowEvents Then
            SetContextMenu()
        End If
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
        Dim iFirstVisibleCell As Integer = Cols.Id
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

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
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowAlb()
            e.Handled = True
        End If
    End Sub


End Class