using Microsoft.AspNetCore.Components;

namespace Controls.Tabs;

public interface ITab
{
    public RenderFragment? ChildContent { get; }
}