public class BuilderUnit : CivilianUnit
{
    
    
    public BuilderUnit(Tile tile) : base(tile)
    {
        unitName = "Builder";
        maxHealth = 30;
        currentHealth = maxHealth;
        canTraverseWater = true;
        tileMovement = 1;
        tileVision = 1;
        coinCost = 1;
        roundCost = 1;
    }
    
}