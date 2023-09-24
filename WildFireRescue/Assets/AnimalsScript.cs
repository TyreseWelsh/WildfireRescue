using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsScript : MonoBehaviour
    
{
    public static int Worth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //When the player collides, will destroy this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}



