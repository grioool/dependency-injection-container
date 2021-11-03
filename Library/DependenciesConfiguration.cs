using System;
using System.Collections.Concurrent;

namespace Library
{
    public class DependenciesConfiguration
    {
        public ConcurrentDictionary<Type, Type> Dependencies { get; } = new();

        public void Register<TDependency, TImplementation>()
        {
            Type dependencyType = typeof(TDependency);
            Type implementationType = typeof(TImplementation);
            
            if (implementationType == null) throw new ArgumentNullException(nameof(implementationType));
            if (implementationType.IsInterface || implementationType.IsAbstract)
                throw new ArgumentException("Implementation type cannot be interface or abstract");
            if (!implementationType.IsAssignableTo(dependencyType))
                throw new ArgumentException("Type " + implementationType + " is not assignable to " + dependencyType);

            Dependencies[dependencyType] = implementationType;
        }
    }
}