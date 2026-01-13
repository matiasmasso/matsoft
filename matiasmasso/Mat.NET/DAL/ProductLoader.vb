Public Class ProductLoader

    Shared Function NewProduct(oGuid As Guid, oCod As DTOProduct.SourceCods, Optional sNom As String = "") As DTOProduct
        Dim retval As DTOProduct = Nothing
        Select Case oCod
            Case DTOProduct.SourceCods.Brand
                retval = New DTOProductBrand(oGuid)
                retval.sourceCod = oCod
                DirectCast(retval, DTOProductBrand).Nom.Esp = sNom
            Case DTOProduct.SourceCods.Category
                retval = New DTOProductCategory(oGuid)
                retval.sourceCod = oCod
                DirectCast(retval, DTOProductCategory).Nom.Esp = sNom
            Case DTOProduct.SourceCods.Sku
                retval = New DTOProductSku(oGuid)
                retval.sourceCod = oCod
                DirectCast(retval, DTOProductSku).Nom.Esp = sNom
        End Select
        Return retval
    End Function

    Shared Function NewProduct(oCod As DTOProduct.SourceCods, oBrandGuid As Guid, sBrandNom As String, oCategoryGuid As Object, sCategoryNom As Object, oSkuGuid As Object, sSkuNom As Object, Optional sSkuNomLlarg As Object = Nothing, Optional iInnerPack As Integer = 0, Optional ForzarInnerPack As Boolean = False) As DTOProduct
        Dim retval As DTOProduct = Nothing
        Select Case oCod
            Case DTOProduct.SourceCods.Brand
                retval = New DTOProductBrand(oBrandGuid)
                With DirectCast(retval, DTOProductBrand)
                    .sourceCod = oCod
                    .Nom.Esp = sBrandNom
                End With
            Case DTOProduct.SourceCods.Category
                Dim oBrand As New DTOProductBrand(oBrandGuid)
                With oBrand
                    .sourceCod = DTOProduct.SourceCods.Brand
                    .Nom.Esp = sBrandNom
                End With
                retval = New DTOProductCategory(oCategoryGuid)
                With DirectCast(retval, DTOProductCategory)
                    .sourceCod = oCod
                    .Nom.Esp = sCategoryNom
                    .innerPack = iInnerPack
                    .forzarInnerPack = ForzarInnerPack
                    .Brand = oBrand
                End With
            Case DTOProduct.SourceCods.Sku
                Dim oBrand As New DTOProductBrand(oBrandGuid)
                With oBrand
                    .sourceCod = DTOProduct.SourceCods.Brand
                    .Nom.Esp = sBrandNom
                End With
                Dim oCategory As New DTOProductCategory(oCategoryGuid)
                With oCategory
                    .sourceCod = DTOProduct.SourceCods.Category
                    .Nom.Esp = sCategoryNom
                    .Brand = oBrand
                End With
                If TypeOf oSkuGuid Is System.String Then oSkuGuid = New Guid(oSkuGuid.ToString())
                retval = New DTOProductSku(oSkuGuid)
                With DirectCast(retval, DTOProductSku)
                    .sourceCod = oCod
                    .Nom.Esp = sSkuNom
                    .Category = oCategory
                    .innerPack = iInnerPack
                    .forzarInnerPack = ForzarInnerPack
                    If sSkuNomLlarg IsNot Nothing Then
                        .NomLlarg.Esp = sSkuNomLlarg
                    End If
                End With
        End Select
        Return retval
    End Function


    Shared Function Find(oGuid As Guid) As DTOProduct
        Dim retval As DTOProduct = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM VwProductNom ")
        sb.AppendLine("WHERE VwProductNom.Guid='" & oGuid.ToString & "'")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetProductFromDataReader(oDrd)
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function NewProduct(oGuid As Guid, oCod As DTOProduct.SourceCods) As DTOProduct
        Dim retval As DTOProduct = Nothing
        Select Case oCod
            Case DTOProduct.SourceCods.Brand
                retval = New DTOProductBrand(oGuid)
            Case DTOProduct.SourceCods.Category
                retval = New DTOProductCategory(oGuid)
            Case DTOProduct.SourceCods.Sku
                retval = New DTOProductSku(oGuid)
        End Select
        Return retval
    End Function

    Shared Function FraccionarTemporalment(oProduct As DTOProduct, oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retVal As Boolean = False

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim SQL = "DELETE SkuMoqLock WHERE Fch < DATEADD(HH,-24,GETDATE())" ' delete all entries older than 24 hours
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            SQL = "SELECT * FROM SkuMoqLock WHERE Sku='" & oProduct.Guid.ToString & "' AND Usr='" & oUser.Guid.ToString & "' "
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Sku") = oProduct.Guid
                oRow("Usr") = oUser.Guid
            Else
                oRow = oTb.Rows(0)
            End If

            oRow("Fch") = DTO.GlobalVariables.Now()
            oDA.Update(oDs)

            oTrans.Commit()
            retVal = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retVal
    End Function

    Shared Function AllowUserToFraccionarInnerPack(oProduct As DTOProduct, oUser As DTOUser) As Boolean
        Dim retVal As Boolean = False
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Fch ")
        sb.AppendLine("FROM SkuMoqLock ")
        sb.AppendLine("WHERE Sku = '" & oProduct.Guid.ToString & "' ")
        sb.AppendLine("AND Usr = '" & oUser.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim oFch As DateTime = oDrd("Fch")
            Dim Deadline As DateTime = oFch.AddMinutes(2)
            retVal = (Now < oFch.AddMinutes(2))
        End If
        oDrd.Close()
        Return retVal
    End Function


    Shared Function Relateds(oSrc As DTOProduct, oRelated As DTOProduct.Relateds, Optional IncludeObsoletos As Boolean = False, Optional AllowInheritance As Boolean = True) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        If oSrc IsNot Nothing Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ArtSpare.ProductGuid AS SkuGuid ")
            sb.AppendLine(", VwSkuNom.* ")
            sb.AppendLine(", SkuExcerpt.Esp AS SkuExcerptEsp, SkuExcerpt.Cat AS SkuExcerptCat, SkuExcerpt.Eng AS SkuExcerptEng, SkuExcerpt.Por AS SkuExcerptPor ")
            sb.AppendLine(", CategoryExcerpt.Esp AS CategoryExcerptEsp, CategoryExcerpt.Cat AS CategoryExcerptCat, CategoryExcerpt.Eng AS CategoryExcerptEng, CategoryExcerpt.Por AS CategoryExcerptPor ")
            sb.AppendLine("FROM ArtSpare ")
            sb.AppendLine("INNER JOIN VwSkuNom ON ArtSpare.ProductGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS SkuExcerpt ON VwSkuNom.SkuGuid = SkuExcerpt.Guid AND SkuExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryExcerpt ON VwSkuNom.CategoryGuid = CategoryExcerpt.Guid AND CategoryExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")

            If AllowInheritance Then
                sb.AppendLine("INNER JOIN VwProductParent ON ArtSpare.TargetGuid = VwProductParent.Parent ")
                sb.AppendLine("WHERE ArtSpare.Cod=" & oRelated & " ")
                sb.AppendLine("AND VwProductParent.Child='" & oSrc.Guid.ToString & "' ")
            Else
                sb.AppendLine("WHERE ArtSpare.Cod=" & oRelated & " ")
                sb.AppendLine("AND ArtSpare.TargetGuid='" & oSrc.Guid.ToString & "' ")
            End If

            sb.AppendLine("ORDER BY BrandNom, CategoryNomEsp, SkuNomEsp ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd) ' ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("SkuGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
                If (IncludeObsoletos Or Not oSku.obsoleto) Then
                    SQLHelper.LoadLangTextFromDataReader(oSku.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    SQLHelper.LoadLangTextFromDataReader(oSku.Excerpt, oDrd, "SkuExcerptEsp", "SkuExcerptCat", "SkuExcerptEng", "SkuExcerptPor")
                    SQLHelper.LoadLangTextFromDataReader(oSku.Category.Excerpt, oDrd, "CategoryExcerptEsp", "CategoryExcerptCat", "CategoryExcerptEng", "CategoryExcerptPor")
                    'oSku.excerpt = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuExcerptEsp", "SkuExcerptCat", "SkuExcerptEng", "SkuExcerptPor")
                    'oSku.Description = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuDscEsp", "SkuDscCat", "SkuDscEng", "SkuDscPor")
                    'oSku.category.excerpt = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryExcerptEsp", "CategoryExcerptCat", "CategoryExcerptEng", "CategoryExcerptPor")
                    'oSku.Category.Description = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryDscEsp", "CategoryDscCat", "CategoryDscEng", "CategoryDscPor")
                    retval.Add(oSku)
                End If
            Loop
            oDrd.Close()
        End If
        Return retval

    End Function


    Shared Function UpdateRelateds(oSrc As DTOProduct, oRelateds As List(Of DTOProductSku), oRelated As DTOProductSku.Relateds, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DeleteRelateds(oSrc, oRelated, oTrans)
            UpdateRelateds(oSrc, oRelateds, oRelated, oTrans)
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

    Shared Sub UpdateRelateds(oSrc As DTOProduct, oRelateds As List(Of DTOProductSku), oRelated As DTOProductSku.Relateds, oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ArtSpare ")
        sb.AppendLine("WHERE TargetGuid='" & oSrc.Guid.ToString & "' AND Cod=" & oRelated & " ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOProduct In oRelateds
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Cod") = oRelated
            oRow("TargetGuid") = oSrc.Guid
            oRow("ProductGuid") = item.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Sub DeleteRelateds(oSrc As DTOProduct, oRelated As DTOProductSku.Relateds, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ArtSpare WHERE TargetGuid='" & oSrc.Guid.ToString & "' AND Cod=" & oRelated & " "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteRelateds(oSrc As DTOProduct, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ArtSpare WHERE TargetGuid='" & oSrc.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function RelatedsExist(oCod As DTOProduct.Relateds, oProduct As DTOProduct) As Boolean
        Dim SQL As String = "SELECT TOP 1 ProductGuid FROM ArtSpare WHERE Cod= " & oCod & " AND TargetGuid ='" & oProduct.Guid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim RetVal As Boolean = oDrd.Read
        oDrd.Close()
        Return RetVal
    End Function

    Shared Sub DeleteLangTexts(oSrc As DTOProduct, oTrans As SqlTransaction)
        Dim SQL As String = "DELETE LangText WHERE Guid='" & oSrc.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class

Public Class ProductsLoader

    Shared Function FromNom(oBrand As DTOProductBrand, sNom As String) As List(Of DTOProduct)
        Dim retval As New List(Of DTOProduct)
        Dim segments = sNom.Split("/")
        Dim sb As New Text.StringBuilder
        If segments.Length = 1 Then
            sb.AppendLine("SELECT Dept.Guid, 5 as Cod ")
            sb.AppendLine("FROM LangText ")
            sb.AppendLine("INNER JOIN Dept ON LangText.Guid = Dept.Guid AND LangText.Src = 28 ")
            sb.AppendLine("WHERE Dept.Brand = '" & oBrand.Guid.ToString & "' ")
            sb.AppendLine("AND LangText.Text LIKE '" & sNom & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("UNION SELECT Stp.Guid, 3 as Cod ")
            sb.AppendLine("FROM LangText ")
            sb.AppendLine("INNER JOIN Stp ON LangText.Guid = Stp.Guid AND LangText.Src = 28 ")
            sb.AppendLine("WHERE StpBrand = '" & oBrand.Guid.ToString & "' ")
            sb.AppendLine("AND LangText.Text LIKE '" & sNom & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
        Else
            Dim sCategory = segments(0)
            sNom = segments(1)
            sb.AppendLine("SELECT Art.Guid, " & DTOProduct.SourceCods.Sku & " as Cod ")
            sb.AppendLine("FROM Art ")
            sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
            sb.AppendLine("INNER JOIN Stp ON Art.Category = Stp.Guid ")
            sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
            sb.AppendLine("WHERE Stp.Brand = '" & oBrand.Guid.ToString & "' ")
            sb.AppendLine("AND (CategoryNom.Esp LIKE '" & sCategory & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     OR CategoryNom.Cat LIKE '" & sCategory & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     OR CategoryNom.Eng LIKE '" & sCategory & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     OR CategoryNom.Por LIKE '" & sCategory & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     ) ")
            sb.AppendLine("AND (SkuNom.Esp LIKE '" & sNom & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     OR SkuNom.Cat LIKE '" & sNom & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     OR SkuNom.Eng LIKE '" & sNom & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     OR SkuNom.Por LIKE '" & sNom & "' COLLATE SQL_Latin1_General_CP1_CI_AI ")
            sb.AppendLine("     ) ")
        End If
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct As New DTOProduct(oDrd("Guid"))
            With oProduct
                .sourceCod = oDrd("Cod")
                .nom.Esp = sNom
            End With
            retval.Add(oProduct)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ForSitemap(oEmp As DTOEmp) As DTOProductBrand.Collection
        Dim retval As New DTOProductBrand.Collection

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid AS BrandGuid, Tpa.FchLastEdited AS BrandFchLastEdited ")
        sb.AppendLine(", VwDeptCategories.Dept AS DeptGuid, Dept.FchLastEdited AS DeptFchLastEdited ")
        sb.AppendLine(", Stp.Guid AS CategoryGuid, Stp.FchLastEdited AS CategoryFchLastEdited ")
        sb.AppendLine(", Art.Guid AS SkuGuid, Art.FchLastEdited AS SkuFchLastEdited, Art.Hereda ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")
        sb.AppendLine(", (CASE WHEN Art.Image IS NULL THEN 0 ELSE 1 END) AS ImageExists ")
        sb.AppendLine("From Tpa ")
        sb.AppendLine("INNER JOIN Stp ON Tpa.Guid = Stp.Brand ")
        sb.AppendLine("INNER JOIN Art ON Stp.Guid = Art.Category ")
        sb.AppendLine("LEFT OUTER JOIN VwDeptCategories ON Stp.Guid = VwDeptCategories.Category ")
        sb.AppendLine("LEFT OUTER JOIN Dept ON VwDeptCategories.Dept = Dept.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")
        sb.AppendLine("WHERE Tpa.Obsoleto=0 AND Stp.Obsoleto=0 AND Art.Obsoleto=0 ")
        sb.AppendLine("AND Stp.WEB_ENABLED_CONSUMER=1 And Art.NoWeb=0 And Art.NoStk=0 ")
        sb.AppendLine("ORDER BY Url.UrlBrandEsp, Url.UrlDeptEsp, Url.UrlCategoryEsp, Url.UrlSkuEsp")

        Dim SQL As String = sb.ToString
        Dim oBrand As New DTOProductBrand
        Dim oDept As New DTODept
        Dim oCategory As New DTOProductCategory
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("BrandGuid").Equals(oBrand.Guid) Then
                oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                With oBrand
                    .fchLastEdited = oDrd("BrandFchLastEdited")
                    .UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                End With
                retval.Add(oBrand)
            End If
            If Not oDrd("DeptGuid").Equals(oDept.Guid) AndAlso SQLHelper.GetBooleanFromDatareader(oDrd("IncludeDeptOnUrl")) = True Then
                oDept = New DTODept(oDrd("DeptGuid"))
                With oDept
                    .UsrLog = DTOUsrLog.Factory()
                    .UsrLog.fchLastEdited = oDrd("DeptFchLastEdited")
                    .UrlCanonicas = SQLHelper.GetProductCategoryUrlCanonicasFromDataReader(oDrd)
                End With
                oBrand.Depts.Add(oDept)
            End If

            If Not oDrd("CategoryGuid").Equals(oCategory.Guid) Then
                oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                With oCategory
                    .fchLastEdited = oDrd("CategoryFchLastEdited")
                    .UrlCanonicas = SQLHelper.GetProductCategoryUrlCanonicasFromDataReader(oDrd)
                End With

                If SQLHelper.GetBooleanFromDatareader(oDrd("IncludeDeptOnUrl")) Then
                    oDept.Categories.Add(oCategory)
                Else
                    oBrand.Categories.Add(oCategory)
                End If
            End If

            If Not SQLHelper.GetBooleanFromDatareader(oDrd("Hereda")) Then
                Dim oSku As New DTOProductSku(oDrd("SkuGuid"))
                With oSku
                    .fchLastEdited = oDrd("SkuFchLastEdited")
                    .imageExists = (oDrd("ImageExists") = 1)
                    .UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                End With
                oCategory.Skus.Add(oSku)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromCnap(oCnap As DTOCnap) As List(Of DTOProduct)
        Dim retval As New List(Of DTOProduct)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT X.Guid, X.SrcCod ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("FROM ")
        sb.AppendLine("(")
        sb.AppendLine("SELECT CnapGuid, Guid FROM Tpa ")
        sb.AppendLine("SELECT CnapGuid, Guid FROM Stp ")
        sb.AppendLine("SELECT CnapGuid, Guid FROM Art ")
        sb.AppendLine(") AS X ")
        sb.AppendLine("INNER JOIN VwProductNom ON X.Guid = VwProductNom.Guid ")
        sb.AppendLine("WHERE X.CnapGuid = '" & oCnap.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY X.Guid, X.SrcCod")
        sb.AppendLine(", VwProductNom.Cod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct As DTOProduct = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("SkuGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
            retval.Add(oProduct)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function FromCnap(sKeyword As String) As List(Of DTOProduct)

        Dim retval As New List(Of DTOProduct)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT X.Guid, X.SrcCod ")
        sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        sb.AppendLine("FROM ")
        sb.AppendLine("(")
        sb.AppendLine("SELECT CnapGuid, Guid FROM Tpa ")
        sb.AppendLine("SELECT CnapGuid, Guid FROM Stp ")
        sb.AppendLine("SELECT CnapGuid, Guid FROM Art ")
        sb.AppendLine(") AS X ")
        sb.AppendLine("INNER JOIN VwProductNom ON X.Guid = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN VwCnap ON X.CnapGuid = VwCnap.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Keyword on VwCnap.guid=Keyword.Target ")
        sb.AppendLine("WHERE (VwCnap.ShortNomEsp like @Keyword or Keyword.value like @Keyword) ")
        sb.AppendLine("GROUP BY X.Guid, X.SrcCod")
        sb.AppendLine(", VwProductNom.Cod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct As DTOProduct = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("SkuGuid"), oDrd("SkuNom"), oDrd("SkuNomLlarg"))
            retval.Add(oProduct)
        Loop
        oDrd.Close()
        Return retval

    End Function

End Class
