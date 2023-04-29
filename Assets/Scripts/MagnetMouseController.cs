using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMouseController : MonoBehaviour
{
    Rigidbody2D bomb;

    public void Start()
    {
        bomb = GetComponent<Rigidbody2D>();
    }
    public float ForceMagnitude = 8.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var bombPos = bomb.transform.position;
            var direction = (bombPos - mousePos).normalized;
            direction *= ForceMagnitude;
            bomb.AddForce(direction, ForceMode2D.Force);
        }
    }
}
