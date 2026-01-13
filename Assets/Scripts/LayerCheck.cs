using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    private Collider2D collider;

    public bool isTouchingGround;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        isTouchingGround = collider.IsTouchingLayers(groundLayer);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isTouchingGround = collider.IsTouchingLayers(groundLayer);
    }
}
