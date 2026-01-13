Public Class Xl_ErrorGrid
    Private mTb As DataTable
    Private mObjectsArray As New ArrayList

    Public Event ItemDoubleClicked(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum Icons
        Success
        Warn
        Failed
    End Enum

    Public Enum Cols
        Ico
        Txt
    End Enum

    Public Sub Clear()
        mObjectsArray = New ArrayList
        PreLoadGrid()
    End Sub

    Public Sub AddItm(ByVal oIcon As Icons, ByVal sTxt As String, Optional ByVal oObject As Object = Nothing)
        If mTb Is Nothing Then PreLoadGrid()
        Dim oRow As DataRow = mTb.NewRow
        Select Case oIcon
            Case Icons.Success
                oRow(Cols.Ico) = LegacyHelper.ImageHelper.GetByteArrayFromImg(My.Resources.info)
            Case Icons.Warn
                oRow(Cols.Ico) = LegacyHelper.ImageHelper.GetByteArrayFromImg(My.Resources.warn)
            Case Icons.Failed
                oRow(Cols.Ico) = LegacyHelper.ImageHelper.GetByteArrayFromImg(My.Resources.wrong)
        End Select
        oRow(Cols.Txt) = sTxt
        mTb.Rows.Add(oRow)
        mObjectsArray.Add(oObject)
    End Sub

    Private Sub PreLoadGrid()
        mTb = New DataTable()
        mTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        mTb.Columns.Add("TXT", System.Type.GetType("System.String"))
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Txt)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim Idx As Integer = DataGridView1.CurrentRow.Index
        Dim oObject As Object = mObjectsArray(Idx)
        RaiseEvent ItemDoubleClicked(oObject, e)
    End Sub
End Class
