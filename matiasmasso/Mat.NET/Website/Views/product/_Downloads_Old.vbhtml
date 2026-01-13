@ModelType List(Of DTOProductDownload)
@Code
    
End Code


<style scoped>
    .pillWrapper {
        clear:both;
        text-align:center;
    }

    .boxDownload {
        clear: both;
        position: relative;
        display: inline-block;
        border: 1px solid darkgray;
        width: 150px;
        height: 239px;
        padding: 0px;
        background-color: lightgray;
    }

        .boxDownload a {
            color: black;
        }

        .boxDownload:hover {
            background-color: red;
            color: white;
        }
    .productDownloadTitle {
        position: absolute;
        top: 0px;
        left: 0px;
        width: 148px;
        height: 58px;
        overflow: hidden;
        padding: 5px 5px;
        /*
        float: left;
        margin-top: -20px;
        margin-left: 25px;
            */
        font-size: 0.8em;
    }



        .productDownloadImgContainer {
        position: absolute;
        top:58px;
        left:22px;
        width: 126px;
        height:100%;
        overflow:hidden;
    }
        .boxDownload img {
        width: 100%;
    }
    .boxFeatures {
        position: absolute;
        bottom:79px; /*cap amunt*/
        right: 48px; /*cap a l'esquerra*/
        height: 23px;
        font-size: 0.8em;
        overflow: hidden;
        width: 180px; /*alçada un cop girat*/
        /*
        margin-left: -100px;
        margin-top: 96px;
            */
        padding-left: 5px;
        background-color: gray;
        color: white;
        -webkit-transform: rotate(-90deg);
        -moz-transform: rotate(-90deg);
        -ms-transform: rotate(-90deg);
        -o-transform: rotate(-90deg);
        transform: rotate(-90deg);
        /* also accepts left, right, top, bottom coordinates; not required, but a good idea for styling */
        -webkit-transform-origin: 50% 50%;
        -moz-transform-origin: 50% 50%;
        -ms-transform-origin: 50% 50%;
        -o-transform-origin: 50% 50%;
        transform-origin: 50% 50%;
        /* Should be unset in IE9+ I think. */
        filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);
    }



</style>

<div class="pillWrapper">
    @For Each oDownload As DTOProductDownload In Model
        Select Case oDownload.Src
            Case DTOProductDownload.Srcs.Catalogos, DTOProductDownload.Srcs.Compatibilidad, DTOProductDownload.Srcs.Instrucciones

                @<div class="boxDownload">
                     <a href="@FEB.DocFile.DownloadUrl(oDownload.DocFile, False)">
                         <div class="productDownloadTitle">
                             @oDownload.DocFile.Nom
                         </div>


                         <div class="productDownloadImgContainer">
                             <img src="@FEB.DocFile.ThumbnailUrl(oDownload.DocFile)" width='150px' height='auto' alt="@oDownload.DocFile.Nom" width="100%">
                         </div>

                         <div class="boxFeatures">
                             @DTODocFile.Features(oDownload.DocFile, True)
                         </div>
                     </a>
                </div>
        End Select
    Next
</div>

