using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float MoveThreshold = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Player.position.x - transform.position.x) > MoveThreshold)
        {
            var targetX = Mathf.Lerp(transform.position.x, Player.position.x, Time.deltaTime);
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }
}
