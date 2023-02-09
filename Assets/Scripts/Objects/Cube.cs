using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool IsPicked = false;
    public bool IsPulled=false;
   [HideInInspector] public Rigidbody rb;
    public MeshRenderer cubeRenderer;

    private void Awake()
    {
        rb.GetComponent<Rigidbody>();
        cubeRenderer = GetComponentInChildren<MeshRenderer>();
    }

   

}
