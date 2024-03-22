using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Project_Esacape_Darkness_CS
{
    internal class PrintManager
    {
        // 캐릭터 색깔 지정
        public static ConsoleColor _playerColor { get; set; }
        public static void PrintGameSet()
        {
            for (int y = 0; y < MapManager.MAXCOL; y++)
            {
                Thread.Sleep(10);
                for (int x = 0; x < MapManager.MAXROW; x++)
                {
                    Console.SetCursorPosition(x * 2, y);
                    Console.Write("□");
                }
            }
        }
        /// <summary>
        /// 맵 전체를 프린트
        /// </summary>
        /// <param name="gold"></param>
        public static void PrintAllMap()
        {
            for (int y = 0; y < MapManager.MAXCOL; y++)
            {
                for (int x = 0; x < MapManager.MAXROW; x++)
                {
                    PrintObject(x, y);
                }
            }
        }
        /// <summary>
        /// 영역을 선택하여 맵을 프린트
        /// </summary>
        public static void PrintSection(int startX, int finishX, int startY, int finishY)
        {
            for (int y = startY; y <= finishY; y++)
            {
                for (int x = startX; x <= finishX; x++)
                {
                    PrintObject(x, y);
                }
            }
        }
        /// <summary>
        /// 표시할 한 칸의 오브젝트만 표시한다.
        /// </summary>
        /// <param name="i">오브젝트의 y축</param>
        /// <param name="j">오브젝트의 x축</param>
        public static void PrintObject(int x, int y)
        {
            Console.SetCursorPosition(x * 2, y);
            if (MapManager.map[y, x] > 100)
            {
                switch (MapManager.map[y, x] % 100)
                {
                    case (int)eBlock.Statue:
                        Console.ForegroundColor = (ConsoleColor)15;
                        Console.Write("◆");
                        break;
                    case (int)eBlock.Wall:
                        Console.ForegroundColor = (ConsoleColor)8;
                        Console.Write("■");
                        break;
                    case (int)eBlock.LightSwitch:
                        Console.ForegroundColor = (ConsoleColor)1;
                        Console.Write("※");
                        break;
                    case (int)eBlock.Key:
                        Console.ForegroundColor = (ConsoleColor)5;
                        Console.Write("¶");
                        break;
                    case (int)eBlock.Door:
                        Console.ForegroundColor = (ConsoleColor)8;
                        Console.Write("▩");
                        break;
                    case (int)eBlock.Gold:
                        Console.ForegroundColor = (ConsoleColor)14;
                        Console.Write("♠");
                        break;
                    case (int)eBlock.Whirlpool:
                        Console.ForegroundColor = (ConsoleColor)11;
                        Console.Write("§");
                        break;
                    case (int)eBlock.Character:
                        Console.ForegroundColor = _playerColor;
                        Console.Write("⊙");
                        break;
                    case (int)eBlock.Ghost:
                        Console.ForegroundColor = (ConsoleColor)12;
                        Console.Write("▣");
                        break;
                    case (int)eBlock.Side:
                        Console.ForegroundColor = (ConsoleColor)8;
                        Console.Write("□");
                        break;
                    case (int)eBlock.Exit:
                        Console.ForegroundColor = (ConsoleColor)15;
                        Console.Write("▦");
                        break;
                    case (int)eBlock.Stone:
                        Console.ForegroundColor = (ConsoleColor)8;
                        Console.Write("●");
                        break;
                    default:
                        Console.Write("  ");
                        break;
                }
            }
            else
            {
                Console.Write("  ");
            }
            Console.SetCursorPosition(MapManager.MAXROW * 2 + 3, 13);
            Console.ForegroundColor = (ConsoleColor)8;
        }
        /// <summary>
        /// 하단 상태표시줄에 현재 일어난 상황을 표시해준다.
        /// </summary>
        /// <param name="str">표시할 현재 상황 문구</param>
        public static void Alarm(string str)
        {
            Console.Write("                            ", str);
            Console.SetCursorPosition(MapManager.MAXROW * 2 + 4, 13);
            Console.Write("{0}", str);
            Console.SetCursorPosition(MapManager.MAXROW * 2 + 3, 13);
        }
        /// <summary>
        /// 옆의 안내판을 띄워준다.
        /// </summary>
        public static void PrintInform()
        {
            int i = 0;
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□□□□□□□□□□□□□□□□□□□□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□          보물 수   :   0  개       □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□          열쇠 획득 :    X          □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□□□□□□□□□□□□□□□□□□□□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□              <Alarm>               □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□□□□□□□□□□□□□□□□□□□□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□□□□□□□□□□□□□□□□□□□□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□------------------------------------□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□ 방향키 : ↑,←,→,↓               □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□------------------------------------□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□ ◆-석상, §-소용돌이, ▣-고스트    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□ ※-스위치, ¶-열쇠, ♠-보물        □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□ ▩,▦-문                           □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□------------------------------------□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□ #  우측 상단의 전체화면을 클릭   # □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□------------------------------------□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□ #    렉이 걸린다면 스페이스바    # □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□------------------------------------□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□□□□□□□□□□□□□□□□□□□□");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□                                    □");
            Console.SetCursorPosition(MapManager.MAXROW * 2, i++);
            Console.Write("□□□□□□□□□□□□□□□□□□□□");
            Console.SetCursorPosition(MapManager.MAXROW * 2 + 6, 13);
        }
    }
}
