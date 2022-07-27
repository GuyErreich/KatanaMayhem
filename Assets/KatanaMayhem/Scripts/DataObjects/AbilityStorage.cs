using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Scripts.DataObjects
{
    [CreateAssetMenu(fileName = "New Ability Storage", menuName = "Data Objects/Dictionary/Ability Storage")]
    public class AbilityStorage : ScriptableObject
    {
        [SerializeField] private List<Colors.Types> keys;
        [SerializeField] private List<bool> values;

        public List<Colors.Types> Keys { get => this.keys; }
        public List<bool> Values { get => this.values; }
    }
}