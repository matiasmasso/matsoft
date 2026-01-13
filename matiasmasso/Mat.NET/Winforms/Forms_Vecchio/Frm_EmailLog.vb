

Public Class Frm_EmailLog

    Private Enum Cols
        Fch
        Hora
        LogCod
        LogNom
    End Enum

    Public Sub New(ByVal oEmail As Email)
        InitializeComponent()
        Dim SQL As String = "SELECT  fch, fch AS Hora, LogCode,'' as LOGNOM " _
        & "FROM WebLog2 WHERE WebLog2.Src=@Email " _
        & "ORDER BY fch DESC "

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@Email", oEmail.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.LogCod)
                .Visible = False
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Hora)
                .HeaderText = "hora"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "HH:mm"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.LogNom)
                .HeaderText = "pagina visitada"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.LogNom
                Try
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim iLogCode As Integer = oRow.Cells(Cols.LogCod).Value
                    Dim sNom As String = [Enum].GetNames(GetType(DTOWebLog.LogCodes))(iLogCode)
                    e.Value = sNom

                Catch ex As Exception

                End Try
        End Select
    End Sub

End Class