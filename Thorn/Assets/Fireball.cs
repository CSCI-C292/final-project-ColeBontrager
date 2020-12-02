using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Vector3 dir = new Vector3();
    [SerializeField] float range;
    private bool exploded = false;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemiesLayer;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!exploded)
        {
            transform.position += dir.normalized * speed;
        }
    }

    public void SetDir(Vector3 dir)
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        this.dir = dir;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.layer == 15 && !exploded)
        {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, range, enemiesLayer);
            foreach(Collider2D enemy in enemiesHit)
            {   
                //Debug.Log("Fireball hit "+ enemy.name);
                enemy.GetComponent<Enemy>().TakeDamage(2, transform.position);
            }
            exploded = true;
            animator.SetTrigger("Exploded");
            transform.localScale += new Vector3(2, 2, 0);
            
        }
    }

    void OnDrawGizmosSelected()
    {
        
        if(exploded)
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
