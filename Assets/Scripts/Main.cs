using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main
{
    //Tile struct for Grid
    public struct Tile
    {
        public enum Type
        {
            Empty,
            Wall,
            Table,
            Chair,
            Counter,
            BurgerCounter,
            PastaCounter,
            PizzaCounter,
            Entrance,
            Exit
        }

        public Type type;
        public Vector2Int pos;
        public bool isOccupied;
        GameObject dirtSprite;
        SpriteRenderer dirtSpriteRend;



        public void SetType(Type newType)
        {
            type = newType;
        }

        public void SetPos(Vector2Int newPos)
        {
            pos = newPos;
        }

        public void Occupy(bool beenOccupied)
        {
            isOccupied = beenOccupied;
        }

        public void AssignDirtGameObj(GameObject dirtOverlay)
        {
            dirtSprite = dirtOverlay;
            dirtSpriteRend = dirtSprite.AddComponent<SpriteRenderer>();
            dirtSpriteRend.sprite = Resources.Load<Sprite>("dirty");
            dirtSprite.transform.position = new Vector2(pos.x + 0.5f, pos.y -0.5f);
            Color color = dirtSpriteRend.color;
            color.a = 0;
            dirtSpriteRend.color = color;
        }

        public void ChangeCleanlinessOfCounter(float cleanRate)
        {
            Color color = dirtSpriteRend.color;
            color.a = color.a += cleanRate * Time.deltaTime;
            color.a = Mathf.Clamp(color.a, 0, 1);

            if (color.a < 0.1f && cleanRate < 0)
                color.a = 0;
            dirtSpriteRend.color = color;
        }

        public bool isDirty()
        {
            return dirtSpriteRend.color.a > 0.50f;
        }

        public bool canBeCleaned()
        {
            return dirtSpriteRend.color.a > 0.1f;
        }

        public float GetDirtiness()
        {
            return dirtSpriteRend.color.a;
        }

        public bool IsMoreDirtyThan(Main.Tile comparison)
        {
            return comparison.GetDirtiness() < GetDirtiness();
        }
    };


    const int gridWidth = 18;
    const int gridHeight = 14;
    //Initialises a grid of 18x14 tiles
    static public Tile[,] gridArray = new Tile[gridWidth, gridHeight];
    static public List<Tile> chairList = new List<Tile>();

    static public AStarPathfind pathfind;
    static List<Tile> curPath = new List<Tile>();

    static public Camera MainCam;

    static public Dictionary<string, object> blackboard = new Dictionary<string, object>();

    static readonly Color ExitColor = new Color(0.678f, 0.133f, 0.459f);
    static readonly Color WallColor = new Color(0.051f, 0.051f, 0.102f);
    static readonly Color EntranceColor = new Color(0.361f, 0.176f, 0.478f);
    static readonly Color PrepColor = new Color(0.584f, 0.278f, 0.518f);
    static readonly Color CounterColor = new Color(0.259f, 0.482f, 0.259f);
    static readonly Color ChairColor = new Color(0.875f, 0.796f, 0.431f);
    static readonly Color TableColor = new Color(0.753f, 0.267f, 0.514f);

    static public List<Main.Tile> CounterList = new List<Tile>();

    static int customerCounter = 1;
    static public List<GameObject> CustomerList = new List<GameObject>();

    static public Tile[,] FoodCountersArray = new Tile[3,2];

    static public List<Transform> ReceiptsOnCounter = new List<Transform>();
    static public bool takeOutBusy = false;

    [RuntimeInitializeOnLoadMethod]
    static void Start()
    {
        
        InitialiseCamera();
        InitialiseBackground();
        InitialiseGridPos();

        SpawnCustomer();
        SpawnWaiter();
        SpawnChef();

        GameObject _obj = new GameObject("SpawnCustomersObj");
        _obj.AddComponent<SpawnCustomersRandomly>();

        //Test
        //AStarPathfind pathfind = new AStarPathfind();
        //pathfind.FindPath(gridArray[0, 12], gridArray[16, 7]);
    }

    static void InitialiseGridPos()
    {
        Texture2D backgroundTexture = Resources.Load<Texture2D>("background");
        float HeightRatio = backgroundTexture.height / gridHeight;
        float WidthRatio = backgroundTexture.width / gridWidth;

        int prepCount = 0;

        for (int i = 0; i < gridWidth - 1; i++)
        {
            for (int o = 0; o < gridHeight - 1; o++)
            {
                gridArray[i, o].SetPos(new Vector2Int(i, o));
                Color curColor = backgroundTexture.GetPixel(i * 100 + 10, o * 100 + 10);
                curColor = new Color(Round(curColor.r), Round(curColor.g), Round(curColor.b));
                if (curColor == ExitColor)
                    gridArray[i, o].SetType(Tile.Type.Exit);
                else if (curColor == WallColor)
                    gridArray[i, o].SetType(Tile.Type.Wall);
                else if (curColor == EntranceColor)
                    gridArray[i, o].SetType(Tile.Type.Entrance);
                else if (curColor == PrepColor)
                {
                    switch (prepCount)
                    {
                        case 0:
                        case 1:
                            gridArray[i, o].SetType(Tile.Type.BurgerCounter);
                            FoodCountersArray[0, prepCount%2] = gridArray[i, o];
                            prepCount++;
                            gridArray[i, o].AssignDirtGameObj(new GameObject("DirtOverlay"));
                            break;
                        case 2:
                        case 3:
                            gridArray[i, o].SetType(Tile.Type.PizzaCounter);
                            FoodCountersArray[1, prepCount % 2] = gridArray[i, o];
                            prepCount++;
                            gridArray[i, o].AssignDirtGameObj(new GameObject("DirtOverlay"));
                            break;
                        case 4:
                        case 5:
                            gridArray[i, o].SetType(Tile.Type.PastaCounter);
                            FoodCountersArray[2, prepCount % 2] = gridArray[i, o];
                            prepCount++;
                            gridArray[i, o].AssignDirtGameObj(new GameObject("DirtOverlay"));
                            break;

                    }
                }
                else if (curColor == CounterColor)
                {
                    gridArray[i, o].SetType(Tile.Type.Counter);
                    CounterList.Add(gridArray[i, o]);
                }
                    
                else if (curColor == ChairColor)
                {
                    gridArray[i, o].SetType(Tile.Type.Chair);
                    chairList.Add(gridArray[i, o]);
                }
                else if (curColor == TableColor)
                    gridArray[i, o].SetType(Tile.Type.Table);
                else
                    gridArray[i, o].SetType(Tile.Type.Empty);
            }
        }

    }

    static void InitialiseCamera()
    {
        GameObject obj_camera = new GameObject("Main Camera");
        obj_camera.transform.position = new Vector3(gridWidth / 2, gridHeight / 2 - 1, -10);
        MainCam = obj_camera.AddComponent<Camera>();
        MainCam.orthographic = true;
        MainCam.orthographicSize = gridHeight / 2;
        MainCam.backgroundColor = new Color32(255, 255, 255, 255);
    }

    static void InitialiseBackground()
    {
        GameObject obj_background = new GameObject("Background");
        obj_background.transform.position = new Vector2(0, gridHeight - 1);
        SpriteRenderer _spriteRend = obj_background.AddComponent<SpriteRenderer>();
        _spriteRend.sprite = Resources.Load<Sprite>("background");
        _spriteRend.sortingOrder = -1;

    }
    static float Round(float value)
    {

        value *= 1000;
        value = Mathf.Round(value);
        value /= 1000;
        return value;
    }

    static public void SpawnCustomer()
    {
        GameObject obj_customer = new GameObject("Customer" + customerCounter++);
        obj_customer.transform.position = new Vector3(0.5f, 11.5f);
        obj_customer.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Customer");
        obj_customer.AddComponent<CustomerAI>();
        CustomerList.Add(obj_customer);
    }

    static void SpawnWaiter()
    {
        GameObject obj_waiter = new GameObject("Waiter");
        obj_waiter.transform.position = new Vector3(13.5f, 0.5f);
        obj_waiter.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Waiter");
        obj_waiter.AddComponent<WaiterAI>();
    }

    static void SpawnChef()
    {
        GameObject obj_chef = new GameObject("Chef");
        obj_chef.transform.position = new Vector3(4.5f, 0.5f);
        obj_chef.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Chef");
        obj_chef.AddComponent<ChefAI>();
    }

    
    static public object GetData(string _bbstring)
    {
        object outObj;
        Main.blackboard.TryGetValue(_bbstring, out outObj);
        return outObj;
    }

    void AssignDirtOverlayToTile(Main.Tile tile)
    {
        GameObject obj_dirt = new GameObject("DirtyOverlay");
        obj_dirt.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("dirty");
    }
}
