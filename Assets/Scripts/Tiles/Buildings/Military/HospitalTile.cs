public class HospitalTile : MilitaryTile
{
    public Unit unit = null;
    public float healPerRound = 0.2f; // Heals 20% of max health per round
    
    public HospitalTile(int x, int y) : base(x, y)
    {
        coinCost = 200;
        roundCost = 10;
        movementCost = 1;
        defenseBonus = 2;
        impassable = false;
    }
    
    public void Heal(){
        unit.Heal((int)(unit.maxHealth * healPerRound));
    }
    
    public override void NextRound(){
        Heal();
    }
}