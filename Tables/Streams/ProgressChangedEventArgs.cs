namespace Controls.Streams;

public delegate void ProgressChangedEventHandler(object? sender, ProgressChangedEventArgs e);

public class ProgressChangedEventArgs : EventArgs
{
    public ProgressChangedEventArgs(int progressPercentage) => ProgressPercentage = progressPercentage;

    public int ProgressPercentage { get; }
}
