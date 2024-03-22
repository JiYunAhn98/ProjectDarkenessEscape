using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project_Esacape_Darkness_CS
{
    internal class Ghost
    {
        int _x;
        int _y;
        int _overlap;

        public Ghost(int x, int y)
        {
            _x = x;
            _y = y;
            _overlap = 0;
        }

        /// <summary>
        /// 움직이는 함수
        /// </summary>
        /// <param name="p"></param>
        /// <returns> player를 잡았을 경우 true, 아니면 false </returns>
        public bool Move(Player p)
        {
            if (MapManager.map[_y, _x] == (int)eBlock.Ghost)
            {
                return false;
            }
            int moveX = _x, moveY = _y;
            
            if (p._x > _x) moveX++;
            else if (p._x < _x) moveX--;

            if (p._y > _y) moveY++;
            else if (p._y < _y) moveY--;


            if (MapManager.map[moveY, moveX] == (int)eBlock.OnCharacter)
            {
                p._death = true;
            }
            else if (MapManager.map[moveY, moveX] == (int)eBlock.OnGhost)
            {
                return true;
            }

            MapManager.map[_y, _x] = _overlap;
            _overlap = MapManager.map[moveY, moveX];
            MapManager.map[moveY, moveX] = (int)eBlock.OnGhost;
            Console.SetCursorPosition(2 * moveX, moveY);
            PrintManager.PrintObject(moveX, moveY);
            Console.SetCursorPosition(2 * _x, _y);
            PrintManager.PrintObject(_x, _y);
            _x = moveX;
            _y = moveY;

            return false;
        }

    }
}
