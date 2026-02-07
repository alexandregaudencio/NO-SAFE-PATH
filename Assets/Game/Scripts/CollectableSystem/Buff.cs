using System;
using UnityEngine;

namespace CollectableSystem
{
    [Serializable]
    public class Buff
    {
        [field: SerializeField, Min(-1)] public float Duration { get; private set; } = 5;
        [field:SerializeField] public int Health { get; private set; } = 0;
        [field: SerializeField] public float Speed { get; private set; } = 0;
        [field: SerializeField] public int Damage { get; private set; } = 0;
    }

}