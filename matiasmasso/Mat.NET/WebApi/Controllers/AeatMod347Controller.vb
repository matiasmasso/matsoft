Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AeatMod347Controller
    Inherits _BaseController

    <HttpGet>
    <Route("api/AeatMod347/{emp}/{year}")>
    Public Function Factory(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim value = BEBL.AeatMod347.Factory(oExercici, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir el 347")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el 347")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AeatMod347/VendesFromUser/{user}/{year}")>
    Public Function VendesFromUser(user As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.AeatMod347.VendesFromUser(exs, oUser, year)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir el 347")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el 347")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AeatMod347/CompresDetall/{emp}/{year}/{contact}")>
    Public Function CompresDetall(emp As DTOEmp.Ids, year As Integer, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.AeatMod347.CompresDetall(oExercici, oContact, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir el 347")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el 347")
        End Try
        Return retval
    End Function
    <HttpGet>
    <Route("api/AeatMod347/VendesDetall/{emp}/{year}/{contact}")>
    Public Function VendesDetall(emp As DTOEmp.Ids, year As Integer, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.AeatMod347.VendesDetall(oExercici, oContact, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir el 347")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el 347")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AeatMod347/minimDeclarable")>
    Public Function minimDeclarable() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.AeatMod347.MinimDeclarable(exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage("error al llegir el minim declarable del 347")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el minim declarable del 347")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/AeatMod347/declarable")>
    Public Function declarable(<FromBody> item As DTOAeatMod347Item) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.AeatMod347.Declarable(item, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage("error al llegir el 347")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el 347")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/AeatMod347/Csv")>
    Public Function Csv(<FromBody> oMod347 As DTOAeatMod347) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.AeatMod347.Csv(oMod347, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage("error al llegir el 347")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el 347")
        End Try
        Return retval
    End Function
End Class
