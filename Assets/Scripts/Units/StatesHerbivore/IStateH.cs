using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateH
{
    public void OnEnter(StateControllerH sc);
    public void UpdateState(StateControllerH sc);
    public void OnExit(StateControllerH sc);
    //public void Scared();
}
