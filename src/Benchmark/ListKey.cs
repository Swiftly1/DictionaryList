namespace Benchmark
{
    public struct ListKey<T>
    {
        private readonly List<T>? data;

        public ListKey(List<T> data)
        {
            if (data is null)
                throw new Exception();

           this.data = data;
        }

        private bool haveHashCode = false;

        private int cachedHashCode = 0;

        public override int GetHashCode()
        {
            if (haveHashCode)
                return cachedHashCode;

            int hashCode = 0;

            foreach (var d in data)
            {
                hashCode = hashCode * 391 + d.GetHashCode();
            }

            haveHashCode = true;
            cachedHashCode = hashCode;
            return hashCode;
        }
    }
}
