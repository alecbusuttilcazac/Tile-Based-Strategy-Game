public class CavalryUnit : MilitaryUnit
{
    public CavalryUnit(Tile tile) : base(tile)
    {
        unitName = "Cavalry";
        maxHealth = 125;
        currentHealth = maxHealth;
        attack = 25;
        range = 1;
        meleeDefense = 0;
        rangedDefense = -5;

        canFortify = true;
        fortifyBonus = meleeDefense * 0.15f; // 15% of max health as fortify bonus
        canTraverseWater = false;
        canTraverseLand = true;
        tileMovement = 2;
        tileVision = 1;
        coinCost = 150;
        roundCost = 7;
    }
}