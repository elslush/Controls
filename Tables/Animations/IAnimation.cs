namespace Controls.Animations;

public interface IAnimation
{
    public static string ActiveClass => "active";

    static abstract string? AnimationClass { get; }
}