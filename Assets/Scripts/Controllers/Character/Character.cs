using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int ID;
    public int collectedCubes = 0;
    protected Rigidbody rb;
    [SerializeField] protected bool isDataUsing;
    [SerializeField] protected PlayerData playerData;
    [SerializeField] protected float forceMultiplier = 10;
    [SerializeField] protected float singleCubeForce = 1;
    [SerializeField] protected float rotationSpeed = 25;
    public int CubeCount = 0;
    protected virtual void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        //Normalde touch classý editor de oynarken çalýþmýyor mouse çalýþýyor. Bu soruna benim çözümüm Touch Manager.
        if (isDataUsing && !ReferenceEquals(playerData, null))
            SetPlayerData(playerData);
    }
    //Get player data from scriptable object and set to level
    protected void SetPlayerData(PlayerData data)
    {
        forceMultiplier = data.ForceMultipler;
        singleCubeForce = data.SingleCubeForce;
        rotationSpeed = data.RotationSpeed;
    }
    protected void CharacterMovement(Vector2 moveVector)
    {
        //Playerý fizik ile oynattým ki objecler birbirinin içinden geçmesin
        rb.AddForce(new Vector3(moveVector.x, 0, moveVector.y) * (forceMultiplier + (CubeCount * singleCubeForce)));

    }
    protected void CharacterRotation(Vector2 moveVector)
    {
        //Smooth rotation with lerp
        Vector3 _directionVector = new Vector3(moveVector.x, 0, moveVector.y);
        if (_directionVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_directionVector), Time.deltaTime * rotationSpeed);
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Cube>(out Cube _cube))
        {
            CollectCube(_cube);
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Cube>(out Cube _cube))
        {
            DropCube(_cube);
        }
    }

    protected abstract void CollectCube(Cube cube);
    protected abstract void DropCube(Cube cube);

}
