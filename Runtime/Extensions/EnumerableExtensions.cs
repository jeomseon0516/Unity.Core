using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jeomseon.Extensions
{
    // .. Linq 커스텀 기능
    // TODO(리팩토링): LINQ 표준 API와 중복되는 확장 메서드는 제거 또는 이름을 변경하고,
    // 지연 실행 컬렉션의 중복 열거 및 null source/action 처리 규칙을 검토해야 합니다.
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> DefaultIfEmpty<T>(this IEnumerable<T> source, IEnumerable<T> defaultCorrection)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (defaultCorrection == null)
                throw new ArgumentNullException(nameof(defaultCorrection));

            return source.Any() ? source : defaultCorrection;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source) action.Invoke(element);
        }

        public static void ForEach<T>(this IEnumerable source, Action<T> action)
        {
            foreach (T element in source) action.Invoke(element);
        }

        public static void ForEachSafe<T>(this IEnumerable<T> source, Action<T> action) where T : class
        {
            foreach (T element in source) if (element != null) action.Invoke(element);
        }

        public static void ForEachSafe<T>(this IEnumerable source, Action<T> action) where T : class
        {
            foreach (T element in source) if (element != null) action.Invoke(element);
        }
    }
}
