using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Bag
{
    public long Capacity { get; set; }
    public long MaxCapacity { get; set; }
    public List<Gem> Gems { get; set; }
    public List<Gold> Gold { get; set; }
    public List<Cash> Cash { get; set; }

    public Bag()
    {
        Gems = new List<Gem>();
        Gold = new List<Gold>();
        Cash = new List<Cash>();
    }

    public Bag(long maxCapacity) : this()
    {
        MaxCapacity = maxCapacity;
    }

    public long GetGemsAmount()
    {
        return Gems.Sum(g => g.Value);
    }

    public long GetGoldAmount()
    {
        return Gold.Sum(g => g.Value);
    }

    public long GetCashAmount()
    {
        return Cash.Sum(c => c.Value);
    }

    public void AddCash(string name, long value)
    {
        if (Capacity + value <= MaxCapacity && GetGemsAmount() >= GetCashAmount() + value)
        {
            if (!Cash.Any(c => c.Name == name))
            {
                Cash.Add(new Cash(name));
            }

            var current = Cash.FirstOrDefault(c => c.Name == name);
            current.Value += value;
            Capacity += value;
        }
    }

    public void AddGem(string name, long value)
    {
        if (Capacity + value <= MaxCapacity && GetGoldAmount() >= GetGemsAmount() + value)
        {
            if (!Gems.Any(g => g.Name == name))
            {
                Gems.Add(new Gem(name));
            }

            var current = Gems.FirstOrDefault(g => g.Name == name);
            current.Value += value;
            Capacity += value;
        }
    }

    public void AddGold(string name, long value)
    {
        if (Capacity + value <= MaxCapacity)
        {
            if (!Gold.Any(g => g.Name == name))
            {
                Gold.Add(new Gold(name));
            }

            var current = Gold.FirstOrDefault(g => g.Name == name);
            current.Value += value;
            Capacity += value;
        }
    }

    public List<List<string>> ArrangeItems()
    {
        var dic = new Dictionary<List<string>, long>();

        var goldAmount = GetGoldAmount();
        var goldPrefix = new List<string>();
        goldPrefix.Add("<Gold> $" + goldAmount);
        Gold = Gold.OrderByDescending(g => g.Name).ThenBy(g => g.Value).ToList();
        var gold = goldPrefix.Concat(Gold.Select(g => $"##{g.Name} - {g.Value}").ToList()).ToList();
        if (gold.Count > 1)
        {
            dic.Add(gold, goldAmount);
        }

        var gemsAmount = GetGemsAmount();
        var gemsPrefix = new List<string>();
        gemsPrefix.Add("<Gem> $" + gemsAmount);
        Gems = Gems.OrderByDescending(g => g.Name).ThenBy(g => g.Value).ToList();
        var gems = gemsPrefix.Concat(Gems.Select(g => $"##{g.Name} - {g.Value}").ToList()).ToList();
        if (gems.Count > 1)
        {
            dic.Add(gems, gemsAmount);
        }

        var cashAmount = GetCashAmount();
        var cashPrefix = new List<string>();
        cashPrefix.Add("<Cash> $" + cashAmount);
        Cash = Cash.OrderByDescending(c => c.Name).ThenBy(c => c.Value).ToList();
        var cash = cashPrefix.Concat(Cash.Select(g => $"##{g.Name} - {g.Value}").ToList()).ToList();
        if (cash.Count > 1)
        {
            dic.Add(cash, cashAmount);
        }

        return dic
            .OrderByDescending(v => v.Value)
            .Select(e => e.Key)
            .ToList();
    }

    public override string ToString()
    {
        var sorted = String.Join(Environment.NewLine, ArrangeItems()
            .Select(e => String.Join(Environment.NewLine, e))
            .ToList());

        return sorted;
    }
}

