Public Class PromoLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOPromo
        Dim retval As DTOPromo = Nothing
        Dim oPromo As New DTOPromo(oGuid)
        If Load(oPromo) Then
            retval = oPromo
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oPromo As DTOPromo) As Boolean
        If Not oPromo.IsLoaded And Not oPromo.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Promo.* ")
            sb.AppendLine(", Product2.Cod AS ProductCod, Product2.BrandGuid, Product2.BrandNom, Product2.CategoryGuid, Product2.CategoryNom, Product2.SkuNom, Product2.SkuMyd ")
            sb.AppendLine("FROM Promo ")
            sb.AppendLine("LEFT OUTER JOIN Product2 ON Promo.Product = Product2.Guid ")
            sb.AppendLine("WHERE Promo.Guid='" & oPromo.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oPromo
                    .Caption = oDrd("Caption")
                    If Not IsDBNull(oDrd("Product")) Then
                        .Product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), CType(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"), oDrd("SkuMyd"))
                    End If
                    .FchFrom = oDrd("FchFrom")
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    .Bases = SQLHelper.GetStringFromDataReader(oDrd("Bases"))
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oPromo.IsLoaded
        Return retval
    End Function

    Shared Function Update(oPromo As DTOPromo, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oPromo, oTrans)
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


    Shared Sub Update(oPromo As DTOPromo, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Promo ")
        sb.AppendLine("WHERE Guid=@Guid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oPromo.Guid.ToString)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oPromo.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oPromo
            oRow("Caption") = .Caption
            oRow("Product") = SQLHelper.NullableBaseGuid(.Product)
            oRow("FchFrom") = .FchFrom
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            oRow("Bases") = SQLHelper.NullableString(.Bases)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oPromo As DTOPromo, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oPromo, oTrans)
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


    Shared Sub Delete(oPromo As DTOPromo, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Promo WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oPromo.Guid.ToString)
    End Sub

#End Region

End Class

Public Class PromosLoader

    Shared Function All(oUser As DTOUser, archive As Boolean) As List(Of DTOPromo)
        Dim retval As New List(Of DTOPromo)
        Dim sb As New System.Text.StringBuilder
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Marketing, DTORol.Ids.Operadora, DTORol.Ids.Comercial
                sb.AppendLine("SELECT Promo.Guid, Promo.Caption, Promo.Product, Promo.FchFrom, Promo.FchTo, Promo.Bases ")
                sb.AppendLine(", COUNT(Pdc.Guid) AS Pdcs ")
                sb.AppendLine("FROM Promo ")
                sb.AppendLine("LEFT OUTER JOIN Pdc ON Promo.Guid=Pdc.Promo ")
                If Not archive Then
                    sb.AppendLine("WHERE GETDATE() BETWEEN Promo.FchFrom AND Promo.FchTo ")
                End If
                sb.AppendLine("GROUP BY Promo.Guid, Promo.Caption, Promo.Product, Promo.FchFrom, Promo.FchTo, Promo.Bases ")
                sb.AppendLine("ORDER BY Promo.FchFrom DESC, Promo.Caption")
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("SELECT Promo.Guid, Promo.Caption, Promo.Product, Promo.FchFrom, Promo.FchTo, Promo.Bases ")
                sb.AppendLine(", COUNT(Pdc.Guid) AS Pdcs ")
                sb.AppendLine("FROM Promo ")
                sb.AppendLine("INNER JOIN ProductParent ON Promo.Product = ProductParent.ChildGuid ")
                sb.AppendLine("INNER JOIN Tpa ON Tpa.Guid = ProductParent.ParentGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = Tpa.Proveidor AND Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN Pdc ON Promo.Guid=Pdc.Promo ")
                If Not archive Then
                    sb.AppendLine("WHERE GETDATE() BETWEEN Promo.FchFrom AND Promo.FchTo ")
                End If
                sb.AppendLine("GROUP BY Promo.Guid, Promo.Caption, Promo.Product, Promo.FchFrom, Promo.FchTo ")
                sb.AppendLine("ORDER BY Promo.FchFrom DESC, Promo.Caption")
            Case DTORol.Ids.Rep
                sb.AppendLine("SELECT Promo.Guid, Promo.Caption, Promo.Product, Promo.FchFrom, Promo.FchTo, Promo.Bases ")
                sb.AppendLine(", COUNT(Pdc.Guid) AS Pdcs ")
                sb.AppendLine("FROM Promo ")
                sb.AppendLine("LEFT OUTER JOIN Pdc ON Promo.Guid=Pdc.Promo ")
                sb.AppendLine("INNER JOIN ProductParent ON Promo.Product = ProductParent.ChildGuid ")
                sb.AppendLine("INNER JOIN RepProducts ON RepProducts.Product = ProductParent.ParentGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = RepProducts.Rep AND Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("WHERE (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE())  ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
                sb.AppendLine("AND GETDATE() >= Promo.FchFrom ") 'evita mostrar les que encara no estan actives
                If Not archive Then
                    sb.AppendLine("AND GETDATE() <= Promo.FchTo ")
                End If
                sb.AppendLine("GROUP BY Promo.Guid, Promo.Caption, Promo.Product, Promo.FchFrom, Promo.FchTo, Promo.Bases ")
                sb.AppendLine("ORDER BY Promo.FchFrom DESC, Promo.Caption")
            Case DTORol.Ids.Cli
                sb.AppendLine("SELECT Promo.Guid, Promo.Caption, Promo.Product, Promo.FchFrom, Promo.FchTo, Promo.Bases ")
                sb.AppendLine(", 0 AS Pdcs ")
                sb.AppendLine("FROM Promo ")
                sb.AppendLine("WHERE FchFrom<=GETDATE() ")
                sb.AppendLine("AND GETDATE() >= Promo.FchFrom ") 'evita mostrar les que encara no estan actives
                If Not archive Then
                    sb.AppendLine("AND GETDATE() <= Promo.FchTo ")
                End If
                sb.AppendLine("ORDER BY FchFrom DESC, Promo.Caption")
        End Select

        Dim SQL As String = sb.ToString
            If SQL > "" Then
                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                Do While oDrd.Read
                    Dim item As New DTOPromo(oDrd("Guid"))
                    With item
                        .Caption = oDrd("Caption")
                        .Bases = SQLHelper.GetStringFromDataReader(oDrd("Bases"))
                        If Not IsDBNull(oDrd("Product")) Then
                            .Product = New DTOProduct(oDrd("Product"))
                        End If
                        .FchFrom = Defaults.FchOrNothing(oDrd("FchFrom"))
                        .FchTo = Defaults.FchOrNothing(oDrd("FchTo"))
                        If Not IsDBNull(oDrd("Pdcs")) Then
                            .OrdersCount = oDrd("Pdcs")
                        End If
                    End With
                    retval.Add(item)
                Loop
                oDrd.Close()
            End If
        Return retval
    End Function

End Class
