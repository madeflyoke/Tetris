using UnityEngine;
using UnityEngine.UI;

public class StartGameUIController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    public void Initialize()
    {      
        startButton.onClick.AddListener(() => { EventManager.CallOnChangeGameState(GameState.Gameplay); });
    }
}
