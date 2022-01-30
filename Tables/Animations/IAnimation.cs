using Controls.Css;
using Microsoft.AspNetCore.Components;

namespace Controls.Animations;

public interface IAnimation
{
    public static readonly Class ActiveClass = new("active");
    public RenderFragment<Class> ChildContent { get; set; }
}