public abstract class BuildingTile : Tile
{
    public int coinCost;
    public int roundCost;
    
    public BuildingTile(int x, int y) : base(x, y)
    {
        
    }
    
    public abstract void NextRound();
}