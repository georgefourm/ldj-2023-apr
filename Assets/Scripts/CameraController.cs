using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float XThreshold = 2.0f;
    public float YThreshold = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Player.position.x - transform.position.x) > XThreshold)
        {
            var targetX = Mathf.Lerp(transform.position.x, Player.position.x, Time.deltaTime);
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(Player.position.y - transform.position.y) > YThreshold)
        {
            var targetY = Mathf.Lerp(transform.position.y, Player.position.y, Time.deltaTime);
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }
}
