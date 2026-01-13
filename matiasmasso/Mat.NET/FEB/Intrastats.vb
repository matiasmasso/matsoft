Public Class Intrastat

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOIntrastat)
        Return Await Api.Fetch(Of DTOIntrastat)(exs, "Intrastat", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oIntrastat As DTOIntrastat, exs As List(Of Exception)) As Boolean
        If Not oIntrastat.IsLoaded And Not oIntrastat.IsNew Then
            Dim pIntrastat = Api.FetchSync(Of DTOIntrastat)(exs, "Intrastat", oIntrastat.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOIntrastat)(pIntrastat, oIntrastat, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOIntrastat, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized As String = Api.Serialize(value.Trimmed, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Intrastat")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oIntrastat As DTOIntrastat, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOIntrastat)(oIntrastat, exs, "Intrastat")
    End Function



    Shared Async Function Factory(oEmp As DTOEmp, oFlujo As DTOIntrastat.Flujos, oYearMonth As DTOYearMonth, exs As List(Of Exception)) As Task(Of DTOIntrastat)
        Dim retval = Await Api.Fetch(Of DTOIntrastat)(exs, "Intrastat/factory", oEmp.Id, oFlujo, oYearMonth.Year, oYearMonth.Month)
        Select Case oFlujo
            Case DTOIntrastat.Flujos.introduccion
                Dim oDeliveries = Await Deliveries.IntrastatPending(oEmp, oYearMonth, exs)
                For Each oDelivery As DTODelivery In oDeliveries
                    retval.Partidas.AddRange(DTOIntrastat.Partida.Factory(oEmp, oDelivery))
                Next
            Case DTOIntrastat.Flujos.expedicion
                Dim oInvoices = Await Invoices.IntrastatPending(oEmp, oYearMonth, exs)
                If oInvoices IsNot Nothing Then
                    For Each oInvoice As DTOInvoice In oInvoices
                        retval.Partidas.AddRange(DTOIntrastat.Partida.Factory(oEmp, oInvoice))
                    Next
                End If
        End Select
        Return retval
    End Function

End Class

Public Class Intrastats

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOIntrastat))
        Return Await Api.Fetch(Of List(Of DTOIntrastat))(exs, "Intrastats", oEmp.Id)
    End Function

End Class