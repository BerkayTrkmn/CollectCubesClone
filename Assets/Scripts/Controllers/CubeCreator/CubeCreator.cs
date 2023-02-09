using HelloScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Oyun farklý cube creator istiyor ortak özellikleri buraya yazýp diðerlerini daðýtýyoruz(Inheritance)
public class CubeCreator : MonoBehaviour
{
    protected ObjectPooler pooler;
    [SerializeField] protected GameObject cubePrefab;
    [SerializeField] protected Material defaultMaterial;
    protected virtual void Awake()
    {
        pooler = ObjectPooler.instance;
        pooler.PoolObject(cubePrefab, 50);
    }
    protected GameObject CreateCube(out Cube cube)
    {
        GameObject _currentCubeGO = pooler.GetPooledObject(cubePrefab);
        cube = _currentCubeGO.GetComponent<Cube>();
        ResetCube(cube);
        _currentCubeGO.transform.position = transform.position;
        _currentCubeGO.SetActive(true);
        Config.LevelCubes.Add(cube);
        return _currentCubeGO;
    }
    protected void ResetCube(Cube cube)
    {
        cube.rb.velocity = Vector3.zero;
        cube.transform.position = Vector3.zero;
        cube.cubeRenderer.material = defaultMaterial;
        cube.gameObject.layer = LayerMask.NameToLayer(Config.LAYER_DEFAULT);
        cube.gameObject.SetActive(false); 
    }

}
