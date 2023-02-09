using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 enum AIState
{
    Idle,
    GetCube,
    ReturnCube,
    CalculateCube
}
//Enemy Ai i�in asl�nda daha fazla fikrim vard� ama (Raycast ile etraftaki k�p ve di�er e�yalar�
//g�rme ve bunlara tepki verme, en yak�ndaki k�pe gitme, alandaki �ok olan k�s�mlar� g�r�p oraya do�ru hareket etme)
//ama ai k�sm� dipsiz kuyu gibi oldu�u i�in burada sadece benim Nav mesh ile yapmay�p benim manuel bi�imde ai yapabilece�imi g�stermek.
//Burada finite state machine ile ai '� yapt���m� belirtmek isterim.
public class Enemy : Character
{
    [SerializeField] private CubeCreator creator;
    [SerializeField] private VacuumTimer timerVacuum;
    [SerializeField] private AIState aiState = AIState.Idle;
    [SerializeField] private int minReturningCubeCount = 5;
    [SerializeField] private float waitingTime = 1.5f;
    float remainingTime;
    
    private void OnEnable()
    {
        remainingTime = waitingTime;
        Config.OnGameStart.AddListener(OnGameStart);
    }
    private void OnDisable()
    {
        Config.OnGameStart.RemoveListener(OnGameStart);

    }

    private void OnGameStart()
    {
        aiState = AIState.GetCube;
    }

    private void Update()
    {
        AIWorkFlow();
    }

    private void AIWorkFlow()
    {
        switch (aiState)
        {
            case AIState.Idle:
                IdleState();
                break;
            case AIState.GetCube:
                GetCubeState();
                break;
            case AIState.ReturnCube:
                ReturnCubeState();
                break;
            case AIState.CalculateCube:
                //Find cube and go there 
                //Bunun state i i�e al�nd�ktan sonra tamamlayaca��m :D
                break;
            default:
                break;
        }
    }
   //Waiting in play to cube dropping
    private void IdleState()
    {
        if(Config.LevelState == LevelState.Play)
        {
            EnemyTimer();
        }
    }
    //For get cubes go to creator
    private void GetCubeState()
    {
        Vector3 _direction = creator.transform.position - transform.position;
      float _distance =  _direction.magnitude;
        if(_distance > 0.0f)
        {
           Vector2 _directionV2 = new Vector2(_direction.x, _direction.z);
            CharacterMovement(_directionV2);
            CharacterRotation(_directionV2);
        }
    }
    //Return to vacuum
    private void ReturnCubeState()
    {
        Vector3 _direction = timerVacuum.transform.position - transform.position;
        float _distance = _direction.magnitude;
        if (_distance > 0.4f)
        {
            Vector2 _directionV2 = new Vector2(_direction.x, _direction.z);
            CharacterMovement(_directionV2);
            CharacterRotation(_directionV2);
        }
        else 
        {
            aiState = AIState.Idle;
        }
    }
    protected override void CollectCube(Cube cube)
    {
        cube.gameObject.layer = LayerMask.NameToLayer(Config.LAYER_COLLECTORAI);
        CubeCount++;
        cube.IsPicked = true;
        if (CubeCount >= minReturningCubeCount)
            aiState = AIState.ReturnCube;
    }
    protected override void DropCube(Cube cube)
    {
        cube.gameObject.layer = LayerMask.NameToLayer(Config.LAYER_DEFAULT);
        CubeCount--;
        cube.IsPicked = false;

        if (CubeCount <= 0)
            aiState = AIState.GetCube;
    }
    //Enemy idle timer
    private void EnemyTimer()
    {
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            remainingTime = waitingTime;
            aiState = AIState.GetCube;
        }
    }
}
