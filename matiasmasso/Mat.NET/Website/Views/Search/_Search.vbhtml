@ModelType DTOSearchRequest
@Code
    
End Code



<div class="pagewrapper">
    <div class="Grid">
        <div class="RowHdr">
            <div class="CellTxt">
                @DTOSearchRequest.FoundCaption(Model)
            </div>
        </div>

        @For Each oSearchResult As DTOSearchRequest.Result In Model.Results
            @<div class="Row ">
                <div class="SingleCell"> <!--important per IOS, si posem CellTxt es contrau-->
                    <a href="@oSearchResult.Url">
                        @oSearchResult.Caption
                    </a>
                </div>
            </div>
        Next
    </div>
</div>





