@ModelType DTOMediaResource.Model
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang = Mvc.ContextHelper.Lang

End Code

<div>
    <div class="Img-container">
        <img src="" id="ResourceImg" />
    </div>

    <form action="/pro/proCatalog/ResourceUpload" method="post" enctype="multipart/form-data">
        <input type="file" name="file" id="file" onchange="onFileSelected(event)">
        <div>
            <select class="Cod" name="Cod">
                <option value=0 selected>@lang.Tradueix("(tipo de recurso)")</option>
                <option value="@CInt(DTOMediaResource.Cods.Product)">@lang.Tradueix("Producto")</option>
                <option value="@CInt(DTOMediaResource.Cods.Features)">@lang.Tradueix("Características")</option>
                <option value="@CInt(DTOMediaResource.Cods.LifeStyle)">@lang.Tradueix("LifeStyle")</option>
                <option value="@CInt(DTOMediaResource.Cods.Logo)">@lang.Tradueix("Logo")</option>
                <option value="@CInt(DTOMediaResource.Cods.Banner)">@lang.Tradueix("Banner")</option>
                <option value="@CInt(DTOMediaResource.Cods.Video)">@lang.Tradueix("Video")</option>
            </select>
        </div>
        <div>
            <select class="Lang" name="Lang">
                <option value="" selected>@lang.Tradueix("(idioma)")</option>
                <option value="ESP">Español</option>
                <option value="POR">Portugués</option>
            </select>
        </div>
        <input type="hidden" name="ProductGuid" value="@Model.Product.Guid.ToString()" />
        <input type="hidden" name="Guid" value="@Model.Guid.ToString()" />
        <input type="submit" />
    </form>



</div>

@Section Styles
    <style>
        .Img-container {
            width: 200px;
            height: 200px;
            border: 1px solid gray;
        }

        .Img-container img {
            width:100%;
            height:auto;
        }

        input[type=file] {
            margin-top: 15px;
        }
    </style>
End Section
@Section Scripts
    <script>
        function onFileSelected(event) {
            var selectedFile = event.target.files[0];
            var reader = new FileReader();

            var imgtag = document.getElementById("ResourceImg");
            imgtag.title = selectedFile.name;

            reader.onload = function (event) {
                imgtag.src = event.target.result;
            };

            reader.readAsDataURL(selectedFile);
        }
    </script>
End Section