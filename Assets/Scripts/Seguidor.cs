using System;
using UnityEngine;

public class Seguidor : MonoBehaviour
{
   [SerializeField] private Transform alvo;
   private Vector3 posicaoPadrao;

   private void Awake()
   {
      posicaoPadrao = (transform.position - alvo.position);
   }

   private void Update()
   {
        transform.position = alvo.position + posicaoPadrao;

   }
}
