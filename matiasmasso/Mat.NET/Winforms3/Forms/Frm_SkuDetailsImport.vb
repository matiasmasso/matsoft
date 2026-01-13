Public Class Frm_SkuDetailsImport
    Private _Sheet As MatHelper.Excel.Sheet
    Private _Proveidor As DTOProveidor

    Public Sub New(oProveidor As DTOProveidor)
        InitializeComponent()
        _Proveidor = oProveidor
        Me.Text = "Importar Excel de " & oProveidor.FullNom
    End Sub

    Private Sub Xl_ExcelFileSelect1_afterupdate(sender As Object, e As MatEventArgs) Handles Xl_ExcelFileSelect1.afterupdate
        Dim oBook As MatHelper.Excel.Book = e.Argument
        _Sheet = oBook.Sheets.First
        Dim oCols = _Sheet.Rows.First.Cells.Select(Function(x) x.Content).ToList
        oCols.Insert(0, "(ignorar)")
        ComboBoxRef.DataSource = oCols.ToArray
        ComboBoxEan.DataSource = oCols.ToArray
        ComboBoxEanExt.DataSource = oCols.ToArray
        ComboBoxCodiMerc.DataSource = oCols.ToArray
        ComboBoxH.DataSource = oCols.ToArray
        ComboBoxW.DataSource = oCols.ToArray
        ComboBoxL.DataSource = oCols.ToArray
        ComboBoxWeight.DataSource = oCols.ToArray
        ButtonNext.Enabled = True

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        Dim colRef = ComboBoxRef.SelectedIndex - 1
        If colRef = -1 Then
            MsgBox("Cal sel.leccionar la columna de la referencia de producte", MsgBoxStyle.Exclamation)
        Else
            With ProgressBar1
                .Maximum = _Sheet.Rows.Count - 1
                .Value = 0
                .Visible = True
            End With
            Dim colEanExt = ComboBoxEanExt.SelectedIndex - 1
            Dim colCodiMerc = ComboBoxCodiMerc.SelectedIndex - 1
            Dim colH = ComboBoxH.SelectedIndex - 1
            Dim colW = ComboBoxW.SelectedIndex - 1
            Dim colL = ComboBoxL.SelectedIndex - 1
            Dim colWeight = ComboBoxWeight.SelectedIndex - 1

            For i As Integer = 1 To _Sheet.Rows.Count - 1
                Dim oRow = _Sheet.Rows(i)
                Dim exs As New List(Of Exception)
                Dim ref As String = oRow.Cells(colRef).Content
                Dim oSku = Await FEB.ProductSku.FromProveidor(exs, _Proveidor, ref)
                Dim sb As New Text.StringBuilder
                If exs.Count = 0 Then
                    If oSku Is Nothing Then
                        _Sheet.Rows(i).AddCell("No s'ha trobat cap producte amb el codi '" & ref & "' (fila " & i + 1 & ")")
                    Else
                        FEB.ProductSku.Load(oSku, exs)
                        Try
                            If colEanExt > 0 AndAlso oRow.Cells(colEanExt).isNotEmpty Then
                                oSku.packageEan = New DTOEan(oRow.Cells(colEanExt).Content.ToString())
                            End If
                            If colCodiMerc > 0 AndAlso oRow.Cells(colCodiMerc).isNotEmpty Then
                                If IsNumeric(oRow.Cells(colCodiMerc).Content) Then
                                    Dim src = oRow.Cells(colCodiMerc).Content.ToString.Trim
                                    If src.Length = 8 Then
                                        oSku.codiMercancia = New DTOCodiMercancia(oRow.Cells(colCodiMerc).Content)
                                    Else
                                        _Sheet.Rows(i).AddCell("Producte '" & ref & "' (fila " & i + 1 & ") amb codi duaner '" & src & "' de longitud incorrecte")
                                    End If
                                End If
                            End If
                            If colH > 0 AndAlso oRow.Cells(colH).IsNotEmpty Then
                                If IsNumeric(oRow.Cells(colH).Content) Then
                                    oSku.DimensionH = oRow.Cells(colH).Content
                                End If
                            End If
                            If colW > 0 AndAlso oRow.Cells(colW).isNotEmpty Then
                                If IsNumeric(oRow.Cells(colW).Content) Then
                                    oSku.dimensionW = oRow.Cells(colW).Content
                                End If
                            End If
                            If colL > 0 AndAlso oRow.Cells(colL).isNotEmpty Then
                                If IsNumeric(oRow.Cells(colL).Content) Then
                                    oSku.dimensionL = oRow.Cells(colL).Content
                                End If
                            End If
                            If colWeight > 0 AndAlso oRow.Cells(colWeight).isNotEmpty Then
                                If IsNumeric(oRow.Cells(colWeight).Content) Then
                                    oSku.kgBrut = oRow.Cells(colWeight).Content
                                End If
                            End If
                            If Await FEB.ProductSku.Update(oSku, exs) Then
                                _Sheet.Rows(i).AddCell("Ok")
                            Else
                                _Sheet.Rows(i).AddCell("Error al desar '" & ref & "': " & ExceptionsHelper.ToFlatString(exs))
                            End If

                        Catch ex As Exception
                            _Sheet.Rows(i).AddCell("Error al llegir la fila " & i + 1 & " '" & ref & "': " & ex.Message)
                        End Try
                    End If
                Else
                    _Sheet.Rows(i).AddCell("Error a la fila " & i + 1 & ": " & ExceptionsHelper.ToFlatString(exs))
                End If
                ProgressBar1.Increment(1)
            Next
            Dim exs2 As New List(Of Exception)
            If Not UIHelper.ShowExcel(_Sheet, exs2) Then
                UIHelper.WarnError(exs2)
            End If
        End If
    End Sub
End Class