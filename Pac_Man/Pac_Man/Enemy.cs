using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class Enemy : Player
    {
        private Random random = new Random();

        private long currentTick;
        private long lastTick = 0;

        private int distanceX;
        private int distanceY;
        public Enemy()
        {
            InitPos();
        }
        public override void InitPos()
        {
            state = random.Next((int)State.up, (int)State.right + 1);
            tileColor = ConsoleColor.Red;
            MaxWidth = GameLoop.Instance.GetSetting().GetGameWidth() - 2;
            MaxHeight = GameLoop.Instance.GetSetting().GetGameHeight() - 2;
            //X = MaxHeight / 2;
            //Y = MaxWidth / 2;
            X = random.Next(MaxHeight / 4, MaxHeight / 4 * 3);
            Y = random.Next(MaxWidth / 4, MaxWidth / 4 * 3);
        }
        public override void Move()
        {
            distanceX = X - GameLoop.Instance.GetPlayer().tileX;
            distanceY = Y - GameLoop.Instance.GetPlayer().tileY;
            if (MathF.Abs(distanceX) + MathF.Abs(distanceY) < 15)
            {
                if (distanceX > 0) // 플레이어가 더 위에 있다면
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
                    if (GameLoop.Instance.GetRender.GetTile(X - 1, Y).isWall)
                    {
                        return; // 벽 못 지나감
                    }
                    X -= 1; // 위로 이동
                    if (X <= 1) X = 1;
                }
                else if (distanceX < 0)
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
                    if (GameLoop.Instance.GetRender.GetTile(X + 1, Y).isWall)
                    {
                        return;
                    }
                    X += 1;
                    if (X >= MaxHeight) X = MaxHeight;
                }
                else if (distanceY > 0)
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
                    if (GameLoop.Instance.GetRender.GetTile(X, Y - 1).isWall)
                    {
                        return;
                    }
                    Y -= 1;
                    if (Y <= 1) Y = 1;
                }
                else if (distanceY < 0)
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
                    if (GameLoop.Instance.GetRender.GetTile(X, Y + 1).isWall)
                    {
                        return;
                    }
                    Y += 1;
                    if (Y >= MaxWidth) Y = MaxWidth;
                }
                else return;
            }
            else
            {
                currentTick = Environment.TickCount & Int32.MaxValue;
                if (currentTick - lastTick > 1000)
                {
                    state = random.Next((int)State.up, (int)State.right + 1);
                    lastTick = currentTick;
                }
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
                            if (GameLoop.Instance.GetRender.GetTile(X - 1, Y).isWall)
                            {
                                state = random.Next((int)State.up, (int)State.right + 1);
                                break; // 벽 못 지나감
                            }
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
                            if (GameLoop.Instance.GetRender.GetTile(X + 1, Y).isWall)
                            {
                                state = random.Next((int)State.up, (int)State.right + 1);
                                break;
                            }
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
                            if (GameLoop.Instance.GetRender.GetTile(X, Y - 1).isWall)
                            {
                                state = random.Next((int)State.up, (int)State.right + 1);
                                break;
                            }
                            Y -= 1;
                            if (Y <= 1) Y = 1;
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
                            if (GameLoop.Instance.GetRender.GetTile(X, Y + 1).isWall)
                            {
                                state = random.Next((int)State.up, (int)State.right + 1);
                                break;
                            }
                            Y += 1;
                            if (Y >= MaxWidth) Y = MaxWidth;
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
        }
        public void FindPlayer()
        {

        }
    }
}
