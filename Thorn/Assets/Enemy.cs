using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 2;

    [SerializeField] float knockback;

    [SerializeField] float knockLength;
    private float currentKnockTime = 0;
    private Vector3 knockDir;
    public bool knocked = false;

    private Rigidbody2D rb;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if(rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);
        if(knocked)
        {
            currentKnockTime -= Time.deltaTime;
        }
        if(currentKnockTime <= 0)
        {
            animator.SetBool("Hit", false);
            knocked = false;
        }
    }

    void FixedUpdate()
    {
        if(knocked)
        {
            rb.velocity = knockDir * knockback * Time.deltaTime;
        }
        else
        {
            if(health <= 0) 
            {
                Destroy(this.gameObject);
            }
            rb.velocity = new Vector2();
        }
    }

    public void TakeDamage(int damage, Vector3 source)
    {
        if(damage > 0) {animator.SetBool("Hit", true);}
        health -= damage;
        currentKnockTime = knockLength;
        knocked = true;
        knockDir = (transform.position - source).normalized;
        
    }

    
}
