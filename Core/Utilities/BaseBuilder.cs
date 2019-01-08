namespace InfoViaLinq.Utilities
{
    public class BaseBuilder<T> where T : new()
    {
        /// <summary>
        /// Creates a new instance of self
        /// </summary>
        /// <returns></returns>
        public static T New() => new T();
    }
}