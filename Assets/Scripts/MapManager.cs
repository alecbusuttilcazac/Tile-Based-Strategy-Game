using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Tile Grid")]
    public Vector2Int gridSize = new(48, 27); // This is how big the map is: 48 tiles wide, 27 tiles tall.
    public float tileSpacing = 0f; // Space between tiles (can be used to add gaps).
    public int mapSeed = 0; // If 0, a random seed is used. If not, the same map layout will repeat every time (good for debugging or replayable maps).
    
    private Tile[,] grid; // a 2D array storing each tile object.
    private float tileSize; // size of each tile (to place visuals correctly).
    private float seedOffset; // random noise offset so maps are not identical.
    
    [Header("Special Tiles: Volcanoes, Fish")]
    public int maxNumberOfVolcanoes = 4; // How many of the volcano tiles can appear.
    public int maxNumberOfFish = 4; // How many of the fish tiles can appear.
    
    [Header("Cluster Scale (Lower = Bigger)")] // Scale = how spread out clusters are, Lower scale → bigger blobs, Higher scale → more scattered
    public float mountainClusterScale = 0.2f;
    public float waterClusterScale = 0.2f;
    public float forestClusterScale = 0.15f;
    
    [Header("Cluster Rarity (Higher = Rarer)")] // Higher threshold → fewer tiles of that type
    public float mountainThreshold = 0.75f;
    public float waterThreshold = 0.8f;
    public float forestThreshold = 0.55f;
    
    [Header("Tile Prefabs")] // References to the prefabs the script will create for each tile type.
    public GameObject plainsTilePrefab;
    public GameObject hillTilePrefab;
    public GameObject forest1TilePrefab;
    public GameObject forest2TilePrefab;
    public GameObject farmTilePrefab;
    public GameObject lumberHutTilePrefab;
    public GameObject mountainTilePrefab;
    public GameObject volcanoTilePrefab;
    public GameObject waterTilePrefab;
    public GameObject fishTilePrefab;
    
    public void Initialize()
    {
        Debug.Log("MapManager initialized.");
        
        // Checks if tile prefabs are square
        if(plainsTilePrefab.transform.localScale.x != plainsTilePrefab.transform.localScale.y
            || !plainsTilePrefab){
            throw new Exception("Tile scale is not square.");
        }
        
        // If mapSeed == 0, pick a random noise offset otherwise use the given seed
        if (mapSeed == 0)
            seedOffset = UnityEngine.Random.Range(0f, 10000f);
        else
            seedOffset = mapSeed;
        
        // Gets the tile's size for correct placement
        tileSize = plainsTilePrefab.transform.localScale.x;

        // Calls InitialiseGrid() to actually build the map
        InitialiseGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Tile GetTile(int x, int y) { return grid[x, y]; }
    
    public void SetTile(int x, int y, Tile newTile) 
    { 
        if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y)
            throw new System.Exception($"Tile position ({x}, {y}) is out of bounds");
        grid[x, y] = newTile; 
    }
    
    void InitialiseGrid()
    {
        grid = new Tile[gridSize.x, gridSize.y]; // create the empty grid
        
        // Prepare lists for later cluster detection – These lists remember where certain tiles were placed.
        List<Vector2Int> mountainTiles = new();
        List<Vector2Int> plainsTiles = new();
        List<Vector2Int> waterTiles = new();
        
        bool oddTile = false; // for switching or rotating textures - random feel
        float rotation = 0f;
        
        // This double loop generates each tile
        for(int y = 0; y < gridSize.y; y++){
            if(gridSize.x % 2 == 0) oddTile = !oddTile;
            
            for(int x = 0; x < gridSize.x; x++){
                // Create tile data
                Tile tile;
                GameObject tilePrefab;
                
                oddTile = !oddTile;
                rotation = 0f;
                
                /*  
                    Generate Perlin noise values for each terrain type (using seedOffset for variation)
                    Perlin Noise returns a value between 0 and 1.
                    Higher → more likely to be that terrain.
                    Each has its own offset so they look different.
                    ClusterScale makes one type of terrain more likely than the other 
                */
                float mountainNoise = Mathf.PerlinNoise(
                    x * mountainClusterScale + seedOffset + 100f, 
                    y * mountainClusterScale + seedOffset + 100f
                );
                float waterNoise = Mathf.PerlinNoise(
                    x * waterClusterScale + seedOffset + 200f, 
                    y * waterClusterScale + seedOffset + 200f
                );
                float forestNoise = Mathf.PerlinNoise(
                    x * forestClusterScale + seedOffset + 300f, 
                    y * forestClusterScale + seedOffset + 300f
                );

                // Decide what type of tile it becomes
                // Check terrain types in priority order
                // Order matters (mountains > water > forest > plains):
                // This creates natural terrain variety.
                
                if (waterNoise > waterThreshold){
                    tile = new WaterTile(x, y);
                    rotation = UnityEngine.Random.Range(0, 4) * 90f;
                    tilePrefab = waterTilePrefab;
                    waterTiles.Add(new Vector2Int(x, y));
                }
                else if (mountainNoise > mountainThreshold){
                    tile = new MountainTile(x, y);
                    tilePrefab = mountainTilePrefab;
                    mountainTiles.Add(new Vector2Int(x, y));
                }
                else if (forestNoise > forestThreshold){
                    tile = new ForestTile(x, y);
                    if(oddTile) tilePrefab = forest1TilePrefab;
                    else tilePrefab = forest2TilePrefab;
                }
                else{
                    tile = new PlainsTile(x, y);
                    rotation = UnityEngine.Random.Range(0, 4) * 90f;
                    tilePrefab = plainsTilePrefab;
                    plainsTiles.Add(new Vector2Int(x, y));
                }
                
                // Instantiate the tile GameObject – This creates the visual representation in the world.
                float offset = tileSize + tileSpacing;
                GameObject visual = Instantiate(
                    tilePrefab, 
                    new Vector3(x * offset, y * offset, 0), 
                    Quaternion.Euler(0, 0, rotation),
                    transform
                );
                visual.name = $"Tile_{x:D2}x_{y:D2}y";
                
                // Link data and visual
                tile.SetVisualObject(visual);
                
                // Save tile data into the grid – This allows the game to access each tile’s info later.
                grid[x, y] = tile;
            }
        }
        


        /* The script then places special objects on clusters: */
        // Place volcano within a mountain cluster
        PlaceTilesInClusters(
            mountainTiles, 
            maxNumberOfVolcanoes, 
            volcanoTilePrefab,
            (x, y) => new VolcanoTile(x, y)
        );
        
        // Place hill in plain cluster
        PlaceTilesInClusters(
            plainsTiles, 
            99, 
            hillTilePrefab,
            (x, y) => new HillTile(x, y)
        );
        
        // Place fish in water cluster
        PlaceTilesInClusters(
            waterTiles, 
            maxNumberOfFish, 
            fishTilePrefab,
            (x, y) => new FishTile(x, y)
        );
    }

    /*
    NOTE ON CLUSTERS:

    A cluster is a group of connected tiles (touching up, down, left, right).
    The bigger the cluster → the more chance to place a volcano, hill, or fish on it.

    */

    void PlaceTilesInClusters(
        /*
        This method:
            - Gets all clusters
            - Ignores tiny clusters
            - Randomly picks positions inside clusters
            - Replaces the tile with a special one (e.g., volcano, hill, fish)
            - It destroys the original visual and creates a new one.
        */
        List<Vector2Int> baseTiles, 
        int maxTiles,
        GameObject prefab,
        Func<int, int, Tile> createTile,
        int minClusterSize = 5,
        int largeClusterSize = 12,
        int veryLargeClusterSize = 20
    ){
        if (maxTiles == 0 || baseTiles.Count == 0) return;
        
        List<List<Vector2Int>> clusters = FindClusters(baseTiles);
        
        int tilesPlaced = 0;
        foreach (var cluster in clusters)
        {
            if (tilesPlaced >= maxTiles) break;
            if (cluster.Count < minClusterSize) continue;
            
            // Decide number of tiles for this cluster
            int tilesInCluster = 1;
            
            if (cluster.Count >= largeClusterSize && UnityEngine.Random.value < 0.2f)
                tilesInCluster = 2;
            
            if (cluster.Count >= veryLargeClusterSize && UnityEngine.Random.value < 0.05f)
                tilesInCluster = 3;
            
            // Place tiles at random positions in the cluster
            for (int i = 0; i < tilesInCluster && tilesPlaced < maxTiles; i++)
            {
                Vector2Int pos = cluster[UnityEngine.Random.Range(0, cluster.Count)];
                
                // Store old visual to destroy after creating new one
                GameObject oldVisual = grid[pos.x, pos.y].visualObject;
                
                // Replace tile data
                grid[pos.x, pos.y] = createTile(pos.x, pos.y);
                
                // Create new visual
                float offset = tileSize + tileSpacing;
                GameObject visual = Instantiate(
                    prefab,
                    new Vector3(pos.x * offset, pos.y * offset, 0),
                    Quaternion.identity,
                    transform
                );
                visual.name = $"Tile_{pos.x:D2}x_{pos.y:D2}y";
                grid[pos.x, pos.y].SetVisualObject(visual);
                
                // Destroy old visual after new one is set up
                if (oldVisual != null)
                    Destroy(oldVisual);
                
                tilesPlaced++;
            }
        }
    }
    

    // in brief: given a tile, if it is of type 'mountain', check it's neighbours up,down,left,right, if it is also of type mountain add it as part of the cluster
    List<List<Vector2Int>> FindClusters(List<Vector2Int> tiles)
    {
        /*
        This uses a flood fill method:
            - Take an unvisited tile
            - Add neighbors if they’re also in the set
            - Continue until no more neighbors exist
            - That whole group = one cluster
            - Repeat for all remaining tiles
            - This results in a list of clusters.
        */
        HashSet<Vector2Int> unvisited = new(tiles);
        List<List<Vector2Int>> clusters = new();
        
            while (unvisited.Count > 0){
                List<Vector2Int> cluster = new();
                Queue<Vector2Int> toCheck = new();
                
                Vector2Int start = unvisited.First();
                toCheck.Enqueue(start);
                unvisited.Remove(start);
                
                // Flood fill to find connected mountains
                while(toCheck.Count > 0){
                    Vector2Int current = toCheck.Dequeue();
                    cluster.Add(current);
                    
                    // Check 4 neighbors
                    Vector2Int[] neighbors = {
                        new(current.x + 1, current.y),
                        new(current.x - 1, current.y),
                        new(current.x, current.y + 1),
                        new(current.x, current.y - 1)
                    };
                    
                    foreach (var neighbor in neighbors)
                    {
                        if (unvisited.Contains(neighbor))
                        {
                            toCheck.Enqueue(neighbor);
                            unvisited.Remove(neighbor);
                        }
                    }
                }
                
                clusters.Add(cluster);
            }
            
        return clusters;
    }
}
