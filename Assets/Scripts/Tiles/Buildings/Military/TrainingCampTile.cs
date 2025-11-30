public class TrainingCampTile : MilitaryTile
{
    public MilitaryUnit currentlyProducing = null;
    public int roundsUntilCompletion = 0;
    
    public TrainingCampTile(int x, int y) : base(x, y)
    {
        coinCost = 300;
        roundCost = 15;
        movementCost = 1;
        impassable = false;
    }
}