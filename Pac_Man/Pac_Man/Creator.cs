using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class Creator
    {
        private Random rand = new Random();
        private int _width;
        private int _height;
        private int startX; // 플레이어 시작 위치
        public int GetStartX() { return startX; }
        private int startY;
        public int GetStartY() { return startY; }

        private int enemyCount = 1; // 적 숫자
        public int EnemyCount() { return enemyCount; }
        private int enemyX;
        public int GetEnemyX() { return enemyX; }
        private int enemyY;
        public int GetEnemyY() { return enemyY; }

        public void CreateMap(Tile[,] tile, int width, int height)
        {
            _width = width;
            _height = height;
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if(i == 0 || i == height - 1) // 화면 상단, 하단 테두리
                    {
                        tile[i, j].tileColor = ConsoleColor.Blue;
                        tile[i, j].SetTileShape('■');
                        tile[i, j].SetIsWall(true);
                    }
                    else if(j == 0 || j == width - 1) // 화면 좌측, 우측 테두리
                    {
                        tile[i, j].tileColor = ConsoleColor.Blue;
                        tile[i, j].SetTileShape('■');
                        tile[i, j].SetIsWall(true);
                    }
                    else // 빈 공간
                    {
                        tile[i, j].tileColor = ConsoleColor.DarkYellow;
                        tile[i, j].SetTileShape('＊');
                        tile[i, j].SetIsItem(true);
                    }
                }
            }
            CreateWall(tile);
        }

        public void CreateWall(Tile[,] tile)
        {
            for(int i = 1; i < _height - 1; i++)
            {
                for(int j = 1; j < _width - 1; j++)
                {
                    if(IsWallStart())
                    {
                        tile[i, j].SetTileShape('■');
                        tile[i, j].tileColor = ConsoleColor.Blue;
                        tile[i, j].SetIsItem(false);
                        tile[i, j].SetIsWall(true);
                    }
                }
            }
            CreateRoad(tile);
        }
        public void CreateRoad(Tile[,] tile)
        {
            for (int i = 2; i < _height - 2; i++)
            {
                for (int j = 2; j < _width - 2; j++)
                {
                    if (tile[i, j].isItem) // 아이템 자리인데
                    {
                        if (tile[i + 1, j].isWall && tile[i - 1, j].isWall || tile[i, j - 1].isWall && tile[i, j + 1].isWall) // 모두 막히면 십자로 뚫음
                        {
                            //tile[i, j].SetIsWall(false);
                            //tile[i, j].SetIsItem(true);
                            //tile[i, j].SetTileShape('＊');
                            //tile[i, j].tileColor = ConsoleColor.DarkYellow;

                            tile[i + 1, j].SetIsWall(false);
                            tile[i + 1, j].SetIsItem(true);
                            tile[i + 1, j].SetTileShape('＊');
                            tile[i + 1, j].tileColor = ConsoleColor.DarkYellow;

                            tile[i - 1, j].SetIsWall(false);
                            tile[i - 1, j].SetIsItem(true);
                            tile[i - 1, j].SetTileShape('＊');
                            tile[i - 1, j].tileColor = ConsoleColor.DarkYellow;

                            tile[i, j + 1].SetIsWall(false);
                            tile[i, j + 1].SetIsItem(true);
                            tile[i, j + 1].SetTileShape('＊');
                            tile[i, j + 1].tileColor = ConsoleColor.DarkYellow;

                            tile[i, j - 1].SetIsWall(false);
                            tile[i, j - 1].SetIsItem(true);
                            tile[i, j - 1].SetTileShape('＊');
                            tile[i, j - 1].tileColor = ConsoleColor.DarkYellow;
                        }
                    }
                }
            }
            RemoveIsolatedItem(tile);
        }
        public void RemoveIsolatedItem(Tile[,] tile)
        {
            for(int i = 0; i < _height - 1; i++)
            {
                for (int j = 0; j < _width - 1; j++)
                {
                    if (tile[i, j].isItem) // 아이템인데
                    {
                        if (tile[i + 1, j].isWall && tile[i - 1, j].isWall && tile[i, j - 1].isWall && tile[i, j + 1].isWall) // 사방이 모두 막히면
                        {
                            tile[i, j].SetIsWall(true);
                            tile[i, j].SetIsItem(false);
                            tile[i, j].SetTileShape('■');
                            tile[i, j].tileColor = ConsoleColor.Blue;
                        }
                    }
                }
            }
            SetPlayerPos(tile);
        }
        public void SetPlayerPos(Tile[,] tile)
        {
            for(int i = 2; i < _height - 2; i++)
            {
                for(int j = 2; j < _width - 2; j++)
                {
                    if (tile[i, j].isWall == false)
                    {
                        if (!(tile[i + 1, j].isWall && tile[i - 1, j].isWall || tile[i, j - 1].isWall && tile[i, j + 1].isWall))
                        {
                            startX = i;
                            startY = j;
                            return;
                        }
                    }
                }
            }
            SetEnemyPos(tile);
        }
        public void SetEnemyPos(Tile[,] tile)
        {
            for (int i = _height - 2; i >= 2; i--)
            {
                for (int j = _width - 2; j >= 2; j--)
                {
                    if (tile[i, j].isWall == false)
                    {
                        if (!(tile[i + 1, j].isWall && tile[i - 1, j].isWall || tile[i, j - 1].isWall && tile[i, j + 1].isWall))
                        {
                            enemyX = i;
                            enemyY = j;
                        }
                    }
                }
            }
        }
        public bool IsWallStart() // 벽 만들까?
        {
            if (rand.Next(0, 4) < 3) return true;
            else return false;
        }
        public bool IsRangeOut(int i, int j)
        {
            if (i < 1 || j < 1 || i > _height - 1 || j > _width - 1)
            {
                return true;
            }
            return false;
        }
    }
}
