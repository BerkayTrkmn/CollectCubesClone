using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumSimple : Vacuum
{

    public override void CubePullingStart(Cube cube)
    {
        base.CubePullingStart(cube);
        if (Config.LevelCubes.Count == 0)
            Config.OnGameSuccess?.Invoke();
    }
}
