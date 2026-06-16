using System.Text;

static string BuildDiamant(int size)
{
    var diamatStruct = new StringBuilder();
    int center = size / 2;
    for (var i = 0; i < size; i++)
    {
        for (var j = 0; j < size; j++)
        {
            if (Math.Abs(i - center) + Math.Abs(j - center) == center)
            {
                diamatStruct.Append('X');
            }
            else
            {
                diamatStruct.Append(' ');
            }
        }
        diamatStruct.AppendLine();
    }
    return diamatStruct.ToString();
}


Console.WriteLine(BuildDiamant(5));