Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SellOutController
    Inherits _BaseController


    <HttpPost>
    <Route("api/sellout")>
    Public Function Load(<FromBody> value As DTOSellOut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            value.User.Emp = MyBase.GetEmp(value.User.Emp.Id) 'perque necessita .Emp.Org a sqlwhereemp
            BEBL.SellOut.Load(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el SellOut")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/sellout/years")>
    Public Function Years(<FromBody> oSellOut As DTOSellOut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            oSellOut.user.emp = MyBase.GetEmp(oSellOut.user.emp.Id) 'recupera Org
            Dim values = BEBL.SellOut.Years(oSellOut)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els anys del SellOut")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/sellout/reps")>
    Public Function Reps(<FromBody> oSellOut As DTOSellOut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SellOut.Reps(oSellOut)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els reps del SellOut")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/sellout/proveidors")>
    Public Function Proveidors(<FromBody> oSellOut As DTOSellOut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SellOut.Proveidors(oSellOut)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els proveidors del SellOut")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/sellout/countries")>
    Public Function Countries(<FromBody> oSellOut As DTOSellOut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SellOut.Countries(oSellOut)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els paisos del SellOut")
        End Try
        Return retval
    End Function

    <Route("api/sellout/channels")>
    Public Function Channels(<FromBody> oSellOut As DTOSellOut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SellOut.Channels(oSellOut)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els canals del SellOut")
        End Try
        Return retval
    End Function

    <Route("api/sellout/brands")>
    Public Function Brands(<FromBody> oSellOut As DTOSellOut) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SellOut.Brands(oSellOut)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les marques del SellOut")
        End Try
        Return retval
    End Function


    <Route("api/sellout/RawDataLast12Months/{proveidor}")>
    Public Function RawDataLast12Months(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.SellOut.RawDataLast12Months(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les marques del SellOut")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/sellout/excel/{year}/{user}")>
    Public Function ExcelRawData(year As Integer, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            oUser.Emp = MyBase.GetEmp(oUser.Emp.Id) 'recupera Org
            Dim oYearMonthTo As New DTOYearMonth(year, 12)
            Dim oSellOut = DTOSellOut.Factory(oUser, oYearMonthTo, DTOSellOut.ConceptTypes.full)
            oSellOut.AddFilter(DTOSellOut.Filter.Cods.Provider, {oUser.Contact})
            Dim oSheet = BEBL.SellOut.RawExcel(oSellOut)
            retval = Request.CreateResponse(HttpStatusCode.OK, oSheet)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el SellOut")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/sellout/data/{user}/{year}")>
    Public Function RawData(user As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            oUser.Emp = MyBase.GetEmp(oUser.Emp.Id) 'recupera Org
            Dim value = BEBL.SellOut.RawData(oUser, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el SellOut")
        End Try
        Return retval
    End Function


End Class
