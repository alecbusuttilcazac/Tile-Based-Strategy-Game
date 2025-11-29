using UnityEngine;

public class Unit
{
    public string unitName;
    public int currentHealth;
    public int maxHealth;
    public bool isDead = false;
    public bool canTraverseWater;
    public int tileMovement; // Number of tiles the unit can move
    public int tileVision; // Number of tiles away the unit can see
    public int cost;
    // public Player Owner; OR public int ownerID;
    public bool isSelected = false;
    public bool hasMoved = false;
    
    public Tile currentTile;
    public Sprite visualSprite;
    
    public Unit(Tile startingTile){
        currentTile = startingTile;
    }
        
    void Start()
    {
        
    }

    public void SetSprite(Sprite obj)
    {
        visualSprite = obj;
    }
}
