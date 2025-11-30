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
    
    public override void NextRound(){
        if(roundsUntilCompletion == 0){
            //add functionality and add current unit info
            
            currentlyProducing = null;
            return;
        }
        
        roundsUntilCompletion--;
    }
}