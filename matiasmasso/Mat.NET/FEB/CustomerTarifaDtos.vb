Public Class CustomerTarifaDto
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCustomerTarifaDto)
        Return Await Api.Fetch(Of DTOCustomerTarifaDto)(exs, "CustomerTarifaDto", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCustomerTarifaDto As DTOCustomerTarifaDto, exs As List(Of Exception)) As Boolean
        If Not oCustomerTarifaDto.IsLoaded And Not oCustomerTarifaDto.IsNew Then
            Dim pCustomerTarifaDto = Api.FetchSync(Of DTOCustomerTarifaDto)(exs, "CustomerTarifaDto", oCustomerTarifaDto.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCustomerTarifaDto)(pCustomerTarifaDto, oCustomerTarifaDto, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCustomerTarifaDto As DTOCustomerTarifaDto, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCustomerTarifaDto)(oCustomerTarifaDto, exs, "CustomerTarifaDto")
        oCustomerTarifaDto.IsNew = False
    End Function

    Shared Async Function Delete(oCustomerTarifaDto As DTOCustomerTarifaDto, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCustomerTarifaDto)(oCustomerTarifaDto, exs, "CustomerTarifaDto")
    End Function
End Class

Public Class CustomerTarifaDtos
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOCustomerTarifaDto))
        Return Await Api.Fetch(Of List(Of DTOCustomerTarifaDto))(exs, "CustomerTarifaDtos/FromCustomer", oCustomer.Guid.ToString())
    End Function
    Shared Async Function All(exs As List(Of Exception), oChannel As DTODistributionChannel) As Task(Of List(Of DTOCustomerTarifaDto))
        Return Await Api.Fetch(Of List(Of DTOCustomerTarifaDto))(exs, "CustomerTarifaDtos/FromChannel", oChannel.Guid.ToString())
    End Function
    Shared Async Function Active(exs As List(Of Exception), oCustomer As DTOCustomer, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOCustomerTarifaDto))
        Dim retval = Await Api.Fetch(Of List(Of DTOCustomerTarifaDto))(exs, "CustomerTarifaDtos/ActiveFromCustomer", oCustomer.Guid.ToString, FormatFch(DtFch))
        Return retval
    End Function
    Shared Async Function Active(exs As List(Of Exception), oChannel As DTODistributionChannel, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOCustomerTarifaDto))
        Return Await Api.Fetch(Of List(Of DTOCustomerTarifaDto))(exs, "CustomerTarifaDtos/ActiveFromChannel", oChannel.Guid.ToString, FormatFch(DtFch))
    End Function

End Class
