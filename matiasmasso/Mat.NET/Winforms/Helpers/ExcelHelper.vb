Imports Excel = Microsoft.Office.Interop.Excel

Public Class ExcelHelper

    Public Shared Function GetExcelFileFromStream(ByVal oByte() As Byte, Optional ByVal sFilename As String = "") As String
        If sFilename = "" Then
            sFilename = System.Guid.NewGuid.ToString & ".xlsx"
            Dim sTmpFolder As String = FileSystemHelper.TmpFolder & "\"
            sFilename = System.IO.Path.Combine(sTmpFolder, sFilename)
        End If

        Dim exs As New List(Of Exception)
        If Not FileSystemHelper.SaveStream(oByte, exs, sFilename) Then
            UIHelper.WarnError(exs, "error al desar el fitxer")
        End If
        Return sFilename
    End Function

    Shared Function GetImgFromExcelFirstPage(ByVal oStream As Byte(), ByRef iCols As Integer, ByRef iRows As Integer) As Image
        Dim retval As Image = Nothing
        Dim sExcelName As String = System.Guid.NewGuid.ToString & ".xlsx"
        Dim sTmpFolder As String = FileSystemHelper.TmpFolder & "\"
        Dim sExcelPath As String = System.IO.Path.Combine(sTmpFolder, sExcelName)

        Dim exs As New List(Of Exception)
        If FileSystemHelper.SaveStream(oStream, exs, sExcelPath) Then
            retval = GetImgFromExcelFirstPage(sExcelPath, iCols, iRows)
        Else
            UIHelper.WarnError(exs, "error al desar el fitxer")
        End If

        Return retval
    End Function

    Shared Function GetExcelThumbnail(ByVal oStream As Byte()) As Image
        Dim sExcelFilename As Object = ExcelHelper.GetExcelFileFromStream(oStream)
        Dim sXpsFilename As String = ExcelHelper.GetXpsFileNameFromExcelFileName(sExcelFilename)
        Dim oXPSHelper As XPSHelper = XPSHelper.FromFilename(sXpsFilename)
        Dim oImg As Image = oXPSHelper.GenerateThumbnail()
        Return oImg
    End Function

    Public Shared Function GetXpsFileNameFromExcelFileName(ByVal sExcelFilename As Object, Optional ByVal sXpsFilename As String = "") As String
        Dim excelApp As New Excel.Application
        Dim workbook As Excel.Workbook = excelApp.Workbooks.Open(sExcelFilename)

        If sXpsFilename = "" Then sXpsFilename = sExcelFilename & ".xps"
        workbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypeXPS, sXpsFilename, From:=1, [To]:=1)

        Return sXpsFilename
    End Function

    Shared Function GetSheetNames(sFilename As String) As List(Of String)
        Dim retval As New List(Of String)
        Dim oApp As New Excel.Application
        Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sFilename)
        For Each oSheet As Excel.Worksheet In oWb.Worksheets
            retval.Add(oSheet.Name)
        Next
        oApp.Quit()
        Return retval
    End Function


    Shared Function GetColumnNames(sFilename As String, Optional sSheetName As String = "") As List(Of String)
        Dim retval As New List(Of String)
        Dim oApp As New Excel.Application
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
        oApp.Quit()
        Return retval

    End Function


    Shared Function GetImgFromExcelFirstPage(ByVal sFileName As String, ByRef iCols As Integer, ByRef iRows As Integer) As Image
        Dim oImg As Image = Nothing
        Dim oApp As New Excel.Application
        Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sFileName)
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

        iRows = oSheet.Cells(65536, 1).end(Excel.XlDirection.xlUp).row
        iCols = oSheet.Cells(1, 255).End(Excel.XlDirection.xlToLeft).column

        Dim sTmpFolder As String = FileSystemHelper.TmpFolder & "\"
        Dim sPdfName As String = System.IO.Path.ChangeExtension(sFileName, ".pdf")
        Dim sPdfPath As String = System.IO.Path.Combine(sTmpFolder, sPdfName)


        oWb.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, sPdfPath)
        oWb.Close(Excel.XlSaveAction.xlDoNotSaveChanges)
        oApp.Quit()
        oApp = Nothing
        oWb = Nothing

        'Dim oPdfRender As New PdfRender(sPdfPath)
        oImg = My.Resources.Excel_Big ' BLL.GetThumbnailToFit(oPdfRender.Image, 350, 400)
        Return oImg
    End Function

End Class
