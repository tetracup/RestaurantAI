using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfind
{
    class Node
    {
        public Node _parent = null;
        public Main.Tile _tile;
        public int f;
        public int g;
        public int h;
        public Node(Main.Tile _iniTile)
        {
            _tile = _iniTile;
        }
        public void CalculateFGH(Vector2 curPos, Vector2 startPos, Vector2 endPos)
        {
            if (_parent == null)
                g = 0;
            else
                g = _parent.g + 1;

            h = (int)Mathf.Abs(curPos.x - endPos.x) + (int)Mathf.Abs(curPos.y - endPos.y);
            f = g + h;
        }
    }
            

    public List<Main.Tile> FindPath(Main.Tile startTile, Main.Tile endTile)
    {
        List<Node> _openList = new List<Node>();
        List<Node> _closedList = new List<Node>();
        List<Main.Tile> _pathList = new List<Main.Tile>();

        Node startNode = null;
        //f_updateTimer = f_updateTimerMax;
        _openList.Add(new Node(Main.gridArray[startTile.pos.x, startTile.pos.y]));
        startNode = _openList[0];
        _openList[0].CalculateFGH(startTile.pos, startTile.pos, endTile.pos);

        while (_openList.Count != 0)
        {

            Node curNode = _openList[0];
            for (int i = 1; i < _openList.Count; i++)
            {
                if (_openList[i].f < curNode.f)
                    curNode = _openList[i];
            }

            _openList.Remove(curNode);
            _closedList.Add(curNode);

            if (curNode._tile.pos == endTile.pos)
            {
                //Debug.Log("path found");
                while (curNode._parent != null)
                {
                    _pathList.Add(curNode._tile);
                    curNode = curNode._parent;
                }
                _pathList.Reverse();
                break;
            }

            //Neighbours List 
            List<Node> _adjacentTilesList = new List<Node>();

            //Easier access variable 
            Vector2 _lowFTilePos = curNode._tile.pos;
            if (_lowFTilePos.x > 0)
                _adjacentTilesList.Add(new Node(Main.gridArray[(int)_lowFTilePos.x - 1, (int)_lowFTilePos.y]));

            if (_lowFTilePos.y < 13)
                _adjacentTilesList.Add(new Node(Main.gridArray[(int)_lowFTilePos.x, (int)_lowFTilePos.y + 1]));

            if (_lowFTilePos.x < 18)
                _adjacentTilesList.Add(new Node(Main.gridArray[(int)_lowFTilePos.x + 1, (int)_lowFTilePos.y]));

            if (_lowFTilePos.y > 0)
                _adjacentTilesList.Add(new Node(Main.gridArray[(int)_lowFTilePos.x, (int)_lowFTilePos.y - 1]));

            foreach (Node childNode in _adjacentTilesList)
            {

                if (ListContains(_closedList, childNode) || ListContains(_openList, childNode) || (childNode._tile.pos != endTile.pos && Avoid(childNode._tile.type, endTile)))
                    continue;

                Vector2 _childPos = childNode._tile.pos;

                if (curNode.g + 1 < curNode.g || !_openList.Contains(childNode))
                {
                    childNode._parent = curNode;
                    childNode.g = curNode.g + 1;
                    childNode.h = (int)Mathf.Abs(_childPos.x - endTile.pos.x) + (int)Mathf.Abs(_childPos.y - endTile.pos.y);
                    childNode.f = childNode.g + childNode.h;
                    _openList.Add(childNode);
                }

                if (!_openList.Contains(childNode))
                    _openList.Add(childNode);

            }
        }
        return _pathList;
    }

    bool ListContains(List<Node> _nodeList, Node _comparison)
    {
        foreach (Node x in _nodeList)
        {
            if (x._tile.pos == _comparison._tile.pos)
                return true;
        }
        return false;
    }

    bool Avoid(Main.Tile.Type type, Main.Tile endTile)
    {
        switch (type)
        {
            case Main.Tile.Type.PizzaCounter:
            case Main.Tile.Type.PastaCounter:
            case Main.Tile.Type.BurgerCounter:
            case Main.Tile.Type.Counter:
            case Main.Tile.Type.Chair:
            case Main.Tile.Type.Wall:
            case Main.Tile.Type.Table:
                return true;
            case Main.Tile.Type.Entrance:
                if (endTile.pos == Main.gridArray[0, 11].pos)
                    return true;
                else return false;
            case Main.Tile.Type.Exit:
                if (endTile.pos == Main.gridArray[0, 11].pos)
                    return false;
                else return true;




            default:
                return false;
        }
    }

}
