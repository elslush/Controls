using Controls.FloatingUI.Middleware;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Controls.FloatingUI
{
    public record FloatingUIOptions
    {
        [JsonIgnore]
        public Placement? Placement { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("placement")]
        public string? PlacementString => Placement?.ToString();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), JsonPropertyName("strategy")]
        public Strategy? Strategy { get; init; }

        [JsonIgnore]
        public IEnumerable<IMiddleware> Middleware { private get; init; } = Enumerable.Empty<IMiddleware>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault), JsonPropertyName("middlewarespec")]
        public IEnumerable<object> MiddlewareObjects => Middleware;
    }
}
