using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] [Range(1, 30)] private int frameRate = 10;
    [SerializeField] private UnityEvent<string> onComplete;
    [SerializeField] private AnimationClip[] clips;
    
    private new SpriteRenderer renderer;
    
    private float secondsPerFrame;
    private float nextFrameTime;
    private int currentFrame;
    private bool isPlaying = true;

    private int currentClip;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        secondsPerFrame = 1f / frameRate;

        StartAnimation();
    }

    private void OnBecameVisible()
    {
        enabled = isPlaying;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    public void SetClip(string clipName)
    {
        for (var i = 0 ; i < clips.Length; i++)
        {
            if (clips[i].Name == clipName)
            {
                currentClip = i;
                StartAnimation();
                return;
            }
        }
        
        enabled = isPlaying = false;
    }

    private void StartAnimation()
    {
        nextFrameTime = Time.time + secondsPerFrame;
        isPlaying = true;
        currentFrame = 0;
    }

    private void OnEnable()
    {
        nextFrameTime = Time.time + secondsPerFrame;
    }

    private void Update()
    {
        if(nextFrameTime >  Time.time) return;
        
        var clip  = clips[currentClip];
        
        if (currentFrame >= clip.Sprites.Length)
        {
            if (clip.Loop)
            {
                currentFrame = 0;
            }
            else
            {
                clip.OnComplete?.Invoke();
                onComplete?.Invoke(clip.Name);
                enabled = isPlaying = clip.AllowNextClip;
                if (clip.AllowNextClip)
                {
                    currentFrame = 0;
                    currentClip = (int) Mathf.Repeat(currentClip + 1, clips.Length);
                }
            }
            return;
        }
        renderer.sprite = clip.Sprites[currentFrame];
        
        nextFrameTime += secondsPerFrame;
        currentFrame++;
    }

    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private bool loop;
        [SerializeField] private bool allowNextClip;
        [SerializeField]  private UnityEvent onComplete;
        
        public string Name => name;
        public Sprite[] Sprites => sprites;
        public bool Loop => loop;
        public bool AllowNextClip => allowNextClip;
        public UnityEvent OnComplete => onComplete;
    }
}
