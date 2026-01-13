Public Class HighResImageLoader


#Region "CRUD"

    Shared Function Find(sHash As String) As DTOHighResImage
        Dim retval As DTOHighResImage = Nothing
        Dim oHighResImage As New DTOHighResImage(sHash)
        If Load(oHighResImage) Then
            retval = oHighResImage
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oHighResImage As DTOHighResImage) As Boolean
        If Not oHighResImage.IsLoaded And Not oHighResImage.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT HighResImage.* ")
            sb.AppendLine(", X.Product, X.ProductCod, X.BrandGuid, X.BrandNom, X.CategoryGuid, X.CategoryNom, X.SkuNom ")
            sb.AppendLine("FROM HighResImage ")
            sb.AppendLine("LEFT OUTER JOIN (SELECT HighResImageProduct.Hash, HighResImageProduct.Product, Product2.Cod as ProductCod, Product2.BrandGuid, Product2.BrandNom, Product2.CategoryGuid, Product2.CategoryNom, Product2.SkuNomHighResImageProduct ")
            sb.AppendLine("             FROM HighResImageProduct ")
            sb.AppendLine("             INNER JOIN Product2 ON HighResImageProduct.Product = Product2.Guid) X ON HighResImage.Hash = X.Hash")
            sb.AppendLine("WHERE HighResImage.Hash = '" & oHighResImage.Hash & "'")

            Dim SQL As String = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oHighResImage.IsLoaded Then
                    With oHighResImage
                        .Url = oDrd("Url")
                        .Mime = oDrd("Mime")
                        .Length = oDrd("Length")
                        .Width = oDrd("Width")
                        .Height = oDrd("Height")
                        .HorizontalResolution = oDrd("HorizontalResolution")
                        .VerticalResolution = oDrd("VerticalResolution")
                        If Not IsDBNull(oDrd("Thumbnail")) Then
                            .Thumbnail = ImageHelper.GetImgFromByteArray(oDrd("Thumbnail"))
                        End If
                        .Fch = oDrd("Fch")
                        .Obsoleto = oDrd("Obsoleto")
                        .Cod = oDrd("Cod")
                        .Products = New List(Of DTOProduct)
                        .IsLoaded = True
                    End With

                    If Not IsDBNull(oDrd("X.Product")) Then
                        Dim oProduct As DTOProduct = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), CType(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"), oDrd("SkuMyd"))
                        oHighResImage.Products.Add(oProduct)
                    End If
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oHighResImage.IsLoaded
        Return retval
    End Function

    Shared Function FromUrl(sUrl As String) As DTOHighResImage
        Dim retval As DTOHighResImage = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT HighResImage.* ")
        sb.AppendLine(", X.Product, X.ProductCod, X.BrandGuid, X.BrandNom, X.CategoryGuid, X.CategoryNom, X.SkuNom ")
        sb.AppendLine("FROM HighResImage ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT HighResImageProduct.Hash, HighResImageProduct.Product, Product2.Cod as ProductCod, Product2.BrandGuid, Product2.BrandNom, Product2.CategoryGuid, Product2.CategoryNom, Product2.SkuNom, Product2.SkuMyd ")
        sb.AppendLine("             FROM HighResImageProduct ")
        sb.AppendLine("             INNER JOIN Product2 ON HighResImageProduct.Product = Product2.Guid) X ON HighResImage.Hash = X.Hash")
        sb.AppendLine("WHERE HighResImage.Url = '" & sUrl & "'")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If retval Is Nothing Then
                With retval
                    .Mime = oDrd("Mime")
                    .Length = oDrd("Length")
                    .Width = oDrd("Width")
                    .Height = oDrd("Height")
                    .HorizontalResolution = oDrd("HorizontalResolution")
                    .VerticalResolution = oDrd("VerticalResolution")
                    If Not IsDBNull(oDrd("Thumbnail")) Then
                        .Thumbnail = ImageHelper.GetImgFromByteArray(oDrd("Thumbnail"))
                    End If
                    .Fch = oDrd("Fch")
                    .Obsoleto = oDrd("Obsoleto")
                    .Cod = oDrd("Cod")
                    .Products = New List(Of DTOProduct)
                    .IsLoaded = True
                End With

                If Not IsDBNull(oDrd("X.Product")) Then
                    Dim oProduct As DTOProduct = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), CType(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Product"), oDrd("SkuNom"), oDrd("SkuMyd"))
                    retval.Products.Add(oProduct)
                End If
            End If
        Loop

        oDrd.Close()

        Return retval
    End Function

    Shared Function Update(oHighResImage As DTOHighResImage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oHighResImage, oTrans)
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

    Shared Sub Update(oHighResImage As DTOHighResImage, ByRef oTrans As SqlTransaction)
        UpdateHeader(oHighResImage, oTrans)
        If oHighResImage.Products IsNot Nothing Then
            If Not oHighResImage.IsNew Then DeleteProducts(oHighResImage, oTrans)
            UpdateProducts(oHighResImage, oTrans)
        End If
    End Sub

    Shared Sub UpdateProducts(oHighResImage As DTOHighResImage, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM HighResImageProduct ")
        sb.AppendLine("WHERE Hash = '" & oHighResImage.Hash & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oProduct As DTOProduct In oHighResImage.Products
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Hash") = oHighResImage.Hash
            oRow("Product") = oProduct.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateHeader(oHighResImage As DTOHighResImage, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM HighResImage ")
        sb.AppendLine("WHERE Hash = '" & oHighResImage.Hash & "'")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Hash") = oHighResImage.Hash
        Else
            oRow = oTb.Rows(0)
        End If

        With oHighResImage
            oRow("Url") = .Url
            oRow("Mime") = .Mime
            oRow("Length") = .Length
            oRow("Width") = .Width
            oRow("Height") = .Height
            oRow("HorizontalResolution") = .HorizontalResolution
            oRow("VerticalResolution") = .VerticalResolution
            oRow("Thumbnail") = ImageHelper.GetByteArrayFromImg(.Thumbnail)
            oRow("Obsoleto") = .Obsoleto
            oRow("Cod") = .Cod
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oHighResImage As DTOHighResImage, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oHighResImage, oTrans)
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


    Shared Sub Delete(oHighResImage As DTOHighResImage, ByRef oTrans As SqlTransaction)
        DeleteProducts(oHighResImage, oTrans)
        DeleteHeader(oHighResImage, oTrans)
    End Sub

    Shared Sub DeleteProducts(oHighResImage As DTOHighResImage, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE HighResImageProduct WHERE Hash = '" & oHighResImage.Hash & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oHighResImage As DTOHighResImage, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE HighResImage WHERE Hash = '" & oHighResImage.Hash & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class HighResImagesLoader

    Shared Function All(Optional oProduct As DTOProduct = Nothing) As List(Of DTOHighResImage)
        Dim retval As New List(Of DTOHighResImage)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT HighResImage.* ")
        sb.AppendLine("FROM HighResImage ")
        sb.AppendLine("INNER JOIN HighResImageProduct ON HighResImage.Hash = HighResImageProduct.Hash ")
        If oProduct IsNot Nothing Then
            sb.AppendLine("WHERE HighResImageProduct.Product='" & oProduct.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Fch DESC")

        Dim oHighResImage As New DTOHighResImage("?")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If oHighResImage.Hash <> oDrd("Hash") Then
                oHighResImage = New DTOHighResImage(oDrd("Hash").ToString)
                With oHighResImage
                    .Url = oDrd("Url")
                    .Mime = oDrd("Mime")
                    .Length = oDrd("Length")
                    .Width = oDrd("Width")
                    .Height = oDrd("Height")
                    .HorizontalResolution = oDrd("HorizontalResolution")
                    .VerticalResolution = oDrd("VerticalResolution")
                    If Not IsDBNull(oDrd("Thumbnail")) Then
                        .Thumbnail = ImageHelper.GetImgFromByteArray(oDrd("Thumbnail"))
                    End If
                    .Fch = oDrd("Fch")
                    .Obsoleto = oDrd("Obsoleto")
                    .Cod = oDrd("Cod")
                    .Products = New List(Of DTOProduct)
                    .IsLoaded = True
                End With
                retval.Add(oHighResImage)
            End If
            If oProduct Is Nothing Then
                oProduct = New DTOProduct(oDrd("Product"))
                oHighResImage.Products.Add(oProduct)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromProductOrChildren(oProduct As DTOProduct) As List(Of DTOHighResImage)
        Dim retval As New List(Of DTOHighResImage)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT HighResImage.* ")
        sb.AppendLine("FROM HighResImage ")
        sb.AppendLine("INNER JOIN HighResImageProduct ON HighResImage.Hash = HighResImageProduct.Hash ")
        sb.AppendLine("INNER JOIN ProductParent ON HighResImageProduct.Product = ProductParent.ChildGuid ")
        sb.AppendLine("WHERE HProductParent.ParentGuid='" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(Sql)
        Do While oDrd.Read
            Dim oHighResImage As New DTOHighResImage(oDrd("Hash").ToString)
            With oHighResImage
                .Url = oDrd("Url")
                .Mime = oDrd("Mime")
                .Length = oDrd("Length")
                .Width = oDrd("Width")
                .Height = oDrd("Height")
                .HorizontalResolution = oDrd("HorizontalResolution")
                .VerticalResolution = oDrd("VerticalResolution")
                If Not IsDBNull(oDrd("Thumbnail")) Then
                    .Thumbnail = ImageHelper.GetImgFromByteArray(oDrd("Thumbnail"))
                End If
                .Fch = oDrd("Fch")
                .Obsoleto = oDrd("Obsoleto")
                .Cod = oDrd("Cod")
                .IsLoaded = True
            End With
            retval.Add(oHighResImage)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ExistsFromProductOrChildren(oProduct As DTOProduct) As Boolean
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 HighResImage.* ")
        sb.AppendLine("FROM HighResImage ")
        sb.AppendLine("INNER JOIN HighResImageProduct ON HighResImage.Hash = HighResImageProduct.Hash ")
        sb.AppendLine("INNER JOIN ProductParent ON HighResImageProduct.Product = ProductParent.ChildGuid ")
        sb.AppendLine("WHERE ProductParent.ParentGuid='" & oProduct.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        retval = oDrd.Read
        oDrd.Close()
        Return retval
    End Function

End Class