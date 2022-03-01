using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    // public static ToolTip Instance {get; private set;}
    // public GameObject tooltip;
    // Start is called before the first frame update
    private RectTransform rect;
    public Vector2 offset = new Vector2(200,-200);
    void Start()
    {
        rect = GameObject.Find("Canvas-ShopUI").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,Input.mousePosition,null,out point);
            transform.localPosition = point + offset;
    }


    // public void Show()
    // {
    //     // tooltip.SetActive(true);
    // }

    // public void Hide()
    // {
    //     // tooltip.SetActive(false);
    // }
}
