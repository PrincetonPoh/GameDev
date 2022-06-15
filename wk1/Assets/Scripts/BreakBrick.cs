using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private bool broken = false;
    public GameObject debris;
    // Start is called before the first frame update
    void Start()
    {
        debris.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D col){
        Debug.Log("Breakable Brick Collision");
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            // assume we have 5 debris per box
            for (int x =  0; x<5; x++){
                Debug.Log("Initialize Debris");
                Instantiate(debris, transform.position, Quaternion.identity);
            }
            // GetComponent<AudioSource>().Play();
            GetComponent<EdgeCollider2D>().enabled  =  false;
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}