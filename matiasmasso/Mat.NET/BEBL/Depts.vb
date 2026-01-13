Public Class Dept

    Shared Function Find(oGuid As Guid) As DTODept
        Dim retval As DTODept = DeptLoader.Find(oGuid)
        'Dim oCategories = BEBL.ProductCategories.All(retval.Brand)

        'Dim oCnaps = BEBL.Cnaps.All()
        'For Each oCnap In oCnaps.Where(Function(x) x.Parent IsNot Nothing)
        'Dim oParentGuid = oCnap.Parent.Guid
        'oCnap.Parent = oCnaps.FirstOrDefault(Function(x) x.Guid.Equals(oParentGuid))
        'Next

        'For Each oCategory In oCategories
        'If oCategory.cNap IsNot Nothing Then
        'oCategory.cNap = oCnaps.FirstOrDefault(Function(x) x.Equals(oCategory.cNap))
        'If retval.cnaps.Any(Function(x) oCategory.cNap.IsSelfOrChildOf(x)) Then
        'retval.Categories.Add(oCategory)
        'End If
        'End If
        'Next

        Return retval
    End Function

    Shared Function FromNom(oBrand As DTOProductBrand, src As String) As DTODept
        Return DeptLoader.FromNom(oBrand, src)
    End Function

    Shared Function Banner(oDept As DTODept) As Byte()
        Return DeptLoader.Banner(oDept)
    End Function

    Shared Function Update(oDept As DTODept, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DeptLoader.Update(oDept, exs)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Depts)
        Return retval
    End Function

    Shared Function Delete(oDept As DTODept, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = DeptLoader.Delete(oDept, exs)
        Return retval
    End Function



    Shared Function Categories(oDept As DTODept) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        Dim oBrandCategories = BEBL.ProductCategories.All(oDept.Brand).Where(Function(x) x.CNap IsNot Nothing).ToList
        Dim oCnaps = BEBL.Cnaps.All()
        For Each oCnap In oCnaps.Where(Function(x) x.Parent IsNot Nothing)
            Dim oParentGuid = oCnap.Parent.Guid
            oCnap.Parent = oCnaps.FirstOrDefault(Function(x) x.Guid.Equals(oParentGuid))
        Next
        For Each oCategory In oBrandCategories
            oCategory.CNap = oCnaps.FirstOrDefault(Function(x) x.Equals(oCategory.CNap))
            If oDept.CNaps.Any(Function(x) oCategory.CNap.IsChildOf(x)) Then
                retval.Add(oCategory)
            End If
        Next

        Return retval
    End Function

End Class


Public Class Depts
    Shared Function All(Optional oBrand As DTOProductBrand = Nothing) As List(Of DTODept)
        Return DeptsLoader.All(oBrand)
    End Function

    Shared Function AllWithFilters(oBrand As DTOProductBrand) As List(Of DTODept)
        Dim retval = DeptsLoader.All(oBrand)

        Dim oCategoriesWithFilterItems = BEBL.ProductCategories.AllWithFilterItems(oBrand)
        Dim oCategories = oCategoriesWithFilterItems.Where(Function(x) x.cNap IsNot Nothing).ToList

        Dim oCnaps = BEBL.Cnaps.All()
        For Each oCnap In oCnaps.Where(Function(x) x.Parent IsNot Nothing)
            Dim oParentGuid = oCnap.Parent.Guid
            oCnap.Parent = oCnaps.FirstOrDefault(Function(x) x.Guid.Equals(oParentGuid))
        Next

        For Each oCategory In oCategories
            Dim oCnap = oCnaps.FirstOrDefault(Function(x) x.Equals(oCategory.cNap))
            If oCnap IsNot Nothing Then
                oCategory.cNap = oCnap
                For Each oDept In retval
                    If oDept.cnaps.Any(Function(x) oCategory.cNap.IsSelfOrChildOf(x)) Then
                        oDept.Categories.Add(oCategory)
                    End If
                Next
            End If
        Next

        Return retval
    End Function

    Shared Function BrandDeptsMenuItems() As List(Of DTOMenu)
        Return DeptsLoader.BrandDeptsMenuItems()
    End Function

    Shared Function Headers(oBrand As DTOProductBrand) As List(Of DTODept)
        Return DeptsLoader.Headers(oBrand)
    End Function

    Shared Function Sprite(oBrand As DTOProductBrand) As Byte()
        Dim oImages = DeptsLoader.Sprite(oBrand)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oImages, DTODept.IMAGEWIDTH, DTODept.IMAGEHEIGHT)
        Return retval
    End Function

    Shared Function Swap(exs As List(Of Exception), dept1 As DTODept, dept2 As DTODept) As Boolean
        Return DeptsLoader.swap(exs, dept1, dept2)
    End Function

End Class
