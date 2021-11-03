using Library;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private DependencyProvider _provider;

        [SetUp]
        public void Setup()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<ITestInterface, TestClass>();
            config.Register<ISecondTestInterface, SecondTestClass>();
            config.Register<GenericArgument, GenericArgument>();
            config.Register<GenericClass<GenericArgument>, GenericClass<GenericArgument>>();
            _provider = new DependencyProvider(config);
        }

        [Test]
        public void CyclingDependencyTest()
        {
            Assert.Catch<CyclingDependencyException>(() =>  _provider.Resolve<ITestInterface>());
        }

        [Test]
        public void GenericTest()
        {
            GenericClass<GenericArgument> genericClass = _provider.Resolve<GenericClass<GenericArgument>>();
            Assert.NotNull(genericClass);
            Assert.NotNull(genericClass.Something);
        }

        [Test]
        public void PoolTest()
        {
            DependenciesConfiguration config = new DependenciesConfiguration();
            config.Register<ForPoolTest, ForPoolTest>();
            DependencyProvider provider = new DependencyProvider(config, 5);
            
            ForPoolTest firstResolved = provider.Resolve<ForPoolTest>();
            provider.Resolve<ForPoolTest>();
            provider.Resolve<ForPoolTest>();
            provider.Resolve<ForPoolTest>();
            Assert.False(provider.Resolve<ForPoolTest>() == firstResolved);
            Assert.True(provider.Resolve<ForPoolTest>() == firstResolved);
        }

        [Test]
        public void OpenGenericTest()
        {
            GenericProvider<object> genericProvider = new GenericProvider<object>();
            Assert.NotNull(genericProvider.Provider.Resolve<object>());
        }
    }
}