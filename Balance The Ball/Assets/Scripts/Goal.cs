using UnityEngine;

public class Goal : MonoBehaviour
{
    public Global g;

    private void Start() {
        g = FindObjectOfType<Global>();
        g.canvas.GetComponent<Canvas>().sortingOrder = 15;
        g.FadeIn();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Ball")
        {
            GetComponent<Animator>().Play("Goal");
            g.Invoke("FadeOut", 0.5f);
        }
    }
}
