using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jeomseon.Helper
{
    // TODO(모듈화): 텍스처 리샘플링은 Core보다 이미지 처리 기능에 가까우므로 별도 모듈 이동을 검토하고,
    // 입력 크기 검증, 보간 방식, 메모리 할당 및 Job/Burst 적용 가능성을 비교해야 합니다.
    public static class TextureHelper
    {
        public static Color[] ResizeColorPixel(Color[] originalPixels, int originalWidth, int originalHeight, int targetWidth, int targetHeight)
        {
            Color[] newPixels = new Color[targetWidth * targetHeight];

            float aspectRatio = (float)originalWidth / originalHeight;
            (int resizeWidth, int resizeHeight) = aspectRatio > 1 
                ? (targetWidth, Mathf.RoundToInt(targetWidth / aspectRatio))
                : (targetHeight, Mathf.RoundToInt(targetHeight / aspectRatio));

            float ratioX = (float)originalWidth / resizeWidth;
            float ratioY = (float)originalHeight / resizeHeight;

            for (int y = 0; y < resizeHeight; y++)
            {
                for (int x = 0; x < resizeWidth; x++)
                {
                    int xFloor = Mathf.Clamp(Mathf.FloorToInt(x * ratioX), 0, originalWidth - 1);
                    int yFloor = Mathf.Clamp(Mathf.FloorToInt(y * ratioY), 0, originalHeight - 1);

                    newPixels[y * resizeWidth + x] = originalPixels[yFloor * originalWidth + xFloor];
                }
            }

            Color[] fullSizePixels = new Color[targetWidth * targetHeight];
            for (int i = 0; i < fullSizePixels.Length; i++)
            {
                fullSizePixels[i] = new(0, 0, 0, 0);
            }

            int xOffset = (targetWidth - resizeWidth) / 2;
            int yOffset = (targetHeight - resizeHeight) / 2;

            for (int y = 0; y < resizeHeight; y++)
            {
                for (int x = 0; x < resizeWidth; x++)
                {
                    fullSizePixels[(y + yOffset) * targetWidth + (x + xOffset)] = newPixels[y * resizeWidth + x];
                }
            }

            return fullSizePixels;
        }
    }
}
