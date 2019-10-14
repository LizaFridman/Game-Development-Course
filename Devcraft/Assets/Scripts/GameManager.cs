using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isJumping = false;
    private bool isPunching = false;
    private bool isBuilding = false;

    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsPunching { get => isPunching; set => isPunching = value; }
    public bool IsBuilding { get => isBuilding; set => isBuilding = value; }

    public void JumpButtonPressed()
    {
        IsJumping = true;
    }

    public void PunchButtonPressed()
    {
        IsPunching = true;
    }

    public void BuildButtonPressed()
    {
        IsBuilding = true;
    }
}
