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
                Title = titles[rnd.Next(0, titles.Length)],
                Company = company[rnd.Next(0, company.Length)],
                Description = description[rnd.Next(0, description.Length)],
                Location = location[rnd.Next(0, location.Length)],
                Salary = rnd.Next(5, 150) * 1000
            };
        }


        public string RandomReply()
        {
            Random rnd = new();

            string[] text =
                [
                "Rád zvedám těžké věci, kromě nálady v pondělí ráno.",
                "Miluji rozhodovat, hlavně o tom, kdy je pauza na kafe.",
                "Miluji auta, řízení i nečekané objížďky – adrenalin každý den.",
                "Vařím rychle, chutně a občas i bez požárního poplachu.",
                "Jsem rychlý jako blesk, ale s úsměvem jako slunce.",
                "Mop a já jsme nerozluční přátelé, i když někdy kloužu.",
                "Kóduji, dokud neztratím chuť na kávu i světlo.",
                "Uhelný mistr s plamenem v srdci, uhlí s láskou.",
                "Ovládám nůžky, hřebeny i tajné účesy na jednu noc.",
                "Miluji prodávat. Obzvlášť ty poslední sušenky v obchodě."
                ];

            return text[rnd.Next(10)];
        }





    }
}
