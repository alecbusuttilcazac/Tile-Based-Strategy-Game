using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Manager References")]
    public MapManager mapManager;
    public UnitManager unitManager;
    
    [Header("Players")]
    public GameObject playersContainer; // Assign in Inspector
    [HideInInspector] public List<Player> players;
    public List<string> playerNames;

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    void Start()
    {
        Debug.Log("GameManager started.");
        
        // Initialise managers in specific order
        if(playerNames.Count > 4) throw new System.Exception("Too many players");
        List<Vector2Int> citiesCoordinates = mapManager.PlaceCities(playerNames.Count);
        List<Vector2Int> trainingCampsCoordinates = mapManager.PlaceTrainingCamps(citiesCoordinates);
        
        mapManager.Initialise(citiesCoordinates, trainingCampsCoordinates); // Map first
        unitManager.Initialise(); // Units need map to exist
        
        
    }
    
    void AddPlayers(List<Vector2Int> citiesCoordinates, List<Vector2Int> trainingCampsCoordinates){
        for(int i=0; i<playerNames.Count; i++){
            // Create a GameObject for this player
            GameObject playerObject = new(playerNames[i]);
            playerObject.transform.SetParent(playersContainer.transform);
            
            // Add Player component
            Player player = playerObject.AddComponent<Player>();
            
            // Get tiles
            CityTile city = (CityTile)mapManager.GetTile(citiesCoordinates[i]);
            TrainingCampTile trainingCamp = (TrainingCampTile)mapManager.GetTile(trainingCampsCoordinates[i]);
            
            // Initialize player data
            player.playerName = playerNames[i];
            player.establishedCities.Add(city);
            player.ownedBuildings.Add(trainingCamp);
            SoldierUnit unit = new SoldierUnit(mapManager.GetTile(citiesCoordinates[i]));
            unitManager.CreateUnit(unit, player);
            
            players.Add(player);
        }
    }
}