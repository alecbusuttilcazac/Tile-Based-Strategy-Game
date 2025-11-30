public class SoldierUnit : MilitaryUnit
{
    public SoldierUnit(Tile tile) : base(tile)
    {
        unitName = "Soldier";
        maxHealth = 100;
        currentHealth = maxHealth;
        attack = 25;
        range = 1;
        meleeDefense = 0;
        rangedDefense = 0;
        
        canFortify = true;
        fortifyBonus = meleeDefense * 0.25f; // 25% of max health as fortify bonus
        canTraverseWater = false;
        canTraverseLand = true;
        tileMovement = 1;
        tileVision = 1;
        coinCost = 100;
        roundCost = 5;
    }
}