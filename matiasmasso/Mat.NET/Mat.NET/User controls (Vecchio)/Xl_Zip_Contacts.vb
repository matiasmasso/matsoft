Public Class Xl_Zip_Contacts

    Private _Zip As Zip
    Private _AllowEvents As Boolean

    Private Enum Cols
        Guid
        Clx
        Adr
        Ex
    End Enum

    Public WriteOnly Property Zip As Zip
        Set(value As Zip)
            _Zip = value
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()

        Dim SQL As String = "SELECT CliAdr.SrcGuid, CLX.clx, CliAdr.adr, CLX.ex " _
        & "FROM            CliAdr INNER JOIN " _
        & "CLX ON CliAdr.emp = CLX.Emp AND CliAdr.cli = CLX.cli " _
        & "WHERE        CliAdr.Zip = @Zip AND CLX.Emp=@Emp " _
        & "ORDER BY CLX.ex, CliAdr.adr"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Zip", _Zip.Guid.ToString, "@Emp", App.Current.Emp.Id)
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
            .AllowDrop = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Adr)
                .HeaderText = "adreça"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Ex)
                .Visible = False
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function CurrentItem() As Contact
        Dim retval As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            retval = New Contact(oGuid)
        End If
        Return retval
    End Function

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Clx

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentItem()

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DoZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact(CurrentItem)
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        DoZoom(sender, e)
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim BlObsolet As Boolean = oRow.Cells(Cols.Ex).Value
        If BlObsolet Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

End Class
