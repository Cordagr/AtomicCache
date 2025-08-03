public static class Exceptions
{
    // Thrown when a requested cache item is not found
    public class CacheItemNotFoundException : Exception
    {
        public CacheItemNotFoundException(string message) : base(message) { }

        public CacheItemNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // Thrown when trying to insert a duplicate key without overwrite allowed
    public class CacheItemAlreadyExistsException : Exception
    {
        public CacheItemAlreadyExistsException(string message) : base(message) { }

        public CacheItemAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // Thrown when the cache reaches capacity and cannot evict
    public class CacheEvictionFailureException : Exception
    {
        public CacheEvictionFailureException(string message) : base(message) { }

        public CacheEvictionFailureException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // Thrown when attempting to register a null eviction callback
    public class InvalidEvictionCallbackException : Exception
    {
        public InvalidEvictionCallbackException(string message) : base(message) { }

        public InvalidEvictionCallbackException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    // Thrown when an operation is attempted on a disposed or inactive cache
    public class CacheDisposedException : Exception
    {
        public CacheDisposedException(string message) : base(message) { }

        public CacheDisposedException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
