using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundButton : MonoBehaviour
{
    
    [field: SerializeField]
    public UnityEvent OnButtonPressed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
         Debug.Log("Pressed button");
        if(other.gameObject.tag == "Player") {
            Debug.Log("Pressed button");
            this.transform.localScale = new Vector3(this.transform.localScale.x, 0.1f, this.transform.localScale.z);
            this.OnButtonPressed?.Invoke();
            this.GetComponent<BoxCollider>().isTrigger = true;

        }
    }
}
