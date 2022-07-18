using UnityEngine;
using UnityEngine.Events;

namespace KatanaMayhem.Scripts
{
    public class EventHandlesHolder : Mono
    {
        public UnityEvent eventHandler;

        public void Trigger() {
            this.eventHandler.Invoke();
        }
    }
}