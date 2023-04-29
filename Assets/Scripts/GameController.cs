using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject WinPanel;
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
        Time.timeScale = 1;
        StartPanel.SetActive(false);
        GamePaused = false;
    }

    public void ResetGame()
    {
        Time.timeScale = 0;
        StartPanel.SetActive(true);
        WinPanel.SetActive(false);
        player.transform.position = StartPosition;
        GamePaused = true;
    }

    public void WinLevel()
    {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
        GamePaused = true;
    }

    public void NextLevel()
    {
        var currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currSceneIndex < SceneManager.sceneCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
