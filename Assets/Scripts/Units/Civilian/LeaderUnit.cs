public class LeaderUnit : CivilianUnit
{
    public LeaderUnit(Tile tile) : base(tile)
    {
        unitName = "Leader";
        maxHealth = 45;
        currentHealth = maxHealth;
        canTraverseWater = false;
        tileMovement = 1;
        tileVision = 2;
        coinCost = 150;
        roundCost = 5;
    }
    
    void Start()
    {
        
    }
}