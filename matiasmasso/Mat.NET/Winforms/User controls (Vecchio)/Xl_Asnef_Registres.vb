

Public Class Xl_Asnef_Registres

    Private mEmp as DTOEmp
    Private mAllowEvents As Boolean = False

    Private Enum Cols
        Guid
        Fch
        Cod
        Ico
        Eur
        Clx
    End Enum

    Private Enum Cods
        NotSet
        Actiu
        Cancelat
    End Enum

    Public WriteOnly Property Emp() as DTOEmp
        Set(ByVal value as DTOEmp)
            mEmp = value
            LoadGrid()
            SetContextMenu()
            mAllowEvents = True
        End Set
    End Property


    Private Sub LoadGrid()

        Dim SQL As String = "SELECT I.GUID, I.ASNEFALTA AS FCH, " _
        & "(CASE WHEN I.ASNEFBAIXA IS NULL THEN 1 ELSE 2 END) AS COD, " _
        & "B.EUR, " _
        & "C.CLX " _
        & "FROM IMPAGATS I INNER JOIN " _
        & "CSB B ON I.EMP=B.EMP AND I.YEA=B.YEA AND I.CSA=B.CSB AND I.CSB=B.DOC INNER JOIN " _
        & "CLX C ON B.EMP=C.EMP AND B.CLI=C.CLI " _
        & "WHERE I.EMP=@EMP AND I.ASNEFALTA IS NOT NULL " _
        & "ORDER BY I.ASNEFALTA DESC, C.CLX"

        Dim oDs As DataSet = Nothing
        oDs = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix icono 
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        mAllowEvents = False
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
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Cod)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "deute"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "deutor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom)
        oMenuItem.Enabled = CurrentItem() IsNot Nothing
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Function CurrentItem() As DTOImpagat
        Dim oRetVal As DTOImpagat = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New DTOImpagat(oRow.Cells(Cols.Guid).Value)
        End If
        Return oRetVal
    End Function

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItem As DTOImpagat = CurrentItem()
        If oItem IsNot Nothing Then
            Dim oFrm As New Frm_Impagat(oItem)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Clx
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
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As Cods = oRow.Cells(Cols.Cod).Value
                Select Case oCod
                    Case Cods.Actiu
                        e.Value = My.Resources.warn
                    Case Cods.Cancelat
                        e.Value = My.Resources.UNDO
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom(sender, e)
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub


End Class

