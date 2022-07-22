using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class Render
    {
        private Creator creator; // 맵 생성 클래스
        public Creator GetCreator() { return creator; }
        private Tile[,] tile; // 기본 타일 공간
        public Tile GetTile(int i, int j) { return tile[i, j]; }
        private int width; // 가로최대
        private int height; // 세로최대

        private int temp;
        public Render()
        {
            Init();
        }
        public void Init()
        {
            width = GameLoop.Instance.GetSetting().GetGameWidth();
            height = GameLoop.Instance.GetSetting().GetGameHeight();
            creator = new Creator();
            tile = new Tile[height, width];
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    tile[i, j] = new Tile();
                }
            }
            creator.CreateMap(tile, width, height);
        }
        public void RenderScreen()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("SCORE :  " + GameLoop.Instance.GetPlayer().GetScore());
            Console.WriteLine();
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if(i == GameLoop.Instance.GetPlayer().tileX && j == GameLoop.Instance.GetPlayer().tileY)
                    {
                        Console.ForegroundColor = GameLoop.Instance.GetPlayer().tileColor;
                        Console.Write(GameLoop.Instance.GetPlayer().tileShape);
                    }
                    else if(i == GameLoop.Instance.GetEnemy().tileX && j == GameLoop.Instance.GetEnemy().tileY)
                    {
                        Console.ForegroundColor = GameLoop.Instance.GetEnemy().tileColor;
                        Console.Write(GameLoop.Instance.GetEnemy().tileShape);
                    }
                    else if (i == GameLoop.Instance.GetEnemy2().tileX && j == GameLoop.Instance.GetEnemy2().tileY)
                    {
                        Console.ForegroundColor = GameLoop.Instance.GetEnemy2().tileColor;
                        Console.Write(GameLoop.Instance.GetEnemy2().tileShape);
                    }
                    else
                    {
                        Console.ForegroundColor = tile[i, j].tileColor;
                        Console.Write(tile[i, j].tileShape);
                    }
                }
            }
        }
    }
}
