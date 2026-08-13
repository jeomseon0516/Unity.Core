using Jeomseon.Unity.Core.Operations;
using NUnit.Framework;

namespace Jeomseon.Tests
{
    public sealed class ManagedOperationStatusTests
    {
        [Test]
        public void ManagedOperationStatus_DefinesTerminalStates()
        {
            Assert.That(ManagedOperationStatus.Completed, Is.Not.EqualTo(ManagedOperationStatus.Running));
            Assert.That(ManagedOperationStatus.Canceled, Is.Not.EqualTo(ManagedOperationStatus.Running));
            Assert.That(ManagedOperationStatus.Faulted, Is.Not.EqualTo(ManagedOperationStatus.Running));
        }
    }
}
