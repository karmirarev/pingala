using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    [SerializeField] IOManager ioManager;
    public int digit;

    public void OnNumberClick()
    {
        ioManager.HandleNumberInput(digit);
    }
}
