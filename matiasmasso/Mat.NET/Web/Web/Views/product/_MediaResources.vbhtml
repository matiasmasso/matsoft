@ModelType DTOProduct
@Code
    
    Dim exs As New List(Of Exception)
    Dim oProduct As DTOProduct = Model
    Dim items As List(Of DTOMediaResource) = FEB2.MediaResources.
AllSync(exs, oProduct).
Where(Function(x) x.Obsolet = False And x.Product.Obsoleto = False And (x.Lang Is Nothing OrElse x.Lang.Equals(Mvc.ContextHelper.lang()))).
ToList
    Dim oSections As New List(Of List(Of DTOMediaResource))
    Dim oSection As New List(Of DTOMediaResource)
    oSections.Add(oSection)
    For Each item In items
        If oSection.Count > 0 Then
            If oSection.First.Cod <> item.Cod Then
                oSection = New List(Of DTOMediaResource)
                oSections.Add(oSection)
            End If
        End If
        oSection.Add(item)
    Next
End Code

<style>

    .BoxCollectionItem {
        display: inline-block;
        width: 140px;
        height: 180px;
        border: 1px solid gray;
        background-color: darkgray;
        margin-bottom:5px;
    }

        .BoxCollectionItem:hover {
            background-color: red;
        }

        .BoxCollectionItem a {
            text-decoration: none;
        }


        .BoxCollectionItem img {
            height: 140px;
            width: auto;
            max-width: 100%;
        }

        .BoxCollectionItem div {
            color: white;
            text-align: center;
            padding: 2px 4px;
            margin: 0;
            width: 100%;
            height: 30px;
            font-size: 0.6em;
        }


        .categoryHeader {
            margin-top:10px;
            margin-bottom:5px;
        }

        .categoryBody {
            height: 185px;
            overflow: hidden;
        }

        .showmore, .showless {
            border-bottom: 1px solid gray;
            margin-left:150px;
            text-align:right;
        }

</style>

<div>
    <!--
    @If Not TypeOf oProduct Is DTOProductBrand Then
     @<a href = "@FEB2.MediaResources.DownloadAllUrl(oProduct)" >@FEB2.MediaResources.DownloadAllText(items, Mvc.ContextHelper.lang())</a>
    End If
        -->
</div>

<div class="page-wrapper mediaResources">
    @If oSection.Count = 0 Then
        @<div>
            @Mvc.ContextHelper.Tradueix("No se han encontrado imágenes disponibles", "No s'han trobat imatges disponibles", "No images available", "Não há imagens disponíveis")
    </div>
    Else

        For Each oSection In oSections

    @<div Class="categoryHeader">
        @DTOMediaResource.CodTitle(oSection.First, Mvc.ContextHelper.lang())
     </div>

    @<div Class="categoryBody" data-cod="@oSection.First.Cod">
        @For Each item As DTOMediaResource In oSection
            @<div class="BoxCollectionItem">
                <a href="@FEB2.MediaResource.Url(item)">
                    <img src="@FEB2.MediaResource.ThumbnailUrl(item)" />
                    <div class="truncate">
                        @item.Nom
                        <br/>
                        @DTODocFile.Features(item, True)
                   </div>
                </a>
            </div>  
        Next
    </div>

    @<div Class="showmore" data-cod="@oSection.First.Cod">
        <a href="#" data-cod="@oSection.First.Cod">mostrar más</a>
    </div>
    @<div Class="showless" hidden ="hidden" data-cod="@oSection.First.Cod">
        <a href="#" data-cod="@oSection.First.Cod">mostrar menos</a>
    </div>      Next
    End If

</div>

