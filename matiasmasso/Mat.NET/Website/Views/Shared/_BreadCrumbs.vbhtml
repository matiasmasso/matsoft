        @For Each oItem As BreadCrumbItem In ViewBag.Breadcrumb.Items
            @:<span itemscope itemtype='http://data-vocabulary.org/Breadcrumb' class="breadcrumbs">


            @If oItem.Url > "" Then
                @:<a itemprop='url' href='@oItem.Url'>
            End If

            @:<span itemprop='title'>@Html.Raw(oItem.Title)</span>
                 @If oItem.Url > "" Then
                    @:</a>
                End If
            @:</span>

        Next
