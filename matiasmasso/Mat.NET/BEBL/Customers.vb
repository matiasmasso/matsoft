Public Class Customer
    Shared Function Find(oGuid As Guid) As DTOCustomer
        Dim retval As DTOCustomer = CustomerLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oCustomer As DTOCustomer) As Boolean
        Dim retval As Boolean = CustomerLoader.Load(oCustomer)
        Return retval
    End Function

    Shared Function Update(oCustomer As DTOCustomer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerLoader.Update(oCustomer, exs)
        Return retval
    End Function

    Shared Function Delete(oCustomer As DTOCustomer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerLoader.Delete(oCustomer, exs)
        Return retval
    End Function

    Shared Function CcxOrMe(oCustomer As DTOCustomer) As DTOCustomer
        BEBL.Customer.Load(oCustomer)
        Dim retval As DTOCustomer = oCustomer
        If oCustomer.Ccx IsNot Nothing Then
            retval = oCustomer.Ccx
            CustomerLoader.Load(retval)
        End If
        Return retval
    End Function

    Shared Function DistributionChannel(oCustomer As DTOCustomer) As DTODistributionChannel
        Return CustomerLoader.DistributionChannel(oCustomer)
    End Function

    Shared Function SkuPrice(oCustomer As DTOCustomer, oSku As DTOProductSku, Optional DtFch As Date = Nothing) As DTOAmt
        BEBL.Customer.Load(oCustomer)
        BEBL.ProductSku.Load(oSku)
        Dim oCcx As DTOCustomer = BEBL.Customer.CcxOrMe(oCustomer)
        Dim oTarifaDtos As List(Of DTOCustomerTarifaDto) = BEBL.CustomerTarifaDtos.Active(oCcx)
        Dim retval As DTOAmt = BEBL.PriceListItemCustomer.GetCustomerCost(oSku, oTarifaDtos, DtFch)
        Return retval
    End Function

    Shared Function ShippingAddressOrDefault(oCustomer As DTOCustomer) As DTOAddress
        Dim exs As New List(Of Exception)
        Dim oAddresses As List(Of DTOAddress) = BEBL.Addresses.All(oCustomer)
        Dim retval As DTOAddress = oAddresses.Find(Function(x) x.Codi = DTOAddress.Codis.Entregas)
        If retval Is Nothing Then
            retval = oAddresses.Find(Function(x) x.Codi = DTOAddress.Codis.Fiscal)
        End If
        Return retval
    End Function

    Shared Function IsImpagat(oCustomer As DTOCustomer) As Boolean
        Dim DcSaldo As Decimal = BEBL.PgcSaldo.FromCtaCod(DTOPgcPlan.Ctas.Impagats, oCustomer, DTO.GlobalVariables.Today())
        Return DcSaldo > 0
    End Function

    Shared Function Exists(oContact As DTOContact) As Boolean
        Dim retval As Boolean = CustomerLoader.Exists(oContact)
        Return retval
    End Function

    Shared Function IsGroup(oContact As DTOContact) As Boolean
        Return CustomerLoader.IsGroup(oContact)
    End Function

    Shared Function EFrasEnabled(oContact As DTOContact) As Boolean
        Dim retval As Boolean = CustomerLoader.EFrasEnabled(oContact)
        Return retval
    End Function



    Shared Function EFrasEnabled(oCustomer As DTOCustomer) As Boolean
        Dim retval As Boolean = CustomerLoader.EFrasEnabled(oCustomer)
        Return retval
    End Function


    Shared Function Children(ccx As DTOCustomer) As List(Of DTOCustomer)
        Dim retval As List(Of DTOCustomer) = CustomersLoader.Children(ccx)
        Return retval
    End Function
End Class
Public Class Customers
    Shared Function FromUser(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As List(Of DTOCustomer) = CustomersLoader.FromUser(oUser)
        Return retval
    End Function

    Shared Function RaonsSocialsWithInvoices(oUser As DTOUser) As List(Of DTOCustomer)
        Dim retval As List(Of DTOCustomer) = CustomersLoader.RaonsSocialsWithInvoices(oUser)
        Return retval
    End Function
End Class
