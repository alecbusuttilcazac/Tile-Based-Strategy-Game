public class SailorUnit : MilitaryUnit
{
    public SailorUnit(Tile tile) : base(tile)
    {
        unitName = "Sailor";
        maxHealth = 100;
        currentHealth = maxHealth;
        attack = 20;
        range = 2;
        meleeDefense = -15;
        rangedDefense = 5;

        canFortify = false;
        canTraverseWater = true;
        canTraverseLand = false;
        tileMovement = 3;
        tileVision = 2;
        coinCost = 150;
        roundCost = 8;
    }
}