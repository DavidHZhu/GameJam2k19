using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_tripleshot : MonoBehaviour
{
    public GameObject bullet;
    public float bullet_spd = 30;
    public float delayTime = 1.2f;
    public float betweenShots = 0.2f;
    public int nShots = 3;
    private int nCompleted = 0;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShootLoop", delayTime);
    }

    void Shoot()
    {
        GameObject clone = Instantiate(bullet, transform.position + transform.up * 0.4f, transform.rotation);
        clone.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bullet_spd);

        //Destroy(clone);

    }

    void ShootLoop()
    {
        nCompleted++;
        Shoot();
        if (nCompleted < nShots)
        {
            Invoke("ShootLoop", betweenShots);
        }
        else
        {
            Invoke("ShootLoop", delayTime);
            nCompleted = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
