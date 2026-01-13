@ModelType DTOAeatModel.Collection
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim oUser = Mvc.ContextHelper.GetUser
End Code


<h1>@ViewBag.Title</h1>

@For Each item In Model.WithDocs()
    @<a href="@item.Url(oUser)">
        <div Class="Cell">
            <div>@item.Nom @item.Dsc</div>
            <div>@item.Contents(lang)</div>
        </div>
    </a>

Next


@Section Styles
    <style>
        .Cell {
            padding: 10px 0 10px 0;
            border-bottom: 1px solid grey;
        }
        .Cell div:nth-child(2) {
            color: grey;
        }
    </style>
End Section
