Public Class PaymentGateway
    Shared Function Find(oGuid As Guid) As DTOPaymentGateway
        Dim retval As DTOPaymentGateway = PaymentGatewayLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPaymentGateway As DTOPaymentGateway) As Boolean
        Dim retval As Boolean = PaymentGatewayLoader.Load(oPaymentGateway)
        Return retval
    End Function

    Shared Function Update(oPaymentGateway As DTOPaymentGateway, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PaymentGatewayLoader.Update(oPaymentGateway, exs)
        Return retval
    End Function

    Shared Function Delete(oPaymentGateway As DTOPaymentGateway, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PaymentGatewayLoader.Delete(oPaymentGateway, exs)
        Return retval
    End Function

End Class

Public Class PaymentGateways

    Shared Function All() As List(Of DTOPaymentGateway)
        Dim retval As List(Of DTOPaymentGateway) = PaymentGatewaysLoader.All()
        Return retval
    End Function

End Class
