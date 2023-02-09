using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Ortak kullan�m s�n�f�
//Ben oyunun genelinde kullan�lan ortak de�i�kenleri config adl� s�n�fta kullan�yorum
//Oyunun ba�lamas� bitmesi oyunun durumu gibi de�i�kenler b�t�n oyundaki objeleri ilgilendiriyor
//Bu objeler belli durumlarda bu de�i�kenleri isteyebiliryorlar. Bu y�zden a�a�idaki gibi konumland�rd�m. 
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
    //Birden fazla scriptte kullan�m alan� olan ve tek instance a sahip olmas� gereken de�i�kenleri buraya yaz�yorum
    public static LevelState LevelState = LevelState.Start;

    public static List<Cube> LevelCubes = new List<Cube>();

    //EVENTS
    //Normalde Action kullanarak yap�yorum ama normalde
    //UnityEventle �al��anlar� g�r�yorum bu y�zden burada UnityEvent kulland�m
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnGameEnd = new UnityEvent();
    public static UnityEvent OnGameFailed = new UnityEvent();
    public static UnityEvent OnGameSuccess = new UnityEvent();
    //point and player
    //Parametreli oldu�u i�in UnityEvent ile u�ra�mamak ad�na action yapt�m.(Zaman k�s�tl�)
    public static Action<int,int> OnPlayerPointChanged;
    


}
public enum LevelState
{
    Start,
    Play,
    Completed,
    Failed
}
