using UnityEngine;

namespace Lukas.MyClass
{
    public class Singleton<T> : MonoBehaviour
        where T : Component
    {
        private static T realInstance;
        public static T Instance
        {
            get
            {
                if (realInstance == null)
                {
                    T[] objects = FindObjectsOfType(typeof(T)) as T[];
                    if (objects?.Length > 0)
                    {
                        realInstance = objects[0];
                    }
                    else
                    {
                        GameObject obj = new GameObject();
                        realInstance = obj.AddComponent<T>();
                    }
                }
                
                return realInstance;
            }
        }
    }
}

