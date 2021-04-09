using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Bogus;
using CsvHelper;
using System.Globalization;

namespace fake_users
{
    public class User
    {
        public string Full_name { get; set; }
        public string Full_address { get; set; }
        public string Phone { get; set; }
        public User(string full_name, string FullAddress, string Phone)
        {
            this.Full_name = full_name;
            this.Full_address = FullAddress;
            this.Phone = Phone;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            args = new string[] { "ru_RU", "100000"};
            List<string> lengs = new List<string> { "ru_RU", "en_EN", "uk_UK" };
            if (args.Count() == 2 && Convert.ToInt32(args[1]) > 0 && lengs.Contains(args[0]))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                string leng = args[0];
                int count_user = Convert.ToInt32(args[1]);

                StreamWriter writer = new StreamWriter(new BufferedStream(new FileStream("f.csv", FileMode.Create)), Encoding.UTF8);
                using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.GetCultureInfo("ru-RU")))
                {
                    csvWriter.WriteField("FullName");
                    csvWriter.WriteField("FullAddress");
                    csvWriter.WriteField("Phone");
                    csvWriter.NextRecord();

                    Faker faker = new Faker(leng.Substring(0, leng.IndexOf('_')));
                    List<User> users = new List<User>();
                    for (int i = count_user; i > 0; i -= 2250)
                    {
                        stopwatch.Start();
                        int decriment = 2250;
                        if (i < 2250)
                        {
                            decriment = i;
                        }

                        users.Clear();
                        Parallel.For(0, decriment, j =>
                        {
                            users.Add(new User(faker.Name.FullName(), faker.Address.FullAddress(), faker.Phone.PhoneNumber()));
                        });
                        foreach (User user in users)
                        {
                            csvWriter.WriteRecord(user);
                            csvWriter.NextRecord();
                        }
                        stopwatch.Stop();

                        Parallel.ForEach(users, user =>
                        {
                            Console.WriteLine($"{user.Full_name} {user.Full_address} {user.Phone}");
                        });
                    }
                }
                Console.WriteLine("Generation time (without output to the terminal): " + stopwatch.Elapsed);
                Console.ReadKey();
            }

        }
    }
}