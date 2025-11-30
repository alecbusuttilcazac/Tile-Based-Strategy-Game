public class SettlerUnit : CivilianUnit
{
    public SettlerUnit(Tile tile) : base(tile)
    {
        unitName = "Settler";
        maxHealth = 35;
        currentHealth = maxHealth;
        canTraverseWater = true;
        canTraverseLand = true;
        tileMovement = 1;
        tileVision = 2;
        coinCost = 175;
        roundCost = 9;
    }
    
    void Start()
    {
        
    }
}