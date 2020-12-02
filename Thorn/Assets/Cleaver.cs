using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaver : MonoBehaviour
{
    private Vector3 dir = new Vector3();
    [SerializeField] float speed;
    [SerializeField] float rotation;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(0,0,rotation*Time.deltaTime);
        transform.position += dir.normalized * speed;
    }

    public void SetDir(Vector3 dir)
    {
        //transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        this.dir = dir;
    }
}
