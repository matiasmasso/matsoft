Public Class CustomerProductLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOCustomerProduct
        Dim retval As DTOCustomerProduct = Nothing
        Dim oCustomerProduct As New DTOCustomerProduct(oGuid)
        If Load(oCustomerProduct) Then
            retval = oCustomerProduct
        End If
        Return retval
    End Function

    Shared Function Find(oCustomer As DTOCustomer, oSku As DTOProductSku, sRef As String) As DTOCustomerProduct
        Dim retval As DTOCustomerProduct = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ArtCustRef.*, EciDept.Id AS DeptId, EciDept.Nom AS DeptNom ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("LEFT OUTER JOIN ECIDept ON ArtCustRef.CustomerDept=ECIDept.Guid ")
        sb.AppendLine("WHERE CliGuid='" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND ArtGuid='" & oSku.Guid.ToString & "' ")
        sb.AppendLine("AND Ref='" & sRef & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOCustomerProduct(oDrd("Guid"))
            With retval
                .Customer = oCustomer
                .Sku = oSku
                .Ref = sRef
                .DUN14 = SQLHelper.GetStringFromDataReader(oDrd("DUN14"))
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .Color = SQLHelper.GetStringFromDataReader(oDrd("Color"))
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                If Not IsDBNull(oDrd("CustomerDept")) Then
                    .EciDept = New DTO.Integracions.ElCorteIngles.Dept(oDrd("CustomerDept"))
                    .EciDept.Id = SQLHelper.GetStringFromDataReader(oDrd("DeptId"))
                    .EciDept.Nom = SQLHelper.GetStringFromDataReader(oDrd("DeptNom"))
                End If
                .IsLoaded = True
            End With
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function Load(ByRef oCustomerProduct As DTOCustomerProduct) As Boolean
        If Not oCustomerProduct.IsLoaded And Not oCustomerProduct.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ArtCustRef.CliGuid, ArtCustRef.ArtGuid, ArtCustRef.Ref, ArtCustRef.DUN14, ArtCustRef.Dsc, ArtCustRef.Color, ArtCustRef.FchFrom, ArtCustRef.FchTo ")
            sb.AppendLine(", VwSkuNom.*, CliGral.FullNom, ArtCustRef.CustomerDept, EciDept.Id AS DeptId, EciDept.Nom AS DeptNom ")
            sb.AppendLine("FROM ArtCustRef ")
            sb.AppendLine("INNER JOIN VwSkuNom ON ArtCustRef.ArtGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("INNER JOIN CliGral ON ArtCustRef.CliGuid = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN ECIDept ON ArtCustRef.CustomerDept=ECIDept.Guid ")
            sb.AppendLine("WHERE ArtCustRef.Guid='" & oCustomerProduct.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oCustomerProduct
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .Customer.FullNom = oDrd("FullNom")
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Ref = oDrd("Ref")
                    .DUN14 = SQLHelper.GetStringFromDataReader(oDrd("DUN14"))
                    .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                    .Color = SQLHelper.GetStringFromDataReader(oDrd("Color"))
                    .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                    If Not IsDBNull(oDrd("CustomerDept")) Then
                        .EciDept = New DTO.Integracions.ElCorteIngles.Dept(oDrd("CustomerDept"))
                        .EciDept.Id = SQLHelper.GetStringFromDataReader(oDrd("DeptId"))
                        .EciDept.Nom = SQLHelper.GetStringFromDataReader(oDrd("DeptNom"))
                    End If

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oCustomerProduct.IsLoaded
        Return retval
    End Function

    Shared Function Update(oCustomerProduct As DTOCustomerProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCustomerProduct, oTrans)
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


    Shared Sub Update(oCustomerProduct As DTOCustomerProduct, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("WHERE Guid='" & oCustomerProduct.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCustomerProduct.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oCustomerProduct
            oRow("CliGuid") = .Customer.Guid
            oRow("ArtGuid") = .Sku.Guid
            oRow("Ref") = .Ref
            oRow("DUN14") = SQLHelper.NullableString(.DUN14)
            oRow("Dsc") = SQLHelper.NullableString(.Dsc)
            oRow("Color") = SQLHelper.NullableString(.Color)
            oRow("FchFrom") = SQLHelper.NullableFch(.FchFrom)
            oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            oRow("CustomerDept") = SQLHelper.NullableBaseGuid(.EciDept)
        End With

        oDA.Update(oDs)
    End Sub



    Shared Function UpdateElCorteIngles(exs As List(Of Exception), item As DTO.Integracions.ElCorteIngles.Cataleg) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("WHERE Guid='" & item.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oConn As SqlConnection = SQLHelper.SQLConnection()
        Try
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oConn)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Guid") = item.Guid
            Else
                oRow = oTb.Rows(0)
            End If

            With item
                oRow("CliGuid") = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles).Guid
                oRow("ArtGuid") = SQLHelper.NullableBaseGuid(.Sku)
                oRow("Ref") = .Ref
                oRow("FchFrom") = SQLHelper.NullableFch(Today)
                If .Dept Is Nothing Then
                    oRow("CustomerDept") = System.DBNull.Value
                Else
                    oRow("CustomerDept") = .Dept.Guid
                End If
            End With

            oDA.Update(oDs)
        Catch ex As Exception
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try
        Return exs.Count = 0
    End Function




    Shared Function Delete(oCustomerProduct As DTOCustomerProduct, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oCustomerProduct, oTrans)
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


    Shared Sub Delete(oCustomerProduct As DTOCustomerProduct, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ArtCustRef WHERE Guid='" & oCustomerProduct.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class CustomerProductsLoader

    Shared Function All(Optional oCustomer As DTOCustomer = Nothing, Optional oSku As DTOProductSku = Nothing, Optional sRef As String = "") As List(Of DTOCustomerProduct)
        Dim retval As New List(Of DTOCustomerProduct)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ArtCustRef.* ")
        If oCustomer Is Nothing Then
            sb.AppendLine(", CliGral.FullNom ")
        End If
        If oSku Is Nothing Then
            sb.AppendLine(", VwSkuNom.* ")
        End If
        sb.AppendLine("FROM ArtCustRef ")
        If oSku Is Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON ArtCustRef.ArtGuid = VwSkuNom.SkuGuid ")
            'sb.AppendLine("LEFT OUTER JOIN VwProductNom ON ArtCustRef.ArtGuid = VwProductNom.Guid ")
        End If
        If oCustomer Is Nothing Then
            sb.AppendLine("LEFT OUTER JOIN CliGral ON ArtCustRef.CliGuid = CliGral.Guid ")
        Else
            sb.AppendLine("INNER JOIN VwCcxOrMe ON ArtCustRef.CliGuid = VwCcxOrMe.Ccx AND VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE 1=1 ")
        If oSku IsNot Nothing Then
            sb.AppendLine("AND ArtCustRef.ArtGuid = '" & oSku.Guid.ToString & "' ")
        End If
        If sRef > "" Then
            sb.AppendLine("AND ArtCustRef.Ref = '" & sRef & "' ")
        End If
        sb.AppendLine("ORDER BY ArtCustRef.Ref")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomerProduct(oDrd("Guid"))
            With item
                If oCustomer Is Nothing Then
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .Customer.FullNom = oDrd("FullNom")
                Else
                    .Customer = oCustomer
                End If
                If oSku Is Nothing Then
                    .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                    .Sku.obsoleto = oDrd("Obsoleto")
                    '.Sku = ProductLoader.NewProduct(DTOProduct.SourceCods.Sku, DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ArtGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
                    '.Sku.Ean13 = SQLHelper.GetEANFromDataReader(oDrd("Ean13"))
                    '.Sku.Ean13 = SQLHelper.GetEANFromDataReader(oDrd("CBar"))
                Else
                    .Sku = oSku
                End If
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .DUN14 = SQLHelper.GetStringFromDataReader(oDrd("DUN14"))
                .Dsc = SQLHelper.GetStringFromDataReader(oDrd("Dsc"))
                .Color = SQLHelper.GetStringFromDataReader(oDrd("Color"))
                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Compact(oCustomer As DTOCustomer) As List(Of DTOCustomerProduct.Compact)
        Dim retval As New List(Of DTOCustomerProduct.Compact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ArtCustRef.ArtGuid, ArtCustRef.Ref ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON ArtCustRef.CliGuid = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY ArtCustRef.Ref")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCustomerProduct.Compact(oDrd("ArtGuid"), oDrd("Ref"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SaveIfMissing(items As List(Of DTOCustomerProduct), exs As List(Of Exception)) As Boolean
        Dim filteredValues = MissingValues(items)
        If filteredValues.Count > 0 Then
            Dim SQL = "SELECT * FROM ArtCustRef WHERE 1=2"
            Dim oConn As SqlConnection = SQLHelper.SQLConnection()
            Try
                Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oConn)
                Dim oDs As New DataSet
                oDA.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)

                For Each item In items
                    Dim oRow = oTb.NewRow
                    oTb.Rows.Add(oRow)
                    With item
                        oRow("Guid") = .Guid
                        oRow("CliGuid") = .Customer.Guid
                        oRow("ArtGuid") = .Sku.Guid
                        oRow("Ref") = .Ref
                        oRow("DUN14") = SQLHelper.NullableString(.DUN14)
                        oRow("Dsc") = SQLHelper.NullableString(.Dsc)
                        oRow("Color") = SQLHelper.NullableString(.Color)
                        oRow("FchFrom") = SQLHelper.NullableFch(.FchFrom)
                        oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
                        oRow("CustomerDept") = SQLHelper.NullableBaseGuid(.EciDept)
                    End With
                Next

                oDA.Update(oDs)
            Catch ex As Exception
                exs.Add(ex)
            Finally
                oConn.Close()
            End Try
        End If
        Return exs.Count = 0
    End Function

    Shared Function MissingValues(values As List(Of DTOCustomerProduct)) As List(Of DTOCustomerProduct)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each value In values
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", value.Sku.Guid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT ArtCustRef.ArtGuid ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("INNER JOIN @Table X ON ArtCustRef.ArtGuid = X.Guid ")
        sb.AppendLine("WHERE ArtCustRef.CliGuid = '" & values.First.Customer.Guid.ToString() & "' ")
        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            values.Remove(values.First(Function(x) x.Sku.Guid.Equals(oDrd("ArtGuid"))))
        Loop
        oDrd.Close()
        Return values
    End Function

    Shared Function Delete(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next
        sb.AppendLine()
        sb.AppendLine("DELETE ArtCustRef ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("INNER JOIN @Table X ON ArtCustRef.Guid = X.Guid ")
        Dim SQL = sb.ToString()
        Dim i = SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0

    End Function


End Class

