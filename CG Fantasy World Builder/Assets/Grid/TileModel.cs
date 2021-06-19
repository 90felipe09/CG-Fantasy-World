using UnityEngine;
using System.Collections.Generic;

public class TileModel
{
    private int line;
    private int collumn;

    public TileModel(int line, int collumn)
    {
        this.line = line;
        this.collumn = collumn;
    }

    public Vector3 getPosition()
    {
        return new Vector3(collumn, 0, line);
    }
}
