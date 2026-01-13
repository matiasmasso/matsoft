

Public Class Frm_EdiFile
    Private mEdiFile As EdiFile
    Private mAllowEvents As Boolean

    Public Event afterUpdate(sender As Object, e As System.EventArgs)

    Private Enum Cols
        Txt
    End Enum

    Public Sub New(oEdiFile As EdiFile)
        MyBase.New()
        Me.InitializeComponent()
        mEdiFile = oEdiFile
        LoadGrid()
    End Sub

    Private Sub LoadGrid()

        Dim oTb As New DataTable
        oTb.Columns.Add(New DataColumn("Txt", System.Type.GetType("System.String")))
        Dim oRow As DataRow = oTb.NewRow
        oTb.Rows.Add(oRow)
        oRow(Cols.Txt) = mEdiFile.TagNom

        For Each oSegment As EdiSegment In mEdiFile.Segments
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow(Cols.Txt) = oSegment.ToString
        Next

        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False


            With .Columns(Cols.Txt)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
        mAllowEvents = True
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        Dim sb As New System.Text.StringBuilder
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not IsDBNull(oRow.Cells(Cols.Txt).Value) Then
                Dim sVal As String = oRow.Cells(Cols.Txt).Value
                If sVal > "" Then
                    sb.AppendLine(sVal)
                End If
            End If
        Next
        Dim sText As String = sb.ToString
        mEdiFile.Text = sText
        Dim exs as New List(Of exception)
        If mEdiFile.Update( exs) Then
            RaiseEvent afterUpdate(mEdiFile, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar el fitxer")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class