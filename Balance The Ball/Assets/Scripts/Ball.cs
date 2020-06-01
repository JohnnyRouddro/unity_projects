using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Rigidbody body;
    public Transform startPoint;
    public Image dragBaseImg;
    public GameObject line;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float bottomPoint = -10f;
    [SerializeField] private float forceFieldIntensity = 50f;
    [SerializeField] private float lineWidth = 0.01f;

    private Vector3 lastVelocity = Vector3.zero;
    private Vector3 forceFieldDirection = Vector3.forward;
    private bool insideForceField = false;
    private Vector2 dragBase = Vector2.zero;
    private bool mousePressed = false;

    private void Awake() {
        dragBaseImg.enabled = false;
        line.GetComponent<Image>().enabled = false;
    }

    private void Start() {
        transform.position = startPoint.position;
    }

    void FixedUpdate()
    {
        //Controlling the player with keyboard
        // if (Input.GetKey("right"))
        // {
        //     body.AddForce(speed, 0, 0);
        // }
        // else if (Input.GetKey("left"))
        // {
        //     body.AddForce(-speed, 0, 0);
        // }
        // if (Input.GetKey("up"))
        // {
        //     body.AddForce(0, 0, speed);
        // }
        // else if (Input.GetKey("down"))
        // {
        //     body.AddForce(0, 0, -speed);
        // }


        //Controlling the player with mouse/touch input
        if (Input.GetMouseButton(0))
        {

            if (!mousePressed) //Making sure the dragBase is set once
            {
                mousePressed = true;
                dragBase = Input.mousePosition;
                dragBaseImg.enabled = true;
                line.GetComponent<Image>().enabled = true;
                dragBaseImg.transform.position = dragBase;
            }
            else
            {
                Vector2 mousePos = Input.mousePosition;


                //Drawing the line from dragBase to mousePos
                line.transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.right , mousePos - dragBase));
                line.transform.localScale = new Vector3(Vector2.Distance(mousePos, dragBase)/Screen.width, lineWidth, 1);


                //Moving the player according to the touch / mouse drag
                body.AddForce((mousePos-dragBase).normalized.x * speed, 0, (mousePos-dragBase).normalized.y * speed);

            }

        }
        else
        {
            mousePressed = false;
            dragBaseImg.enabled = false;
            line.GetComponent<Image>().enabled = false;
            line.transform.localScale = Vector3.zero;
        }


        //Reset position to starting point if player falls off the floor
        if (transform.position.y < bottomPoint)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            Start();
        }


        //Getting affected by the force field / boost
        if (insideForceField)
        {
            body.AddForce(forceFieldDirection * forceFieldIntensity);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Goal")
        {
            Invoke("Stop", 0.5f);
        }

        if (other.gameObject.tag == "ForceField")
        {
            insideForceField = true;
            forceFieldDirection = other.transform.forward;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "ForceField")
        {
            insideForceField = false;
        }
    }

    private void Stop() {
        body.isKinematic = true;
    }
}
