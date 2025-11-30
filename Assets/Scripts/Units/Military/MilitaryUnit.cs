public class MilitaryUnit : Unit
{
    public int attack;
    public int range;
    public int meleeDefense;
    public int rangedDefense;
    public bool canFortify;
    public bool isFortified;
    public float fortifyBonus;
    
    public MilitaryUnit(Tile tile) : base(tile){
        isFortified = false;
    }
}
