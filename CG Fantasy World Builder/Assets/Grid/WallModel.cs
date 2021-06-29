using UnityEngine;
using System.Collections.Generic;

public class WallModel : MonoBehaviour
{
    [SerializeField] private int size;

    public WallModel(int size)
    {
        this.size = size;
    }

    public int getSize()
    {
        return size;
    }
}
