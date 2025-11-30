public class ShieldedUnit : MilitaryUnit{
    
    public ShieldedUnit(Tile tile) : base(tile)
    {
        unitName = "Shielder";
        maxHealth = 175;
        currentHealth = maxHealth;
        attack = 8;
        range = 1;
        meleeDefense = 20;
        rangedDefense = -10;

        canFortify = true;
        fortifyBonus = meleeDefense * 0.12f; // 12% of max health as fortify bonus
        canTraverseWater = false;
        canTraverseLand = true;
        tileMovement = 1;
        tileVision = 1;
        coinCost = 135;
        roundCost = 7;
    }
    
}