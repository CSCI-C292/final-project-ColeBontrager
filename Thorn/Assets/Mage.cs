using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float speed;
    [SerializeField] float dist;
    private Rigidbody2D rb;
    [SerializeField] float attackTime;
    [SerializeField] float spellDist;
    [SerializeField] GameObject iceball;

    [SerializeField] GameObject manaPickup;
    public bool isQuitting = false;

    private float currTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currTime = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currTime <= 0)
        {
            currTime = attackTime;
            Attack();
        }
        else
        {
            currTime -= Time.deltaTime;
        }
    }

    void Attack()
    {
        Debug.Log("Mage attack");

        Vector3 playerDir = (player.transform.position - transform.position).normalized;
        Vector3 icePos = transform.position + (spellDist * playerDir);
        GameObject ice = Instantiate(iceball, icePos, Quaternion.identity);
        ice.GetComponent<Iceball>().SetDir(playerDir);
    }

    void FixedUpdate()
    {
        if (!GetComponent<Enemy>().knocked)
        {
            Vector3 playerDir = player.transform.position - transform.position;
            if (playerDir.magnitude <= dist) { rb.velocity = new Vector3(); }
            else
            {
                rb.velocity = (player.transform.position - transform.position).normalized * speed;

            }

        }
        if (player.GetComponent<Player>().dead)
        {
            rb.velocity = new Vector3();
        }
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    void OnDestroy()
    {
        if (!isQuitting)
        {
            float rand = Random.Range(0, 2);
            if (rand == 1)
            {
                Instantiate(manaPickup, transform.position, Quaternion.identity);
            }
        }

    }
}
