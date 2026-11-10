using UnityEngine;

namespace Utils
{
    public static class GameObjectExtensions
    {
        public static TInterfaceType GetInterface<TInterfaceType>(this GameObject gameObject)
        {
            var components = gameObject.GetComponents<Component>();
            foreach (var component in components)
            {
                if (component is TInterfaceType interfaceComponent)
                    return interfaceComponent;
            }

            return default;
        }
    }
}
