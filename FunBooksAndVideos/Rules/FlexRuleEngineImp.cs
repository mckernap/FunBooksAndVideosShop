using System.Collections.Generic;

namespace FunBooksAndVideos.Rules
{
    public class FlexRuleEngineImp<T> where T : class, new()
    {
        IEnumerable<AbstractBaseRule<T>> _rules;

        public FlexRuleEngineImp(IEnumerable<AbstractBaseRule<T>> rules)
        {
            _rules = rules;
        }

        public T Run()
        {
            T serviceInterface = (new T());
            foreach (var rule in _rules)
                serviceInterface.ApplyRule(rule);
           
            return serviceInterface;
        }
    }
}
