using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BallDragScript : MonoBehaviour
{
    public Animator animator;
    
    public BallDragScript instance;
    public float height = -3.4f;
    //public float BallPower;
    //public Rigidbody2D rb;
    public Rigidbody2D rb2;
    public float jumpforce;
    public bool isOnPlatform2 = false;

    //public Vector2 minimumpower;
    //public Vector2 maximumpower;

    //TrajectoryLine tl;
    bool facingRight = true;

    Camera cam;
    //Vector2 ballforce;
    //Vector3 startpoint;
    //Vector3 endpoint;

    Vector3 camOffset = new Vector3(0, 0, 10);

    public bool isGrounded = false;
    public bool isOnPlatform = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        //tl = GetComponent<TrajectoryLine>();
        rb2 = GetComponent<Rigidbody2D>();
        jumpforce = 1000;
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    GameObject box = GameObject.Find("Box(Clone)");


    //    if (collision.gameObject.name == "Box(Clone)" && box.GetComponent<BoxScript>().ignoreCollision == false && isOnPlatform == false)
    //    {
    //        isOnPlatform2 = true;
    //        box.GetComponent<BoxScript>().ignoreCollision = true;
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}
    private void Update()
    {
        
        float vert_speed = rb2.velocity.y;
        float horiz_speed = rb2.velocity.x;
        animator.SetFloat("y_velocity", vert_speed);
        animator.SetBool("Is_Grounded", isGrounded);

        if (horiz_speed < 0 && facingRight &&!isGrounded) flip();
        if (horiz_speed > 0 && !facingRight &&!isGrounded) flip();

        if (isGrounded)
        {
            animator.SetBool("Falling", true);
        }
        if (isOnPlatform) return;


        //GameObject box = GameObject.Find("Box(Clone)");

        if (Input.touchCount > 0 && Input.GetTouch(0).phase==TouchPhase.Began && isGrounded == true && isOnPlatform2 == false)
            {
            Debug.Log(isOnPlatform);
                //animator.SetBool("Is_Charging", true);
                //startpoint = cam.ScreenToWorldPoint(Input.mousePosition);
                //startpoint.z = 15;
                //tl.lr.numCapVertices = 10;
                rb2.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Force);

        }

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded == true)
        //    {
        //        Vector3 currentpoint = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        //        currentpoint.z = 15;
        //        tl.RenderLine(startpoint, currentpoint);

        //}

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded == true)
        //    {
        //        endpoint = cam.ScreenToWorldPoint(Input.mousePosition) + camOffset;
        //        endpoint.z = 15;

        //        ballforce = new Vector2(Mathf.Clamp(startpoint.x - endpoint.x, minimumpower.x, maximumpower.x),
        //            Mathf.Clamp(startpoint.y - endpoint.y, minimumpower.y, maximumpower.y));
        //        rb.AddForce(ballforce * BallPower, ForceMode2D.Impulse);

        //   tl.endline();

        //    }
        if (!isGrounded)
        {
            animator.SetBool("Is_Charging", false);
        }
        if (!isGrounded && (vert_speed == 0))
        {
            animator.SetBool("Falling", true);
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
