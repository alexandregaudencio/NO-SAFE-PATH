using CollectableSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;


namespace Game.Attributes
{


    [CreateAssetMenu(fileName = "atributos")]
    public class CharacterAttributes : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; } =10;
       [field: SerializeField]  public int Health { get; private set; }= 1;
       [field: SerializeField]  public float Speed { get; private set; } = 100;
       
        public async void BuffAttributes(Buff buff)
        {
            Damage += buff.Damage;
            Health += buff.Health;
            Speed += buff.Speed;
            await UniTask.Delay((int)(buff.Duration * 1000));
            DebuffAttributes(buff);
        }

        private void DebuffAttributes(Buff buff)
        {
            Damage -= buff.Damage;
            Health -= buff.Health;
            Speed -= buff.Speed;
        }
        
        
    }
}
