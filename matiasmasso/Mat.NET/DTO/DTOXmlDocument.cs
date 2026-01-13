using MatHelperStd;
using System.Collections.Generic;

namespace DTO
{
    public class DTOXmlDocument
    {
        public bool IncludeXMLDeclaration { get; set; }
        public List<DTOXmlSegment> Segments { get; set; }


        public DTOXmlDocument(bool includeXMLDeclaration = false) : base()
        {
            IncludeXMLDeclaration = includeXMLDeclaration;
            Segments = new List<DTOXmlSegment>();
        }

        public DTOXmlSegment AddSegment(string sTag, string sContent = "")
        {
            DTOXmlSegment retval = new DTOXmlSegment(sTag, sContent);
            Segments.Add(retval);
            return retval;
        }

        public static string ToString(DTOXmlDocument value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (value.IncludeXMLDeclaration)
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            foreach (DTOXmlSegment oSegment in value.Segments)
                sb.Append(ToString(oSegment));
            string retval = sb.ToString();
            return retval;
        }

        public static string ToString(DTOXmlSegment value)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<");
            sb.Append(value.Tag);
            foreach (DTOXmlAttribute oAttribute in value.Attributes)
                sb.Append(" " + oAttribute.ToString());

            if (value.Segments.Count == 0 & value.Content == "")
                sb.Append("/>");
            else
            {
                if (!string.IsNullOrEmpty(value.Content))
                    sb.Append(">" + System.Net.WebUtility.HtmlEncode(value.Content));
                else
                {
                    sb.Append(">");
                    foreach (DTOXmlSegment oSegment in value.Segments)
                        sb.Append(DTOXmlDocument.ToString(oSegment));
                }
                sb.Append("</" + value.Tag + ">");
            }
            string retval = sb.ToString();
            return retval;
        }
    }

    public class DTOXmlSegment
    {
        public string Tag { get; set; }
        public string Content { get; set; }
        public DTOXmlSegments Segments { get; set; }
        public DTOXmlAttributes Attributes { get; set; }

        public DTOXmlSegment(string sTag, string sContent = "") : base()
        {
            Tag = sTag;
            Attributes = new DTOXmlAttributes();
            Segments = new DTOXmlSegments();
            Content = sContent;
        }

        public DTOXmlAttribute AddAttribute(string sNom, string sValue)
        {
            DTOXmlAttribute retval = new DTOXmlAttribute(sNom, sValue);
            Attributes.Add(retval);
            return retval;
        }

        public void AddAttributes(params string[] sAttributes)
        {
            for (int i = 0; i <= sAttributes.Length - 2; i += 2)
                AddAttribute(sAttributes[i], sAttributes[i + 1]);
        }

        public DTOXmlSegment AddSegment(string sTag, string sContent = "")
        {
            DTOXmlSegment retval = new DTOXmlSegment(sTag, sContent);
            Segments.Add(retval);
            return retval;
        }

        public static string SafeText(string src)
        {
            string retval = System.Security.SecurityElement.Escape(src);
            return retval;
        }
    }

    public class DTOXmlSegments : List<DTOXmlSegment>
    {
    }

    public class DTOXmlAttribute
    {
        public string Nom { get; set; }
        public string Value { get; set; }

        public DTOXmlAttribute(string sNom, string sValue) : base()
        {
            Nom = sNom;
            Value = sValue;
        }

        public new string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(Nom);
            sb.Append("=");
            sb.Append(TextHelper.VbChr(39) + Value + TextHelper.VbChr(39));
            string retval = sb.ToString();
            return retval;
        }
    }

    public class DTOXmlAttributes : List<DTOXmlAttribute>
    {
    }
}
