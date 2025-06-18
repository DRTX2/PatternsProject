using UnityEngine;

public class ArrowProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 10f;
    [SerializeField]  public int damage = 10;
    [SerializeField]  public Vector2 knockback = new Vector2(0, 0);
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
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            bool damaged = target.ReceiveDamage(damage, knockback);
            if (damaged)
            {
                Debug.Log($"{other.name} hit for {damage}"); 
                Destroy(gameObject);
            }
        }
    }
}
