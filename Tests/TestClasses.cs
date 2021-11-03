using Library;

namespace Tests
{
    public interface ITestInterface
    {
    }

    public class TestClass : ITestInterface
    {
        public ISecondTestInterface SecondTestClass { get; }

        public TestClass(ISecondTestInterface secondTestClass)
        {
            SecondTestClass = secondTestClass;
        }
    }

    public interface ISecondTestInterface
    {
    }

    public class SecondTestClass : ISecondTestInterface
    {
        public SecondTestClass(ITestInterface testClass)
        {
        }
    }

    public class GenericArgument
    {
        
    }

    public class ForPoolTest
    {
        
    }

    public abstract class AbstractTestClass : ITestInterface
    {
        
    }

    public class GenericClass<T>
    {
        public T Something { get; set; }

        public GenericClass(T something)
        {
            Something = something;
        }
    }

    public class GenericProvider<T>
    {
        
        public DependencyProvider Provider { get; }
        public GenericProvider()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            configuration.Register<T, T>();
            configuration.Register<GenericClass<T>, GenericClass<T>>();
            Provider = new DependencyProvider(configuration);
            
        }
    }
}