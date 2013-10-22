using System;

namespace MockDataVisualizerTestConsole.Tests.TestObjects
{
    public interface IServicedEntity<T>
    {
        bool IsValid { get; }
        Exception Exception { get; }
        T Entity { get; }
    }

    public class ServicedEntity<T> : IServicedEntity<T>
    {
        public ServicedEntity(T entity)
        {
            if (entity == null)
            {
                IsValid = false;
                Exception = new NullReferenceException("Object is null");
                return;
            }
            IsValid = true;
            Entity = entity;
        }

        public ServicedEntity(Exception exception)
        {
            IsValid = false;
            Exception = exception;
        }

        public ServicedEntity(){} 

        public bool IsValid { get; protected set; }

        public Exception Exception { get; protected set; }

        public T Entity { get; protected set; }
    }
}
