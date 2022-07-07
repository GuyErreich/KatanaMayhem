using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Scripts.DataObjects
{
    [CreateAssetMenu(fileName = "New Slime Storage", menuName = "Data Objects/Dictionary/Slime Storage")]
    public class SlimeStorage : ScriptableObject
    {
        [SerializeField] private List<Colors.Types> keys;
        [SerializeField] private List<GameObject> values;

        public List<Colors.Types> Keys { get => this.keys; }
        public List<GameObject> Values { get => this.values; }
    }
}