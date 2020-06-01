using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    public Canvas canvas;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        FadeIn();
    }

    public GameObject panel;
    public Text timerText;
    public Text levelText;

    private float time = 0f;
    private bool timePaused = false;
    private int level = 1;
    private string timeTaken = "0s";

    private int second = 0;
    private int minute = 0;
    private int hour = 0;

    public void FadeOut() {
        GetComponent<Animator>().Play("LevelEnd");
    }

    public void FadeIn(){
        GetComponent<Animator>().Play("LevelStart");
    }

    public void Back() {
        Debug.Log("asfd");
        // SceneManager.LoadScene(0);
        // Destroy(gameObject);
    }

    private void FixedUpdate() {
        if (!timePaused)
        {
            time += Time.deltaTime;
            second = (int)Mathf.Round(time);

            if (second < 60)
            {
                timeTaken = second.ToString() + "s";
                timerText.text = timeTaken;
            }
            else if(second < 3600)
            {
                minute = second / 60;
                second = second % 60;

                timeTaken = minute.ToString() + "m" + second.ToString() + "s";
                timerText.text = timeTaken;
            }
            else
            {
                minute = second / 60;
                second = second % 60;
                hour = minute / 60;
                minute = minute % 60;

                timeTaken = hour.ToString() + "h" +  minute.ToString() + "m" + second.ToString() + "s";
                timerText.text = timeTaken;
            }
        }
    }

    public void NextLevel() {
        level++;
        levelText.text = "Stage " + level.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseTime(){
        timePaused = true;
    }

    public void ResumeTime(){
        timePaused = false;
    }

    public string TimeTaken(){
        return timeTaken;
    }

    

}
