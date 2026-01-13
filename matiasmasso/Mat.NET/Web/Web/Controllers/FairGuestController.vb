Public Class FairGuestController
    Inherits _MatController


    Function SignUp() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oContact As DTOContact = GetSession.Contact
        Select Case MyBase.Authorize({DTORol.Ids.CliFull, DTORol.Ids.CliLite})
            Case AuthResults.success
                Dim oContacts As List(Of DTOCustomer) = BLLUser.CustomersRaonsSocials(MyBase.GetSession.User)
                If oContacts.Count > 0 Then
                    retval = View()
                Else
                    retval = MyBase.UnauthorizedView()
                End If
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Function Update() As ActionResult
        Dim oTextInfo As System.Globalization.TextInfo = New System.Globalization.CultureInfo("es-ES", False).TextInfo
        Dim oEventGuid As New Guid("6EF358CC-1DA2-4B54-BF25-5DD4FDAD83D3")
        Dim oEvento As New DTOEvento(oEventGuid)
        Dim oFairGuest As New DTOFairGuest(oEvento)
        With oFairGuest
            .FirstName = oTextInfo.ToTitleCase(Request.Form("FirstName").ToLower)
            .LastName = oTextInfo.ToTitleCase(Request.Form("LastName").ToLower)
            .Position = oTextInfo.ToTitleCase(Request.Form("Position").ToLower)
            .NIF = Request.Form("NIF")
            .RaoSocial = Request.Form("raosocial")
            .ActivityCode = Request.Form("activitycode")
            .Address = oTextInfo.ToTitleCase(Request.Form("address").ToLower)
            .Zip = Request.Form("zip")
            .Location = oTextInfo.ToTitleCase(Request.Form("location").ToLower)
            .Country = New DTOCountry(New Guid(Request.Form("country").ToString))
            .Phone = Request.Form("phone")
            .CellPhone = Request.Form("cellphone")
            .Fax = Request.Form("fax")
            .Email = Request.Form("email").Replace(" ", "").ToLower
            .web = Request.Form("web").ToLower
            .CodeDistance = Request.Form("distance")
            .Evento = oEvento
            .UserCreated = GetSession.User()
        End With

        Dim myData As Object = Nothing
        Dim exs As New List(Of Exception)
        If BLLFairGuest.Update(oFairGuest, exs) Then
            myData = New With {.result = "1", .id = oFairGuest.Guid.ToString, .template = CInt(BLL.BLLMailing.Templates.FairGuestConfirmation), .param1 = oFairGuest.Guid.ToString, .message = ""}
        Else
            myData = New With {.result = "0", .message = "error"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Shadows Function LoginOrView() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oActiveCustomers As List(Of DTO.DTOCustomer) = Nothing
        If GetSession.IsAuthenticated Then
            Select Case GetSession.User.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.SalesManager, DTORol.Ids.Admin, DTORol.Ids.Manufacturer, DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.Operadora
                    Dim oContact As DTOContact = BLLSession.Contact(GetSession)
                    If oContact IsNot Nothing Then
                        Dim oCustomer As New DTOCustomer(oContact.Guid)
                        oActiveCustomers = New List(Of DTOCustomer)
                        oActiveCustomers.Add(oCustomer)
                        ViewData("test") = "1"
                    End If
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    oActiveCustomers = BLLUser.GetCustomers(GetSession.User).FindAll(Function(x) x.Obsoleto = False)
            End Select

            If oActiveCustomers Is Nothing Then
                retval = MyBase.UnauthorizedView()
            ElseIf oActiveCustomers.Count = 0 Then
                retval = MyBase.UnauthorizedView()
            Else
                retval = View()
            End If

        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
        End If
        Return retval
    End Function


End Class