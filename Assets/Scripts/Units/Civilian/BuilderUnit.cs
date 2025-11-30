public class BuilderUnit : CivilianUnit
{
    public int timeToBuild; // number of rounds required to build structures
    public int maxBuilds; // maximum number of structures the builder can build
    public int buildsLeft; // number of structures the builder can still build
    
    public BuilderUnit(Tile tile) : base(tile)
    {
        unitName = "Builder";
        maxHealth = 30;
        currentHealth = maxHealth;
        canTraverseWater = true;
        canTraverseLand = true;
        tileMovement = 1;
        tileVision = 1;
        coinCost = 130;
        roundCost = 6;
    }
    
}