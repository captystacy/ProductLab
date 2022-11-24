using System.Text;

namespace CardPullouter.Core
{
    public class HtmlElement
    {
        public string Name { get; }
        public Dictionary<string, string>? Attributes { get; set; }
        public Dictionary<string, string>? AvoidableAttributes { get; set; }

        public HtmlElement(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Name);

            if (Attributes is not null &&  Attributes.Count > 0)
            {
                AppendAttributeSelector(sb, Attributes);
            }

            if (AvoidableAttributes is not null && AvoidableAttributes.Count > 0)
            {
                sb.Append(":not(");

                AppendAttributeSelector(sb, AvoidableAttributes);

                sb.Append(")");
            }

            return sb.ToString();
        }

        private void AppendAttributeSelector(StringBuilder sb, Dictionary<string, string> attributes)
        {
            foreach (var attribute in attributes)
            {
                sb.Append($"[{attribute.Key}*='{attribute.Value}']");
            }
        }
    }
}
