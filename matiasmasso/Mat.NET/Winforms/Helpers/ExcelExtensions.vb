Imports System.Runtime.CompilerServices
Imports MatHelperStd

Module ExcelExtensions

    <Extension()>
    Public Function AddCellAmt(ByVal oRow As ExcelHelper.Row, oAmt As DTOAmt) As MatHelperStd.ExcelHelper.Cell
        Dim oCell As New MatHelperStd.ExcelHelper.Cell
        If oAmt IsNot Nothing Then
            oCell = New MatHelperStd.ExcelHelper.Cell(oAmt.Eur, , ExcelHelper.Sheet.NumberFormats.Decimal2Digits)
        End If
        oRow.Cells.Add(oCell)
        Return oCell
    End Function


    <Extension()>
    Public Function AddCellEan(ByVal oRow As ExcelHelper.Row, oEan As DTOEan) As ExcelHelper.Cell
        Dim oCell As New ExcelHelper.Cell
        If oEan IsNot Nothing Then
            oCell = New ExcelHelper.Cell(oEan.Value)
        End If
        oRow.Cells.Add(oCell)
        Return oCell
    End Function

End Module
