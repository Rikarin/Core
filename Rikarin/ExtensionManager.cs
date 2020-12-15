using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rikarin {
    public static class ExtensionManager {
        private static IDictionary<Type, IEnumerable<Type>> _types;
        public static IEnumerable<Assembly> Assemblies { get; private set; }

        public static void SetAssemblies(IEnumerable<Assembly> assemblies) {
            Assemblies = assemblies;
            _types = new Dictionary<Type, IEnumerable<Type>>();
        }


        // === Getting an implementation
        public static Type GetImplementation<T>(bool useCaching = false) {
            return GetImplementation<T>(null, useCaching);
        }
        
        public static Type GetImplementation<T>(Func<Assembly, bool> predicate, bool useCaching = false) {
            return GetImplementations<T>(predicate, useCaching).FirstOrDefault();
        }

        public static IEnumerable<Type> GetImplementations<T>(bool useCaching = false) {
            return GetImplementations<T>(null, useCaching);
        }

        public static IEnumerable<Type> GetImplementations<T>(Func<Assembly, bool> predicate, bool useCaching = false) {
            var type = typeof(T);
            return GetImplementations(type, predicate, useCaching);
        }

        public static IEnumerable<Type> GetImplementations(Type type, Func<Assembly, bool> predicate, bool useCaching = false) {
            if (useCaching && _types.ContainsKey(type)) {
                return _types[type];
            }

            var ret = new List<Type>();
            foreach (var x in GetAssemblies(predicate)) {
                foreach (var et in x.GetExportedTypes()) {
                    if (et.GetTypeInfo().IsClass && type.GetTypeInfo().IsAssignableFrom(et)) {
                        ret.Add(et);
                    }
                }
            }

            if (useCaching) {
                _types.Add(type, ret);
            }

            return ret;
        }

        public static IList<Type> GetInterfaceImplementations(Type type) {
            var ret = new List<Type>();

            foreach (var x in GetAssemblies(x => true)) {
                ret.AddRange(x.GetTypes()
                    .Where(y => y.GetInterfaces().Any(z => z.IsGenericType && z.GetGenericTypeDefinition() == type))
                    .ToList());
            }

            return ret;
        }


        // === Getting an instance of Assembly
        public static T GetInstance<T>(bool useCaching = false) {
            return GetInstance<T>(null, useCaching, new object[] { });
        }

        public static T GetInstance<T>(bool useCaching = false, params object[] args) {
            return GetInstance<T>(null, useCaching, args);
        }

        public static T GetInstance<T>(Func<Assembly, bool> predicate, bool useCaching = false) {
            return GetInstances<T>(predicate, useCaching).FirstOrDefault();
        }

        public static T GetInstance<T>(Func<Assembly, bool> predicate, bool useCaching = false, params object[] args) {
            return GetInstances<T>(predicate, useCaching, args).FirstOrDefault();
        }

        public static IEnumerable<T> GetInstances<T>(bool useCaching = false) {
            return GetInstances<T>(null, useCaching, new object[] { });
        }

        public static IEnumerable<T> GetInstances<T>(bool useCaching = false, params object[] args) {
            return GetInstances<T>(null, useCaching, args);
        }

        public static IEnumerable<T> GetInstances<T>(Func<Assembly, bool> predicate, bool useCaching = false) {
            return GetInstances<T>(predicate, useCaching, new object[] { });
        }

        public static IEnumerable<T> GetInstances<T>(Func<Assembly, bool> predicate, bool useCaching = false, params object[] args) {
            var ret = new List<T>();
                              
            foreach (var x in GetImplementations<T>(predicate, useCaching)) {
                if (!x.GetTypeInfo().IsAbstract) {
                    ret.Add((T)Activator.CreateInstance(x, args));
                }
            }

            return ret;
        }

        private static IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> predicate) {
            return predicate == null ? Assemblies : Assemblies.Where(predicate);
        }
    }
}
