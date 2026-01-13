

Public Class Frm_IncidenciesCods
    Public mCod As DTOIncidenciaCod.cods

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public WriteOnly Property Cod() As DTOIncidenciaCod.cods
        Set(ByVal value As DTOIncidenciaCod.cods)
            mCod = value
        End Set
    End Property

    Private Sub Frm_IncidenciesCods_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ID,ESP FROM INCIDENCIESCODS WHERE COD=@COD ORDER BY ESP"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@COD", mCod)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow(Cols.Id) = 0
        oRow(Cols.Nom) = "(afegir nou codi)"
        oTb.Rows.InsertAt(oRow, 0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
    End Sub

    Private Function CurrentCod() As DTOIncidenciaCod
        Dim oCod As DTOIncidenciaCod = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iCod As Integer = oRow.Cells(Cols.Id).Value
            oCod = New DTOIncidenciaCod(iCod)
            oCod.Esp = oRow.Cells(Cols.Nom).Value
            oCod.Cod = mCod
        End If
        Return oCod
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oIncidenciaCod As DTOIncidenciaCod = CurrentCod()
        If oIncidenciaCod.Id = 0 Then
            Dim oFrm As New Frm_IncidenciaCod
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            With oFrm
                .Cod = oIncidenciaCod
                .Show()
            End With
        Else
            RaiseEvent AfterSelect(Me, New MatEventArgs(oIncidenciaCod))
            Me.Close()
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
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