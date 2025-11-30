public class CityTile : BuildingTile
{
    public int moneyPerRound;
    
    public CityTile(int x, int y) : base(x, y)
    {
        movementCost = 1;
        defenseBonus = 5;
        impassable = false;
    }
    
    public override void NextRound(){
        owner.coinAmount += moneyPerRound;
    }
}