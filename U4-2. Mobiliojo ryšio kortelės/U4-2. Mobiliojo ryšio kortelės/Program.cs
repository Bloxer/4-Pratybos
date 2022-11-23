using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace U4_2.Mobiliojo_ryšio_kortelės
{
    /// <summary>
    /// Kortelės klasė
    /// </summary>
    class Kortele
    {
        string pavadinimas; //Tinklo pavadinimas
        double suma, //Pradinė suma
            tarSav, //Skambučių tarifas savame tinkle
            tarKit, //Skambučių tarifas kitame tinkle
            SMStarSav, //Žinučių tarifas savame tinkle
            SMStarKit; //Žinučių tarifas kitame tinkle
        /// <summary>
        /// Pradiniai kortelės duomenys
        /// </summary>
        public Kortele()
        {
            pavadinimas = "";
            suma = 0;
            tarSav = 0;
            tarKit = 0;
            SMStarSav = 0;
            SMStarKit = 0;
        }
        /// <summary>
        /// Kortelės įdėjimo į masyvą metodas
        /// </summary>
        /// <param name="pavadinimas"></param>
        /// <param name="suma"></param>
        /// <param name="tarSav"></param>
        /// <param name="tarKit"></param>
        /// <param name="SMStarSav"></param>
        /// <param name="SMStarKit"></param>
        public Kortele(string pavadinimas, double suma, double tarSav,
            double tarKit, double SMStarSav, double SMStarKit)
        {
            this.pavadinimas = pavadinimas;
            this.suma = suma;
            this.tarSav = tarSav;
            this.tarKit = tarKit;
            this.SMStarSav = SMStarSav;
            this.SMStarKit = SMStarKit;
        }
        /// <summary>
        /// Užklotas ToString() metodas
        /// </summary>
        /// <returns>Grąžina pakeistą string</returns>
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, -12}   {1,-6:f2}             {2,-6:f2}" +
                "             {3,-6:f2}              {4,-6:f2}     " +
                "          {5,-6:f2}",
                                   pavadinimas, suma, tarSav, tarKit, SMStarSav,
                                   SMStarKit);
            return eilute;
        }
        /// <summary>
        /// Metodas, kuris grąžina tinklo pavadiniimą
        /// </summary>
        /// <returns></returns>
        public string ImtPavadinima() { return pavadinimas; }
        /// <summary>
        /// Metodas, kuris grąžina pradinę sumą kortelėje
        /// </summary>
        /// <returns></returns>
        public double ImtSuma() { return suma; }
        /// <summary>
        /// Metodas, kuris grąžina skambučių savame tinkle kainą
        /// </summary>
        /// <returns></returns>
        public double ImtTarSav() { return tarSav; }
        /// <summary>
        /// Metodas, kuris grąžina skambučių kitame tinkle kainą
        /// </summary>
        /// <returns></returns>
        public double ImtTarKit() { return tarKit; }
        /// <summary>
        /// Metodas, kuris grąžina SMS savame tinkle kainą
        /// </summary>
        /// <returns></returns>
        public double ImtSMStarSav() { return SMStarSav; }
        /// <summary>
        /// Metodas, kuris grąžina SMS kitame tinkle kainą
        /// </summary>
        /// <returns></returns>
        public double ImtSMStarKit() { return SMStarKit; }
        /// <summary>
        /// <=palyginimo operatorius
        /// </summary>
        /// <param name="kortele1"></param>
        /// <param name="kortele2"></param>
        /// <returns></returns>
        public static bool operator <=(Kortele kortele1, Kortele kortele2)
        {
            int abecele = String.Compare(kortele1.pavadinimas,
                                             kortele2.pavadinimas,
                                             StringComparison.CurrentCulture);

            return (kortele1.suma < kortele2.suma ||
                    ((kortele1.suma == kortele2.suma) && (abecele > 0)));
        }
        /// <summary>
        /// >=palyginimo operatorius
        /// </summary>
        /// <param name="kortele1"></param>
        /// <param name="kortele2"></param>
        /// <returns></returns>
        public static bool operator >=(Kortele kortele1, Kortele kortele2)
        {
            int abecele = String.Compare(kortele1.pavadinimas,
                                             kortele2.pavadinimas,
                                             StringComparison.CurrentCulture);

            return (kortele1.suma > kortele2.suma ||
                    ((kortele1.suma == kortele2.suma) && (abecele < 0)));
        }


    }
    /// <summary>
    /// Kortelių konteineris
    /// </summary>
    class KortKon
    {
        const int CMaxi = 100;
        private Kortele[] kortele;
        private int kiek;
        /// <summary>
        /// Pradinės konteinerio reikšmės
        /// </summary>
        public KortKon()
        {
            kiek = 0;
            kortele = new Kortele[CMaxi];
        }
        /// <summary>
        /// Metodas, kuris paima informaciją iš kortelės klasės
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Kortele Imti(int i) { return kortele[i]; }
        /// <summary>
        /// Metodas, kuris grąžina įrašų kiekį masyve
        /// </summary>
        /// <returns></returns>
        public int Imti() { return kiek; }
        /// <summary>
        /// Metodas, kuris į konteinerį įdeda naują įrašą
        /// </summary>
        /// <param name="ob"></param>
        public void Deti(Kortele ob) { kortele[kiek++] = ob; }
        /// <summary>
        /// Metodas, kuris surūšiuoja konteinerį abecelės tvarka ir
        /// pradinę sumą mažėjant
        /// </summary>
        public void Rusiavimas()
        {
            for (int i = 0; i < kiek - 1; i++)
            {
                Kortele kitas = kortele[i];
                int im = i;
                for (int j = i + 1; j < kiek; j++)
                {
                    if (kortele[j] <= kitas)
                    {
                        kitas = kortele[j];
                        im = j;
                    }
                }
                kortele[im] = kortele[i];
                kortele[i] = kitas;
            }
        }


    }
    internal class Program
    {
        const string CFd = "..\\..\\Duomenys.txt"; //Nuorodą į duomenų failą
        const string CFr = "..\\..\\Rezultatai.txt"; //Nuoroda į rezultatų failą
        static void Main(string[] args)
        {
            KortKon kortKon = new KortKon(); //Sukuriamas naujas konteineris

            Skaityti(ref kortKon, CFd); //Skaitoma informacija iš failo ir įrašoma
                                        //į konteinerį

            using (var fr = File.CreateText(CFr)) { fr.WriteLine(""); }
                if (kortKon.Imti() > 0) //Spausdinama lentelė, jeigu konteineryje
            {                       //yra įrašų
                Spausdinti(CFr ,kortKon);
            }
            else
            {
                using (var fr = File.AppendText(CFr))
                {
                    fr.WriteLine("Nėra mobilaus ryšio operatorių");
                }
            }
            //Spausdinami tinklo operatoriai, kurių sms tarifai į kitus tinklus yra
            //mažiausi
            SpausdintiMaz(CFr, kortKon, kortKon.Imti(IeskotiSMSMaz(kortKon)).ImtSMStarKit());

            KortKon naujosKorteles = new KortKon(); //Sukuriamas naujas konteineris

            //Iš seno konteinerio įvedama informacija atitinkanti užduoties sąlygas
            KorteliuAtrinkimas(kortKon, ref naujosKorteles);
            //Tikrinama ar konteineriai vienodi
            if (kortKon.Imti().Equals(naujosKorteles.Imti()))
            {
                Console.WriteLine("Visi mobilaus ryšio operatoriai leidžia nemokamai" +
                    " skambinti ir rašyti žinutes į savo tinklą.");
            }

            naujosKorteles.Rusiavimas(); //Pertvarkomas naujas konteineris

            if (naujosKorteles.Imti() > 0) //Spausdinama lentelė, jeigu konteineryje
            {                              //į konteinerį
                Spausdinti(CFr, naujosKorteles);
            }
            else
            {
                using (var fr = File.AppendText(CFr))
                {
                    fr.WriteLine("Nėra tinkamų mobilaus ryšio operatorių");
                }
            }
        }
        /// <summary>
        /// Skaitymo metodas
        /// </summary>
        /// <param name="kortKon">Kortelių konteineris</param>
        /// <param name="fv">Duomenų failo nuoroda</param>
        static void Skaityti(ref KortKon kortKon, string fv)
        {
            string pavadinimas;
            double suma,
                tarSav,
                tarKit,
                SMStarSav,
                SMStarKit;

            string[] eilutes = File.ReadAllLines(fv, Encoding.GetEncoding(1257));

            using (StreamReader reader = new StreamReader(fv))
            {
                foreach (string eilute in eilutes)
                {
                    string[] parts = eilute.Split(';');
                    pavadinimas = parts[0].Trim();
                    suma = double.Parse(parts[1].Trim());
                    tarSav = double.Parse(parts[2].Trim());
                    tarKit = double.Parse(parts[3].Trim());
                    SMStarSav = double.Parse(parts[4].Trim());
                    SMStarKit = double.Parse(parts[5].Trim());

                    Kortele kort = new Kortele(pavadinimas, suma, tarSav, tarKit,
                        SMStarSav, SMStarKit);
                    kortKon.Deti(kort);
                }
            }
        }
        /// <summary>
        /// Lentelės spausdinimo metodas
        /// </summary>
        /// <param name="kortKon">Kortelių konteineris</param>
        static void Spausdinti(string fv, KortKon kortKon)
        {
            string virsus = "                         Informacija apie MRO \r\n" +
                            "--------------------------------------------------" +
                            "--------------------------------------------------" +
                            "--------" +
                            "\r\nTinklas  /  Pradinė Suma  /  Lokalus Tarifas /" +
                            " Išorinis Tarifas / Lokalus SMS Tarifas /" +
                            " Išorinis SMS Tarifas\r\n" +
                            "--------------------------------------------------" +
                            "--------------------------------------------------" +
                            "--------";

            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(virsus);

                for (int i = 0; i < kortKon.Imti(); i++)
                    fr.WriteLine("{0}", kortKon.Imti(i).ToString());

                fr.WriteLine("--------------------------------------------------" +
                                "--------------------------------------------------" +
                                "--------\n");
            }
        }
        /// <summary>
        /// Metodas, kuris atrenka tinklo operatorius su
        /// mažiausiais SMS tarifais į kitus tarifus
        /// </summary>
        /// <param name="kortKon">Kortelių konteineris</param>
        /// <returns>Grąžinama pozicija konteineryje</returns>
        static int IeskotiSMSMaz(KortKon kortKon)
        {
            double maziausias = 1;
            int mazInt = 0;

            for (int i = 0; i < kortKon.Imti(); i++)
            {
                if (kortKon.Imti(i).ImtSMStarKit() < maziausias &&
                    kortKon.Imti(i).ImtSMStarKit() != 0)
                {
                    maziausias = kortKon.Imti(i).ImtSMStarKit();
                    mazInt = i;
                }
            }
            return mazInt;
        }
        /// <summary>
        /// Metodas, kuris atrenka įrašus į naują konteinerį pagal užduoties
        /// sąlygą
        /// </summary>
        /// <param name="kortKon">Pradinis kortelių konteineris</param>
        /// <param name="naujosKorteles">Naujas kortelių konteineris</param>
        static void KorteliuAtrinkimas(KortKon kortKon, ref KortKon naujosKorteles)
        {
            Kortele kort;
            for (int i = 0; i < kortKon.Imti(); i++)
            {
                if (kortKon.Imti(i).ImtSMStarSav() == 0 &&
                    kortKon.Imti(i).ImtTarSav() == 0)
                {
                    kort = kortKon.Imti(i);
                    naujosKorteles.Deti(kort);
                }
            }
        }
        /// <summary>
        /// Metodas, kuris spausdina IeskotiSMSMaz() metodo atrinktą
        /// informaciją
        /// </summary>
        /// <param name="kortKon">Pradinis kortelių konteineris</param>
        /// <param name="maziausias">Mažiausia SMS tarifo į kitus reikšmė</param>
        static void SpausdintiMaz(string fv, KortKon kortKon, double maziausias)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("Kortelės su mažiausiais mokamais SMS tarifais į kitus tinklus:");
                fr.WriteLine("Mažiausias mokamas tarifas {0,-6:f2}", maziausias);
                for (int i = 0; i < kortKon.Imti(); i++)
                {
                    if (kortKon.Imti(i).ImtSMStarKit() == maziausias)
                    {
                        fr.WriteLine("{0}", kortKon.Imti(i).ToString());
                    }
                }
                fr.WriteLine("");
            }
        }
    }
}