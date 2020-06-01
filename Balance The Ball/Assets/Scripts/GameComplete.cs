using UnityEngine;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour
{
    public Global g;
    public Text timeTaken;

    void Start()
    {
        g = FindObjectOfType<Global>();
        timeTaken.text = g.TimeTaken();
        g.canvas.GetComponent<Canvas>().sortingOrder = 0;
    }
}
