using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    public Global g;

    private void Start() {
        g = FindObjectOfType<Global>();
    }

    public void Back() {
        Destroy(g.gameObject);
        SceneManager.LoadScene(0);
    }
}
