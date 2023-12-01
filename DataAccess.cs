using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Khajiit
{

  public class DataAccess
  {

    private readonly KhajiitContext context;

    public DataAccess()
    {
      context = new KhajiitContext();
    }

    // READ/RETRIEVE
    public void GetAllItems()
    {
      var itemList = context.Items
          .Join(context.Item_Properties,
              item => item.Id,
              itemProp => itemProp.Item_id,
              (item, itemProp) => new
              {
                ItemId = item.Id,
                ItemName = item.Name,
                ItemType = item.Type,
                ItemStat = item.Stat,
                ItemRarity = item.Rarity,
                ItemPrice = item.Price,
                PropertyId = itemProp.Property_id
              })
          .Join(context.Properties,
              itemInfo => itemInfo.PropertyId,
              prop => prop.Id,
              (itemInfo, prop) => new
              {
                ItemId = itemInfo.ItemId,
                ItemName = itemInfo.ItemName,
                ItemType = itemInfo.ItemType,
                ItemStat = itemInfo.ItemStat,
                ItemRarity = itemInfo.ItemRarity,
                ItemPrice = itemInfo.ItemPrice,
                PropertyElement = prop.Element,
                PropertyBonus = prop.Bonus,
                UniqueAbility = prop.Unique_ability
              })
          .ToList();

      foreach (var itemInfo in itemList) // Display the results
      {

        Console.WriteLine("Item Name: " + itemInfo.ItemName);
        Console.WriteLine("Item Type: " + itemInfo.ItemType);

        if (itemInfo.ItemType == "Weapon") // If the item is a weapon... 
        {
          var weaponData = context.Weapons
          .Where(weapon => weapon.Item_id == itemInfo.ItemId)
          .FirstOrDefault();

          if (weaponData != null)
          {
            Console.WriteLine("Weapon Type: " + weaponData.Type);
            Console.WriteLine("Weapon Size: " + weaponData.Size);
          }
        }
        else if (itemInfo.ItemType == "Armor") // If the item is an armor...
        {
          var armorData = context.Armors
            .Where(armor => armor.Item_id == itemInfo.ItemId)
            .FirstOrDefault();

          if (armorData != null)
          {
            Console.WriteLine("Armor Slot: " + armorData.Slot);
          }
        }
        Console.WriteLine("Item Stat: " + itemInfo.ItemStat);
        Console.WriteLine("Item Rarity: " + itemInfo.ItemRarity);
        Console.WriteLine("Item Element: " + itemInfo.PropertyElement);
        Console.WriteLine("Item Bonus: " + itemInfo.PropertyBonus);
        Console.WriteLine("Item Unique Ability: " + itemInfo.UniqueAbility);
        Console.WriteLine("Price in Gold Coins: " + itemInfo.ItemPrice);
        Console.WriteLine();
      }
    }

    // CREATE
    public void CreateItem()
    {
      Console.WriteLine("Creating a new item...");

      // USER INPUT - ITEM NAME 
      Console.Write("Item Name: ");
      string? itemName = Console.ReadLine();

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // USER INPUT - ITEM TYPE 
      Console.WriteLine("Item Type (Select from the list):");
      GetTypesEnum();
      ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), Console.ReadLine() ?? throw new ArgumentNullException(nameof(itemType)));

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // USER INPUT - ITEM STAT VALUE 
      Console.Write("Item Stat (int): ");
      int itemStat = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(itemStat)));

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // USER INPUT - ITEM RARITY 
      Console.WriteLine("Item Rarity (Select from the list):");
      GetRarityEnum();
      Rarity itemRarity = (Rarity)Enum.Parse(typeof(Rarity), Console.ReadLine() ?? throw new ArgumentNullException(nameof(itemRarity)));

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // USER INPUT - ITEM ELEMENT 
      Console.WriteLine("Property Element (Select from the list):");
      GetElementEnum();
      Element propertyElement = (Element)Enum.Parse(typeof(Element), Console.ReadLine() ?? throw new ArgumentNullException(nameof(propertyElement)));

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // USER INPUT - ITEM BONUS 
      Console.Write("Property Bonus (int): ");
      int propertyBonus = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(propertyBonus)));

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // USER INPUT - ITEM ABILITY 
      Console.Write("Unique Ability: ");
      string? uniqueAbility = Console.ReadLine();

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // USER INPUT - ITEM PRICE 
      Console.Write("Price: ");
      float price = float.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(price)));

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // Create a new Item
      var newItem = new Item
      {
        Name = itemName,
        Type = itemType.ToString(),
        Stat = itemStat,
        Rarity = itemRarity.ToString(),
        Price = price
      };

      context.Items.Add(newItem);
      context.SaveChanges(); // Creates an item in the database to set an ID

      /* — — — — — — — — — — — — — — — — — — — — — — — — — — */

      // Check if the item type is Weapon or Armor and set properties
      if (itemType == ItemType.Weapon) // If weapon...
      {
        Console.WriteLine("Weapon Type (Select from the list):");
        GetWeaponTypeEnum();
        WeaponType weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), Console.ReadLine() ?? throw new ArgumentNullException(nameof(weaponType)));

        Console.WriteLine("Weapon Size (Select from the list):");
        GetWeaponSizeEnum();
        WeaponSize weaponSize = (WeaponSize)Enum.Parse(typeof(WeaponSize), Console.ReadLine() ?? throw new ArgumentNullException(nameof(weaponSize)));

        // ...creates a new weapon
        var newWeapon = new Weapon
        {
          Item_id = newItem.Id,
          Type = weaponType.ToString(),
          Size = weaponSize.ToString()
        };
        context.Weapons.Add(newWeapon);
      }
      else if (itemType == ItemType.Armor) // If armor...
      {
        Console.WriteLine("Armor Slot (Select from the list):");
        GetArmorSlotEnum();

        ArmorSlot armorSlot = (ArmorSlot)Enum.Parse(typeof(ArmorSlot), Console.ReadLine() ?? "");

        // ...creates a new armor
        var newArmor = new Armor
        {
          Item_id = newItem.Id,
          Slot = armorSlot.ToString()
        };
        context.Armors.Add(newArmor);
      }

      // Create a new property
      var newProperty = new Property
      {
        Element = propertyElement.ToString(),
        Bonus = propertyBonus,
        Unique_ability = uniqueAbility
      };
      context.Properties.Add(newProperty);
      context.SaveChanges(); // Creates a property in the database to set an ID

      // Creates a relationship between Item and Property
      var itemProperty = new Item_Properties
      {
        Item_id = newItem.Id,
        Property_id = newProperty.Id
      };
      context.Item_Properties.Add(itemProperty);

      context.SaveChanges(); // Saves the changes to the database

      Console.WriteLine("Item created successfully!");

    }

    // UPDATE MENU
    public void UpdateItem()
    {
      Console.WriteLine("Updating an item... Which item do you want to update? (Enter the number)");
      var items = context.Items.ToList();

      foreach (var item in items)
      {
        Console.WriteLine($"{item.Id} | {item.Name} — {item.Type}");
      }

      int itemId = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(itemId)));

      var itemToChange = context.Items.Find(itemId);

      if (itemToChange != null)
      {
        bool exit = false;
        while (!exit)
        {
          // Displays a menu with the options to update
          Console.WriteLine("What do you want to update?");
          Console.WriteLine("- Basics -");
          Console.WriteLine("1. Name");
          Console.WriteLine("2. Type");
          Console.WriteLine("3. Stat");
          Console.WriteLine("4. Rarity");
          Console.WriteLine("5. Price");
          if (itemToChange.Type == "Weapon")
          {
            Console.WriteLine("6. Weapon Type");
            Console.WriteLine("- Properties -");
          }
          else if (itemToChange.Type == "Armor")
          {
            Console.WriteLine("6. Armor Slot");
            Console.WriteLine("- Properties -");
          }
          else
          {
            Console.WriteLine("- Properties -");
          }
          Console.WriteLine("7. Property Element");
          Console.WriteLine("8. Property Bonus");
          Console.WriteLine("9. Unique Ability");
          Console.WriteLine("0. Exit");

          int choice = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(choice)));

          if (int.TryParse(choice.ToString(), out choice))
          {
            switch (choice)
            {
              case 1:
                Console.Write("New Name: ");
                string? newName = Console.ReadLine();
                UpdateItemName(itemToChange, newName);
                break;
              case 2:
                Console.WriteLine("New Type (Select from the list):");
                GetTypesEnum();
                ItemType newType = (ItemType)Enum.Parse(typeof(ItemType), Console.ReadLine() ?? throw new ArgumentNullException(nameof(newType)));
                UpdateItemType(itemToChange, newType);
                break;
              case 3:
                Console.Write("New Stat: ");
                int newStat = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(newStat)));
                UpdateItemStat(itemToChange, newStat);
                break;
              case 4:
                Console.WriteLine("New Rarity (Select from the list):");
                GetRarityEnum();
                Rarity newRarity = (Rarity)Enum.Parse(typeof(Rarity), Console.ReadLine() ?? throw new ArgumentNullException(nameof(newRarity)));
                UpdateItemRarity(itemToChange, newRarity);
                break;
              case 5:
                Console.Write("New Price: ");
                float newPrice = float.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(newPrice)));
                UpdateItemPrice(itemToChange, newPrice);
                break;
              case 6:
                if (itemToChange.Type != "Armor")
                {
                  Console.WriteLine("New Weapon Type (Select from the list):");
                  GetWeaponTypeEnum();
                  WeaponType newWeaponType = (WeaponType)Enum.Parse(typeof(WeaponType), Console.ReadLine() ?? throw new ArgumentNullException(nameof(newWeaponType)));
                  UpdateWeaponType(itemToChange, newWeaponType);

                  Console.WriteLine("New Weapon Size (Select from the list):");
                  GetWeaponSizeEnum();
                  WeaponSize newWeaponSize = (WeaponSize)Enum.Parse(typeof(WeaponSize), Console.ReadLine() ?? throw new ArgumentNullException(nameof(newWeaponSize)));
                  UpdateWeaponSize(itemToChange, newWeaponSize);
                }
                else if (itemToChange.Type != "Weapon")
                {
                  Console.WriteLine("New Armor Slot (Select from the list):");
                  GetArmorSlotEnum();
                  ArmorSlot newArmorSlot = (ArmorSlot)Enum.Parse(typeof(ArmorSlot), Console.ReadLine() ?? throw new ArgumentNullException(nameof(newArmorSlot)));
                  UpdateArmorSlot(itemToChange, newArmorSlot);
                }
                break;
              case 7:
                Console.WriteLine("New Property Element (Select from the list):");
                GetElementEnum();
                Element newElement = (Element)Enum.Parse(typeof(Element), Console.ReadLine() ?? throw new ArgumentNullException(nameof(newElement)));
                UpdatePropertyElement(itemToChange, newElement);
                break;
              case 8:
                Console.Write("New Property Bonus: ");
                int newBonus = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(newBonus)));
                UpdatePropertyBonus(itemToChange, newBonus);
                break;
              case 9:
                Console.Write("New Unique Ability: ");
                string? newAbility = Console.ReadLine();
                UpdateUniqueAbility(itemToChange, newAbility);
                break;
              case 0:
                exit = true;
                break;
              default:
                Console.WriteLine("You should probably choose one of the options...");
                break;
            }
          }
          else
          {
            Console.WriteLine("Wrong input, moron.");
          }
        }
        // Step 4: Save the changes to the database context
        context.SaveChanges();
        Console.WriteLine("Item updated successfully, you didn't break anything... yet!");

      }
    }

    // DELETE
    public void DeleteItem()
    {
      Console.WriteLine("Deleting an item... Which item do you want to delete? (Enter the number)");
      var items = context.Items.ToList();

      foreach (var item in items)
      {
        Console.WriteLine($"{item.Id} | {item.Name} — {item.Type}");
      }

      int itemId = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(itemId)));

      var itemToDelete = context.Items.Find(itemId);

      if (itemToDelete != null)
      {
        // Remove the item from the Items table
        context.Items.Remove(itemToDelete);

        // Save changes to trigger cascading deletes
        context.SaveChanges();
      }
    }

    // WAREHOUSE - Not implemented yet
    public void RefillWarehouse()
    {
      Console.WriteLine("Select an item to add to the warehouse:");

      // List all the items' names
      var items = context.Items.ToList();
      for (int i = 0; i < items.Count; i++)
      {
        Console.WriteLine($"{i + 1}. {items[i].Name}");
      }

      // Read the user input
      Console.Write("Enter the ID of the item: ");
      if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= items.Count)
      {
        Console.WriteLine("How many of this item do you want to add to the warehouse?");
        int quantity = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(quantity)));
        var selectedItemId = items[selectedIndex - 1].Id;

        // Check if the item already exists in the warehouse
        var existingItem = context.Warehouse.FirstOrDefault(w => w.Item_id == selectedItemId);
        if (existingItem != null)
        {
          // If it does, add the quantity to the existing quantity
          existingItem.Quantity += quantity;
          context.SaveChanges();
          Console.WriteLine("Item(s) added to the warehouse.");
          return;
        }
        else
        {
          // If it doesn't, create a new entry
          var warehouseEntry = new Warehouse
          {
            Item_id = selectedItemId,
            Quantity = quantity
          };

          context.Warehouse.Add(warehouseEntry);
          context.SaveChanges();
          Console.WriteLine("Item(s) added to the warehouse.");
        }
      }
      else
      {
        Console.WriteLine("Invalid choice.");
      }
    }

    public void ListWarehouseItems()
    {
      var warehouseItems = context.Warehouse
      .Join(context.Items,
          warehouse => warehouse.Item_id,
          item => item.Id,
          (warehouse, item) => new
          {
            ItemName = item.Name,
            ItemType = item.Type,
            ItemPrice = item.Price,
            WarehouseQuantity = warehouse.Quantity
          })
          .ToList();

      foreach (var item in warehouseItems)
      {
        Console.WriteLine($"{item.ItemName} — {item.ItemType} — {item.ItemPrice} Gold Coins — {item.WarehouseQuantity} in stock");
      }
    }

    // WH → VENDORS
    public void AddVendor()
    {
      Console.WriteLine("Create a vendor:");
      Console.Write("Vendor Name: ");
      string? vendorName = Console.ReadLine();
      Console.Write("Vendor Wallet: ");
      float vendorWallet = float.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(vendorWallet)));

      var newVendor = new Vendor
      {
        Name = vendorName,
        Wallet = vendorWallet
      };

      context.Vendors.Add(newVendor);
      context.SaveChanges();

      var vendorInv = new Vendor_Inventory
      {
        Vendor_id = newVendor.Id
      };

      context.Vendor_Inventory.Add(vendorInv);
      context.SaveChanges();
    }

    public void ListVendors()
    {
      var vendors = context.Vendors.ToList();
      foreach (var vendor in vendors)
      {
        Console.WriteLine($"{vendor.Id}. {vendor.Name} — {vendor.Wallet} Gold Coins");
      }
    }

    public void DeleteVendor()
    {
      Console.WriteLine("Deleting a vendor...\n Which vendor do you want to delete? (Enter the number)");
      var vendors = context.Vendors.ToList();

      foreach (var vendor in vendors)
      {
        Console.WriteLine($"{vendor.Id} | {vendor.Name} — {vendor.Wallet} Gold Coins");
      }

      int vendorId = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(vendorId)));

      var vendorToDelete = context.Vendors.Find(vendorId);

      if (vendorToDelete != null)
      {
        // Remove the vendor from the Vendors table
        context.Vendors.Remove(vendorToDelete);

        // Save changes to trigger cascading deletes
        context.SaveChanges();
      }
    }

    // Not functional yet
    public void RefillStock()
    {
      Console.WriteLine("Select a vendor whose stock to refill:");
      ListVendors();
      Console.Write("Enter the ID of the vendor: ");
      int vendorId = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(vendorId)));

      Console.WriteLine("Select an item to add to the vendor's inventory:");
      ListWarehouseItems();
      Console.Write("Enter the ID of the item: ");
      int itemId = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(itemId)));
      Console.Write("Enter the quantity: ");
      int quantity = int.Parse(Console.ReadLine() ?? throw new ArgumentNullException(nameof(quantity)));

      var vendorInv = new Vendor_Inventory
      {
        Vendor_id = vendorId,
        Item_id = itemId,
        Quantity = quantity
      };

      context.Vendor_Inventory.Add(vendorInv);
      context.SaveChanges();

      Console.WriteLine("Item added to the vendor's inventory.");
    }

    public void ListVendorItems(int vendorId)
    {
      var vendorItems = context.Vendor_Inventory
        .Join(context.Items,
          vendorInv => vendorInv.Item_id,
          item => item.Id,
          (vendorInv, item) => new
          {
            VendorId = vendorInv.Vendor_id,
            ItemName = item.Name,
            ItemType = item.Type,
            ItemPrice = item.Price,
            VendorQuantity = vendorInv.Quantity
          })
        .Where(vendorInv => vendorInv.VendorId == vendorId)
        .ToList();

      foreach (var item in vendorItems)
      {
        Console.WriteLine($"{item.ItemName} — {item.ItemType} — {item.ItemPrice} Gold Coins — {item.VendorQuantity} in stock");
      }
    }


    // — — — — — — — — — — — — — — — — — — — — — — GET ENUMS METHODS — — — — — — — — — — — — — — — — — — — 
    private static void GetRarityEnum()
    {
      foreach (var rarity in Enum.GetValues(typeof(Rarity)))
      {
        Console.WriteLine($"{(int)rarity}. {rarity}");
      }
    }

    private static void GetTypesEnum()
    {
      foreach (var type in Enum.GetValues(typeof(ItemType)))
      {
        Console.WriteLine($"{(int)type}. {type}");
      }
    }

    private static void GetElementEnum()
    {
      foreach (var element in Enum.GetValues(typeof(Element)))
      {
        Console.WriteLine($"{(int)element}. {element}");
      }
    }

    private static void GetWeaponSizeEnum()
    {
      foreach (var wSize in Enum.GetValues(typeof(WeaponSize)))
      {
        Console.WriteLine($"{(int)wSize}. {wSize}");
      }
    }

    private static void GetWeaponTypeEnum()
    {
      foreach (var wType in Enum.GetValues(typeof(WeaponType)))
      {
        Console.WriteLine($"{(int)wType}. {wType}");
      }
    }

    private static void GetArmorSlotEnum()
    {
      foreach (var aSlot in Enum.GetValues(typeof(ArmorSlot)))
      {
        Console.WriteLine($"{(int)aSlot}. {aSlot}");
      }
    }




    // — — — — — — — — — — — — — — — — — — — — — GET UPDATE METHODS — — — — — — — — — — — — — — — — — — — — 
    private void UpdateUniqueAbility(Item itemToChange, string? newAbility)
    {
      var prop = context.Properties
      .Where(p => p.Id == itemToChange.Id)
      .FirstOrDefault();

      if (prop != null)
      {
        prop.Unique_ability = newAbility;
      }
    }

    private void UpdatePropertyBonus(Item itemToChange, int newBonus)
    {
      var prop = context.Properties
      .Where(p => p.Id == itemToChange.Id)
      .FirstOrDefault();

      if (prop != null)
      {
        prop.Bonus = newBonus;
      }
    }

    private void UpdatePropertyElement(Item itemToChange, Element newElement)
    {
      var prop = context.Properties
      .Where(p => p.Id == itemToChange.Id)
      .FirstOrDefault();

      if (prop != null)
      {
        prop.Element = newElement.ToString();
      }
    }

    private void UpdateArmorSlot(Item itemToChange, ArmorSlot newArmorSlot)
    {
      var armor = context.Armors
      .Where(a => a.Item_id == itemToChange.Id)
      .FirstOrDefault();

      if (armor != null)
      {
        armor.Slot = newArmorSlot.ToString();
      }
    }

    private void UpdateWeaponType(Item itemToChange, WeaponType newWeaponType)
    {
      var weapon = context.Weapons
      .Where(w => w.Item_id == itemToChange.Id)
      .FirstOrDefault();

      if (weapon != null)
      {
        string weaponTypeName = GetEnumDisplayName(newWeaponType);

        if (!string.IsNullOrEmpty(weaponTypeName))
        {
          weapon.Type = weaponTypeName;
          context.SaveChanges(); // Save changes to the database
        }
        else
        {
          Console.WriteLine("Invalid weapon type.");
        }
      }
    }

    private void UpdateWeaponSize(Item itemToChange, WeaponSize newWeaponSize)
    {
      var weapon = context.Weapons
      .Where(w => w.Item_id == itemToChange.Id)
      .FirstOrDefault();

      if (weapon != null)
      {
        string weaponSizeName = GetEnumDisplayName(newWeaponSize);

        if (!string.IsNullOrEmpty(weaponSizeName))
        {
          weapon.Size = weaponSizeName;
          context.SaveChanges(); // Save changes to the database
        }
        else
        {
          Console.WriteLine("Invalid weapon size.");
        }
      }
    }

    public static string GetEnumDisplayName<TEnum>(TEnum enumValue) where TEnum : Enum
    {
      var displayAttribute = typeof(TEnum)
        .GetField(enumValue.ToString()!)
        ?.GetCustomAttributes(typeof(DisplayAttribute), false)
        .SingleOrDefault() as DisplayAttribute;

      return displayAttribute?.Name ?? "";
    }

    private static void UpdateItemPrice(Item itemToChange, float newPrice)
    {
      itemToChange.Price = newPrice;
    }

    private static void UpdateItemRarity(Item itemToChange, Rarity newRarity)
    {
      itemToChange.Rarity = newRarity.ToString();
    }

    private static void UpdateItemStat(Item itemToChange, int newStat)
    {
      itemToChange.Stat = newStat;
    }

    private static void UpdateItemType(Item itemToChange, ItemType newType)
    {
      itemToChange.Type = newType.ToString();
    }

    private static void UpdateItemName(Item itemToChange, string? newName)
    {
      itemToChange.Name = newName;
    }
  }

}