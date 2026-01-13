Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class MvcHomeController
    Inherits _BaseController

    <HttpGet>
    <Route("api/MvcHome/Model/{emp}/{lang}/{user}")>
    Public Function Model(emp As DTOEmp.Ids, lang As String, user As Guid) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim oEmp As DTOEmp
        Dim oLang As DTOLang
        Dim oUser As DTOUser = Nothing
        Dim retval As HttpResponseMessage = Nothing
        Try
            oEmp = New DTOEmp(emp)
            oLang = DTOLang.Factory(lang)
            If user <> Guid.Empty Then oUser = BEBL.User.Find(user)

            Dim value = BEBL.MvcHome.Model(oEmp, oLang, oUser)
            retval = Request.CreateResponse(Of MvcHomeModel)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, String.Format("error al llegir la Home. Emp:{0}, Lang:{1}, User:{2}", emp, lang, user))
        End Try
        Return retval
    End Function

End Class
