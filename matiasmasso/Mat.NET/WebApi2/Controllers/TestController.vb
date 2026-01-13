Imports System.Drawing
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports DAL

Public Class TestController
    Inherits _BaseController

    <Route("api/test/helloworld")>
    <HttpPost>
    Public Function PostHelloWorld(oTask As DTOTask) As DTOTaskResult
        Dim retval As New DTOTaskResult
        retval.Msg = "Hello World!"
        retval.ResultCod = DTOTask.ResultCods.Success
        Return retval
    End Function


    <Route("api/test")>
    <HttpGet>
    Public Function DoTest() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Try
                DAL.ProductCategoriesLoader.CreateThumbnails()
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al executar el test")
            End Try
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al executar el test")
        End Try
        Return retval
    End Function
    Public Function DoTestOld() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Try
                Procede()
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al executar el test")
            End Try
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al executar el test")
        End Try
        Return retval
    End Function

    Private Sub Procede()
        Dim usr = DTOUser.Wellknown(DTOUser.Wellknowns.matias)
        BEBL.User.Load(usr)
        Dim items = BEBL.WinMenuItems.All(usr)
        For Each item In items
            Dim icon = BEBL.WinMenuItem.Icon(item.Guid)
            If icon IsNot Nothing Then
                Dim mime = MatHelperStd.MimeHelper.GuessMime(icon.ByteArray)
                If mime <> icon.Mime Then
                    Dim exs As New List(Of Exception)
                    Dim menuitem = BEBL.WinMenuItem.Find(item.Guid)
                    menuitem.icon = icon.ByteArray
                    menuitem.Mime = mime
                    BEBL.WinMenuItem.Update(menuitem, exs)
                    If exs.Count > 0 Then
                        Stop
                    End If
                End If
            End If

        Next
    End Sub

    Private Sub AddResults(text As String, reg_exp As Regex, results As List(Of String))
        Dim matches As MatchCollection = reg_exp.Matches(text)
        For Each a_match As Match In matches
            If a_match.Value.Contains("/blog/wp-content/uploads/") Then
                Dim attribute = a_match.Value.Split(" ").FirstOrDefault(Function(x) x.Contains("/blog/wp-content/uploads/"))
                Dim start = attribute.IndexOf("http")
                Dim result = attribute.Substring(start).Replace("""", "")
                If Not results.Contains(result) Then
                    results.Add(result)
                End If
            End If
        Next a_match

    End Sub

    Public Class imgsrc
        Property wpSrc As String
        Property mmSrc As String

        Public Sub New(a_match As Match)
            Dim result = a_match.Value
            Dim attribute = result.Split(" ").FirstOrDefault(Function(x) x.Contains("/blog/wp-content/uploads/"))
            Dim start = attribute.IndexOf("http")
            _wpSrc = attribute.Substring(start).Replace("""", "")
        End Sub

    End Class

    Class test
        Property ISO8601 As String
        Property text As String

        Public Sub New(mins As Integer)
            MyBase.New
            _ISO8601 = TimeHelper.ISO8601(mins)
            Dim oTimeSpan = TimeHelper.fromISO8601(_ISO8601)
            Try
                _text = String.Format("time: {0:hh\:mm}", oTimeSpan)
            Catch ex As Exception
                Stop
            End Try
        End Sub
    End Class

    <Route("api/test")>
    <HttpPost>
    Public Function comanda(oPurchaseOrder As DTOPurchaseOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            BEBL.Integracions.Edi.Invrpts.ProcesaOpenFiles(exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al insertar la data")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al insertar la data")
        End Try
        Return retval
    End Function

    <Route("api/test/helloWorld")>
    <HttpGet>
    Public Function GetHelloWorld() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            retval = Request.CreateResponse(HttpStatusCode.OK, "Hello World")
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al request hello world")
        End Try
        Return retval
    End Function




End Class
