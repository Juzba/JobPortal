using JobPortal.Models;

namespace JobPortal.Code
{
    public class Components
    {
        public Job RandomJob()
        {
            Random rnd = new();

            string[] titles = ["Dělník", "Programátor", "Řidič", "Zedník", "Ředitel", "Doktor", "Právník", "Prodavač bot"];
            string[] company = ["Komerční banka", "Jednota", "Mc Donald", "České dráhy", "Kaufland", "Pilzner", "Hilti", "Crop"];
            string[] description = ["Budete řídit projekty a spolupracovat s týmem na výsledcích.", "Navrhnete grafiku pro naše kampaně a webové stránky.", "Budete vyvíjet a testovat nový software pro klienty.", "Staráte se o zákazníky a řešíte jejich požadavky.", "selhání diety povinné, výčitky zakázány.", "Šéfuješ vlastnímu týmu a občas i firemnímu karaoke.", "Pomáháš zákazníkům a děláš jim den o něco lepší.", "Zachraňuješ počítače kolegům i před tím nejdivnějším problémem."];
            string[] location = ["Praha", "Olomouc", "Ostrava", "Brno", "Pardubice", "Přerov", "Kladno", "Hranice"];

            return new Job()
            {
                Title = titles[rnd.Next(0,titles.Length)],
                Company = company[rnd.Next(0, company.Length)],
                Description = description[rnd.Next(0, description.Length)],
                Location = location[rnd.Next(0, location.Length)],
                Salary = rnd.Next(5, 150) * 1000
            };
        }








    }
}
