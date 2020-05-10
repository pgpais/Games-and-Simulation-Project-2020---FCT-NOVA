using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Puzzle Room", menuName = "Puzzle/Sample Room", order = 0)]
    public class PuzzleRoomList : ScriptableObject
    {
        [Tooltip("Prefab to instantiate puzzles with")]
        [SerializeField]
        public List<GameObject> puzzlePrefabs;
    }
}