    // remember you can NOT have even numbers of height or width in this style of block maze
    // to ensure we can get walls around all tunnels...  so use 21 x 13 , or 7 x 7 for examples.

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ExitGames.Client.Photon;
    using Photon.Pun;
    using Photon.Realtime;
    using UnityEngine;
    using UnityEngine.Serialization;
    using Random = System.Random;

    namespace Puzzles
    {
        public class MazeGenerator : MonoBehaviour {
            public int width, height;
            public Material brick;
            private int[,] maze;
            private readonly List<Vector3> pathMazes = new List<Vector3>();
            private readonly Stack<Vector2> tiletoTry = new Stack<Vector2>();
            private readonly List<Vector2> offsets = new List<Vector2> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
            private Vector2 currentTile;
            [FormerlySerializedAs("Mazes")] public List<string> mazes;
            private string mazeString;

            private Vector2 CurrentTile {
                get => currentTile;
                set {
                    if (value.x < 1 || value.x >= this.width - 1 || value.y < 1 || value.y >= this.height - 1){
                        throw new ArgumentException("Width and Height must be greater than 2 to make a maze");
                    }
                    currentTile = value;
                }
            }

            private static MazeGenerator Instance { get; set; }
            void Awake()  { Instance = this;}

            void Start()
            {
                MakeBlocks();
            }

            
            
            void MakeBlocks() {
       
            
                maze = new int[width, height];
                if (mazes == null)
                {
                    for (int x = 0; x < width; x++) {
                        for (int y = 0; y < height; y++)  {
                            maze[x, y] = 1;
                        }
                    }
                    maze = CreateMaze(); 
                }
                else
                {
                    int seed;
                    if (NetworkManager.instance)
                    {
                        seed = NetworkManager.instance.Seed % mazes.Count;
                    }
                    else
                    {
                        Debug.LogWarning("Couldn't find instance of network manager. Setting seed to 0");
                        seed = 0;
                    }
                    mazeString = mazes[seed];
                    mazeString = mazeString.Replace(" ", String.Empty);
                
                    Debug.Log(mazeString);
                    Debug.Log(mazeString.Length);
                    var idx = 0;
                    var i = 0;
                    for (var x = 0; x < width; x++)
                    {
                        for (var y = 0; y < height; y++)
                        {
                        
                            if (mazeString[idx].Equals('X'))
                            {
                                maze[x, y] = 1;

                            }
                            else
                                maze[x, y] = 0;

                            idx++;
                        }

                    }
                }

                CurrentTile = Vector2.one;
                tiletoTry.Push(CurrentTile);

                putBlocks();
                
                mazeString=mazeString+"\n";  // added to create String
                
                print (mazeString);  // added to create String
            }
            
            void putBlocks()
            {
                GameObject ptype = null;


                for (var i = 0; i <= maze.GetUpperBound(0); i++)
                {
                    for (var j = 0; j <= maze.GetUpperBound(1); j++)
                    {
                        switch (maze[i, j])
                        {
                            case 1:
                            {
                                mazeString = mazeString + "X"; // added to create String
                                var prefab = Resources.Load("Prefabs/MazeCube", typeof(GameObject));
                                ptype = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                                ptype.transform.parent = transform;

                                var localScale = ptype.transform.localScale;

                                var position = Vector3.zero;
                                var newPos = new Vector3(position.x + i * localScale.x, 0,
                                    position.z + j * localScale.z);

                                //newPos = transform.forward * newPos.z + transform.right * newPos.x;

                                ptype.transform.localPosition = newPos;


                                if (brick != null)
                                {
                                    ptype.GetComponent<Renderer>().material = brick;
                                }

                                break;
                            }
                            case 0:
                                mazeString = mazeString + "0"; // added to create String
                                pathMazes.Add(new Vector3(i, 0, j));
                                break;
                            
                        } 
                    }
                }
            }

            // =======================================
            public int[,] CreateMaze() {
       
                //local variable to store neighbors to the current square as we work our way through the maze
                List<Vector2> neighbors;
                //as long as there are still tiles to try
                while (tiletoTry.Count > 0)
                {
                    //excavate the square we are on
                    maze[(int)CurrentTile.x, (int)CurrentTile.y] = 0;
                    //get all valid neighbors for the new tile
                    neighbors = GetValidNeighbors(CurrentTile);
                    //if there are any interesting looking neighbors
                    if (neighbors.Count > 0)
                    {
                        //remember this tile, by putting it on the stack
                        tiletoTry.Push(CurrentTile);
                        //move on to a random of the neighboring tiles
                        var rnd = new Random();
                        CurrentTile = neighbors[rnd.Next(neighbors.Count)];
                    }
                    else
                    {
                        //if there were no neighbors to try, we are at a dead-end toss this tile out
                        //(thereby returning to a previous tile in the list to check).
                        CurrentTile = tiletoTry.Pop();
                    }
                }
                print("Maze Generated ...");
                return maze;
            }
       
            // ================================================
            // Get all the prospective neighboring tiles "centerTile" The tile to test
            // All and any valid neighbors</returns>
            private List<Vector2> GetValidNeighbors(Vector2 centerTile) {
                List<Vector2> validNeighbors = new List<Vector2>();
                //Check all four directions around the tile
                foreach (var offset in offsets) {
                    //find the neighbor's position
                    Vector2 toCheck = new Vector2(centerTile.x + offset.x, centerTile.y + offset.y);
                    //make sure the tile is not on both an even X-axis and an even Y-axis
                    //to ensure we can get walls around all tunnels
                    if (toCheck.x % 2 == 1 || toCheck.y % 2 == 1) {
                   
                        //if the potential neighbor is unexcavated (==1)
                        //and still has three walls intact (new territory)
                        if (maze[(int)toCheck.x, (int)toCheck.y]  == 1 && HasThreeWallsIntact(toCheck)) {
                       
                            //add the neighbor
                            validNeighbors.Add(toCheck);
                        }
                    }
                }
                return validNeighbors;
            }
            // ================================================
            // Counts the number of intact walls around a tile
            //"Vector2ToCheck">The coordinates of the tile to check
            //Whether there are three intact walls (the tile has not been dug into earlier.
            private bool HasThreeWallsIntact(Vector2 vector2ToCheck) {
           
                int intactWallCounter = 0;
                //Check all four directions around the tile
                foreach (var offset in offsets) {
               
                    //find the neighbor's position
                    Vector2 neighborToCheck = new Vector2(vector2ToCheck.x + offset.x, vector2ToCheck.y + offset.y);
                    //make sure it is inside the maze, and it hasn't been dug out yet
                    if (IsInside(neighborToCheck) && maze[(int)neighborToCheck.x, (int)neighborToCheck.y] == 1) {
                        intactWallCounter++;
                    }
                }
                //tell whether three walls are intact
                return intactWallCounter == 3;
            }
       
            // ================================================
            private bool IsInside(Vector2 p) {
                //return p.x >= 0  p.y >= 0  p.x < width  p.y < height;
                return p.x >= 0 && p.y >= 0 && p.x < width && p.y < height;
            }
        }
    }
