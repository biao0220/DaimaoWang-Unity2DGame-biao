using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TextMap : MonoBehaviour
{
    [Header("背景瓦片")] public TileBase backBase;
    [Header("前景瓦片")] public TileBase foreBase;
    [Header("梯子瓦片")] public TileBase ladderBase;
    [Header("前景")] public Tilemap foremap;
    [Header("背景")] public Tilemap backmap;
    [Header("梯子背景")] public Tilemap laddermap;
    [Header("房子最大高度")] public int roomHeightMax;  //20
    [Header("房子最小高度")] public int roomHeightMin; //15
    [Header("房子最大宽度")] public int roomWidthMax;   //20
    [Header("房子最小宽度")] public int roomWidthMin;   //30
    [Header("房子间的间隔")] public int roomDistance;
    [Header("房间数")] public int roomCount;   //30
    [Header("地图最大宽度")] public int mapHeightMax;
    [Header("地图最大长度")] public int mapWidthMax;
    [Header("对象门")] public GameObject door;
    [Header("对象蝙蝠")] public GameObject bat;
    private int qq = 0;
    private Vector3Int[] tondao = new Vector3Int[40];
    //private Vector3Int []qq1;

    Dictionary<Vector3Int, int> road = new Dictionary<Vector3Int, int>();
    Dictionary<Vector3Int, int> roadmap = new Dictionary<Vector3Int, int>();
    List<Vector3Int> wall = new List<Vector3Int>();//墙的坐标
    //List<Vector3Int> pointnext = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {

        backmap.ClearAllTiles();
        foremap.ClearAllTiles();
        DrawMap();
        BuildWall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DrawMap()
    {
        backmap.ClearAllTiles();
        foremap.ClearAllTiles();
        GetRoomMap(mapWidthMax, mapHeightMax, roomCount);

        foreach (Vector3Int it in roadmap.Keys)
        {
            tondao[qq] = it;
            //Debug.Log(tondao[qq]);
            qq++;
        }

        for (int i = 1; i < roomCount; i++)
        {
            Vector3Int tondaocha;
            tondaocha = tondao[i] - tondao[i - 1];
            //Debug.Log("通道差");
            //Debug.Log(tondaocha);

            if (tondaocha.y > 0)
            {
                for (int e1 = 0; e1 < tondaocha.y; e1++)
                {
                    //backmap.SetTile((tondao[i - 1] + new Vector3Int(0, e1, 0)), backBase);

                    if (!road.ContainsKey(tondao[i - 1] + new Vector3Int(0, e1, 0)))
                    {
                        road.Add((tondao[i - 1] + new Vector3Int(0, e1, 0)), 0);
                        laddermap.SetTile((tondao[i - 1] + new Vector3Int(0, e1, 0)), ladderBase);
                        // road.Add((tondao[i - 1] + new Vector3Int(1, e1, 0)), 0);
                        // road.Add((tondao[i - 1] + new Vector3Int(2, e1, 0)), 0);
                    }

                    if (!road.ContainsKey(tondao[i - 1] + new Vector3Int(1, e1, 0)))
                    {
                        road.Add((tondao[i - 1] + new Vector3Int(1, e1, 0)), 0);
                        // road.Add((tondao[i - 1] + new Vector3Int(1, e1, 0)), 0);
                        // road.Add((tondao[i - 1] + new Vector3Int(2, e1, 0)), 0);
                    }
                }
            }

            if (tondaocha.x > 0)
            {
                for (int e1 = 0; e1 < tondaocha.x; e1++)
                {
                    //backmap.SetTile((tondao[i - 1] + new Vector3Int(0, e1, 0)), backBase);
                    if (!road.ContainsKey(tondao[i - 1] + new Vector3Int(e1, 0, 0)))
                    {
                        road.Add((tondao[i - 1] + new Vector3Int(e1, 0, 0)), 0);
                    }
                    if (!road.ContainsKey(tondao[i - 1] + new Vector3Int(e1, 1, 0)))
                    {
                        road.Add((tondao[i - 1] + new Vector3Int(e1, 1, 0)), 0);
                    }
                }
            }
            if (tondaocha.x < 0)
            {
                for (int e1 = 0; e1 < Mathf.Abs(tondaocha.x) + 1; e1++)
                {
                    //backmap.SetTile((tondao[i - 1] + new Vector3Int(0, e1, 0)), backBase);
                    if (!road.ContainsKey(tondao[i - 1] - new Vector3Int(e1, 0, 0)))
                    {
                        road.Add((tondao[i - 1] - new Vector3Int(e1, 0, 0)), 0);
                    }
                    if (!road.ContainsKey(tondao[i - 1] - new Vector3Int(e1, 1, 0)))
                    {
                        road.Add((tondao[i - 1] - new Vector3Int(e1, 1, 0)), 0);
                    }
                }
            }
            if (tondaocha.y < 0)
            {
                for (int e1 = 0; e1 < Mathf.Abs(tondaocha.y) + 1; e1++)
                {
                    //backmap.SetTile((tondao[i - 1] + new Vector3Int(0, e1, 0)), backBase);
                    if (!road.ContainsKey(tondao[i - 1] - new Vector3Int(0, e1, 0)))
                    {
                        road.Add((tondao[i - 1] - new Vector3Int(0, e1, 0)), 0);
                        laddermap.SetTile((tondao[i - 1] - new Vector3Int(0, e1, 0)), ladderBase);
                    }
                    if (!road.ContainsKey(tondao[i - 1] - new Vector3Int(1, e1, 0)))
                    {
                        road.Add((tondao[i - 1] - new Vector3Int(1, e1, 0)), 0);
                    }
                }
            }
        }
        //Array.Clear(tondao, 0, tondao.Length);
        // GetRoomMap(1000, 1000, 7);

        foreach (Vector3Int it in roadmap.Keys)
        {

            //tondao[qq] = it;
            //Debug.Log(tondao[qq]);
            //qq++;
            //road.Clear();
            int width = GetOddNumber(roomWidthMin, roomWidthMax);
            int height = GetOddNumber(roomHeightMin, roomHeightMax);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    if (!(road.ContainsKey(new Vector3Int(i, j, 0) + it)))
                    {
                        road.Add(new Vector3Int(i, j, 0) + it, 0);
                    }
                }
            }

            //RandomRoom(50, 50, 20, 20);
            foreach (Vector3Int item in road.Keys)
            {
                backmap.SetTile(item, backBase);
            }


        }


    }
    void BuildWall()
    {
        //wall.Clear();
        foreach (Vector3Int item in road.Keys)
        {
            Vector3Int right = item + Vector3Int.right;
            Vector3Int left = item + Vector3Int.left;
            Vector3Int up = item + Vector3Int.up;
            Vector3Int down = item + Vector3Int.down;
            Vector3Int leftdown = item + Vector3Int.down + Vector3Int.left;
            Vector3Int rightdown = item + Vector3Int.down + Vector3Int.right;
            Vector3Int rightup = item + Vector3Int.right + Vector3Int.up;
            Vector3Int leftup = item + Vector3Int.left + Vector3Int.up;

            // 下边有三个方向，一个没有
            if (!(wall.Contains(down)) && (road.ContainsKey(up))
            && (road.ContainsKey(right))
            && (road.ContainsKey(left))
            && !(road.ContainsKey(down))

            )
            {
                wall.Add(down);
            }

            // 上边有三个方向，一个没有
            if (!(wall.Contains(up))
            && (road.ContainsKey(left))
            && (road.ContainsKey(right))
            && (road.ContainsKey(down))
            && !(road.ContainsKey(up))

            )
            {
                wall.Add(up);

            }

            // 左边有三个方向，一个没有
            if (!(wall.Contains(left))
            && (road.ContainsKey(up))
            && (road.ContainsKey(down))
            && (road.ContainsKey(right))
            && !(road.ContainsKey(left))

            )
            {
                wall.Add(left);

            }

            // 右边有三个方向，一个没有
            if (!(wall.Contains(right))
            && (road.ContainsKey(up))
            && (road.ContainsKey(down))
            && (road.ContainsKey(left))
            && !(road.ContainsKey(right))

            )
            {
                wall.Add(right);

            }

            //左上角有二个方向，二个没有，和一个角没有
            if (!(wall.Contains(up))
            && !(wall.Contains(left))
            && !(wall.Contains(leftup))
            && (road.ContainsKey(right))
            && (road.ContainsKey(down))
            && !(road.ContainsKey(leftup))
            )
            {

                wall.Add(up);
                wall.Add(left);
                wall.Add(leftup);
                //Debug.Log(item);


            }

            //左下角有二个方向，二个没有，和一个角没有
            if (!(wall.Contains(left))
            && !(wall.Contains(down))
            && (road.ContainsKey(up))
            && (road.ContainsKey(right))
            && !(road.ContainsKey(leftdown))
            && !(wall.Contains(leftdown))
            )
            {
                wall.Add(down);
                wall.Add(left);
                wall.Add(leftdown);
            }

            //右下角有二个方向，二个没有，和一个角没有
            if (!(wall.Contains(right))
            && !(wall.Contains(down))
            && !(wall.Contains(rightdown))
            && (road.ContainsKey(up))
            && (road.ContainsKey(left))
            && !(road.ContainsKey(rightdown))
            )
            {
                wall.Add(down);
                wall.Add(right);
                wall.Add(rightdown);
            }




            //右上角有二个方向，二个没有，和一个角没有
            if (!(wall.Contains(up))
            && !(wall.Contains(right))
            && !(wall.Contains(rightup))
            && (road.ContainsKey(left))
            && (road.ContainsKey(down))
            && !(road.ContainsKey(rightup))
            )
            {
                wall.Add(up);
                wall.Add(right);
                wall.Add(rightup);
            }


        }

        foreach (Vector3Int item in wall)
        {
            foremap.SetTile(item, foreBase);

            foreach (Vector3Int it in road.Keys)
            {
                if (item == it)
                {
                    foremap.SetTile(item, null);
                    //wall.Remove(item);
                }
            }

            //foremap.SetTile(item, foreBase);
        }

    }


    void GetRoomMap(int mapW, int mapH, int roomCount)
    {
        roadmap.Clear();
        //第一个房间的坐标点
        var nowPoint = Vector2Int.zero;

        roadmap.Add(new Vector3Int(nowPoint.x, nowPoint.y, 0), 1);
        //当前生成的房间数
        int mCount = 1;
        Instantiate(bat, new Vector3Int(nowPoint.x + 5, nowPoint.y + 2, 0), Quaternion.identity);
        Instantiate(bat, new Vector3Int(nowPoint.x + 5, nowPoint.y + 2, 0), Quaternion.identity);

        //第一个格子总有房间，作为出生房间
        //map[nowPoint.x, nowPoint.y] = 1;

        while (mCount < roomCount)
        {
            nowPoint = GetNextPoint(nowPoint, mapW, mapH);
            if (!(roadmap.ContainsKey(new Vector3Int(nowPoint.x, nowPoint.y, 0))))
            {// map[nowPoint.x, nowPoi nt.y] = 1;

                roadmap.Add(new Vector3Int(nowPoint.x, nowPoint.y, 0), 1);
                mCount++;
            }

            Instantiate(bat, new Vector3Int(nowPoint.x + 5, nowPoint.y + 5, 0), Quaternion.identity);
            //Instantiate(bat, new Vector3Int(nowPoint.x + 5, nowPoint.y + 2, 0), Quaternion.identity);
        }

        //在此添加enenmy 
        if (mCount == 7)
        {
            Instantiate(door, new Vector3Int(nowPoint.x + 5, nowPoint.y + 2, 0), Quaternion.identity);
        }

    }

    private Vector2Int GetNextPoint(Vector2Int nowPoint, int maxW, int maxH)
    {
        while (true)
        {
            var mNowPoint = nowPoint;
            //int upcount=0,downcount=0,leftcount=0,rightcount=0;
            switch (Random.Range(0, 4))
            {
                case 0:
                    mNowPoint.x += roomDistance;//roomDistance * Random.Range(1, 3);
                    break;
                case 1:
                    mNowPoint.y += roomDistance;//roomDistance * Random.Range(1, 3);
                    break;
                case 2:
                    mNowPoint.x -= roomDistance;//roomDistance * Random.Range(1, 3);
                    break;
                default:
                    mNowPoint.y -= roomDistance;//roomDistance * Random.Range(1, 3);
                    break;
            }

            //if (mNowPoint.x >= 0 && mNowPoint.y >= 0 && mNowPoint.x < maxW && mNowPoint.y < maxH)
            if (mNowPoint.x >= 0 && mNowPoint.y >= 0 && mNowPoint.x < maxW && mNowPoint.y < maxH &&
            //roadmap.Add(new Vector3Int(nowPoint.x, nowPoint.y, 0), 1);
            !(roadmap.ContainsKey(new Vector3Int(mNowPoint.x, mNowPoint.y, 0)))
            )
            {
                return mNowPoint;
                //Debug.Log(mNowPoint);

            }
        }
    }


    //首先是使用，我在 Update 函数中检测按下 R 键重新生成地图，生成很简单只要调用 DrawMap 函数就可以

    //在 DrawMap 中 会调用 DrawRoad 和 DrawFloor 俩个函数来分别画出道路和地板、墙壁。
    //取一定范围内的一个奇数
    private int GetOddNumber(int min, int max)
    {
        while (true)
        {
            var temp = Random.Range(min, max);
            if ((temp & 1) != 1) continue;
            return temp;
        }
    }
    //生成一个房间，用二维 int 数组表示
    //private int[,] RandomRoom(int maxW, int maxH, int minW, int minH, out int width, out int height)
    void RandomRoom(int maxW, int maxH, int minW, int minH)
    {

        int width = GetOddNumber(minW, maxW);
        int height = GetOddNumber(minH, maxH);

        road.Clear();
        //var room = new int[width, height];
        //方便以后扩展使用了二维数组，这里为了演示方便对房间内生成其他物体
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                road.Add(new Vector3Int(i, j, 0), 0);
            }
        }

    }

    //生成墙壁



}

