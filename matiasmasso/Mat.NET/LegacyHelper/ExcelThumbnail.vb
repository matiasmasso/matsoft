Imports DocumentFormat.OpenXml.Office
Imports Microsoft.Office.Interop


Public Class ExcelThumbnail

    'DEPRECATED
    Shared Function GetExcelThumbnail(ByVal oStream As Byte(), exs As List(Of Exception)) As Byte()
        Dim filename As Object = MatHelper.Excel.ClosedXml.SaveExcelStream(exs, oStream)
        Dim sXpsFilename As String = ExcelAppHelper.GetXpsFileNameFromExcelFileName(exs, filename)
        Dim oXPSHelper As XPSHelper = XPSHelper.FromFilename(sXpsFilename)
        Dim retval As Byte() = oXPSHelper.GenerateThumbnail()
        Return retval
    End Function


End Class
