using System;
using System.Data;
using MySql;
using MySql.Data;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

namespace Khajiit
{
  public class Khajiit
  {
    static void Main(string[] args)
    {
      var dataAccess = new DataAccess();

      // Main menu
      Console.WriteLine("Khajiit has wares if you have coins...");
      Console.WriteLine("- - - - - - - - - - - - - - - - - - - -");
      Console.WriteLine("Who are you?");
      Console.WriteLine("1. Vendor");
      Console.WriteLine("2. Customer");
      Console.WriteLine("3. Warehouse Manager");

      int roleChoice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

      // Not necessary anymore :
      switch (roleChoice)
      {
        case 1:
          // Vendor Menu
          VendorMenu(dataAccess);
          break;
        case 2:
          // Customer Menu
          CustomerMenu(dataAccess);
          break;
        case 3:
          // Warehouse Manager Menu
          WarehouseManagerMenu(dataAccess);
          break;
        default:
          Console.WriteLine("Who?");
          break;
      }
    }

    static void VendorMenu(DataAccess da)
    {
      Console.WriteLine("Vendor Menu:");
      Console.WriteLine("1. List your wares");
      Console.WriteLine("2. Add wares to your shop");

      int choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

      switch (choice)
      {
        case 1:
          Console.WriteLine("Who are you ?");
          da.ListVendors();
          int vendorId = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
          Console.WriteLine("This is what you have in stock:");
          da.ListVendorItems(vendorId);
          break;
        case 2:
          Console.WriteLine("- - Refill your stock - -");
          da.RefillStock();
          break;
        default:
          Console.WriteLine("Khajiit can't make your coffee.");
          break;
      }
    }

    // Not necessary anymore :
    static void CustomerMenu(DataAccess da)
    {
      Console.WriteLine("Customer Menu:");
      Console.WriteLine("1. Browse and purchase wares");
      Console.WriteLine("2. List your purchased wares");

      int choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
    }

    static void WarehouseManagerMenu(DataAccess da)
    {
      Console.WriteLine("|------------------------|");
      Console.WriteLine("| Warehouse Manager Menu |");
      Console.WriteLine("|------------------------|");
      Console.WriteLine("1. Manage warehouse wares");
      Console.WriteLine("2. Manage detailers");
      // Console.WriteLine("3. View transaction history"); Not necessary anymore

      int choice = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

      switch (choice)
      {
        case 1:
          Console.WriteLine("|----------------------|");
          Console.WriteLine("| Warehouse Management |");
          Console.WriteLine("|----------------------|");
          Console.WriteLine("1. Show all the possible wares?");
          Console.WriteLine("2. Create a new piece of ware");
          Console.WriteLine("3. Update an existing ware");
          Console.WriteLine("4. Remove a ware from the warehouse");
          // Console.WriteLine("5. Refill the warehouse"); Not implemented yet

          int choice2 = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

          switch (choice2)
          {
            case 1:
              da.GetAllItems();
              break;
            case 2:
              da.CreateItem();
              break;
            case 3:
              da.UpdateItem();
              break;
            case 4:
              da.DeleteItem();
              break;
            case 5:
              da.RefillWarehouse();
              break;
            default:
              Console.WriteLine("Khajiit can't count that high.");
              break;
          }
          break;
        case 2:
          Console.WriteLine("|----------------------|");
          Console.WriteLine("| Detailers Management |");
          Console.WriteLine("|----------------------|");
          Console.WriteLine("1. Show all the detailers?");
          Console.WriteLine("2. Create a new detailer");
          Console.WriteLine("3. Update an existing detailer"); // Not functional yet
          Console.WriteLine("4. Remove a detailer"); // Not functional yet

          int choice3 = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

          switch (choice3)
          {
            case 1:
              da.ListVendors();
              break;
            case 2:
              da.AddVendor();
              break;
            case 3:
              da.DeleteVendor();
              break;
            default:
              Console.WriteLine("Khajiit can't count that high.");
              break;
          }
          break;
        default:
          Console.WriteLine("I can't make your coffee.");
          break;
      }
    }

  }
}