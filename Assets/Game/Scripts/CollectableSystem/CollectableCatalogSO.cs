using System.Collections.Generic;
using UnityEngine;

namespace CollectableSystem
{
    [CreateAssetMenu]
    public class CollectableCatalogSO : ScriptableObject
    {
       [SerializeField] private  CollectableEntry[] collectables;

       public Dictionary<CollectableType, CollectableObject> GetcollectableDictionary()
       {
           var dictionary = new Dictionary<CollectableType, CollectableObject>();
           for (int i = 0; i < collectables.Length; i++)
           {
               dictionary.Add(collectables[i].type, collectables[i].collectableObject);
           }

           return dictionary;
       }
       
    }

    [System.Serializable]
    public class CollectableEntry
    {
        public CollectableType type;
        public CollectableObject collectableObject;
    }
}
