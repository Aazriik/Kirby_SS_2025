using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile: MonoBehaviour
{
    [SerializeField] private ProjectileType type = ProjectileType.Player;
    [SerializeField, Range(0.5f, 10)] private float lifetime = 10f;
    [SerializeField] private float torqueAmount = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => Destroy(gameObject, lifetime);
    public void SetVelocity(Vector2 velocity) => GetComponent<Rigidbody2D>().linearVelocity = velocity;

    // Make Projectile Spin
    public void SetSpinDirection(bool clockwise)
    {
        float torque = clockwise ? -torqueAmount : torqueAmount;
        GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == ProjectileType.Player)
        {
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(10);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == ProjectileType.Enemy)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            if (pc != null)
            {
                pc.TakeDamage(1);
                Destroy(gameObject);
            }
            //else
            //{
            //    Destroy(gameObject);
            //}
        }
    }
}

public enum ProjectileType
{
    Player,
    Enemy
}
