using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData",menuName =("Data/PlayerData"),order =0)]
public class PlayerData : ScriptableObject
{
    public float ForceMultipler;
    public float RotationSpeed;
    public float SingleCubeForce;
}
