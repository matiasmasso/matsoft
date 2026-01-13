Imports OfficeOpenXml
'Imports OfficeOpenXml.Core
'Imports OfficeOpenXml.Core.ExcelPackage

Public Class ExcelEPPlusHelper
    'requires EPPlus by Jan Källman (NuGet)


    Shared Function Read(exs As List(Of Exception), oByteArray As Byte(), Optional Filename As String = "", Optional Title As String = "") As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet(Title, Filename)
        Try
            Dim oStream As New System.IO.MemoryStream(oByteArray)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Using package As ExcelPackage = New ExcelPackage(oStream)
                Dim worksheets = package.Workbook.Worksheets
                Dim worksheet As ExcelWorksheet = worksheets.First
                Dim colCount As Integer = worksheet.Dimension.[End].Column
                Dim rowCount As Integer = worksheet.Dimension.[End].Row

                Dim col As Integer
                For row As Integer = 1 To rowCount
                    Try
                        Dim oRow = retval.AddRow()
                        For col = 1 To colCount
                            Dim cellValue = worksheet.Cells(row, col).Value
                            If cellValue Is Nothing Then
                                oRow.AddCell()
                            Else
                                oRow.AddCell(cellValue.ToString().Trim())
                            End If
                        Next

                    Catch ex As Exception
                        Dim msg = String.Format("Error al llegir la linia {0} columna {1}", row, col)
                        exs.Add(New Exception(msg))
                        exs.Add(ex)
                    End Try
                Next
            End Using
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


End Class
