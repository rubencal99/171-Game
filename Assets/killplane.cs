using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killplane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            other.GetComponent<Player>().resetToSpawnPoint();
        else if(other.tag == "Enemy")
            Destroy(other.gameObject);
    }
}
