using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject skeleton;
    [SerializeField] GameObject caster;

    [SerializeField] GameObject ogre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameObject skele = Instantiate(skeleton, new Vector3(1, 10, -1), Quaternion.identity);
            skele.GetComponent<Skeleton>().player = this.player;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject cast = Instantiate(caster, new Vector3(5, 10, -1), Quaternion.identity);
            cast.GetComponent<Mage>().player = this.player;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            GameObject og = Instantiate(ogre, new Vector3(9, 10, -1), Quaternion.identity);
            og.GetComponent<Ogre>().player = this.player;
        }


    }
}
