namespace ConNyelvvizsgak
{
    internal class Program
    {
        static List<Nyelv> nyelvek;

        //1.
        private static List<Nyelv> AdatokBetoltese(string sikeres, string sikertelen)
        {
            List<Nyelv> nyelvek = new();
            List<string> sikeresSorok = File.ReadAllLines(sikeres).Skip(1).ToList();
            List<string> sikertelenSorok = File.ReadAllLines(sikertelen).Skip(1).ToList();
            for (int i = 0; i < sikeresSorok.Count; i++)
            {
                string[] sikeresMezok = sikeresSorok[i].Split(';');
                string[] sikertelenMezok = sikertelenSorok[i].Split(';');

                nyelvek.Add(new Nyelv(
                    sikeresMezok[0],
                    sikeresMezok.Skip(1).Select(x => Convert.ToInt32(x)).ToList(),
                    sikertelenMezok.Skip(1).Select(x => Convert.ToInt32(x)).ToList()
                ));
            }
            return nyelvek;
        }

        static void Main(string[] args)
        {
            nyelvek = AdatokBetoltese("datas\\sikeres.csv", "datas\\sikertelen.csv");


            //2.
            Console.WriteLine("2. feladat: Legnépszerűbb nyelvek:");
            nyelvek
                .OrderByDescending(x => x.EddigiOsszesViszgakSzama)
                .Take(3)
                .ToList()
                .ForEach(x => Console.WriteLine("\t" + x.Nev));

            //3.
            Console.Write("3. feladat:\n\tVizsgálandó év: ");
            int ev = Convert.ToInt32(Console.ReadLine());
            if (ev < 2009 || ev > 2018)
            {
                return;
            }

            //4.
            Console.WriteLine("4. feladat:");

            //A feladatban nagy probléma az, amikor egy évben egyáltalán nem volt vizsga.
            //Arra milyen értéket adjon vissza? A rendezés miatt nem mindegy!
            //Ezeket az eseteket ki kell szűrni!
            Nyelv? v1 = null, v2 = null, v3 = null, v4 = null, v5 = null;


            v1 = nyelvek.Where(x => x.VizsgakSzamaAdottEvben(ev) != 0)
                        .MinBy(x => x.SikeressegiAranyAdottEvben(ev));  //.NET6-tól !

            v2 = nyelvek.Where(x => x.VizsgakSzamaAdottEvben(ev) != 0)
                        .MaxBy(x => x.SikertelensegiAranyAdottEvben(ev));  //.NET6-tól !

            v3 = nyelvek.Where(x => x.VizsgakSzamaAdottEvben(ev) != 0)
                        .OrderBy(x => x.SikeressegiAranyAdottEvben(ev))
                        .First();

            v4 = nyelvek.OrderBy(x => x.SikeressegiAranyAdottEvben(ev))
                        .First(x => x.SikeressegiAranyAdottEvben(ev) > 0);

            //Azért nem kell szűrni, mivel amikor nincs vizsga (-1) az érték és a rendezés miatt az előre kerül.
            v5 = nyelvek.OrderBy(x => x.SikertelensegiAranyAdottEvben(ev))
                        .Last();


            Nyelv legsikertelenebbNyelvAdottEvben = v5;

            Console.WriteLine($"\t{ev}-ben {legsikertelenebbNyelvAdottEvben.Nev} nyelvből a sikertelen vizsgák aránya: {legsikertelenebbNyelvAdottEvben.SikertelensegiAranyAdottEvben(ev):f2}");


            if (v1 == v2 && v2 == v3 && v3 == v4 && v4 == v5)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tMinden megoldási út ugyanazt a megoldást adta! Az eredmények hivatkozási címei megegyeznek.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }


            //5.
            Console.WriteLine("5. feladat:");

            if (nyelvek.Count(x => x.VizsgakSzamaAdottEvben(ev) > 0) == nyelvek.Count)
            {
                Console.WriteLine("\tMinden nyelvből volt vizsgázó!");
            }
            else
            {
                nyelvek.Where(x => x.VizsgakSzamaAdottEvben(ev) == 0)
                    .ToList()
                    .ForEach(x => Console.WriteLine("\t" + x.Nev));
            }
            //6.
            File.WriteAllLines("osszesites.csv",
                nyelvek.Select(x => $"{x.Nev};{x.EddigiOsszesViszgakSzama};" +
                $"{Math.Round(x.SikeressegiAranyTeljesIdoszakban, 2)}%")
                );
        }
    }
}