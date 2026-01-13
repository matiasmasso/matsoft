@ModelType DTORaffle.HeadersModel.Item


<div class="Item Active">

    <a href="@Model.PlayUrl()" title="@Model.Title" class="Image-container">
        <img class="Img178" src="@Model.ThumbnailUrl()" width="178" height="125" alt="@Model.Title" />
        <img class="Img600" src="@Model.BannerUrl()" width="600" height="200" alt="@Model.Title" hidden="hidden" />
    </a>


    <div class="Text-container">
        <a href="@Model.PlayUrl()" title="@Model.Title" class="Image-container">
            <div class="Caption">
                @Model.Title
            </div>
        </a>
        
        <div>
            <a href="@Model.PlayUrl()" title="@Model.Title">
                <div class="Properties">
                    <div>
                        @ContextHelper.Tradueix("fecha de inicio:", "data de inici:", "start up date:", "Data de inicio;")
                    </div>
                    <div>
                        @Model.FchFrom.ToString("dd/MM/yy")
                    </div>
                    <div>
                        @ContextHelper.Tradueix("fecha de sorteo:", "data del sorteig:", "end date:", "Data do sorteio:")
                    </div>
                    <div>
                        @Model.FchTo.ToString("dd/MM/yy")
                    </div>
                </div>
            </a>

                <div class="ActionCall">
                    <div>
                        <a href="@Model.PlayUrl()" title="@Model.Title">
                            @ContextHelper.Tradueix("¡participa, aún estás a tiempo!", "participa, encara hi ets a temps!", "join us, you are still on time!", "Participa, ainda vais a tempo!")
                        </a>
                    </div>

                    <div class="ShareButtons">
                        <a class="ShareIt" href="#" title="@Model.Title" data-guid="@Model.Guid.ToString()">
                            <img src="~/Media/Img/Ico/share20.png" alt="@ContextHelper.Tradueix("Compartir", "Compartir", "Share", "Partilha")" width="20" height="20" />
                            <span>@ContextHelper.Tradueix("Compártelo", "Comparteix-ho", "Share it!", "Partilha")</span>
                        </a>
                        <a href="@Model.PlayUrl()" title="@Model.Title" class="ShareItText">@ContextHelper.Tradueix("Participa", "Participa", "Join", "Participa")</a>

                        <div class="ShareButtonsModal" data-guid='@Model.Guid.ToString()' hidden="hidden">
                            <a class="CloseModal" title='@ContextHelper.Tradueix("Cerrar", "Tancar", "Close", "Feche")'>
                                <img src="~/Media/Img/Ico/Cross24.png" width="24" height="24" alt='@ContextHelper.Tradueix("Cerrar", "Tancar", "Close", "Feche")' />
                            </a>
                            <a class="CopyLink" href="#">
                                @ContextHelper.Tradueix("Copiar enlace", "Copiar l'enllaç", "Copy link")
                            </a>
                            <a class="EmailMe" href="#">
                                @ContextHelper.Tradueix("Email")
                            </a>
                        </div>
                    </div>

                    <hr />
                </div>
        </div>



        </a>
    </div>
</div>



