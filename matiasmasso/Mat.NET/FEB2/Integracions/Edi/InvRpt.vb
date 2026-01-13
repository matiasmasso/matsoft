Public Class InvRpt
    Inherits _FeblBase


    Shared Async Function Delete(exs As List(Of Exception), value As DTO.Integracions.Edi.Invrpt) As Task(Of Boolean)
        Return Await Api.Delete(Of DTO.Integracions.Edi.Invrpt)(value, exs, "invrpt")
    End Function

    Shared Async Function Raport(exs As List(Of Exception), oCustomerGuid As Guid, oSkuGuid As Guid, DtFch As DateTime) As Task(Of String)
        Dim retval = Await Api.Fetch(Of String)(exs, "invrpt/raport", oCustomerGuid.ToString, oSkuGuid.ToString, FormatFchTime(DtFch))
        Return retval
    End Function
End Class

Public Class InvRpts
    Inherits _FeblBase

    Shared Async Function Model(exs As List(Of Exception), oHolding As DTOHolding, oUser As DTOUser, Optional fch As Date = Nothing) As Task(Of DTO.Models.InvrptModel)
        If fch = Nothing Then
            Return Await Api.Fetch(Of DTO.Models.InvrptModel)(exs, "invrpts", oHolding.Guid.ToString, oUser.Guid.ToString())
        Else
            Return Await Api.Fetch(Of DTO.Models.InvrptModel)(exs, "invrpts", oHolding.Guid.ToString, oUser.Guid.ToString(), FormatFch(fch))
        End If
    End Function

    Shared Async Function Exceptions(exs As List(Of Exception)) As Task(Of List(Of DTO.Integracions.Edi.Exception))
        Return Await Api.Fetch(Of List(Of DTO.Integracions.Edi.Exception))(exs, "invrpts/exceptions")
    End Function

End Class
