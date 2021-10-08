using UnityEngine;
using UnityEngine.UI;

public class MovementUIController : MonoBehaviour
{
    [SerializeField] private Button arrowLeft;
    [SerializeField] private Button arrowRight;
    [SerializeField] private Button arrowRotate;

    private void Start()
    {
        arrowLeft.onClick.AddListener(()=> EventManager.CallOnInputButton(Direction.Left));
        arrowRight.onClick.AddListener(()=> EventManager.CallOnInputButton(Direction.Right));
        arrowRotate.onClick.AddListener(()=> EventManager.CallOnInputButton(Direction.Rotation));
    }
}
