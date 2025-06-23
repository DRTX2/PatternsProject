using UnityEngine;

/// <summary>
/// ArrowProjectile is a simple projectile class that represents an arrow in the game.
/// </summary>

public class ArrowProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 10f;
    [SerializeField]  public int damage = 10;
    [SerializeField]  public Vector2D knockback = new Vector2D(0, 0);
    private float _direction;

    public void Launch(float direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        transform.position += Vector3.right * _direction * speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageBehaviour>(out var target))
        {
            bool damaged = target.ReceiveDamage(damage, knockback);
            if (damaged)
            {
                //Debug.Log($"{other.name} hit for {damage}"); 
                Destroy(gameObject);
            }
        }
    }
}
