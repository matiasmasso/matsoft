
Public Class RepcomsLoader

    Shared Function PendentsDeLiquidar() As List(Of DTORepComLiquidable)
        Dim retval As New List(Of DTORepComLiquidable)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid, Arc.RepGuid, CliRep.Abr ")
        sb.AppendLine(", Arc.Qty, Arc.Eur, Arc.Dto, Arc.Com ")
        sb.AppendLine(", Alb.Guid AS AlbGuid, Alb.FraGuid ")
        sb.AppendLine(", Fra.Fra, Fra.Fch, Fra.Vto, Fra.Cfp , Fra.Fpg ")
        sb.AppendLine(", Fra.CliGuid, CliGral.FullNom ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("INNER JOIN CliRep ON Arc.RepGuid = CliRep.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Arc.RepComLiquidable IS NULL ")
        sb.AppendLine("AND Arc.Qty*Arc.Eur <> 0 ")
        sb.AppendLine("AND Arc.Com <> 0 ")
        sb.AppendLine("AND Alb.Fch > DATEADD(m,-12,GETDATE()) ")
        sb.AppendLine("ORDER BY CliRep.Abr, Arc.RepGuid, Fra.Fch, Fra.Fra ")

        Dim SQL As String = sb.ToString
        Dim oCustomers As New List(Of DTOCustomer)
        Dim oReps As New List(Of DTORep)
        Dim oRepComLiquidable As New DTORepComLiquidable
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As DTORep = oReps.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("RepGuid")))
            If oRep Is Nothing Then
                oRep = New DTORep(oDrd("RepGuid"))
                oRep.NickName = SQLHelper.GetStringFromDataReader(oDrd("Abr"))
                oReps.Add(oRep)
            End If

            Dim oCustomer As DTOCustomer = oCustomers.FirstOrDefault(Function(x) x.Guid.Equals(oDrd("CliGuid")))
            If oCustomer Is Nothing Then
                oCustomer = New DTOCustomer(oDrd("CliGuid"))
                oCustomer.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                oCustomers.Add(oCustomer)
            End If

            Dim oInvoice As New DTOInvoice(oDrd("FraGuid"))
            With oInvoice
                .Num = oDrd("Fra")
                .Fch = oDrd("Fch")
                .Vto = oDrd("Vto")
                .Cfp = oDrd("Cfp")
                .Fpg = oDrd("Fpg")
                .Customer = oCustomer
            End With

            If Not (oInvoice.Equals(oRepComLiquidable.Fra) And oRep.Equals(oRepComLiquidable.Rep)) Then
                oRepComLiquidable = New DTORepComLiquidable()
                With oRepComLiquidable
                    .Fra = oInvoice
                    .Rep = oRep
                End With
                retval.Add(oRepComLiquidable)
            End If

            Dim oItem As New DTODeliveryItem(oDrd("Guid"))
            With oItem
                .Delivery = New DTODelivery(oDrd("AlbGuid"))
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .RepCom = New DTORepCom(oRep, oDrd("Com"))
            End With

            With oRepComLiquidable
                .Items.Add(oItem)
                .baseFras = DTOAmt.Factory(.items.Where(Function(x) x.import IsNot Nothing).Sum(Function(y) y.import.Eur))
                .Comisio = DTOAmt.Factory(.Items.Where(Function(x) x.Import IsNot Nothing And x.RepCom IsNot Nothing).Sum(Function(y) y.Import.Eur * y.RepCom.Com / 100))
            End With


        Loop
        oDrd.Close()

        Return retval

    End Function
End Class
