using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUP : MonoBehaviour
{
    // Start is called before the first frame update
    private RectTransform rect;
    public Vector2 offset = new Vector2(200,-200);
    void Start()
    {
        rect = GameObject.Find("ShopKeeper").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,Input.mousePosition,null,out point);
            transform.localPosition = point + offset;
    }
}
