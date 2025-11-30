public class CityTile : ResourceTile
{
    public CityTile(int x, int y) : base(x, y)
    {
        movementCost = 1;
        defenseBonus = 5;
        impassable = false;
    }
}