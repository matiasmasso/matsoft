Public Class DTOEdiversaOrdrsp
    Inherits DTOBaseGuid

    Property Order As DTOEdiversaOrder
    Property Fch As Date
    Property FchCreated As DateTime
    Property Items As List(Of DTOEdiversaOrdrspItem)


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oEdiversaOrder As DTOEdiversaOrder) As DTOEdiversaOrdrsp

        Dim retval As New DTOEdiversaOrdrsp
        With retval
            .Order = oEdiversaOrder
            .Fch = DateTime.Today
            .Items = New List(Of DTOEdiversaOrdrspItem)
        End With

        For Each orderItem As DTOEdiversaOrderItem In oEdiversaOrder.Items
            Dim spItem As New DTOEdiversaOrdrspItem
            With spItem
                .OrderItem = orderItem
                .Qty = orderItem.Qty
            End With
            retval.Items.Add(spItem)
        Next
        Return retval
    End Function

    Shared Function EdiFile(src As DTOEdiversaOrdrsp) As DTOEdiversaFile
        Dim sb As New Text.StringBuilder
        sb.AppendLine(String.Format("BGM|{0}|231|{1}", src.Order.DocNum, HasChangedCode(src)))
        sb.AppendLine(String.Format("DTM|{0:yyyyMMdd}", src.Fch))
        sb.AppendLine("NADSU")
        sb.AppendLine("NADBY")
        sb.AppendLine("NADDP")
        sb.AppendLine("NADIV")
        sb.AppendLine("CUX")
        For Each item As DTOEdiversaOrdrspItem In src.Items
            sb.AppendLine("LIN")
            sb.AppendLine("PIALIN")
            sb.AppendLine("QTYLIN")
            sb.AppendLine("DMTLIN")
            sb.AppendLine("PRILIN")
            sb.AppendLine("TAXLIN")
        Next
        sb.AppendLine("CNTRES")
        Dim retval As New DTOEdiversaFile
        retval.Stream = sb.ToString()
        Return retval
    End Function

    Shared Function HasChanged(src As DTOEdiversaOrdrsp) As Boolean
        Dim retval As Boolean = False
        For Each item As DTOEdiversaOrdrspItem In src.Items
            If item.Qty <> item.OrderItem.Qty Then
                retval = True
                Exit For
            End If
        Next
        Return retval
    End Function

    Shared Function HasChangedCode(src As DTOEdiversaOrdrsp) As Integer
        Dim retval As Integer = 29 'aceptado sin correccion
        If HasChanged(src) Then retval = 4 'cambio
        Return retval
    End Function
End Class


Public Class DTOEdiversaOrdrspItem
    Property OrderItem As DTOEdiversaOrderItem
    Property Qty As Integer

    Public Sub New()
        MyBase.New()
    End Sub

End Class


