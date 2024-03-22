using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace Project_Esacape_Darkness_CS
{
    struct Rank
    {
        public string _name;
        public int _gold;
        public TimeSpan _ts;
        public int _color;

        public Rank(string n, int g, TimeSpan ts, int c) 
        {
            _name = n;
            _gold = g;
            _ts = ts;
            _color = c;
        }
    }
    internal class GameManager
    {
        public readonly static int FINALGOLD = 50;
        public readonly static int PLAYER = 208;

        static Random rd = new Random();
        Stopwatch stopwatch = new Stopwatch();

        Player _player;
        MapManager _map;
        List<Ghost> _ghosts;
        List<Rank> _rankGold;
        List<Rank> _rankTime;

        int _ghostdelay; // 고스트 움직이는 시간 지연 변수
        int _delay; // 시간 지연 변수
        ConsoleKeyInfo key; // 방향키 저장
        
        public GameManager()
        {
            _player = new Player();
            _map = new MapManager();
            _delay = 0;
            _ghostdelay = 0;
            _ghosts = new List<Ghost>();
            _rankGold = new List<Rank>();
            _rankTime = new List<Rank>();
        }
        public void Start()
        {
            Console.CursorVisible = false;
            PrintManager._playerColor= ConsoleColor.White;
            LoadRank();
            bool inMenu = true;

            while (inMenu)
            {
                Console.Clear();
                Console.Title = "암실탈출";
                Console.CursorVisible = false;
                Console.SetWindowSize(87, 7);
                Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
                Console.WriteLine("□==================================================================================□");
                Console.WriteLine("□                                  1. 시작하기                                     □");
                Console.WriteLine("□                                  2. 랭킹보기                                     □");
                Console.WriteLine("□                                  3. 종료하기                                     □");
                Console.WriteLine("□==================================================================================□");
                Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");

                ConsoleKeyInfo tmpKey = Console.ReadKey();
                while (tmpKey.Key != ConsoleKey.D1 && tmpKey.Key != ConsoleKey.D2 && tmpKey.Key != ConsoleKey.D3)
                {
                    tmpKey = Console.ReadKey();
                }
                Console.Clear();
                Console.CursorVisible = false;

                switch ((int)tmpKey.Key)
                {
                    case (int)ConsoleKey.D1:
                        {
                            Console.SetWindowSize(86, 26);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
                            Console.WriteLine("□==================================================================================□");
                            Console.WriteLine("□                                     스토리                                       □");
                            Console.WriteLine("□----------------------------------------------------------------------------------□");
                            Console.WriteLine("□ 이 곳은 엄청난 양의 보물이 숨겨져 있다고 하는 한 사원...                         □");
                            Console.WriteLine("□ 어느 도굴꾼이 보물을 캐러왔다가 갇혀버리고 말았다...                             □");
                            Console.WriteLine("□ 오래된 사원 안 정체모를 존재들을 피해 반드시 탈출을 해야만 한다!!                □");
                            Console.WriteLine("□ 하지만 여기까지 왔는데 보물은 두고 갈 수 없겠지?...                              □");
                            Console.WriteLine("□==================================================================================□");
                            Console.WriteLine("□                                    게임 설명                                     □");
                            Console.WriteLine("□----------------------------------------------------------------------------------□");
                            Console.WriteLine("□ 방향키 : ↑,←,→,↓                                                             □");
                            Console.WriteLine("□ 피해야 할 대상: ◆-석상, §-소용돌이, ▣-고스트                                  □");
                            Console.WriteLine("□ ◆: 만나면 보물을 앗아가 빛을 낸다. 보물이 없다면 목숨을..                       □");
                            Console.WriteLine("□ §: 소용돌이 소리가 거세게 들려 위치를 알 수있다. 빠져 나올 수는 없다.           □");
                            Console.WriteLine("□ ▣: 자신들의 땅을 밟은 당신을 노리고 있으니 도망가라.                            □");
                            Console.WriteLine("□ 얻어야 할 대상: ※-스위치, ¶-열쇠, ♠-보물                                      □");
                            Console.WriteLine("□ ▩,▦-문 : 열쇠가 없으면 열 수 없으니 조심할 것.                                 □");
                            Console.WriteLine("□==================================================================================□");
                            Console.WriteLine("□ # 전체화면으로 플레이할 것을 권장합니다. 우측 상단의 전체화면을 클릭하세요.    # □");
                            Console.WriteLine("□ # 마우스 클릭 시 렉이 걸릴 수 있습니다.                                        # □");
                            Console.WriteLine("□ # 혹시 렉이 걸린다면 스페이스바를 한번 눌러주세요.                             # □");
                            Console.WriteLine("□==================================================================================□");
                            Console.WriteLine("□ 캐릭터 customizing (<-, ->): ⊙                                                  □");
                            Console.WriteLine("□==================================================================================□");
                            Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
                            Console.SetCursorPosition(32, 23);
                            Console.CursorVisible = false;
                            while (true)
                            {
                                tmpKey = Console.ReadKey();
                                // 캐릭터 색깔 커스터마이징
                                if (tmpKey.Key == ConsoleKey.Enter)
                                {
                                    Console.WriteLine("□ 이름을 설정하세요(1~5글자):                                                      □");
                                    Console.WriteLine("□==================================================================================□");
                                    Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
                                    break;
                                }
                                else if (tmpKey.Key == ConsoleKey.RightArrow)
                                {
                                    if (PrintManager._playerColor < (ConsoleColor)15) PrintManager._playerColor++;
                                    Console.SetCursorPosition(32, 23);
                                    Console.ForegroundColor = PrintManager._playerColor;
                                    Console.Write("⊙");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else if (tmpKey.Key == ConsoleKey.LeftArrow)
                                {
                                    if (PrintManager._playerColor > (ConsoleColor)1) PrintManager._playerColor--;
                                    Console.SetCursorPosition(32, 23);
                                    Console.ForegroundColor = PrintManager._playerColor;
                                    Console.Write("⊙");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            while (_player._name == null)
                            {
                                Console.SetCursorPosition(31, 23);
                                string str = Console.ReadLine();

                                if (str.ToArray().Length < 1 || str.ToArray().Length > 5)
                                {
                                    Console.SetCursorPosition(0, 23);
                                    Console.Write("□ 이름을 설정하세요(1~5글자):                            (글자 수가 맞지 않습니다.)□");
                                    continue;
                                }
                                else _player._name = str;
                            }
                            Console.SetWindowSize(240, 63);
                            inMenu = false;
                            Console.Clear();
                            Console.ForegroundColor = (ConsoleColor)8;
                            Console.CursorVisible = false;
                            PrintManager.PrintInform();
                            stopwatch.Start();
                            break;
                        }
                    case (int)ConsoleKey.D2:
                        {
                            Console.SetWindowSize(86, 10 + _rankGold.Count * 3);
                            bool WatchRankTime = true;
                            while (true)
                            {
                                int i = 0, tmp = 1;
                                Console.Clear();
                                Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
                                if (WatchRankTime)
                                {
                                    TimeSpan timeTmp = new TimeSpan();

                                    Console.WriteLine("□----------------------------------------------------------------------------------□");
                                    Console.WriteLine("□==================================[시간 별 랭킹]==================================□");
                                    Console.WriteLine("□----------------------------------------------------------------------------------□");
                                    Console.WriteLine("□    캐릭터  |   등수  |    이름    |      보물갯수      |         걸린 시간       □");
                                    foreach (Rank r in _rankTime)
                                    {
                                        if (timeTmp == r._ts) tmp++;
                                        else
                                        {
                                            timeTmp = r._ts;
                                            i += tmp;
                                            tmp = 1;
                                        }
                                        Console.ForegroundColor = (ConsoleColor)r._color;
                                        Console.WriteLine("□----------------------------------------------------------------------------------□");
                                        Console.WriteLine("□      ⊙    |{0,7}등| {1,10} | {2,17}개| {3,16}분 {4,3}초□", i, r._name, r._gold, (int)r._ts.TotalSeconds / 60, (int)r._ts.TotalSeconds % 60);
                                        Console.WriteLine("□----------------------------------------------------------------------------------□");
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("□============================  << 보물 별 랭킹 >>  ================================□");
                                }
                                else
                                {
                                    int goldTmp = int.MaxValue;
                                    Console.WriteLine("□----------------------------------------------------------------------------------□");
                                    Console.WriteLine("□==================================[보물 별 랭킹]==================================□");
                                    Console.WriteLine("□----------------------------------------------------------------------------------□");
                                    Console.WriteLine("□    캐릭터  |   등수  |    이름    |      보물갯수      |         걸린 시간       □");
                                    foreach (Rank r in _rankGold)
                                    {
                                        if (r._gold == goldTmp) tmp++;
                                        else
                                        {
                                            goldTmp = r._gold;
                                            i += tmp;
                                            tmp = 1;
                                        }
                                        Console.ForegroundColor = (ConsoleColor)r._color;
                                        Console.WriteLine("□----------------------------------------------------------------------------------□");
                                        Console.WriteLine("□      ⊙    |{0,7}등| {1,10} | {2,17}개| {3,16}분 {4,3}초□", i, r._name, r._gold, (int)r._ts.TotalSeconds / 60, (int)r._ts.TotalSeconds % 60);
                                        Console.WriteLine("□----------------------------------------------------------------------------------□");

                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("□============================  << 시간 별 랭킹 >>  ================================□");
                                }
                                Console.WriteLine("□----------------------------------------------------------------------------------□");
                                Console.WriteLine("□                        =  나가시려면 아무 키나 누르세요  =                       □");
                                Console.WriteLine("□----------------------------------------------------------------------------------□");
                                Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
                                tmpKey = Console.ReadKey();
                                if (tmpKey.Key == ConsoleKey.LeftArrow || tmpKey.Key == ConsoleKey.RightArrow)
                                {
                                    WatchRankTime = WatchRankTime ? false : true;
                                }
                                else break;
                            }
                            break;
                        }
                    default:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }
        public void Finish()
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            Rank tmp = new Rank(_player._name, _player._gold, ts, (int)PrintManager._playerColor);
            

            int playerTimeRank = 0, playerGoldRank = 0;

            FileStream fs = new FileStream("RankGold.txt", FileMode.Create);
            StreamWriter sr = new StreamWriter(fs);
            int goldi = 0, timei = 0;

            for (goldi = 0; goldi < _rankGold.Count; goldi++)
            {
                if (_rankGold[goldi]._gold < _player._gold)
                {
                    playerGoldRank = goldi + 1;
                    _rankGold.Insert(goldi, tmp);
                    break;
                }
            }
            if (_rankGold.Count < 10 && goldi == _rankGold.Count)
            {
                _rankGold.Add(tmp);
                playerGoldRank = _rankGold.Count;
            }
            for (int i = 0; i < _rankGold.Count; i++)
            {
                sr.WriteLine(String.Format("{0}/{1}/{2}/{3}", _rankGold[i]._name, _rankGold[i]._gold, _rankGold[i]._ts, _rankGold[i]._color));
            }
            sr.Close();
            fs.Close();

            fs = new FileStream("RankTime.txt", FileMode.Create);
            sr = new StreamWriter(fs);
            for (timei = 0; timei < _rankTime.Count; timei++)
            {
                if (_rankTime[timei]._ts > ts)
                {
                    playerTimeRank = timei + 1;
                    _rankTime.Insert(timei, tmp);
                    break;
                }
            }
            if (_rankTime.Count < 10 && timei == _rankTime.Count)
            {
                _rankTime.Add(tmp);
                playerTimeRank = _rankTime.Count;
            }
            for (int i = 0; i < _rankTime.Count; i++)
            {
              sr.WriteLine(String.Format("{0}/{1}/{2}/{3}", _rankTime[i]._name, _rankTime[i]._gold, _rankTime[i]._ts, _rankTime[i]._color));
            }
            sr.Close();
            fs.Close();

            Console.Clear();
            Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
            Console.WriteLine("□                     축하합니다!!! 탈출하셨습니다!!!                              □");
            Console.WriteLine("□                   당신은 총 보물 {0,2}개를 가졌습니다.                              □", _player._gold);
            Console.WriteLine("□                 탈출까지 {0,2}시간 {1,2}분 {2,2}초가 걸렸습니다.                          □", ts.Hours, ts.Minutes, ts.Seconds);
            if (_rankTime.Count < 10 && timei == _rankTime.Count || timei != _rankTime.Count)
            {
                Console.WriteLine("□----------------------------------------------------------------------------------□");
                Console.WriteLine("□                대단하군요! 시간 랭킹 {0,2}위에 등록되셨습니다!                      □", playerTimeRank);
            }
            if (_rankGold.Count < 10 && goldi == _rankGold.Count || goldi != _rankGold.Count)
            {
                Console.WriteLine("□----------------------------------------------------------------------------------□");
                Console.WriteLine("□                대단하군요! 보물 랭킹 {0,2}위에 등록되셨습니다!                      □", playerGoldRank);
            }
            Console.WriteLine("□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□");
            Thread.Sleep(5000);
            Console.ReadKey();
        }
        public bool InFirstStage()
        {
            StageSetting();
            Queue<int> notMoveGhost = new Queue<int>();
            bool isEscape = false;

            while (!isEscape && !_player._death)
            {
                Console.CursorVisible = false;
                do
                {
                    if (_ghostdelay >= 70000)
                    { 
                        for (int i = 0; i < _ghosts.Count; i++)
                        {
                            if (_ghosts[i].Move(_player))
                            {
                                notMoveGhost.Enqueue(i);
                            }
                            if (_player._death) break;
                        }

                        while (notMoveGhost.Count > 0 && !_player._death)
                        {
                            int index = notMoveGhost.Dequeue();
                            _ghosts[index].Move(_player);
                        }
                        _ghostdelay = 0;
                    }
                    else
                    {
                        _ghostdelay++;
                    }
                    if (_player._death) break;
                    _map.FirstRule();
                } while (!Console.KeyAvailable);

                if (!_player._death)
                {
                    key = Console.ReadKey();
                    isEscape = _player.Move(key);
                }
                else break;
            }
            return _player._death;
        }

        public bool InSecondStage()
        {
            StageSetting();
            Queue<int> notMoveGhost = new Queue<int>();
            bool isEscape = false;
            while (!isEscape && !_player._death)
            {
                Console.CursorVisible = false;
                do
                {
                    if (_ghostdelay == 70000)
                    {
                        for (int i = 0; i < _ghosts.Count; i++)
                        {
                            if (_ghosts[i].Move(_player))
                            {
                                notMoveGhost.Enqueue(i);
                            }
                            if (_player._death) break;
                        }

                        while (notMoveGhost.Count > 0 && !_player._death)
                        {
                            int index = notMoveGhost.Dequeue();
                            _ghosts[index].Move(_player);
                        }
                        _ghostdelay = 0;
                    }
                    else
                    {
                        _ghostdelay++;
                    }
                    _map.SecondRule();
                } while (!Console.KeyAvailable);

                if (!_player._death)
                {
                    key = Console.ReadKey();
                    isEscape = _player.Move(key);
                }
                else break;

            }
            return _player._death;
        }
        public bool InThirdStage()
        {
            StageSetting();
            Gold();
            PrintManager.PrintAllMap();
            bool isEscape = false;
            while (!isEscape)
            {
                Console.CursorVisible = false;
                do
                {
                    _delay++;
                    _map.ThirdRule();
                    Rock();
                } while (!Console.KeyAvailable);

                if (!_player._death)
                {
                    key = Console.ReadKey();
                    isEscape = _player.Move(key);
                }
                else break;
            }
            return _player._death;
        }
        public void StageSetting()
        {
            PrintManager.PrintGameSet();
            _player._death = false;
            _map.MapChange();
            SettingPlayer();
            GhostCheck();
            _ghostdelay = 0;
            PrintManager.PrintAllMap();
        }
        public void SettingPlayer()
        {
            int x, y;
            for (y = 0; y < MapManager.MAXCOL; y++)
            {
                for (x = 0; x < MapManager.MAXROW; x++)
                {
                    if (MapManager.map[y, x] == PLAYER)
                    {
                        _player._x = x;       // y축
                        _player._y = y;    // x축
                        return;
                    }
                }
            }
        }
        public void LoadRank()
        {
            // 시간 순위가 기록된 파일을 받아오는 코드
            FileStream fs = new FileStream("RankTime.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            char[] tmpTime = sr.ReadToEnd().ToCharArray();

            for (int i = 0; i < tmpTime.Length; i++)
            {
                string n = "", g = "", t = "", c = "";
                while (tmpTime[i] != '/') n += tmpTime[i++];
                i++;
                while (tmpTime[i] != '/') g += tmpTime[i++];
                i++;
                while (tmpTime[i] != '/') t += tmpTime[i++];
                i++;
                while (tmpTime[i] != '\n') c += tmpTime[i++];
                _rankTime.Add(new Rank(n, int.Parse(g), TimeSpan.Parse(t), int.Parse(c)));
            }
            sr.Close();
            fs.Close();

            // 보물 순위가 기록된 파일을 받아오는 코드
            fs = new FileStream("RankGold.txt", FileMode.OpenOrCreate);
            sr = new StreamReader(fs);

            char[] tmpGold = sr.ReadToEnd().ToCharArray();

            for (int i = 0; i < tmpGold.Length; i++)
            {
                string n = "", g = "", t = "", c = "";
                while (tmpGold[i] != '/') n += tmpGold[i++];
                i++;
                while (tmpGold[i] != '/') g += tmpGold[i++];
                i++;
                while (tmpGold[i] != '/') t += tmpGold[i++];
                i++;
                while (tmpGold[i] != '\n') c += tmpGold[i++];
                _rankGold.Add(new Rank(n, int.Parse(g), TimeSpan.Parse(t), int.Parse(c)));
            }
            sr.Close();
            fs.Close();
        }
        void GhostCheck()
        {
            _ghosts.Clear();
            int x, y;
            for (y = 0; y < 62; y++)
            {
                for (x = 0; x < 72; x++)
                {
                    if (MapManager.map[y, x] == 9)
                    {
                        _ghosts.Add(new Ghost(x, y));
                    }
                }
            }
        }
        public void Rock()
        {
            int x, y;
            while (true)
            {
                y = rd.Next(0, 62); //y = rand() % 62;
                x = rd.Next(0, 72); //x = rand() % 72;
                if (MapManager.map[y, x] % 100 == 0 || MapManager.map[y, x] % 100 == 6) break;
                continue;
            }
            if (_delay > 2000 && MapManager.map[33, 34] == 212)
            {
                MapManager.map[y, x] = 213;
                Console.SetCursorPosition(2 * x, y);
                PrintManager.PrintObject(x, y);
                _delay = 0;
            }
        }
        public void Gold()
        {
            int x, y, i;
            for (i = 0; i < FINALGOLD;)
            {
                y = rd.Next(5, 62);  //y = rand() % 57 + 5;
                x = rd.Next(0, 72); //x = rand() % 72;
                if (MapManager.map[y, x] % 100 == 0)
                {
                    MapManager.map[y, x] = 6;
                    i++;
                }
            }
        }
        public static void Sound(int situation)
        {
            switch (situation % 100)
            {
                case 1:
                    Console.Beep(247, 20);
                    Console.Beep(123, 70);
                    break;
                case 3:
                case 4:
                    Console.Beep(659, 20);
                    Console.Beep(698, 20);
                    Console.Beep(784, 60);
                    break;
                case 5:
                    Console.Beep(65, 50);
                    Console.Beep(65, 50);
                    break;
                case 6:
                    Console.Beep(1397, 20);
                    Console.Beep(2637, 70);
                    break;
            }
        }
    }
}
