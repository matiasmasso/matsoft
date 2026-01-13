Imports MatHelperStd
Public Class Frm_Vivace_Stocks

    Private _AllowEvents As Boolean

    Private Enum Fields
        Referencia
        Descripcio
        Stock
        Ubicacio
        Data_de_Entrada
        Procedencia
    End Enum

    Private Enum Tabs
        Inventari
        Import
    End Enum

    Private Enum Cols
        LLetra
        Concepte
    End Enum

    Private Async Sub Frm_Vivace_Stocks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetImportFch()
        Await LoadFchs()
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Function ReadFile(oSheet As ExcelHelper.Sheet, exs As List(Of Exception)) As List(Of DTOVivaceStock)
        Dim retval As New List(Of DTOVivaceStock)

        Dim idx As Integer
        For Each oRow As ExcelHelper.Row In oSheet.Rows
            Dim item As New DTOVivaceStock
            With item
                .Referencia = oRow.Cells(Fields.Referencia).Content
                .Descripcio = oRow.Cells(Fields.Descripcio).Content
                If IsNumeric(oRow.Cells(Fields.Stock).Content) Then
                    .Stock = oRow.Cells(Fields.Stock).Content
                End If
                .Ubicacio = oRow.Cells(Fields.Ubicacio).Content
                If IsDate(oRow.Cells(Fields.Data_de_Entrada).Content) Then
                    .FchEntrada = oRow.Cells(Fields.Data_de_Entrada).Content
                Else

                End If
            End With

            idx += 1
            If Not IsDate(oRow.Cells(Fields.Data_de_Entrada).Content) Then
                exs.Add(New Exception(String.Format("omesa fila {0} ref. {1} {2} per data entrada incorrecte '{3}'", idx, item.Referencia, item.Descripcio, oRow.Cells(Fields.Data_de_Entrada).Content)))
            ElseIf Not IsNumeric(oRow.Cells(Fields.Stock).Content) Then
                exs.Add(New Exception(String.Format("omesa fila {0} ref. {1} {2} per data stock no numeric '{3}'", idx, item.Referencia, item.Descripcio, oRow.Cells(Fields.Stock).Content)))
            Else
                retval.Add(item)
            End If
        Next
        Return retval
    End Function

    Private Sub SetImportFch()
        Dim oExercici As DTOExercici = DTOExercici.Past(Current.Session.Emp)
        DateTimePicker1.Value = oExercici.LastFch
    End Sub

    Private Async Function LoadFchs() As Task
        Dim exs As New List(Of Exception)
        Dim fchs = Await FEB2.VivaceStocks.Fchs(exs)
        If exs.Count = 0 Then
            With ComboBoxFch
                .DataSource = Await FEB2.VivaceStocks.Fchs(exs)
                .FormatString = "dd/MM/yy"
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub ComboBoxFch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFch.SelectedIndexChanged
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = ComboBoxFch.SelectedItem
        Dim items = Await FEB2.VivaceStocks.All(exs, Current.Session.Emp, DtFch)
        If exs.Count = 0 Then
            Dim iPalets As Integer = DTOVivaceStock.PaletsCount(items)
            TextBoxPalets.Text = Format(iPalets, "#,###")
            TextBoxPaletsInactius.Text = Format(DTOVivaceStock.PaletsInactius(items, 365) / iPalets, "#,###%")

            Dim iRefs As Integer = DTOVivaceStock.RefsCount(items)
            TextBoxRefs.Text = Format(iRefs, "#,###")
            TextBoxRefsInactives.Text = Format(DTOVivaceStock.RefsInactives(items, 365) / iRefs, "#,###%")
            Xl_VivaceStocks1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Sub Importar()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Inventari de Vivace en Excel"
            .Filter = "Excel|*.xls;*.xlsx|Tots els fitxers|*.*"
            If .ShowDialog And .FileName > "" Then
                Dim oFrm As New Frm_ExcelColumsMapping({"Referencia", "Descripcio", "Stock", "Ubicacio", "Data_de_Entrada"}, .FileName)
                AddHandler oFrm.AfterUpdate, AddressOf OnExcelImported
                oFrm.Show()
            End If
        End With
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarToolStripMenuItem.Click
        Importar()
    End Sub

    Private Sub ButtonImportar_Click(sender As Object, e As EventArgs) Handles ButtonImportar.Click
        Importar()
    End Sub

    Private Async Sub OnExcelImported(sender As Object, e As MatEventArgs)
        Dim oSheet As ExcelHelper.Sheet = e.Argument
        Dim exs As New List(Of Exception)
        Dim items As List(Of DTOVivaceStock) = ReadFile(oSheet, exs)
        If exs.Count = 0 Then
            Await Save(DateTimePicker1.Value, items)
        Else
            Dim rc As MsgBoxResult = UIHelper.WarnError(exs, "Ignorar per grabar omitint els següents registres:", MsgBoxStyle.AbortRetryIgnore)
            If rc = MsgBoxResult.Ignore Then
                Await Save(DateTimePicker1.Value, items)
            End If
        End If

    End Sub

    Private Async Function Save(DtFch As Date, items As List(Of DTOVivaceStock)) As Task
        Dim exs As New List(Of Exception)
        If Await FEB2.VivaceStocks.Update(exs, GlobalVariables.Emp, DtFch, items) Then
            Await LoadFchs()
            ComboBoxFch.SelectedIndex = 0
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub ExportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarToolStripMenuItem.Click
        Dim items As List(Of DTOVivaceStock) = Xl_VivaceStocks1.Values
        Dim oSheet = DTOVivaceStock.Excel(items)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class