@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
End Code

@Using (Html.BeginForm("FileUpload", "UploadInbox"))
    @Html.AntiForgeryToken()
    @<div class="wrapper">
         <input type="button" id="loadFileXml" value="loadXml" onclick="document.getElementById('files').click();" />
         <input type="file" name="files" id="files" style="display:none;">
    <input type="text" id="fileLabel"/>
         <input type="submit" value="Enviar">
         <div class="progress progress-striped">
             <div Class="progress-bar progress-bar-success">0%</div>
         </div>
    </div>
End Using

@section styles
    <style>
        .wrapper {
            margin: 20px 20px 20px 20px;
            width: 300px;
        }
        .wrapper input {
            margin-top: 20px;
        }

        .progress {
            margin-top: 20px;
        }
    </style>
End Section

@section Scripts
    <script src="http://malsup.github.com/jquery.form.js"></script>
<script>
    document.getElementById("files").onchange = function () {
        document.getElementById("fileLabel").value = this.value.replace("C:\\fakepath\\", "");
    };

        (function () {
            var bar = $('.progress-bar');
            var percent = $('.progress-bar');
            var status = $('#status');

            $('form').ajaxForm({
                beforeSend: function () {
                    status.empty();
                    var percentVal = '0%';
                    bar.width(percentVal)
                    percent.html(percentVal);
                },
                uploadProgress: function (event, position, total, percentComplete) {
                    var percentVal = percentComplete + '%';
                    bar.width(percentVal)
                    percent.html(percentVal);
                },
                success: function () {
                    var percentVal = '100%';
                    bar.width(percentVal)
                    percent.html(percentVal);
                },
                complete: function (xhr) {
                    status.html(xhr.responseText);
                }
            });

        })();
</script>
End Section 