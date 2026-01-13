Public Class SelloutController
    Inherits _MatController

    <HttpGet>
    Function Test() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser
        If oUser Is Nothing Then
            retval = LoginOrView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.manufacturer
                    retval = View("Test", oUser)
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If

        Return retval
    End Function


    <HttpGet>
    Function Index(customer As Nullable(Of Guid)) As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser
        If oUser Is Nothing Then
            retval = LoginOrView()
        Else
            If FEB.User.Load(exs, oUser) Then
                Select Case oUser.Rol.id
                    Case DTORol.Ids.manufacturer
                        retval = View("Test", oUser)

                        'Dim oSellout = Await FEB.SellOut.Factory(exs, oUser)

                        'provisional per recuperar Org al SQLWhereEmp
                        'oSellout.user.emp = Website.GlobalVariables.Emp

                        'If customer IsNot Nothing Then
                        'Dim oCustomer = Await FEB.Customer.Find(exs, customer)
                        'oSellout.AddFilter(DTOSellOut.Filter.Cods.Customer, {oCustomer})
                        'End If
                        'oSellout = Await FEB.SellOut.Load(exs, oSellout)
                        'If exs.Count = 0 Then
                        'retval = View("Sellout", oSellout)
                        'Else
                        'retval = View("Error")
                        'End If
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
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oSellOut = Await FEB.SellOut.Factory(oUser, year, conceptType, format, brand, category, channel, country, zona, location, contact, groupbyholding)
        oSellOut = Await FEB.SellOut.Load(exs, oSellOut)
        Return PartialView("Sellout_", oSellOut)
    End Function


    <HttpPost>
    Public Async Function Excel(year As Integer, conceptType As DTOSellOut.ConceptTypes, format As DTOSellOut.ConceptTypes, brand As String, category As String, channel As String, country As String, zona As String, location As String, contact As String, groupbyholding As Boolean) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim retval As JsonResult = Nothing
        Dim oSellOut = Await FEB.SellOut.Factory(oUser, year, conceptType, format, brand, category, channel, country, zona, location, contact, groupbyholding)

        'provisional per recuperar Org al SQLWhereEmp
        oSellOut.user.emp = Website.GlobalVariables.Emp

        Dim pSellOut = Await FEB.SellOut.Load(exs, oSellOut)
        If exs.Count = 0 Then
            Dim oSheet As MatHelper.Excel.Sheet = DTOSellOut.Excel(exs, pSellOut)
            If exs.Count = 0 Then
                Dim bytes As Byte() = MatHelper.Excel.ClosedXml.Bytes(oSheet)

                Dim handle As String = Guid.NewGuid().ToString()
                TempData(handle) = bytes
                retval = New JsonResult() With {
                        .Data = New With {.FileGuid = handle, .FileName = "Sellout Report.xlsx"}
                        }
            End If

        End If
        Return retval
    End Function


    Public Async Function ExcelFull(id As Integer) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser Is Nothing Then
            retval = LoginOrView()
        ElseIf oUser.Rol.id <> DTORol.Ids.manufacturer Then
            Dim year As Integer = id
            Dim oSheet As MatHelper.Excel.Sheet = Await FEB.SellOut.ExcelRawData(exs, year, oUser)
            retval = Await MyBase.ExcelResult(oSheet)
        End If
        Return retval
    End Function


    Function HelloWorld() As String
        Dim retval As String = "HelloWorld"
        Return retval
    End Function

    Async Function Zonas(id As Guid) As Threading.Tasks.Task(Of String)
        Dim exs As New List(Of Exception)
        Dim oCountryGuid As Guid = id
        Dim oLocations As List(Of DTOLocation) = Nothing
        Dim oUser = ContextHelper.GetUser()
        Dim oSellOut = Await FEB.SellOut.Factory(exs, oUser)

        Dim oCountries As List(Of DTOCountry) = Await FEB.SellOut.Countries(exs, oSellOut)
        Dim oCountry As DTOCountry = oCountries.FirstOrDefault(Function(x) x.Guid.Equals(oCountryGuid))
        Dim oZonas As New List(Of DTOZona)
        If oCountry IsNot Nothing Then
            oZonas = oCountry.Zonas
        End If

        Dim oJSON As New MatJSonArray
        oJSON.Add(New MatJSonObject("value", "", "text", oSellOut.Lang.Tradueix("(todas las zonas)", "(totes les zones)", "(all zones)")))
        For Each oZona As DTOZona In oZonas
            oJSON.Add(New MatJSonObject("value", oZona.Guid.ToString, "text", oZona.Nom))
        Next

        Dim retval As String = oJSON.ToString
        Return retval
    End Function


    Async Function Locations(id As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oZonaGuid As Guid = id
        Dim oLocations As List(Of DTOLocation) = Nothing
        Dim oUser = ContextHelper.GetUser()
        Dim oSellOut = Await FEB.SellOut.Factory(exs, oUser)
        'provisional per recuperar Org al SQLWhereEmp
        oSellOut.User.Emp = Website.GlobalVariables.Emp

        Dim oCountries As List(Of DTOCountry) = Await FEB.SellOut.Countries(exs, oSellOut)
        For Each oCountry In oCountries
            Dim oZona As DTOZona = oCountry.Zonas.FirstOrDefault(Function(x) x.Guid = oZonaGuid)
            If oZona IsNot Nothing Then
                oLocations = oZona.Locations
                Exit For
            End If
        Next

        Dim retval As New List(Of DTOValueText)
        Dim oLang = DTOApp.current.lang
        retval.Add(DTOValueText.Factory("", oLang.Tradueix("(todas las poblaciones)", "(totes les poblacions)", "(all locations)")))
        For Each oLocation In oLocations
            retval.Add(DTOValueText.Factory(oLocation.Guid.ToString, oLocation.Nom))
        Next
        Return Json(retval, JsonRequestBehavior.AllowGet)
    End Function

    Async Function GetStat() As Threading.Tasks.Task(Of DTOStat)
        Dim exs As New List(Of Exception)
        Dim retval As New DTOStat()
        retval.Lang = ContextHelper.Lang()
        Dim oUser = ContextHelper.GetUser()
        If oUser.Rol.id = DTORol.Ids.Manufacturer Then
            Dim oContact = Await ContextHelper.Contact(exs)
            If oContact IsNot Nothing Then
                retval.Proveidor = DTOProveidor.FromContact(oContact)
            End If
        End If

        Return retval
    End Function
End Class
