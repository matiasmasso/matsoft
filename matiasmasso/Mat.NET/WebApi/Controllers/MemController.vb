Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MemController
    Inherits _BaseController



    <HttpGet>
    <Route("api/Mem/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Mem.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el raport")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/mem/Sprite/{mem}")>
    Public Function Sprite(mem As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMem = New DTOMem(mem)
            Dim value = BEBL.Mem.Sprite(oMem)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el mem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/mem")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOMem)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el mem")
            Else
                Dim idx As Integer
                For Each oDocFile In value.docfiles
                    idx += 1
                    oDocFile.Thumbnail = oHelper.GetImage(String.Format("docfile_thumbnail_{0:000}", idx))
                    oDocFile.Stream = oHelper.GetFileBytes(String.Format("docfile_stream_{0:000}", idx))

                    'recrea el Docfile per assignar hash i thumbnail en imatges pujades desde IOS
                    If oDocFile.hash = "" AndAlso oDocFile.Stream IsNot Nothing Then
                        BEBL.DocFile.LoadFromStream(exs, oDocFile, oDocFile.Stream, oDocFile.mime)
                        'DTODocFile.LoadFromStream(exs, value.docfiles(idx - 1), oDocFile.Stream, oDocFile.mime)
                    End If
                Next

                If BEBL.Mem.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, True) ', value)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al desar el mem")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al pujar el mem")
        End Try

        Return result
    End Function




    <HttpPost>
    <Route("api/Mem/delete")>
    Public Function Delete(<FromBody> value As DTOMem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Mem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el raport")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el raport")
        End Try
        Return retval
    End Function



End Class

Public Class MemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Mems/{contact}/{user}")>
    Public Function ForIMat2020(contact As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As DTOContact = Nothing
            If Not contact.Equals(Guid.Empty) Then
                oContact = New DTOContact(contact)
            End If
            Dim oUser As DTOUser = Nothing
            If Not user.Equals(Guid.Empty) Then oUser = BEBL.User.Find(user)
            Dim values = BEBL.Mems.All(oContact:=oContact, oUser:=oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Mems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Mems/fromUser/{user}")>
    Public Function FromUserForIMat2020(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Mems.All(oUser:=oUser, year:=DTO.GlobalVariables.Today().Year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Mems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Mems/fromRep/{rep}")>
    Public Function FromRepForIMat2020(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep As New DTORep(rep)
            Dim values = BEBL.Mems.All(fromRep:=oRep, year:=DTO.GlobalVariables.Today().Year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Mems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Mems/{contact}/{cod}/{user}/{offset}/{pagesize}/{onlyfromLast24H}")>
    Public Function All(contact As Guid, cod As Integer, user As Guid, offset As Integer, pagesize As Integer, onlyfromLast24H As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As DTOContact = Nothing
            If Not contact.Equals(Guid.Empty) Then
                oContact = New DTOContact(contact)
            End If
            Dim oUser As DTOUser = Nothing
            If Not user.Equals(Guid.Empty) Then oUser = BEBL.User.Find(user)
            Dim values = BEBL.Mems.All(oContact:=oContact, oCod:=cod, oUser:=oUser, Offset:=offset, MaxCount:=pagesize, OnlyFromLast24H:=(onlyfromLast24H = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Mems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Mems/{contact}/{cod}/{user}/{offset}/{pagesize}/{onlyfromLast24H}/{year}")>
    Public Function All(contact As Guid, cod As Integer, user As Guid, offset As Integer, pagesize As Integer, onlyfromLast24H As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As DTOContact = Nothing
            If Not contact.Equals(Guid.Empty) Then
                oContact = New DTOContact(contact)
            End If
            Dim oUser As DTOUser = Nothing
            If Not user.Equals(Guid.Empty) Then oUser = BEBL.User.Find(user)
            Dim values = BEBL.Mems.All(oContact:=oContact, oCod:=cod, oUser:=oUser, Offset:=offset, MaxCount:=pagesize, OnlyFromLast24H:=(onlyfromLast24H = 1), year:=year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Mems")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Mems/Count/{cod}/{user}")>
    Public Function ReportsCount(cod As Integer, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Mems.Count(oCod:=cod, oUser:=oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Mems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Mems/Impagats/{emp}")>
    Public Function Impagats(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Mems.Impagats(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Mems")
        End Try
        Return retval
    End Function


End Class
