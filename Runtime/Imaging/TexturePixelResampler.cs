using System;
using UnityEngine;

namespace Jeomseon.Imaging
{
    /// <summary>
    /// CPU에서 <see cref="Color"/> 픽셀 버퍼를 변환합니다.
    /// </summary>
    public static class TexturePixelResampler
    {
        /// <summary>
        /// 원본 비율을 유지하면서 픽셀을 대상 영역 안에 맞추고 남는 영역을 투명하게 채웁니다.
        /// 최근접 이웃 보간을 사용합니다.
        /// </summary>
        public static Color[] ResizeToFit(
            Color[] sourcePixels,
            int sourceWidth,
            int sourceHeight,
            int targetWidth,
            int targetHeight)
        {
            return ResizeToFit(
                sourcePixels,
                sourceWidth,
                sourceHeight,
                targetWidth,
                targetHeight,
                Color.clear);
        }

        /// <summary>
        /// 원본 비율을 유지하면서 픽셀을 대상 영역 안에 맞추고 남는 영역을 지정한 색으로 채웁니다.
        /// 최근접 이웃 보간을 사용합니다.
        /// </summary>
        public static Color[] ResizeToFit(
            Color[] sourcePixels,
            int sourceWidth,
            int sourceHeight,
            int targetWidth,
            int targetHeight,
            Color paddingColor)
        {
            if (sourcePixels is null)
                throw new ArgumentNullException(nameof(sourcePixels));
            if (sourceWidth <= 0)
                throw new ArgumentOutOfRangeException(nameof(sourceWidth));
            if (sourceHeight <= 0)
                throw new ArgumentOutOfRangeException(nameof(sourceHeight));
            if (targetWidth <= 0)
                throw new ArgumentOutOfRangeException(nameof(targetWidth));
            if (targetHeight <= 0)
                throw new ArgumentOutOfRangeException(nameof(targetHeight));
            if (sourcePixels.Length != sourceWidth * sourceHeight)
            {
                throw new ArgumentException(
                    "픽셀 수는 sourceWidth * sourceHeight와 같아야 합니다.",
                    nameof(sourcePixels));
            }

            float scale = Mathf.Min(
                (float)targetWidth / sourceWidth,
                (float)targetHeight / sourceHeight);
            int resizedWidth = Mathf.Clamp(
                Mathf.RoundToInt(sourceWidth * scale),
                1,
                targetWidth);
            int resizedHeight = Mathf.Clamp(
                Mathf.RoundToInt(sourceHeight * scale),
                1,
                targetHeight);

            Color[] targetPixels = new Color[targetWidth * targetHeight];
            if (paddingColor != Color.clear)
            {
                for (int i = 0; i < targetPixels.Length; i++)
                    targetPixels[i] = paddingColor;
            }

            int xOffset = (targetWidth - resizedWidth) / 2;
            int yOffset = (targetHeight - resizedHeight) / 2;

            for (int y = 0; y < resizedHeight; y++)
            {
                int sourceY = Mathf.Min(
                    Mathf.FloorToInt((y + 0.5f) * sourceHeight / resizedHeight),
                    sourceHeight - 1);

                for (int x = 0; x < resizedWidth; x++)
                {
                    int sourceX = Mathf.Min(
                        Mathf.FloorToInt((x + 0.5f) * sourceWidth / resizedWidth),
                        sourceWidth - 1);
                    targetPixels[(y + yOffset) * targetWidth + x + xOffset] =
                        sourcePixels[sourceY * sourceWidth + sourceX];
                }
            }

            return targetPixels;
        }
    }
}
