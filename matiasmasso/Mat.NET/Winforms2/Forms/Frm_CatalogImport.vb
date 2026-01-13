Public Class Frm_CatalogImport
    Private _sheet As MatHelper.Excel.Sheet
    Private _mapping As List(Of DTOProductSkuExcelMap)
    Private _allowEvents As Boolean

    Private Enum Tabs
        Mapping
        Results
    End Enum

    Public Sub New(oSheet As MatHelper.Excel.Sheet)
        MyBase.New
        InitializeComponent()
        _sheet = oSheet
        _mapping = New List(Of DTOProductSkuExcelMap)
    End Sub

    Private Sub Frm_CatalogImport_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadHeaderRowCombo()
        LoadColHeaders()
        LoadSkuFields()
        _allowEvents = True
    End Sub

    Private Sub LoadColHeaders()
        ComboBoxColHeader.Items.Clear()
        Dim oRow = ColHeadersRow()
        Dim idx As Integer
        For Each oCell In oRow.Cells
            Dim item As New ListItem(idx, oCell.Content)
            ComboBoxColHeader.Items.Add(item)
            idx += 1
        Next

        For Each item In _mapping
            item.colHeader = oRow.Cells(item.sheetCol).Content
        Next
        Xl_CatalegExcelMap1.Load(_mapping)

    End Sub

    Private Sub LoadSkuFields()
        UIHelper.LoadComboFromEnum(ComboBoxSkuField, GetType(DTOProductSkuExcelMap.SkuFields), "(seleccionar camp)")
    End Sub

    Private Function ColHeadersRow() As MatHelper.Excel.Row
        Dim rowIdx = ComboBoxHeadersFromRow.SelectedIndex
        Dim retval = _sheet.Rows(rowIdx)
        Return retval
    End Function

    Private Sub LoadHeaderRowCombo()
        For i = 1 To 10
            ComboBoxHeadersFromRow.Items.Add(i)
        Next
        ComboBoxHeadersFromRow.SelectedIndex = 0
    End Sub

    Private Sub ButtonAddField_Click(sender As Object, e As EventArgs) Handles ButtonAddField.Click
        Dim colHeader As String = ColHeadersRow.Cells(ComboBoxColHeader.SelectedIndex).Content
        Dim item = DTOProductSkuExcelMap.Factory(ComboBoxColHeader.SelectedIndex, colHeader, ComboBoxSkuField.SelectedValue)
        _mapping.Add(item)
        Xl_CatalegExcelMap1.Load(_mapping)
        ButtonOk.Enabled = True
    End Sub

    Private Sub ComboBoxHeadersFromRow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxHeadersFromRow.SelectedIndexChanged
        If _allowEvents Then
            LoadColHeaders()
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(PanelButtons, True)
        TabControl1.SelectedIndex = Tabs.Results
        Dim oResults As New List(Of MatHelper.Excel.ValidationResult)
        Dim lin As Integer
        For Each oRow In _sheet.Rows
            Dim exs As New List(Of Exception)
            lin += 1
            Dim oEan = Ean(oRow)
            If DTOEan.isValid(oEan) Then
                Dim oSku = Await FEB.ProductSku.FromEan(exs, oEan)
                If exs.Count = 0 Then
                    If oSku Is Nothing Then
                        oResults.Add(MatHelper.Excel.ValidationResult.Factory(lin, MatHelper.Excel.ValidationResult.Cods.Fail, "No s'ha trobat cap article amb l'Ean '" & oEan.Value & "'"))
                    Else
                        If FEB.ProductSku.Load(oSku, exs) Then

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Ref_proveidor) > "" Then
                                If oSku.RefProveidor = "" Then
                                    oSku.RefProveidor = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Ref_proveidor)
                                Else
                                    If oSku.RefProveidor <> CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Ref_proveidor) Then
                                        oResults.Add(MatHelper.Excel.ValidationResult.Factory(lin, MatHelper.Excel.ValidationResult.Cods.Fail, "la referencia no concorda amb l'Ean"))
                                    End If
                                End If
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Descripcio_proveidor) > "" Then
                                oSku.NomProveidor = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Descripcio_proveidor)
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Ean_packaging) > "" Then
                                oSku.PackageEan = DTOEan.Factory(CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Descripcio_proveidor))
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Moq) > "" Then
                                oSku.InnerPack = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Moq)
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Amplada_mm) > "" Then
                                oSku.DimensionW = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Amplada_mm)
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Longitut_mm) > "" Then
                                oSku.DimensionL = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Longitut_mm)
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Alçada_mm) > "" Then
                                oSku.DimensionH = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Alçada_mm)
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Pes_net_grams) > "" Then
                                If IsNumeric(CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Pes_net_grams)) Then
                                    oSku.KgNet = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Pes_net_grams) / 1000
                                End If
                            End If

                            If CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Pes_brut_grams) > "" Then
                                If IsNumeric(CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Pes_brut_grams)) Then
                                    oSku.KgBrut = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Pes_brut_grams) / 1000
                                End If
                            End If

                            If Not oResults.Any(Function(x) x.Row = lin) Then
                                If Await FEB.ProductSku.Update(oSku, exs) Then
                                    oResults.Add(MatHelper.Excel.ValidationResult.Factory(lin, MatHelper.Excel.ValidationResult.Cods.Success, oSku.NomLlarg.Tradueix(Current.Session.Lang)))
                                Else
                                    oResults.Add(MatHelper.Excel.ValidationResult.Factory(lin, MatHelper.Excel.ValidationResult.Cods.Fail, ExceptionsHelper.ToFlatString(exs)))
                                End If
                            End If
                        Else
                            oResults.Add(MatHelper.Excel.ValidationResult.Factory(lin, MatHelper.Excel.ValidationResult.Cods.Fail, ExceptionsHelper.ToFlatString(exs)))
                        End If
                    End If
                Else
                    oResults.Add(MatHelper.Excel.ValidationResult.Factory(lin, MatHelper.Excel.ValidationResult.Cods.Fail, ExceptionsHelper.ToFlatString(exs)))
                End If
            Else
                oResults.Add(MatHelper.Excel.ValidationResult.Factory(lin, MatHelper.Excel.ValidationResult.Cods.Fail, "EAN invalid"))
            End If
            Xl_ExcelValidationResults1.Load(oResults)
            Application.DoEvents()
        Next
        UIHelper.ToggleProggressBar(PanelButtons, False)

    End Sub

    Private Function CellValue(oRow As MatHelper.Excel.Row, oField As DTOProductSkuExcelMap.SkuFields) As String
        Dim retval As String = ""
        Dim iCellIdx As Integer = CellIdx(oField)
        If iCellIdx >= 0 And iCellIdx < oRow.Cells.Count Then
            Dim oCell = oRow.Cells(iCellIdx)
            retval = oCell.Content
        End If
        Return retval
    End Function

    Private Function Ean(oRow As MatHelper.Excel.Row) As DTOEan
        Dim retval As DTOEan = Nothing
        Dim sEan = CellValue(oRow, DTOProductSkuExcelMap.SkuFields.Ean_producte)
        retval = DTOEan.Factory(sEan)
        Return retval
    End Function

    Private Function CellIdx(oField As DTOProductSkuExcelMap.SkuFields) As Integer
        Dim retval As Integer = -1
        Dim oMap As DTOProductSkuExcelMap = Xl_CatalegExcelMap1.values.FirstOrDefault(Function(x) x.skuField = oField)
        If oMap IsNot Nothing Then
            retval = oMap.sheetCol
        End If
        Return retval
    End Function


End Class