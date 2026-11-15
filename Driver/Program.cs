using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace Driver
{
    /// <summary>
    /// This programs goal is to take a textfile of employees sales information and store it.
    /// With this information we will generate sorted reports of ever single user.
    /// After we will calculate Statistics on the same set of data and report it back to the user.
    /// Last we will calculate the average sales of all the users and display the above average employees.
    /// 
    /// Creator: Cody S
    /// Project Program.cs
    /// File: clsEmployee
    /// </summary>
    internal class Program
    {
        // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
        // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
        // CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS -- CONSTANTS
        public const int STATSCONSOLESIZE = 50;
        private const string filePath = "ToyOrder.txt";

        /// <summary>
        /// This enum stores integer values to string
        /// </summary>
        public enum EmployeeType
        {
            firstName,
            lastName,
            orderID,
            ID,
            gamesSales,
            gamesQuantity,
            dollsSales,
            dollsQuantity,
            buildingSales,
            buidingQuantity,
            modelSales,
            modelQuantity
        }

        // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS
        // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS
        // METHODS/FUNCTIONS -- METHODS/FUNCTIONS -- METHODS/FUNCTIONS

        /// <summary>
        /// This method will take the string and split it into sub parts.
        /// after the split string the methoed returns a new clsEmployee with
        /// the information from the string you send it. 
        /// </summary>
        /// <param name="currentLine"></param>  This is the unsplit string
        /// <returns>single clsEmployee instance</returns>
        private static clsEmployee parseStringEmployee(string currentLine)
        {
            string[] parts = currentLine.Split(' ');

            return new clsEmployee(
            parts[(int)EmployeeType.firstName],
            parts[(int)EmployeeType.lastName],
            int.Parse(parts[(int)EmployeeType.orderID]),
            int.Parse(parts[(int)EmployeeType.ID]),
            float.Parse(parts[(int)EmployeeType.gamesSales]),
            int.Parse(parts[(int)EmployeeType.gamesQuantity]),
            float.Parse(parts[(int)EmployeeType.dollsSales]),
            int.Parse(parts[(int)EmployeeType.dollsQuantity]),
            float.Parse(parts[(int)EmployeeType.buildingSales]),
            int.Parse(parts[(int)EmployeeType.buidingQuantity]),
            float.Parse(parts[(int)EmployeeType.modelSales]),
            float.Parse(parts[(int)EmployeeType.modelQuantity]));
        }
        /// <summary>
        /// This method will open and read the file. It will create a List
        /// of employees from the file data.
        /// </summary>
        /// <returns>List of employees</returns>
        private static List<clsEmployee> getFile()
        {
            List<clsEmployee> tempList = new List<clsEmployee>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        // Process each line here
                        tempList.Add(parseStringEmployee(line));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
            return tempList;
        }
        /// <summary>
        /// This method will print the Header information fot he sales report.
        /// Next it prints the needed headers for the types of items report on.
        /// </summary>
        /// <param name="orderByChoice"></param>    Is used in printing the heading
        ///                                         We use this method for printing
        ///                                         more than one heading. This string
        ///                                         allows us to cusomize heading.                                     
        private static void printSalesReport(string orderByChoice)
        {
            string[] txtToCenter = { "Waste O' Time Toys", "*** Sales Report by " + orderByChoice + " ***", "--------------------------------------------------" };
            int leftPad = 0;
            string prtinStringFormated;

            foreach (string currentTxtToCenter in txtToCenter)
            {
                leftPad = (95 - currentTxtToCenter.Length) / 2;
                leftPad += currentTxtToCenter.Length;

                prtinStringFormated = string.Format("{0, " + leftPad + "}", currentTxtToCenter);
                Console.WriteLine(prtinStringFormated);
            }
            salesReportPrintHeadings();
        }
        /// <summary>
        /// Prints the headings for the type of headings we report on.
        /// IE: games,dolls ect..
        /// </summary>
        private static void salesReportPrintHeadings()
        {
            var SALESREPORTTABING = clsEmployee.getstringFormat();
            Console.WriteLine(SALESREPORTTABING, "Name", "ID", "Games", "Dolls", "Building", "Models", "Total");
            Console.WriteLine(SALESREPORTTABING, "----", "--", "-----", "-----", "--------", "------", "-----");
        }
        /// <summary>
        /// This method will order our List of clsEmployee by what ever type
        /// we pass in using orderByChoice var (Name/ID).
        /// 
        /// After the method sorts it will print it to console.
        /// </summary>
        /// <param name="employeeIDOrder"></param>  This is the list of all employee data
        /// <param name="orderByChoice"></param>    This type of data we are printing.
        private static void salesReportBy(List<clsEmployee> employeeIDOrder, String orderByChoice)
        {
            IEnumerable<clsEmployee> LINQResults;

            //The user can choose what variable we are sorting by in the call with orderByChoice string.
            //The default will just order by _last name so we dont get any crashing.
            switch (orderByChoice)
            {
                case "Name":
                    LINQResults = from employee in employeeIDOrder
                                  orderby employee._lastName
                                  select employee;
                    break;
                case "ID":
                    LINQResults = from employee in employeeIDOrder
                                  orderby employee._orderID
                                  select employee;
                    break;
                default:
                    LINQResults = from employee in employeeIDOrder
                                  orderby employee._lastName
                                  select employee;
                    break;
            }
            printSalesReport(orderByChoice);

            foreach (var employee in LINQResults)
            {
                Console.WriteLine(employee.ToString());
            }
        }
        /// <summary>
        /// This method will get the minimum sales data from the List we pass in.
        /// </summary>
        /// <param name="employeeIDOrder"></param>  List of all employees
        /// <returns></returns>
        private static clsSalesStatistics getStatisticSalesLow(List<clsEmployee> employeeIDOrder)
        {
            var games = (from employee in employeeIDOrder
                         select employee._gamesSales).Min();
            var dolls = (from employee in employeeIDOrder
                         select employee._dollsSales).Min();
            var build = (from employee in employeeIDOrder
                         select employee._buildingSales).Min();
            var model = (from employee in employeeIDOrder
                         select employee._modelSales).Min();

            return new clsSalesStatistics(games, dolls, build, model, "Low");
        }
        /// <summary>
        /// This method will get the Maximum sales data from the List we pass in.
        /// </summary>
        /// <param name="employeeIDOrder"></param>   List of all employees
        /// <returns></returns>
        private static clsSalesStatistics getStatisticsSalesHigh(List<clsEmployee> employeeIDOrder)
        {
            var games = (from employee in employeeIDOrder
                         select employee._gamesSales).Max();
            var dolls = (from employee in employeeIDOrder
                         select employee._dollsSales).Max();
            var build = (from employee in employeeIDOrder
                         select employee._buildingSales).Max();
            var model = (from employee in employeeIDOrder
                         select employee._modelSales).Max();

            return new clsSalesStatistics(games, dolls, build, model, "High");
        }
        /// <summary>
        /// This method will get the average sales data from the List we pass in.
        /// </summary>
        /// <param name="employeeIDOrder"></param>   List of all employees
        /// <returns></returns>
        private static clsSalesStatistics getStatisticsSalesAve(List<clsEmployee> employeeIDOrder)
        {
            var games = (from employee in employeeIDOrder
                         select employee._gamesSales).Average();
            var dolls = (from employee in employeeIDOrder
                         select employee._gamesSales).Average();
            var build = (from employee in employeeIDOrder
                         select employee._gamesSales).Average();
            var model = (from employee in employeeIDOrder
                         select employee._gamesSales).Average();

            return new clsSalesStatistics((float)games, (float)dolls, (float)build, (float)model, "Ave");
        }
        /// <summary>
        /// This method will get the minimum quantity of sales from the List we pass in.
        /// </summary>
        /// <param name="employeeIDOrder"></param>   List of all employees
        /// <returns></returns>
        private static clsSalesStatistics getStatisticsQuantLow(List<clsEmployee> employeeIDOrder)
        {
            var games = (from employee in employeeIDOrder
                         select employee._gamesQuantity).Min();
            var dolls = (from employee in employeeIDOrder
                         select employee._dollsQuantity).Min();
            var build = (from employee in employeeIDOrder
                         select employee._buildingQuantity).Min();
            var model = (from employee in employeeIDOrder
                         select employee._modelQuantity).Min();

            return new clsSalesStatistics(games, dolls, build, model, "Low");
        }
        /// <summary>
        /// This method will get the maximum quantity of sales from the List we pass in.
        /// </summary>
        /// <param name="employeeIDOrder"></param>   List of all employees
        /// <returns></returns>
        private static clsSalesStatistics getStatisticsQuantHigh(List<clsEmployee> employeeIDOrder)
        {
            var games = (from employee in employeeIDOrder
                         select employee._gamesQuantity).Max();
            var dolls = (from employee in employeeIDOrder
                         select employee._dollsQuantity).Max();
            var build = (from employee in employeeIDOrder
                         select employee._buildingQuantity).Max();
            var model = (from employee in employeeIDOrder
                         select employee._modelQuantity).Max();

            return new clsSalesStatistics(games, dolls, build, model, "High");
        }
        /// <summary>
        /// This method will get the average quantity of sales from the List we pass in.
        /// </summary>
        /// <param name="employeeIDOrder"></param>   List of all employees
        /// <returns></returns>
        private static clsSalesStatistics getStatisticsQuantAve(List<clsEmployee> employeeIDOrder)
        {
            double games = (from employee in employeeIDOrder
                            select employee._gamesQuantity).Average();
            double dolls = (from employee in employeeIDOrder
                            select employee._dollsQuantity).Average();
            double build = (from employee in employeeIDOrder
                            select employee._buildingQuantity).Average();
            double model = (from employee in employeeIDOrder
                            select employee._modelQuantity).Average();

            return new clsSalesStatistics((float)games, (float)dolls, (float)build, (float)model, "Ave");
        }
        /// <summary>
        /// This method will get the total sales from the List we pass in.
        /// </summary>
        /// <param name="employeeIDOrder"></param>  List of all employees
        /// <returns></returns>
        private static clsSalesStatistics getStatisticsTotals(List<clsEmployee> employeeIDOrder)
        {
            var games = (from employee in employeeIDOrder
                         select (employee._gamesSales)).Sum();
            var dolls = (from employee in employeeIDOrder
                         select (employee._dollsSales)).Sum();
            var build = (from employee in employeeIDOrder
                         select (employee._buildingSales)).Sum();
            var model = (from employee in employeeIDOrder
                         select (employee._modelSales)).Sum();

            return new clsSalesStatistics(games, dolls, build, model, "Total");
        }
        /// <summary>
        /// This method creates a List of first and last names of the employees that did above average
        /// sales.
        /// </summary>
        /// <param name="employeeIDOrder"></param>
        /// <returns></returns>
        private static IEnumerable<(string firstName, string lastName)> getAllAboveAverage(List<clsEmployee> employeeIDOrder)
        {
            var average = (from employee in employeeIDOrder
                                          select employee._totalSales).Average();

            var allAbove = (from employee in employeeIDOrder
                            where employee._totalSales > average
                         select (employee._lastName,employee._firstName));

            return allAbove;
        }
        /// <summary>
        /// This method will print the Sales Statistics Summary header.
        /// </summary>
        private static void printStatsReport()
        {
            string[] txtToCenter = { "--------------------------------------------------", "Sales Statistics Summary", "--------------------------------------------------" };
            int leftPad = 0;
            string prtinStringFormated;

            foreach (string currentTxtToCenter in txtToCenter)
            {
                leftPad = (95 - currentTxtToCenter.Length) / 2;
                leftPad += currentTxtToCenter.Length;

                prtinStringFormated = string.Format("{0, " + leftPad + "}", currentTxtToCenter);
                Console.WriteLine(prtinStringFormated);
            }       
        }
        /// <summary>
        /// This method will center the text sent in by getting the max console size
        /// that is also passed in.
        /// </summary>
        /// <param name="txtToCenter"></param>  text to print
        /// <param name="maxConsole"></param>   max console size
        private static void genericCenter(string txtToCenter, int maxConsole)
        {
            int leftPad = 0;
            string prtinStringFormated;

                leftPad = (maxConsole - txtToCenter.Length) / 2;
                leftPad += txtToCenter.Length;

                prtinStringFormated = string.Format("{0, " + leftPad + "}", txtToCenter);
                Console.WriteLine(prtinStringFormated);
        }
        /// <summary>
        /// Used for sales Statistics
        /// Prints the headings for the type of headings we report on.
        /// IE: games,dolls ect..
        /// </summary>
        private static void salesStatsPrintHeadings()
        {
            var SALESREPORTTABING = clsSalesStatistics.getstringFormat();
            Console.WriteLine(SALESREPORTTABING, " ", "Games", "Dolls", "Building", "Models");
            Console.WriteLine(SALESREPORTTABING, " ", "-----", "-----", "--------", "------");
        }
        /// <summary>
        /// This method is used to generate all Statistics ouput.
        /// Including the the statistics for average sales. 
        /// 
        /// Quantity & Sales & Totals & Above average
        /// </summary>
        /// <param name="employeeIDOrder"></param>  list of all sales data
        private static void generateStatisticsSummary(List<clsEmployee> employeeIDOrder)
        {
            
            printStatsReport();
            genericCenter("Quantity Statistics", STATSCONSOLESIZE);
            salesStatsPrintHeadings();

            //quant
            var quantLow = getStatisticsQuantLow(employeeIDOrder);
            var quantHigh = getStatisticsQuantHigh(employeeIDOrder);
            var quantAve = getStatisticsQuantAve(employeeIDOrder);

            Console.WriteLine(quantLow.ToStringNoCurrency());
            Console.WriteLine(quantAve.ToStringNoCurrency());
            Console.WriteLine(quantHigh.ToStringNoCurrency());

            Console.WriteLine("\n");

            genericCenter("Sales Statistics", STATSCONSOLESIZE);
            salesStatsPrintHeadings();

            //Sales
            var salesLow = getStatisticSalesLow(employeeIDOrder);
            var salesHigh = getStatisticsSalesHigh(employeeIDOrder);
            var salesAve = getStatisticsSalesAve(employeeIDOrder);

            Console.WriteLine(salesLow.ToString());
            Console.WriteLine(salesAve.ToString());
            Console.WriteLine(salesHigh.ToString());

            genericCenter("--------------------------------------------------------", STATSCONSOLESIZE);
            
            //Totals
            var salesTotals = getStatisticsTotals(employeeIDOrder);
            string outputFormat = salesTotals.ToStringNoCurrency() + "{0,-10:F2}";           
            Console.WriteLine(salesTotals.ToString() + $" ${(salesTotals._total),-10:F2}");

            Console.WriteLine();
            var printHolder =  getAllAboveAverage(employeeIDOrder);

            Console.WriteLine($"There are {printHolder.Count()} employees who sold above the average sales level.\n" +
                $"The names of the above average selling employees in alphabetical order are:");
            foreach (var item in printHolder)
            {
                Console.WriteLine(item.ToString());
            }
        }
        /// <summary>
        /// This ist he main driver of the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<clsEmployee> employeeIDOrder = getFile();

            salesReportBy(employeeIDOrder, "ID");

            Console.WriteLine("\n");        //Adds spaces between report generation

            salesReportBy(employeeIDOrder, "Name");

            generateStatisticsSummary(employeeIDOrder);

        }
    }
}
