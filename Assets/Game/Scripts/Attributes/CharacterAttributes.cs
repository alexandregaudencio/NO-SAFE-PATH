using UnityEngine;
using UnityEngine.Serialization;


namespace Game.Attributes
{


    [CreateAssetMenu(fileName = "atributos")]
    public class CharacterAttributes : ScriptableObject
    {
        [FormerlySerializedAs("dano")] public int damage = 10;
        [FormerlySerializedAs("vida")] public int health = 1;
        [FormerlySerializedAs("velocidade")] public float speed = 100;

    }
}
