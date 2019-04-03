using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UI;

public class TileAutomata : MonoBehaviour {

    /**
     * Change for a tile to be alive
     */
    [Range(0, 100)]
    public int iniChance;

    // Has more neighbors than birthlimit will make that tile alive
    [Range(1, 8)]
    public int birthLimit;
    // If a living cell has less neighbours than this limit, it will die
    [Range(1, 8)]
    public int deathLimit;

    /**
     * number of reptitions. mopre repitions and more connected it will be
     */
    [Range(1, 10)]
    public int numR;

    // number of save files
    private int count = 0;

    // Integer array that stores if tile is alive or not. 1 = alive
    private int[,] terrainMap;
    // Tile map size
    public Vector3Int tmpSize;
    // The walkable area
    public Tilemap topMap;
    // THe water or lava
    public Tilemap botMap;

    // For walkable area
    public TerrainTile botTile;
    // For water or lava
    public AnimatedTile topTile;

    private bool madePlayer = false;
    public GameObject player;

    // Helps to loop through knowing these
    int width;
    int height;

    public GameObject blackTile;
    public GameObject leftWallTile;
    public GameObject topWallTile;
    public GameObject rightWallTile;
    public GameObject bottomWallTile;
    public GameObject topLeftTile;
    public GameObject topRightTile;
    public GameObject BottomRightTile;
    public GameObject bottomLeftTile;

    public GameObject[] enemies;        
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private float timeLeft;
    private int currentWaveNumber = 1;
    public Text waveNumber;
    public Text timeUntilNextWave;
    private Timer timer;
    public int areaThatEnemiesCanSpawnIn = 10;

    void Start()
    {
        timeLeft = startWait;
        timer = Timer.Register(1f, () => updateWaveText(), isLooped: true);
        doSim(numR);
        StartCoroutine(SpawnWaves());
    }

    public void updateWaveText()
    {
        timeLeft -= 1;
        timeUntilNextWave.text = string.Format("Time left until next wave: {0} seconds", timeLeft);
        if (timeLeft == 0)
        {
            timeUntilNextWave.text = "Starting!!";
            timer.Pause();
        }
    }

