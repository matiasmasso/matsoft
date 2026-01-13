@ModelType List(Of DTODept)

@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    
End Code


<div class="DeptBoxes">
    @For Each oDept In Model
        @<a href="@oDept.GetUrl(Mvc.ContextHelper.Lang)">
            <div class="DeptBox">
                <img src="@FEB2.Dept.BannerUrl(oDept)" />
                <div class="DeptCaption">
                    @oDept.Nom.Tradueix(Mvc.ContextHelper.Lang())
                </div>
            </div>
        </a>
    Next
</div>

<style>
    .DeptBoxes {
        clear: both;
        width:230px;
        margin:auto;
    }

    .DeptBox {
        display: inline-block;
        width: 320px;
        height: 150px;
        border: 1px solid gray;
    }

        .DeptBox img {
            max-width: 100%;
        }
</style>

