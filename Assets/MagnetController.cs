using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    public Transform TopMagnet,RightMagnet;
    public Rigidbody2D Bomb;

    public float TopMagnetStrength = 2f;
    public float RightMagnetStrength = 0.5f;
    public float TopMagnetSpeed = 0.5f;
    public float RightMagnetSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            TopMagnet.Translate(new Vector3(TopMagnetSpeed * Input.GetAxis("Horizontal"), 0));
        }
        if (Input.GetAxis("Vertical") != 0 && isInTopMagnetRange())
        {
            Bomb.AddForce(new Vector2(0, TopMagnetStrength * Input.GetAxis("Vertical")),ForceMode2D.Impulse);
        }
        if(Input.mouseScrollDelta.magnitude > 0)
        {
            RightMagnet.Translate(Input.mouseScrollDelta * RightMagnetSpeed, 0);
        }
        if (Input.GetMouseButton(0) && isInBottomMagnetRange())
        {
            Bomb.AddForce(new Vector2(-1*RightMagnetStrength,0), ForceMode2D.Impulse);
        }
        if (Input.GetMouseButton(1) && isInBottomMagnetRange())
        {
            Bomb.AddForce(new Vector2(RightMagnetStrength, 0), ForceMode2D.Impulse);
        }
    }

    bool isInTopMagnetRange()
    {
        var leftEdge = TopMagnet.transform.position.x - TopMagnet.transform.localScale.x / 2f;
        var rightedge = TopMagnet.transform.position.x + TopMagnet.transform.localScale.x / 2f;
        return Bomb.transform.position.x >= leftEdge && Bomb.transform.position.x <= rightedge;
    }

    bool isInBottomMagnetRange()
    {
        var topEdge = RightMagnet.transform.position.y + TopMagnet.transform.localScale.y / 2f;
        var bottomEdge = RightMagnet.transform.position.y - TopMagnet.transform.localScale.y / 2f;
        return Bomb.transform.position.y >= bottomEdge && Bomb.transform.position.y <= topEdge;
    }
}
