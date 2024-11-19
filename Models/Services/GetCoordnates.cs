using Xadrez.Draw;

public static class GetCoordinates
{
    public static int GetCoordinateLine(string[] caseNumber, string codeLine)
    {
        int coordinateLine = Array.IndexOf(caseNumber, codeLine);
        if (coordinateLine == -1) throw new ArgumentException("Invalid column code.");
        return coordinateLine;
    }
    
    public static int GetCoordinateColumn(string[] caseAlpha, string codeCol)
    {
        int coordinateColumn = Array.IndexOf(caseAlpha, codeCol);
        if (coordinateColumn == -1) throw new ArgumentException("Invalid column code.");
        return coordinateColumn;
    }
}
