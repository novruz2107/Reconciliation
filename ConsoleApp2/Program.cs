using CsvHelper;
using System.Globalization;

IList<Transaction> transactionAList;
IList<Transaction> transactionBList;

using (var reader = new StreamReader(@"C:\Users\aliyevnf\Desktop\Senedler\Transactions-A.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    transactionAList = csv.GetRecords<Transaction>().ToList();

    using(var reader2 = new StreamReader(@"C:\Users\aliyevnf\Desktop\Senedler\Transactions-B.csv"))
        using(var csv2 = new CsvReader(reader2, CultureInfo.InvariantCulture))
    {
        transactionBList = csv2.GetRecords<Transaction>().ToList();


        //First task
        var firstResult = transactionAList.ToHashSet();
        firstResult.ExceptWith(transactionBList);
        Console.WriteLine($"First task result count: {firstResult.Count}");

        //Second task
        var secondResult = transactionBList.ToHashSet();
        secondResult.ExceptWith(transactionAList);
        Console.WriteLine($"Second task result count: {secondResult.Count}");

        var thirdResult = transactionBList.ToHashSet();
        thirdResult.IntersectWith(transactionAList);
        Console.WriteLine($"Third task result count: {thirdResult.Count}");
    }
}

public class Transaction
{
    public string? Ticker { get; set; }
    public int TradeId { get; set; }
    public string? Counterparty { get; set; }
    public double Quantity { get; set; }
    public long CalcEstimate { get; set; }
    public string? TradeType { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Transaction transaction &&
               TradeId == transaction.TradeId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TradeId);
    }
}