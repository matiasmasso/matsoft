

Public Class Xl_Client_CreditLimit
    Private mClient As Client = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        Eur
        Fch
        Obs
    End Enum

    Public WriteOnly Property Client() As Client
        Set(ByVal value As Client)
            mClient = value
            LoadGrid()
        End Set
    End Property


    Public Sub LoadGrid()
        Dim SQL As String = "SELECT GUID,EUR,FCHCREATED,OBS FROM CLICREDITLOG WHERE CliGuid=@CliGuid ORDER BY FCHCREATED DESC"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@EMP", mClient.Emp.Id, "@CliGuid", mClient.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)


        mAllowevents = False
        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "limit"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        mAllowevents = True
    End Sub

    Private Function CurrentItem() As DTOCliCreditLog
        Dim oRetVal As DTOCliCreditLog = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = BLL.BLLCliCreditLog.Find(New System.Guid(oRow.Cells(Cols.Guid).Value.ToString))
        End If
        Return oRetVal
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom(sender, e)
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowevents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
        oMenuItem.Enabled = (Currentitem() IsNot Nothing)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("afegir nou limit", Nothing, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItem As DTOCliCreditLog = CurrentItem()
        If oItem IsNot Nothing Then
            Dim oFrm As New Frm_CliCreditLimit(oItem)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItem As New DTOCliCreditLog
        With oItem
            .Customer = New DTOCustomer(mClient.CcxOrMe.Guid)
        End With
        BLL.BLLContact.Load(oItem.Customer)
        Dim oFrm As New Frm_CliCreditLimit(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Eur
        Dim oGrid As DataGridView = DataGridView1
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
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

        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
