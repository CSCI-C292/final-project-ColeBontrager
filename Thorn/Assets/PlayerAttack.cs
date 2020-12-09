using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] float range;
    [SerializeField] float dist;
    [SerializeField] LayerMask enemiesLayer;

    [SerializeField] float spellDist;
    [SerializeField] GameObject fireball;
    [SerializeField] int mana;
    [SerializeField] Image[] manaContainers;
    [SerializeField] Sprite fullMana;
    [SerializeField] Sprite emptyMana;

    private int maxMana;

    private Vector3 attackPoint;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        maxMana = mana;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Player>().dead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetMouseButtonDown(0) && mana > 0)
            {
                mana--;
                Fireball();
            }
        }


        for (int i = 0; i < manaContainers.Length; i++)
        {
            if (mana < i + 1) { manaContainers[i].GetComponent<Image>().sprite = emptyMana; }
            else if (mana >= i + 1) { manaContainers[i].GetComponent<Image>().sprite = fullMana; }
        }


    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        float x = animator.GetFloat("LookHoriz");
        float y = animator.GetFloat("LookVert");
        if (y == -1)
        {
            //attack down
            attackPoint = new Vector3(transform.position.x, transform.position.y - dist, transform.position.z);
        }
        else if (y == 1)
        {
            //attack up
            attackPoint = new Vector3(transform.position.x, transform.position.y + dist, transform.position.z);
        }
        else if (x == 1)
        {
            //attack right
            attackPoint = new Vector3(transform.position.x + dist, transform.position.y, transform.position.z);
        }
        else
        {
            //attack left
            attackPoint = new Vector3(transform.position.x - dist, transform.position.y, transform.position.z);
        }

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint, range, enemiesLayer);
        foreach (Collider2D enemy in enemiesHit)
        {
            //Debug.Log("We hit "+ enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(1, transform.position);
        }

    }

    void Fireball()
    {

        Vector3 playerPos = GetComponent<BoxCollider2D>().bounds.center;
        Vector3 mouseDir = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPos;
        mouseDir.z = 0;
        Vector3 firePos = playerPos + (spellDist * mouseDir.normalized);
        GameObject fire = Instantiate(fireball, firePos, Quaternion.identity);
        fire.GetComponent<Fireball>().SetDir(mouseDir);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) { return; }
        Gizmos.DrawWireSphere(attackPoint, range);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Length > 4 && collider.gameObject.name.Substring(0, 4) == "Mana")
        {
            if (mana < maxMana)
            {
                mana++;
                Destroy(collider.gameObject);
            }

        }
    }
}
