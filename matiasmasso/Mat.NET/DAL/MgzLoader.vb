Public Class MgzLoader


#Region "CRUD"
    Shared Function FromNum(oEmp As DTOEmp, iNum As Integer) As DTOMgz
        Dim retval As DTOMgz = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, Mgz.Nom as MgzAbr, CliGral.RaoSocial ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("LEFT OUTER JOIN Mgz ON CliGral.Guid = Mgz.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & oEmp.Id & " AND CliGral.Cli=" & iNum & " ")
        Dim SQL As String = sb.ToString
        Dim odrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If odrd.Read Then
            retval = New DTOMgz(odrd("Guid"))
            With retval
                .Emp = oEmp
                .Id = iNum
                If IsDBNull(odrd("MgzAbr")) Then
                    .Abr = odrd("RaoSocial")
                Else
                    .Abr = odrd("MgzAbr")
                End If
            End With
        End If
        odrd.Close()
        Return retval
    End Function

    Shared Function Find(oGuid As Guid) As DTOMgz
        Dim retval As DTOMgz = Nothing
        Dim oMgz As New DTOMgz(oGuid)
        If Load(oMgz) Then
            retval = oMgz
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oMgz As DTOMgz) As Boolean
        If Not oMgz.IsLoaded And Not oMgz.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT CliGral.Emp, CliGral.Cli, Mgz.Nom as MgzAbr, CliGral.RaoSocial ")
            sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
            sb.AppendLine("FROM CliGral ")
            sb.AppendLine("LEFT OUTER JOIN Mgz ON CliGral.Guid = Mgz.Guid ")
            sb.AppendLine("WHERE CliGral.Guid='" & oMgz.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oMgz
                    .Emp = New DTOEmp(oDrd("Emp"))
                    .Id = oDrd("Cli")
                    .Nom = oDrd("RaoSocial")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    If IsDBNull(oDrd("MgzAbr")) Then
                        .Abr = oDrd("RaoSocial")
                    Else
                        .Abr = oDrd("MgzAbr")
                    End If
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oMgz.IsLoaded
        Return retval
    End Function

    Shared Function Update(oMgz As DTOMgz, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oMgz, oTrans)
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


    Shared Sub Update(oMgz As DTOMgz, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Mgz ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oMgz.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oMgz.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oMgz
            oRow("Nom") = .Nom
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oMgz As DTOMgz, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oMgz, oTrans)
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


    Shared Sub Delete(oMgz As DTOMgz, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Mgz WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oMgz.Guid.ToString())
    End Sub

#End Region

    Shared Function Stocks(oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT SkuGuid, Stock, Clients, ClientsAlPot, ClientsEnProgramacio, ClientsBlockStock, Pn1 ")
        sb.AppendLine("FROM (")
        sb.AppendLine("SELECT SkuGuid, Stock, 0 AS Clients, 0 AS ClientsAlPot, 0 AS ClientsEnProgramacio, 0 AS ClientsBlockStock, 0 AS Pn1 ")
        sb.AppendLine("FROM VwSkuStocks ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT SkuGuid, 0 AS Stock, Clients, ClientsAlPot, ClientsEnProgramacio, ClientsBlockStock, Pn1 ")
        sb.AppendLine("FROM VwSkuPncs) AS X ")
        sb.AppendLine("GROUP BY SkuGuid, Stock, Clients, ClientsAlPot, ClientsEnProgramacio, ClientsBlockStock, Pn1 ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As New DTOProductSku(oDrd("SkuGuid"))
            With oSku
                .Stock = oDrd("Stock")
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function RawStocks(oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuStocks.SkuGuid, VwSkuStocks.Stock, Art.Pmc, VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuStocks ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwSkuStocks.SkuGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Art  ON  VwSkuStocks.SkuGuid = Art.Guid ")
        sb.AppendLine("WHERE VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("AND VwSkuStocks.Stock > 0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.BrandGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom, VwSkuNom.SkuGuid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                .Stock = oDrd("Stock")
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval

    End Function
    Shared Function Inventory(oMgz As DTOMgz, DtFch As Date) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sFch As String = Format(DtFch, "yyyyMMdd")
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT X.Stk, Y.LastPmc, X.LastIn, VwSkuNom.* ")
        sb.AppendLine("FROM ( ")
        sb.AppendLine("		SELECT Arc.MgzGuid, Arc.ArtGuid ")
        sb.AppendLine("		, SUM(CASE WHEN Arc.Cod < 50 THEN Arc.Qty ELSE - Arc.Qty END) AS Stk ")
        sb.AppendLine("		, MAX(CASE WHEN (Arc.Cod < 50 OR Arc.Qty<0) AND Arc.Eur > 0 THEN Alb.Fch ELSE NULL END) AS LastIn ")
        sb.AppendLine("		, MAX(CASE WHEN (Arc.Cod < 50 OR Arc.Qty<0) THEN Arc.Eur ELSE 0 END) AS Cost ")
        sb.AppendLine("		FROM Arc ")
        sb.AppendLine("		INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("		WHERE Alb.Fch<='" & sFch & "' ")
        sb.AppendLine("		GROUP BY Arc.MgzGuid, ARC.ArtGuid ")
        sb.AppendLine("		HAVING (SUM(CASE WHEN Arc.Cod < 50 THEN Arc.Qty ELSE -Arc.Qty END) > 0) ")
        sb.AppendLine("		) AS X ")
        sb.AppendLine("INNER JOIN ( ")
        sb.AppendLine("		SELECT Arc.MgzGuid, Arc.ArtGuid, Alb.Fch, Max(Arc.Pmc) AS LastPmc ")
        sb.AppendLine("		FROM Arc ")
        sb.AppendLine("		INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("		WHERE Alb.Fch<='" & sFch & "' ")
        sb.AppendLine("		GROUP BY Arc.MgzGuid, Arc.ArtGuid, Alb.Fch ")
        sb.AppendLine("		) Y ON X.MgzGuid = Y.MgzGuid AND X.ArtGuid = Y.ArtGuid AND X.LastIn = Y.Fch ")
        sb.AppendLine("INNER JOIN VwSkuNom ON X.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE X.MgzGuid ='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("AND VwSkuNom.SkuNoStk=0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oCategory As New DTOProductCategory
        Dim oBrand As New DTOProductBrand
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            'If oSku.Id = 20559 Then Stop
            With oSku
                .Stock = oDrd("Stk")
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("LastPmc"))
                .LastPurchaseDate = oDrd("LastIn")
            End With
            If oSku.Category.Equals(oCategory) Then
                oSku.Category = oCategory
            Else
                oCategory = oSku.Category
                If oCategory.Brand.Equals(oBrand) Then
                    oCategory.Brand = oBrand
                Else
                    oBrand = oCategory.Brand
                End If
            End If
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Skus(oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select Arc.ArtGuid ")
        sb.AppendLine(", VwProductNom.Cod As ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN VwProductNom ON Arc.ArtGuid = VwProductNom.Guid ")
        sb.AppendLine("WHERE Arc.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Arc.ArtGuid ")
        sb.AppendLine(", VwProductNom.Cod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("ORDER BY VwProductNom.BrandNom,VwProductNom.CategoryNom, VwProductNom.SkuNom")
        Dim sql As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(sql)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ArtGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SetPrecioMedioCoste(oMgz As DTOMgz, oSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean


        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid, Arc.Cod, Arc.Qty, Arc.Eur, Arc.Dto ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("WHERE Arc.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("AND Arc.ArtGuid = '" & oSku.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Alb.Fch, Alb.Alb ")
        Dim SQL As String = sb.ToString
        Dim iStkAnterior As Integer = 0
        Dim iNouStk As Integer = 0
        Dim DcPmcAnterior As Decimal = 0

        Dim oDrd = SQLHelper.GetDataReader(SQL)

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Do While oDrd.Read
                Dim iCod As Integer = oDrd("Cod")
                Dim iQty As Integer = oDrd("Qty")
                Dim BlInput As Boolean = iCod < 50 Or iQty < 0
                Dim DcPmc As Decimal
                Dim DcEur As Decimal = oDrd("Eur")
                Dim DcDto As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                Dim DcPreuNet = Math.Round(DcEur * (100 - DcDto) / 100, 2, MidpointRounding.AwayFromZero)

                If BlInput And iQty > 0 And (iStkAnterior + iQty) > 0 Then
                    DcPmc = (iStkAnterior * DcPmcAnterior + iQty * DcPreuNet) / (iStkAnterior + iQty)
                    iNouStk = iStkAnterior + iQty
                Else
                    DcPmc = DcPmcAnterior
                    iNouStk = iStkAnterior - iQty
                End If

                SQL = String.Format("UPDATE Arc SET Net={0}, Stk={1}, Pmc={2} WHERE Guid='{3}'", DcPreuNet.ToString(System.Globalization.CultureInfo.InvariantCulture), iNouStk, DcPmc.ToString(System.Globalization.CultureInfo.InvariantCulture), oDrd("Guid").ToString())
                SQLHelper.ExecuteNonQuery(SQL, oTrans)

                DcPmcAnterior = DcPmc
                iStkAnterior = iNouStk
            Loop

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try
        oDrd.Close()


        Return retval
    End Function

    Shared Function DeliveryItems(oMgz As DTOMgz, iYear As Integer) As List(Of DTODeliveryItem)
        Dim retval As New List(Of DTODeliveryItem)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.Guid AS ArcGuid, Arc.ArtGuid, Arc.Qty, Arc.Eur, Arc.Dto, Arc.Pmc, Arc.Cod AS ArcCod, Arc.PncGuid, Arc.PdcGuid, Arc.SpvGuid ")
        sb.AppendLine(", Alb.Guid AS AlbGuid, Alb.Alb, Alb.Fch, Alb.Cod AS AlbCod, Alb.CliGuid, CliGral.FullNom ")
        sb.AppendLine(", VwProductNom.Cod AS ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Alb.CliGuid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwProductNom ON Arc.ArtGuid = VwProductNom.Guid ")
        sb.AppendLine("WHERE Arc.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("AND Year(Alb.Fch) = " & iYear & " ")
        sb.AppendLine("ORDER BY Alb.Fch, Alb.Alb, Arc.Lin")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oDelivery As New DTODelivery
        Dim oContact As New DTOCustomer
        Do While oDrd.Read
            If Not oContact.Guid.Equals(oDrd("CliGuid")) Then
                oContact = New DTOCustomer(oDrd("CliGuid"))
                oContact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
            End If

            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Id = oDrd("Alb")
                    .Fch = oDrd("Fch")
                    .Customer = oContact
                    .Cod = oDrd("AlbCod")
                End With
            End If

            Dim item As New DTODeliveryItem(oDrd("ArcGuid"))
            With item
                .Delivery = oDelivery
                .Qty = oDrd("Qty")
                .Price = DTOAmt.Factory(CDec(oDrd("Eur")))
                .Dto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .Cod = oDrd("ArcCod")
                .sku = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ArtGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))

                If Not IsDBNull(oDrd("PdcGuid")) Then
                    Dim oPurchaseOrder As New DTOPurchaseOrder(oDrd("PdcGuid"))
                    If IsDBNull(oDrd("PncGuid")) Then
                        .PurchaseOrderItem = New DTOPurchaseOrderItem()
                    Else
                        .PurchaseOrderItem = New DTOPurchaseOrderItem(oDrd("PncGuid"))
                    End If
                    .PurchaseOrderItem.Sku = item.Sku
                ElseIf Not IsDBNull(oDrd("SpvGuid")) Then
                    .Spv = New DTOSpv(oDrd("SpvGuid"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class MgzsLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOMgz)
        Dim retval As New List(Of DTOMgz)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Mgz.Guid, Mgz.Nom AS Abr, CliGral.RaoSocial, CliGral.Obsoleto ")
        sb.AppendLine("FROM Mgz ")
        sb.AppendLine("INNER JOIN CliGral ON Mgz.Guid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY CliGral.Obsoleto, Abr, RaoSocial")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOMgz(oDrd("Guid"))
            With item
                .Emp = oEmp
                .Abr = oDrd("Abr")
                .Nom = oDrd("RaoSocial")
                .Obsoleto = oDrd("Obsoleto")
                '.Id = oDrd("Cli")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oSku As DTOProductSku) As List(Of DTOMgz)
        Dim retval As New List(Of DTOMgz)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.MgzGuid, (CASE WHEN Mgz.NOM IS NULL THEN CliGral.FullNom ELSE Mgz.NOM END) AS Nom ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Arc.MgzGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Mgz ON Arc.MgzGuid=Mgz.Guid ")
        sb.AppendLine("WHERE Arc.ArtGuid = '" & oSku.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Arc.MgzGuid, Mgz.Nom, CliGral.FullNom ")
        sb.AppendLine("ORDER BY Nom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("MgzGuid")
            Dim oMgz As New DTOMgz(oGuid)
            oMgz.Nom = oDrd("Nom").ToString
            retval.Add(oMgz)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Actius(oEmp As DTOEmp, Optional oSku As DTOProductSku = Nothing, Optional DtFch As Date = Nothing) As List(Of DTOMgz)
        Dim retval As New List(Of DTOMgz)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.MgzGuid, (CASE WHEN Mgz.NOM IS NULL THEN CliGral.FullNom ELSE Mgz.NOM END) AS Nom ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid=Alb.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Arc.MgzGuid=CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Mgz ON Arc.MgzGuid=Mgz.Guid ")
        sb.AppendLine("WHERE Alb.Emp=" & oEmp.Id & " ")
        If oSku IsNot Nothing Then
            sb.AppendLine("AND Arc.ArtGuid = '" & oSku.Guid.ToString & "' ")
        End If
        If DtFch <> Nothing Then
            sb.AppendLine("AND Alb.Fch <= '" & Format(DtFch, "yyyyMMdd") & "' ")
        End If
        sb.AppendLine("GROUP BY Arc.MgzGuid, Mgz.Nom, CliGral.FullNom ")
        'sb.AppendLine("HAVING SUM(CASE WHEN Arc.COD<50 THEN Arc.QTY ELSE -Arc.QTY END)>0 ")
        sb.AppendLine("ORDER BY Nom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("MgzGuid")
            Dim oMgz As New DTOMgz(oGuid)
            oMgz.Nom = oDrd("Nom").ToString
            retval.Add(oMgz)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class