using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRiflePickup : MonoBehaviour
{
    public GameObject FireArm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            GameObject thisFireArm = Instantiate(FireArm, new Vector3(0.574f, 0f, 0f), Quaternion.identity) as GameObject;
            thisFireArm.transform.parent = GameObject.Find("WeaponParent").transform;
        }
    }
}
