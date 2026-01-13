Public Class ECIController
    Inherits _MatController

    Async Function InvRpt() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Select Case MyBase.Authorize({DTORol.Ids.superUser,
                                               DTORol.Ids.admin,
                                               DTORol.Ids.salesManager,
                                               DTORol.Ids.comercial,
                                               DTORol.Ids.manufacturer})
            Case AuthResults.success
                Dim oUser = ContextHelper.GetUser()
                Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
                Dim model = Await FEB2.InvRpts.Model(exs, oHolding, oUser)
                If exs.Count = 0 Then
                    retval = View(model)
                Else
                    retval = Await ErrorResult(exs)
                End If
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Async Function SellOut() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Select Case MyBase.Authorize({DTORol.Ids.superUser,
                                               DTORol.Ids.admin,
                                               DTORol.Ids.salesManager,
                                               DTORol.Ids.comercial,
                                               DTORol.Ids.manufacturer})
            Case AuthResults.success
                Dim oUser = ContextHelper.GetUser()
                Select Case oUser.Rol.id
                    Case DTORol.Ids.comercial, DTORol.Ids.rep
                        Dim oChannels = Await FEB2.DistributionChannels.All(oUser, exs)
                        Dim oGranDistribucio As DTODistributionChannel = DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.granDistribucio)
                        Dim allow As Boolean = oChannels.Exists(Function(x) x.Equals(oGranDistribucio))
                        If allow Then
                            retval = View()
                        Else
                            retval = MyBase.UnauthorizedView()
                        End If
                    Case Else
                        retval = View()
                End Select
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Async Function SellOutData(year As Integer) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim myData As Object

        Dim oUser = MyBase.User
        Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        Dim value = Await FEB2.EdiversaSalesReportItems.SellOutData(exs, year, oUser, oHolding)
        If value Is Nothing Then
            myData = New With {.success = False, .reason = ExceptionsHelper.ToFlatString(exs)}
        Else
            myData = New With {.success = True, .result = value}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function


    Async Function Excel(year As Integer, units As DTOStat2.Units, brand As Guid, category As Guid, sku As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = MyBase.User
        Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        Dim oSheet = Await FEB2.EdiversaSalesReportItems.Excel(exs, year, MyBase.User, oHolding, units, brand, category, sku)
        If exs.Count = 0 Then
            retval = MyBase.ExcelResult(oSheet)
        Else
            retval = Await ErrorResult(exs)
        End If

        Return retval
    End Function


End Class
