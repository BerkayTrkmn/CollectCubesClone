using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumTimer : Vacuum
{
    public override void CubePullingEnded(Cube cube)
    {
        base.CubePullingEnded(cube);
        if (cube.IsPicked) UsedPlayer.CubeCount--;
        cube.gameObject.SetActive(false);
        UsedPlayer.collectedCubes++;
        Config.OnPlayerPointChanged?.Invoke(UsedPlayer.ID, UsedPlayer.collectedCubes);
    }

}
