@ModelType DTOProduct.BreadcrumbViewModel


<div Class="Breadcrumbs">
    @If Model.BrandNom > "" Then
        If Model.BrandUrl = "" Then
            @<span>@Model.BrandNom</span>
        Else
            @<a href="@Model.BrandUrl">@Model.BrandNom</a>
        End If
    End If

    @If Model.DeptNom > "" Then
        If Model.DeptUrl = "" Then
            @<span>@Model.DeptNom</span>
        Else
            @<a href="@Model.DeptUrl">@Model.DeptNom</a>
        End If
    End If

    @If Model.CategoryNom > "" Then
        If Model.CategoryUrl = "" Then
            @<span>@Model.CategoryNom</span>
        Else
            @<a href="@Model.CategoryUrl">@Model.CategoryNom</a>
        End If
    End If

    @If Model.SkuNom > "" Then
        If Model.SkuUrl = "" Then
            @<span>@Model.SkuNom</span>
        Else
            @<a href="@Model.SkuUrl">@Model.SkuNom</a>
        End If
    End If

    @If Model.Subtiltle > "" Then
        @<span>@Model.Subtiltle</span>
    End If
</div>
