Imports System.Net.Http
Imports System.Web.Http

Public Class VivaceController
    Inherits _BaseController

    <HttpGet>
    <Route("api/vivace/excelRefs")>
    Public Function excelRefs() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Vivace.ExcelRefs()
            retval = MyBase.HttpExcelResponseMessage(value, String.Format("M+O referencias {0:yyyy.MM.dd}.xlsx", DTO.GlobalVariables.Today()))
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Template")
        End Try
        Return retval
    End Function
End Class

