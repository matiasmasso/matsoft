Public Class CustomerTarifaDto
    Shared Function Find(oGuid As Guid) As DTOCustomerTarifaDto
        Dim retval As DTOCustomerTarifaDto = CustomerTarifaDtoLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oCustomerDto As DTOCustomerTarifaDto) As Boolean
        Dim retval As Boolean = CustomerTarifaDtoLoader.Load(oCustomerDto)
        Return retval
    End Function

    Shared Function Update(oCustomerDto As DTOCustomerTarifaDto, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerTarifaDtoLoader.Update(oCustomerDto, exs)
        Return retval
    End Function

    Shared Function Delete(oCustomerDto As DTOCustomerTarifaDto, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerTarifaDtoLoader.Delete(oCustomerDto, exs)
        Return retval
    End Function

End Class

Public Class CustomerTarifaDtos



    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOCustomerTarifaDto)
        Dim oChannel As DTODistributionChannel = BEBL.Customer.DistributionChannel(oCustomer)
        Dim oChannelDtos As List(Of DTOCustomerTarifaDto) = All(oChannel)
        Dim retval As List(Of DTOCustomerTarifaDto) = CustomerTarifaDtosLoader.All(oCustomer)
        For Each oChannelDto In oChannelDtos
            If retval.Any(Function(x) x.Product IsNot Nothing AndAlso x.Product.Equals(oChannelDto.Product)) Then
            Else
                retval.Add(oChannelDto)
            End If
        Next
        Return retval
    End Function

    Shared Function All(oChannel As DTODistributionChannel) As List(Of DTOCustomerTarifaDto)
        Dim retval As List(Of DTOCustomerTarifaDto) = CustomerTarifaDtosLoader.All(oChannel)
        Return retval
    End Function

    Shared Function Active(oCustomer As DTOCustomer, Optional DtFch As Date = Nothing) As List(Of DTOCustomerTarifaDto)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Now()
        Dim oDtos As List(Of DTOCustomerTarifaDto) = All(oCustomer) ' CustomerTarifaDtosLoader.All(oCustomer)
        Dim retval As New List(Of DTOCustomerTarifaDto)
        For Each item As DTOCustomerTarifaDto In oDtos
            If item.Fch.Date <= DtFch Then
                If Not retval.Exists(Function(x) x.Product.Equals(item.Product)) Then
                    retval.Add(item)
                    If item.Product Is Nothing Then Exit For
                End If
            End If
        Next
        Return retval
    End Function

    Shared Function Active(oChannel As DTODistributionChannel, Optional DtFch As Date = Nothing) As List(Of DTOCustomerTarifaDto)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Now()
        Dim oDtos As List(Of DTOCustomerTarifaDto) = CustomerTarifaDtosLoader.All(oChannel)
        Dim retval As New List(Of DTOCustomerTarifaDto)
        For Each item As DTOCustomerTarifaDto In oDtos
            If item.Fch.Date <= DtFch.Date Then
                If Not retval.Exists(Function(x) x.Product.Equals(item)) Then
                    retval.Add(item)
                    If item.Product Is Nothing Then Exit For
                End If
            End If
        Next
        Return retval
    End Function

End Class
