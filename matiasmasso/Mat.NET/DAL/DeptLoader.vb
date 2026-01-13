Public Class DeptLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTODept
        Dim retval As DTODept = Nothing
        Dim oDept As New DTODept(oGuid)
        If Load(oDept) Then
            retval = oDept
        End If
        Return retval
    End Function


    Shared Function Banner(oDept As DTODept) As Byte()
        Dim retval As Byte() = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Dept.Banner ")
        sb.AppendLine("FROM Dept ")
        sb.AppendLine("WHERE Dept.Guid='" & oDept.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval = oDrd("Banner")
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromNom(oBrand As DTOProductBrand, src As String) As DTODept
        Dim retval As DTODept = Nothing
        src = src.Replace("_", " ")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Dept.Guid ")
        sb.AppendLine("FROM Dept ")
        sb.AppendLine("INNER JOIN VwLangText DeptNom ON Dept.Guid = DeptNom.Guid AND DeptNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("WHERE Dept.Brand='" & oBrand.Guid.ToString & "' AND ( ")
        sb.AppendLine("   CAST(DeptNom.Esp AS VARCHAR) = '" & src & "' COLLATE Latin1_general_CI_AI  ")
        sb.AppendLine("OR CAST(DeptNom.Cat AS VARCHAR) = '" & src & "' COLLATE Latin1_general_CI_AI  ")
        sb.AppendLine("OR CAST(DeptNom.Eng AS VARCHAR) = '" & src & "' COLLATE Latin1_general_CI_AI  ")
        sb.AppendLine("OR CAST(DeptNom.Por AS VARCHAR) = '" & src & "' COLLATE Latin1_general_CI_AI  ")
        sb.AppendLine(") ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTODept(oDrd("Guid"))
        End If
        oDrd.Close()
        If retval IsNot Nothing Then
            Load(retval)
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oDept As DTODept) As Boolean
        If Not oDept.IsLoaded And Not oDept.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Dept.Brand, BrandNom.Esp AS BrandNom ")
            sb.AppendLine(", BrandNom.Esp AS BrandNomEsp, BrandNom.Cat AS BrandNomCat, BrandNom.Eng AS BrandNomEng, BrandNom.Por AS BrandNomPor ")
            sb.AppendLine(", DeptNom.Esp AS DeptNomEsp, DeptNom.Cat AS DeptNomCat, DeptNom.Eng AS DeptNomEng, DeptNom.Por AS DeptNomPor ")
            sb.AppendLine(", SeoTitle.Esp AS SeoTitleEsp, SeoTitle.Cat AS SeoTitleCat, SeoTitle.Eng AS SeoTitleEng, SeoTitle.Por AS SeoTitlePor ")
            sb.AppendLine(", DeptExcerpt.Esp AS DeptExcerptEsp, DeptExcerpt.Cat AS DeptExcerptCat, DeptExcerpt.Eng AS DeptExcerptEng, DeptExcerpt.Por AS DeptExcerptPor ")
            sb.AppendLine(", DeptContent.Esp AS DeptContentEsp, DeptContent.Cat AS DeptContentCat, DeptContent.Eng AS DeptContentEng, DeptContent.Por AS DeptContentPor ")
            sb.AppendLine(", Dept.Ord ")
            sb.AppendLine(", DeptCnap.Dept, DeptCnap.Cnap, VwCnap.Id AS CnapId ")
            sb.AppendLine(", VwCnap.LongNomEsp AS CnapEsp, VwCnap.LongNomCat AS CnapCat, VwCnap.LongNomEng AS CnapEng, VwCnap.LongNomPor AS CnapPor ")
            sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor ")
            sb.AppendLine("FROM Dept ")
            sb.AppendLine("INNER JOIN Tpa ON Dept.Brand = Tpa.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText BrandNom ON Dept.Brand = BrandNom.Guid AND BrandNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText DeptNom ON Dept.Guid = DeptNom.Guid AND DeptNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText SeoTitle ON Dept.Guid = SeoTitle.Guid AND SeoTitle.Src = " & DTOLangText.Srcs.SeoTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText DeptExcerpt ON Dept.Guid = DeptExcerpt.Guid AND DeptExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText DeptContent ON Dept.Guid = DeptContent.Guid AND DeptContent.Src = " & DTOLangText.Srcs.ProductText & " ")
            sb.AppendLine("LEFT OUTER JOIN DeptCnap ON Dept.Guid = DeptCnap.Dept ")
            sb.AppendLine("LEFT OUTER JOIN VwCnap ON DeptCnap.Cnap = VwCnap.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Dept.Guid = Url.Guid ")
            sb.AppendLine("WHERE Dept.Guid='" & oDept.Guid.ToString & "' ")

            Dim oBrand As New DTOProductBrand
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                If Not oBrand.Guid.Equals(oDrd("Brand")) Then
                    oBrand = New DTOProductBrand(oDrd("Brand"))
                    SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNomEsp", "BrandNomCat", "BrandNomEng", "BrandNomPor")
                End If
                If Not oDept.IsLoaded Then
                    With oDept
                        .Brand = oBrand
                        .Brand.UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                        SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "DeptNomEsp", "DeptNomCat", "DeptNomEng", "DeptNomPor")
                        SQLHelper.LoadLangTextFromDataReader(.SeoTitle, oDrd, "SeoTitleEsp", "SeoTitleCat", "SeoTitleEng", "SeoTitlePor")
                        SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "DeptExcerptEsp", "DeptExcerptCat", "DeptExcerptEng", "DeptExcerptPor")
                        SQLHelper.LoadLangTextFromDataReader(.Content, oDrd, "DeptContentEsp", "DeptContentCat", "DeptContentEng", "DeptContentPor")
                        .UrlCanonicas = SQLHelper.GetProductDeptUrlCanonicasFromDataReader(oDrd)
                        'TODO: Acabar de assignar les Url
                        '.UrlSegments
                        .Ord = oDrd("Ord")
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("Cnap")) Then
                    Dim oCnap As New DTOCnap(oDrd("Cnap"))
                    With oCnap
                        .Id = oDrd("CnapId")
                        SQLHelper.LoadLangTextFromDataReader(.NomLong, oDrd, "CnapEsp", "CnapCat", "CnapEng", "CnapPor")
                    End With
                    oDept.CNaps.Add(oCnap)
                End If
            Loop

            oDrd.Close()
        End If

        oDept.Categories = Categories(oDept)
        oDept.UrlSegments = UrlSegmentsLoader.All(oDept)
        'oDept.Urls = ProductLoader.Urls(oDept)

        Dim retval As Boolean = oDept.IsLoaded
        Return retval
    End Function



    Shared Function Update(oDept As DTODept, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oDept, oTrans)
            oTrans.Commit()
            oDept.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oDept As DTODept, ByRef oTrans As SqlTransaction)
        UpdateHeader(oDept, oTrans)
        LangTextLoader.Update(oDept.Nom, oTrans)
        LangTextLoader.Update(oDept.SeoTitle, oTrans)
        LangTextLoader.Update(oDept.Excerpt, oTrans)
        LangTextLoader.Update(oDept.Content, oTrans)
        UrlSegmentsLoader.Update(oDept, oDept.UrlSegments, oTrans)
        UpdateCnaps(oDept, oTrans)
    End Sub

    Shared Sub UpdateHeader(oDept As DTODept, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Dept ")
        sb.AppendLine("WHERE Guid='" & oDept.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oDept.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oDept
            oRow("Brand") = .Brand.Guid
            oRow("Ord") = .Ord

            If .banner IsNot Nothing Then
                oRow("Banner") = .banner
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateCnaps(oDept As DTODept, ByRef oTrans As SqlTransaction)
        If Not oDept.IsNew Then DeleteCnaps(oDept, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM DeptCnap ")
        sb.AppendLine("WHERE Dept='" & oDept.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        For Each oCnap In oDept.CNaps
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Dept") = oDept.Guid
            oRow("Cnap") = oCnap.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oDept As DTODept, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oDept, oTrans)
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

    Shared Sub Delete(oDept As DTODept, ByRef oTrans As SqlTransaction)
        DeleteCnaps(oDept, oTrans)
        LangTextLoader.Delete(oDept, oTrans)
        UrlSegmentsLoader.Delete(oDept, oTrans)
        DeleteHeader(oDept, oTrans)
    End Sub


    Shared Sub DeleteHeader(oDept As DTODept, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Dept WHERE Guid='" & oDept.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteCnaps(oDept As DTODept, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE DeptCnap WHERE Dept='" & oDept.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub



    Shared Function Categories(oDept As DTODept) As List(Of DTOProductCategory)
        'TODO: Llegir les Url de les categories
        Dim retval As New List(Of DTOProductCategory)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwDeptCategories.Category, Stp.Codi, Stp.WEB_ENABLED_CONSUMER, Stp.WEB_ENABLED_PRO ")
        sb.AppendLine(", VwProductText.NomEsp, VwProductText.NomCat, VwProductText.NomEng, VwProductText.NomPor ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor ")
        sb.AppendLine(", VwFilterTarget.FilterItem ")
        sb.AppendLine("FROM VwDeptCategories ")
        sb.AppendLine("INNER JOIN Stp ON VwDeptCategories.Category = Stp.Guid ")
        sb.AppendLine("INNER JOIN VwProductText ON VwDeptCategories.Category = VwProductText.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical Url ON VwDeptCategories.Category=Url.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwFilterTarget ON Stp.Guid = VwFilterTarget.ParentProduct ")
        sb.AppendLine("WHERE VwDeptCategories.Dept='" & oDept.Guid.ToString() & "' ")
        sb.AppendLine("AND Stp.obsoleto = 0 ")
        sb.AppendLine("ORDER BY Stp.Ord, VwProductText.NomEsp, Stp.Guid ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)

        Dim oCategory As New DTOProductCategory
        Do While oDrd.Read
            If Not oCategory.Guid.Equals(oDrd("Category")) Then
                oCategory = New DTOProductCategory(oDrd("Category"))
                With oCategory
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                    .Codi = oDrd("Codi")
                    .EnabledxConsumer = oDrd("WEB_ENABLED_CONSUMER")
                    .EnabledxPro = oDrd("WEB_ENABLED_PRO")
                    .UrlCanonicas = SQLHelper.GetProductCategoryUrlCanonicasFromDataReader(oDrd)
                End With
                retval.Add(oCategory)
            End If
            If Not IsDBNull(oDrd("FilterItem")) Then
                Dim oFilterItem As New DTOFilter.Item(oDrd("FilterItem"))
                oCategory.FilterItems.Add(oFilterItem)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

#End Region

End Class

Public Class DeptsLoader

    Shared Function Cache(oEmp As DTOEmp) As List(Of DTODept)
        Dim retval As New List(Of DTODept)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Dept.Guid, Dept.Ord, Dept.Brand ")
        sb.AppendLine(", Dept.Obsoleto, Dept.FchLastEdited ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por")
        sb.AppendLine("FROM Dept ")
        sb.AppendLine("INNER JOIN Tpa ON Dept.Brand = Tpa.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON Dept.Guid = VwLangText.Guid AND VwLangText.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("WHERE Tpa.Emp = '" & oEmp.Id & "' ")
        sb.AppendLine("ORDER BY Dept.Brand, Dept.Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDept As New DTODept(oDrd("Guid"))
            oDept.Brand = New DTOProductBrand(oDrd("Brand"))
            oDept.Ord = oDrd("Ord")
            SQLHelper.LoadLangTextFromDataReader(oDept.Nom, oDrd)
            oDept.obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
            oDept.UsrLog.FchLastEdited = SQLHelper.GetFchFromDataReader(oDrd("FchLastEdited"))
            retval.Add(oDept)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Categories(oEmp As DTOEmp) As List(Of DTODeptCategory) 'For Cache
        Dim retval As New List(Of DTODeptCategory)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwDeptCategories.Dept, VwDeptCategories.Category ")
        sb.AppendLine("FROM VwDeptCategories ")
        sb.AppendLine("ORDER BY VwDeptCategories.Dept, VwDeptCategories.Category")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim item As New DTODeptCategory()
            item.Dept = oDrd("Dept")
            item.Category = oDrd("Category")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(Optional oBrand As DTOProductBrand = Nothing) As List(Of DTODept)
        Dim retval As New List(Of DTODept)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Dept.Guid, Dept.Brand, BrandNom.Esp AS BrandNom ")
        sb.AppendLine(", Dept.Ord ")
        sb.AppendLine(", DeptNom.Esp AS DeptEsp, DeptNom.Cat AS DeptCat, DeptNom.Eng AS DeptEng, DeptNom.Por AS DeptPor ")
        sb.AppendLine(", DeptCnap.Dept, DeptCnap.Cnap, VwCnap.Id AS CnapId ")
        sb.AppendLine(", VwCnap.LongNomEsp AS CnapEsp, VwCnap.LongNomCat AS CnapCat, VwCnap.LongNomEng AS CnapEng, VwCnap.LongNomPor AS CnapPor ")
        sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor ")
        sb.AppendLine("FROM Dept ")
        sb.AppendLine("INNER JOIN Tpa ON Dept.Brand = Tpa.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS DeptNom ON Dept.Guid = DeptNom.Guid AND DeptNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Dept.Guid = Url.Guid ")
        sb.AppendLine("LEFT OUTER JOIN DeptCnap ON Dept.Guid = DeptCnap.Dept ")
        sb.AppendLine("LEFT OUTER JOIN VwCnap ON DeptCnap.Cnap = VwCnap.Guid ")
        If oBrand Is Nothing Then
            oBrand = New DTOProductBrand
        Else
            sb.AppendLine("WHERE Dept.Brand = '" & oBrand.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Tpa.Ord, BrandNom.Esp, Dept.Ord ")
        Dim oDept As New DTODept()
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDept.Guid.Equals(oDrd("Guid")) Then
                If Not oBrand.Guid.Equals(oDrd("Brand")) Then
                    oBrand = New DTOProductBrand(oDrd("Brand"))
                    oBrand.UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                End If
                SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")

                oDept = New DTODept(oDrd("Guid"))
                With oDept
                    .brand = oBrand
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "DeptEsp", "DeptCat", "DeptEng", "DeptPor")
                    .UrlCanonicas = SQLHelper.GetProductDeptUrlCanonicasFromDataReader(oDrd)
                    .Ord = oDrd("Ord")
                End With
                retval.Add(oDept)
            End If

            If Not IsDBNull(oDrd("Cnap")) Then
                Dim oCnap As New DTOCnap(oDrd("Cnap"))
                With oCnap
                    .Id = oDrd("CnapId")
                    SQLHelper.LoadLangTextFromDataReader(.NomLong, oDrd, "CnapEsp", "CnapCat", "CnapEng", "CnapPor")
                End With
                oDept.cnaps.Add(oCnap)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function BrandDeptsMenuItems() As DTOMenu.Collection
        Dim retval As New DTOMenu.Collection
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid AS BrandGuid ")
        sb.AppendLine(", BrandNom.Esp AS BrandNomEsp, BrandNom.Cat AS BrandNomCat, BrandNom.Eng AS BrandNomEng, BrandNom.Por AS BrandPor ")
        sb.AppendLine(", Dept.Guid AS DeptGuid ")
        sb.AppendLine(", DeptNom.Esp AS DeptNomEsp, DeptNom.Cat AS DeptNomCat, DeptNom.Eng AS DeptNomEng, DeptNom.Por AS DeptNomPor ")

        sb.AppendLine(", BrandUrl.UrlBrandEsp, BrandUrl.UrlBrandCat, BrandUrl.UrlBrandEng, BrandUrl.UrlBrandPor ")
        sb.AppendLine(", DeptUrl.UrlDeptEsp, DeptUrl.UrlDeptCat, DeptUrl.UrlDeptEng, DeptUrl.UrlDeptPor ")

        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("LEFT OUTER JOIN Dept ON Tpa.Guid = Dept.Brand ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS DeptNom ON Dept.Guid = DeptNom.Guid AND DeptNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS BrandUrl ON Tpa.Guid = BrandUrl.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS DeptUrl ON Dept.Guid = DeptUrl.Guid ")
        sb.AppendLine("WHERE Tpa.Obsoleto = 0 AND Tpa.WEB_ENABLED_CONSUMER <> 0 ")
        sb.AppendLine("ORDER BY Tpa.Ord, BrandNom.Esp, Tpa.Guid, Dept.Ord, DeptNom.Esp, Dept.Guid ")
        Dim oBrand As New DTOProductBrand
        Dim oBrandSegment As New DTOLangText
        Dim oBrandMenuItem As DTOMenu = Nothing
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                Dim oBrandNom = SQLHelper.GetLangTextFromDataReader(oDrd, "BrandNomEsp", "BrandNomCat", "BrandNomEng", "BrandNomPor")
                oBrand.UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                oBrandMenuItem = DTOMenu.Factory(oBrandNom, oBrand.UrlCanonicas.Path)
                oBrandMenuItem.Cod = DTOMenu.Cods.Product 'important per Mvc._SideNav
                retval.Add(oBrandMenuItem)
            End If

            If Not IsDBNull(oDrd("DeptGuid")) Then
                Dim oDeptNom = SQLHelper.GetLangTextFromDataReader(oDrd, "DeptNomEsp", "DeptNomCat", "DeptNomEng", "DeptNomPor")
                Dim oDeptUrlCanonicas = SQLHelper.GetProductDeptUrlCanonicasFromDataReader(oDrd)
                oBrandMenuItem.AddChild(oDeptNom, oDeptUrlCanonicas.Path)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Headers(oBrand As DTOProductBrand) As List(Of DTODept)
        Dim retval As New List(Of DTODept)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Dept.Guid, Dept.Ord ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por")
        sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor ")
        sb.AppendLine("FROM Dept ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON Dept.Guid = VwLangText.Guid AND VwLangText.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Dept.Guid = Url.Guid ")
        sb.AppendLine("WHERE Dept.Brand = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Dept.Brand, Dept.Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oDept As New DTODept(oDrd("Guid"))
            oDept.Ord = oDrd("Ord")
            SQLHelper.LoadLangTextFromDataReader(oDept.nom, oDrd)
            oDept.UrlCanonicas = SQLHelper.GetProductDeptUrlCanonicasFromDataReader(oDrd)
            retval.Add(oDept)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Sprite(oBrand As DTOProductBrand) As List(Of Byte())
        Dim retval As New List(Of Byte())
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Banner ")
        sb.AppendLine("FROM Dept ")
        sb.AppendLine("WHERE Dept.Brand = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Dept.Ord, Dept.Brand ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oImage = oDrd("Banner")
            retval.Add(oImage)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Swap(exs As List(Of Exception), dept1 As DTODept, dept2 As DTODept) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM Dept ")
            sb.AppendLine("WHERE (Guid='" & dept1.Guid.ToString & "' OR Guid='" & dept2.Guid.ToString & "') ")
            sb.AppendLine("ORDER BY Ord")
            Dim SQL As String = sb.ToString

            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)

            If oTb.Rows.Count <> 2 Then
                exs.Add(New Exception("No s'han trobat tots dos departaments o n'hi ha de duplicats"))
            Else
                Dim idx0 = oTb.Rows(0)("Ord")
                Dim idx1 = oTb.Rows(1)("Ord")
                oTb.Rows(0)("Ord") = idx1
                oTb.Rows(1)("Ord") = idx0
            End If

            oDA.Update(oDs)

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
