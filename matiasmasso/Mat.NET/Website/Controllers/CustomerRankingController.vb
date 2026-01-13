Public Class CustomerRankingController
    Inherits _MatController

    '
    ' GET: /CustomerRanking

    Function Index() As ActionResult
        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.superUser,
                                               DTORol.Ids.admin,
                                               DTORol.Ids.salesManager,
                                               DTORol.Ids.marketing,
                                               DTORol.Ids.accounts,
                                               DTORol.Ids.manufacturer,
                                               DTORol.Ids.comercial,
                                               DTORol.Ids.rep})
            Case AuthResults.success
                Dim exs As New List(Of Exception)
                Dim oUser = ContextHelper.GetUser()
                Dim Model = DTOCustomerRanking.Factory(oUser)
                FEB.CustomerRanking.Load(Model, exs)
                retval = View("CustomerRanking", Model)
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Function reload(data As String) As PartialViewResult
        Dim exs As New List(Of Exception)
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim oSrc As ControlItem = jss.Deserialize(Of ControlItem)(data)

        Dim oUser = ContextHelper.GetUser()
        Dim Model = DTOCustomerRanking.Factory(oUser)
        With Model
            .FchFrom = oSrc.FchFrom
            .FchTo = oSrc.FchTo
            If Not oSrc.Area.Equals(Guid.Empty) Then
                .Area = New DTOArea(oSrc.Area)
            End If
        End With
        FEB.CustomerRanking.Load(Model, exs)
        Return PartialView("_CustomerRanking", Model)
    End Function

    Async Function Zonas(country As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oCountry As New DTOCountry(country)
        Dim oUser = ContextHelper.GetUser()
        Dim oZonas As List(Of DTOZona) = Await FEB.Contacts.Zonas(exs, oUser, oCountry)
        Dim retval As JsonResult = Json(oZonas, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function csvUrl(data As String) As JsonResult
        Dim exs As New List(Of Exception)
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim oSrc As ControlItem = jss.Deserialize(Of ControlItem)(data)

        Dim oUser = ContextHelper.GetUser()
        Dim Model = DTOCustomerRanking.Factory(oUser)
        With Model
            .FchFrom = oSrc.FchFrom
            .FchTo = oSrc.FchTo
            If Not oSrc.Area.Equals(Guid.Empty) Then
                .Area = New DTOArea(oSrc.Area)
            End If
        End With
        Dim url As String = FEB.CustomerRanking.CsvUrl(Model)

        Dim myData As Object = New With {.result = url}
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Private Class ControlItem
        Property FchFrom As Date
        Property FchTo As Date
        Property Area As Guid
    End Class
End Class