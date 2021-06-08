using System.Collections.Generic;

public class GridModel
{
    public List<TileModel> layout;

    public GridModel(int size)
    {
        layout = new List<TileModel>();
        for(int line = 0; line < size; line++)
        {
            for(int collumn = 0; collumn < size; collumn++)
            {
                layout.Add(new TileModel(line, collumn));
            }
        }
    }
}
