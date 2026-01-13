Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepController
    Inherits _BaseController

    <HttpGet>
    <Route("api/rep/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Rep.Find(guid)
            BEBL.Contact.Load(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el rep")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/rep/exists/{contact}")>
    Public Function Exists(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.Rep.Exists(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el rep")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/rep/foto/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Rep.Find(guid)
            retval = MyBase.HttpBinaryResponseMessage(value.Foto)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del rep")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/rep")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTORep)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la rep")
            Else
                'value.Foto = oHelper.GetImage("foto")

                If DAL.RepLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar la foto a DAL.repLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.repLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/rep/delete")>
    Public Function Delete(<FromBody> value As DTORep) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Rep.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el rep")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el rep")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/rep/archive/{rep}")>
    Public Function Archive(rep As Guid) As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        Dim oRep As DTORep = BEBL.Rep.Find(rep)
        If oRep IsNot Nothing Then
            retval = BEBL.Rep.Archive(oRep)
        End If
        Return retval
    End Function

    <HttpGet>
    <Route("api/rep/baixa/{emp}/{rep}/{fch}/{removePrivileges}")>
    Public Function Baixa(emp As DTOEmp.Ids, rep As Guid, fch As Date, removePrivileges As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oRep As New DTORep(rep)
            If BEBL.Rep.Baixa(oEmp, oRep, fch, (removePrivileges = 1), exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el rep")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el rep")
        End Try
        Return retval
    End Function

End Class

Public Class RepsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/reps/{user}")>
    Public Function reps(user As Guid) As List(Of DTORep)
        Dim retval As New List(Of DTORep)
        Dim oUser As DTOUser = BEBL.User.Find(user)
        If oUser IsNot Nothing Then
            retval = BEBL.Reps.All(oUser.Emp, Active:=False)
        End If
        Return retval
    End Function

    <HttpGet>
    <Route("api/reps/emails/{area}/{channel}")>
    Public Function Emails(area As Guid, channel As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oArea As New DTOArea(area)
            Dim oChannel As New DTODistributionChannel(channel)
            Dim values = BEBL.Reps.Emails(oArea, oChannel)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els emails dels reps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/reps/emails/FromEmp/{emp}")>
    Public Function EmailsFromEmp(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Reps.Emails(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els emails dels reps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/reps/emails/FromProduct/{product}")>
    Public Function EmailsFromProduct(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = New DTOProduct(product)
            Dim values = BEBL.Reps.Emails(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els emails dels reps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/reps/fromProduct/{product}")>
    Public Function fromProduct(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.Reps.All(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els reps")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/reps/active/{user}")> 'for Mat.NET
    Public Function active(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("usuari no reconegut")
            Else
                Dim values = BEBL.Reps.All(oUser.Emp, Active:=True)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els reps")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/reps/saldos/{emp}/{year}")> 'for Mat.NET
    Public Function saldos(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Reps.Saldos(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els saldos dels reps")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/reps/ibans/{emp}")> 'for Mat.NET
    Public Function ibans(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Reps.Ibans(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els ibans dels reps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/reps/WithRetencions/{emp}/{fchfrom}/{fchto}")> 'for Mat.NET
    Public Function WithRetencions(emp As DTOEmp.Ids, fchfrom As Date, fchto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Reps.WithRetencions(oEmp, fchfrom, fchto)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els ibans dels reps")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/reps/sprite")>
    Public Function sprite(guids As List(Of Guid)) As System.Net.Http.HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSprite = BEBL.Reps.Sprite(guids, 48, 48)
            retval = MyBase.HttpImageResponseMessage(oSprite)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar el sprite")
        End Try
        Return retval
    End Function

End Class