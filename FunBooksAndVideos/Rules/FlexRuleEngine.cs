namespace FunBooksAndVideos.Rules
{
    public static class FlexRuleEngine
    { 
        public static T ApplyRule<T>(this T obj, IRule<T> rule) where T : class
        {
            rule.ClearConditions();
            rule.Initialize(obj);
            if (rule.IsValid())
            {
                rule.Apply(obj);
            }
            return obj;
        }
    }
}
