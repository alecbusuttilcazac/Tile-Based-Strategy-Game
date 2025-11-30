public class ResourceTile : BuildingTile
{
    public int coinsPerRound = 5;
    
    public ResourceTile(int x, int y) : base(x, y)
    {
        
    }
    
    public override void NextRound(){
        owner.coinAmount += coinsPerRound;
    }
}