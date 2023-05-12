using UnityEngine;
using UnityEngine.Events;

namespace UI.Generic
{
    public class ExposeStartEvent : MonoBehaviour
    {
        [SerializeField] public UnityEvent OnStart = new();
    
        // Start is called before the first frame update
        void Start()
        {
            OnStart.Invoke();
        }
    }
}
