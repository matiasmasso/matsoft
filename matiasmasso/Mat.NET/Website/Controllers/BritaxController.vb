Public Class BritaxController
    Inherits _MatController

    'Feed for Hatch
    Public Async Function Stocks(MerchantId As Integer) As Threading.Tasks.Task(Of FileContentResult)
        Dim exs As New List(Of Exception)
        Dim oStream As Byte() = Nothing
        Dim oSite As DTOWtbolSite = Await FEB.WtbolSite.FromMerchantId(exs, MerchantId)

        If exs.Count = 0 AndAlso oSite IsNot Nothing AndAlso FEB.WtbolSite.Load(oSite, exs) Then
            Dim Ip As String = Web.HttpContext.Current.Request.UserHostAddress
            Await FEB.WtbolSite.Log(exs, oSite, Ip)
            Dim src As String = Await FEB.WtbolStocks.Xml(exs, Website.GlobalVariables.Emp, oSite)
            oStream = System.Text.Encoding.UTF8.GetBytes(src)
        End If

        Dim retval As New FileContentResult(oStream, "text/xml")
        Return retval
    End Function


    Public Async Function StoreLocator() As Threading.Tasks.Task(Of XmlResult)
        Dim exs As New List(Of Exception)
        Dim value = Await FEB.Britax.StoreLocator(exs)
        Dim retval As New XmlResult(value)
        Return retval
    End Function


    Public Async Function Forecast() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.Manufacturer})
            Case AuthResults.success
                Dim oMgz = Website.GlobalVariables.Emp.Mgz
                'Dim oForecasts = Await FEB.Forecasts.All(oMgz, exs, oProveidor:=oProveidor, FromNowOn:=True)
                'oForecasts = oForecasts.Where(Function(x) x.Forecasts.Any(Function(y) y.Target > 0)).ToList
                '        Dim oSheet = DTOProductSkuForecast.Excel(_Forecasts)

                Dim oExcelSheet = Await FEB.Forecasts.Excel(oUser, oMgz, exs)
                retval = Await MyBase.ExcelResult(oExcelSheet)
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select
        Return retval
    End Function

End Class
