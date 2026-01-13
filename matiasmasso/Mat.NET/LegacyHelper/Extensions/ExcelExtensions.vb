Imports System.Runtime.CompilerServices
Module ExcelExtensions

    <Extension()>
    Public Function AddCellAmt(ByVal oRow As MatHelper.Excel.Row, oAmt As DTOAmt) As MatHelper.Excel.Cell
        Dim oCell As New MatHelper.Excel.Cell
        If oAmt IsNot Nothing Then
            oCell = New MatHelper.Excel.Cell(oAmt.Eur, , MatHelper.Excel.Cell.NumberFormats.Decimal2Digits)
        End If
        oRow.Cells.Add(oCell)
        Return oCell
    End Function


    <Extension()>
    Public Function AddCellEan(ByVal oRow As MatHelper.Excel.Row, oEan As DTOEan) As MatHelper.Excel.Cell
        Dim oCell As New MatHelper.Excel.Cell
        If oEan IsNot Nothing Then
            oCell = New MatHelper.Excel.Cell(oEan.Value)
        End If
        oRow.Cells.Add(oCell)
        Return oCell
    End Function

End Module

