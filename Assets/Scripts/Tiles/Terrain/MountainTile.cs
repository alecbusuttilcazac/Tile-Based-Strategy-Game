public class MountainTile : TerrainTile
{
    public MountainTile(int x, int y) : base(x, y)
    {
        movementCost = 1;
        defenseBonus = 0;
        impassable = true;
    }
}
