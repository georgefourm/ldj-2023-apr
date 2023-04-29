using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorBlowController : MonoBehaviour
{
    private Rigidbody2D bomb;
    private SpriteRenderer spriteRenderer;
    public AudioSource blow1;
    public AudioSource blow2;
    public AudioSource blow3;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.enabled = true;
            int rand = Random.Range(0, 10);
            if (rand == 0)
            {
                blow1.Play();
            }
            else if (rand <= 4)
            {
                blow2.Play();
            }
            else
            {
                blow3.Play();
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.enabled = false;
            if (blow1.isPlaying) blow1.Stop();
            if (blow2.isPlaying) blow2.Stop();
            if (blow3.isPlaying) blow3.Stop();
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
        if (Input.GetMouseButton(0))
        {
            //
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
