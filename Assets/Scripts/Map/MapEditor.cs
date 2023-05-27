using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MapEditor", menuName = "ProjektEwolucja/MapEditor", order = 0)]
public class MapEditor : ScriptableObject
{
    public ToggleGroup tools;

    public bool BrushActive = false;
    public int BrushMode = 1;
    public int BrushShape = 1;
    public int BrushSize = 1;
    public int BrushValue = 1;

    public bool BucketActive = false;
    public int BucketMode = 1;
    public int BucketShape = 1;
    public int BucketThreshold = 1;
    public int BucketValue = 1;
}
