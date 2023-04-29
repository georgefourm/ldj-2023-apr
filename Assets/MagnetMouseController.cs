using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMouseController : MonoBehaviour
{
    public Rigidbody2D Bomb2;
    public float forceMagnitude = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var bombPos = Bomb2.transform.position;
            var direction = (bombPos - mousePos).normalized;
            direction *= forceMagnitude;
            Bomb2.AddForce(direction, ForceMode2D.Force);
        }
    }
}
