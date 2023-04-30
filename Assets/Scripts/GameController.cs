using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject WinPanel;
    public GameObject GameWinPanel;
    public bool GamePaused = false;

    protected Vector2 StartPosition;
    protected GameObject player;

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
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePaused && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        GamePaused = false;
    }

    public void ResetGame()
    {
        StartPanel.SetActive(true);
        WinPanel.SetActive(false);
        player.transform.position = StartPosition;
        GamePaused = true;
    }

    public void WinLevel()
    {
        GamePaused = true;
        var currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currSceneIndex < SceneManager.sceneCount)
        {
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
        if(currSceneIndex < SceneManager.sceneCount)
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
