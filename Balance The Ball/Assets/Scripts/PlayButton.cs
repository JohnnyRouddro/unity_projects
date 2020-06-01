using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Global g;

    private void Start() {
        g = FindObjectOfType<Global>();
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void Replay() {
        Destroy(g.gameObject);
        SceneManager.LoadScene(1);
    }
}
