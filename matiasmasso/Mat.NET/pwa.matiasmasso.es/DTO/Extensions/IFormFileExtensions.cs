using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public static class IFormFileExtensions
{
    /// <summary>
    /// Method <c>BytesAsync</c> converts uploaded file such as an image to a Byte array ready to feed a SQL database varbinary(max) field
    /// </summary>
    public static async Task<Byte[]?> BytesAsync(this IFormFile file, int? maxBytesLength = null)
    {
        using (var memoryStream = new MemoryStream())
        {
            Byte[]? retval = null;
            await file.CopyToAsync(memoryStream);

            if (maxBytesLength == null || memoryStream.Length < maxBytesLength)
                retval = memoryStream.ToArray();
            else
                throw (new System.Exception("The file is too large."));
            return retval;
        }
    }
}