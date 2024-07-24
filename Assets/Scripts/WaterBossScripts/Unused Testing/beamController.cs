using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamController : MonoBehaviour
{
    public SpriteRenderer splash;
    private LineRenderer line;
    private RaycastHit2D hit;
    public float lineLength;
    public LayerMask layermask;
    private bool splashPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, Vector3.zero); // Start at the GameObject's local origin
        line.SetPosition(1, new Vector3(0, 5, 0));
        //splash = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.left * lineLength, Color.red);
        splash.gameObject.transform.position = transform.position;
        line.enabled = true;
        hit = Physics2D.Raycast(transform.position,Vector2.left,lineLength,layermask);
        if(hit)
        {
            if(splashPlaying == false)
            {
                splashPlaying = true;
                splash.enabled = true;
            }
            splash.gameObject.transform.position = hit.point + new Vector2(0,-0.15f);
            float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
            line.SetPosition(1, new Vector3(-distance+9,0,0));
        }
        else
        {
            line.SetPosition(1, new Vector3(-lineLength+9, 0, 0));
            splashPlaying = false;
            splash.enabled = false;
        }    
    }
}
