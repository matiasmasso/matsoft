Public Class PaymentGateway
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPaymentGateway)
        Return Await Api.Fetch(Of DTOPaymentGateway)(exs, "PaymentGateway", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oPaymentGateway As DTOPaymentGateway, exs As List(Of Exception)) As Boolean
        If Not oPaymentGateway.IsLoaded And Not oPaymentGateway.IsNew Then
            Dim pPaymentGateway = Api.FetchSync(Of DTOPaymentGateway)(exs, "PaymentGateway", oPaymentGateway.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPaymentGateway)(pPaymentGateway, oPaymentGateway, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oPaymentGateway As DTOPaymentGateway, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPaymentGateway)(oPaymentGateway, exs, "PaymentGateway")
        oPaymentGateway.IsNew = False
    End Function


    Shared Async Function Delete(oPaymentGateway As DTOPaymentGateway, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPaymentGateway)(oPaymentGateway, exs, "PaymentGateway")
    End Function

    Shared Async Function ProductionEnvironment(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of DTOPaymentGateway)
        Dim retval As DTOPaymentGateway = Nothing
        Dim sGuid As String = FEB2.Default.EmpValueSync(oEmp, DTODefault.Codis.SermepaTpvProductionEnvironment, exs)
        If GuidHelper.IsGuid(sGuid) Then
            Dim oGuid As New Guid(sGuid)
            retval = Await FEB2.PaymentGateway.Find(oGuid, exs)
        End If
        Return retval
    End Function

    Shared Async Function TestingEnvironment(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of DTOPaymentGateway)
        Dim retval As DTOPaymentGateway = Nothing
        Dim sGuid As String = FEB2.Default.EmpValueSync(oEmp, DTODefault.Codis.SermepaTpvTestingEnvironment, exs)
        If GuidHelper.IsGuid(sGuid) Then
            Dim oGuid As New Guid(sGuid)
            retval = Await FEB2.PaymentGateway.Find(oGuid, exs)
        End If
        Return retval
    End Function

End Class

Public Class PaymentGateways
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOPaymentGateway))
        Return Await Api.Fetch(Of List(Of DTOPaymentGateway))(exs, "PaymentGateways")
    End Function

End Class
