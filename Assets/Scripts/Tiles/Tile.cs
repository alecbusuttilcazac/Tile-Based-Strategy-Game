using UnityEngine;

public class Tile
{
    public int movementCost;
    public int defenseBonus;
    public bool impassable;
    public bool hasBuilding;
    public bool hasUnit = false;
    public Player owner = null;
    
    public Vector2Int position;
    public GameObject visualObject;
    
    public Tile(int x, int y)
    {
        position = new Vector2Int(x, y);
    }
    
    public void SetVisualObject(GameObject obj) { visualObject = obj; }
}
