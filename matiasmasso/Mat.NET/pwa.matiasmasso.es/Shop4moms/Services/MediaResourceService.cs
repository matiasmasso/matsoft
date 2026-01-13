using DTO;
using Microsoft.AspNetCore.Hosting;

namespace Shop4moms.Services
{
    public class MediaResourceService
    {

        public static ImageMime? ImageMime(string filename)
        {
            ImageMime? retval = null;
            var url = string.Format("~/Recursos/{0}", filename);
            //var path = Server.MapPath(url);
            Byte[]? oBuffer = null;
            //If MatHelperStd.FileSystemHelper.GetStreamFromFile(path, oBuffer, exs) Then
            //    Dim filename = DTOMediaResource.FriendlyName(oMediaResource)
            //    Dim sContentType As String = MediaHelper.ContentType(oMediaResource.Mime)
            //    retval = New FileContentResult(oBuffer, sContentType)
            //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" & filename & "")
            //Else
            //    retval = Await ErrorResult(exs)
            //End If
            return retval;
        }
    }
}
