using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqJoin1
{
    /// <summary>
    /// http://linq.jp.net/groupjoin/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GroupJoinSample1();
            GroupJoinSample2();
            GroupJoinSample3();
            GroupJoinSample4();
            GroupJoinSample5();
        }

        public static void GroupJoinSample1()
        {
            var todaySlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 200},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300}
            };
            var monthSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 1000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 2000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 3000}
            };
            var yearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 10000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 20000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 30000}
            };
            var lastYearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300000}
            };

            var query = todaySlip
                .GroupJoin(
                    monthSlip,
                    today => new { today.SummeryCD, today.ClsCD, today.CD },
                    month => new { month.SummeryCD, month.ClsCD, month.CD },
                    (today, month) => new { today, month }
                )
                .GroupJoin(
                    yearSlip,
                    joined1 => new { joined1.today.SummeryCD, joined1.today.ClsCD, joined1.today.CD },
                    year => new { year.SummeryCD, year.ClsCD, year.CD },
                    (joined1, year) => new { joined1, year }
                )
                .GroupJoin(
                    lastYearSlip,
                    joined2 => new { joined2.joined1.today.SummeryCD, joined2.joined1.today.ClsCD, joined2.joined1.today.CD },
                    lastYearSlip => new { lastYearSlip.SummeryCD, lastYearSlip.ClsCD, lastYearSlip.CD },
                    (joined2, lastYearSlip) => new { joined2, lastYearSlip }
                );
            Console.WriteLine("sample1");
            foreach (var item in query)
            {
                Console.WriteLine("todayPrice:" + item.joined2.joined1.today.Price.ToString());
                Console.WriteLine("monthPrice:" + item.joined2.joined1.month.FirstOrDefault()?.Price.ToString() ?? "0");
                Console.WriteLine("yearPrice:" + item.joined2.year.FirstOrDefault()?.Price.ToString() ?? "0");
                Console.WriteLine("lastYearPrice:" + item.lastYearSlip.FirstOrDefault()?.Price.ToString() ?? "0");
            }
        }

        public static void GroupJoinSample2()
        {
            var todaySlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 200},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300}
            };
            var monthSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 1000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 2000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 3000}
            };
            var yearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 10000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 20000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 30000}
            };
            var lastYearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300000}
            };

            var query = todaySlip
                .GroupJoin(
                    monthSlip,
                    today => new { today.SummeryCD, today.ClsCD, today.CD },
                    month => new { month.SummeryCD, month.ClsCD, month.CD },
                    (today, month) => new { today, month }
                )
                .SelectMany(
                    result => result.month.DefaultIfEmpty(),
                    (joined1, month) => new { joined1.today, month }
                )
                .GroupJoin(
                    yearSlip,
                    joined1 => new { joined1.today.SummeryCD, joined1.today.ClsCD, joined1.today.CD },
                    year => new { year.SummeryCD, year.ClsCD, year.CD },
                    (joined1, year) => new { joined1, year }
                )
                .SelectMany(
                    result => result.year.DefaultIfEmpty(),
                    (joined2, year) => new { joined2.joined1, year }
                )
                .GroupJoin(
                    lastYearSlip,
                    joined2 => new { joined2.joined1.today.SummeryCD, joined2.joined1.today.ClsCD, joined2.joined1.today.CD },
                    lastYear => new { lastYear.SummeryCD, lastYear.ClsCD, lastYear.CD },
                    (joined2, lastYear) => new { joined2, lastYear }
                )
                .SelectMany(
                    result => result.lastYear.DefaultIfEmpty(),
                    (joined3, lastYear) => new { joined3, lastYear }
                );
            Console.WriteLine("");
            Console.WriteLine("sample2");
            foreach (var item in query)
            {
                Console.WriteLine("todayPrice:" + item.joined3.joined2.joined1.today.Price.ToString());
                Console.WriteLine("monthPrice:" + item.joined3.joined2.joined1.month.Price.ToString());
                Console.WriteLine("yearPrice:" + item.joined3.joined2.year.Price.ToString());
                Console.WriteLine("lastYearPrice:" + item.lastYear?.Price.ToString());
            }
        }

        public static void GroupJoinSample3()
        {
            var todaySlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 200},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300}
            };
            var monthSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 1000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 2000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 3000}
            };
            var yearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 10000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 20000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 30000}
            };
            var lastYearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300000}
            };

            var query = todaySlip
                .GroupJoin(
                    monthSlip,
                    today => new { today.SummeryCD, today.ClsCD, today.CD },
                    month => new { month.SummeryCD, month.ClsCD, month.CD },
                    (today, month) => new { today, month }
                )
                .GroupJoin(
                    yearSlip,
                    joined1 => new { joined1.today.SummeryCD, joined1.today.ClsCD, joined1.today.CD },
                    year => new { year.SummeryCD, year.ClsCD, year.CD },
                    (joined1, year) => new { joined1, year }
                )
                .GroupJoin(
                    lastYearSlip,
                    joined2 => new { joined2.joined1.today.SummeryCD, joined2.joined1.today.ClsCD, joined2.joined1.today.CD },
                    lastYearSlip => new { lastYearSlip.SummeryCD, lastYearSlip.ClsCD, lastYearSlip.CD },
                    (joined2, lastYear) => new { joined2, lastYear }
                ).SelectMany(
                    result => result.lastYear.DefaultIfEmpty(),
                    (joined3, lastYear) => {
                        var today = joined3.joined2.joined1.today.Price;
                        var month = joined3.joined2.joined1.month.FirstOrDefault()?.Price ?? 0;
                        var year = joined3.joined2.year.FirstOrDefault()?.Price ?? 0;
                        return new
                        {
                            todayPrice = today,
                            monthPrice = month,
                            yearPrice = year,
                            lastYearPrice = lastYear?.Price ?? 0,
                            diff = year- lastYear?.Price ?? 0
                        };
                   }
                );
            Console.WriteLine("");
            Console.WriteLine("sample3");
            foreach (var item in query)
            {
                Console.WriteLine("todayPrice:" + item.todayPrice);
                Console.WriteLine("monthPrice:" + item.monthPrice);
                Console.WriteLine("yearPrice:" + item.yearPrice);
                Console.WriteLine("lastYearPrice:" + item.lastYearPrice);
                Console.WriteLine("diff:" + item.diff);
            }
        }

        public static void GroupJoinSample4()
        {
            var todaySlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 200}
            };
            var monthSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 1000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 2000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 3000}
            };
            var yearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 10000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 20000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 30000}
            };
            var lastYearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300000}
            };

            var query = todaySlip
                .GroupJoin(
                    monthSlip,
                    today => new { today.SummeryCD, today.ClsCD, today.CD },
                    month => new { month.SummeryCD, month.ClsCD, month.CD },
                    (today, month) => new { today, month }
                )
                .GroupJoin(
                    yearSlip,
                    joined1 => new { joined1.today.SummeryCD, joined1.today.ClsCD, joined1.today.CD },
                    year => new { year.SummeryCD, year.ClsCD, year.CD },
                    (joined1, year) => new { joined1, year }
                )
                .GroupJoin(
                    lastYearSlip,
                    joined2 => new { joined2.joined1.today.SummeryCD, joined2.joined1.today.ClsCD, joined2.joined1.today.CD },
                    lastYearSlip => new { lastYearSlip.SummeryCD, lastYearSlip.ClsCD, lastYearSlip.CD },
                    (joined2, lastYear) => new { joined2, lastYear }
                ).Select(
                    joined3 => {
                        var today = joined3.joined2.joined1.today.Price;
                        var month = joined3.joined2.joined1.month.FirstOrDefault()?.Price ?? 0;
                        var year = joined3.joined2.year.FirstOrDefault()?.Price ?? 0;
                        var lastYear = joined3.lastYear.FirstOrDefault()?.Price ?? 0;
                        return new
                        {
                            todayPrice = today,
                            monthPrice = month,
                            yearPrice = year,
                            lastYearPrice = lastYear,
                            diff = year - lastYear
                        };
                    }
                );
            Console.WriteLine("");
            Console.WriteLine("sample4");
            foreach (var item in query)
            {
                Console.WriteLine("todayPrice:" + item.todayPrice);
                Console.WriteLine("monthPrice:" + item.monthPrice);
                Console.WriteLine("yearPrice:" + item.yearPrice);
                Console.WriteLine("lastYearPrice:" + item.lastYearPrice);
                Console.WriteLine("diff:" + item.diff);
            }
        }

        public static void GroupJoinSample5()
        {
            var todaySlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 200}
            };
            var monthSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 1000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 2000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 3000}
            };
            var yearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 10000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 20,Price = 20000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 30000}
            };
            var lastYearSlip = new List<SlipData>()
            {
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 10,Price = 100000},
                new SlipData(){SummeryCD = 1,ClsCD = 0,CD = 30,Price = 300000}
            };

            var query = todaySlip
                .GroupJoin(
                    monthSlip,
                    today => new { today.SummeryCD, today.ClsCD, today.CD },
                    month => new { month.SummeryCD, month.ClsCD, month.CD },
                    (today, month) => new { today, month }
                )
                .GroupJoin(
                    yearSlip,
                    joined1 => new { joined1.today.SummeryCD, joined1.today.ClsCD, joined1.today.CD },
                    year => new { year.SummeryCD, year.ClsCD, year.CD },
                    (joined1, year) => new { joined1, year }
                )
                .GroupJoin(
                    lastYearSlip,
                    joined2 => new { joined2.joined1.today.SummeryCD, joined2.joined1.today.ClsCD, joined2.joined1.today.CD },
                    lastYearSlip => new { lastYearSlip.SummeryCD, lastYearSlip.ClsCD, lastYearSlip.CD },
                    (joined2, lastYear) => {
                        var todayPrice = joined2.joined1.today.Price;
                        var monthPrice = joined2.joined1.month.FirstOrDefault()?.Price ?? 0;
                        var yearPrice = joined2.year.FirstOrDefault()?.Price ?? 0;
                        var lastYearPrice = lastYear.FirstOrDefault()?.Price ?? 0;
                        return new
                        {
                            todayPrice = todayPrice,
                            monthPrice = monthPrice,
                            yearPrice = yearPrice,
                            lastYearPrice = lastYearPrice,
                            diff = yearPrice - lastYearPrice
                        };
                    }
                );
            Console.WriteLine("");
            Console.WriteLine("sample5");
            foreach (var item in query)
            {
                Console.WriteLine("todayPrice:" + item.todayPrice);
                Console.WriteLine("monthPrice:" + item.monthPrice);
                Console.WriteLine("yearPrice:" + item.yearPrice);
                Console.WriteLine("lastYearPrice:" + item.lastYearPrice);
                Console.WriteLine("diff:" + item.diff);
            }
        }



        public static void GroupJoinEx1()
        {
            Person magnus = new Person { Name = "Hedlund, Magnus" };
            Person terry = new Person { Name = "Adams, Terry" };
            Person charlotte = new Person { Name = "Weiss, Charlotte" };

            Pet barley = new Pet { Name = "Barley", Owner = terry };
            Pet boots = new Pet { Name = "Boots", Owner = terry };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
            Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

            List<Person> people = new List<Person> { magnus, terry, charlotte };
            List<Pet> pets = new List<Pet> { barley, boots, whiskers, daisy };

            // Create a list where each element is an anonymous 
            // type that contains a person's name and 
            // a collection of names of the pets they own.
            var query = people
                .GroupJoin(
                    pets,
                    person => person,
                    pet => pet.Owner,
                    (person, petCollection) =>
                        new
                        {
                            OwnerName = person.Name,
                            Pets = petCollection.Select(pet => pet.Name)
                        }
                );

            foreach (var obj in query)
            {
                // Output the owner's name.
                Console.WriteLine("{0}:", obj.OwnerName);
                // Output each of the owner's pet's names.
                foreach (string pet in obj.Pets)
                {
                    Console.WriteLine("  {0}", pet);
                }
            }
        }
    }

    public class SlipData
    {
        public int SummeryCD { get; set; }
        public int ClsCD { get; set; }
        public int CD { get; set; }
        public decimal Price { get; set; }
    }

    class Person
    {
        public string Name { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }
}
