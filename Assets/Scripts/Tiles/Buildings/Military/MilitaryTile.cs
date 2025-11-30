public abstract class MilitaryTile : BuildingTile
{    
    public MilitaryTile(int x, int y) : base(x, y)
    {
        defenseBonus = 3;
    }
}