using Exercicio1Linq.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Exercicio1Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\temp\ExercicioLinq.txt";

            try 
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    List<Product> products = new List<Product>();

                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        string name = line[0];
                        double price = double.Parse(line[1], CultureInfo.InvariantCulture);
                        products.Add(new Product(name, price));
                    }

                    Console.WriteLine("Lista de pordutos:");
                    foreach (var item in products)
                    {
                        Console.WriteLine(item);
                    }

                    Console.Write("\nMédida de preço:");
                    var averagePrice = products.Select(p => p.Price).Average();
                    Console.WriteLine(averagePrice.ToString("C2"));
                    Console.WriteLine();

                    var minPrice = products
                        .Where(p => p.Price < averagePrice)
                        .OrderByDescending(p => p.Name)
                        .Select(p => p.Name);
                    foreach (var item in minPrice)
                    {
                        Console.WriteLine(item);
                    }
                    Console.ReadLine();
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine($"Erro ao Ler o aquivo: {ex.Message}");
            }   
        }
    }
}
