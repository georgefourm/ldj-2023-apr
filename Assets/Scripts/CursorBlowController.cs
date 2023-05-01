using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorBlowController : MonoBehaviour
{
    private Rigidbody2D bomb;
    private SpriteRenderer spriteRenderer;
    public AudioSource fart1;
    public AudioSource fart2;
    public AudioSource fart3;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            bomb = player.GetComponent<Rigidbody2D>();
        }
        GameController.Instance.OnGamePaused += StopBlowing;
    }

    void StopBlowing()
    {
        StopFart();
        spriteRenderer.enabled = false;
    }

    void StartFart()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            if (!fart1.isPlaying) fart1.Play();
        }
        else if (rand == 1)
        {
            if (!fart2.isPlaying) fart2.Play();
        }
        else
        {
            if (!fart3.isPlaying) fart3.Play();
        }
    }

    void StopFart()
    {
        if (fart1.isPlaying) fart1.Stop();
        if (fart2.isPlaying) fart2.Stop();
        if (fart3.isPlaying) fart3.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !spriteRenderer.enabled && !GameController.Instance.GamePaused)
        {
            StartFart();
            spriteRenderer.enabled = true;
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.enabled = false;
            StopFart();
        }
        if (spriteRenderer.enabled)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var bombPos = bomb.transform.position;
            var direction = (bombPos - mousePos).normalized;
            transform.position = mousePos + direction * 0.5f;
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Rotate(Vector3.forward, 90f);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameController.Instance.OnGamePaused -= StopBlowing;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            bomb = player.GetComponent<Rigidbody2D>();
        }
    }
}
