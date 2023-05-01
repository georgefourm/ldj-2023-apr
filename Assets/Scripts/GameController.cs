using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject WinPanel;
    public GameObject GameWinPanel;
    public bool GamePaused = false;

    public delegate void PauseStateDelegate();
    public event PauseStateDelegate OnGamePaused;
    public event PauseStateDelegate OnGameResumed;

    protected Vector2 StartPosition;
    protected GameObject player;
    protected TimerController timer;

    public static GameController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartPosition = player.transform.position;
        timer = GetComponentInChildren<TimerController>();
        PauseGame();
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        if (GamePaused && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    public void PauseGame()
    {
        GamePaused = true;
        if(OnGamePaused != null)
        {
            OnGamePaused();
        }
    }

    public void ResumeGame()
    {
        GamePaused = false;
        if(OnGameResumed != null)
        {
            OnGameResumed();
        }
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        ResumeGame();
    }

    public void ResetGame()
    {
        StartPanel.SetActive(true);
        WinPanel.SetActive(false);
        player.transform.position = StartPosition;
    }

    public void WinLevel()
    {
        PauseGame();
        var currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            var txt = WinPanel.GetComponentInChildren<TMP_Text>();
            txt.text = string.Format("Time: {0}", timer.GetFormattedTime());
            WinPanel.SetActive(true);
        }
        else
        {
            GameWinPanel.SetActive(true);
        }
    }

    public void NextLevel()
    {
        var currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
