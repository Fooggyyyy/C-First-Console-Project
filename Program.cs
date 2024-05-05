using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//struct:
/* 
 1 Level: Middle Menu
 2 Level: BackPack, Shop, Cart
 3 Level: 
        Cart:
            4 Level:
                Buy (-> Check Card)
                Back in Shop <-
        Shop:
            4 Level:
                Buy (-> Check Card)
                in Cart <-
        BackPack:
            //You can just see, wath you Buy
            (-> Middle Menu || Cart || Shop)
5 Level:
    Check Card:
        (-> Up Balance || Middle Menu)

    Up Balance
        (-> Midde Menu)




<- Function Without Link
(-> NAME) - Link in NAME



struct shop_object
{
    global string name_object;
    global in price;
}



Card:
    1. Name:
         -Only English
         -Has is "Space"
    2. Id number:
         -Three-digit-int number;
    3. Time:
         Format:
             Two-digit-int1/Two-digit-int2
         0 < Two-digit-int1 < 12
         24 < Two-digit-int1 < 99
    4. Number:
        -Format:
            Four-dint1_Four-dint2_Four-dint3_Four-dig4



*/

namespace ConsoleApp2
{
    struct Product
    {
        public string Name_product;
        public int price;
    }

  
    public class Program
    {
        // All Menu
        static void Basic_Menu(int Balance)
        {
            Console.WriteLine("1 - BackPack");
            Console.WriteLine("2 - Shop");
            Console.WriteLine("3 - Cart");
            Console.WriteLine("4 - Up your balance");

            Console.WriteLine("\nYour balance: " + Balance);
        }

        static void Shopmenu()
        {
            Console.WriteLine("1 - Check all product");
            Console.WriteLine("2 - Push product in Cart");
        }

        static void Cartmenu()
        {
            Console.WriteLine("1 - Check all product in Cart");
            Console.WriteLine("2 - Buy all product");
        }

        // Balance

        static int Up_Balanse(ref int Balance)
        {
            Console.WriteLine("Print your name on cart: ");
            string str = Console.ReadLine();
            char[] name = str.ToCharArray();
            bool Check = false;
            for(int i = 0; i < name.Length; i++)
                if (name[i] == ' ')
                    Check = true;
            if(!Check)
            {
                Console.WriteLine("Invalid Name!!!");
                return 0;
            }

            Console.WriteLine("Print CVC: ");
            int CVC;
            if (!int.TryParse(Console.ReadLine(), out CVC) || CVC < 100 || CVC > 999)
            {
                Console.WriteLine("ERROR CVC");
                return 0 ;
            }

            Console.WriteLine("Print Number(Without Space):");
            long number;
            if (!long.TryParse(Console.ReadLine(), out number) || number < 1000_0000_0000_0000 || number > 9999_9999_9999_9999)
            {
                Console.WriteLine("ERROR Number");
                return 0;
            }

            Console.WriteLine("Print on how upp your balance: ");
            if (!int.TryParse(Console.ReadLine(), out Balance))
            {
                Console.WriteLine("ERROR");
                return 0;
            }
            return Balance;
        }
      

        //Shop Function
        static void Go_To_Cart(in int choice_product, ref int Size_Cart,  in Product[] productShop, ref Product[] productCart)
        {
            productCart[choice_product].Name_product = productShop[Size_Cart].Name_product;
            productCart[choice_product].price = productShop[Size_Cart].price;

            Size_Cart++;
        }


        //Cart Function

        static void Buy(in int choice, ref int Balance, ref int Size_Backpack, ref int Size_Cart, in Product[] productCart, ref Product[] productBackpack)
        {
            if (Balance >= productBackpack[Size_Backpack].price)
            {
                productBackpack[Size_Backpack].Name_product = productCart[choice].Name_product;
                productBackpack[Size_Backpack].price = productCart[choice].price;

                Balance -= productBackpack[Size_Backpack].price;

                Size_Backpack++;
                Size_Cart--;
                productCart[choice] = new Product();
            }

            else
            {
                Console.WriteLine("You Havent Money!!!");
                return;
            }

        }
        //Check
        static void Check(in Product[] product, int size)
        {
            if(size == 0)
            {
                Console.WriteLine("Clear");
                return;
            }
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Product name: " + product[i].Name_product);
                Console.WriteLine("Product price: " + product[i].price);
                Console.WriteLine("\n");
            }

        }

        static void Main(string[] args)
        {
            Product[] Shop = new Product[10];
            Product[] Backpack = new Product[10];
            Product[] Cart = new Product[10];
            for (int i = 0; i < Shop.Length; i++)
                {
                    Shop[i] = new Product {Name_product = "Product" + $"{i+1}", price = (i + 1)*100 };
                }

            int Balance = 1000;

            int choice, Shop_Choice, Cart_Choice, Shop_product_choice, Cart_product_choice;

            int backpackSize = 0, Shopsize = 10, CartSize = 0;
            do
            {
                Basic_Menu(Balance);

                Console.Write("-> ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Its not int-value!!");
                    return;
                }

                switch(choice)
                {
                    case 1:
                        Check(Backpack, backpackSize);
                        break;
                    case 2:
                        Shopmenu();

                        Console.Write("-> ");
                        if (!int.TryParse(Console.ReadLine(), out Shop_Choice))
                        {
                            Console.WriteLine("Its not int-value!!");
                            return;
                        }

                        switch(Shop_Choice)
                        {
                            case 1:
                                Check(Shop, Shopsize);
                                break;
                            case 2:
                                Console.WriteLine("Print number product, who you push in cart");
                                if (!int.TryParse(Console.ReadLine(), out Shop_product_choice))
                                {
                                    Console.WriteLine("Its not int-value!!");
                                    return;
                                }
                                --Shop_product_choice;
                                Go_To_Cart(Shop_product_choice, ref CartSize, Shop, ref Cart);
                                break;
                            default:
                                Console.WriteLine("You are Input Invalid Number!!!");
                                break;
                        }
                        break;
                    case 3:
                        Cartmenu();

                        Console.Write("-> ");
                        if (!int.TryParse(Console.ReadLine(), out Cart_Choice))
                        {
                            Console.WriteLine("Its not int-value!!");
                            return;
                        }

                        switch (Cart_Choice)
                        {
                            case 1:
                                Check(Cart, CartSize);
                                break;
                            case 2:
                                Console.WriteLine("Print number product, who you push in Backpack");
                                if (!int.TryParse(Console.ReadLine(), out Cart_product_choice))
                                {
                                    Console.WriteLine("Its not int-value!!");
                                    return;
                                }
                                --Cart_product_choice;
                                Buy(Cart_product_choice, ref Balance, ref backpackSize, ref CartSize, Cart, ref Backpack);
                                break;
                            default:
                                Console.WriteLine("You are Input Invalid Number!!!");
                                break;
                        }
                        break;

                    case 4:
                        Up_Balanse(ref Balance);
                        break;
                    default:
                        Console.WriteLine("You are Input Invalid Number!!!");
                        break;
                }
                    

            } while (choice != 0 );
        }
    }

   

}
