Public Class CliProductBlockedLoader

    Shared Function Find(oContact As DTOContact, oProduct As DTOProduct) As DTOCliProductBlocked
        Dim retval As DTOCliProductBlocked = Nothing
        Dim oCliProductBlocked As New DTOCliProductBlocked
        With oCliProductBlocked
            .Contact = oContact
            .Product = oProduct
        End With

        If Load(oCliProductBlocked) Then
            retval = oCliProductBlocked
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oCliProductBlocked As DTOCliProductBlocked) As Boolean
        If Not oCliProductBlocked.IsLoaded And Not oCliProductBlocked.IsNew Then
            If oCliProductBlocked.Contact IsNot Nothing And oCliProductBlocked.Product IsNot Nothing Then
                Dim sb As New System.Text.StringBuilder
                sb.AppendLine("SELECT CliTpa.Cod, CliTpa.Zip, CliTpa.Obs, Tpa.CodDist, CliGral.FullNom ")
                sb.AppendLine(", VwProductNom.* ")
                sb.AppendLine("FROM CliTpa ")
                sb.AppendLine("INNER JOIN VwProductNom ON CliTpa.ProductGuid = VwProductNom.Guid ")
                sb.AppendLine("INNER JOIN VwProductParent ON CliTpa.ProductGuid = VwProductParent.Child ")
                sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
                sb.AppendLine("INNER JOIN CliGral ON CliTpa.CliGuid = CliGral.Guid ")
                sb.AppendLine("WHERE CliTpa.CliGuid='" & oCliProductBlocked.Contact.Guid.ToString & "' ")
                sb.AppendLine("AND CliTpa.ProductGuid = '" & CType(oCliProductBlocked.product, DTOProduct).Guid.ToString & "' ")
                Dim SQL As String = sb.ToString

                Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
                If oDrd.Read Then
                    With oCliProductBlocked
                        .DistModel = IIf(oDrd("CodDist") = 1, DTOCliProductBlocked.DistModels.Closed, DTOCliProductBlocked.DistModels.Open)
                        .product = SQLHelper.GetProductFromDataReader(oDrd)
                        .Cod = oDrd("Cod")
                        .Zip = oDrd("Zip")
                        .obs = oDrd("Obs")
                        .contact.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                        .IsLoaded = True
                    End With
                End If

                oDrd.Close()
            End If
        End If
        Dim retval As Boolean = oCliProductBlocked.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCliProductBlocked As DTOCliProductBlocked, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oCliProductBlocked, oTrans)
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

    Shared Sub Update(oCliProductBlocked As DTOCliProductBlocked, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM CliTpa ")
        sb.AppendLine("WHERE CliGuid=@CliGuid AND ProductGuid = @ProductGuid")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@CliGuid", oCliProductBlocked.contact.Guid.ToString, "@ProductGuid", CType(oCliProductBlocked.product, DTOProduct).Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("CliGuid") = oCliProductBlocked.Contact.Guid
            oRow("ProductGuid") = CType(oCliProductBlocked.product, DTOProduct).Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCliProductBlocked
            oRow("Cod") = .Cod
            oRow("Zip") = .Zip
            oRow("Obs") = .Obs
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oCliProductBlocked As DTOCliProductBlocked, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCliProductBlocked, oTrans)
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


    Shared Sub Delete(oCliProductBlocked As DTOCliProductBlocked, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE CliTpa ")
        sb.AppendLine("WHERE CliGuid='" & oCliProductBlocked.Contact.Guid.ToString & "' ")
        sb.AppendLine("AND ProductGuid = '" & CType(oCliProductBlocked.product, DTOProduct).Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function Closest(oContact As DTOContact, oProduct As DTOProduct) As DTOCliProductBlocked
        Dim retval As New DTOCliProductBlocked

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.CodDist, CliTpa.Cod, CliTpa.Zip ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN VwProductParent PP1 ON Tpa.Guid = PP1.Parent ")
        sb.AppendLine("LEFT OUTER JOIN VwProductParent PP2 ON PP1.Child = PP2.Child")
        sb.AppendLine("LEFT OUTER JOIN CliTpa ON PP2.Parent = CliTpa.ProductGuid AND CliTpa.CliGuid='" & oContact.Guid.ToString & "' ")
        sb.AppendLine("WHERE PP1.Child = '" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Tpa.CodDist, CliTpa.Cod, CliTpa.Zip ")
        sb.AppendLine("ORDER BY CliTpa.Cod DESC, CliTpa.Zip DESC")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With retval
                .DistModel = IIf(oDrd("CodDist") = 1, DTOCliProductBlocked.DistModels.Closed, DTOCliProductBlocked.DistModels.Open)
                If Not IsDBNull(oDrd("Cod")) Then
                    .Cod = oDrd("Cod")
                    .Zip = oDrd("Zip")
                End If
            End With
        End If

        oDrd.Close()

        Return retval
    End Function

    Shared Function AltresEnExclusiva(oContact As DTOContact, oProduct As DTOProduct) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        ContactLoader.Load(oContact)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select CliGral.Guid, CliGral.RaoSocial ")
        sb.AppendLine("FROM CliTpa ")
        sb.AppendLine("INNER JOIN CliGral ON CliTpa.CliGuid = CliGral.Guid  ")
        sb.AppendLine("INNER JOIN CliAdr ON CliTpa.CliGuid = CliAdr.SrcGuid  ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip = Zip.Guid  ")
        sb.AppendLine("WHERE CliTpa.Cod = 1 ")
        sb.AppendLine("AND CliTpa.ProductGuid = @Product ")
        sb.AppendLine("AND (Zip.Location=@Location OR '@ZipCod' LIKE Zip.ZipCod)  ")
        Dim SQL As String = sb.ToString

        Dim oZip As DTOZip = oContact.Address.Zip
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Product", oProduct.Guid.ToString, "@Location", oZip.Location.Guid.ToString, "@ZipCod", oZip.ZipCod)
        Do While oDrd.Read
            Dim item As New DTOContact(oDrd("Guid"))
            With item
                .Nom = oDrd("RaoSocial")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval

    End Function

End Class

Public Class CliProductsBlockedLoader

    Shared Function All(oUser As DTOUser) As List(Of DTOCliProductBlocked)
        Dim retval As New List(Of DTOCliProductBlocked)

        If Not oUser Is Nothing Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT X.ProductGuid, X.Cod, VwProductNom.Cod AS ProductCod, Tpa.CodDist ")
            sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")

            sb.AppendLine("FROM (SELECT C1.CliGuid, C1.ProductGuid, C1.Cod FROM CliTpa C1 ")
            'sb.AppendLine("UNION ")
            'sb.AppendLine("(SELECT C2.CliGuid, C2.ProductGuid, " & CInt(DTOCliProductBlocked.Codis.AltresEnExclusiva) & " AS Cod ")
            'sb.AppendLine("     FROM CliTpa C2 ")
            'sb.AppendLine("     INNER JOIN CliAdr ON C2.CliGuid = CliAdr.SrcGuid ")
            'sb.AppendLine("     INNER JOIN Zip ON CliAdr.Zip = Zip.Guid ")
            'sb.AppendLine("     WHERE C2.Cod =" & CInt(DTOCliProductBlocked.Codis.Exclusiva) & " ")
            'sb.AppendLine("     AND (Zip.Location='" & oContact.Address.Zip.Location.Guid.ToString & "' OR '" & oContact.Address.Zip.ZipCod & "' LIKE Zip.ZipCod) ")
            'sb.AppendLine("     AND C2.CliGuid <> '" & oContact.Guid.ToString & "' ")
            'sb.AppendLine("     GROUP BY C2.ProductGuid) ")
            sb.AppendLine(") X ")
            sb.AppendLine("INNER JOIN VwProductNom ON X.ProductGuid = VwProductNom.Guid ")
            sb.AppendLine("INNER JOIN CliTpa ON X.ProductGuid = CliTpa.ProductGuid ")
            sb.AppendLine("INNER JOIN VwProductParent ON X.ProductGuid = VwProductParent.Child ")
            sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
            sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = X.CliGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            sb.AppendLine("GROUP BY X.ProductGuid, X.Cod, VwProductNom.Cod, Tpa.CodDist ")
            sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine("ORDER BY VwProductNom.BrandGuid, VwProductNom.CategoryGuid, X.ProductGuid")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim item As New DTOCliProductBlocked
                With item
                    '.Contact = oContact
                    .product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ProductGuid"), oDrd("SkuNom"))
                    .DistModel = IIf(oDrd("CodDist") = 1, DTOCliProductBlocked.DistModels.Closed, DTOCliProductBlocked.DistModels.Open)
                    .Cod = oDrd("Cod")
                    '.Zip = oDrd("Zip")
                    '.Obs = oDrd("Obs")
                    '.IsLoaded = True
                End With
                retval.Add(item)
            Loop
            oDrd.Close()
        End If

        Return retval
    End Function

    Shared Function All(oContact As DTOContact) As List(Of DTOCliProductBlocked)
        Dim retval As New List(Of DTOCliProductBlocked)

        If Not oContact Is Nothing Then
            ContactLoader.Load(oContact)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT X.ProductGuid, X.Cod, VwProductNom.Cod AS ProductCod, Tpa.CodDist ")
            sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")

            sb.AppendLine("FROM (SELECT C1.ProductGuid, C1.Cod FROM CliTpa C1 WHERE C1.CliGuid = '" & oContact.Guid.ToString & "' ")
            sb.AppendLine("UNION ")
            sb.AppendLine("(SELECT C2.ProductGuid, " & CInt(DTOCliProductBlocked.Codis.AltresEnExclusiva) & " AS Cod ")
            sb.AppendLine("     FROM CliTpa C2 ")
            sb.AppendLine("     INNER JOIN CliAdr ON C2.CliGuid = CliAdr.SrcGuid ")
            sb.AppendLine("     INNER JOIN Zip ON CliAdr.Zip = Zip.Guid ")
            sb.AppendLine("     WHERE C2.Cod =" & CInt(DTOCliProductBlocked.Codis.Exclusiva) & " ")
            sb.AppendLine("     AND (Zip.Location='" & oContact.Address.Zip.Location.Guid.ToString & "' OR '" & oContact.Address.Zip.ZipCod & "' LIKE Zip.ZipCod) ")
            sb.AppendLine("     AND C2.CliGuid <> '" & oContact.Guid.ToString & "' ")
            sb.AppendLine("     GROUP BY C2.ProductGuid) ")
            sb.AppendLine(") X ")
            sb.AppendLine("INNER JOIN VwProductNom ON X.ProductGuid = VwProductNom.Guid ")
            sb.AppendLine("INNER JOIN CliTpa ON X.ProductGuid = CliTpa.ProductGuid ")
            sb.AppendLine("INNER JOIN VwProductParent ON X.ProductGuid = VwProductParent.Child ")
            sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
            sb.AppendLine("GROUP BY X.ProductGuid, X.Cod, VwProductNom.Cod, Tpa.CodDist ")
            sb.AppendLine(", VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine("ORDER BY VwProductNom.BrandGuid, VwProductNom.CategoryGuid, X.ProductGuid")
            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim item As New DTOCliProductBlocked
                With item
                    .Contact = oContact
                    .product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ProductGuid"), oDrd("SkuNom"))
                    .DistModel = IIf(oDrd("CodDist") = 1, DTOCliProductBlocked.DistModels.Closed, DTOCliProductBlocked.DistModels.Open)
                    .Cod = oDrd("Cod")
                    '.Zip = oDrd("Zip")
                    '.Obs = oDrd("Obs")
                    '.IsLoaded = True
                End With
                retval.Add(item)
            Loop
            oDrd.Close()
        End If

        Return retval
    End Function

    Shared Function DistribuidorsOficialsActiveEmails(oBrand As DTOProductBrand) As List(Of DTOEmail)
        Dim retval As New List(Of DTOEmail)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.adr ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer=Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid=Email.Guid ")
        sb.AppendLine("WHERE VwCustomerSkus.Brandguid='7C097674-233E-4899-92A7-37F37DD6D1F4' ")
        sb.AppendLine("AND Email.BadmailGuid IS NULL AND Email.NoNews=0 AND Email.Obsoleto=0 ")
        sb.AppendLine("GROUP BY Email.Guid, Email.Adr ")
        sb.AppendLine("ORDER BY SUBSTRING(email.adr,CHARINDEX('@',Email.Adr)+1,100) ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(Sql)
        Do While oDrd.Read
            Dim item As New DTOEmail(oDrd("guid"))
            item.EmailAddress = oDrd("adr")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Delete(oCliProductsBlocked As List(Of DTOCliProductBlocked), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCliProductsBlocked, oTrans)
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


    Shared Sub Delete(oCliProductsBlocked As List(Of DTOCliProductBlocked), ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE CliTpa ")
        sb.AppendLine("WHERE (")
        For Each item As DTOCliProductBlocked In oCliProductsBlocked
            If Not item.Equals(oCliProductsBlocked.First) Then
                sb.Append("OR ")
            End If
            sb.AppendLine("(CliGuid='" & item.Contact.Guid.ToString & "' ")
            sb.AppendLine("AND ProductGuid = '" & CType(item.product, DTOProduct).Guid.ToString & "') ")
        Next
        sb.AppendLine(") ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class