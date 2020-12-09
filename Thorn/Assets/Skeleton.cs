using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject player;
    private Rigidbody2D rb;

    [SerializeField] GameObject healthPickup;
    [SerializeField] GameObject manaPickup;
    public bool isQuitting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!GetComponent<Enemy>().knocked && !player.GetComponent<Player>().dead)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
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
            int rand = Random.Range(1, 8);
            if (rand == 1)
            {
                rand = Random.Range(0, 3);
                if (rand >= 1)
                {
                    Instantiate(healthPickup, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(manaPickup, transform.position, Quaternion.identity);
                }
            }
        }

    }
}
