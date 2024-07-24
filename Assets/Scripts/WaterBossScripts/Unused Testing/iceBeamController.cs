using UnityEngine;

public class BeamController : MonoBehaviour
{
    public SpriteRenderer splash;
    public float rotationSpeed; // Degrees per second for beam rotation
    private LineRenderer line;
    private RaycastHit2D hit;
    public float lineLength; // Default length of the beam
    public LayerMask layermask;
    private bool splashPlaying = false;

    public float maxAngle = 60f;
    private float currentAngle = -60f; // Current rotation angle of the beam

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero); // Assuming the beam originates from the GameObject's position
        line.SetPosition(1, new Vector3(1, 0, 0));
        // Assuming the splash SpriteRenderer is correctly assigned in the Inspector
    }

    void Update()
    {
        RotateBeam(); // Rotate the beam each frame

        // Perform the raycast from the current beam position in the left direction
        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), lineLength, layermask);

        Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector2.left) * lineLength, Color.red);

        if (hit.collider != null)
        {
            // Hit detected, adjust beam length and position splash
            line.enabled = true;
            AdjustBeam(hit.distance);
            AdjustSplash(hit.point, true);
        }
        else
        {
            // No hit detected, extend the beam to its full length
            AdjustBeam(lineLength);
            AdjustSplash(transform.position, false); // Disable splash when no hit is detected
        }
    }

    void RotateBeam()
    {
        currentAngle += rotationSpeed * Time.deltaTime;
        currentAngle %= 360; // Ensure the angle stays within 0-360 degrees

        if (currentAngle > maxAngle)
        {
            currentAngle = maxAngle;
            //rotatingClockwise = false; // Reverse direction
        }

        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void AdjustBeam(float distance)
    {
        // Offset by 9 units towards the beam's starting point to adjust for specific positioning needs
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, new Vector3(-distance, 0, 0));
    }

    void AdjustSplash(Vector2 position, bool isActive)
    {
        if (isActive )
        {
            splashPlaying = true;
            splash.enabled = true;
            splash.gameObject.transform.position = position + new Vector2(0, -0.15f); // Adjust splash position
        }
        else if (!isActive)
        {
            splashPlaying = false;
            splash.enabled = false;
        }
    }
}