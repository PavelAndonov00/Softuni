using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DungeonMaster
{
    //FIELDS\\
    private List<Character> characters;
    private Stack<Item> items;
    private int lastSurvivorRound;
    private CharacterFactory characterFactory;
    private ItemFactory itemFactory;

    //CONSTRUCTORS\\
    public DungeonMaster()
    {
        this.characters = new List<Character>();
        this.items = new Stack<Item>();
        this.lastSurvivorRound = 0;
    }

    //PROPERTIES\\

    //METHODS\\
    public string JoinParty(string[] args)
    {
        var faction = args[0];
        var characterType = args[1];
        var characterName = args[2];
        this.characters.Add(characterFactory.CreateCharacter(faction, characterType, characterName));

        return characterName + " joined the party!";
    }

    public string AddItemToPool(string[] args)
    {
        var itemName = args[0];
        this.items.Push(itemFactory.CreateItem(itemName));

        return itemName + " added to pool.";
    }

    public string PickUpItem(string[] args)
    {
        var characterName = args[0];
        var currentCharacter = this.characters.FirstOrDefault(c => c.Name == characterName);
        if (currentCharacter == null)
        {
            throw new ArgumentException($"Character {characterName} not found!");
        }

        if (this.items.Count == 0)
        {
            throw new InvalidOperationException("No items left in pool!");
        }

        var item = this.items.Pop();
        currentCharacter.Bag.AddItem(item);

        return characterName + " picked up " + item.GetType().Name + "!";
    }

    public string UseItem(string[] args)
    {
        var characterName = args[0];
        var currentCharacter = this.characters.FirstOrDefault(c => c.Name == characterName);
        if (currentCharacter == null)
        {
            throw new ArgumentException($"Character {characterName} not found!");
        }

        var itemName = args[1];

        var currentItem = currentCharacter.Bag.GetItem(itemName);

        currentCharacter.UseItem(currentItem);

        return characterName + " used " + itemName + ".";
    }

    public string UseItemOn(string[] args)
    {
        var giverName = args[0];
        var giver = this.characters.FirstOrDefault(c => c.Name == giverName);
        if (giver == null)
        {
            throw new ArgumentException($"Character {giverName} not found!");
        }

        var receiverName = args[1];
        var receiver = this.characters.FirstOrDefault(c => c.Name == receiverName);
        if (receiver == null)
        {
            throw new ArgumentException($"Character {receiverName} not found!");
        }

        var itemName = args[2];
        var currentItem = giver.Bag.GetItem(itemName);

        giver.UseItemOn(currentItem, receiver);

        return giverName + " used " + itemName + " on " + receiverName + ".";
    }

    public string GiveCharacterItem(string[] args)
    {
        var giverName = args[0];
        var giver = this.characters.FirstOrDefault(c => c.Name == giverName);
        if (giver == null)
        {
            throw new ArgumentException($"Character {giverName} not found!");
        }

        var receiverName = args[1];
        var receiver = this.characters.FirstOrDefault(c => c.Name == receiverName);
        if (receiver == null)
        {
            throw new ArgumentException($"Character {receiverName} not found!");
        }

        var itemName = args[2];
        var currentItem = giver.Bag.GetItem(itemName);

        giver.GiveCharacterItem(currentItem, receiver);

        return giverName + " gave " + receiverName + " " + itemName + ".";
    }

    public string GetStats()
    {
        var sb = new StringBuilder();

        this.characters
            .OrderByDescending(c => c.IsAlive == true)
            .ThenByDescending(c => c.Health)
            .ToList()
            .ForEach(c => sb.AppendLine(c.ToString()));

        return sb.ToString().Trim();
    }

    public string Attack(string[] args)
    {
        var attackerName = args[0];
        var attacker = this.characters.FirstOrDefault(c => c.Name == attackerName);
        if (attacker == null)
        {
            throw new ArgumentException($"Character {attackerName} not found!");
        }

        var receiverName = args[1];
        var receiver = this.characters.FirstOrDefault(c => c.Name == receiverName);
        if (receiver == null)
        {
            throw new ArgumentException($"Character {receiverName} not found!");
        }

        if (!(attacker is IAttackable))
        {
            throw new ArgumentException($"{attackerName} cannot attack!");
        }

        var sb = new StringBuilder();

        var iattackble = (IAttackable)attacker;

        iattackble.Attack(receiver);

        sb.AppendLine($"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points! " +
        $"{receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");

        if (!receiver.IsAlive)
        {
            sb.AppendLine($"{receiverName} is dead!");
        }

        return sb.ToString().Trim();
    }

    public string Heal(string[] args)
    {
        var healerName = args[0];
        var healer = this.characters.FirstOrDefault(c => c.Name == healerName);
        if (healer == null)
        {
            throw new ArgumentException($"Character {healerName} not found!");
        }

        var healingReceiverName = args[1];
        var healingReceiver = this.characters.FirstOrDefault(c => c.Name == healingReceiverName);
        if (healingReceiver == null)
        {
            throw new ArgumentException($"Character {healingReceiverName} not found!");
        }

        if (!(healer is IHealable))
        {
            throw new ArgumentException($"{healerName} cannot heal!");
        }

        var ihealalbe = (IHealable)healer;
        ihealalbe.Heal(healingReceiver);

        return $"{healerName} heals {healingReceiverName} for {healer.AbilityPoints}! {healingReceiver.Name} has {healingReceiver.Health} health now!";
    }

    public string EndTurn(string[] args)
    {
        var aliveCharacter = this.characters.Where(c => c.IsAlive == true).ToList();
        if (aliveCharacter.Count <= 1)
        {
            this.lastSurvivorRound++;
        }

        var sb = new StringBuilder();
        for (int i = 0; i < aliveCharacter.Count; i++)
        {
            var current = aliveCharacter[i];
            var healthBeforeRest = current.Health;
            current.Rest();
            sb.AppendLine($"{current.Name} rests ({healthBeforeRest} => {current.Health})");
        }

        return sb.ToString().Trim();
    }

    public bool IsGameOver()
    {
        return this.lastSurvivorRound > 1;
    }
}
