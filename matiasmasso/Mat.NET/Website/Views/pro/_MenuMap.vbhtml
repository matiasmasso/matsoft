@Code
    Dim exs As New List(Of Exception)
    Dim oUser = ContextHelper.FindUserSync()
    Dim oGroups = FEB.WebMenuGroups.AllSync(exs, oUser, JustActiveItems:=True)
    End Code

<style>

    .webmenugroups {
        max-width:500px;
        margin:auto;
    }
    .webmenugroups div {
        vertical-align:top;
        margin-top:20px;
    }
        .webmenugroups div {
       float:left;
        width: 250px;
        padding:0 0 10px 10px;
    }

        .webmenugroups span {
            display: block;
            font-weight: 800;
            margin-bottom:5px;
        }

    .webmenugroups a {
        display: block;
        padding-left: 0;
    }
    .webmenugroups a:hover {
        color:red;
    }
</style>

<div class="webmenugroups">
    @For Each oGroup As DTOWebMenuGroup In oGroups
        @<div class="webmenugroup">
            <span>@DTOWebMenuGroup.Nom(oGroup, ContextHelper.lang())</span>
            @For Each oItem As DTOWebMenuItem In oGroup.Items.FindAll(Function(x) x.Actiu = True)
                @<a href='@oItem.Url'>
                    @DTOWebMenuItem.Nom(oItem, ContextHelper.lang())
                </a>
            Next
        </div>
    Next
</div>
