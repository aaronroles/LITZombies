//-------------------------------------------------------------------------------------------------
//  ©Copyright KDToons & Games 2014
//-------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace KDInteractive.PathFinder
{
    public class KDPathFinder
    {
        #region "Private Class"

        /// <summary>
        /// Represents directions where iterate looking for the right path
        /// with value 1.4
        /// </summary>
        private class Skew
        {
            public readonly Vector2 Direction;

            public virtual float Cost { get { return 1.4f; } }

            public virtual void RemoveFromList(List<Skew> a_list) { }

            public Skew(Vector2 a_direction)
            {
                Direction = a_direction;
            }
        }
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Represents directions where iterate looking for the right path
        /// with value 1
        /// </summary>
        private class Straight : Skew
        {
            private readonly Skew m_cOne;
            private readonly Skew m_cTwo;

            public override float Cost { get { return 1.0f; } }

            public override void RemoveFromList(List<Skew> a_list)
            {
                a_list.Remove(m_cOne);
                a_list.Remove(m_cTwo);
            }

            public Straight(Vector2 a_direction, Skew a_cOne, Skew a_cTwo)
                : base(a_direction)
            {
                m_cOne = a_cOne;
                m_cTwo = a_cTwo;
            }
            //--------------------------------------------------------------------------------------
        }

        #endregion

        #region "Constants"

        public const float MIN_SIZE = .1f;
        public const float MAX_SIZE = 5f;
        public const float MIN_SEPARATION = 1.1f;
        public const float MAX_SEPARATION = 10.0f;
        //------------------------------------------------------------------------------------------
        #endregion

        #region "Fields"

        private static Vector2 m_up;
        private static Vector2 m_right;
        private static Vector2 m_down;
        private static Vector2 m_left;
        //------------------------------------------------------------------------------------------

        private static Vector2 m_topRight;
        private static Vector2 m_bottomRight;
        private static Vector2 m_bottomLeft;
        private static Vector2 m_topLeft;
        //------------------------------------------------------------------------------------------

        private static float m_size;
        private static int m_tileLayer;
        private static float m_eparation;
        private static GameObject[] m_tiles;
        private static List<GameObject> m_openList;
        private static List<GameObject> m_closedList;
        private static PolygonCollider2D m_floodArea;
        private static GameObject m_floodFillSeed;
        //------------------------------------------------------------------------------------------
        #endregion

        #region "Properties"

        public int Density { get { return m_tiles.Length; } }

        #endregion

        /// <summary>
        /// Configure the parameters of PathFinder2D, after set the values of the object will fill the 
        /// flood area of p_tiles.
        /// </summary>
        /// <param name="a_tileLayer">Physic layer where p_tiles must be setted, the flood area and 
        /// p_tiles must be setted on different physic layers</param>
        /// <param name="a_size">_size of the p_tiles</param>
        /// <param name="a_eparation">factor of separation between p_tiles</param>
        /// <param name="a_floodFillSeed">object where p_tiles started to grow and fill the flood 
        /// polygon area</param>
        /// <param name="a_floodArea">close are where p_tile can stay</param>
        public KDPathFinder
        (
            GameObject a_floodFillSeed,
            int a_tileLayer,
            float a_size,
            float a_eparation,
            PolygonCollider2D a_floodArea
        )
        {
            if (a_size < MIN_SIZE)
                throw new ArgumentException("The _size of the nodes must be bigger than "
                    + MIN_SIZE + " meter");

            if (a_tileLayer < 0)
                throw new ArgumentException("The layer of the Tiles must be bigger " +
                                            "or equal than 0");

            if (a_eparation < MIN_SEPARATION || a_eparation > MAX_SEPARATION)
                throw new ArgumentException("Separation must be between "
                    + MIN_SEPARATION + " and " + MAX_SEPARATION);

            if (!a_floodFillSeed)
                throw new ArgumentException("The seed can´t be null");

            m_size = a_size;
            m_eparation = a_eparation;
            m_tileLayer = a_tileLayer;
            m_floodArea = a_floodArea;
            InitVectors();

            FillChilds(a_floodFillSeed);

            m_openList = new List<GameObject>(m_tiles.Length);
            m_closedList = new List<GameObject>(m_tiles.Length);
        }
        //------------------------------------------------------------------------------------------


        private static void FillChilds(GameObject a_floodFillSeed)
        {
            m_tiles = new GameObject[a_floodFillSeed.transform.childCount];
            var seedTransform = a_floodFillSeed.transform;

            for (int i = 0; i < m_tiles.Length; i++)
            {
                GameObject tempTile = seedTransform.GetChild(i).gameObject;

                if (tempTile.GetComponent<KD2DTile>())
                    m_tiles[i] = seedTransform.GetChild(i).gameObject;
            }
        }
        //------------------------------------------------------------------------------------------

        private static void InitVectors()
        {
            float factor = m_size * m_eparation;

            m_up = Vector2.up * factor;
            m_left = -Vector2.right * factor;
            m_down = -Vector2.up * factor;
            m_right = Vector2.right * factor;

            m_topRight = (Vector2.up + Vector2.right) * factor;
            m_bottomRight = (-Vector2.up + Vector2.right) * factor;
            m_bottomLeft = (-Vector2.up - Vector2.right) * factor;
            m_topLeft = (Vector2.up - Vector2.right) * factor;
        }
        //------------------------------------------------------------------------------------------

        private static List<Skew> PathDirections()
        {
            var topR = new Skew(m_topRight);
            var botR = new Skew(m_bottomRight);
            var botL = new Skew(m_bottomLeft);
            var topL = new Skew(m_topLeft);

            return new List<Skew>
            {
                new Straight(m_up, topR, topL), 
                new Straight(m_right, topR, botR), 
                new Straight(m_down, botR, botL), 
                new Straight(m_left, botL, topL), 
                topR, 
                botR, 
                botL, 
                topL
            };
        }
        //------------------------------------------------------------------------------------------

        public List<KD2DTile> FindPath(KDNav2DAgent a_agent)
        {
            GameObject endTile = NearestTile(a_agent.Destination).gameObject;
            RestartMail(a_agent.NearestTile.gameObject, m_openList, m_closedList);

            if (endTile != a_agent.NearestTile.gameObject)
            {
                GameObject tileGo;
                Vector2 end = endTile.transform.position;
                do
                {
                    KD2DTile currentTile;
                    BinaryDelete(m_openList, m_closedList, out tileGo, out currentTile);

                    int i = 0;
                    List<Skew> dirs = PathDirections();
                    Collider2D collider2D = tileGo.collider2D;
                    collider2D.enabled = false;
                    do
                    {
                        Skew dir = dirs[i];
                        RaycastHit2D hit = Physics2D.Raycast
                            (
                                tileGo.transform.position,
                                dir.Direction.normalized,
                                dir.Direction.magnitude,
                                1 << m_tileLayer
                            );

                        if (hit)
                        {
                            var hitTile = hit.collider.GetComponent<KD2DTile>();
                            if (hitTile && !hitTile.Occupied && !hitTile.IsBloked)
                            {
                                float nextG = currentTile.G + dir.Cost;
                                if (!hitTile.IsWrited)
                                {
                                    WriteTile(hitTile, tileGo, nextG, end);
                                    continue;
                                }

                                if (hitTile.G > nextG)
                                    WriteTile(hitTile, tileGo, nextG);
                            }
                            else
                                dir.RemoveFromList(dirs);
                        }
                        else
                            dir.RemoveFromList(dirs);

                    } while (++i < dirs.Count);
                    collider2D.enabled = true;
                } while (tileGo != endTile && m_openList.Count > 0);
            }

            var finalList = new List<KD2DTile>();

            if (endTile)
                finalList = FinalList(endTile.GetComponent<KD2DTile>());

            return finalList;
        }
        //------------------------------------------------------------------------------------------

        private void WriteTile(KD2DTile a_grazedTile, GameObject a_currenTileGameObject, float a_nextG)
        {
            a_grazedTile.Father = a_currenTileGameObject;
            a_grazedTile.G = a_nextG;
            a_grazedTile.SetF();
            Reorder(a_grazedTile.gameObject, m_openList);
        }
        //------------------------------------------------------------------------------------------

        private void WriteTile
        (
            KD2DTile a_tile,
            GameObject a_currenTileGameObject,
            float a_nextG,
            Vector2 a_end
        )
        {
            Vector3 nextTilePosition = a_tile.gameObject.transform.position;
            a_tile.Father = a_currenTileGameObject;
            a_tile.G = a_nextG;
            a_tile.H = Math.Abs(a_end.x - nextTilePosition.x) + Math.Abs(a_end.y - nextTilePosition.y);
            a_tile.IsWrited = true;
            a_tile.SetF();
            Insert(a_tile.gameObject, m_openList);
        }
        //------------------------------------------------------------------------------------------

        private static void RestartList(ICollection<GameObject> a_list)
        {
            foreach (GameObject tile in a_list)
                tile.GetComponent<KD2DTile>().Restart();
            a_list.Clear();
        }
        //------------------------------------------------------------------------------------------

        private static void RestartMail
        (
            GameObject a_startTile,
            List<GameObject> a_openList,
            List<GameObject> a_closeList
        )
        {
            RestartList(a_openList);
            RestartList(a_closeList);
            a_openList.Add(a_startTile);
        }
        //------------------------------------------------------------------------------------------

        private static List<KD2DTile> FinalList(KD2DTile a_destination)
        {
            var list = new List<KD2DTile>();

            for (var c = a_destination; c.Father != null; c = c.Father.GetComponent<KD2DTile>())
                list.Add(c);

            return list;
        }
        //------------------------------------------------------------------------------------------

        private static void Insert(GameObject a_tile, IList<GameObject> a_list)
        {
            a_list.Add(a_tile);
            if (a_list.Count > 1)
            {
                var m = a_list.Count - 1;
                BinarySwitch(a_list, m);
            }
        }
        //------------------------------------------------------------------------------------------

        private static void Reorder(GameObject a_tile, IList<GameObject> a_list)
        {
            int index = a_list.IndexOf(a_tile);
            BinarySwitch(a_list, index);
        }
        //------------------------------------------------------------------------------------------

        private static void BinarySwitch(IList<GameObject> a_list, int a_index)
        {
            var half = a_index / 2;
            while
            (
                a_index != 0 &&
                a_list[a_index].GetComponent<KD2DTile>().F <= a_list[half].GetComponent<KD2DTile>().F
            )
            {
                var temp = a_list[half];
                a_list[half] = a_list[a_index];
                a_list[a_index] = temp;
                a_index = half;
                half = a_index / 2;
            }
        }
        //------------------------------------------------------------------------------------------

        private void BinaryDelete
        (
            IList<GameObject> a_openList,
            List<GameObject> a_closeList,
            out GameObject a_tile,
            out KD2DTile a_currentTile)
        {
            a_tile = a_openList.FirstOrDefault();
            a_currentTile = a_tile.GetComponent<KD2DTile>();
            a_currentTile.IsBloked = true;
            a_closeList.Add(a_tile);

            a_openList.RemoveAt(0);
            if (a_openList.Count > 0)
            {
                a_openList.Insert(0, a_openList[a_openList.Count - 1]);
                a_openList.RemoveAt(a_openList.Count - 1);

                var i = 0;

                while (true)
                {
                    var j = i;

                    if (2 * j + 1 <= a_openList.Count - 1)
                    {
                        if 
                        (
                            a_openList[j].GetComponent<KD2DTile>().F >= 
                            a_openList[2 * j].GetComponent<KD2DTile>().F
                        )
                            i = 2 * j;

                        if 
                        (
                            a_openList[i].GetComponent<KD2DTile>().F >= 
                            a_openList[2 * j + 1].GetComponent<KD2DTile>().F
                        )
                            i = 2 * j + 1;
                    }
                    else if 
                    (
                        2 * j <= a_openList.Count - 1 &&
                        a_openList[j].GetComponent<KD2DTile>().F >= 
                        a_openList[2 * j].GetComponent<KD2DTile>().F
                    )
                        i = 2 * j;

                    if (i == j)
                        break;

                    var temp = a_openList[j];
                    a_openList[j] = a_openList[i];
                    a_openList[i] = temp;
                }
            }
        }
        //------------------------------------------------------------------------------------------

        public KD2DTile NearestTile(Vector2 a_position)
        {
            GameObject gameObject = null;
            float minimum = float.MaxValue;

            int length = m_tiles.Length;
            for (var i = 0; i < length; i++)
                if (!m_tiles[i].GetComponent<KD2DTile>().Occupied)
                {
                    float distance = Vector2.Distance(a_position, m_tiles[i].transform.position);

                    if (distance < minimum)
                    {
                        minimum = distance;
                        gameObject = m_tiles[i];

                        if (minimum <= m_size)
                            break;
                    }
                }
            return gameObject.GetComponent<KD2DTile>();
        }
        //------------------------------------------------------------------------------------------

        /// <summary>
        /// Made a flood of p_tiles inside the navigation polygon.
        /// </summary>
        /// <returns>A "chessboard" of p_tiles, where you can move using A* pathfinding method</returns>
        private static GameObject[] Fill()
        {
            PolygonCollider2D[] obstacles =
            (
                from polygonCollider2D
                in m_floodArea.gameObject.GetComponentsInChildren<PolygonCollider2D>()
                where polygonCollider2D.gameObject != m_floodArea.gameObject
                select polygonCollider2D
            ).ToArray();

            PolygonCollider2D[] staticObstacles =
            (
                from obstacle
                in obstacles
                let oCompo = obstacle.GetComponent<KD2DObstacle>()
                where oCompo!=null && !oCompo.IsDynamic
                select obstacle
            ).ToArray();

            List<GameObject> tiles = Init<KD2DTileZero>(m_floodFillSeed.transform, m_size);

            Vector2[] directions =
            {
                m_up, m_right, m_down, m_left, m_topRight, m_bottomRight, m_bottomLeft, m_topLeft
            };

            int level = 0;
            GameObject[] nodes;
            var virtualTile = new Rect(0, 0, m_size, m_size);
            do
            {
                nodes =
                (
                    from tile
                    in tiles
                    where tile.GetComponent<KD2DTile>().Level == level
                    select tile
                ).ToArray();

                for (int i = 0; i < nodes.Length; i++)
                {
                    GameObject levelNode = nodes[i];
                    var currentPosition = new Vector2
                    (
                        levelNode.transform.position.x,
                        levelNode.transform.position.y
                    );

                    for (int j = 0; j < directions.Length; j++)
                    {
                        Vector2 nextPosition = currentPosition + directions[j];
                        virtualTile.center = nextPosition;

                        if (ItsFree(tiles, staticObstacles, virtualTile, nextPosition))
                        {
                            string name = "til-E";
                            //Math.Round(nextPosition.x, 1).ToString("0.0") + " | " + Math.Round(nextPosition.y, 1).ToString("0.0");
                            GameObject newtile = CreateNode<KD2DTile>
                            (
                                m_floodFillSeed.transform,
                                nextPosition,
                                level + 1,
                                m_size,
                                name
                            );

                            tiles.Add(newtile);
                        }
                    }
                }
                level++;
            } while (nodes.Length > 0);
            return tiles.ToArray();
        }
        //------------------------------------------------------------------------------------------

        private static bool ItsFree
        (
            IEnumerable<GameObject> a_tiles,
            PolygonCollider2D[] a_obstacles,
            Rect a_vTile,
            Vector2 a_point
        )
        {
            var corner1 = new Vector2(a_vTile.xMin, a_vTile.yMin);
            var corner2 = new Vector2(a_vTile.xMax, a_vTile.yMin);
            var corner3 = new Vector2(a_vTile.xMin, a_vTile.yMax);
            var corner4 = new Vector2(a_vTile.xMax, a_vTile.yMax);

            Vector2[] corners = { corner1, corner2, corner3, corner4 };

            if
            (
                corners.Any(a_corner => !m_floodArea.OverlapPoint(a_corner) ||
                a_obstacles.Any(a_obstacle => a_obstacle.OverlapPoint(a_corner)))
            )
                return false;

            if (a_obstacles.Any(a_collider2D => a_collider2D.OverlapPoint(a_point)))
                return false;

            if
            (
                a_obstacles.Any
                (
                    a_obstacle =>
                    a_obstacle.points.Any
                    (
                        a_oPoint =>
                        a_vTile.Contains
                        (
                            ((Vector2)KDTools.Adjust
                                (
                                    a_oPoint,
                                    a_obstacle.transform.position,
                                    a_obstacle.transform.eulerAngles,
                                    a_obstacle.transform.localScale
                                )
                            )
                        )
                    )
                )
            )
                return false;

            if (a_tiles.Any(a_tile => Vector2.Distance(a_point, a_tile.transform.position) < m_size))
                return false;

            return true;
        }
        //------------------------------------------------------------------------------------------

        private static List<GameObject> Init<U>
        (
            Transform a_seed,
            float a_size
        ) where U : KD2DTile
        {
            GameObject firstTile = CreateNode<U>
            (
                a_seed,
                a_seed.transform.position,
                0,
                a_size,
                "A* zero "
            );

            return new List<GameObject> { firstTile };
        }
        //------------------------------------------------------------------------------------------

        private static GameObject CreateNode<U>
        (
            Transform a_parent,
            Vector3 a_nextPosition,
            int a_currentLevel,
            float a_size,
            string a_name
        )
        where U : KD2DTile
        {
            var tileGameObject = new GameObject(a_name, typeof(BoxCollider2D), typeof(U));

            tileGameObject.transform.parent = a_parent;
            tileGameObject.transform.position = new Vector3(a_nextPosition.x, a_nextPosition.y, 0);
            tileGameObject.transform.rotation = Quaternion.identity;
            tileGameObject.layer = m_tileLayer;

            var collider2D = tileGameObject.GetComponent<BoxCollider2D>();
            ConfigureCollider(collider2D, a_size);

            var tile2DComponent = tileGameObject.GetComponent<U>();
            tile2DComponent.Level = a_currentLevel;

            return tileGameObject;
        }
        //------------------------------------------------------------------------------------------

        private static void ConfigureCollider(BoxCollider2D a_collider2D, float a_size)
        {
            a_collider2D.isTrigger = true;
            a_collider2D.size = new Vector2(a_size, a_size);
        }
        //------------------------------------------------------------------------------------------

        public static int Bake
        (
            int a_tileLayer,
            float a_size,
            float a_separation,
            GameObject a_floodFillSeed,
            PolygonCollider2D a_floodArea
        )
        {
            Init(a_tileLayer, a_size, a_separation, a_floodFillSeed, a_floodArea);

            KillChilds(a_floodFillSeed);
            m_tiles = Fill();
            return m_tiles.Length;
        }
        //------------------------------------------------------------------------------------------

        private static void Init
        (
            int a_tileLayer,
            float a_size,
            float a_separation,
            GameObject a_floodFillSeed,
            PolygonCollider2D a_floodArea
        )
        {
            if (a_size < MIN_SIZE)
                throw new ArgumentException("The _size of the nodes must be bigger than "
                    + MIN_SIZE + " meter");

            if (a_tileLayer < 0)
                throw new ArgumentException("The layer of the Tiles must be bigger or equal " +
                                            "than 0");

            if (a_separation < MIN_SEPARATION || a_separation > MAX_SEPARATION)
                throw new ArgumentException("Separation must be between "
                    + MIN_SEPARATION + " and " + MAX_SEPARATION);

            if (!a_floodFillSeed)
                throw new ArgumentException("The seed can´t be null");

            m_size = a_size;
            m_eparation = a_separation;
            m_tileLayer = a_tileLayer;
            m_floodArea = a_floodArea;
            m_floodFillSeed = a_floodFillSeed;
            InitVectors();
        }
        //------------------------------------------------------------------------------------------

        public static void KillChilds(GameObject a_floodFillSeed)
        {
            Transform transformFill = a_floodFillSeed.transform;
            while (a_floodFillSeed.transform.childCount > 0)
                Object.DestroyImmediate(transformFill.GetChild(0).gameObject);
        }
        //------------------------------------------------------------------------------------------
    }
}