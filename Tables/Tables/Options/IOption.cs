namespace Controls.Tables.Options;

public interface IOption<T>
{
    public IQueryable<T> Transform(IQueryable<T> source);
}
