public class ScoutUnit : CivilianUnit
{
    public ScoutUnit(Tile tile) : base(tile)
    {
        unitName = "Scout";
        maxHealth = 20;
        currentHealth = maxHealth;
        canTraverseWater = false;
        canTraverseLand = true;
        tileMovement = 3;
        tileVision = 2;
        coinCost = 80;
        roundCost = 4;
    }
    
    void Start()
    {
        
    }
}