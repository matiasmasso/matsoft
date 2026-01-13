

Public Class Frm_PdcSrc_Select

    Private mSrc As DTOPurchaseorder.Sources
    Private mClosed As Boolean
    Private mDs As DataSet

    Private Enum Cols
        Ico
        Id
        Nom
    End Enum

    Public Property Src() As DTOPurchaseorder.Sources
        Get
            Return mSrc
        End Get

        Set(ByVal value As DTOPurchaseorder.Sources)
            LoadGrid()
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.Rows
                If oRow.Cells(Cols.Id).Value = value Then
                    DataGridView1.CurrentCell = oRow.Cells(Cols.Nom)
                    Exit For
                End If
            Next
        End Set
    End Property

    Private Sub LoadGrid()
        mDs = New DataSet
        Dim oTb As New System.Data.DataTable
        oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oTb.Columns.Add("COD", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As System.Data.DataRow

        Dim v As Integer
        For Each v In [Enum].GetValues(GetType(DTOPurchaseorder.Sources))
            oRow = oTb.NewRow
            'oRow(Cols.Ico) = Pdc.SrcIcon(v)
            oRow(Cols.Id) = v
            oTb.Rows.Add(oRow)
        Next

        Dim i As Integer = 0
        Dim s As String = ""
        For Each s In [Enum].GetNames(GetType(DTOPurchaseorder.Sources))
            oTb.Rows(i)(Cols.Nom) = s
            i = i + 1
        Next
        mDs.Tables.Add(oTb)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
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
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim iCod As Integer = oRow.Cells(Cols.Id).Value
                e.Value = IconHelper.PurchaseSrcIcon(iCod)
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Me.Close()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            mClosed = True
            Me.Close()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If Not mClosed Then
            mSrc = CurrentSrc()
        End If
    End Sub

    Private Function CurrentSrc() As DTOPurchaseorder.Sources
        Dim oSrc As DTOPurchaseorder.Sources
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iCod As Integer = oRow.Cells(Cols.Id).Value
            oSrc = DirectCast(iCod, DTOPurchaseorder.Sources)
        End If
        Return oSrc
    End Function
End Class