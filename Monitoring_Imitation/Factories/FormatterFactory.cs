using Monitoring_Imitation.Interfaces;
using Monitoring_Imitation.Strategies.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Factories
{
    public static class FormatterFactory
    {
        public enum FormatType { Text, Json, Html }

        public static IMessageFormatter Create(FormatType type) => type switch
        {
            FormatType.Text => new TextFormatter(),
            FormatType.Json => new JsonFormatter(),
            FormatType.Html => new HtmlFormatter(),
            _ => throw new ArgumentException($"Unknown format: {type}")
        };

        public static IMessageFormatter Create(string type) =>
            Enum.TryParse<FormatType>(type, true, out var result)
                ? Create(result)
                : throw new ArgumentException($"Unknown format: {type}");
    }
}
