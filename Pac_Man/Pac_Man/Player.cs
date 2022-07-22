using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class Player : Tile
    {   
        public enum State // 플레이어 상태
        {
            up,
            down,
            left,
            right,
            start,
            dead,
        }
        protected int state; // 현재 이동 상태
        public int MoveState { get { return state; } set { state = value; } }
        protected int MaxWidth;
        protected int MaxHeight;

        protected bool flag = true; // 애니메이션 플래그
        private int score = 0;
        public int GetScore() { return score; }
        public Player()
        {
            InitPos();
        }

        public virtual void InitPos() // 플레이어 초기화
        {
            state = (int)State.start;
            tileColor = ConsoleColor.Yellow;
            this.SetTileShape('⊂');
            MaxWidth = GameLoop.Instance.GetSetting().GetGameWidth() - 2;
            MaxHeight = GameLoop.Instance.GetSetting().GetGameHeight() - 2;
            X = GameLoop.Instance.GetRender.GetCreator().GetStartX();
            Y = GameLoop.Instance.GetRender.GetCreator().GetStartY();
        }
        public virtual void Move() // 이동 시작
        {
            CheckItem();
            CheckEnemy();
            switch (state)
            {
                case (int)State.up:
                    {
                        if (flag) // 애니메이션
                        {
                            SetTileShape('∪');
                        }
                        else
                        {
                            SetTileShape('∨');
                        }
                        flag = !flag;
                        if (GameLoop.Instance.GetRender.GetTile(X - 1, Y).isWall) break; // 벽 못 지나감
                        X -= 1; // 이동
                        if (X <= 1) X = 1;
                        break;
                    }
                case (int)State.down:
                    {
                        if (flag)
                        {
                            SetTileShape('∩');
                        }
                        else
                        {
                            SetTileShape('∧');
                        }
                        flag = !flag;
                        if (GameLoop.Instance.GetRender.GetTile(X + 1, Y).isWall) break;
                        X += 1;
                        if (X >= MaxHeight) X = MaxHeight;
                        break;
                    }
                case (int)State.left:
                    {
                        if (flag)
                        {
                            SetTileShape('⊃');
                        }
                        else
                        {
                            SetTileShape('＞');
                        }
                        flag = !flag;
                        if (GameLoop.Instance.GetRender.GetTile(X, Y - 1).isWall) break;
                        Y -= 1;
                        if(Y <= 1) Y = 1;
                        break;
                    }
                case (int)State.right:
                    {
                        if (flag)
                        {
                            SetTileShape('⊂');
                        }
                        else
                        {
                            SetTileShape('＜');
                        }
                        flag = !flag;
                        if (GameLoop.Instance.GetRender.GetTile(X, Y + 1).isWall) break;
                        Y += 1;
                        if(Y >= MaxWidth) Y = MaxWidth;
                        break;
                    }
                case (int)State.start:
                    {
                        break;
                    }
                case (int)State.dead:
                    {
                        break;
                    }
            }
        }
        private void CheckItem()
        {
            if (GameLoop.Instance.GetRender.GetTile(X, Y).isItem)
            {
                GameLoop.Instance.GetRender.GetTile(X, Y).SetIsItem(false);
                GameLoop.Instance.GetRender.GetTile(X, Y).SetTileShape('　');
                score += 10;
            }
        }
        private void CheckEnemy()
        {
            if((X == GameLoop.Instance.GetEnemy().tileX && Y == GameLoop.Instance.GetEnemy().tileY) ||
                    (X == GameLoop.Instance.GetEnemy2().tileX && Y == GameLoop.Instance.GetEnemy2().tileY))
            {
                GameLoop.Instance.collision = true;
                Thread.Sleep(1000);
            }
        }
    }
}
