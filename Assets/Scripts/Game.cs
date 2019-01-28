using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{
    public GameObject enemy_b;
    public GameObject enemy_triple;
    public GameObject enemy_spread;

    public float spawnRate = 3f;
    private int spawns = 1;

    public float minSpeed = 100f;
    public float maxSpeed = 200f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("NewEnemy", spawnRate);

    }

    // Update is called once per frame

    public void NewEnemy()
    {
        GameObject[] options = new GameObject[3];
        options[0] = enemy_b;
        options[1] = enemy_triple;
        options[2] = enemy_spread;
        GameObject choice = options[Random.Range(0, 3)];

        GameObject clone = Instantiate(choice, new Vector2(Random.Range(-5f, 5f), 7f), transform.rotation);
        clone.transform.eulerAngles = new Vector3(0, 0, Random.Range(140, 220) - clone.transform.position.x*5f);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.up * Random.Range(minSpeed, maxSpeed));
        spawnRate = Mathf.Pow(0.95f, spawns) + 0.25f;
        Invoke("NewEnemy", spawnRate);
    }
}
