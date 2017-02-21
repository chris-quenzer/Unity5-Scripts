using UnityEngine;
using System.Collections;

public class matchAngleWithGround : MonoBehaviour {

    public Rigidbody2D rb2d;
    public objGroundCheck groundObj;
    float gravityStore;

    void Start()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        groundObj = transform.parent.GetComponentInChildren<objGroundCheck>();
        gravityStore = rb2d.gravityScale;
    }

    void Update()
    {
        int layerMask = 1 << 9;
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector2.up, 1, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector2.up, Color.red);

            if (hit.collider.tag == "Platform" && groundObj.grounded)
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                transform.parent.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
        if (!groundObj.grounded)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(transform.rotation.x, transform.rotation.y, 0));
            transform.parent.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(transform.parent.rotation.x, transform.parent.rotation.y, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform" && Mathf.Abs(transform.parent.localRotation.z) > 0)
        {
            if (groundObj.grounded)
            {
                rb2d.gravityScale = 0f;
            }
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Platform" && Mathf.Abs(transform.parent.localRotation.z) > 0)
        {
            if (groundObj.grounded)
            {
                //slipGrav = true;
                rb2d.gravityScale = 0f;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            //slipGrav = false;
            rb2d.gravityScale = gravityStore;
        }
    }
}
