namespace FunBooksAndVideos.Rules
{
    internal class Equals : ICondition
    {
        private readonly int _left;
        private readonly int _right;

        public Equals(int left, int right)
        {
            _left = left;
            _right = right;
        }

        public bool IsSatisfied()
        {
            return _left == _right;
        }
    }
}
