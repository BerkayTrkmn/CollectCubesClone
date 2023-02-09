using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;
using Random = UnityEngine.Random;
//Part2 CubeCreator
public class CubeCreatorTimer : CubeCreator
{
    [SerializeField] protected float maxForceX = 250f;
    [SerializeField] protected float maxTimeInterval = 1f;
    [SerializeField] protected float minTimeInterval = 0.25f;


    protected void OnEnable()
    {
        Config.OnGameStart.AddListener(OnGameStart);
    }

    protected void OnDisable()
    {
        Config.OnGameStart.RemoveListener(OnGameStart);
    }
    private void OnGameStart()
    {

        StartCoroutine(CreateCubeWithTimer());

    }

    private IEnumerator CreateCubeWithTimer()
    {
        while (Config.LevelState == LevelState.Play)
        {
            float _randomForceX = Random.Range(-maxForceX, maxForceX);
            float _randomTime = Random.Range(minTimeInterval, maxTimeInterval);
            CreateCube(out Cube _cube);
            _cube.rb.AddForce(new Vector3(_randomForceX, 0, 0));
            
            yield return _randomTime.GetWait();
           
        }

    }



}
