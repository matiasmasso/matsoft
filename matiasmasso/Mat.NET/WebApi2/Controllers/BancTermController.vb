Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BancTermController
    Inherits _BaseController

    <HttpPost>
    <Route("api/BancTerm")>
    Public Function Update(<FromBody> value As DTOBancTerm) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancTerm.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la BancTerm")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la BancTerm")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/BancTerm/delete")>
    Public Function Delete(<FromBody> value As DTOBancTerm) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancTerm.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la BancTerm")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BancTerm")
        End Try
        Return retval
    End Function

End Class

Public Class BancTermsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BancTerms/{emp}/{banc}")>
    Public Function All(emp As DTOEmp.Ids, banc As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oBanc As DTOBanc = Nothing
            If banc <> Nothing Then oBanc = New DTOBanc(banc)
            Dim values = BEBL.BancTerms.All(oEmp, oBanc)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BancTerms")
        End Try
        Return retval
    End Function

End Class
