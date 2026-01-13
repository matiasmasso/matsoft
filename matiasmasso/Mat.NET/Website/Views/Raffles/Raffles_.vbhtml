@ModelType DTORaffle.HeadersModel

@For Each item In Model.DueItems()
    @<div class="Item">
        <a href="@item.ZoomUrl()" class="Image-container" title="@item.Title">
             <div class="Image-container">
                 <img Class="Img178" src="@item.ThumbnailUrl()" width="178" height="125" alt="@item.Title" />
                 <img Class="Img600" src="@item.BannerUrl()" width="600" height="200" alt="@item.Title" hidden="hidden" />
             </div>
        </a>

        <a href="@item.ZoomUrl()" class="Item" title="@item.Title">
            <div class="Text-container">
                <div class="Caption">
                    @item.Title
                </div>

                <div class="Properties">
                    <div>
                        @ContextHelper.Tradueix("fecha de inicio:", "data de inici:", "start up date:", "Data de inicio;")
                    </div>
                    <div>
                        @item.FchFrom.ToString("dd/MM/yy")
                    </div>
                    <div>
                        @ContextHelper.Tradueix("fecha de sorteo:", "data del sorteig:", "end date:", "Data do sorteio:")
                    </div>
                    <div>
                        @item.FchTo.ToString("dd/MM/yy")
                    </div>
                    <div>
                        @ContextHelper.Tradueix("ganador:", "guanyador:", "winner:", "ganhador:")
                    </div>
                    <div>
                        @item.Winner
                    </div>
                </div>
            </div>
        </a>
    </div>
Next
