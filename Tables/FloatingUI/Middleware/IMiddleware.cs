using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Controls.FloatingUI.Middleware
{
    public interface IMiddleware
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("options")]
        public object? Options { get; }
    }
}
