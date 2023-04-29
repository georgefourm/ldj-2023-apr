using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    Rigidbody2D bomb;
    Animator animator;

    public void Start()
    {
        bomb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public float ForceMagnitude = 8.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !GameController.Instance.GamePaused)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var bombPos = bomb.transform.position;
            var direction = (bombPos - mousePos).normalized;
            direction *= ForceMagnitude;
            bomb.AddForce(direction, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                animator.SetTrigger("Explode");
                break;
            case "Goal":
                GameController.Instance.WinLevel();
                break;
        }
    }

    void Restart()
    {
        GameController.Instance.ResetGame();
    }
}
