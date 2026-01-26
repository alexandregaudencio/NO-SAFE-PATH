// using System;
// using Game.Attributes;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.Serialization;
//
// public abstract class Personagem : MonoBehaviour, IDamageable
// {
//     protected Rigidbody rigidBody;
//     [FormerlySerializedAs("Atributos")] [SerializeField] protected CharacterAttributes characterAttributes;
//     protected Vector3 direcaoMovimento = new Vector3(1, 0, 1);
//
//     public UnityEvent Morreu;
//     public event Action<int> VidaMudou;
//     private int vidaAtual;
//
//
//     private void OnEnable()
//     {
//         vidaAtual = characterAttributes.health;
//         Morreu.AddListener(DesativarObjeto);
//     }
//     private void OnDisable()
//     {
//         Morreu.RemoveListener(DesativarObjeto);
//     }
//
//     private void DesativarObjeto()
//     {
//         this.gameObject.SetActive(false);
//     }
//
//     protected virtual void Awake()
//     {
//         rigidBody = GetComponent<Rigidbody>();
//     }
//
//
//
//
//     protected void Mover(Vector3 direcao)
//     {
//         Vector3 velocidadeH = direcao.normalized * (characterAttributes.velocidade * Time.fixedDeltaTime);
//         rigidBody.linearVelocity = new Vector3(velocidadeH.x, rigidBody.linearVelocity.y, velocidadeH.z);
//     }
//
//
// }
