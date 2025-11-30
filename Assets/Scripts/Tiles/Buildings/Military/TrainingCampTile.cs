public class TrainingCampTile : MilitaryTile
{
    public MilitaryUnit currentlyProducing = null;
    public int roundsUntilCompletion = 0;
    
    public TrainingCampTile(int x, int y) : base(x, y)
    {
        movementCost = 1;
        impassable = false;
    }
}