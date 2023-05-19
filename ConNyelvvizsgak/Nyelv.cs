namespace ConNyelvvizsgak
{
    public class Nyelv
    {
        const int BAZIS_EV = 2009;
        string nev;
        List<int> sikeresVizsgak;
        List<int> sikertelenVizsgak;

        public Nyelv(string nyelv, List<int> sikeresVizsgak, List<int> sikertelenVizsgak)
        {
            this.nev = nyelv;
            this.sikeresVizsgak = sikeresVizsgak;
            this.sikertelenVizsgak = sikertelenVizsgak;
        }

        public string Nev { get => nev; }
        public List<int> SikeresVizsgak { get => sikeresVizsgak; }
        public List<int> SikertelenVizsgak { get => sikertelenVizsgak; }

        //Teljes időszakra
        public int EddigiOsszesViszgakSzama { get => sikeresVizsgak.Sum() + sikertelenVizsgak.Sum(); }
        public double SikeressegiAranyTeljesIdoszakban { get => 100d * sikeresVizsgak.Sum() / EddigiOsszesViszgakSzama; }
        public double SikertelensegiAranyTeljesIdoszakban { get => 100 - SikeressegiAranyTeljesIdoszakban; }

        //Csak egy évre
        public int VizsgakSzamaAdottEvben(int evIndex)
        {
            return sikeresVizsgak[evIndex - BAZIS_EV] + sikertelenVizsgak[evIndex - BAZIS_EV];
        }
        public double SikeressegiAranyAdottEvben(int evIndex)
        {
            int osszesVizsga = VizsgakSzamaAdottEvben(evIndex);
            return osszesVizsga == 0 ? -1 : sikeresVizsgak[evIndex - BAZIS_EV] * 100d / osszesVizsga;
        }
        public double SikertelensegiAranyAdottEvben(int evIndex)
        {
            return VizsgakSzamaAdottEvben(evIndex) != 0 ? 100 - SikeressegiAranyAdottEvben(evIndex) : -1;
        }
    }

    //Miben más?
    //A konstruktorban több mindent előre kiszámolunk. Igaz több a tárigény, de gyorsabb a futtatás!

    /*
public class Nyelv
{
    const int BAZIS_EV = 2009;
    string nev;
    List<int> sikeresVizsgak;
    List<int> sikertelenVizsgak;

    //Ha a CPU terhelést akarom csökkenteni, akkor:
    List<int> osszesVizsga;
    int osszesVizsgaSzama;
    int osszesSikeresVizsgaSzama;


    public Nyelv(string nyelv, List<int> sikeresVizsgak, List<int> sikertelenVizsgak)
    {
        this.nev = nyelv;
        this.sikeresVizsgak = sikeresVizsgak;
        this.sikertelenVizsgak = sikertelenVizsgak;

        //CPU miatt, dönthetünk úgy, hogy egyszer számoljuk ki és inkább tároljuk
        //Ez akkor jó csak, ha az adatok nem változnak!
        osszesVizsga = sikertelenVizsgak.Zip(sikeresVizsgak, (elsobol, masodikbol) => elsobol + masodikbol).ToList();
        this.osszesSikeresVizsgaSzama = sikeresVizsgak.Sum();
        this.osszesVizsgaSzama = osszesSikeresVizsgaSzama + sikertelenVizsgak.Sum();

    }

    public string Nev { get => nev; }
    public List<int> SikeresVizsgak { get => sikeresVizsgak; }
    public List<int> SikertelenVizsgak { get => sikertelenVizsgak; }

    //Teljes időszakra
    public int EddigiOsszesViszgakSzama { get => this.osszesVizsgaSzama; }

    public double SikeressegiAranyTeljesIdoszakban { get => 100d * osszesSikeresVizsgaSzama / this.osszesVizsgaSzama; }

    public double SikertelensegiAranyTeljesIdoszakban { get => 100 - SikeressegiAranyTeljesIdoszakban; }

    //Csak egy évre
    public int VizsgakSzamaAdottEvben(int evIndex)
    {
        return osszesVizsga[evIndex - BAZIS_EV];
    }

    public double SikeressegiAranyAdottEvben(int evIndex)
    {
        int osszesVizsga = this.osszesVizsga[evIndex - BAZIS_EV];
        return osszesVizsga == 0 ? -1 : sikeresVizsgak[evIndex - BAZIS_EV] * 100d / osszesVizsga;
    }

    public double SikertelensegiAranyAdottEvben(int evIndex)
    {
        return this.osszesVizsga[evIndex - BAZIS_EV] != 0 ? 100 - SikeressegiAranyAdottEvben(evIndex) : -1;
    }
}
*/

}
