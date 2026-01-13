Public Class EdiversaOrder

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOEdiversaOrder)
        Dim retval = Await Api.Fetch(Of DTOEdiversaOrder)(exs, "EdiversaOrder", oGuid.ToString())
        retval.RestoreTagsToOriginalObjects()
        Return retval
    End Function

    Shared Async Function LoadFromSrc(exs As List(Of Exception), stream As String) As Task(Of DTOEdiversaOrder)
        Dim retval = Await Api.Execute(Of String, DTOEdiversaOrder)(stream, exs, "EdiversaOrder/LoadFromSrc")
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Boolean
        If Not oEdiversaOrder.IsLoaded And Not oEdiversaOrder.IsNew Then
            Dim pEdiversaOrder = Api.FetchSync(Of DTOEdiversaOrder)(exs, "EdiversaOrder", oEdiversaOrder.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEdiversaOrder)(pEdiversaOrder, oEdiversaOrder, exs)
                oEdiversaOrder.RestoreTagsToOriginalObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(oOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oTrimmedOrder = oOrder.Trimmed
        Return Await Api.Update(Of DTOEdiversaOrder)(oOrder, exs, "ediversaorder")
    End Function

    Shared Async Function Delete(oOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiversaOrder)(oOrder, exs, "ediversaorder/delete")
    End Function

    Shared Async Function SetResult(exs As List(Of Exception), oEdiOrder As DTOEdiversaOrder, oOrder As DTOPurchaseOrder) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "ediversaorder/setResult", oEdiOrder.Guid.ToString, oOrder.Guid.ToString)
    End Function

    Shared Async Function Validate(oEdiversaOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOEdiversaOrder)(oEdiversaOrder, exs, "EdiversaOrder/validate")
    End Function

    Shared Function EdiMessage(oEmp As DTOEmp, oOrder As DTOPurchaseOrder, exs As List(Of Exception)) As String
        Dim retval As String = Ediversa_ORDERS_D_96A_UN_EAN008.Factory(oEmp, oOrder, exs)
        Return retval
    End Function

    Shared Function EdiFile(oEmp As DTOEmp, oOrder As DTOPurchaseOrder, exs As List(Of Exception)) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = Nothing
        If oOrder Is Nothing Then
            exs.Add(New Exception("comanda buida"))
        Else
            PurchaseOrder.Load(oOrder, exs)
            Dim oSender As New DTOEdiversaContact
            With oSender
                .Contact = oEmp.Org
                .Ean = .Contact.GLN
            End With

            Dim oReceiver As New DTOEdiversaContact
            With oReceiver
                .Contact = oOrder.Proveidor
                .Ean = .Contact.GLN
            End With

            retval = New DTOEdiversaFile
            With retval
                .Tag = DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString
                .Fch = oOrder.Fch
                .Sender = oSender
                .Receiver = oReceiver
                .docnum = If(oOrder.cod = DTOPurchaseOrder.Codis.proveidor, oOrder.num, oOrder.concept)
                .Amount = oOrder.SumaDeImportes()
                .Result = DTOEdiversaFile.Results.Pending
                .ResultBaseGuid = oOrder
                .Stream = EdiversaOrder.EdiMessage(oEmp, oOrder, exs)
                .IOCod = DTOEdiversaFile.IOcods.Outbox
            End With
        End If
        Return retval

    End Function

End Class
Public Class EdiversaOrders


    Shared Async Function Headers(exs As List(Of Exception), oEmp As DTOEmp, year As Integer) As Task(Of List(Of DTOEdiversaOrder))
        Dim retval = Await Api.Fetch(Of List(Of DTOEdiversaOrder))(exs, "ediversaorders/headers", oEmp.Id, year)
        If exs.Count = 0 Then
            For Each oFile In retval
                oFile.RestoreTagsToOriginalObjects()
            Next
        End If
        Return retval
    End Function

    Shared Async Function OpenFiles(exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaOrder))
        Dim retval = Await Api.Fetch(Of List(Of DTOEdiversaOrder))(exs, "ediversaorders/openfiles")
        If exs.Count = 0 Then
            For Each oFile In retval
                oFile.RestoreTagsToOriginalObjects()
            Next
        End If
        Return retval
    End Function

    Shared Async Function Validate(oOrders As List(Of DTOEdiversaOrder), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOEdiversaOrder))(oOrders, exs, "ediversaorders/validate")
    End Function

    Shared Async Function searchByDocNum(exs As List(Of Exception), docnum As String) As Task(Of List(Of DTOEdiversaOrder))
        Dim retval = Await Api.Execute(Of String, List(Of DTOEdiversaOrder))(docnum, exs, "ediversaorder/searchByDocNum")
        Return retval
    End Function

    Shared Async Function ProcessAllValidated(oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "EdiversaOrders/ProcessAllValidated", oUser.Guid.ToString())
    End Function

    Shared Async Function Procesa(oUser As DTOUser, values As List(Of DTOEdiversaOrder), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOEdiversaOrder))(values, exs, "EdiversaOrders/Procesa", oUser.Guid.ToString())
    End Function

    Shared Async Function Descarta(exs As List(Of Exception), values As List(Of DTOEdiversaOrder)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOEdiversaOrder))(values, exs, "EdiversaOrders/Descarta")
    End Function

    Shared Async Function ConfirmationPending(exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaOrder))
        Return Await Api.Fetch(Of List(Of DTOEdiversaOrder))(exs, "EdiversaOrders/ConfirmationPending")
    End Function


    Shared Function Csv(oEdiversaOrders As List(Of DTOEdiversaOrder)) As DTOCsv
        Dim retval As New DTOCsv("Comandes.csv")
        For Each item As DTOEdiversaOrder In oEdiversaOrders
            Dim oRow As DTOCsvRow = retval.AddRow()
            oRow.AddCell(item.DocNum)
            oRow.AddCell(item.FchDoc)
            oRow.AddCell(item.Departamento)
            oRow.AddCell(item.Centro)
            If item.Comprador Is Nothing Then
                oRow.AddCell("")
            Else
                oRow.AddCell(item.Comprador.FullNom)
            End If
        Next
        Return retval
    End Function
End Class
