using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blueprint", menuName = "Custom Stuff", order = 1)]
public class Blueprint : ScriptableObject
{
    public int width;
    public int length;
    public int height;
}
