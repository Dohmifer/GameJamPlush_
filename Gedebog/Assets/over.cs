using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class over : MonoBehaviour
{
   void OnCollisionEnter2D(Collision2D colJ)
   {
     if (colJ.gameObject.CompareTag("Player"))
     {
        SceneManager.LoadScene(0);
     }
   }
}
