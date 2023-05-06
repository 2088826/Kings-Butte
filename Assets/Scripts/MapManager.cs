using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace finished1
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance { get { return _instance; } }

        [SerializeField] private GameObject grassOverlayPrefab;
        [SerializeField] private GameObject iceOverlayPrefab;
        [SerializeField] private GameObject tileContainerPrefab;

        public float littleBump;

        public Dictionary<Vector2Int, GameObject> map;
        private GameObject container;

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else
            {
                _instance = this;
            }

            container = Instantiate(tileContainerPrefab);
            container.name = "TileContainer";
        }

        // Start is called before the first frame update
        void Start()
        {
            littleBump = 0.0003f;
            var tileMap = gameObject.GetComponentInChildren<Tilemap>();
            map = new Dictionary<Vector2Int, GameObject>();
            int grassCount = 0;
            int iceCount = 0;

            BoundsInt bounds = tileMap.cellBounds;

            for (int z = bounds.max.z; z > bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey) && z >= 0)
                        {
                            string tileName = tileMap.GetTile(tileLocation).name;

                            if (tileName.Contains("GrassTile"))
                            {
                                var newTile = Instantiate(grassOverlayPrefab, container.transform);
                                newTile.name = "GrassTile " + grassCount++;
                                var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                                newTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                                newTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                                map.Add(tileKey, newTile);
                            }
                            else if (tileName.Contains("IceTile"))
                            {
                                var newTile = Instantiate(iceOverlayPrefab, container.transform);
                                newTile.name = "IceTile " + iceCount++;
                                var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);
                                newTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                                newTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;
                                map.Add(tileKey, newTile);
                            }
                        }
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
