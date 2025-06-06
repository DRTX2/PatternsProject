using UnityEngine;

public class Damageable : MonoBehaviour
{

    Animator animator;
    [SerializeField]
    private float _maxHealth=100;

    public float MaxHealth
    {

        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    private float _health = 100;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive=false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true; 

    [SerializeField]
    private bool isInvencible= false;
    private float timeSinceHit=0;
    public float invencibilityTime =0.25f;



    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set to " + value + " on " + gameObject.name);

            if (!_isAlive)
            {
            }
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    public void Update()
    {
        if(isInvencible)
        {
            if(timeSinceHit> invencibilityTime)
            {
                isInvencible=false;
                timeSinceHit = 0;
            }

            timeSinceHit+=Time.deltaTime;
        }

        Hit(10);
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvencible)
        {
            Health -= damage;
            isInvencible = true;

            if (Health <= 0)
            {
                IsAlive = false;
            }
            else
            {
                //animator.SetTrigger(AnimationStrings.hit); // Trigger hit animation
            }
        }
    }
}
