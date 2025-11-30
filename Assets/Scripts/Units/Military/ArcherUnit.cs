public class ArcherUnit : MilitaryUnit
{
    public ArcherUnit(Tile tile) : base(tile)
    {
        unitName = "Archer";
        maxHealth = 75;
        currentHealth = maxHealth;
        attack = 15;
        range = 3;
        meleeDefense = -10;
        rangedDefense = 0;
        
        canFortify = false;
        canTraverseWater = false;
        canTraverseLand = true;
        tileMovement = 1;
        tileVision = 1;
        coinCost = 85;
        roundCost = 4;
    }
}