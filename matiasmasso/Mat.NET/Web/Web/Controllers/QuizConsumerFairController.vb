Public Class QuizConsumerFairController
    Inherits _MatController

    Private _Evento As String = "0261ED62-45DB-4418-95B4-2282EB23BD35"

    Function Apuntame() As ActionResult
        Dim retval As ActionResult = LoginOrView("QuizConsumerFairSignup")
        Return retval
    End Function

    Function Load(customer As Guid) As JsonResult
        Dim oCustomer As New DTOCustomer(customer)

        Dim Model As New List(Of RawQuiz)
        Dim oQuizs As List(Of DTOQuizConsumerFair) = BLLQuizConsumerFair.FromContact(oCustomer)
        For Each oQuiz As DTOQuizConsumerFair In oQuizs
            Dim item As New RawQuiz
            With item
                .contact = oQuiz.Contact.Guid
                .brand = oQuiz.Brand.Guid
                .tpv2pax = oQuiz.Tpv2pax
                .franja = oQuiz.Franja
            End With
            Model.Add(item)
        Next

        Dim retval As JsonResult = Json(Model, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function Save(data As String) As JsonResult

        Dim myData As Object = Nothing
        Dim ser As New System.Web.Script.Serialization.JavaScriptSerializer
        Dim raws As List(Of RawQuiz) = ser.Deserialize(Of List(Of RawQuiz))(data)

        If raws.Count > 0 Then

            Dim oQuizs As New List(Of DTOQuizConsumerFair)
            For Each raw As RawQuiz In raws
                Dim oQuiz As New DTOQuizConsumerFair
                With oQuiz
                    .Evento = New DTOEvento
                    .Brand = New DTOProductBrand(raw.brand)
                    .Contact = New DTOContact(raw.contact)
                    .Tpv2pax = raw.tpv2pax
                    .Franja = raw.franja
                End With
                oQuizs.Add(oQuiz)
            Next

            Dim exs As New List(Of Exception)
            If BLLQuizConsumerFair.Update(oQuizs, exs) Then
                myData = New With {.result = "OK", .template = CInt(DTO.DTODefault.MailingTemplates.QuizConfirmationConsumerFair), .param1 = raws(0).contact}
            Else
                myData = New With {.result = "KO", .message = "ERROR_24"}
            End If

        Else
            myData = New With {.result = "EMPTY"}
        End If

        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Shadows Function LoginOrView(sView As String) As ActionResult
        Dim retval As ActionResult = Nothing
        If GetSession.IsAuthenticated Then
            retval = View(sView)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function


    Private Class RawQuiz
        Property tpv2pax As Boolean
        Property brand As Guid
        Property contact As Guid
        Property franja As Integer
    End Class

End Class