using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class GameLoop : Manager<GameLoop>
    {
        private const int gameSpeed = 8; // 게임 속도
        private long currentTick;
        private long lastTick = 0;
        private float waitTick = 1000 / gameSpeed;

        private GameSetting setting; // 게임 세팅
        public GameSetting GetSetting() { return setting; }


        private InputKey input; // 키 입력
        public InputKey GetInput() { return input; }

        private Render render; // 렌더링
        public Render GetRender { get { return render; } }
        private Player player; // 플레이어
        public Player GetPlayer() { return player; }

        private Enemy enemy; // 적
        public Enemy GetEnemy() { return enemy; }

        private Enemy enemy2; // 적2
        public Enemy GetEnemy2() { return enemy2; }
        public bool collision = false; // 충돌 판정 변수
        public void Awake() // 1 행동 전 초기화
        {
            setting = new GameSetting();
            input = new InputKey();
            render = new Render();
            player = new Player();
            enemy = new Enemy();
            enemy2 = new Enemy();
        }
        public void Start() // 2 처음 한번만 실행할 행동 시작
        {
            setting.InitWindow(); // 윈도우 초기화
        }
        public void Update() // 3
        {
            while(true)
            {
                currentTick = Environment.TickCount & Int32.MaxValue;
                if(currentTick - lastTick < waitTick)
                {
                    continue;
                }
                else // 게임 속도(프레임)을 지나면 실행
                {
                    lastTick = currentTick;
                    input.OnKeyBoard(); // 키
                    render.RenderScreen();
                    player.Move(); // 이동
                    enemy.Move();
                    enemy2.Move();
                    if(player.GetScore() >= 4000) // 점수 4000 이상
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("\t\tGame Win");
                        Console.WriteLine();
                        return;
                    }
                    if(collision) // 적과의 충돌
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("\t\tGame Over");
                        Console.WriteLine();
                        return;
                    }
                }
            }
        }
    }
}
