using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[,] place = new string[7, 60];
            int[] road = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 19, 29, 39, 49, 59, 58, 57, 56, 55, 54, 53, 52, 51, 50, 40, 30, 20, 10 }; //角色行走路線
            int[] player_position = new int[4];
            place[1, 0] = "| 起點  |"; //格子的第二行塞地名
            place[1, 1] = "|基隆市 |";
            place[1, 3] = "|宜蘭縣 |";
            place[1, 4] = "|花蓮縣 |";
            place[1, 5] = "|台東縣 |";
            place[1, 7] = "|屏東縣 |";
            place[1, 8] = "|澎湖縣 |";
            place[1, 9] = "|高雄市 |";
            place[1, 10] = "|台北市 |";
            place[1, 20] = "|新北市 |";
            place[1, 29] = "|台南市 |";
            place[1, 39] = "|嘉義市 |";
            place[1, 40] = "|桃園市 |";
            place[1, 49] = "|嘉義縣 |";
            place[1, 50] = "|新竹縣 |";
            place[1, 51] = "|新竹市 |";
            place[1, 52] = "|苗栗縣 |";
            place[1, 53] = "|金門縣 |";
            place[1, 55] = "|台中市 |";
            place[1, 56] = "|彰化縣 |";
            place[1, 57] = "|南投縣 |";
            place[1, 59] = "|雲林縣 |";
            place[1, 6] = place[1, 30] = "| 機會  |";
            place[1, 2] = place[1, 58] = "| 命運  |";
            place[1, 19] = place[1, 54] = "| 水溝  |";
            for (int i = 0; i < 60; i++)
            {
                for (int j = 0; j < road.Length; j++)
                {
                    if (i == road[j])
                    {
                        place[0, i] = "|       |";
                        place[2, i] = place[3, i] = place[4, i] = place[5, i] = "|       |"; //這裡可以放角色名稱 //當place[2,i]=='|Player1|'就代表那角色在那格，觸發格子的效果
                        place[6, i] = "|_______|";
                        break;
                    }
                    else
                    {
                        place[2, i] = place[3, i] = place[4, i] = place[5, i] = place[6, i] = "         ";
                        if (place[1, i] == null)
                        {
                            place[0, i] = place[1, i] = "         ";
                        }
                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                place[0, i] = "_________";
            }
            for (int i = 51; i < 59; i++)
            {
                place[0, i] = "_________";
            }
            Console.Write("請輸入玩家人數(2~4)：");
            string[] playername = { "Player1", "Player2", "Player3", "Player4" };
            string[] place_name = new string[] { "起點", "基隆市", " 命運", "宜蘭縣", "花蓮縣", " 台東縣", " 機會", "屏東縣", "澎湖縣", " 高雄市", " 水溝", " 台南市", " 嘉義市", "嘉義縣", "雲林縣", " 命運", " 南投縣", " 彰化縣", " 台中市", "水溝", " 金門縣", " 苗栗縣", "新竹市", " 新竹縣", " 桃園市", " 機會", " 新北市", " 台北市 " };
            int[] money = { 150000, 150000, 150000, 150000 };
            int people = int.Parse(Console.ReadLine());
            if (people == 2)
            {
                string[] player = { "|Player1|", "|Player2|" };
                place[2, 0] = "|Player1|";
                place[3, 0] = "|Player2|";
                money[2] = money[3] = 0;
            }
            if (people == 3)
            {
                string[] player = { "|Player1|", "|Player2|", "|Player3|" };
                place[2, 0] = "|Player1|";
                place[3, 0] = "|Player2|";
                place[4, 0] = "|Player3|";
                money[3] = 0;
            }
            if (people == 4)
            {
                string[] player = { "|Player1|", "|Player2|", "|Player3|", "|Player4|" };
                place[2, 0] = "|Player1|";
                place[3, 0] = "|Player2|";
                place[4, 0] = "|Player3|";
                place[5, 0] = "|Player4|";
            }
            int character = 0;
            int walk = 0;   //骰子丟到的數(要走的步數)
            int start = 0;  //讓初始回合不會有骰子之類類的
            int[] situation_player_one = new[] { 0, 0, 0, 0 };//暫停一輪用的，玩家狀態，正常為零，若需休息一輪將變為一
            int[] situation_player_three = new int[4];//暫停3輪用的，玩家狀態，正常為零
            int[] stop = new int[4];//住院用的
            int[] situation_place = new int[28];//土地狀態,0未購買1玩家一2玩家二3玩家三4玩家四
            int[] buy_place = new int[] { 0, 30000, 0, 22000, 20000, 21000, 0, 24000, 18000, 38000, 0, 32000, 28000, 23000, 22000, 0, 21000, 23000, 35000, 0, 18000, 23000, 28000, 24000, 30000, 0, 35000, 40000 };//土地購買價格
            int[] pay_place = new int[] { 0, 3000, 0, 2200, 2000, 2100, 0, 2400, 1800, 3800, 0, 3200, 2800, 2300, 2200, 0, 2100, 2300, 3500, 0, 1800, 2300, 2800, 2400, 3000, 0, 3500, 4000 };//土地被採要付的價格
            char ans;
            string[] chance = new string[] { "墾丁春吶舉辦抽獎活動，抽到10000", "扶老奶奶過馬路，獎金1000元", "到台中歌劇院聽音樂會，門票2500", "逛六合夜市，花了2000元", ".在山路上幫單車背包客維修腳踏車，得到2500元", "參加台灣野鳥保育活動，獎勵金1000元", "參加新北市沙崙海水浴場淨灘活動，獎金1500元", "買宜蘭的牛舌餅當伴手禮，花1000", "買生活用品累積點數，兌換禮券金3000元", "在全家體驗一日店長，得到工讀金1000", "幫其他玩家買飲料，得到跑路費，其他玩家各付你500元", "在清境農場幫忙照顧綿羊一天，得到工讀金1500元", "參加蘭嶼馬拉松路跑活動第一名，獎金3000元", "在故宮博物院嬉鬧奔跑，罰款2500", "參加日月潭萬人泳渡活動，報名費1000元" };
            string[] destiny = new string[] { "油沒了，加油付2000元", "媽媽病了，回家探望媽媽，移到屏東縣", "錢包不見，到警局報失，成本1000", "經過知本泡溫泉，花了1500元", "到台北101頂樓看夜景，成本1000元", "女友想買名牌包，支付8000元", "考試考不好，想到全臺首學祈福，移到台南市", "在太魯閣險被落石砸到，驚嚇過度，休息一次", "經過澎湖時浮潛，花了2500", "在瑞豐夜市吃太雜，肚子劇痛，進醫院休息一輪", "在火車上拾獲錢包，送到警局，獎金1000", "發現出門時忘記關瓦斯，回到起點", "單眼相機沒電，又沒帶預備電池，回到起點充電", "在路上遇到老友，晚上喝酒通宵，聊天聲音過大，被罰3500元", "在捷運上吃雞排，屢勸不聽，罰款5000元" };
            string[,] move = new string[4, 28];//移動
            do
            {
                if (character == people)
                {
                    character = 0;
                }
                if (start > 0)
                {
                    Random dice = new Random();
                    walk = dice.Next(1, 7);
                    Console.WriteLine("輪到{0},目前持有金額:{1}", playername[character], money[character]);
                    if (situation_player_three[character] == 0 && situation_player_one[character] == 0)
                    {
                        Console.WriteLine("骰到的數字是{0}!", walk);
                    }
                    if (situation_player_three[character] != 0)
                    {
                        walk = 0;
                        stop[character]++;
                        Console.WriteLine("仍在醫院，無法擲骰子，{0}回合後出院", 3 - stop[character]);
                    }
                    if (situation_player_one[character] == 1)
                    {
                        walk = 0;
                        situation_player_one[character] = 0;          // 若玩家須休息一輪，會被跳過，狀態再變回正常，以供下次可以繼續玩
                    }
                }
                //place[character + 2, road[player_position[character]]] = "|       |";
                for (int i = 0; i < 4; i++)//player1移動
                {
                    move[character, player_position[i]] = "|       |";


                    if (0 <= player_position[i] && player_position[i] <= 9)
                    { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                    if (10 <= player_position[i] && player_position[i] <= 13)
                    { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                    if (14 <= player_position[i] && player_position[i] <= 23)
                    { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                    if (24 <= player_position[i] && player_position[i] <= 27)
                    { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }
                }
                player_position[character] = player_position[character] + walk;

                if (player_position[character] > 27)
                {
                    player_position[character] = player_position[character] - 28;
                }

                move[0, player_position[character]] = "|Player1|";
                move[1, player_position[character]] = "|Player2|";
                move[2, player_position[character]] = "|Player3|";
                move[3, player_position[character]] = "|Player4|";
                for (int i = 0; i < 4; i++)//player1移動
                {
                    if (0 <= player_position[i] && player_position[i] <= 9)
                    { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                    if (10 <= player_position[i] && player_position[i] <= 13)
                    { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                    if (14 <= player_position[i] && player_position[i] <= 23)
                    { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                    if (24 <= player_position[i] && player_position[i] <= 27)
                    { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }

                }
                /*player_position[character] = player_position[character] + walk;
                if (player_position[character] >= 28)
                {
                    player_position[character] = player_position[character] - 28;
                }
                if (character == 0)
                {
                    place[2, road[player_position[0]]] = "|Player1|";
                }
                if (character == 1)
                {
                    place[3, road[player_position[1]]] = "|Player2|";
                }
                if (character == 2)
                {
                    place[4, road[player_position[2]]] = "|Player3|";
                }
                if (character == 3)
                {
                    place[5, road[player_position[3]]] = "|Player4|";
                }*/

                for (int k = 0; k <= 50; k = k + 10)
                {
                    for (int i = 0; i < 7; i++)   //以下開始印格子跟字
                    {
                        for (int j = 0 + k; j < 10 + k; j++)
                        {
                            Console.Write(place[i, j]);
                        }
                        Console.Write("\n");
                    }
                }
                Console.Read();
                if (player_position[character] == 0)
                {
                    if (start > 0)
                    {
                        Console.WriteLine("你回到{0}!獲得20000元!", place_name[player_position[character] - 1]);//起點
                        money[character] = money[character] + 20000;//加20000元
                    }
                }
                else if (player_position[character] == 2 || player_position[character] == 15)
                {
                    Console.WriteLine(place_name[player_position[character]]);//命運
                    int number2 = 6/*destiny_num()*/;         //利用方法destiny_num()來存取隨機變數，讓玩家隨機抽到十五個命運中的其中一個
                    Console.WriteLine("{0}", destiny[number2]); //列出已抽中的命運
                    Console.Read();
                    if (number2 == 0)                           //以下為各命運的結果
                    {
                        money[character] = money[character] - 2000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額

                    }
                    if (number2 == 1)
                    {
                        Console.Read();
                        Console.Clear();
                        Console.WriteLine("輪到{0},目前持有金額:{1}", playername[character], money[character]);
                        //place[character + 2, road[player_position[character]]] = "|       |";
                        for (int i = 0; i < 4; i++)//player1移動
                        {
                            move[character, player_position[i]] = "|       |";


                            if (0 <= player_position[i] && player_position[i] <= 9)
                            { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                            if (10 <= player_position[i] && player_position[i] <= 13)
                            { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                            if (14 <= player_position[i] && player_position[i] <= 23)
                            { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                            if (24 <= player_position[i] && player_position[i] <= 27)
                            { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }
                        }
                        player_position[character] = 7;     //用土地代號陣列將玩家位置移到屏東縣
                        move[0, player_position[character]] = "|Player1|";
                        move[1, player_position[character]] = "|Player2|";
                        move[2, player_position[character]] = "|Player3|";
                        move[3, player_position[character]] = "|Player4|";
                        for (int i = 0; i < 4; i++)//player1移動
                        {
                            if (0 <= player_position[i] && player_position[i] <= 9)
                            { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                            if (10 <= player_position[i] && player_position[i] <= 13)
                            { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                            if (14 <= player_position[i] && player_position[i] <= 23)
                            { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                            if (24 <= player_position[i] && player_position[i] <= 27)
                            { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }

                        }
                        /*
                        if (character == 0)
                        {
                            place[2, road[player_position[0]]] = "|Player1|";
                        }
                        if (character == 1)
                        {
                            place[3, road[player_position[1]]] = "|Player2|";
                        }
                        if (character == 2)
                        {
                            place[4, road[player_position[2]]] = "|Player3|";
                        }
                        if (character == 3)
                        {
                            place[5, road[player_position[3]]] = "|Player4|";
                        }*/
                        for (int k = 0; k <= 50; k = k + 10)
                        {
                            for (int i = 0; i < 7; i++)   //以下開始印格子跟字
                            {
                                for (int j = 0 + k; j < 10 + k; j++)
                                {
                                    Console.Write(place[i, j]);
                                }
                                Console.Write("\n");
                            }
                        }
                        Console.Read();

                    }
                    if (number2 == 2)
                    {
                        money[character] = money[character] - 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number2 == 3)
                    {
                        money[character] = money[character] - 1500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number2 == 4)
                    {
                        money[character] = money[character] - 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number2 == 5)
                    {
                        money[character] = money[character] - 8000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number2 == 6)
                    {
                        Console.Read();
                        Console.Clear();
                        Console.WriteLine("輪到{0},目前持有金額:{1}", playername[character], money[character]);
                        //place[character + 2, road[player_position[character]]] = "|       |";
                        for (int i = 0; i < 4; i++)//player1移動
                        {
                            move[character, player_position[i]] = "|       |";
                            if (0 <= player_position[i] && player_position[i] <= 9)
                            { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                            if (10 <= player_position[i] && player_position[i] <= 13)
                            { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                            if (14 <= player_position[i] && player_position[i] <= 23)
                            { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                            if (24 <= player_position[i] && player_position[i] <= 27)
                            { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }
                        }
                        player_position[character] = 11;     //用土地代號陣列將玩家移到台南市
                        move[0, player_position[character]] = "|Player1|";
                        move[1, player_position[character]] = "|Player2|";
                        move[2, player_position[character]] = "|Player3|";
                        move[3, player_position[character]] = "|Player4|";
                        for (int i = 0; i < 4; i++)//player1移動
                        {
                            if (0 <= player_position[i] && player_position[i] <= 9)
                            { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                            if (10 <= player_position[i] && player_position[i] <= 13)
                            { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                            if (14 <= player_position[i] && player_position[i] <= 23)
                            { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                            if (24 <= player_position[i] && player_position[i] <= 27)
                            { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }

                        }
                        /*if (character == 0)
                        {
                            place[2, road[player_position[0]]] = "|Player1|";
                        }
                        if (character == 1)
                        {
                            place[3, road[player_position[1]]] = "|Player2|";
                        }
                        if (character == 2)
                        {
                            place[4, road[player_position[2]]] = "|Player3|";
                        }
                        if (character == 3)
                        {
                            place[5, road[player_position[3]]] = "|Player4|";
                        }*/
                        for (int k = 0; k <= 50; k = k + 10)
                        {
                            for (int i = 0; i < 7; i++)   //以下開始印格子跟字
                            {
                                for (int j = 0 + k; j < 10 + k; j++)
                                {
                                    Console.Write(place[i, j]);
                                }
                                Console.Write("\n");
                            }
                        }
                        Console.Read();
                    }
                    if (number2 == 7)
                    {
                        situation_player_one[character] = 1;          //玩家須休息一輪，玩家狀態變為一

                    }
                    if (number2 == 8)
                    {
                        money[character] = money[character] - 2500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number2 == 9)
                    {
                        situation_player_one[character] = 1;      //玩家須休息一輪，玩家狀態變為一

                    }
                    if (number2 == 10)
                    {
                        money[character] = money[character] + 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number2 == 11 || number2 == 12)
                    {
                        Console.Read();
                        Console.Clear();
                        Console.WriteLine("輪到{0},目前持有金額:{1}", playername[character], money[character]);
                        //place[character + 2, road[player_position[character]]] = "|       |";
                        
                        for (int i = 0; i < 4; i++)//player1移動
                        {
                            move[character, player_position[i]] = "|       |";
                            if (0 <= player_position[i] && player_position[i] <= 9)
                            { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                            if (10 <= player_position[i] && player_position[i] <= 13)
                            { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                            if (14 <= player_position[i] && player_position[i] <= 23)
                            { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                            if (24 <= player_position[i] && player_position[i] <= 27)
                            { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }
                        }
                        player_position[character] = 0;
                        move[0, player_position[character]] = "|Player1|";
                        move[1, player_position[character]] = "|Player2|";
                        move[2, player_position[character]] = "|Player3|";
                        move[3, player_position[character]] = "|Player4|";
                        for (int i = 0; i < 4; i++)//player1移動
                        {
                            if (0 <= player_position[i] && player_position[i] <= 9)
                            { place[character + 2, player_position[i]] = move[character, player_position[i]]; }

                            if (10 <= player_position[i] && player_position[i] <= 13)
                            { place[character + 2, player_position[i] + (player_position[i] - 9) * 9] = move[character, player_position[i]]; }

                            if (14 <= player_position[i] && player_position[i] <= 23)
                            { place[character + 2, player_position[i] + (45 - (2 * (player_position[i] - 14)))] = move[character, player_position[i]]; }

                            if (24 <= player_position[i] && player_position[i] <= 27)
                            { place[character + 2, 40 - 10 * (player_position[i] - 24)] = move[character, player_position[i]]; }

                        }
                        /*if (character == 0)
                        {
                            place[2, road[player_position[0]]] = "|Player1|";
                        }
                        if (character == 1)
                        {
                            place[3, road[player_position[1]]] = "|Player2|";
                        }
                        if (character == 2)
                        {
                            place[4, road[player_position[2]]] = "|Player3|";
                        }
                        if (character == 3)
                        {
                            place[5, road[player_position[3]]] = "|Player4|";
                        }*/
                        for (int k = 0; k <= 50; k = k + 10)
                        {
                            for (int i = 0; i < 7; i++)   //以下開始印格子跟字
                            {
                                for (int j = 0 + k; j < 10 + k; j++)
                                {
                                    Console.Write(place[i, j]);
                                }
                                Console.Write("\n");
                            }
                        }
                        Console.Read();
                    }
                    if (number2 == 13)
                    {
                        money[character] = money[character] - 3500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number2 == 14)
                    {
                        money[character] = money[character] - 5000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    Console.Read();
                }
                else if (player_position[character] == 6 || player_position[character] == 25)  //機會
                {
                    Console.WriteLine(place_name[player_position[character]]);//印出機會
                    int number1 = chance_num();
                    Console.WriteLine("{0}", chance[number1]);
                    if (number1 == 0)
                    {
                        money[character] = money[character] + 10000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 1)
                    {
                        money[character] = money[character] + 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 2)
                    {
                        money[character] = money[character] - 2500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 3)
                    {
                        money[character] = money[character] - 2000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 4)
                    {
                        money[character] = money[character] + 2500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 5)
                    {
                        money[character] = money[character] + 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 6)
                    {
                        money[character] = money[character] + 1500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 7)
                    {
                        money[character] = money[character] - 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 8)
                    {
                        money[character] = money[character] + 3000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 9)
                    {
                        money[character] = money[character] + 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 10)                           //此為其他玩家須給付500元給擲骰的玩家
                    {
                        if (character == 0)
                        {
                            money[0] = money[0] + 1500;
                            money[1] = money[1] - 500;
                            money[2] = money[2] - 500;
                            money[3] = money[3] - 500;
                        }
                        if (character == 1)
                        {
                            money[1] = money[1] + 1500;
                            money[0] = money[0] - 500;
                            money[2] = money[2] - 500;
                            money[3] = money[3] - 500;
                        }
                        if (character == 2)
                        {
                            money[2] = money[2] + 1500;
                            money[1] = money[1] - 500;
                            money[0] = money[0] - 500;
                            money[3] = money[3] - 500;
                        }
                        if (character == 3)
                        {
                            money[3] = money[3] + 1500;
                            money[1] = money[1] - 500;
                            money[2] = money[2] - 500;
                            money[0] = money[0] - 500;
                        }
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 11)
                    {
                        money[character] = money[character] + 1500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 12)
                    {
                        money[character] = money[character] + 3000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 13)
                    {
                        money[character] = money[character] - 2500;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    if (number1 == 14)
                    {
                        money[character] = money[character] - 1000;
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                    Console.Read();
                }
                else if (player_position[character] == 10 && situation_player_three[character] == 0 || player_position[character] == 19 && situation_player_three[character] == 0)
                {
                    situation_player_three[character]++;
                    Console.Write("跌到水溝送醫！停止三回合");
                    Console.Read();
                }
                else
                {
                    if (situation_place[player_position[character]] == 0)//踩到土地
                    {
                        Console.Write("是否要花費{0}元購入{1}(請填y or n)：",buy_place[player_position[character]], place_name[player_position[character]]);
                        Console.Read();
                        ans = char.Parse(Console.ReadLine());
                        if (ans == 'y')
                        {
                            if (money[character] - buy_place[player_position[character]] >= 0)
                            {
                                situation_place[player_position[character]] = character + 1;//改變土地狀態
                                money[character] = money[character] - buy_place[player_position[character]];//付錢買土地
                                Console.WriteLine("恭喜成功購入{0}", place_name[player_position[character]]);
                                Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                            }
                            else
                            {
                                Console.WriteLine("你錢不夠呢呵呵！");
                            }
                        }
                        else
                        {
                            Console.WriteLine("歡迎下次再來!");
                        }
                    }
                    else if (situation_place[player_position[character]] != character + 1) //踩到別人的土地
                    {
                        Console.WriteLine("您已踩到玩家{0}的土地{1},總共需繳給對方{2}元", situation_place[player_position[character]], place_name[player_position[character]], pay_place[player_position[character]]);
                        money[character] = money[character] - pay_place[player_position[character]];//付錢
                        money[situation_place[player_position[character]] - 1] = money[situation_place[player_position[character]] - 1] + pay_place[player_position[character]];//別人收錢
                        Console.WriteLine("目前持有金額：{0}", money[character]);//顯示金額
                    }
                }
            if (stop[character] == 3)
                {
                    stop[character] = 0;
                    situation_player_three[character] = 0;
                }
                if (character == people)
                {
                    character = 0;
                }
                if (start > 0)
                {
                    character++;
                } 
                start++;
                Console.Read();
                Console.Clear();

            } while (money[0] >= 0 && money[1] >= 0 && money[2] >= 0 && money[3] >= 0) ;
        }
        public static int chance_num()
        {
            Random number1 = new Random();
            return number1.Next(0, 14);
        }
        public static int destiny_num()
        {
            Random number2 = new Random();
            return number2.Next(0, 14);
        }
    }
}
