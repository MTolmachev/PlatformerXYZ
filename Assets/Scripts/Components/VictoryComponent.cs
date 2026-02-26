using Character;
using UnityEngine;
using TMPro;

namespace Components
{
    public class VictoryComponent : MonoBehaviour
    {
        [SerializeField] private Hero character;
        [SerializeField] private TMP_Text coinText;
        [SerializeField] private GameObject victoryWindow;
    
        public void Win()
        {
            Time.timeScale = 0;
            victoryWindow.SetActive(true);
            coinText.text = $"Coins got: {character.GetCoinsValue()} / 150";
        }
    }
}
