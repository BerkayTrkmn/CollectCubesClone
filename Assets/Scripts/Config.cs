using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Ortak kullaným sýnýfý
//Ben oyunun genelinde kullanýlan ortak deðiþkenleri config adlý sýnýfta kullanýyorum
//Oyunun baþlamasý bitmesi oyunun durumu gibi deðiþkenler bütün oyundaki objeleri ilgilendiriyor
//Bu objeler belli durumlarda bu deðiþkenleri isteyebiliryorlar. Bu yüzden aþaðidaki gibi konumlandýrdým. 
public class Config
{

    //TAGS
    public const string TAG_CUBE = "Cube";

    //LAYER
    public const string LAYER_PULLEDCUBE = "PulledCube";
    public const string LAYER_DEFAULT = "Default";
    public const string LAYER_COLLECTORAI = "CollectorAI";
    public const string LAYER_COLLECTOR = "Collector";

    //STATIC VARIABLES
    //Birden fazla scriptte kullaným alaný olan ve tek instance a sahip olmasý gereken deðiþkenleri buraya yazýyorum
    public static LevelState LevelState = LevelState.Start;

    public static List<Cube> LevelCubes = new List<Cube>();

    //EVENTS
    //Normalde Action kullanarak yapýyorum ama normalde
    //UnityEventle çalýþanlarý görüyorum bu yüzden burada UnityEvent kullandým
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnGameEnd = new UnityEvent();
    public static UnityEvent OnGameFailed = new UnityEvent();
    public static UnityEvent OnGameSuccess = new UnityEvent();
    //point and player
    //Parametreli olduðu için UnityEvent ile uðraþmamak adýna action yaptým.(Zaman kýsýtlý)
    public static Action<int,int> OnPlayerPointChanged;
    


}
public enum LevelState
{
    Start,
    Play,
    Completed,
    Failed
}
