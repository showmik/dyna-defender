using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpwanPoint;
    [SerializeField] private float rotationSpeed = 17f;
    [SerializeField] private float projectileSpeed = 17f;
    [SerializeField] private float fireDelay = 0.2f;

    private Quaternion targetRotation;
    private bool hasFired = false;
    private float fireTimeCounter;

    private void Start()
    {
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        fireTimeCounter -= Time.deltaTime;
        TakeInput();
        RotateCannon();

        if (Input.GetMouseButtonUp(0))
        {
            hasFired = true;
        }

        FireProjectile();
    }

    private void TakeInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 touchCordinate = Camera.main.ScreenToWorldPoint(mousePosition);

            if (touchCordinate.y > transform.position.y)
            {
                Vector2 targetVector = touchCordinate - transform.position;
                targetRotation = Quaternion.FromToRotation(-transform.position, targetVector);
            }
        }
    }

    private void RotateCannon()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void FireProjectile()
    {
        if (transform.rotation == targetRotation && hasFired && fireTimeCounter < 0)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpwanPoint.position, transform.rotation);
            projectileInstance.GetComponent<Projectile>().SetVelocity(17);
            Destroy(projectileInstance, 2f);
            hasFired = false;
            fireTimeCounter = fireDelay;
        }
    }
}