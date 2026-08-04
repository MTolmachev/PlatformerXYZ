using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{
    private string currentInput;
    [SerializeField] private float inputTimeToLive;
    [SerializeField] private CheatItem[] cheats;

    private float inputTime;
    
    private void Awake()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char inputChar)
    {
        currentInput += inputChar;
        inputTime = inputTimeToLive;
        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (CheatItem cheat in cheats)
        {
            if (currentInput == cheat.Name)
            {
                cheat.Action.Invoke();
                currentInput = string.Empty;
            }
                
        }
    }

    private void Update()
    {
        if(inputTime <= 0)
            currentInput = string.Empty;
        else
            inputTime -= Time.deltaTime;
    }
}

[Serializable]
public class CheatItem
{
    public string Name;
    public UnityEvent Action;
}
