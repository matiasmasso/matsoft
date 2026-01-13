Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaFileController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaFile/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaFile.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el fitxer Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/EdiversaFile/FromResultGuid/{resultGuid}")>
    Public Function FromResultGuid(resultGuid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaFile.FromResultGuid(resultGuid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el fitxer Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversa/PreProcessOrder/{guid}")>
    Public Async Function PreProcessOrder(guid As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oFile = BEBL.EdiversaFile.Find(guid)
            If Await BEBL.EdiversaFile.PreProcessOrder(exs, oFile) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaFile")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaFile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversa/PreProcessOrders")>
    Public Async Function PreProcessOrders() As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If Await BEBL.EdiversaFiles.PreProcessOrders(exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaFile")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaFile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversa/Procesa/{guid}")>
    Public Async Function Procesa(guid As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oFile = BEBL.EdiversaFile.Find(guid)
            Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)

            If Await BEBL.EdiversaFile.Procesa(oEmp, oFile, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaFile")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaFile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversa/Orders/deleteduplicates")>
    Public Function DeleteDuplicatedOrders() As DTOTaskResult
        'For debug. Cal passae PreProcessOrders a continuació
        Dim retval = BEBL.EdiversaFiles.DeleteDuplicatedOrders()
        Return retval
    End Function

    <HttpPost>
    <Route("api/EdiversaFile/restore/{emp}")>
    Public Function Restore(emp As DTOEmp.Ids, <FromBody> oEdiversaFile As DTOEdiversaFile) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            If BEBL.EdiversaFile.Restore(oEdiversaFile, oEmp.Org, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, oEdiversaFile)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al restaurar el fitxer Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al restaurar el fitxer Edi")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ediversa/readpending")>
    Public Function ReadPending() As List(Of DTOEdiversaFile)
        Dim sb As New Text.StringBuilder
        Dim retval As New List(Of DTOEdiversaFile)
        Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
        Dim exs As New List(Of DTOEdiversaException)
        If BEBL.EdiversaFileSystem.ReadPending(oEmp, retval, exs) Then
            Dim iComandes As Integer = retval.Where(Function(x) x.Tag = DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString()).Count
            sb.Append(String.Format("{0} comandes pendents de importar del servidor", iComandes))
        Else
            Dim exs2 As List(Of Exception) = DTOEdiversaException.ToSystemExceptions(exs)
            sb.Append(ExceptionsHelper.ToFlatString(exs2))
        End If

        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversa/procesaInbox/{user}")>
    Public Function ProcessaInbox(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim oUser = BEBL.User.Find(user)
        If oUser IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If BEBL.Ediversa.ProcessaInbox(oUser, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs)
            End If
        End If
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversa/PendingToWriteToOutbox")>
    Public Function PendingToWriteToOutbox() As List(Of DTOEdiversaFile)
        Dim retval = BEBL.EdiversaFiles.PendingToWriteToOutbox
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversa/FromNumComanda/{emp}/{year}/{NumComanda}")>
    Public Function FromNumComanda(emp As DTOEmp.Ids, year As Integer, NumComanda As String) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Dim oEmp As New DTOEmp(emp)
        Try
            Dim value = BEBL.EdiversaOrder.FromNumComanda(oEmp, year, NumComanda)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(exs)
        End Try

        Return retval
    End Function


    <HttpPost>
    <Route("api/ediversafile")>
    Public Function UpdateWrittenFileToOutbox(value As DTOEdiversaFile) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaFile.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaFie")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaFie")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ediversafile/QueueInvoice")>
    Public Function QueueInvoice(<FromBody> value As DTOInvoicePrintLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaFile.QueueInvoice(exs, value) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaFie")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaFie")
        End Try
        Return retval
    End Function
End Class

Public Class EdiversaFilesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ediversafiles/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.EdiversaFiles.All(OnlyOpenFiles:=False)
            retval = Request.CreateResponse(Of List(Of DTOEdiversaFile))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al processar el fitxer Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversafiles/tags/{emp}/{year}/{iocod}")>
    Public Function All(emp As Integer, year As Integer, IOcod As DTOEdiversaFile.IOcods) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.EdiversaFiles.Tags(oEmp, year, IOcod)
            retval = Request.CreateResponse(Of List(Of String))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al processar el fitxer Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversafiles/{emp}/{year}/{iocod}/{tag}")>
    Public Function All(emp As Integer, year As Integer, iocod As DTOEdiversaFile.IOcods, tag As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.EdiversaFiles.All(oEmp, year, iocod, tag)
            retval = Request.CreateResponse(Of List(Of DTOEdiversaFile))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al processar el fitxer Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversafiles/openFiles/{emp}")>
    Public Function OpenFiles(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.EdiversaFiles.All(OnlyOpenFiles:=True)
            retval = Request.CreateResponse(Of List(Of DTOEdiversaFile))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al processar el fitxer Edi")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ediversafiles/saveInboxFile")>
    Public Async Function SaveInboxFile(oEdiversaFile As DTOEdiversaFile) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            If Await BEBL.EdiversaFile.SaveInboxFile(oEmp, oEdiversaFile, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al processar el fitxer Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al processar el fitxer Edi")
        End Try
        Return retval
    End Function


    'Ho crida MatSchedService un cop ha mogut els fitxers de bandeja Edi
    <HttpPost>
    <Route("api/ediversafiles/saveInboxFiles")>
    Public Async Function SaveInboxFiles(oEdiversaFiles As List(Of DTOEdiversaFile)) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            If Await BEBL.EdiversaFiles.SaveInboxFiles(oEmp, oEdiversaFiles, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al processar els fitxers Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al processar els fitxers Edi")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ediversafiles/delete")>
    Public Function Delete(<FromBody> values As List(Of DTOEdiversaFile)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaFiles.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar las ediversafiles")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar las ediversafiles")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/EdiversaFiles/restore/{emp}")>
    Public Function Restore(emp As DTOEmp.Ids, <FromBody> oEdiversaFiles As List(Of DTOEdiversaFile)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            If BEBL.EdiversaFiles.Restore(oEmp, oEdiversaFiles, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al restaurar els fitxers Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al restaurar els fitxers Edi")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/EdiversaFiles/Descarta")>
    Public Function Descarta(<FromBody> oEdiversaFiles As List(Of DTOEdiversaFile)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaFiles.Descarta(oEdiversaFiles, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al restaurar els fitxers Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al restaurar els fitxers Edi")
        End Try
        Return retval
    End Function
End Class

