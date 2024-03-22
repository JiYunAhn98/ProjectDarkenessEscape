using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Project_Esacape_Darkness_CS
{
    internal class Player
    {
        public string _name { get; set; }
        // 현재위치 map[column][row]
        public int _x { get; set; }
        public int _y { get; set; }
        // 현재 가지고 있는 보물
        public int _gold { get; set; }
        // 가지고 있는 현재 층의 열쇠
        public bool _key { get; set; }
        public bool _death { get; set; }

        public Player(){
            _name = null;
            _x = 0;
            _y = 0;
            _gold = 0;
            _key = false;
            _death = false;
        }
        public bool Move(ConsoleKeyInfo key)
        {
            if (_death)
            {
                DeathProcess();
                return false;
            }
            int checkX, checkY;

            checkX = _x;
            checkY = _y;
            lightOff(_x - 1, _x + 1, _y - 1, _y + 1);

            if (key.Key == ConsoleKey.UpArrow) checkY--;
            else if (key.Key == ConsoleKey.LeftArrow) checkX--;
            else if (key.Key == ConsoleKey.DownArrow) checkY++;
            else if (key.Key == ConsoleKey.RightArrow) checkX++;

            GameManager.Sound(MapManager.map[checkY, checkX]);

            // 상황에 따른 동작
            switch (MapManager.map[checkY, checkX] % 100)
            {
                case (int)eBlock.Statue:
                    if (_gold > 0)
                    {
                        PrintManager.Alarm("보물을 뺐겼습니다..");
                        Console.SetCursorPosition(MapManager.MAXROW * 2 + 24, 3);
                        Console.WriteLine("{0,4}", --_gold);
                    }
                    else
                    {
                        PrintManager.Alarm("고대의 석상에게 잡혔습니다..");
                        MapManager.map[_y, _x] = 0;
                        PrintManager.PrintObject(_x, _y);
                        DeathProcess();
                        return false;
                    }
                    break;

                case (int)eBlock.LightSwitch:
                    if (MapManager.map[checkY, checkX] != 103) MapManager._fire++;
                    MapManager._checkfire = true;
                    break;

                case (int)eBlock.Key:
                    PrintManager.Alarm("열쇠를 주웠습니다!!");
                    _key = true;
                    Console.SetCursorPosition(MapManager.MAXROW * 2 + 27, 6);
                    Console.WriteLine("O");
                    break;

                case (int)eBlock.Door:
                    if (_key == false)
                    {
                        PrintManager.Alarm("문을 열 수 없습니다..");
                        lightOn(_x - 1, _x + 1, _y - 1, _y + 1);
                        return false;
                    }
                    else
                    {
                        PrintManager.Alarm("문이 열립니다!!");
                        _key = false;
                        Console.SetCursorPosition(MapManager.MAXROW * 2 + 27, 6);
                        Console.WriteLine("X");
                    }
                    if (MapManager._checkfire == false) MapManager._fire += (MapManager._fire % 2 == 1 ? 2 : 1);
                    else MapManager._checkfire = false;
                    break;

                case (int)eBlock.Gold:
                    PrintManager.Alarm("보물을 주웠습니다!!");
                    Console.SetCursorPosition(MapManager.MAXROW * 2 + 24, 3);
                    Console.WriteLine("{0,4}", ++_gold);
                    break;

                case (int)eBlock.Whirlpool:
                    PrintManager.Alarm("소용돌이를 밟았습니다..");
                    break;

                case (int)eBlock.Ghost:
                    PrintManager.Alarm("망령에게 잡혔습니다..");
                    break;

                case (int)eBlock.NotStoneHere:
                    int tmp = MapManager.map[checkY, checkX];
                    MapManager.map[checkY, checkX] = MapManager.map[_y, _x];
                    MapManager.map[_y, _x] = tmp;
                    _y = checkY;
                    _x = checkX;
                    break;

                case (int)eBlock.Exit:
                    PrintManager.Alarm("탈출하였습니다!!");
                    _key = false;
                    Console.SetCursorPosition(MapManager.MAXROW * 2 + 27, 6);
                    Console.WriteLine("X");
                    return true;

                default:
                    break;
            }
            // 추가 이동
            switch (MapManager.map[checkY, checkX] % 100)
            {
                // 물건을 획득하는 경우
                case (int)eBlock.Empty:
                case (int)eBlock.LightSwitch:
                case (int)eBlock.Key:
                case (int)eBlock.Door:
                case (int)eBlock.Gold:
                case (int)eBlock.Exit:
                    MapManager.map[checkY, checkX] = MapManager.map[_y, _x];
                    MapManager.map[_y, _x] = (int)eBlock.Empty;
                    _y = checkY;
                    _x = checkX;
                    break;

                // 게임 오버 되는 경우
                case (int)eBlock.Whirlpool:
                case (int)eBlock.Ghost:
                    DeathProcess();
                    break;
            }
            
            lightOn(_x - 1, _x + 1, _y - 1, _y + 1);
            PrintManager.PrintSection(_x - 2, _x + 2, _y - 2, _y + 2);

            return false;
        }
        public void DeathProcess()
        {
            MapManager.map[_y, _x] = 0;
            _death = true;
            PrintManager.PrintObject(_x, _y);
        }
        public static void lightOff(int startX, int finsishX, int startY, int finishY)
        {
            for (int y = startY; y <= finishY; y++)
            {
                for (int x = startX; x <= finsishX; x++)
                {
                    if (MapManager.map[y, x] >= 100)
                    {
                        MapManager.map[y, x] -= 100;
                    }
                }
            }
        }
        public static void lightOn(int startX, int finsishX, int startY, int finishY)
        {
            for (int y = startY; y <= finishY; y++)
            {
                for (int x = startX; x <= finsishX; x++)
                {
                    if (MapManager.map[y, x] <= 200)
                    {
                        MapManager.map[y, x] += 100;
                    }
                }
            }
        }

    }
}
