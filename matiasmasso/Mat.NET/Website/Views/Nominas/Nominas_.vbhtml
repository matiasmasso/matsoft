@Modeltype DTONomina.Header.Collection

<div class="Grid-container">
    @For Each item In Model
        @<div class="Grid-item">
            <a href="@item.DownloadUrl()" target="_blank">
                <img src="@item.ThumbnailUrl" width="@DTODocFile.THUMB_WIDTH" height="@DTODocFile.THUMB_HEIGHT" />
                <div class="CellFch">
                    @Format(item.Fch, "dd/MM/yy")
                </div>
            </a>

        </div>
    Next
</div>




