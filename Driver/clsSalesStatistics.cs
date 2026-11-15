/// <summary>
/// This class will hold calculated data for all employees for each sales item.
/// 
/// Creator: Cody S
/// Project Program.cs
/// File: clsSalesStatistics
/// </summary>
public class clsSalesStatistics
{
    // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
    // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
    // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
    private const string SALESREPORTTABING = "{0,-10} ${1,-10:F2} ${2,-10:F2} ${3,-10:F2} ${4,-10:F2}";
    private const string SALESREPORTTABINGNOCURRENCY = "{0,-10:F2} {1,-11:F2} {2,-11:F2} {3,-11:F2} {4,-11:F2}";

    // VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES
    // VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES
    // VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES
    public float _Games;
    public float _Dolls;
    public float _Building;
    public float _Model;
    public string _Name;
    public float _total;

    // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS
    // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS
    // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS
    public clsSalesStatistics(float games, float dolls, float building, float model, string name)
    {
        this._Games = games;
        this._Dolls = dolls;
        this._Building = building;
        this._Model = model;
        this._Name = name;
        _total = _Games + _Dolls +_Building + _Model;
    }
    /// <summary>
    /// will retrn the constant SALESREPORTTABINGNOCURRENCY
    /// </summary>
    /// <returnsSALESREPORTTABINGNOCURRENCY></returns>
    public static string getstringFormat()
    {
        return SALESREPORTTABINGNOCURRENCY;
    }
    /// <summary>
    /// over rides the tostring to print all the Sales stats
    /// </summary>
    /// <returns>String</returns>
    public override string ToString()
    {
        return string.Format(SALESREPORTTABING, _Name, _Games, _Dolls, _Building, _Model);
    }
    /// <summary>
    /// Will return a string ready to print with no currency values in the print.
    /// </summary>
    /// <returns></returns>
    public string ToStringNoCurrency()
    {
        return string.Format(SALESREPORTTABINGNOCURRENCY, _Name, _Games, _Dolls, _Building, _Model);
    }
}