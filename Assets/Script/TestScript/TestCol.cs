using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCol : MonoBehaviour
{
    float puls_x = 0f;

    // Start is called before the first frame update
    void Start()
    {
        puls_x = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        puls_x -= 0.1f;
        gameObject.transform.position = new Vector3(puls_x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("•¶Žš‚É“–‚½‚Á‚½");
        
    }
}
