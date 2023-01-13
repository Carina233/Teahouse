using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMove : MonoBehaviour
{
    public float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Vector2 vec = transform.position;
            this.transform.position = new Vector3(vec.x, vec.y - speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector2 vec = transform.position;
            this.transform.position = new Vector3(vec.x, vec.y + speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector2 vec = transform.position;
            this.transform.position = new Vector3(vec.x - speed, vec.y);

        }

        if (Input.GetKey(KeyCode.D))
        {
            Vector2 vec = transform.position;
            this.transform.position = new Vector3(vec.x + speed, vec.y);
        }
    }
}
