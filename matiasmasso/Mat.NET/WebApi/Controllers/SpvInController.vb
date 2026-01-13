Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace Controllers
    Public Class SpvInController
        Inherits _BaseController


        <HttpGet>
        <Route("api/SpvIn/{guid}")>
        Public Function Find(guid As Guid) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim value = BEBL.SpvIn.Find(guid)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SpvIn")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/SpvIn/{emp}/{yea}/{id}")>
        Public Function Find(emp As Integer, yea As Integer, id As Integer) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = MyBase.GetEmp(emp)
                Dim value = BEBL.SpvIn.Find(oEmp, yea, id)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SpvIn")
            End Try
            Return retval
        End Function

        <HttpPost>
        <Route("api/spvin")>
        Public Function Update(oSpvIn As DTOSpvIn) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Dim exs As New List(Of Exception)
            If BEBL.SpvIn.Update(oSpvIn, exs) Then
                retval = Request.CreateResponse(Of Integer)(HttpStatusCode.OK, oSpvIn.Id)
            Else
                retval = New HttpResponseMessage(HttpStatusCode.InternalServerError)
                retval.Content = New StringContent(ExceptionsHelper.ToFlatString(exs))
                retval.ReasonPhrase = "error al grabar la entrada de mercancia per reparar"
            End If
            Return retval
        End Function


        <HttpPost>
        <Route("api/SpvIn/delete")>
        Public Function Delete(<FromBody> value As DTOSpvIn) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim exs As New List(Of Exception)
                If BEBL.SpvIn.Delete(value, exs) Then
                    retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la SpvIn")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la SpvIn")
            End Try
            Return retval
        End Function

    End Class

    Public Class SpvInsController
        Inherits _BaseController

        <HttpGet>
        <Route("api/SpvIns/{emp}")>
        Public Function All(emp As Integer) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = MyBase.GetEmp(emp)
                Dim values = BEBL.SpvIns.All(oEmp.Trimmed)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les SpvIns")
            End Try
            Return retval
        End Function

    End Class


End Namespace


