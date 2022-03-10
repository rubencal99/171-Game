using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject FireArm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            GameObject thisFireArm = Instantiate(FireArm) as GameObject;
            thisFireArm.transform.parent = GameObject.Find("WeaponParent").transform;
            thisFireArm.transform.localPosition = new Vector3(0.574f, 0f, 0f);
            thisFireArm.transform.localRotation = Quaternion.identity;
            //SpriteRenderer sprite = thisFireArm.GetComponent<SpriteRenderer>();
            //sprite.enabled = false;
            popup popup = FindObjectOfType<popup>();
            popup.SetText("weapon");
            popup.ShowText();
            thisFireArm.SetActive(false);
        }
    }
}
