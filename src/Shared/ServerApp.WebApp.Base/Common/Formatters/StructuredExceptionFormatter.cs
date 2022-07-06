using Serilog.Events;
using Serilog.Exceptions.Core;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace ServerApp.WebApp.Base.Common.Formatters;

public class StructuredExceptionFormatter : ITextFormatter
{
    private readonly string rootName;
    private readonly JsonValueFormatter valueFormatter = new(typeTagName: null);
    
    public StructuredExceptionFormatter(string? rootName = null)
    {
        this.rootName = rootName ?? new DestructuringOptionsBuilder().RootName;
    }
    
    public void Format(LogEvent logEvent, TextWriter output)
    {
        output.Write("{\"Timestamp\":\"");
        output.Write(logEvent.Timestamp.UtcDateTime.ToString("O"));

        output.Write("\",\"Message\":");
        var message = logEvent.MessageTemplate.Render(logEvent.Properties);
        JsonValueFormatter.WriteQuotedJsonString(message, output);

        output.Write(",\"Level\":\"");
        output.Write(logEvent.Level);
        output.Write('\"');

        var propCount = logEvent.Properties.Count;

        if (logEvent.Properties.TryGetValue(this.rootName, out var exceptionProperty))
        {
            output.Write(",\"Exception\":");
            this.valueFormatter.Format(exceptionProperty, output);
            propCount--;
        }

        if (propCount > 0)
        {
            output.Write(",\"Properties\":{");
            var comma = false;

            foreach (var property in logEvent.Properties)
            {
                if (property.Key == this.rootName)
                {
                    continue;
                }

                if (comma)
                {
                    output.Write(',');
                }
                else
                {
                    comma = true;
                }

                JsonValueFormatter.WriteQuotedJsonString(property.Key, output);
                output.Write(':');
                this.valueFormatter.Format(property.Value, output);
            }

            output.Write("}");
        }

        output.WriteLine('}');
    }
}