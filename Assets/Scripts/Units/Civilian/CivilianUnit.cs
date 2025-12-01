public class CivilianUnit : Unit
{    
    public CivilianUnit(Tile tile) : base (tile)
    {
        
    }
    
    public override void Damage(int attackDamage){
        maxHealth -= attackDamage;
    }
}