    IEnumerator SpawnWaves()
    {
        Random rand = new Random();
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {

                if (player != null)
                {
                    while (true)
                    {
                        // Spawn enemies around the player
                        float angle = Random.Range(0.0f, Mathf.PI * 2);

                        // create a vector with length 1.0
                        Vector3 V = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                        // scale it to the desired length
                        V *= Random.Range(2, areaThatEnemiesCanSpawnIn);
                        Vector2 mapCoord = getPlayerToMapCoordinates(player.transform.position.x, player.transform.position.y);
                        try
                        {
                            Vector3 pos = new Vector3((int)(-(V.x + mapCoord.x) + width / 2), (int)(-(V.y + mapCoord.y) + height / 2), 0);
                            if (castleTiles[(int)pos.x, (int)(pos.y)] != CastleTiles.CastleTile)
                            {
                               
                                int index = Random.Range(0, enemies.Length);
                                Quaternion spawnRotation = Quaternion.identity;
                                Instantiate(enemies[index], pos, spawnRotation);
                                break;
                            }
                        }
                        catch
                        {

                        }
                        
                    }
                 
                }

                yield return new WaitForSeconds(spawnWait);
            }
            currentWaveNumber++;
            waveNumber.text = string.Format("Wave {0}", currentWaveNumber);
            timeLeft = waveWait;
            timer.Resume();
            yield return new WaitForSeconds(waveWait);
        }
    }

    public Vector2 spawnTunnelForEnemies = Vector2.zero;

    private HashSet<string> visited = new HashSet<string>();
    private CastleTiles[,] castleTiles;

    public enum CastleTiles {
        CastleTile, NotCastle,
        TopLeft, TopLeftToMiddleTransition, TopMiddle, TopRightToMiddleTransition, TopRight,
        RightTopToMiddle, RightMiddle,
        BottomRight, BottomRightToMiddleTransition, BottomMiddle, BottomMiddleLeftTransition,
        BottomLeft, LeftMiddle, LeftToTopLeftTransition,
    }

    public List<Vector2> getShortestPath(float startingXPosWorld, float startingYPosWorld, float endXPosWorld, float endYPosWorld)
    {
        Debug.Log("Starting world: " + startingXPosWorld + " : " + startingYPosWorld);
        Vector2 startPos = getPlayerToMapCoordinates(startingXPosWorld,startingYPosWorld);
        //Debug.Log(startPos.x + " : " + startPos.y);
        Vector2 endPos = getPlayerToMapCoordinates(endXPosWorld, endYPosWorld);
        Debug.Log("player world coordinates: " + endXPosWorld + " : " + endYPosWorld);
        Queue<Node> queue = new Queue<Node>();
        List<Vector2> curPath = new List<Vector2>();
        curPath.Add(new Vector2(startPos.x, startPos.y));
        Node node = new Node(curPath, startPos.x, startPos.y);
        queue.Enqueue(node);
        int rowLength = castleTiles.GetLength(0);
        int colLength = castleTiles.GetLength(1);
        HashSet<string> visitedPos = new HashSet<string>();
        while (queue.Count != 0)
        {
            Node curNode = queue.Dequeue();
            if (Vector2.Distance(new Vector2(curNode.currX, curNode.currY), new Vector2(endPos.x,endPos.y)) <= 1)
            {
                // We have a list on how to get to the destionation
                List<Vector2> distancePath = new List<Vector2>();
                foreach (Vector2 vector in curNode.previousPositions)
                {
                    Vector2 direction = getMapToPlayerCoordinates(vector.x, vector.y);
                    distancePath.Add(direction);
                }
                
                Debug.Log("Starting location for distance: " + endXPosWorld + " : " + endYPosWorld);
                return distancePath;
            }
            if (curNode.currX < 0 || curNode.currY < 0 || curNode.currX >= rowLength || curNode.currY >= colLength)
            {
                continue;
            }

            string curPos = curNode.currX + " : " + curNode.currY;
            if (visitedPos.Contains(curPos))
            {
                continue;
            }
            visitedPos.Add(curPos);
            try
            {

            } catch{
                if (castleTiles[(int)Mathf.Round(curNode.currX), (int)Mathf.Round(curNode.currY)] == CastleTiles.CastleTile) // hit a wall
                {
                    continue;
                }
            }
            if (castleTiles[(int) Mathf.Round(curNode.currX), (int) Mathf.Round(curNode.currY)] == CastleTiles.CastleTile) // hit a wall
            {
                continue;
            }
            if (castleTiles[(int)(curNode.currX), (int)(curNode.currY)] == CastleTiles.CastleTile) // hit a wall
            {
                continue;
            }
            List<Vector2> newList = new List<Vector2>(curNode.previousPositions);
            newList.Add(new Vector2(curNode.currX, curNode.currY + 1));
            Node upNode = new Node(newList, curNode.currX, curNode.currY + 1);
            queue.Enqueue(upNode);

            List<Vector2> leftList = new List<Vector2>(curNode.previousPositions);
            leftList.Add(new Vector2(curNode.currX - 1, curNode.currY));
            Node leftNode = new Node(leftList, curNode.currX - 1, curNode.currY);
            queue.Enqueue(leftNode);

            List<Vector2> downList = new List<Vector2>(curNode.previousPositions);
            downList.Add(new Vector2(curNode.currX, curNode.currY - 1));
            Node downNode = new Node(downList, curNode.currX, curNode.currY - 1);
            queue.Enqueue(downNode);

            List<Vector2> rightList = new List<Vector2>(curNode.previousPositions);
            rightList.Add(new Vector2(curNode.currX + 1, curNode.currY));
            Node rightNode = new Node(rightList, curNode.currX + 1, curNode.currY);
            queue.Enqueue(rightNode);
        }
        
        return new List<Vector2>();
    }

    public class Node
    {
        public List<Vector2> previousPositions;
        public float currX;
        public float currY;
        public Node(List<Vector2> previousPositions, float x, float y)
        {
            this.previousPositions = previousPositions;
            this.currX = x;
            this.currY = y;
        }
    }

    public Vector2 getPlayerToMapCoordinates(float x, float y)
    {
        return new Vector2(-x + width/2 , -y + height / 2);
    }

    public Vector2 getMapToPlayerCoordinates(float x, float y)
    {
        return new Vector2(-x + width / 2, -y + height / 2);
    }

    // Do simulation, takes in number of reptitions
    public void doSim(int nu)
    {
        // False = should only clear tilemaps and not integer array of filled/not filled
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (castleTiles == null)
        {
            this.castleTiles = new CastleTiles[tmpSize.x, tmpSize.y];
        }

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }

        // Runs the amount of times you want to do simulations
        for (int i = 0; i < nu; i++)
        {
            // Updates the map with death and alive cells
            terrainMap = genTilePos(terrainMap);
        }
        GameObject castleHolder = GameObject.Find("CastleHolder");
        // Go through all the tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Always set the bottom tile as it will fill in the map completly
                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                Vector3Int tilePos = new Vector3Int(-x + width / 2, -y + height / 2, 0);
                this.castleTiles[x, y] = CastleTiles.NotCastle;
                // We have an alive tile (acutally dead tile since I switched top and bottom)
                if (terrainMap[x, y] == 1)
                {
                    // We don't want to start our tilemap at 0,0 but rather the middle
                    //topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                    this.castleTiles[x, y] = CastleTiles.CastleTile;
                    var castleObject = Instantiate(this.bottomWallTile, tilePos, Quaternion.identity);
                    castleObject.transform.parent = castleHolder.transform;
                }
                

            }
        }

        int rowLength = castleTiles.GetLength(0);
        int colLength = castleTiles.GetLength(1);
        int[,] castleCount = new int[rowLength, colLength];

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                for (int i2 = i - 1; i2 <= i + 1; i2++)
                {
                    for (int j2 = j - 1; j2 <= j + 1; j2++)
                    {
                        castleCount[i, j] = castleCount[i, j] + countCastleTiles(castleTiles, i2, j2);
                    }
                }

            }
        }

        // Instantiate prefabs based on count
        int maxCount = 0;
        int bestX = 0;
        int bestY = 0;
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                int currentCount = countOfConnectedLandMass(i, j);
                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                    bestX = i;
                    bestY = j;
                }

            }
        }
        spawnTunnelForEnemies.x = -bestX + width / 2;
        spawnTunnelForEnemies.y = -bestY + height / 2;

        Vector3 playerPos = new Vector3(-bestX + width / 2, -bestY + height / 2, 0);
        var playerObject = Instantiate(player, playerPos, Quaternion.identity);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().followTarget = playerObject;
        playerObject.name = "Player";
        this.player = playerObject;

    }

    public int countOfConnectedLandMass(int x, int y)
    {
        int rowLength = castleTiles.GetLength(0);
        int colLength = castleTiles.GetLength(1);
        if (x < 0 || y < 0 || x >= rowLength || y >= colLength)
        {
            return 0;
        }

        if (visited.Contains(x + " : " + y))
        {
            return 0;
        }

        visited.Add(x + " : " + y);
        
        if (castleTiles[x,y] == CastleTiles.NotCastle)
        {
            return 1 + countOfConnectedLandMass(x + 1, y) + countOfConnectedLandMass(x - 1, y) + countOfConnectedLandMass(x, y + 1) + countOfConnectedLandMass(x, y - 1);
        }
        return 0;
    }

    public bool outOfBounds(int x, int y)
    {
        int rowLength = castleTiles.GetLength(0);
        int colLength = castleTiles.GetLength(1);

        if (x < 0 || x >= rowLength)
        {
            return true;
        }
        if (y < 0 || y >= colLength)
        {
            return true;
        }
        return false;
    }

    public int countCastleTiles(CastleTiles[,] castleTiles, int i, int j)
    {
        int rowLength = castleTiles.GetLength(0);
        int colLength = castleTiles.GetLength(1);

        if (i < 0 || j < 0 || i >= rowLength|| j >= colLength)
        {
            return 1;
        }
        if (castleTiles[i,j] == CastleTiles.CastleTile)
        {
            return 1;
        }

        return 0;
    }

    /**
     * Generates the terrainMap with random living or dead values
     */
    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }

        }

    }

    // Uses the game of life generation with birth limits and deaht limits and reptitions
    // generates a map of 0-1 that represents that top and bottom
    public int[,] genTilePos(int[,] oldMap)
    {
        //Output array
        int[,] newMap = new int[width,height];
        int neighb;
        // virtual rectangle of x and y
        // -1x, -1y, 0 in z direction, length of 3 and 3 in x and y, 1 in z
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // The nieghbour check starts at 0
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    // Exclude because this means it's the current position
                    if (b.x == 0 && b.y == 0) continue;
                    // Exclude values outside of the map
                    if (x+b.x >= 0 && x+b.x < width && y+b.y >= 0 && y+b.y < height)
                    {
                        // Add the value of the oldmap to neighbour
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        // At the border, so draw a border
                        neighb++;
                    }
                }
                // If tile is alive
                if (oldMap[x,y] == 1)
                {
                    // This tile will die and become 0
                    if (neighb < deathLimit) newMap[x, y] = 0;

                        else
                        {
                            // Tile becomes alive
                            newMap[x, y] = 1;

                        }
                }

                if (oldMap[x,y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;

                else
                {
                    newMap[x, y] = 0;
                }
                }

            }

        }



        return newMap;
    }


	void Update () {
        /*
        // Do the number of reptitions
        if (Input.GetMouseButtonDown(0))
            {
            doSim(numR);
            }

        // Right clicking will clear the map
        if (Input.GetMouseButtonDown(1))
            {
            clearMap(true);
            }

         
        if (Input.GetMouseButton(2))
        {
            SaveAssetMap();
            count++;
        }
        */







        }


    public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath,mf))
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An ERROR occured while trying to saveTilemap under" + savePath, "Continue");
            }


        }


    }

    // False = should only clear tilemaps and not integer array of filled/not filled
    public void clearMap(bool complete)
    {

        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }


    }



}
