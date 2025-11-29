public class MilitaryUnit : Unit
{
    public int attack;
    public int range;
    public int defense;
    public bool isRanged;
    public bool canFortify;
    public bool isFortified;
    public int fortifyBonus;
    
    public MilitaryUnit(Tile tile) : base(tile){
        
    }
    
    void Start()
    {
        
    }
}
