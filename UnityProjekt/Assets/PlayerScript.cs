using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float jumpForceForward = 600.0f;
    public float jumpForceUpward = 300.0f;
    public float jumpTorgue = 100.0f;

    public float currentForwardSpeed = 0.0f;
    public float maxSpeed = 5.0f;
    public float forwardSpeed = 50f;
    public float rotation = 2.0f;
    public float maxFallSpeed = 10.0f;
    public float speedChange = 1.0f;

    public float time = 0.0f;
    GUIStyle labelStyle;

    private float lastPlayerX = 0.0f;

    public static float moved = 0.0f;
    public static float currentMovement = 0.0f;

    public bool gameOver = false;

    void Start()
    {
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 20;
        labelStyle.normal.textColor = Color.white;
    }
    void Update()
    {
        if (gameOver)
        {

        }
        else
        {
            // rigidbody.AddForce(speedChange * Vector3.right);
            time += Time.deltaTime;

            currentMovement  = transform.position.x - lastPlayerX;
            moved += currentMovement;

            lastPlayerX = transform.position.x;
            if (Input.GetMouseButtonDown(0))
            {
                rigidbody.AddForce(jumpForceForward * rigidbody.mass * Vector3.right + jumpForceUpward * rigidbody.mass * Vector3.up);
                rigidbody.AddTorque(-jumpTorgue * rigidbody.mass * Vector3.forward);
            }

            if (rigidbody.velocity.magnitude < 0.01f)
            {
                gameOver = true; 
            }
        }
       
        if (Input.GetButtonDown("Reset"))
        {
            transform.position = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            time = 0.0f;
            moved = 0.0f;
            gameOver = false;
        }

        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 1000, 10), time.ToString(), labelStyle);
        GUI.Label(new Rect(0, labelStyle.fontSize, 1000, 10), moved.ToString(), labelStyle);

        GUI.Label(new Rect(0, labelStyle.fontSize*2.0f, 1000, 10), (moved/time).ToString(), labelStyle);

        if (gameOver)
        {
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, 1000, 10), "Punkte: " + (moved / time).ToString(), labelStyle);
        }
    }

	// Update is called once per frame
    void FixedUpdate()
    {
        //currentVelocity = rigidbody2D.velocity;
        //currentVelocity.x += speedChange * Time.deltaTime;
        

        rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody.velocity.y, -maxFallSpeed, maxFallSpeed), rigidbody.velocity.z);

        //currentVelocity = Vector2.ClampMagnitude(currentVelocity, maxSpeed);
        //rigidbody2D.velocity = currentVelocity;
        

        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 1) * rigidbody2D.velocity.y * rotation);
	}
}
