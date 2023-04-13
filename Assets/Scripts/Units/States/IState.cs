using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter(StateController sc);
    public void UpdateState(StateController sc);
    public void OnExit(StateController sc);
    //public void Scared();
}
