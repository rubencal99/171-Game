using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyreducebase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TurnOff());
    }
 
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("TurnOff");
        gameObject.SetActive(false);
    }


}
