using UnityEngine;
using System.Collections;

public class beamTester : MonoBehaviour
{
    public SpriteRenderer splash;
    public float rotationSpeed; // Degrees per second for beam rotation
    private LineRenderer line;
    private RaycastHit2D hit;
    public float lineLength; // Default length of the beam
    public LayerMask layermask;
    public int beamDamage = 10;
    private bool splashPlaying = false;
    private bool isBeamActive = false;
    public float maxAngle = 60f;
    private float currentAngle; // Current rotation angle of the beam
    public GameObject player;
    public float beamDuration = 5f; // The beam lasts for 5 seconds
    public float beamRotationSpeed = 20f; // Degrees per second, adjust as needed

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.positionCount = 2;
        //player = GameObject.FindGameObjectWithTag("Player");
        InitializeBeam();
        DeactivateBeam();
    }

    private void InitializeBeam()
    {



        line.SetPosition(0, Vector3.zero); // Assuming the beam originates from the GameObject's position
        line.SetPosition(1, new Vector3(1, 0, 0));
        this.enabled = true;
        // Assuming the splash SpriteRenderer is correctly assigned in the Inspector
        // Any other initialization code
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //ActivateBeam();
        }


    }
    void PerformRaycastAndAdjustBeam(Vector2 directionToPlayer)
    {
        hit = Physics2D.Raycast(transform.position, transform.up, lineLength, layermask);
        Debug.DrawLine(transform.position, transform.position + (Vector3)directionToPlayer * lineLength, Color.red);

        if (hit.collider != null)
        {
            //line.enabled = true;
            float distanceToHit = (hit.point - (Vector2)transform.position).magnitude;
            line.SetPosition(1, new Vector3(0, distanceToHit, 0));
            AdjustSplash(hit.point, true);
            if(hit.collider.CompareTag("Player"))
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                // Deal damage
                damageable.OnHit(beamDamage);
            }
            
        }
        else
        {
            line.SetPosition(1, new Vector3(0, lineLength, 0));
            AdjustSplash(transform.position, false); // Consider how you want to handle the splash when there's no hit
        }
    }
    public void ActivateBeam()
    {
        Debug.Log("ActivateBeam method called");
        getStartAngle();
        StartCoroutine(BeamActiveRoutine());
    }
    void getStartAngle()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        currentAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void DeactivateBeam()
    {
        isBeamActive = false;
        line.enabled = false; // This will make the LineRenderer disappear
        splash.enabled = false; // Also hide the splash if it's active
        // Optionally, reset other visual effects or states as needed
    }
    void AimTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.GetComponentInChildren<Collider2D>().bounds.center - transform.position).normalized;
            float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90; // Adjust based on beam orientation
            float step = beamRotationSpeed * Time.deltaTime;
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, step);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            PerformRaycastAndAdjustBeam(directionToPlayer);
        }
    }
    IEnumerator BeamActiveRoutine()
    {
        yield return new WaitForSeconds(1f);
        isBeamActive = true;
        line.enabled = true;
        splash.enabled = true;
        this.enabled = true;
        float endTime = Time.time + beamDuration;

        while (Time.time < endTime)
        {
            AimTowardsPlayer();
            yield return null;
        }

        DeactivateBeam();
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
        if (currentAngle == maxAngle)
        {
            currentAngle = -60f;
            this.enabled = false;
            line.enabled = false;
            splash.enabled = false;
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
        if (isActive)
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