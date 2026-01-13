Public Class Frm_Vivace_Stocks
    Private _ExcelColumns As List(Of ExcelColumn)

    Private Enum Tabs
        Inventari
        Import
    End Enum

    Private Enum Cols
        LLetra
        Concepte
    End Enum

    Private Sub Frm_Vivace_Stocks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _ExcelColumns = ExcelColumns()
        LoadColumns()
        LoadFchs()
    End Sub

    Private Sub LoadColumns()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ExcelColumns
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False

            .Columns.Add(New DataGridViewComboBoxColumn)
            With CType(.Columns(Cols.LLetra), DataGridViewComboBoxColumn)
                .HeaderText = "Columna"
                .DataSource = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "Q", "R"}
                .DataPropertyName = "Lletra"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .DropDownWidth = 60
                .MaxDropDownItems = 3
                .FlatStyle = FlatStyle.Flat
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Concepte)
                .HeaderText = "Concepte"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With

    End Sub


    Private Function ExcelColumns() As List(Of ExcelColumn)
        Dim retval As New List(Of ExcelColumn)
        retval.Add(New ExcelColumn("E", ExcelColumn.Conceptes.Referencia))
        retval.Add(New ExcelColumn("H", ExcelColumn.Conceptes.Descripcio))
        retval.Add(New ExcelColumn("K", ExcelColumn.Conceptes.Stock))
        retval.Add(New ExcelColumn("M", ExcelColumn.Conceptes.Ubicacio))
        retval.Add(New ExcelColumn("Q", ExcelColumn.Conceptes.Data_de_Entrada))
        retval.Add(New ExcelColumn("R", ExcelColumn.Conceptes.Procedencia))
        Return retval
    End Function

    Protected Class ExcelColumn
        Property Concepte As Conceptes
        Property Lletra As String

        Public ReadOnly Property Nom As String
            Get
                Return _Concepte.ToString.Replace("_", " ")
            End Get
        End Property

        Public Sub New(sLletra As String, oConcepte As Conceptes)
            MyBase.New()
            _Concepte = oConcepte
            _Lletra = slletra
        End Sub

        Public Enum Conceptes
            Referencia
            Descripcio
            Stock
            Ubicacio
            Data_de_Entrada
            Procedencia
        End Enum
    End Class

    Private Sub ButtonFileSearch_Click(sender As Object, e As EventArgs) Handles ButtonFileSearch.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "fitxers Excel|*.xls;*.xslx|tots els fitxers|*.*"
            If .ShowDialog Then
                If .FileName > "" Then
                    Dim items As List(Of DTOVivaceStock) = ReadFile(.FileName)
                    Dim exs As New List(Of Exception)
                    If BLL.BLLVivaceStocks.Update(DateTimePicker1.Value, items, exs) Then
                        LoadFchs()
                        ComboBoxFch.SelectedIndex = 0
                        TabControl1.SelectedIndex = Tabs.Inventari
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End With
    End Sub


    Private Function ReadFile(sFilename As String) As List(Of DTOVivaceStock)
        Dim retval As New List(Of DTOVivaceStock)

        Dim oExcel As DTOExcelBook = MatExcel.Read2(sFilename)
        Dim oSheet As DTOExcelSheet = oExcel.Sheets(0)
        For Each oRow As DTOExcelRow In oSheet.Rows
            If IsNumeric(CellValue(oRow, ExcelColumn.Conceptes.Stock)) Then
                Dim item As New DTOVivaceStock
                With item
                    .Referencia = CellValue(oRow, ExcelColumn.Conceptes.Referencia)
                    .Descripcio = CellValue(oRow, ExcelColumn.Conceptes.Descripcio)
                    .Stock = CellValue(oRow, ExcelColumn.Conceptes.Stock)
                    .FchEntrada = CellValue(oRow, ExcelColumn.Conceptes.Data_de_Entrada)
                    .Ubicacio = CellValue(oRow, ExcelColumn.Conceptes.Ubicacio)
                End With
                retval.Add(item)
            End If
        Next
        Return retval
    End Function

    Private Function CellValue(oRow As DTOExcelRow, oConcepte As ExcelColumn.Conceptes) As String
        Dim oColumn As ExcelColumn = _ExcelColumns(oConcepte)
        Dim Lletra As String = oColumn.Lletra
        Dim ColIndex As Integer = Asc(Lletra) - 65 'A=65
        Dim retval As String = oRow.Cells(ColIndex).Content

        Return retval
    End Function

    Private Sub LoadFchs()
        With ComboBoxFch
            .DataSource = BLL.BLLVivaceStocks.Fchs
            .FormatString = "dd/MM/yy"
        End With
    End Sub

    Private Sub ComboBoxFch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFch.SelectedIndexChanged
        Dim DtFch As Date = ComboBoxFch.SelectedItem
        Dim items As List(Of DTOVivaceStock) = BLL.BLLVivaceStocks.All(DtFch)

        Dim iPalets As Integer = BLL.BLLVivaceStocks.PaletsCount(items)
        TextBoxPalets.Text = Format(iPalets, "#,###")
        TextBoxPaletsInactius.Text = Format(BLL.BLLVivaceStocks.PaletsInactius(items, 365) / iPalets, "#,###%")

        Dim iRefs As Integer = BLL.BLLVivaceStocks.RefsCount(items)
        TextBoxRefs.Text = Format(iRefs, "#,###")
        TextBoxRefsInactives.Text = Format(BLL.BLLVivaceStocks.RefsInactives(items, 365) / iRefs, "#,###%")
        Xl_VivaceStocks1.Load(items)
    End Sub


End Class