using HelloScripts;
using UnityEngine;

//Player collector controller
public class Player : Character
{
   
    protected override void Start()
    {
        base.Start();
        //Normalde touch classý editor de oynarken çalýþmýyor mouse çalýþýyor.
        //Siz bonus olarak interface kullanmayý istemiþsiniz ancak bu soruna benim çözümüm Touch Manager.
        //Daha sonra isterseniz interface e geçiþ yapabilirim. 
        TouchManager.Instance.onTouchMoved += OnTouchMoved;
    }

    private void OnTouchMoved(TouchInput touch)
    {
        if (Config.LevelState == LevelState.Play)
        {
            CharacterMovement(touch.DeltaScreenPosition);
            CharacterRotation(touch.DeltaScreenPosition);
        }
    }

    private void OnDisable()
    {
        TouchManager.Instance.onTouchMoved -= OnTouchMoved;
    }

    protected override void CollectCube(Cube cube)
    {
        cube.gameObject.layer = LayerMask.NameToLayer(Config.LAYER_COLLECTOR);
        CubeCount++;
        cube.IsPicked = true;
    }

    protected override void DropCube(Cube cube)
    {
       if(!cube.IsPulled)cube.gameObject.layer = LayerMask.NameToLayer(Config.LAYER_DEFAULT);
        CubeCount--;
        cube.IsPicked = false;
    }
}
