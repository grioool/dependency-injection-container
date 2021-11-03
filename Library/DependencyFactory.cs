using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Library
{
    public class DependencyFactory
    {
        private int _objectPoolIndex;
        private readonly object[] _objectPool;
        private readonly ConstructorInfo _constructorInfo;
        private readonly DependencyProvider _provider;

        public DependencyFactory(Type type, DependencyProvider provider, int maxPoolSize)
        { 
            _provider = provider;
            _constructorInfo = GetConstructorWithMostNumberOfParam(type);
            _objectPoolIndex = 0;
            _objectPool = new object[maxPoolSize];
        }

        public object Produce(UniqueId id)
        {
            object produced;
            if (_objectPool[_objectPoolIndex] == null)
            {
                produced = _objectPool[_objectPoolIndex] = _constructorInfo.Invoke(
                    _constructorInfo.GetParameters()
                        .Select(info => _provider.Construct(id, info.ParameterType))
                        .ToArray()
                );
            }
            else
            {
                produced = _objectPool[_objectPoolIndex];
            }
            _objectPoolIndex = (_objectPoolIndex + 1) % _objectPool.Length;
            return produced;
        }

        private ConstructorInfo GetConstructorWithMostNumberOfParam(Type type)
        {
            ConstructorInfo mostConstructor = null;
            List<ConstructorInfo> constructors = type.GetConstructors()
                .ToList();
            constructors.Sort((a, b) =>
                a.GetParameters().Length.CompareTo(b.GetParameters().Length)
            );
            
            constructors.ForEach(constructor =>
            {
                if (constructor.GetParameters().All(info => _provider.CanConstruct(info.ParameterType)))
                    mostConstructor = constructor;
            });
            return mostConstructor;
        }
    }
}