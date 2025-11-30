using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Manager References")]
    public MapManager mapManager;
    public UnitManager unitManager;

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
        
        // Initialize managers in specific order
        mapManager.Initialize();      // Map first
        unitManager.Initialize();     // Units need map to exist
    }
}