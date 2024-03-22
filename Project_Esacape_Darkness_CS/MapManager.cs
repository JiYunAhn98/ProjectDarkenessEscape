using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Esacape_Darkness_CS
{
    internal class MapManager
    {
        public readonly static int MAXROW = 72;
        public readonly static int MAXCOL = 62;

        public static int[,] map { get; set; }
        public static int _fire { get; set; } // 불켜기 룰
        public static bool _checkfire { get; set; }
        static int _stage { get; set; }

        public MapManager() 
        { 
            map = new int[MAXCOL, MAXROW];
            _stage = 0;
            _fire = 0;
            _checkfire = false;
        }
        public void MapChange()
        {
            PrintManager.Alarm("다음 장소로 이동합니다.");
            _fire = 0;
            _checkfire = false;
            FileStream fs;
            if (_stage == 0) fs = new FileStream("FirstMap.txt", FileMode.Open);
            else if (_stage == 1) fs = new FileStream("SecondMap.txt", FileMode.Open);
            else if (_stage == 2) fs = new FileStream("ThirdMap.txt", FileMode.Open);
            else return;
            try
            {
                StreamReader sr = new StreamReader(fs);
                _stage++;
                char[] tmp;
                string tmpS= "";

                for(int y = 0; y < MAXCOL; y++) 
                {
                    int x = 0;
                    tmp = sr.ReadLine().ToCharArray();

                    for (int cnt = 0; cnt < tmp.Length; cnt++)
                    {
                        if (tmp[cnt] == ',')
                        {
                            if (x == MAXROW - 1 || y == MAXCOL - 1 || x == 0 || y == 0) map[y, x] = 211;
                            else map[y, x] = int.Parse(tmpS);
                            tmpS = "";
                            x++;
                        }
                        else tmpS += tmp[cnt];
                    }
                }
                sr.Dispose();
                sr.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("The file was not found: {0}", e);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The directory was not found: {0}", e);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be opened: {0}", e);
            }
            fs.Dispose();
            fs.Close();
        }
        public void FirstRule()
        {
            switch (_fire)
            {
                case 1: buttonOn(51, 71, 0, 27);
                    break;
                case 3: buttonOn(0, 50, 0, 27);
                    break;
                case 5: buttonOn(36, 71, 28, 61);
                    break;
                case 7: buttonOn(0, 35, 28, 61);
                    break;
                default:
                    break;
            }
        }
        public void SecondRule()
        {
            switch (_fire)
            {
                case 1: buttonOn(0, 36, 0, 61);
                    break;
                case 3: buttonOn(37, 71, 26, 61);
                    break;
                case 5: buttonOn(37, 71, 0, 25);
                    break;
                default: break;
            }
        }
        public void ThirdRule()
        {
            switch (_fire)
            {
                case 1: buttonOn(0, 71, 0, 61);
                    break;
            }
        }
        /// <summary>
        /// 스위치를 켜서 모든 곳의 불을 켰을 경우에 사용된다.
        /// </summary>
        /// <param name="startX"> 불을 켜기 시작할 X값 </param>
        /// <param name="finishX"> 불을 켜야하는 마지막 X값 </param>
        /// <param name="startY"> 불을 켜기 시작할 Y값 </param>
        /// <param name="finishY"> 불을 켜야하는 마지막 Y값 </param>
        public void buttonOn(int startX, int finishX, int startY, int finishY)
        {
            for (int y = startY; y <= finishY; y++)
            {
                for (int x = startX; x <= finishX; x++)
                {
                    if (MapManager.map[y, x] < 100)
                    {
                        MapManager.map[y, x] += 200;
                    }
                    else if (MapManager.map[y, x] < 200)
                    {
                        MapManager.map[y, x] += 100;
                    }
                }
            }
            PrintManager.PrintSection(startX, finishX, startY, finishY);
            PrintManager.Alarm("특정구역에 불이 켜집니다!!!");
            _fire++;
        }
    }
}
