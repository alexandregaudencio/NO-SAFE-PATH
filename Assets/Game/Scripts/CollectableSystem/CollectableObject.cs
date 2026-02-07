using UnityEngine;

namespace CollectableSystem 
{

    public class CollectableObject : MonoBehaviour, ICollectable
    {
        [SerializeField] private Buff buff;
        public void Collect(ICollector collector)
        {
            collector.ApplyEffect(buff);
            Destroy(this.gameObject);
        }
        
        
        
    }
}