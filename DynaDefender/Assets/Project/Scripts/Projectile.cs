using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D body2d;

    private void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body2d.velocity = transform.up * speed;
    }

    public void SetVelocity(float speed)
    {
        this.speed = speed;
    }
}