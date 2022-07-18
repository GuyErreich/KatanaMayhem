using UnityEngine;
using UnityEngine.Events;

namespace KatanaMayhem.Scripts
{
    public class EventHandlesHolder : MonoBehaviour
    {
        public UnityEvent eventHandler;

        public void Trigger() {
            this.eventHandler.Invoke();
        }
    }
}