@ModelType DTOSellOut

@Code
    Dim items As List(Of DTOSelloutItem) = DTOSellOut.FlattenItems(Model)
    Model.ExpandToLevel = 0
    If Model.Items.Where(Function(x) x.Level = 0).Count = 1 Then
        Model.ExpandToLevel = 1
    End If
End Code


<span class="Selloutrow">
    <span class="SelloutConcept truncate">

    </span>
    <span class="SelloutValue">
        total
    </span>
    @For m As Integer = 1 To 12
        @<span class="SelloutValue">
            @DTOLang.MesAbr(Model.Lang, m)
        </span>
    Next
</span>



@For Each oItem As DTOSelloutItem In items
    @<div data-parent="@oItem.Parent.ToString" @IIf(oItem.Level > Model.ExpandToLevel, "hidden='hidden'", "")>

        <span class='Selloutrow @Html.Raw("rowlevel" & oItem.Level.ToString())'>
            <span class="SelloutConcept truncate" data-level="@oItem.Level" title="@oItem.Concept">
                @For i As Integer = 0 To oItem.Level
            @Html.Raw("&nbsp;&nbsp;&nbsp;&nbsp;")
                Next
                @If oItem.HasChildren Then
                    If oItem.Level < Model.ExpandToLevel Then
                        @<a href="#" class="drilldown" data-rowindex="@oItem.Index.ToString" data-expanded="1" data-guid="@oItem.Key.ToString">
                            <img src="~/Media/Img/Ico/collapse9.png" />
                        </a>
                    Else
                        @<a href="#" class="drilldown" data-rowindex="@oItem.Index.ToString" data-expanded="0" data-guid="@oItem.Key.ToString">
                            <img src="~/Media/Img/Ico/expand9.png" />
                        </a>
                    End If
                Else
            @Html.Raw("&nbsp;&nbsp")
                End If
                @oItem.Concept
            </span>

                <span class="SelloutValue">
                    @Html.Raw(Format(oItem.Tot, "#,###"))
                </span>
                @For m As Integer = 0 To 11
                @<span class="SelloutValue">
                    @If oItem.Values(m) <> 0 Then
                        @Html.Raw(Format(oItem.Values(m), "#,###"))
                    Else
                        @Html.Raw("&nbsp;")
                    End If
                </span>
                Next
        </span>


    </div>
Next