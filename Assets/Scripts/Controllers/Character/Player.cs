using HelloScripts;
using UnityEngine;

//Player collector controller
public class Player : Character
{
   
    protected override void Start()
    {
        base.Start();
        //Normalde touch class� editor de oynarken �al��m�yor mouse �al���yor.
        //Siz bonus olarak interface kullanmay� istemi�siniz ancak bu soruna benim ��z�m�m Touch Manager.
        //Daha sonra isterseniz interface e ge�i� yapabilirim. 
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
