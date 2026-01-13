Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PndController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Pnd/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Pnd.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Pnd")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Pnd")>
    Public Function Update(<FromBody> value As DTOPnd) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Pnd.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Pnd")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Pnd")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Pnd/delete")>
    Public Function Delete(<FromBody> value As DTOPnd) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Pnd.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Pnd")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Pnd")
        End Try
        Return retval
    End Function

End Class

Public Class PndsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Pnds/{emp}/{contact}/{cod}/{onlyPendents}")>
    Public Function All(emp As DTOEmp.Ids, contact As Guid, cod As DTOPnd.Codis, onlyPendents As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = New DTOContact(contact)
            Dim values = BEBL.Pnds.All(oEmp, oContact, cod:=cod, onlyPendents:=(onlyPendents = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Pnds")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Pnds/FromFra/{contact}/{franum}/{fch}")>
    Public Function FromFra(contact As Guid, franum As String, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact = BEBL.Contact.Find(contact)
            Dim values = BEBL.Pnds.All(oContact.emp, oContact, franum, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Pnds")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Pnds/pending/{emp}")>
    Public Function Pending(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Pnds.Pending(oEmp, DTOPnd.Codis.Deutor, IncludeDescomptats:=False)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Pnds")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Pnds/BankTransferReminderDeutors/{emp}/{vto}")>
    Public Function BankTransferReminderDeutors(emp As DTOEmp.Ids, vto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Pnds.BankTransferReminderDeutors(oEmp, vto)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els clients pendents de transferencia")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Pnds/cartera/{emp}/{fch}")>
    Public Function Pending(emp As DTOEmp.Ids, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Pnds.Cartera(oEmp, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Pnds")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Pnds/descuadres/{emp}/{year}")>
    Public Function Descuadres(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Pnds.Descuadres(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els descuadres")
        End Try
        Return retval
    End Function

End Class
