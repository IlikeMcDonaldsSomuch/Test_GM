using System;
using Microsoft.EntityFrameworkCore;
using Test_GreenMoon;
using Test_GreenMoon.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        #region question 1
        Random random = new Random();
        int previousNumber = 1000;
        int randomNumber = previousNumber;

        while (randomNumber != 0)
        {
            Console.WriteLine("Random number: " + randomNumber);
            randomNumber = random.Next(0, previousNumber);
            previousNumber = randomNumber;
        }
        Console.WriteLine("Generated zero. Exiting program.");
        #endregion

        #region question  3 - 5
        var data = new List<PersonModel>();
        using (var context = new EFContext())
        {
            data = Context(context);
        }

        Console.WriteLine(SearchByName(data, "Lee"));
        Console.WriteLine(SearchByName(data, "Kris"));

        Console.WriteLine(SearchByNameOrderByYongerToOlder(data, "Lee"));
        Console.WriteLine(SearchByNameOrderByYongerToOlder(data, "Kris"));
        #endregion
    }

    internal static List<PersonModel> Context(EFContext context)
    {
        var lee = new PersonModel {PersonId = 1 , Name = "Lee", DateOfBirth = new DateTime(1890, 5, 7) };
        var robert = new PersonModel {PersonId = 2 , Name = "Robert", DateOfBirth = new DateTime(1920, 7, 10), Parent = lee };
        var kris = new PersonModel { PersonId = 3,  Name = "Kris", DateOfBirth = new DateTime(1944, 12, 26), Parent = lee };

        context.People.AddRange(new List<PersonModel> { lee, robert, kris });

        context.SaveChanges();

        var response = context.People.ToList();
        return response;
       
    }


    internal static string SearchByNameOrderByYongerToOlder(List<PersonModel> query, string name)
    {
        string response = string.Empty;
        string notFound = "Not Found";
        if (query != null && query.Count() > 0 && !string.IsNullOrEmpty(name))
        {
            var searchByCondition = SearchByCondition(query,name);
            if (searchByCondition != null && searchByCondition.Count() > 0)
            {
                var orderBy = searchByCondition.OrderByDescending(o => o.DateOfBirth).ToList();
                response = $"{orderBy?.FirstOrDefault()?.Name ?? ""} is yogner than {orderBy?.LastOrDefault()?.Name ?? ""}";
            }
            else
            {
                response = notFound;
            }
        }
        else
        {
            response = notFound;
        }
        return response;
    }

    internal static List<PersonModel> SearchByCondition(List<PersonModel> query, string name)
    {
        var response = new List<PersonModel>();
        if (query != null && query.Count() > 0 && !string.IsNullOrEmpty(name))
        {
            var person = query.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
            if (person != null)
            {
                var siblings = query.Where(w => w?.Parent?.PersonId == person.PersonId && w.PersonId != person.PersonId);
                if (siblings.Any())
                {
                    response = siblings.ToList();
                }
            }
        }
        return response;
    }

    internal static string SearchByName(List<PersonModel> query, string name)
    {
        string response = string.Empty;
        string notFound = "Not Found";
        if (query != null && query.Count() > 0 && !string.IsNullOrEmpty(name))
        {
            var searchByCondition = SearchByCondition(query, name);
            if (searchByCondition != null && searchByCondition.Count() > 0)
            {
                var orderBy = searchByCondition.OrderBy(o => o.DateOfBirth).ToList();
                response = $"{orderBy?.FirstOrDefault()?.Name ?? ""} > {orderBy?.LastOrDefault()?.Name ?? ""}";
            }
            else
            {
                response = notFound;
            }
        }
        else
        {
            response = notFound;
        }
        return response;
    }
}

