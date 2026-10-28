using Creatures;
using Creatures.Character;
using UnityEngine;
using TMPro;

namespace Components
{
    public class VictoryComponent : MonoBehaviour
    {
        // [SerializeField] private Hero character;
        // [SerializeField] private TMP_Text coinText;
        // [SerializeField] private GameObject victoryWindow;
    
        public void Win()
        {
            Time.timeScale = 0f;
            var window = Resources.Load<GameObject>("UI/VictoryWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
            //coinText.text = $"Coins got: {character.GetCoinsValue()}";
        }
    }
}
