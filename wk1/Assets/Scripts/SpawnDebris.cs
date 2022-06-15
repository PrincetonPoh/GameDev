using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebris : MonoBehaviour
{
    // public GameConstants gameConstants;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {

        for (int x =  0; x<5; x++){
			Instantiate(prefab, transform.position, Quaternion.identity);
		}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}