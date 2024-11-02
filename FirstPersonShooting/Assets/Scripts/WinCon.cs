using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            if (Application.isEditor)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
