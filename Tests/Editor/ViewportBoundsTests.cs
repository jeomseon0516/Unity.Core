using System;
using Jeomseon.Unity.Core.Grids;
using Jeomseon.Unity.Core.Rendering;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Jeomseon.Tests
{
    public sealed class ViewportBoundsTests
    {
        private Camera _camera;
        private Grid _grid;

        [SetUp]
        public void SetUp()
        {
            _camera = new GameObject("Camera").AddComponent<Camera>();
            _camera.transform.position = new Vector3(0f, 0f, -8f);
            _camera.orthographic = true;
            _camera.orthographicSize = 2f;
            _camera.aspect = 2f;

            _grid = new GameObject("Grid").AddComponent<Grid>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_camera.gameObject);
            Object.DestroyImmediate(_grid.gameObject);
        }

        [Test]
        public void ProjectCorners_UsesViewportOrderAndPreservesExtraSlots()
        {
            var corners = new Vector3[5];
            corners[4] = Vector3.one;

            Assert.That(ViewportPlaneProjection.TryProjectCorners(_camera,
                new Plane(Vector3.forward, Vector3.zero), corners), Is.True);
            Assert.That(corners, Is.EqualTo(new[]
            {
                new Vector3(-4f, -2f, 0f), new Vector3(4f, -2f, 0f),
                new Vector3(-4f, 2f, 0f), new Vector3(4f, 2f, 0f), Vector3.one,
            }));
        }

        [Test]
        public void ProjectCorners_PerspectiveProjectionTracksViewDepth()
        {
            _camera.orthographic = false;
            _camera.fieldOfView = 90f;
            Span<Vector3> corners = stackalloc Vector3[4];

            Assert.That(ViewportPlaneProjection.TryProjectCorners(_camera,
                new Plane(Vector3.forward, Vector3.zero), corners), Is.True);
            Assert.That(corners[0].x, Is.EqualTo(-16f).Within(0.001f));
            Assert.That(corners[0].y, Is.EqualTo(-8f).Within(0.001f));
            Assert.That(corners[3].x, Is.EqualTo(16f).Within(0.001f));
            Assert.That(corners[3].y, Is.EqualTo(8f).Within(0.001f));
        }

        [Test]
        public void ProjectCorners_WhenOnlySomeRaysMeetPlane_PreservesBuffer()
        {
            var sentinel = new Vector3(99f, 98f, 97f);
            var corners = new[] { sentinel, sentinel, sentinel, sentinel };
            var plane = new Plane(new Vector3(-3f, 0f, 1f), Vector3.zero);

            Assert.That(ViewportPlaneProjection.TryProjectCorners(_camera, plane, corners), Is.False);
            Assert.That(corners, Is.EqualTo(new[] { sentinel, sentinel, sentinel, sentinel }));
        }

        [Test]
        public void ProjectCorners_RejectsNonFiniteIntersection()
        {
            var plane = new Plane(Vector3.forward, float.NegativeInfinity);
            var corners = new[] { Vector3.one, Vector3.one, Vector3.one, Vector3.one };

            Assert.That(ViewportPlaneProjection.TryProjectCorners(_camera, plane, corners), Is.False);
            Assert.That(corners, Is.All.EqualTo(Vector3.one));
        }

        [Test]
        public void ProjectCorners_RejectsShortBuffer()
        {
            Assert.Throws<ArgumentException>(() => ViewportPlaneProjection.TryProjectCorners(
                _camera, new Plane(Vector3.forward, Vector3.zero), new Vector3[3]));
        }

        [Test]
        public void Calculate_UsesHalfOpenIntervalWithExactNegativeEdges()
        {
            Assert.That(ViewportCellBounds.TryCalculate(_camera, _grid, out var bounds), Is.True);
            Assert.That(bounds, Is.EqualTo(new BoundsInt(-4, -2, 0, 8, 4, 1)));
        }

        [Test]
        public void Calculate_RoundsOutwardInTranslatedGridWithNonUnitCells()
        {
            _grid.transform.position = new Vector3(1f, -0.5f, 2f);
            _grid.cellSize = new Vector3(2f, 1f, 1f);

            Assert.That(ViewportCellBounds.TryCalculate(_camera, _grid, out var bounds), Is.True);
            Assert.That(bounds, Is.EqualTo(new BoundsInt(-3, -2, 0, 5, 5, 1)));
        }

        [Test]
        public void Calculate_ReturnsDefaultWhenPlaneIsBehindCamera()
        {
            _grid.transform.position = new Vector3(0f, 0f, -20f);

            Assert.That(ViewportCellBounds.TryCalculate(_camera, _grid, out var bounds), Is.False);
            Assert.That(bounds, Is.EqualTo(default(BoundsInt)));
        }

        [TestCase(1e9f)]
        [TestCase(2e9f)]
        public void Calculate_RejectsUnrepresentableBounds(float orthographicSize)
        {
            _camera.orthographicSize = orthographicSize;

            Assert.That(ViewportCellBounds.TryCalculate(_camera, _grid, out var bounds), Is.False);
            Assert.That(bounds, Is.EqualTo(default(BoundsInt)));
        }

        [Test]
        public void Calculate_RejectsUnsupportedLayoutOrSwizzle()
        {
            _grid.cellLayout = GridLayout.CellLayout.Hexagon;
            Assert.That(ViewportCellBounds.TryCalculate(_camera, _grid, out _), Is.False);

            _grid.cellLayout = GridLayout.CellLayout.Rectangle;
            _grid.cellSwizzle = GridLayout.CellSwizzle.XZY;
            Assert.That(ViewportCellBounds.TryCalculate(_camera, _grid, out _), Is.False);
        }
    }
}
