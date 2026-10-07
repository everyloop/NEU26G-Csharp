
Player myPlayer = new Player();
myPlayer.Inventory.Add(new HealthPotion());
myPlayer.Inventory.Add(new Knife());


if (!myPlayer.Inventory.TryAdd(new Sword()))
{
    Console.WriteLine("Could not collect item.");
}

if (!myPlayer.Inventory.TryAdd(new Shield()))
{
    Console.WriteLine("Could not collect item.");
}

Console.WriteLine();



class Potion { };

// Can-do-relation (interface): En healthPotion kan plockas upp och kan konsumeras.
class HealthPotion : Potion, ICollectable, IConsumable
{
    public void Collect()
    {
        Console.WriteLine("You collected a health potion.");
    }

    public void Drop()
    {
        throw new NotImplementedException();
    }
};

abstract class Weapon : IEquiplable 
{
    //public abstract void Collect();

    public void Drop()
    {
        throw new NotImplementedException();
    }
};

class Sword : Weapon, ICollectable
{
    public void Collect()
    {
        Console.WriteLine("You collected a sword.");
    }
};

class Knife : Weapon, ICollectable
{
    public void Collect()
    {
        Console.WriteLine("You collected a knife.");
    }
};

class Armor : IEquiplable { };

class Shield : Armor { };

class Character { }

// Is-a-relationship (arv): En spelare är en karaktär.
class Player : Character
{
    public Player()
    {
        LeftHandEquipment = new Sword();
        RightHandEquipment = new Shield();
        Inventory = new Inventory();
    }

    // Has-a-relationship (komposition): En spelare har utrustning.
    public IEquiplable LeftHandEquipment { get; set; }
    public IEquiplable RightHandEquipment { get; set; }
    public Inventory Inventory { get; set; }

}

class Inventory
{
    private List<ICollectable> _items;

    public Inventory()
    {
        _items = new List<ICollectable>();
    }

    public void Add(ICollectable item)
    {
        _items.Add(item);
        item.Collect();
    }

    public bool TryAdd(object item)
    {
        if (item is ICollectable collectable)
        {
            _items.Add(collectable);
            collectable.Collect();
            return true;
        }
        
        return false;
    }

}

interface ICollectable
{
    public void Collect();

    public void Drop();
}

interface IConsumable { }

interface IEquiplable { }