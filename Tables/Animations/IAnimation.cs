using Controls.Css;
using Microsoft.AspNetCore.Components;

namespace Controls.Animations;

public interface IAnimation
{
    public RenderFragment<Class> ChildContent { get; set; }
}