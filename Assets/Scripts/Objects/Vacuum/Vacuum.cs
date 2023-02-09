using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
     public Character UsedPlayer;
    [SerializeField] private Material pulledCubeMaterial;
    [SerializeField] private float pullForce=10f;
    [SerializeField] private float endingRadius = 0.5f;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Cube>(out Cube _cube))
        {
            Vector3 direction = (transform.position - _cube.transform.position);
            float distance = direction.magnitude;
            CubePullingStart(_cube);
            _cube.rb.AddForce(direction * pullForce);
            if (distance < endingRadius && _cube.IsPulled)
                CubePullingEnded(_cube);
        }

    }
    public virtual void CubePullingStart(Cube cube)
    {
        cube.cubeRenderer.material = pulledCubeMaterial;
        cube.IsPulled = true;
        //Burada her kübe id verilip id ile yokedilebilir.(No Time)
        Config.LevelCubes.Remove(cube);
        
    }
    public virtual void CubePullingEnded(Cube cube)
    {
        cube.gameObject.layer = LayerMask.NameToLayer(Config.LAYER_PULLEDCUBE);
        Debug.Log(Config.LevelCubes.Count);
    }

}
