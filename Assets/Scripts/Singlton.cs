using UnityEngine;

namespace Lukas.MyClass
{
    public class Singelton<T> : MonoBehaviour
        where T : Component
    {
        private static T RealInstance;
        public static T Instance
        {
            get
            {
                if (RealInstance == null)
                {
                    T[] objects = FindObjectsOfType(typeof(T)) as T[];
                    if (objects?.Length > 0)
                    {
                        RealInstance = objects[0];
                    }
                    else
                    {
                        GameObject obj = new GameObject();
                        RealInstance = obj.AddComponent<T>();
                    }
                }
                
                return RealInstance;
            }
        }
    }
}

