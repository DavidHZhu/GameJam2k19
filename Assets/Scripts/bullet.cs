using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullet : MonoBehaviour
{
    public static int score = 0;
    public Text text;
    private bool outBox = false;
    public GameObject explosion;
    // Start is called before the first frame update
    void Awake()

    {
        text = GameObject.FindWithTag("GameController").GetComponentInChildren<Text>();
        outBox = false;

    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && outBox) {
            Destroy(col.gameObject);
            score += 1;
            text.text = "Score: " + score;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        outBox = true;
    }
}
