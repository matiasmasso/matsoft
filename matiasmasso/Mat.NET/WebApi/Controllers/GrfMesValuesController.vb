Imports System.Net.Http
Imports System.Web.Http

Public Class GrfMesValueController
    Inherits _BaseController


    <HttpGet>
    <Route("api/GrfMesValue/image/{user}")>
    Public Function GetIcon(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Img.Factory(Defaults.ImgTypes.SalesGrafic, user)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del GrfMesValue")
        End Try
        Return retval
    End Function


End Class


