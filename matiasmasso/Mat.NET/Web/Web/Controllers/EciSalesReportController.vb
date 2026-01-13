Public Class EciSalesReportController
    Inherits _MatController

    <HttpGet>
    Async Function Index(customer As Nullable(Of Guid)) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser
        If oUser Is Nothing Then
            retval = LoginOrView()
        Else
            If FEB2.User.Load(exs, oUser) Then
                Select Case oUser.Rol.id
                    Case DTORol.Ids.manufacturer
                        Dim oSellout = DTOSellOut.Factory(oUser, oConceptType:=DTOSellOut.ConceptTypes.centres)
                        Dim oProveidor = Await FEB2.User.GetProveidor(oUser, exs)
                        If exs.Count = 0 AndAlso oProveidor IsNot Nothing Then
                            oSellout.AddFilter(DTOSellOut.Filter.Cods.Provider, {oProveidor})
                            Dim oCustomers As New List(Of DTOCustomer)
                            oCustomers.Add(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.ElCorteIngles))
                            oCustomers.Add(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.Eciga))
                            oSellout.AddFilter(DTOSellOut.Filter.Cods.Provider, oCustomers)

                            If FEB2.EciSalesReport.Load(oSellout, exs) Then
                                retval = View("Sellout", oSellout)
                            Else
                                If Debugger.IsAttached Then Stop
                                retval = View("Error")
                            End If
                        Else
                            If Debugger.IsAttached Then Stop
                            retval = View("Error")
                        End If

                    Case Else
                        retval = MyBase.UnauthorizedView()
                End Select
            Else
                retval = View("error")
            End If
        End If
        Return retval
    End Function


    <HttpPost>
    Async Function Index(year As Integer, conceptType As DTOSellOut.ConceptTypes, format As DTOSellOut.Formats, brand As String, category As String, channel As String, country As String, zona As String, location As String, contact As String, groupbyholding As Boolean) As Threading.Tasks.Task(Of PartialViewResult)
        Dim oSellOut As DTOSellOut = Await FEB2.EciSalesReport.Factory(ContextHelper.GetUser(), year, conceptType, format, brand, category, channel, country, zona, location, contact, groupbyholding)
        Dim exs As New List(Of Exception)
        If FEB2.EciSalesReport.Load(oSellOut, exs) Then
            Return PartialView("Sellout_", oSellOut)
        End If
    End Function


    <HttpPost>
    Public Async Function Excel(year As Integer, conceptType As DTOSellOut.ConceptTypes, format As DTOSellOut.ConceptTypes, brand As String, category As String, channel As String, country As String, zona As String, location As String, contact As String, groupbyholding As Boolean) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As JsonResult = Nothing
        Dim oSellOut As DTOSellOut = Await FEB2.EciSalesReport.Factory(ContextHelper.GetUser(), year, conceptType, format, brand, category, channel, country, zona, location, contact, groupbyholding)
        Dim exs As New List(Of Exception)
        If FEB2.EciSalesReport.Load(oSellOut, exs) Then
            Dim oSheet As ExcelHelper.Sheet '= FEB2.EciSalesReport.Excel(oSellOut)
            Dim bytes As Byte() = Nothing
            If LegacyHelper.ExcelHelper.Stream(oSheet, bytes, exs) Then
                Dim handle As String = Guid.NewGuid().ToString()
                TempData(handle) = bytes
                retval = New JsonResult() With {
                .Data = New With {.FileGuid = handle, .FileName = "TestReportOutput.xlsx"}
                }
            End If
        End If
        Return retval
    End Function


    Async Function Zonas(id As String) As Threading.Tasks.Task(Of String)
        Dim exs As New List(Of Exception)
        Dim oCountryGuid As Guid = New Guid(id)
        Dim oLocations As List(Of DTOLocation) = Nothing
        Dim oSellOut As DTOSellOut = Await FEB2.EciSalesReport.Factory(ContextHelper.GetUser)
        Dim oCountries As List(Of DTOCountry) = FEB2.EciSalesReport.CountriesSync(oSellOut, exs)
        Dim oCountry As DTOCountry = oCountries.FirstOrDefault(Function(x) x.Guid.Equals(oCountryGuid))
        Dim oZonas As New List(Of DTOZona)
        If oCountry IsNot Nothing Then
            oZonas = oCountry.Zonas
        End If

        Dim oJSON As New MatJSonArray
        oJSON.Add(New MatJSonObject("value", "", "text", ContextHelper.Lang.Tradueix("(todas las zonas)", "(totes les zones)", "(all zones)")))
        For Each oZona As DTOZona In oZonas
            oJSON.Add(New MatJSonObject("value", oZona.Guid.ToString, "text", oZona.Nom))
        Next

        Dim retval As String = oJSON.ToString
        Return retval
    End Function


    Async Function Locations(id As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim oZonaGuid As Guid = id
        Dim oLocations As List(Of DTOLocation) = Nothing
        Dim oSellOut As DTOSellOut = Await FEB2.EciSalesReport.Factory(ContextHelper.GetUser)
        Dim oCountries As List(Of DTOCountry) '= FEB2.EciSalesReport.Countries(oSellOut)
        For Each oCountry In oCountries
            Dim oZona As DTOZona = oCountry.Zonas.FirstOrDefault(Function(x) x.Guid = oZonaGuid)
            If oZona IsNot Nothing Then
                oLocations = oZona.Locations
                Exit For
            End If
        Next

        Dim retval As New List(Of DTOValueText)
        Dim oLang = ContextHelper.Lang
        retval.Add(DTOValueText.Factory("", oLang.Tradueix("(todas las poblaciones)", "(totes les poblacions)", "(all locations)")))
        For Each oLocation In oLocations
            retval.Add(DTOValueText.Factory(oLocation.Guid.ToString, oLocation.Nom))
        Next
        Return Json(retval, JsonRequestBehavior.AllowGet)
    End Function

    Async Function GetStat() As Threading.Tasks.Task(Of DTOStat)
        Dim exs As New List(Of Exception)
        Dim retval As New DTOStat()
        retval.Lang = ContextHelper.Lang
        Dim oUser = ContextHelper.GetUser()
        If oUser.Rol.id = DTORol.Ids.Manufacturer Then
            Dim oContact = Await ContextHelper.Contact(exs)
            If oContact IsNot Nothing Then
                retval.Proveidor = New DTOProveidor(oContact.Guid)
            End If
        End If

        Return retval
    End Function
End Class
