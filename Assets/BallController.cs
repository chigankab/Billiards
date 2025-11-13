using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxForce = 20f;
    public float minForce = 5f;
    public float forceStep = 1f;

    private float currentForce = 10f;
    private Rigidbody rb;
    private Renderer ballRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        HandleForceAdjustment();
        HandleInput();
        UpdateBallColor();
    }

    void HandleForceAdjustment()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            currentForce += scroll * forceStep * 10f;
            currentForce = Mathf.Clamp(currentForce, minForce, maxForce);
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Plane plane = new Plane(Vector3.up, transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Vector3 direction = (hitPoint - transform.position).normalized;

                rb.AddForce(direction * currentForce, ForceMode.Impulse);
            }
        }
    }

    void UpdateBallColor()
    {
        float t = (currentForce - minForce) / (maxForce - minForce);
        Color color = Color.Lerp(Color.green, Color.red, t);
        ballRenderer.material.color = color;
    }
}
