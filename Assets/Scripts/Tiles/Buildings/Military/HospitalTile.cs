public class HospitalTile : MilitaryTile
{
    public HospitalTile(int x, int y) : base(x, y)
    {
        movementCost = 1;
        defenseBonus = 2;
        impassable = false;
    }
}