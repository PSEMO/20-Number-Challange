using System.Globalization;

FileStream fs = new FileStream("output.txt", FileMode.Create);
StreamWriter sw = new StreamWriter(fs);

Random rnd = new Random();

int[] Numbers = new int[20];
int score = 0;
int GameWon = 0;
int GameLost = 0;

Boolean GameIsContinuing = true;

int GameCount = 0;

while(GameIsContinuing)
{
    int Number = rnd.Next(1, 1001);
    WriteToConsole(Number + "");

    float placeF = (Number * 2) / 100;
    int place = (int)Math.Ceiling(placeF);

    if (place == 20)
        place--;

    Place(place, Number);
    CheckBoard();

    if(GameWon + GameLost > 100000)
    {
        GameIsContinuing = false;

        WriteToConsole("GameWon: " + GameWon);
        WriteToConsole("GameLost: " + GameLost);
    }
}

sw.Flush();
sw.Close();


//-------------------------------------------------------------------------------

void Place(int To, int What)
{
    if (Numbers[To] == 0)
    {
        Numbers[To] = What;
    }
    else
    {
        if (What > Numbers[To])
        {
            if (To + 1 == 20)
            {
                Lost();
            }
            else
            {
                if(What < Numbers[To + 1])
                {
                    if (Numbers[To + 1] != 0)
                        Lost();
                }
                else
                {
                    Place(To + 1, What);
                }
            }
        }
        else
        {
            if (To - 1 == -1)
            {
                Lost();
            }
            else
            {
                if (What > Numbers[To - 1])
                {
                    if (Numbers[To - 1] != 0)
                        Lost();
                }
                else
                {
                    Place(To - 1, What);
                }
            }
        }
    }
}

void CheckBoard()
{
    WriteToConsole("Current game state: ");

    int LastNumber = -1;

    score = 0;

    int j = 0;
    foreach(int i in Numbers)
    {
        //Checking game state
        if(i != 0)
        {
            if(LastNumber < i)
            {
                score++;
            }
            else
            {
                GameLost++;
                ResetGame();
            }
            LastNumber = i;
        }
        //writing game state
        j++;
        WriteToConsole(j + ": " + i);
    }
    WriteToConsole("Score = " + score + ".");
    WriteToConsole("-");

    if(score >= 20)
    {
        Won();
    }
}

void ResetGame()
{
    int j = 0;
    foreach(int i in Numbers)
    {
        Numbers[j] = 0;
        j++;
    }
}

void Lost()
{
    GameLost++;
    ResetGame();
    GameCount++;
    Console.WriteLine(GameCount);
}

void Won()
{
    GameWon++;
    ResetGame();
    GameCount++;
    Console.WriteLine(GameCount);
}

void WriteToConsole(string input)
{
    //Console.WriteLine(input);
    sw.WriteLine(input);
}