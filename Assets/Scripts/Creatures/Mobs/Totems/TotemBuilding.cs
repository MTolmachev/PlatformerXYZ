using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures.Mobs.Totems
{
    public class TotemBuilding : MonoBehaviour
    {
        [SerializeField] private Sprite[] botSprites;
        [SerializeField] private Sprite[] topSprites;
        
        [SerializeField] private SpriteRenderer[] totems;


        private int rand;
        
        private void Start()
        {
            foreach (var totem in totems)
            {
                rand = Random.Range(0, totems.Length);
                totem.sprite = botSprites[rand];
            }
        }
    }
}