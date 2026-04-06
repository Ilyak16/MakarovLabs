using Monitoring_Imitation.Events;
using Monitoring_Imitation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Imitation.Strategies.Formatters
{
    public class HtmlFormatter : IMessageFormatter
    {
        public string Format(MetricEventArgs eventArgs)
        {
            var data = eventArgs.Data;
            return $@"<!DOCTYPE html>
<html><head><style>
body{{font-family:Segoe UI,sans-serif;padding:20px;background:#f5f5f5}}
.alert{{background:#ffebee;border-left:4px solid #f44336;padding:15px;margin:10px 0}}
.metric{{color:#333}}.value{{color:#d32f2f;font-weight:bold}}
</style></head><body>
<div class='alert'>
<h3>🚨 {eventArgs.EventType}</h3>
<p class='metric'><strong>Metric:</strong> {data.MetricName}</p>
<p class='value'><strong>Value:</strong> {data.Value} (Threshold: {data.Threshold})</p>
<p><strong>Time:</strong> {data.Timestamp:yyyy-MM-dd HH:mm:ss}</p>
</div></body></html>";
        }
    }
}
