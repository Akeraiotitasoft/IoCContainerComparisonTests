using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This code is based on
// https://github.com/unitycontainer/aspnet-mvc/blob/master/src/UnityPerRequestHttpModule.cs

namespace IoCContainerComparisonTests.Mock
{
    internal class CustomModule
    {
        private static readonly object ModuleKey = new object();
        internal static object GetValue(object lifetimeManagerKey)
        {
            var dict = GetDictionary(CustomScope.Current);

            if (dict != null)
            {
                if (dict.TryGetValue(lifetimeManagerKey, out var obj))
                {
                    return obj;
                }
            }

            return Unity.Lifetime.LifetimeManager.NoValue;
        }

        internal static void SetValue(object lifetimeManagerKey, object value)
        {
            var dict = GetDictionary(CustomScope.Current);

            if (dict == null)
            {
                dict = new Dictionary<object, object>();

                CustomScope.Current.Bag[ModuleKey] = dict;
            }

            dict[lifetimeManagerKey] = value;
        }

        /// <summary>
        /// Disposes the resources used by this module.
        /// </summary>
        public void Dispose()
        {
        }

        private static Dictionary<object, object> GetDictionary(CustomScope scope)
        {
            if (scope == null)
            {
                throw new InvalidOperationException(
                    "The CustomScopeLifetimeManager can only be used in the context of an CustomScope.  Possible causes for this error are not entering a custom scope.");
            }

            // this is safe to keep adding the event.
            // see the code for CustomScope to see why.
            scope.NotifyDisposed += Scope_NotifyDisposed;

            var dict = (Dictionary<object, object>)scope.Bag[ModuleKey];

            return dict;
        }

        private static void Scope_NotifyDisposed(object obj)
        {
            CustomScope scope = (CustomScope)obj;
            var dict = GetDictionary(scope);

            if (dict != null)
            {
                foreach (var disposable in dict.Values.OfType<IDisposable>())
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
