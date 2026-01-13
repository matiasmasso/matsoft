Public Class QuizController
    Inherits _MatController



    Private Class MvcQuizResult
        Property User As Guid
        Property Answers As List(Of Guid)
    End Class

    Function PromoFisher1() As ActionResult
        Return LoginOrView()
    End Function

    <HttpPost>
    Function PromoFisher1(data As String) As ActionResult
        Dim oUser As DTOUser = GetSession.User
        Dim oCustomer As New DTOCustomer(New Guid(Request.Form("customer")))
        Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Factory(oCustomer, oUser, Today, DTOPurchaseOrder.Sources.cliente_por_Web, "promoción Fisher-Price")
        With oPurchaseOrder
            '.UsrCreatedGuid = GetSession.User.Guid
            '.UsrLastEditedGuid = GetSession.User.Guid
            ' .Incentiu = New DTOIncentiu(New Guid("5052fb89-c5ff-4c28-aea9-3f1291cc501c"))
        End With

        Dim oSku As DTOProductSku
        For i As Integer = 0 To 3
            Dim sQtyProp As String = "product." & i.ToString & ".qty"
            Dim iQty As Integer = Request.Form(sQtyProp)
            If iQty > 0 Then
                Dim sGuidProp As String = "product." & i.ToString & ".guid"
                Dim oGuid As New Guid(Request.Form(sGuidProp))
                oSku = BLL.BLLProductSku.Find(oGuid)
                BLLPurchaseOrder.AddItem(GlobalVariables.Emp, oPurchaseOrder, oSku, iQty, 10)
            End If
        Next

        Dim myData As Object = Nothing
        Dim exs As New List(Of Exception)
        If BLLPurchaseOrder.Update(oPurchaseOrder, exs) Then
            myData = New With {.result = "1", .id = oPurchaseOrder.Num, .template = CInt(DTO.DTODefault.MailingTemplates.QuizConfirmationPromoFisher1), .param1 = oPurchaseOrder.Guid.ToString, .message = ""}
        Else
            myData = New With {.result = "0", .message = "error"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function PromoFisher() As ActionResult
        Return LoginOrView()
    End Function

    <HttpPost> _
    Function PromoFisher(data As String) As ActionResult
        Dim oUser As DTOUser = GetSession.User
        Dim oCustomer As New DTOCustomer(New Guid(Request.Form("customer")))
        Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Factory(oCustomer, oUser, Today, DTOPurchaseOrder.Sources.cliente_por_Web, "promoción Fisher-Price Junio/Julio")
        'oPurchaseOrder.UsrCreatedGuid = GetSession.User.Guid
        'oPurchaseOrder.UsrLastEditedGuid = GetSession.User.Guid
        'oPurchaseOrder.Incentiu = New DTOIncentiu(New Guid("bbe42b34-45dd-494e-84c4-625f1d7de041"))

        Dim oSku As DTOProductSku
        For i As Integer = 0 To 7
            Dim sQtyProp As String = "product." & i.ToString & ".qty"
            Dim iQty As Integer = Request.Form(sQtyProp)
            If iQty > 0 Then
                Dim sGuidProp As String = "product." & i.ToString & ".guid"
                Dim oGuid As New Guid(Request.Form(sGuidProp))
                oSku = BLL.BLLProductSku.Find(oGuid)
                BLLPurchaseOrder.AddItem(GlobalVariables.Emp, oPurchaseOrder, oSku, iQty)
            End If
        Next

        Dim myData As Object = Nothing
        Dim exs As New List(Of Exception)
        If BLLPurchaseOrder.Update(oPurchaseOrder, exs) Then
            myData = New With {.result = "1", .id = oPurchaseOrder.Num, .template = CInt(DTO.DTODefault.MailingTemplates.QuizConfirmationPromoFisher), .param1 = oPurchaseOrder.Guid.ToString, .message = ""}
        Else
            myData = New With {.result = "0", .message = "error"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function PromoDualFix() As ActionResult
        Return LoginOrView()
    End Function

    <HttpPost> _
    Function PromoDualFix(data As String) As ActionResult
        Dim oUser As DTOUser = GetSession.User
        Dim oCustomer As New DTOCustomer(New Guid(Request.Form("customer")))
        Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Factory(oCustomer, oUser, Today, DTOPurchaseOrder.Sources.cliente_por_Web, "promoción Dual-Fix en Mi Bebe Y Yo")
        ' oPurchaseOrder.UsrCreatedGuid = GetSession.User.Guid
        ' oPurchaseOrder.UsrLastEditedGuid = GetSession.User.Guid

        Dim oGuids As New List(Of Guid)
        oGuids.Add(New Guid(Request.Form("Product1")))
        oGuids.Add(New Guid(Request.Form("Product2")))

        Dim oSku As DTOProductSku
        Dim oItem As DTOPurchaseOrderItem
        For Each oGuid As Guid In oGuids
            oItem = oPurchaseOrder.Items.Find(Function(x) x.Sku.Guid.Equals(oGuid))
            If oItem Is Nothing Then
                oSku = BLL.BLLProductSku.Find(oGuid)
                BLLPurchaseOrder.AddItem(GlobalVariables.Emp, oPurchaseOrder, oSku, 1)
            Else
                oItem.Qty += 1
                oItem.Pending += 1
            End If
        Next

        Dim oPackAccesoris As New Guid("32111732-1943-4634-AAFC-8EA73B331FF3")
        oSku = BLLProductSku.Find(oPackAccesoris)
        BLLPurchaseOrder.AddItem(GlobalVariables.Emp, oPurchaseOrder, oSku, 2, , True)

        Dim myData As Object = Nothing
        Dim exs As New List(Of Exception)
        If BLLPurchaseOrder.Update(oPurchaseOrder, exs) Then
            myData = New With {.result = "1", .id = oPurchaseOrder.Num, .template = CInt(DTO.DTODefault.MailingTemplates.QuizConfirmationPromoDualFix), .param1 = oPurchaseOrder.Guid.ToString, .message = ""}
        Else
            myData = New With {.result = "0", .message = "error"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function MailConfirmation(template As Integer, param1 As String) As JsonResult
        Dim exs as new List(Of Exception)
        BLLMail.QuizConfirmation(GlobalVariables.Emp, template, GetSession.User, exs, param1)
        Dim myData As Object = New With {.result = "1"}
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

#Region "OldQuizs"


#End Region

    '-------------------------------------------------------------------- utilities -----------------------------------------------

    Shadows Function LoginOrView(sViewName As String) As ActionResult
        Dim retval As ActionResult = Nothing
        If GetSession.IsAuthenticated Then
            retval = View(sViewName)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

    Shadows Function LoginOrView(oModel As Object) As ActionResult
        Dim retval As ActionResult = Nothing
        If GetSession.IsAuthenticated Then
            retval = View(oModel)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

    Shadows Function LoginOrView() As ActionResult
        Dim retval As ActionResult = Nothing
        Dim oActiveCustomers As List(Of DTO.DTOCustomer) = Nothing
        If GetSession.IsAuthenticated Then
            Select Case GetSession.User.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.SalesManager, DTORol.Ids.Admin, DTORol.Ids.Manufacturer, DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.Operadora
                    Dim oContact As DTOContact = GetSession.Contact
                    Dim oCustomer As New DTOCustomer(oContact.Guid)
                    oActiveCustomers = New List(Of DTOCustomer)
                    oActiveCustomers.Add(oCustomer)
                    ViewData("test") = "1"
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