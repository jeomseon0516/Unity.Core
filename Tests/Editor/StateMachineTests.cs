using Jeomseon.State;
using NUnit.Framework;

namespace Jeomseon.Tests
{
    public sealed class StateMachineTests
    {
        public sealed class Controller : IStateObject<Controller>
        {
            public StateMachine<Controller> StateMachine { get; } = new();
        }

        public sealed class FirstState : IState<Controller>, IFixedUpdateState<Controller>
        {
            public int AwakeCount { get; private set; }
            public int EnterCount { get; private set; }
            public int UpdateCount { get; private set; }
            public int FixedUpdateCount { get; private set; }
            public int ExitCount { get; private set; }

            public void Awake(Controller controller) => AwakeCount++;
            public void Enter(Controller controller) => EnterCount++;
            public void Update(Controller controller) => UpdateCount++;
            public void FixedUpdate(Controller controller) => FixedUpdateCount++;
            public void Exit(Controller controller) => ExitCount++;
        }

        public sealed class SecondState : IState<Controller>
        {
            public int AwakeCount { get; private set; }
            public int EnterCount { get; private set; }
            public int ExitCount { get; private set; }

            public void Awake(Controller controller) => AwakeCount++;
            public void Enter(Controller controller) => EnterCount++;
            public void Update(Controller controller) { }
            public void Exit(Controller controller) => ExitCount++;
        }

        [Test]
        public void ChangeState_CachesStateAndCallsLifecycleInOrder()
        {
            Controller controller = new();

            controller.StateMachine.ChangeState<FirstState>(controller);
            FirstState first = controller.StateMachine.GetState<FirstState>();
            controller.StateMachine.Update(controller);
            controller.StateMachine.FixedUpdate(controller);
            controller.StateMachine.ChangeState<SecondState>(controller);
            SecondState second = controller.StateMachine.GetState<SecondState>();
            controller.StateMachine.ChangeState<FirstState>(controller);

            Assert.That(first.AwakeCount, Is.EqualTo(1));
            Assert.That(first.EnterCount, Is.EqualTo(2));
            Assert.That(first.UpdateCount, Is.EqualTo(1));
            Assert.That(first.FixedUpdateCount, Is.EqualTo(1));
            Assert.That(first.ExitCount, Is.EqualTo(1));
            Assert.That(second.AwakeCount, Is.EqualTo(1));
            Assert.That(second.EnterCount, Is.EqualTo(1));
            Assert.That(second.ExitCount, Is.EqualTo(1));
            Assert.That(controller.StateMachine.CheckNowState<FirstState>(), Is.True);
            Assert.That(controller.StateMachine.GetState<SecondState>(), Is.Null);
        }

        [Test]
        public void UpdateWithoutState_DoesNotThrow()
        {
            Controller controller = new();

            Assert.DoesNotThrow(() => controller.StateMachine.Update(controller));
            Assert.DoesNotThrow(() => controller.StateMachine.FixedUpdate(controller));
        }
    }
}
