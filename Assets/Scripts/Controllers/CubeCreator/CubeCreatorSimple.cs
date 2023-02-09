using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;
//PArt1 CubeCreator
//resim i�leyebiliyor.
public class CubeCreatorSimple : CubeCreator
{
    [SerializeField] private Texture2D[] images; 
    Texture2D currentImage;
    [SerializeField] private float cubeSideLength = 0.2f;
    [SerializeField] private float gapLength = 0.05f;
    private int xLength = 5;
    private int yLength = 7;
    public Color[] pixels; 
    protected override void Awake()
    {
        base.Awake();
        SetImage();


        CreateCubeImage(xLength, yLength);

    }
    private void SetImage()
    {
        currentImage = GetRandomImage();
        pixels = currentImage.GetPixels();
        xLength = currentImage.width;
        yLength = currentImage.height;
    }
    //Basit cube alan� olu�turma 
    private void CreateCubeImage(int x, int y)
    {
        float startingX = transform.position.x - (((x / 2) * cubeSideLength) + ((x - 1) / 2) * gapLength);
        float startingY = transform.position.z - (((y / 2) * cubeSideLength) + ((y - 1) / 2) * gapLength);
        for (int j = 0; j < y; j++)
        {
            for (int i = 0; i < x; i++)
            {
                GameObject _cubeGO = CreateCube(out Cube _cube);
                CubePlacement(_cubeGO, i, j, startingX, startingY);
               //Pixele g�re renklendirme
                ChangeCubeMaterial(_cube, pixels[(j * x) + i]);
            }
        }
    }
    private void CubePlacement(GameObject cubeGO, int x, int y, float startingX, float startingY)
    {
        cubeGO.transform.position = new Vector3(startingX + ((1 / 2 * cubeSideLength) + (x * cubeSideLength) + (x * gapLength)), 0,
           startingY + ((1 / 2 * cubeSideLength) + (y * cubeSideLength) + (y * gapLength)));
    }
     private Texture2D GetRandomImage()
    {
        return images[Random.Range(0, images.Length)];
    }
    private void ChangeCubeMaterial(Cube cube, Color color)
    {
        //Burada her seferinde yeni material olu�turuluyor bunu kendi elimizle
        //olu�turup var olan� sharedmaterial ile da��tsak daha optimize olur
        //Dictionary<Color,Material> gibi
        cube.cubeRenderer.material.color = color;
    }
}
