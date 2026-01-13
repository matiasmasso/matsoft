Public Class Frm_PriceListSupplier_Import

    Private _SourceCols() As String = {"(seleccionar columna)"}

    Private Enum Cols
        Destination
        Source
    End Enum

    Private Enum Fields
        Referencia
        Descripcio
        EAN
        Cost
        Pvp
        Unitats_per_caixa
    End Enum

    Private Sub Frm_PriceListSupplier_Import_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetGrid()
        SetDataSource()
    End Sub

    Private Sub SetDataSource()
        Dim iRow As Integer = 0
        For Each oField As String In [Enum].GetNames(GetType(Fields))
            Dim sDestination As String = oField.ToString.Replace("_", " ")
            Dim sSource As String = ""
            If _SourceCols.Length > iRow Then
                sSource = _SourceCols(iRow)
            Else
                sSource = _SourceCols(0)
            End If
            DataGridView1.Rows.Add(sDestination, sSource)
            iRow += 1
        Next
    End Sub

    Private Sub ButtonFileBrowse_Click(sender As Object, e As EventArgs) Handles ButtonFileBrowse.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar tarifa de proveidor"
            .Filter = "Excel (*.xls,*.xlsx)|*.xls;*.xlsx|documents csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                TextBoxFile.Text = .FileName
                Dim oSheets As List(Of String) = MatHelper.Excel.AppHelper.GetSheetNames(exs, .FileName)
                If exs.Count = 0 Then
                    ComboBoxSheets.DataSource = oSheets
                    Dim sSelectedSheet As String = ComboBoxSheets.SelectedItem
                    SetSourceColumns(exs, .FileName, sSelectedSheet)
                    If exs.Count = 0 Then
                        SetDataSource()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Sub SetSourceColumns(exs As List(Of Exception), sFileName As String, sSheetName As String)

        Dim oSrcColumn As DataGridViewComboBoxColumn = DataGridView1.Columns(Cols.Source)
        Dim oDataSource = MatHelper.Excel.AppHelper.GetColumnNames(exs, sFileName, sSheetName)
        oDataSource.Insert(0, "(seleccionar columna)")
        _SourceCols = oDataSource.ToArray
        oSrcColumn.DataSource = _SourceCols
    End Sub

    Private Function CreateComboBoxWithEnums() As DataGridViewComboBoxColumn
        Dim combo As New DataGridViewComboBoxColumn()
        combo.DataSource = [Enum].GetValues(GetType(Fields))
        combo.DataPropertyName = "Fields"
        combo.Name = "Fields"
        Return combo
    End Function


    Private Sub SetGrid()
        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3

            .AutoGenerateColumns = False
            .Columns.Clear()

            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = False

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Destination)
                .HeaderText = "Destinació"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
            .Columns.Add(New DataGridViewComboBoxColumn)
            With DirectCast(.Columns(Cols.Source), DataGridViewComboBoxColumn)
                .HeaderText = "Columna"
                .DataSource = {"(seleccionar columna)"}
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DropDownWidth = 60
                .MaxDropDownItems = 3
                .FlatStyle = FlatStyle.Flat
            End With
        End With
    End Sub

    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        Stop
    End Sub
End Class

