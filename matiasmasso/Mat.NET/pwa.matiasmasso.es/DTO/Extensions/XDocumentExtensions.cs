using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

public static class XDocumentExtensions
{
    public static string Serialize(this XDocument xdoc)
    {
        var options = SaveOptions.None;
        var newLine = (options & SaveOptions.DisableFormatting) == SaveOptions.DisableFormatting ? "" : Environment.NewLine;
        var retval = xdoc.Declaration == null ? xdoc.ToString(options) : xdoc.Declaration + newLine + xdoc.ToString(options);
        return retval;
    }
}