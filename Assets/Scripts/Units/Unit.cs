using UnityEngine;

public class Unit
{
    public string unitName;
    public int currentHealth;
    public int maxHealth;
    public bool canTraverseWater;
    public bool canTraverseLand;
    public int tileMovement; // Number of tiles the unit can move
    public int tileVision; // Number of tiles away the unit can see
    public int coinCost; // Cost in coins to produce the unit
    public int roundCost; // Number of rounds required to produce the unit
    public bool isSelected = false;
    public bool hasMoved = false;
    
    public Player owner;
    public Tile currentTile;
    public GameObject visualObject; // contains sprite and transform info
    
    public Unit(Tile currentTile){
        this.currentTile = currentTile;
    }

    public void Kill()
    {
        if (owner) owner.ownedUnits.Remove(this);
        if (visualObject) Object.Destroy(visualObject);
    }
    
    public void Heal(int addedHealth){
        currentHealth += addedHealth;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
