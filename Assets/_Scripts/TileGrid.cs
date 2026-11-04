using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int height => rows.Length;
    public int size => cells.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
        CreateCoordinates();
    }

    private void CreateCoordinates()
    {
        for(int y = 0; y < rows.Length; y++)
        {
            for(int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }
    }

    public TileCell GetCellCoordinates(int x, int y)
    {
        if(x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;    
        }
    }

    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCellCoordinates(coordinates.x, coordinates.y);
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);

    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        // find the empty cell
        while (cells[index].occupied)
        {
            index++;
            
            // if the number is higher than the length of cells, then reset it
            if(index >= cells.Length)
            {
                index = 0;
            }

            // if all of the cells are occupied, return null
            if(index == startingIndex)
            {
                return null;
            }
        }
        
        return cells[index]; 
    }


}
