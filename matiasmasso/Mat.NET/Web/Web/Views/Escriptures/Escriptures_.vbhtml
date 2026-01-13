@ModelType List(Of DTOEscriptura)

@For Each item As DTOEscriptura In Model
@<div class="Row truncate">
     <div class="Cell W70">@Format(item.FchFrom, "dd/MM/yy")</div>
     <div class="Cell Pdf">
         <a href="@FEB2.DocFile.DownloadUrl(item.DocFile, False)" title="pdf">
             <img src="/Media/Img/Ico/pdf.gif" alt="pdf" />
         </a>
     </div>
     <div class="Cell W60 truncate">@item.Codi.ToString</div>
     @item.Nom
</div>
Next
    
<style scoped>
    .Row {
        height:30px;
    }
</style>

