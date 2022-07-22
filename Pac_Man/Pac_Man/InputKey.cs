using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pac_Man
{
    public class InputKey
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int myKey);
        private short myKey = 0;
        public void OnKeyBoard()
        {
            myKey = 0;
            if (Console.KeyAvailable) // 키입력이 존재한다면
            {
                myKey = GetAsyncKeyState((int)ConsoleKey.RightArrow); // 키에 따라
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameLoop.Instance.GetPlayer().MoveState = (int)Player.State.right; // 캐릭터의 상태 변경
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.LeftArrow);
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameLoop.Instance.GetPlayer().MoveState = (int)Player.State.left;
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.UpArrow);
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameLoop.Instance.GetPlayer().MoveState = (int)Player.State.up;
                }
                myKey = GetAsyncKeyState((int)ConsoleKey.DownArrow);
                if ((myKey & 0x8000) == 0x8000)
                {
                    GameLoop.Instance.GetPlayer().MoveState = (int)Player.State.down;
                }
            }
        }
    }
}
