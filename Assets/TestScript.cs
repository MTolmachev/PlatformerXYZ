using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
   private void Start()
   {
      StartCoroutine(SomeCoroutine());
   }
   
   private IEnumerator SomeCoroutine()
   {
      var some = "34";
      yield return null;
      
      some += "42";
      while (enabled)
      {
         Debug.Log(some);
         yield return SomeCoroutine2();
      }
      
   }

   private IEnumerator SomeCoroutine2()
   {
      yield return new  WaitForSeconds(2f);

   }
   
}
