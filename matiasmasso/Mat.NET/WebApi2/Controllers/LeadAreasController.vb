Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class LeadAreasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/LeadAreas/Consumer/{emp}/{lang}")>
    Public Function Consumers(emp As DTOEmp.Ids, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.LeadAreas.Consumers(oEmp, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les LeadAreas")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/LeadAreas/Pro/{emp}/{lang}")>
    Public Function Pro(emp As DTOEmp.Ids, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.LeadAreas.Pro(oEmp, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les LeadAreas")
        End Try
        Return retval
    End Function

End Class