using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosMove : MonoBehaviour
{
    public Camera camera;
    private GameObject mosquito;
    private GameObject head;
    private float risefallSpeed = 4f;
    private float moveSpeed = 5f;
    private float flySpeed = 7f;
    private float rotSpeed = 5f;
    private float rotLR = 0;
    private float rotUD = -90f;
    private bool isFly;
    private bool canBit;

    // Start is called before the first frame update
    void Start()
    {
        mosquito = transform.GetChild(0).gameObject;
        head = transform.GetChild(0).GetChild(1).gameObject;
        isFly = true;
        canBit = false;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rotateMouse();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            isFly = false;
        }
        if(collision.gameObject.tag=="Human")
        {
            isFly = false;
            canBit = true;
        }
    }

    private void rotateMouse()
    {
        rotLR += rotSpeed * Input.GetAxis("Mouse X");
        rotUD += rotSpeed * Input.GetAxis("Mouse Y") * -1; 
        
        // Up and Down rotate limit
        rotUD = Mathf.Clamp(rotUD, -150f, -60f);

        head.transform.eulerAngles = new Vector3(rotUD, rotLR, -180f);
        transform.eulerAngles = new Vector3(0f, rotLR, 0f);
    }

    private void move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if (isFly)
            {
               transform.Translate(((head.transform.rotation.x + 90f) * Vector3.forward) * flySpeed * Time.smoothDeltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.smoothDeltaTime);
            }
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.up * risefallSpeed * Time.smoothDeltaTime);
            isFly = true;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * risefallSpeed * Time.smoothDeltaTime);
        }
    }
}
