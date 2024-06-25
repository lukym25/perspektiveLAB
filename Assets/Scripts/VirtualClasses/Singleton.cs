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
                    GameObject obj = new GameObject();
                    realInstance = obj.AddComponent<T>();
                }
                
                return realInstance;
            }
        }

        protected virtual void Awake()
        {
            if (realInstance == null)
            {
                realInstance = GetComponent<T>();
            }
            else
            {
                Destroy(this);
            }
        }
    }
}

