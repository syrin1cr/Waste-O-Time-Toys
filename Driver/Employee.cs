/// <summary>
/// This class will store all the data for a single employee. Including personal information,
/// and sales information.
/// 
/// Data held:
/// first name, last name, sales order ID number, employee ID, number, games sales amount, games sales quantity
/// 
/// Creator: Cody S
/// Project Program.cs
/// File: clsEmployee
/// </summary>
public class clsEmployee
{
    // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
    // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
    // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS

    private const string SALESREPORTTABING = "{0,-20} {1,-5} ${2,-10:F2} ${3,-10:F2} ${4,-10:F2} ${5,-10:F2} ${6,-10:F2}";
    private const string SALESREPORTTABINGNOCURRENCY = "{0,-20} {1,-5} {2,-11} {3,-11} {4,-11} {5,-11} {6,-11}";

    // VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES
    // VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES
    // VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES -- VARIABLES

    public string _firstName;
    public string _lastName;
    public int _orderID;
    public int _ID;
    public float _gamesSales;
    public int _gamesQuantity;
    public float _dollsSales;
    public int _dollsQuantity;
    public float _buildingSales;
    public int _buildingQuantity;
    public float _modelSales;
    public float _modelQuantity;
    public float _totalSales;

    // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS
    // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS
    // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS

    /// <summary>
    /// This is the default constructor for the clsEmployee class. Its job is to assign 
    /// values to all variables in the class on creation of a new clsEmployee.
    /// </summary>
    /// <param name="firstName"></param>            Employee first name
    /// <param name="lastName"></param>             Employee last name
    /// <param name="orderID"></param>              Order ID name
    /// <param name="ID"></param>                   Employee ID name
    /// <param name="gamesSales"></param>           Prise for game sales 
    /// <param name="gamesQuantity"></param>        Game sales done by this Employee
    /// <param name="dollsSales"></param>           Prise for doll sales
    /// <param name="dollsQuantity"></param>        Dolls sales done by this Employee
    /// <param name="buildingSales"></param>        Prise for building sales
    /// <param name="buidingQuantity"></param>      Building sales done by this Employee
    /// <param name="modelSales"></param>           Prise for model sales
    /// <param name="modelQuantity"></param>        Model sales done by this Employee
    public clsEmployee(string firstName, string lastName, int orderID, int ID,
        float gamesSales, int gamesQuantity, float dollsSales, int dollsQuantity,
        float buildingSales, int buidingQuantity, float modelSales, float modelQuantity)
    {
        this._firstName = firstName;
        this._lastName = lastName;
        this._orderID = orderID;
        this._ID = ID;
        this._gamesSales = gamesSales;
        this._gamesQuantity = gamesQuantity;
        this._dollsSales = dollsSales;
        this._dollsQuantity = dollsQuantity;
        this._buildingSales = buildingSales;
        this._buildingQuantity = buidingQuantity;
        this._modelSales = modelSales;
        this._modelQuantity = modelQuantity;
        calcTotalSales();
    }
    /// <summary>
    /// This method will calculate the total sales of the currnt clsEmployee class.
    /// </summary>
    public void calcTotalSales()
    {
        this._totalSales =
            _gamesSales +
            _dollsSales +
            _buildingSales +
            _modelSales;
    }
    /// <summary>
    /// This method will return a formated string that is ready for console ouput
    /// </summary>
    /// <returns>Formated string</returns>
    public override string ToString()
    {
        return string.Format(SALESREPORTTABING,
            $"{_lastName}, {_firstName}", _orderID,
            (_gamesSales * _gamesQuantity),
            (_dollsSales * _dollsQuantity),
            (_buildingSales * _buildingQuantity),
            (_modelSales * _modelQuantity),
            _totalSales);
    }
    /// <summary>
    /// Will return the string formater we use for output.
    /// </summary>
    /// <returns>String formating we use for output in tostring</returns>
    public static string getstringFormat()
    {
        return SALESREPORTTABINGNOCURRENCY;
    }
}
