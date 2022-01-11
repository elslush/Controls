using System.Collections.Immutable;

namespace Controls.DynamicComponents
{
    public class ComponentMetadata
    {
        public ComponentMetadata(Type componentType) => ComponentType = componentType;

        public Type ComponentType { get; }

        public ImmutableDictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>().ToImmutableDictionary();
    }
}
