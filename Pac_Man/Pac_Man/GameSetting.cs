using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class GameSetting
    {
        // 윈도우 크기
        private int windowWidth;
        private int windowHeight;
        // 버퍼 크기
        private int bufferWidth;
        private int bufferHeight;
        // 게임 크기
        private int gameWidth;
        public int GetGameWidth() { return gameWidth; }
        private int gameHeight;
        public int GetGameHeight() { return gameHeight; }
        private int ScoreArea = 3;

        public GameSetting()
        {
            Init(); // 값 초기화
        }

        public void Init() // 초기화
        {
            windowWidth = 80;
            windowHeight = 20 + ScoreArea;
            bufferWidth = windowWidth;
            bufferHeight = windowHeight;
            gameWidth = bufferWidth / 2;
            gameHeight = bufferHeight - ScoreArea;
        }
        public void InitWindow() // 윈도우 창 초기화
        {
            Console.Title = "PAC - MAN";
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(bufferWidth, bufferHeight);
            Console.CursorVisible = false;
        }
    }
}
