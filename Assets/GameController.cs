using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject StartPanel;

    protected Vector2 StartPosition;
    protected bool paused =  false;

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
        StartPosition = player.transform.position;
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        StartPanel.SetActive(false);
        paused = false;
    }

    public void ResetGame()
    {
        Time.timeScale = 0;
        StartPanel.SetActive(true);
        player.transform.position = StartPosition;
        paused = true;
    }
}
