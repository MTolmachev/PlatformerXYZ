using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private int frameRate;
    [SerializeField] private bool loop;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private UnityEvent onComplete;
    
    private SpriteRenderer renderer;
    private float secondsPerFrame;
    private int currentSpriteIndex;
    private float nextFrameTime;
    private bool isPlaying = true;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        secondsPerFrame = 1f / frameRate;
        nextFrameTime = Time.time + secondsPerFrame;
    }

    private void Update()
    {
        if(!isPlaying || nextFrameTime > Time.time) return;
        if (currentSpriteIndex >= sprites.Length)
        {
            if (loop)
            {
                currentSpriteIndex = 0;
            }
            else
            {
                isPlaying = false;
                onComplete?.Invoke();
                return;
            }
        }
        renderer.sprite = sprites[currentSpriteIndex];
        nextFrameTime += secondsPerFrame;
        currentSpriteIndex++;
    }
}
