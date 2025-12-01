using UnityEngine;
using UnityEngine.AI;

public class UnitManager : MonoBehaviour
{    
    // Civilian Sprites
    public Sprite settlerSpriteSheet;
    public Sprite builderSpriteSheet;
    public Sprite scoutSpriteSheet;
    
    // Military Sprites
    public Sprite soldierSpriteSheet;
    public Sprite archerSpriteSheet;
    public Sprite shielderSpriteSheet;
    public Sprite cavalrySpriteSheet;
    public Sprite sailorSpriteSheet;
    
    public void Initialise()
    {
        Debug.Log("UnitManager initialized.");
        
        // Unit initialization code will go here
    }
    
    
    
    public void CreateUnit(Unit unit, Player unitOwner)
    {
        GameObject unitVisual = new GameObject($"{unitOwner.playerName}_{unit.GetType().Name}");
        unitVisual.AddComponent<SpriteRenderer>();
        unitVisual.AddComponent<Animator>();
        
        // Set sprite based on unit type
        if(unit is SoldierUnit){
            unitVisual.GetComponent<SpriteRenderer>().sprite = soldierSpriteSheet;
            // also set animator
        }
        else if(unit is ArcherUnit){
            // ...
        }
        
        // Position unit at its tile
        Vector2Int tilePos = unit.currentTile.position;
        unitVisual.transform.position = new Vector3(tilePos.x, tilePos.y, 0);
        
        // Link visual to unit data
        unit.visualObject = unitVisual;
        
        unitOwner.AddUnit(unit);
    }
}