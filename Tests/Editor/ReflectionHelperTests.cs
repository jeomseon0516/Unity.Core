using System;
using System.Linq;
using Jeomseon.Helper;
using NUnit.Framework;

namespace Jeomseon.Tests
{
    public sealed class ReflectionHelperTests
    {
        public interface ITestService
        {
        }

        public sealed class TestService : ITestService
        {
        }

        private abstract class AbstractTestService : ITestService
        {
        }

        private enum TestValue
        {
            First = 3,
            Second = 7
        }

        [Test]
        public void GetChildTypes_FiltersInterfacesAndAbstractTypes()
        {
            Type[] types = ReflectionHelper.GetChildTypesFromBaseType<ITestService>().ToArray();

            CollectionAssert.Contains(types, typeof(TestService));
            CollectionAssert.DoesNotContain(types, typeof(AbstractTestService));
            CollectionAssert.DoesNotContain(types, typeof(ITestService));
        }

        [Test]
        public void CreateChildClasses_CreatesConcreteImplementations()
        {
            ITestService[] instances = ReflectionHelper.CreateChildClassesFromType<ITestService>().ToArray();

            Assert.That(instances.Any(instance => instance is TestService), Is.True);
        }

        [Test]
        public void EnumLookup_ReturnsNamesAndValues()
        {
            string[] names = ReflectionHelper.GetEnumValuesFromEnumName(nameof(TestValue)).ToArray();
            var values = ReflectionHelper.GetEnumKvpFromEnumName(nameof(TestValue));

            Assert.That(names, Is.EquivalentTo(new[] { nameof(TestValue.First), nameof(TestValue.Second) }));
            Assert.That(values[nameof(TestValue.First)], Is.EqualTo(3));
            Assert.That(values[nameof(TestValue.Second)], Is.EqualTo(7));
        }
    }
}
