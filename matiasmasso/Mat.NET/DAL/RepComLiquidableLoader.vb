Public Class RepComLiquidableLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTORepComLiquidable
        Dim retval As DTORepComLiquidable = Nothing
        Dim oRepComLiquidable As New DTORepComLiquidable(oGuid)
        If Load(oRepComLiquidable) Then
            retval = oRepComLiquidable
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oRepComLiquidable As DTORepComLiquidable) As Boolean
        If Not oRepComLiquidable.IsLoaded And Not oRepComLiquidable.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Rps.*, CliRep.Abr as RepNickname, Fra.fra, Fra.Fch AS FraFch, Fra.CliGuid as FraCli, CliGral.FullNom ")
            sb.AppendLine(", RepLiq.Id AS RepLiqId, RepLiq.Fch AS RepLiqFch ")
            sb.AppendLine("FROM Rps ")
            sb.AppendLine("INNER JOIN CliRep ON Rps.RepGuid = CliRep.Guid ")
            sb.AppendLine("INNER JOIN Fra ON Rps.FraGuid = Fra.Guid ")
            sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN RepLiq ON Rps.RepLiqGuid = RepLiq.Guid ")
            sb.AppendLine("WHERE Rps.Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oRepComLiquidable.Guid.ToString())
            If oDrd.Read Then
                With oRepComLiquidable
                    .Fra = New DTOInvoice(oDrd("FraGuid"))
                    With .Fra
                        .Num = oDrd("Fra")
                        .Fch = oDrd("FraFch")
                        .Customer = New DTOCustomer(oDrd("FraCli"))
                        .Customer.FullNom = oDrd("FullNom")
                    End With

                    .Rep = New DTORep(oDrd("RepGuid"))
                    With .Rep
                        .NickName = oDrd("RepNickname")
                    End With

                    If Not IsDBNull(oDrd("RepLiqGuid")) Then
                        .RepLiq = New DTORepLiq(oDrd("RepLiqGuid"))
                        With .RepLiq
                            .Rep = oRepComLiquidable.Rep
                            .Id = oDrd("RepLiqId")
                            .Id = oDrd("RepLiqFch")
                        End With
                    End If

                    .Comisio = DTOAmt.Factory(CDec(oDrd("ComVal")))
                    .baseFras = DTOAmt.Factory(CDec(oDrd("Bas")))
                    .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                    .Liquidable = oDrd("Liquidable")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oRepComLiquidable.IsLoaded
        Return retval
    End Function

    Shared Function Update(oRepComLiquidable As DTORepComLiquidable, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oRepComLiquidable, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oRepComLiquidable As DTORepComLiquidable, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Rps ")
        sb.AppendLine("WHERE Rps.Guid='" & oRepComLiquidable.Guid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oRepComLiquidable.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oRepComLiquidable
            oRow("RepGuid") = .Rep.Guid
            oRow("FraGuid") = .Fra.Guid
            oRow("RepLiqGuid") = SQLHelper.NullableBaseGuid(.RepLiq)
            oRow("ComVal") = .Comisio.eur
            oRow("Bas") = .baseFras.eur
            oRow("Obs") = SQLHelper.NullableString(.Obs)
            oRow("Liquidable") = .Liquidable
        End With

        oDA.Update(oDs)

        If Not oRepComLiquidable.IsNew Then
            SQL = "UPDATE Arc SET RepComLiquidable=NULL WHERE RepComLiquidable='" & oRepComLiquidable.Guid.ToString & "' "
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End If

        If oRepComLiquidable.Items IsNot Nothing Then
            If oRepComLiquidable.Items.Count > 0 Then
                sb = New Text.StringBuilder
                sb.AppendLine("UPDATE Arc ")
                sb.AppendLine("SET RepComLiquidable='" & oRepComLiquidable.Guid.ToString & "' ")
                sb.AppendLine("WHERE (")
                For Each oItem As DTODeliveryItem In oRepComLiquidable.Items
                    If Not oItem.Equals(oRepComLiquidable.Items.First) Then sb.AppendLine("OR ")
                    sb.AppendLine("Guid='" & oItem.Guid.ToString & "' ")
                Next
                sb.AppendLine(")")
                SQL = sb.ToString
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
            End If
        End If

    End Sub

    Shared Function Delete(oRepComLiquidable As DTORepComLiquidable, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oRepComLiquidable, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Function Descarta(oRepComLiquidable As DTORepComLiquidable, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Descarta(oRepComLiquidable, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(oRepComLiquidable As DTORepComLiquidable, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Rps WHERE Guid='" & oRepComLiquidable.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Descarta(oRepComLiquidable As DTORepComLiquidable, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "UPDATE Rps SET Liquidable=" & IIf(oRepComLiquidable.Liquidable, 1, 0) & " WHERE Guid='" & oRepComLiquidable.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class RepComLiquidablesLoader
    Shared Function PendentsDeLiquidar() As List(Of DTORepComLiquidable)
        Dim retval As New List(Of DTORepComLiquidable)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid, Arc.RepGuid, CliRep.Abr ")
        sb.AppendLine(", Arc.Qty, Arc.Eur, Arc.Dto, Arc.Com ")
        sb.AppendLine(", Alb.Guid AS AlbGuid, Alb.FraGuid ")
        sb.AppendLine(", Fra.Fra, Fra.Fch, Fra.Vto, Fra.Cfp , Fra.Fpg ")
        sb.AppendLine(", Fra.CliGuid, CliGral.FullNom ")
        sb.AppendLine(", VwImpagatsOInsolvents.Cod AS PaymentStatus ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        sb.AppendLine("INNER JOIN CliRep ON Arc.RepGuid = CliRep.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Fra.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwImpagatsOInsolvents ON Fra.CliGuid = VwImpagatsOInsolvents.Customer ")

        '29/10/21: substitueix Arc.RepComLiquidable per check si Rps is null
        sb.AppendLine("LEFT OUTER JOIN Rps ON Fra.Guid = Rps.FraGuid ")
        sb.AppendLine("WHERE Rps.RepLiqGuid IS NULL ")
        'sb.AppendLine("WHERE Arc.RepComLiquidable IS NULL ")
        sb.AppendLine("AND CliRep.DisableLiqs = 0 ")
        sb.AppendLine("AND Arc.Qty*Arc.Eur <> 0 ")
        sb.AppendLine("AND Arc.Com <> 0 ")
        sb.AppendLine("AND (Arc.Bundle IS NULL OR Arc.Bundle <> Arc.Guid) ")
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
                    .Status = IIf(IsDBNull(oDrd("PaymentStatus")), DTORepComLiquidable.PaymentStatus.NoProblem, oDrd("PaymentStatus"))
                End With
                retval.Add(oRepComLiquidable)
            End If

            Dim oItem As New DTODeliveryItem(oDrd("Guid"))
            With oItem
                .Delivery = New DTODelivery(oDrd("AlbGuid"))
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = oDrd("Dto")
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


    Shared Function Sincronitza(exs As List(Of Exception)) As Boolean
        Dim retval As New Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Dim sb As New Text.StringBuilder
        Dim sbResult As New Text.StringBuilder
        Dim SQL As String = ""
        Dim iCount As Integer
        Try
            sb = New Text.StringBuilder

            sb.AppendLine("UPDATE Arc ")
            sb.AppendLine("SET Arc.RepGuid = Pnc.RepGuid, Arc.Com=Pnc.Com ")
            sb.AppendLine("FROM Pnc ")
            sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
            sb.AppendLine("INNER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
            sb.AppendLine("WHERE ( ")
            sb.AppendLine("(Arc.RepGuid IS NULL AND Pnc.RepGuid IS NOT NULL) ")
            sb.AppendLine("OR (Arc.RepGuid IS NOT NULL AND Pnc.RepGuid IS NULL) ")
            sb.AppendLine("OR (Arc.RepGuid<>Pnc.RepGuid) ")
            sb.AppendLine("OR (Arc.Com IS NULL AND Pnc.Com IS NOT NULL) ")
            sb.AppendLine("OR (Arc.Com IS NOT NULL AND Pnc.Com IS NULL) ")
            sb.AppendLine("OR (Arc.Com<>Pnc.Com) ")
            sb.AppendLine(") ")
            sb.AppendLine("AND Pdc.Fch > DATEADD(m,-12, GETDATE()) ")
            sb.AppendLine("AND Arc.RepComLiquidable IS NULL ")

            SQL = sb.ToString
            iCount = SQLHelper.ExecuteNonQuery(SQL, oTrans)
            If iCount = 0 Then
                sbResult.AppendLine("Totes les sortides es corresponien en representant i comisió amb les seves comandes")
            Else
                sbResult.AppendLine(String.Format("Actualitzades {0} sortides amb el representant i comisió de la comanda", iCount))
            End If

            'Retorna les sortides sense liquidació a pendents de liquidar
            sb = New Text.StringBuilder
            sb.AppendLine("UPDATE Arc SET Arc.RepComLiquidable=NULL ")
            sb.AppendLine("FROM Arc ")
            sb.AppendLine("LEFT OUTER JOIN Rps ON Arc.RepComLiquidable=Rps.Guid ")
            sb.AppendLine("WHERE  Rps.RepLiqGuid IS NULL ")
            sb.AppendLine("AND Arc.RepComLiquidable IS NOT NULL ")
            SQL = sb.ToString
            iCount = SQLHelper.ExecuteNonQuery(SQL, oTrans)
            If iCount = 0 Then
                sbResult.AppendLine("Totes les sortides sense liquidació estaven pendents de liquidar")
            Else
                sbResult.AppendLine(String.Format("Retornades {0} sortides sense liquidació a pendents de liquidar", iCount))
            End If

            'esborra les partides liquidades que no tenen liquidació
            sb = New Text.StringBuilder
            sb.AppendLine("DELETE Rps WHERE Rps.RepliqGuid IS NULL ")
            SQL = sb.ToString
            iCount = SQLHelper.ExecuteNonQuery(SQL, oTrans)
            If iCount = 0 Then
                sbResult.AppendLine("No s'ha trobat cap partida sense liquidació")
            Else
                sbResult.AppendLine(String.Format("Eliminades {0} partides que no tenien liquidació", iCount))
            End If

            'marca com a liquidades les sortides facturades i liquidades
            sb = New Text.StringBuilder
            sb.AppendLine("UPDATE Arc SET Arc.RepComLiquidable = Rps.Guid ")
            sb.AppendLine("FROM Arc ")
            sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
            sb.AppendLine("INNER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
            sb.AppendLine("INNER JOIN Rps ON Fra.Guid = Rps.FraGuid AND Rps.RepGuid = Arc.RepGuid ")
            sb.AppendLine("WHERE Arc.RepComLiquidable IS NULL ")
            sb.AppendLine("AND YEAR(Alb.Fch)>2016 ")
            SQL = sb.ToString
            iCount = SQLHelper.ExecuteNonQuery(SQL, oTrans)
            If iCount = 0 Then
                sbResult.AppendLine("Totes les sortides liquidades estaven marcades correctament")
            Else
                sbResult.AppendLine(String.Format("Marcades {0} sortides liquidades que no constaven així", iCount))
            End If

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function
End Class