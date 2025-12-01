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
    public int coinAmount = 0;
    
    public Player(string playerName, CityTile cityTile, TrainingCampTile trainingCamp, Unit soldier)
    {
        this.playerName = playerName;
        establishedCities.Add(cityTile);
        ownedBuildings.Add(trainingCamp);
        soldier.owner = this;
        ownedUnits.Add(soldier);
    }
    
    public int GetMoneyPerTurn(){
        int moneyPerTurn = 0;
        
        foreach(BuildingTile tile in ownedBuildings){
            if(tile is not ResourceTile) continue;
            
            ResourceTile resourceTile = (ResourceTile) tile;
            moneyPerTurn += resourceTile.coinsPerRound;
        }
        
        return moneyPerTurn;
    }
        
    public static bool IsManhattanAdjacent(Vector2Int a, Vector2Int b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        return dx + dy == 1;
    }
   
    public bool CanOwnTile(Tile tile) // Returns true if the player can own the tile
    {
        if(OwnsTile(tile)) return false;
        if (tile.owner) return false;

        // Check orthogonal adjacency to owned tiles (Manhattan distance == 1)
        foreach (Tile ownedTile in ownedTiles)
            if (IsManhattanAdjacent(ownedTile.position, tile.position)) return true;
        
        return false;
    }
        
    
    public void AddOwnedTile(Tile tile){
        if(!CanOwnTile(tile)) throw new System.Exception("Player "+playerName+" cannot own this tile");
        ownedTiles.Add(tile);
        tile.owner = this;
    }
    
    public bool OwnsTile(Tile tile)
    {
        if(tile.owner == this) return true;
        return false;
    }
    
    public void AddUnit(Unit unit){
        if(unit == null) throw new System.Exception("Cannot add null unit");
        if(ownedUnits.Contains(unit)) return; // Already owned
        
        ownedUnits.Add(unit);
        unit.owner = this;
    }
    
    public void AddCity(CityTile tile){
        if(tile == null) throw new System.Exception("Cannot add null city");
        if(establishedCities.Contains(tile)) return; // Already owned
        
        establishedCities.Add(tile);
        tile.owner = this;
    }
    
    public void AddBuilding(BuildingTile tile){
        if(tile == null) throw new System.Exception("Cannot add null building");
        if(ownedBuildings.Contains(tile)) return; // Already owned
        
        ownedBuildings.Add(tile);
        tile.owner = this;
    }
    
    public void AddBuilding(Tile oldTile, BuildingTile buildingTile, GameObject buildingPrefab, MapManager mapManager){
        if(buildingTile == null) 
            throw new System.Exception("Cannot add null building tile");
        if(oldTile == null) 
            throw new System.Exception("Cannot add null old tile");
        if(!OwnsTile(oldTile)) 
            throw new System.Exception("Cannot build on tile you don't own");
        
        GameObject oldVisual = oldTile.visualObject;
        
        // Create new building visual
        GameObject newVisual = Object.Instantiate(
            buildingPrefab,
            oldVisual.transform.position,
            Quaternion.identity,
            mapManager.transform
        );
        newVisual.name = $"Building_{oldTile.position.x:D2}x_{oldTile.position.y:D2}y";
        
        // Set up the new building tile at same position
        buildingTile.position = oldTile.position;
        buildingTile.owner = this;
        buildingTile.SetVisualObject(newVisual);
        
        // Replace in grid
        mapManager.SetTile(buildingTile.position.x, buildingTile.position.y, buildingTile);
        
        // Update player's lists
        ownedBuildings.Add(buildingTile);
        
        // Destroy old visual
        Object.Destroy(oldVisual);
    }
    
    public bool CanAffordItem(Unit unit){
        if(unit.coinCost > coinAmount) return false;
        return true;
    }
    
    public bool CanAffordItem(BuildingTile tile){
        if(tile.coinCost > coinAmount) return false;
        return true;
    }
    
    public void NextRound(){
        foreach(BuildingTile building in ownedBuildings){
            building.NextRound();
        }
    }
}