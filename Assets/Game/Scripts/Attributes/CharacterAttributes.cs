using UnityEngine;


namespace Game.Attributes
{


    [CreateAssetMenu(fileName = "atributos")]
    public class CharacterAttributes : ScriptableObject
    {
        public int dano = 10;
        public int vida = 1;
        public float velocidade = 100;

    }
}
