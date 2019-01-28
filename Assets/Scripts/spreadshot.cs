using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spreadshot : MonoBehaviour
{
    public GameObject bullet;
    public float bullet_spd = 30f;
    public float delayTime = 1.2f;
    public float spread = 20f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0, delayTime);
    }

    void Shoot()
    {
        GameObject clone = Instantiate(bullet, transform.position + transform.up * 0.4f, transform.rotation);

        Vector2 dir = transform.up;

        clone.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * bullet_spd);

        clone = Instantiate(bullet, transform.position + transform.up * 0.4f, transform.rotation);

        clone.gameObject.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0,0,spread) * dir * bullet_spd);

        clone = Instantiate(bullet, transform.position + transform.up * 0.4f, transform.rotation);
        clone.gameObject.GetComponent<Rigidbody2D>().AddForce(Quaternion.Euler(0, 0, -spread) * dir * bullet_spd);
        //clone.AddForce(transform.up * thrust);

        //Destroy(clone);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
