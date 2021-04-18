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
            
            List<string> lengs = new List<string> { "ru_RU", "en_EN", "uk_UK" };
            if (args.Count() == 2 && Convert.ToInt32(args[1]) > 0 && lengs.Contains(args[0]))
            {
string leng = args[0];
                int count_user = Convert.ToInt32(args[1]);

                StreamWriter writer = new StreamWriter(new BufferedStream(new FileStream("OutPut.csv", FileMode.Create)), Encoding.UTF8);
                using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.GetCultureInfo("ru-RU")))
                {
                    csvWriter.WriteField("FullName");
                    csvWriter.WriteField("FullAddress");
                    csvWriter.WriteField("Phone");
                    csvWriter.NextRecord();

                    Faker faker = new Faker(leng.Substring(0, leng.IndexOf('_')));
                
                    for (int i = 0; i < count_user; i++)
                    {
                        stopwatch.Start();
User user = new User(faker.Name.FullName(), faker.Address.FullAddress(), faker.Phone.PhoneNumber());
                        
                        csvWriter.WriteRecord(user);
                        csvWriter.NextRecord();
                    
Console.WriteLine($"{user.Full_name} {user.Full_address} {user.Phone}");
                    }
                }
            }
        }
    }
}
