

Public Class Xl_NUK_fras
    Private mAllowEvents As Boolean
    Private mRoche As Contact = Roche.Contact
    Private mOnlyWarn As Boolean = False

    Public Event SelectionChange(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        IcoWarn
        Num
        Fch
        Bas
        Iva
        Tot
        Vto
        Obs
        PendentAbonar
    End Enum

    Public Sub New()
        MyBase.new()
        InitializeComponent()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT  Guid, Num, Fch, BasEur, Iva, TotEur, Vto, Obs, PendentAbonar " _
        & "FROM FRAPRV " _
        & "WHERE EMP=@EMP AND PROVEIDOR=@PROVEIDOR "
        If mOnlyWarn Then
            SQL = SQL & "AND PendentAbonar > 0 "
        End If
        SQL = SQL & "ORDER BY Num DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mRoche.Emp.Id, "@PROVEIDOR", mRoche.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oColIcoWarn As DataColumn = oTb.Columns.Add("WARNICO", System.Type.GetType("System.Byte[]"))
        oColIcoWarn.SetOrdinal(Cols.IcoWarn)


        Dim BlOldEvents As Boolean = mAllowEvents
        mAllowEvents = False
        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
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
            With .Columns(Cols.IcoWarn)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Num)
                .HeaderText = "factura"
                .Width = 70
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Bas)
                .HeaderText = "base"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Iva)
                .HeaderText = "Iva"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Tot)
                .HeaderText = "total"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.PendentAbonar)
                .HeaderText = "per abonar"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = BlOldEvents

        SetContextMenu()
        RaiseEvent SelectionChange(CurrentFra, EventArgs.Empty)
    End Sub

    Private Function CurrentRow() As DataGridViewRow
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Return oRow
    End Function


    Private Function CurrentFra() As FacturaDeProveidor
        Dim oFra As FacturaDeProveidor = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New Guid(oRow.Cells(Cols.Guid).Value.ToString)
            oFra = New FacturaDeProveidor(oGuid)

        End If
        Return oFra
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then

            oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf ShowFra)
            oContextMenu.Items.Add(oMenuItem)

            Select Case BLL.BLLSession.Current.User.Rol.id
                Case Rol.Ids.SuperUser, Rol.Ids.Admin, Rol.Ids.Accounts
                    oMenuItem = New ToolStripMenuItem("nova fra.rectificativa", My.Resources.candau, AddressOf NewFraRectificativa)
                    oContextMenu.Items.Add(oMenuItem)
            End Select

        End If

        oMenuItem = New ToolStripMenuItem("refresca", Nothing, AddressOf LoadGrid)
        oContextMenu.Items.Add(oMenuItem)


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub ShowFra(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFra As FacturaDeProveidor = CurrentFra()
        If oFra IsNot Nothing Then
            Dim oFrm As New Frm_FraProveidor(oFra)
            'AddHandler oFra.afterupdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub NewFraRectificativa(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFra As FacturaDeProveidor = CurrentFra()
        If oFra IsNot Nothing Then
            Dim oFrm As New Frm_FraPrvRectificativa(oFra)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoWarn
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.PendentAbonar).Value = 0 Then
                    e.Value = My.Resources.empty
                Else
                    e.Value = My.Resources.warn
                End If
        End Select

    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            Dim oFra As FacturaDeProveidor = CurrentFra()
            SetContextMenu()
            RaiseEvent SelectionChange(oFra, EventArgs.Empty)
        End If
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Num
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
