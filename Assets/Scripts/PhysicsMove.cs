using UnityEngine;
using System.Collections;

public class PhysicsMove : MonoBehaviour {

    //Variable Decloration
    public float moveForce = 100f;
    public float turnForce = 50f;

    public bool OnGround
    {
        get
        {
            return onGround;
        }
        set
        {
            onGround = value;
        }
    }

    private Rigidbody rigBody;

    private bool onGround = false;

    // Use this for initialization
    void Start () {
        rigBody = gameObject.GetComponent<Rigidbody>();
        if(rigBody == null)
        {
            Debug.LogError("No RigidBody attached to this object!");
        }
        rigBody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        onGround = CheckIsGrounded();

        //Keybinds
        //If player is on ground they are able to move.
        if (onGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigBody.AddForce(-transform.forward * moveForce, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rigBody.AddForce(transform.forward * moveForce, ForceMode.Acceleration);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigBody.AddTorque(0, -turnForce, 0f, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigBody.AddTorque(0, turnForce, 0f, ForceMode.Acceleration);
        }
    }

    //Returns t/f if player is on the ground
    bool CheckIsGrounded()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, -transform.up, out hitInfo, 1f))
        {
            if (hitInfo.collider.CompareTag("Ground"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + (-transform.up * 1f));
    }
}
