using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustInteropTest: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        byte[] arr = { 104, 104, 104, 104, 104, 104 };

        int randomNum = RustInterop.GetRandomInt();
        Debug.Log("Random number: " + randomNum);
        Debug.Log("Multiply by two: " + RustInterop.MultiplyByTwo(randomNum));
        Debug.Log("bufferstuff: " + RustInterop.AddXToEachElement(arr, 6));
        //Debug.Log("Add X to each Element: " + RustInterop.AddXToEachElement(2, arr, 3));
        //byte[] jeu = RustInterop.AddXToEachElement(2, arr, 3);
    }
}
