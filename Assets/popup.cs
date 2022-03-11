using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    // Start is called before the first frame update
    private string txt;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text) {
        txt = "Picked up " + text;
        GetComponent<Text>().text = txt;
    }

     public void SetAltText(string text) {
        txt = text;
        GetComponent<Text>().text = txt;
    }

    public void ShowText() {
        var color = GetComponent<Text>().color;
        color.a = 255f;
        GetComponent<Text>().color = color;
        StartCoroutine(FadeText());
    }

    public IEnumerator FadeText() {
         var color = GetComponent<Text>().color;
         yield return new WaitForSeconds(3f);
       
         for(float t = 1f; t > 0f; t -= 0.01f) {
            var newAlpha = Mathf.Lerp(1f, 0f, (1-t)* (1-t));
            color.a = newAlpha;
             GetComponent<Text>().color = color;
            yield return null;

        }
        // while(color.a > 0f) {
        //      color.a -= 1f;
        //      GetComponent<Text>().color = color;
        //       yield return null;
        // }
       
    }
}
