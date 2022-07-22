using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class Tile
    {
        protected int X; // 타일의 X
        public int tileX { get { return X; } set { X = value; } }

        protected int Y; // 타일의 Y
        public int tileY { get { return Y; } set { Y = value; } }

        protected char shape; // 타일의 모양
        public char tileShape { get { return shape; }}
        public void SetTileShape(char myShape) { shape = myShape; }

        protected ConsoleColor color; // 타일의 색상
        public ConsoleColor tileColor { get { return color; } set { color = value; } }

        protected bool IsItem = false;
        public bool isItem { get { return IsItem; }}
        public void SetIsItem(bool flag) { IsItem = flag; }

        protected bool IsWall = false;
        public bool isWall { get { return IsWall; }}
        public void SetIsWall(bool flag) { IsWall = flag; }

        public virtual void SetPosition(int x, int y)
        {

        }
    }
}
