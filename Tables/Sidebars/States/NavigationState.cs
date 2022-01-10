using Microsoft.AspNetCore.Components.Routing;

namespace Controls.Sidebars.States
{
    public class NavigationState
    {
        public string Path { get; set; } = string.Empty;

        public void NavigationChange(NavigationContext navigationContext) => Path = navigationContext.Path;
    }
}
