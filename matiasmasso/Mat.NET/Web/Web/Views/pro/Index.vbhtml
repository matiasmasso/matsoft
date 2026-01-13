@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)

    Dim DtFchTo As Date = New Date(Today.Year, Today.Month, 1).AddMonths(1).AddDays(-1)
    Dim DtFchFrom As Date = DtFchTo.AddDays(1).AddMonths(-12)

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim showGraph = FEB2.PurchaseOrders.ExistsSync(exs, oUser, DtFchFrom, DtFchTo)
    Dim url = MmoUrl.image(DTO.Defaults.ImgTypes.salesgrafic, oUser.Guid)
    Dim oGroups = FEB2.WebMenuGroups.AllSync(exs, oUser, JustActiveItems:=True)
End Code


    @If showGraph Then
        @<div class="ProGraph">
            <a href="/diari">
                <img src="@url" />
            </a>
        </div>
    End If

    <div class="MenuMap">
        @For Each oMenuGroup In oGroups
            @<div class="MenuGroup">
                <span class="Caption">@oMenuGroup.LangText.Tradueix(Mvc.ContextHelper.Lang)</span>
                @For Each oMenu In oMenuGroup.Items
                    @<a class="MenuItem" href="@oMenu.Url">@oMenu.LangText.Tradueix(Mvc.ContextHelper.Lang)</a>
                Next
            </div>
        Next
    </div>


@Section Styles
    <style scoped>
        .ContentColumn {
            max-width: 600px;
            margin: 0 auto;
        }

        .ProGraph {
            display: flex;
            justify-content: center;
        }

            .ProGraph img {
                width: 100%;
            }

        .MenuMap {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            column-gap: 10px;
        }

        .MenuGroup {
            display: inline-flex;
            width: 250px;
            flex-direction: column;
            margin-top: 15px;
        }

            .MenuGroup span.Caption {
                display: inline-block;
                font-weight: 600;
                padding: 20px 0 10px 0;
            }

            .MenuGroup a.MenuItem {
                display: block;
            }

                .MenuGroup a.MenuItem:hover {
                    color: #167ac6;
                }


        @@media screen and (max-width:400px) {
            .MenuMap {
                display: flex;
                flex-direction: column;
            }

            .MenuGroup {
                width: 100%;
            }

                .MenuGroup .Caption {
                    width: 100%;
                    text-align: center;
                }

                .MenuGroup a.MenuItem {
                    padding: 10px 0;
                    border-bottom: 1px solid lightgray;
                }
        }
    </style>
End Section
