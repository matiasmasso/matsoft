Public Class DeliveryTraspas

    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTODeliveryTraspas)
        Return Await Api.Fetch(Of DTODeliveryTraspas)(exs, "DeliveryTraspas", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oDeliveryTraspas As DTODeliveryTraspas, exs As List(Of Exception)) As Boolean
        If Not oDeliveryTraspas.IsLoaded And Not oDeliveryTraspas.IsNew Then
            Dim pDeliveryTraspas = Api.FetchSync(Of DTODeliveryTraspas)(exs, "DeliveryTraspas", oDeliveryTraspas.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTODeliveryTraspas)(pDeliveryTraspas, oDeliveryTraspas, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oDeliveryTraspas As DTODeliveryTraspas, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTODeliveryTraspas)(oDeliveryTraspas, exs, "DeliveryTraspas")
        oDeliveryTraspas.IsNew = False
    End Function

    Shared Async Function Delete(oDeliveryTraspas As DTODeliveryTraspas, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTODeliveryTraspas)(oDeliveryTraspas, exs, "DeliveryTraspas")
    End Function

    Shared Function Excel(oDeliveryTraspas As DTODeliveryTraspas, exs As List(Of Exception)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("Traspas de magatzem " & oDeliveryTraspas.Id)
        If Load(oDeliveryTraspas, exs) Then
            For Each item In oDeliveryTraspas.Items
                Dim oRow = retval.AddRow()
                oRow.AddCell(item.Sku.Id)
                oRow.AddCell(item.Sku.Category.Brand.Nom.Esp)
                oRow.AddCell(item.Sku.Category.Nom.Esp)
                oRow.AddCell(item.Sku.Nom.Esp)
                oRow.AddCell(item.Qty)
            Next
        End If
        Return retval
    End Function
End Class

Public Class DeliveryTraspassos
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTODeliveryTraspas))
        Return Await Api.Fetch(Of List(Of DTODeliveryTraspas))(exs, "DeliveryTraspassos", oEmp.Id)
    End Function

End Class

