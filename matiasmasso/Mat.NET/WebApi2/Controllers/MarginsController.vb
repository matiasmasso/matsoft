Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class MarginsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Margins/{emp}/{year?}/{mode?}/{target?}")>
    Public Function All(emp As DTOEmp.Ids, Optional year As Nullable(Of Integer) = Nothing, Optional mode As Nullable(Of Integer) = 0, Optional target As Nullable(Of Guid) = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = GetEmp(emp)
            mode = If(mode, DTO.Models.MarginsModel.Modes.Full)
            year = If(year, DTO.GlobalVariables.Today().Year)
            Dim values = BEBL.Margins.Fetch(oEmp, year, mode, target)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els marges comercials")
        End Try
        Return retval
    End Function

End Class
