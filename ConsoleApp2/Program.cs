using CsvHelper;
using System.Globalization;

using (StreamReader readerA = new(@"C:\Users\aliyevnf\Desktop\Senedler\Transactions-A.csv"),
       readerB = new(@"C:\Users\aliyevnf\Desktop\Senedler\Transactions-B.csv"))
using (CsvReader csvA = new(readerA, CultureInfo.InvariantCulture),
       csvB = new(readerB, CultureInfo.InvariantCulture))
{
    IList<Transaction> transactionAList = csvA.GetRecords<Transaction>().ToList();

    IList<Transaction> transactionBList = csvB.GetRecords<Transaction>().ToList();

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

public class Transaction
{
    public string? Ticker { get; set; }
    public int TradeId { get; init; }
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