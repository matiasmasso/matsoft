Imports Excel = Microsoft.Office.Interop.Excel
Public Class ExcelAppHelper
    Private Shared _App As Excel.Application

    Shared Function ExcelApp(exs As List(Of Exception)) As Excel.Application
        If _App Is Nothing Then
            Try
                _App = New Excel.Application
            Catch ex As System.Exception
                exs.Add(ex)
            End Try
        End If
        Return _App
    End Function

    Shared Sub Quit()
        If _App IsNot Nothing Then
            _App.Quit()
            _App = Nothing
        End If
    End Sub


    Public Shared Function GetXpsFileNameFromExcelFileName(exs As List(Of Exception), ByVal sExcelFilename As Object, Optional ByVal sXpsFilename As String = "") As String
        Dim oApp = ExcelApp(exs)
        If exs.Count = 0 Then
            Dim workbook As Excel.Workbook = oApp.Workbooks.Open(sExcelFilename)

            If sXpsFilename = "" Then sXpsFilename = sExcelFilename & ".xps"
            workbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypeXPS, sXpsFilename, From:=1, [To]:=1)
        End If

        Return sXpsFilename
    End Function

    Shared Function GetSheetNames(exs As List(Of Exception), sFilename As String) As List(Of String)
        Dim retval As New List(Of String)
        Dim oApp = ExcelApp(exs)
        If exs.Count = 0 Then
            Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sFilename)
            For Each oSheet As Excel.Worksheet In oWb.Worksheets
                retval.Add(oSheet.Name)
            Next
        End If
        Return retval
    End Function


    Shared Function GetColumnNames(exs As List(Of Exception), sFilename As String, Optional sSheetName As String = "") As List(Of String)
        Dim retval As New List(Of String)
        Dim oApp = ExcelApp(exs)
        If exs.Count = 0 Then
            Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sFilename)

            Dim oSheet As Excel.Worksheet = Nothing
            If sSheetName = "" Then
                oSheet = oWb.ActiveSheet
            Else
                oSheet = oWb.Sheets(sSheetName)
            End If

            Dim LastCol As Integer = oSheet.Cells(1, oSheet.Columns.Count).End(Excel.XlDirection.xlToLeft).Column
            For iCol As Integer = 1 To LastCol
                retval.Add(oSheet.Cells(1, iCol).value)
            Next

        End If
        Return retval

    End Function


End Class
