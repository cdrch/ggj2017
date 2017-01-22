using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blueprint", menuName = "Blueprint", order = 1)]
public class Blueprint : ScriptableObject
{
    public int width;
    public int length;
    public int height;
    public bool hasRoof;
    public int[] blueprint;

    public Blueprint[] subBlueprints;
}
