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

        private bool isIsolate = false; // 고립 판단 변수
        private int solutionDepth = 1; // 솔루션의 깊이

        private int best = 0;
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
        public override void Move() // AI 이동
        {
            distanceX = X - GameLoop.Instance.GetPlayer().tileX;
            distanceY = Y - GameLoop.Instance.GetPlayer().tileY;
            if (MathF.Abs(distanceX) + MathF.Abs(distanceY) < 15)
            {
                if(isIsolate) // 플레이어를 추격중인데 고립되었다면
                {
                    IsolatedSolution(X, Y); // 고립 해결 솔루션
                    return;
                }
                best = 0;
                if (MathF.Abs(distanceX) == MathF.Abs(distanceY)) //  XY 거리차의 절댓값 이 같으면
                {
                    best = random.Next(0, 4); // 아무 방향으로 간다.
                }
                if (MathF.Abs(distanceX) > MathF.Abs(distanceY)) // X 거리차가 더 크다면
                {
                    if (distanceX > 0) // 그 중에서도 양수라면
                    {
                        best = 0; // 위로 이동
                    }
                    else best = 1; // 음수면 아래로 이동
                }
                else // Y 거리차가 더 크다면
                {
                    if (distanceY > 0) // 양수라면
                    {
                        best = 2; // 왼쪽 이동
                    }
                    else best = 3; // 오른쪽 이동
                }

                switch(best) // 최적 솔루션
                {
                    case 0: { MoveUP(); break; }
                    case 1: { MoveDown(); break; }
                    case 2: { MoveLeft(); break; }
                    case 3: { MoveRight(); break; }
                }
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
        public void IsolatedSolution(int _x, int _y) // 고립 해결 솔루션
        {
            int[] dir = new int[4];
            if(GameLoop.Instance.GetRender.GetCreator().IsRangeOut(_x - 1, _y) == false ||
                GameLoop.Instance.GetRender.GetTile(_x - 1, _y).isWall == false) // 위로 갈 수 있으면
            {
                dir[0] = 1;
            }
            if (GameLoop.Instance.GetRender.GetCreator().IsRangeOut(_x - 1, _y) == false ||
               GameLoop.Instance.GetRender.GetTile(_x + 1, _y).isWall == false) // 아래로 갈 수 있으면
            {
                dir[1] = 1;
            }
            if (GameLoop.Instance.GetRender.GetCreator().IsRangeOut(_x - 1, _y) == false ||
                GameLoop.Instance.GetRender.GetTile(_x , _y - 1).isWall == false) // 왼쪽으로 갈 수 있으면
            {
                dir[2] = 1;
            }
            if (GameLoop.Instance.GetRender.GetCreator().IsRangeOut(_x - 1, _y) == false ||
                GameLoop.Instance.GetRender.GetTile(_x, _y + 1).isWall == false) // 오른쪽으로 갈 수 있으면
            {
                dir[3] = 1;
            }
            int bestWay = 0; int count = 0; // 최적은 
            for(int i = 0; i < 4; i++)
            {
                if (dir[i] == 1) // 최적의 갯수를 세기
                {
                    count++;
                }
            }
            if(count > 1) // 만약 최적이 여러개라면
            {
                bestWay = random.Next(0, 4); // 랜덤 이동
            }
            else // 최적이 1개라면
            {
                for (int i = 0; i < 4; i++)
                {
                    if (dir[i] == 1) // 
                    {
                        bestWay = i; // 최적 경로 저장하고 브레이크
                        break;
                    }
                }
            }
            switch(bestWay) // 최적경로에 따라 이동
            {
                case 0: { MoveUP(); break; }
                case 1: { MoveDown(); break; }
                case 2: { MoveLeft(); break; }
                case 3: { MoveRight(); break; }
            }
            solutionDepth++; // 깊이 1 증가
            if(solutionDepth > 5) // 1부터 시작해서 5까지 5번 호출동안 솔루션 제공
            {
                solutionDepth = 0; // 다시 0으로
                isIsolate = false; // 고립 해제
            }
        }
        public void MoveUP()
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
                isIsolate = true;
                return; // 벽 못 지나감
            }
            X -= 1; // 이동
            if (X <= 1) X = 1;
        }
        public void MoveDown()
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
                isIsolate = true;
                return; // 벽 못 지나감
            }
            X += 1;
            if (X >= MaxHeight) X = MaxHeight;
        }
        public void MoveLeft()
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
                isIsolate = true;
                return; // 벽 못 지나감
            }
            Y -= 1;
            if (Y <= 1) Y = 1;
        }
        public void MoveRight()
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
                isIsolate = true;
                return; // 벽 못 지나감
            }
            Y += 1;
            if (Y >= MaxWidth) Y = MaxWidth;
        }
    }
}
