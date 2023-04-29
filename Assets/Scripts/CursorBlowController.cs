using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorBlowController : MonoBehaviour
{
    private Rigidbody2D bomb;
    private SpriteRenderer spriteRenderer;

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
        }
        if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.enabled = false;
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
