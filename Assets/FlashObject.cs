using UnityEngine;
using System.Collections;

public class FlashObject : MonoBehaviour {

 // vars to Flash a colour when hit 
 public Color NormalColor;
 public Color FlashColour = Color.white;

 public Renderer GameMesh;
 public float FlashDelay = 0.025f;
 public int TimesToFlash = 3;


public void Start() { 
     GameMesh = this.GetComponent<MeshRenderer>();
     NormalColor = this.GetComponent<MeshRenderer>().material.color;
}
 public void Flash(){

  StartCoroutine(FlashRoutine());
 }

 private IEnumerator  FlashRoutine() {
  var renderer = GameMesh; 
  var texture = renderer.material.mainTexture;
  if (renderer != null) {  

   for (int i = 1; i <= TimesToFlash; i++) {
    renderer.material.color = FlashColour; 
        renderer.material.mainTexture = null;
    yield return new WaitForSeconds (FlashDelay);
    renderer.material.mainTexture = texture;
    renderer.material.color = NormalColor;
    yield return new WaitForSeconds (FlashDelay);
   }
  }
 }

}
