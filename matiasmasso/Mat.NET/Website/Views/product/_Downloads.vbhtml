@ModelType List(Of DTOProductDownload)

<div class="Downloads">
    @For Each oDownload As DTOProductDownload In Model
        Select Case oDownload.src
            Case DTOProductDownload.Srcs.catalogos, DTOProductDownload.Srcs.compatibilidad, DTOProductDownload.Srcs.instrucciones

                @<div class="Item">
                     <a href="@FEB.DocFile.DownloadUrl(oDownload.docFile, False)">
                         <div class="Nom">
                             @oDownload.docFile.nom
                         </div>


                         <div class="Thumbnail">
                             <img src="@oDownload.docFile.ThumbnailUrl()" width='150px' height='auto' alt="@oDownload.docFile.nom" width="100%">
                         </div>

                         <div class="Features">
                             @DTODocFile.features(oDownload.docFile, True)
                         </div>
                     </a>
                </div>
        End Select
    Next
</div>

