namespace Controls.Animations;

public interface IAnimation
{
    public static string ActiveClass => "active";

    public static string? AnimationClass { get; }
}