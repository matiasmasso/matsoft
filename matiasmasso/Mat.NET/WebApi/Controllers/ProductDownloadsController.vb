Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductDownloadsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/productdownloads/{target}")>
    Public Function All(target As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTarget As New DTOBaseGuid(target)
            Dim values = BEBL.ProductDownloads.All(oTarget)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function




    <HttpGet>
    <Route("api/productdownloads/LastCompatibilityReport/{product}")>
    Public Function LastCompatibilityReport(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.ProductDownloads.All(oProduct).Where(Function(x) x.Src = DTOProductDownload.Srcs.Compatibilidad And x.DocFile IsNot Nothing).ToList
            Select Case values.Count
                Case 0
                Case Else
                    Dim value = values.OrderByDescending(Function(x) x.DocFile.Fch).First
                    BEBL.DocFile.load(value.DocFile, loadstream:=True)
                    retval = MyBase.HttpPdfResponseMessage(value.DocFile.Stream, "Listado de compatibilidad.pdf")
            End Select

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els documents")
        End Try
        Return retval
    End Function

End Class


