using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class earth : MonoBehaviour
{

    private int health;
    public int STARTING_HEALTH = 100;
    private SpriteRenderer sr;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        health = STARTING_HEALTH;
        sr = GetComponent<SpriteRenderer>();
        text = GameObject.FindWithTag("GC2").GetComponentInChildren<Text>();
        text.text = "Health: " + health + "/100";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            health--;

            if (health <= 0)
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }

            text.text = "Health: " + health + "/100";
            Destroy(col.gameObject);

            Color tmpColor = sr.color;
            tmpColor.a -= 1.0f/STARTING_HEALTH;
            sr.color = tmpColor;


        }
    }
}
