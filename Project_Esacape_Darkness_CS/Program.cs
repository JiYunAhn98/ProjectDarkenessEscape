using System;

// 1: 석상, 2: 벽, 3: 스위치, 4: 열쇠, 5: 문 6: 보물, 7: 소용돌이
// 8: 캐릭터, 9: 고스트, 10: 돌이 떨어지지 않을 곳, 11 : 테두리, 12: 탈출구, 13: 돌
// 1 ~ 199: 스위치 안켜진 상태, 101 ~ 299: 스위치가 켜짐
public enum eBlock
{
    Empty           = 0,
    Statue          = 1,
    Wall            = 2,
    LightSwitch     = 3,
    Key             = 4,
    Door            = 5,
    Gold            = 6,
    Whirlpool       = 7,
    Character       = 8,
    Ghost           = 9,
    NotStoneHere    = 10,
    Side            = 11,
    Exit            = 12,
    Stone           = 13,

    OnCharacter     = 208,
    OnGhost         = 209
}

namespace Project_Esacape_Darkness_CS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                GameManager gm = new GameManager();

                gm.Start();

                if (gm.InFirstStage())
                {
                    PrintManager.PrintGameSet();
                    continue;
                }
                if (gm.InSecondStage())
                {
                    PrintManager.PrintGameSet();
                    continue;
                }
                if (gm.InThirdStage())
                {
                    PrintManager.PrintGameSet();
                    continue;
                }

                gm.Finish();
            }
        }
    }
}
