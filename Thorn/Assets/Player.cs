using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed;
    private Vector3 movement;

    private Animator animator;

    [SerializeField] int health = 6;
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] float knockback;

    [SerializeField] float knockLength;
    private float currentKnockTime = 0;
    private Vector3 knockDir;
    public bool knocked = false;

    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector3();
        animator = GetComponent<Animator>();
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        //update health UI
        for(int i = 0; i < hearts.Length; i++)
        {
            if(health <= i * 2) {hearts[i].GetComponent<Image>().sprite = emptyHeart;}
            else if(health == (i+1) * 2 - 1) {hearts[i].GetComponent<Image>().sprite = halfHeart;}
            else if(health >= (i+1) * 2) {hearts[i].GetComponent<Image>().sprite = fullHeart;}
        }

        //update knockback time
        if(knocked)
        {
            currentKnockTime -= Time.deltaTime;
        }
        if(currentKnockTime <= 0)
        {
            knocked = false;
            animator.SetBool("Hit", false);
        }


        //update movement
        
        if(Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("LookHoriz", 0);
            animator.SetFloat("LookVert", 1);
        }
        if(Input.GetKey(KeyCode.S))
        {
            animator.SetFloat("LookHoriz", 0);
            animator.SetFloat("LookVert", -1);
        }
        if(Input.GetKey(KeyCode.A))
        {
            animator.SetFloat("LookHoriz", -1);
            animator.SetFloat("LookVert", 0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("LookHoriz", 1);
            animator.SetFloat("LookVert", 0);
        }
        
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        
        
       
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
            rb.velocity = movement * speed * Time.deltaTime;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.name.Length > 8 && collider.gameObject.name.Substring(0, 8) == "Skeleton")
        {
            Debug.Log("Skeleton damage");
            TakeDamage(1, collider.gameObject.transform.position);
            collider.gameObject.GetComponent<Enemy>().TakeDamage(0, transform.position);
        }

        if(collider.gameObject.name.Length > 4 && collider.gameObject.name.Substring(0, 4) == "Ogre")
        {
            Debug.Log("Ogre damage");
            TakeDamage(1, collider.gameObject.transform.position);
            collider.gameObject.GetComponent<Enemy>().TakeDamage(0, transform.position);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name.Length > 7 && collider.gameObject.name.Substring(0, 7) == "Iceball")
        {
            Debug.Log("Mage damage");
            TakeDamage(1, collider.gameObject.transform.position);
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.name.Length > 7 && collider.gameObject.name.Substring(0, 7) == "Cleaver")
        {
            Debug.Log("Cleaver damage");
            TakeDamage(2, collider.gameObject.transform.position);
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.name.Length > 6 && collider.gameObject.name.Substring(0, 6) == "Health")
        {
            if(health < maxHealth)
            {
                health += 2;
                Destroy(collider.gameObject);
            }
            if(health > maxHealth)
            {
                health = maxHealth;
            }
            
        }
    }

    void TakeDamage(int damage, Vector3 source)
    {
        health -= damage;
        currentKnockTime = knockLength;
        knocked = true;
        animator.SetBool("Hit", true);
        knockDir = (transform.position - source).normalized;
    }
        
}
