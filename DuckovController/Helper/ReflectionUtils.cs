using System;
using System.Reflection;

namespace DuckovController.Helper
{
    public static class ReflectionUtils
    {
        public static Type FindType(string name)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(name);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }

        public static FieldInfo FindField<T>(string name)
        {
            return typeof(T).GetField(name,
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.Static);
        }
    }
}
