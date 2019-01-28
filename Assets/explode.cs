using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<ParticleSystem>().Play();
        Invoke("del", 0.2f);
    }

    void del()
    {
        Destroy(gameObject);
    }
}
