namespace FunBooksAndVideos.Rules
{
    public interface IRule<T> where T : class
    {
        void ClearConditions();
        void Initialize(T obj);
        bool IsValid();
        T Apply(T obj);
    }
}