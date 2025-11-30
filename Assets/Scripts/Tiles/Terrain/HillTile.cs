public class HillTile : TerrainTile
{
    public HillTile(int x, int y) : base(x, y)
    {
        movementCost = 1;
        defenseBonus = 0;
        impassable = false;
    }
}
