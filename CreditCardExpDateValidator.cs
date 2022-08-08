using System.Text.RegularExpressions;

static bool isValidExpiration(string cardDate)
{
    string[] date = Regex.Split(cardDate, "/");
    string[] currentDate = Regex.Split(DateTime.Now.ToString("MM/yy"), "/");
    string currentDateNew = currentDate[0].Replace(".", "/");
    currentDate = Regex.Split(currentDateNew, $"/");
    int compareYears = string.Compare(date[1], currentDate[1]);
    int compareMonths = string.Compare(date[0], currentDate[0]);

    if (Regex.Match(cardDate, @"^(0[1-9]|1[0-2])\/?([0-9]{2})$").Success)
    {
        //if month is "01-12" and year starts with "20"
        if (Regex.Match(date[0], @"^[0][1-9]|[1][0-2]$").Success)
        {
            //if expiration date is after current date
            if ((compareYears == 1) || (compareYears == 0 && (compareMonths == 1)))
            {
                return true;
            }
        }
    }
    return false;
}


Console.WriteLine("Enter Date:");
string date = Console.ReadLine();

if (isValidExpiration(date))
{
    Console.WriteLine("Ok");
}
else
{
    Console.WriteLine("Not Ok");
}
