using UnityEngine;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    public string playerName;
    public List<Tile> ownedTiles = new();
    public List<Unit> ownedUnits = new();
    public List<CityTile> establishedCities = new();
    public List<BuildingTile> ownedBuildings = new();
    public int ownableTileRadius = 3;
    
    public Player(string playerName, CityTile cityTile, TrainingCampTile trainingCamp, Unit soldier)
    {
        this.playerName = playerName;
        establishedCities.Add(cityTile);
        ownedBuildings.Add(trainingCamp);
        ownedUnits.Add(soldier);
    }
    
    public int getMoneyPerTurn(){
        int moneyPerTurn = 0;
        
        foreach(BuildingTile tile in ownedBuildings){
            if(tile is not ResourceTile) continue;
            
            ResourceTile resourceTile = (ResourceTile) tile;
            moneyPerTurn += resourceTile.moneyPerRound;
        }
        
        return moneyPerTurn;
    }
        
    public static bool IsManhattanAdjacent(Vector2Int a, Vector2Int b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        return dx + dy == 1;
    }
   
    public bool canOwnTile(Tile tile) // Returns true if the player can own the tile
    {
        if(OwnsTile(tile)) return false;
        if (!tile.owner) return false;

        // Check orthogonal adjacency to owned tiles (Manhattan distance == 1)
        foreach (Tile ownedTile in ownedTiles)
        {
            if (IsManhattanAdjacent(ownedTile.position, tile.position))
            {
                return true;
            }
        }
        return false;
    }
        
    
    public void addOwnedTile(Tile tile){
        if(!canOwnTile(tile)) throw new System.Exception("Player "+playerName+" cannot own this tile");
        ownedTiles.Add(tile);
        tile.owner = this;
    }
    
    public bool OwnsTile(Tile tile)
    {
        if(tile.owner == this) return true;
        return false;
    }
}