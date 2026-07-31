using System;
using Jeomseon.Imaging;
using Jeomseon.Rendering;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Jeomseon.Tests
{
    public sealed class UnityUtilityTests
    {
        [Test]
        public void ResizeToFit_PreservesAspectRatioAndFillsPadding()
        {
            Color[] source = { Color.red, Color.blue };

            Color[] result = TexturePixelResampler.ResizeToFit(source, 2, 1, 4, 4);

            Assert.That(result, Has.Length.EqualTo(16));
            Assert.That(result[0], Is.EqualTo(Color.clear));
            Assert.That(result[4], Is.EqualTo(Color.red));
            Assert.That(result[5], Is.EqualTo(Color.red));
            Assert.That(result[6], Is.EqualTo(Color.blue));
            Assert.That(result[7], Is.EqualTo(Color.blue));
            Assert.That(result[15], Is.EqualTo(Color.clear));
        }

        [Test]
        public void ResizeToFit_RejectsMismatchedPixelCount()
        {
            Assert.Throws<ArgumentException>(() =>
                TexturePixelResampler.ResizeToFit(new Color[1], 2, 1, 2, 1));
        }

        [Test]
        public void TryCalculateWorldBounds_CombinesRenderersAndExcludesParticles()
        {
            GameObject root = new("Root");
            GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject particles = new("Particles", typeof(ParticleSystem));

            try
            {
                left.transform.SetParent(root.transform);
                left.transform.position = new Vector3(-1f, 0f, 0f);
                right.transform.SetParent(root.transform);
                right.transform.position = new Vector3(2f, 0f, 0f);
                particles.transform.SetParent(root.transform);
                particles.transform.position = new Vector3(100f, 0f, 0f);

                bool found = RendererBoundsCalculator.TryCalculateWorldBounds(root, out Bounds bounds);

                Assert.That(found, Is.True);
                Assert.That(bounds.center.x, Is.EqualTo(0.5f).Within(0.0001f));
                Assert.That(bounds.size, Is.EqualTo(new Vector3(4f, 1f, 1f)));
            }
            finally
            {
                Object.DestroyImmediate(root);
            }
        }

        [Test]
        public void TryCalculateWorldBounds_ReturnsFalseWithoutRenderers()
        {
            GameObject root = new("Root");

            try
            {
                bool found = RendererBoundsCalculator.TryCalculateWorldBounds(root, out Bounds bounds);

                Assert.That(found, Is.False);
                Assert.That(bounds, Is.EqualTo(default(Bounds)));
            }
            finally
            {
                Object.DestroyImmediate(root);
            }
        }
    }
}
