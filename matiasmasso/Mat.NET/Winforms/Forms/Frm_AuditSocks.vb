Imports MatHelperStd

Public Class Frm_AuditSocks

    Private Enum Cols
        Ref
        Dsc
        Stk
        Palet
        Entrada
        FchEntrada
        Procedencia
    End Enum

    Private Async Sub Frm_AuditSocks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Xl_Yea1.Yea = Today.Year - 1
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oExercici As New DTOExercici(Current.Session.Emp, Xl_Yea1.Yea)
        Dim items = Await FEB2.AuditStocks.All(exs, oExercici)
        If exs.Count = 0 Then
            Xl_AuditStocks1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub ImportarDeVivaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarDeVivaceToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar tancament de stocks anual de Vivace"
            .Filter = "Excel (*.xlsx)|*.xlsx|tots els fitxers|*.*"
            If .ShowDialog Then
                Dim oFrm As New Frm_ExcelColumsMapping({"Referencia",
                                               "Descripció",
                                               "stock",
                                               "ubicacions",
                                               "entrada",
                                               "data entrada",
                                               "procedencia"},
                                                       .FileName)
                AddHandler oFrm.AfterUpdate, AddressOf onColumnsMapped
                oFrm.Show()

            End If
        End With

    End Sub

    Private Async Sub onColumnsMapped(sender As Object, e As MatEventArgs)
        Dim oSheet As ExcelHelper.Sheet = e.Argument
        Dim exs As New List(Of Exception)
        Dim items As List(Of DTOAuditStock) = GetItemsFromSheet(oSheet, exs)
        If exs.Count = 0 Then
            If Await FEB2.AuditStocks.Update(items, exs) Then
                Await refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function GetItemsFromSheet(oSheet As ExcelHelper.Sheet, exs As List(Of Exception)) As List(Of DTOAuditStock)
        Dim retval As New List(Of DTOAuditStock)
        Dim iFirstRow As Integer = IIf(oSheet.ColumnHeadersOnFirstRow, 1, 0)
        For i As Integer = iFirstRow To oSheet.Rows.Count - 1
            Dim oRow As ExcelHelper.Row = oSheet.Rows(i)
            If IsNumeric(oRow.Cells(Cols.Stk).Content) Then
                Dim item As New DTOAuditStock
                With item
                    .Year = Xl_Yea1.Yea
                    If IsNumeric(oRow.Cells(Cols.Ref).Content) Then
                        .Ref = oRow.Cells(Cols.Ref).Content
                    End If
                    .Dsc = oRow.Cells(Cols.Dsc).Content
                    .Qty = oRow.Cells(Cols.Stk).Content
                    .Palet = oRow.Cells(Cols.Palet).Content
                    .Entrada = oRow.Cells(Cols.Entrada).Content
                    If IsDate(oRow.Cells(Cols.FchEntrada).Content) Then
                        .FchEntrada = oRow.Cells(Cols.FchEntrada).Content
                    Else
                        exs.Add(New Exception(String.Format("data d'entrada incorrecte a {0}", .Dsc)))
                    End If
                    .Procedencia = oRow.Cells(Cols.Procedencia).Content
                End With
                retval.Add(item)
            End If
            If exs.Count > 5 Then
                exs.Insert(0, New Exception(String.Format("detectats molts errors. Reportats els 5 primers")))
                Exit For
            End If
        Next
        Return retval
    End Function

    Private Async Sub AsignarUltimCostToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsignarUltimCostToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim items As List(Of DTOAuditStock) = Xl_AuditStocks1.Values
        With ProgressBar1
            .Visible = True
            .Value = 0
            .Maximum = items.Count
        End With

        For Each item In items
            If item.Sku IsNot Nothing Then
                Dim oAmt = Await FEB2.ProductSku.LastCost(exs, GlobalVariables.Emp, item.Sku, item.FchEntrada)
                If oAmt IsNot Nothing Then
                    item.Cost = oAmt.eur
                    Await FEB2.AuditStock.Update(item, exs)
                    ProgressBar1.Increment(1)
                    Application.DoEvents()
                End If
            End If
        Next

        ProgressBar1.Visible = False
        Await refresca()
    End Sub

    Private Sub ExportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarToolStripMenuItem.Click
        Dim items As List(Of DTOAuditStock) = Xl_AuditStocks1.Values
        Dim oCsv As New DTOCsv()
        For Each item As DTOAuditStock In items
            Dim oRow = oCsv.AddRow()
            oRow.AddCell(item.Ref)
            oRow.AddCell(item.Dsc)
            oRow.AddCell(item.Qty)
            oRow.AddCell(item.Cost)
            oRow.AddCell(item.Palet)
            oRow.AddCell(item.Entrada)
            oRow.AddCell(item.FchEntrada)
            oRow.AddCell(item.Procedencia)
        Next
        If UIHelper.SaveCsvDialog(oCsv, "Desar fitxer de stocks auditats") Then
            Me.Close()
        End If
    End Sub
End Class