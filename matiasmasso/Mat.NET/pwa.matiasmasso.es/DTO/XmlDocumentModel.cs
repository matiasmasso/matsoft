using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class XmlDocumentModel
    {
        public bool IncludeXMLDeclaration { get; set; }
        public List<Segment> Segments { get; set; } = new();

        public XmlDocumentModel(bool includeXMLDeclaration = false)
        {
            IncludeXMLDeclaration = includeXMLDeclaration;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (IncludeXMLDeclaration)
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");

            foreach (var segment in Segments)
                sb.Append(segment.ToString());

            return sb.ToString();
        }

        public Segment AddSegment(string? tag, string? content = null)
        {
            var retval = new Segment(tag, content);
            Segments.Add(retval);
            return retval;
        }

        public class Segment
        {
            public string? Tag { get; set; }
            public string? Content { get; set; }
            public List<Segment> Segments { get; set; } = new();
            public List<Attribute> Attributes { get; set; } = new();

            public Segment(string? tag, string? content)
            {
                Tag = tag;
                Content = content;
            }

            public Segment AddSegment(string? tag, string? content=null)
            {
                var retval = new Segment(tag, content);
                Segments.Add(retval);
                return retval;
            }

            public Attribute AddAttribute(string? nom, string? value)
            {
                var retval = new Attribute(nom, value);
                Attributes.Add(retval);
                return retval;
            }
            public List<Attribute> AddAttributes(params string[] values)
            {
                var retval = new List<Attribute>();
                for (var i = 0; i < values.Length; i += 2)
                {
                    var attribute = new Attribute(values[i], values[i + 1]);
                    retval.Add(attribute);
                }
                Attributes.AddRange(retval);
                return retval;
            }

            public bool IsEmpty() => Segments.Count == 0 && string.IsNullOrEmpty(Content);

            public override string ToString()
            {
                string? retval;
                var serializedAttributes = string.Join(" ", Attributes.Select(x => x.ToString()).ToArray());
                if (IsEmpty())
                    retval = $"<{Tag} {serializedAttributes}/>";
                else
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"<{Tag} {serializedAttributes}>");

                    foreach (var segment in Segments)
                        sb.Append(segment.ToString());

                    sb.Append(System.Net.WebUtility.HtmlEncode(Content));

                    sb.Append($"</{Tag}>");
                    retval = sb.ToString();
                }
                return retval;
            }

        }
        public class Attribute
        {
            public string? Nom { get; set; }
            public string? Value { get; set; }

            public Attribute(string? nom, string? value)
            {
                Nom = nom;
                Value = value;
            }

            public override string ToString()
            {
                return $"\"{Nom}\"=\"{Value}\"";
            }
        }
    }
}
