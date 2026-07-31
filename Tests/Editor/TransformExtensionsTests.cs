using Jeomseon.GameObjects;
using NUnit.Framework;
using UnityEngine;

namespace Jeomseon.Tests
{
    public sealed class TransformExtensionsTests
    {
        private GameObject _gameObject;
        private Transform _transform;

        [SetUp]
        public void SetUp()
        {
            _gameObject = new GameObject("TransformExtensionsTests");
            _transform = _gameObject.transform;
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
        }

        [Test]
        public void PositionSetters_ChangeOnlySelectedAxis()
        {
            _transform.position = new Vector3(1f, 2f, 3f);

            _transform.SetPositionX(4f);
            Assert.That(_transform.position, Is.EqualTo(new Vector3(4f, 2f, 3f)));
            _transform.SetPositionY(5f);
            Assert.That(_transform.position, Is.EqualTo(new Vector3(4f, 5f, 3f)));
            _transform.SetPositionZ(6f);
            Assert.That(_transform.position, Is.EqualTo(new Vector3(4f, 5f, 6f)));
        }

        [Test]
        public void LocalPositionSetters_ChangeOnlySelectedAxis()
        {
            _transform.localPosition = new Vector3(1f, 2f, 3f);

            _transform.SetLocalPositionX(4f);
            Assert.That(_transform.localPosition, Is.EqualTo(new Vector3(4f, 2f, 3f)));
            _transform.SetLocalPositionY(5f);
            Assert.That(_transform.localPosition, Is.EqualTo(new Vector3(4f, 5f, 3f)));
            _transform.SetLocalPositionZ(6f);
            Assert.That(_transform.localPosition, Is.EqualTo(new Vector3(4f, 5f, 6f)));
        }

        [Test]
        public void LocalScaleSetters_ChangeOnlySelectedAxis()
        {
            _transform.localScale = new Vector3(1f, 2f, 3f);

            _transform.SetLocalScaleX(4f);
            Assert.That(_transform.localScale, Is.EqualTo(new Vector3(4f, 2f, 3f)));
            _transform.SetLocalScaleY(5f);
            Assert.That(_transform.localScale, Is.EqualTo(new Vector3(4f, 5f, 3f)));
            _transform.SetLocalScaleZ(6f);
            Assert.That(_transform.localScale, Is.EqualTo(new Vector3(4f, 5f, 6f)));
        }
    }
}
