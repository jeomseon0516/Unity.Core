using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Jeomseon.Helper
{
    public static class ReflectionHelper
    {
        // TODO(리팩토링): Editor 전용 호출부는 UnityEditor.TypeCache를 사용하도록 분리하고,
        // 런타임 검색 결과는 도메인 리로드 단위로 캐시해 반복적인 전체 어셈블리 순회를 줄여야 합니다.
        public static IEnumerable<T> CreateChildClassesFromType<T>() where T : class
        {
            Type baseType = typeof(T);

            foreach (Type type in GetLoadableTypes()
                         .Where(type => !type.IsInterface && !type.IsAbstract && baseType.IsAssignableFrom(type)))
            {
                // TODO(리팩토링): 기본 생성자가 없거나 생성 중 예외가 발생하는 타입을
                // 호출자가 진단할 수 있도록 결과 및 오류 보고 정책을 정의해야 합니다.
                if (Activator.CreateInstance(type) is T instance)
                {
                    yield return instance;
                }
            }
        }

        public static IEnumerable<string> GetClassNamesFromParent(string baseClass)
        {
            Type baseType = GetLoadableTypes()
                .FirstOrDefault(type => type.FullName == baseClass || type.Name == baseClass);

            return baseType == null
                ? Enumerable.Empty<string>()
                : GetChildTypesFromBaseType(baseType).Select(type => type.Name);
        }

        public static IEnumerable<string> GetClassNamesFromParent<TBaseType>() where TBaseType : class
        {
            return GetChildTypesFromBaseType(typeof(TBaseType)).Select(type => type.Name);
        }

        public static IEnumerable<Type> GetChildTypesFromBaseType(Type baseType)
        {
            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            return GetLoadableTypes()
                .Where(type => !type.IsInterface && !type.IsAbstract && baseType.IsAssignableFrom(type));
        }

        public static IEnumerable<Type> GetChildTypesFromBaseType<T>()
        {
            return GetChildTypesFromBaseType(typeof(T));
        }

        public static IEnumerable<Type> GetChildClassesFromFieldTypeName(string typeName)
        {
            Type baseType = GetTypeFromFieldName(typeName);

            if (baseType == null)
            {
                Debug.LogWarning($"기준 타입을 찾지 못했습니다: {typeName}");
                return Enumerable.Empty<Type>();
            }

            return GetChildTypesFromBaseType(baseType);
        }

        public static Type GetTypeFromFieldName(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                Debug.LogWarning("타입 이름이 비어 있습니다.");
                return null;
            }

            string[] splitTypeNames = typeName.Split(new[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (splitTypeNames.Length == 0)
            {
                return null;
            }

            string assemblyName = splitTypeNames[0];
            string baseTypeName = splitTypeNames[^1];
            Assembly targetAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(assembly => assembly.GetName().Name == assemblyName);

            if (targetAssembly == null)
            {
                Debug.LogWarning($"어셈블리를 찾지 못했습니다: {assemblyName}");
                return null;
            }

            Type baseType = GetLoadableTypes(targetAssembly)
                .FirstOrDefault(type => type.Name == baseTypeName);

            if (baseType == null)
            {
                Debug.LogWarning($"타입을 찾지 못했습니다: {baseTypeName}");
            }

            return baseType;
        }

        public static IEnumerable<string> GetEnumValuesFromEnumName(string enumTypeName)
        {
            return GetLoadableTypes()
                .Where(type => type.IsEnum && type.Name == enumTypeName)
                .SelectMany(Enum.GetNames);
        }

        public static Dictionary<string, int> GetEnumKvpFromEnumName(string enumTypeName)
        {
            Type enumType = GetLoadableTypes()
                .FirstOrDefault(type => type.IsEnum && type.Name == enumTypeName);

            if (enumType == null)
            {
                return new Dictionary<string, int>();
            }

            return Enum.GetNames(enumType)
                .ToDictionary(
                    name => name,
                    name => Convert.ToInt32(Enum.Parse(enumType, name)));
        }

        private static IEnumerable<Type> GetLoadableTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(GetLoadableTypes);
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                return exception.Types.Where(type => type != null);
            }
        }
    }
}
