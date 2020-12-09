using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject player;
    private Rigidbody2D rb;

    [SerializeField] GameObject cleaver;

    [SerializeField] float attackTime;
    private float currTime;
    [SerializeField] float attackDelay;

    [SerializeField] float throwDist;

    private float currDelay;
    private bool attacking = false;
    [SerializeField] GameObject healthPickup;
    public bool isQuitting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currTime = attackTime;
    }

    // Update is called once per frame
    void Update()
    {

        currTime -= Time.deltaTime;
        if (currTime <= 0 && !attacking)
        {
            currTime = attackDelay;
            attacking = true;
        }
        else if (currTime <= 0 && attacking)
        {
            currTime = attackTime;
            attacking = false;
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (!GetComponent<Enemy>().knocked)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
        }
        if (attacking)
        {
            rb.velocity = new Vector3();
        }

        if (player.GetComponent<Player>().dead)
        {
            rb.velocity = new Vector3();
        }
    }

    void Attack()
    {
        Debug.Log("Ogre attack");
        Vector3 playerDir = (player.transform.position - transform.position).normalized;
        Vector3 cleavePos = transform.position + (throwDist * playerDir);
        GameObject cleave = Instantiate(cleaver, cleavePos, Quaternion.identity);
        cleave.GetComponent<Cleaver>().SetDir(playerDir);
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    void OnDestroy()
    {
        if (!isQuitting)
        {
            Instantiate(healthPickup, transform.position, Quaternion.identity);
        }

    }
}
