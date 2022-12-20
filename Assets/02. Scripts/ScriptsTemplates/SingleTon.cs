using UnityEngine;

namespace BaseFrame
{
    public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T m_instance;
        public static T instance
        {
            get
            {
                if (m_instance == null)
                {
                    GameObject obj;
                    obj = GameObject.Find(typeof(T).Name);
                    if (obj == null)
                    {
                        obj = new GameObject(typeof(T).Name);
                        m_instance = obj.AddComponent<T>();
                    }
                    else
                    {
                        m_instance = obj.GetComponent<T>();
                    }
                }
                return m_instance;
            }
        }

        virtual public void Awake()
        {
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
        }
    }
}
