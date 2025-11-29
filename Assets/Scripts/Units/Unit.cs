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
    public int coinCost; // Cost in coins to produce the unit
    public int roundCost; // Number of rounds required to produce the unit
    // public Player Owner; OR public int ownerID;
    public bool isSelected = false;
    public bool hasMoved = false;
    
    public Tile currentTile;
    public Sprite visualSprite;
    
    public Unit(Tile currentTile){
        this.currentTile = currentTile;
    }
        
    void Start()
    {
        
    }

    public void SetSprite(Sprite obj)
    {
        visualSprite = obj;
    }
}
