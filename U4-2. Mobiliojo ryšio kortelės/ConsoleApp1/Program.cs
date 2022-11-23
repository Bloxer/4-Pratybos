using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Globalization;

namespace lab3
{
    /// <summary>
    /// klasė žadėjui aprašyti
    /// </summary>
    class Zaidejas
    {
        string komanda;
        string pavarde;
        string vardas;
        int RungtSk; // sužaistų rungtynių skaičius
        int ImustSk; // įmuštų įvarčių skaičius
        bool Daug;
        /// <summary>
        /// pradiniai žaidėjo duomenys
        /// </summary>
        /// <param name="komanda"></param>
        /// <param name="pavarde"></param>
        /// <param name="vardas"></param>
        /// <param name="rungtSk"></param>
        /// <param name="imustSk"></param>
        public Zaidejas(string komanda, string pavarde, string vardas, int rungtSk, int imustSk)
        {
            this.komanda = komanda;
            this.vardas = vardas;
            this.pavarde = pavarde;
            RungtSk = rungtSk;
            ImustSk = imustSk;
            Daug = true;
        }

        /// <summary>
        /// spausdinimo metodas
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, -12} | {1, -15} | {2, -9} |    {3,8}    | {4, 9} |  ",
            komanda, pavarde, vardas, RungtSk, ImustSk);
            return eilute;
        }
        public string ImtiKomanda() { return komanda; }
        public string ImtiVarda() { return vardas; }
        public int ImtiRungt() { return RungtSk; }
        public int ImtiIvarciai() { return ImustSk; }
        public void KeistiDaug(bool a) { Daug = a; }
        public bool ImtiDaug() { return Daug; }

        public override bool Equals(object obj)
        {
            return obj is Zaidejas zaidejas &&
                   komanda == zaidejas.komanda &&
                   pavarde == zaidejas.pavarde &&
                   vardas == zaidejas.vardas &&
                   RungtSk == zaidejas.RungtSk &&
                   ImustSk == zaidejas.ImustSk &&
                   Daug == zaidejas.Daug;
        }
        /// <summary>
        /// užklotas metodas
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = 741578156;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(komanda);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(pavarde);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(vardas);
            hashCode = hashCode * -1521134295 + RungtSk.GetHashCode();
            hashCode = hashCode * -1521134295 + ImustSk.GetHashCode();
            hashCode = hashCode * -1521134295 + Daug.GetHashCode();
            return hashCode;
        }

        public static bool operator <=(Zaidejas s1, Zaidejas s2)
        {
            int st = string.Compare(s1.komanda, s2.komanda, StringComparison.CurrentCulture);
            int st2 = string.Compare(s1.vardas, s2.vardas, StringComparison.CurrentCulture);
            return (st < 0) || ((st == 0) && (st2 < 0));
        }
        public static bool operator >=(Zaidejas s1, Zaidejas s2)
        {
            int st = string.Compare(s1.komanda, s2.komanda, StringComparison.CurrentCulture);
            int st2 = string.Compare(s1.vardas, s2.vardas, StringComparison.CurrentCulture);
            return (st > 0) || ((st == 0) && (st2 > 0));
        }
    }
    /// <summary>
    /// Futbolininkų konteinerinė klasė
    /// </summary>
    class Futbolininkai
    {
        const int CMax = 100;
        private Zaidejas[] z;
        private int kiek;
        public Futbolininkai()
        {
            kiek = 0;
            z = new Zaidejas[CMax];
        }
        /// <summary>
        /// grąžina kiekį
        /// </summary>
        /// <returns></returns>
        public int imtiKiek() { return kiek; }
        /// <summary>
        /// grąžina nurodyto indekso žaidėjo objektą
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Zaidejas Imti(int i) { return z[i]; }
        /// <summary>
        /// padeda į žaidėjo masyvą naują žaidėją ir masyvo dydi padidina vienetu
        /// </summary>
        /// <param name="ob"></param>
        public void Dėti(Zaidejas ob) { z[kiek++] = ob; }

        /// <summary>
        /// rikiavimo metodas
        /// </summary>
        public void Rikiuoti()
        {
            Futbolininkai Fut = new Futbolininkai();
            for (int i = 0; i < kiek; i++)
                for (int j = i + 1; j < kiek; j++)
                {
                    if (z[i] >= z[j])
                    {
                        Zaidejas min = z[i];
                        Zaidejas max = z[j];
                        z[i] = max;
                        z[j] = min;
                    }
                }
        }
    }
    class Komanda
    {
        string pav;
        const int CMax = 100;
        private Zaidejas[] z;
        private int kiek;

        public Komanda()
        {
            pav = "";
            z = new Zaidejas[CMax];
        }
        public void Deti(string pav)
        {
            this.pav = pav;
        }
        public void Dėti(Zaidejas ob) { z[kiek++] = ob; }
        public int imtiKiek() { return kiek; }
        public string imtiPav() { return pav; }
    }
    internal class Program
    {
        const string CFd = "..\\..\\U1.txt";
        const string CFr = "..\\..\\Rez.txt";
        static void Main(string[] args)
        {
            if (File.Exists(CFr))
                File.Delete(CFr);
            Futbolininkai Fut = new Futbolininkai();
            Futbolininkai Fut2 = new Futbolininkai();
            Futbolininkai Fut3 = new Futbolininkai();
            Skaitymas(Fut, CFd);
            Fut.Rikiuoti();
            GeriausiasZaid(Fut, Fut2);
            Spausdinti(Fut, CFr, "PRADINIAI DUOMENYS(SURIKIUOTI PAGAL KOMANDA IR PAVARDE)");
            string[] komandos = new string[6];
            komandos[0] = "";
            AtrinktiKomandas(komandos, Fut);
            AtrinktiZaidejus(komandos, Fut2, Fut3);
            Fut3.Rikiuoti();
            Spausdinti(Fut3, CFr, "ATRINKTI DUOMENYS(DIDŽAUSIA NAUDINGUMO KOEFICINTĄ TURINTYS ŽAIDĖJAI)");
            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine("Vidutinis sužaistų rungtynių skaičius - {0,3}", VidRungt(Fut));
            }
        }
        /// <summary>
        /// skaitymo metodas
        /// </summary>
        /// <param name="Fut"></param>
        /// <param name="fv"></param>
        static void Skaitymas(Futbolininkai Fut, string fv)
        {
            string komanda, vardas, pavarde;
            int RuSk, ImSk;
            string[] lines = File.ReadAllLines(fv, Encoding.GetEncoding(1257));
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                komanda = parts[0];
                pavarde = parts[1];
                vardas = parts[2];
                RuSk = int.Parse(parts[3]);
                ImSk = int.Parse(parts[4]);
                Zaidejas z = new Zaidejas(komanda, pavarde, vardas, RuSk, ImSk);
                Fut.Dėti(z);
            }
        }
        /// <summary>
        /// spausdinimo metodas
        /// </summary>
        /// <param name="fut"></param>
        /// <param name="fv"></param>
        /// <param name="antraštė"></param>
        static void Spausdinti(Futbolininkai fut, string fv, string antraštė)
        {

            string virsus = "---------------------------------------------------" +
                                "---------------------\r\n  Komanda    |     Pavarde   " +
                                "  |  Vardas   |Sužaistos rungt.| Įvarčiai  |\r\n-" +
                                "---------------------------------------------------" +
                                "--------------------";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraštė);
                fr.WriteLine(virsus);

                for (int i = 0; i < fut.imtiKiek(); i++)
                    fr.WriteLine("{0}", fut.Imti(i).ToString());

                fr.WriteLine("---------------------------------------------" +
                                  "---------------------------\n\n");
            }
        }
        /// <summary>
        /// metodas skirtas apskaičiuoti kiek vidutiniškai žaidėjai sužaidė runtynių
        /// </summary>
        /// <param name="Fut"></param>
        /// <returns></returns>
        static double VidRungt(Futbolininkai Fut)
        {
            double laikinas = 0;
            for (int i = 0; i < Fut.imtiKiek(); i++)
            {
                laikinas += Fut.Imti(i).ImtiRungt();
            }
            laikinas = laikinas / Fut.imtiKiek();
            return laikinas;
        }
        /// <summary>
        /// metodas skirtas surasti geriausią žaidėją
        /// </summary>
        /// <param name="Fut"></param>
        /// <param name="Fut2"></param>
        static void GeriausiasZaid(Futbolininkai Fut, Futbolininkai Fut2)
        {
            for (int i = 0; i < Fut.imtiKiek(); i++)
            {
                if (Fut.Imti(i).ImtiRungt() > VidRungt(Fut))
                {
                    Fut2.Dėti(Fut.Imti(i));
                }
            }
        }
        static void AtrinktiKomandas(string[] komPav, Futbolininkai Fut)
        {
            int j = 0;
            for(int i = 0; i < Fut.imtiKiek(); i++)
            {
                if (!komPav.Contains(Fut.Imti(i).ImtiKomanda()))
                {
                    komPav[j] = Fut.Imti(i).ImtiKomanda();
                    j++;
                }
            }
        }
        static void AtrinktiZaidejus(string[] komPav, Futbolininkai Fut2, Futbolininkai Fut3)
        {
            int ivarciai = 0;
            int indeksas = 0;
            foreach (string pavadinimas in komPav)
            {
                for (int i = 0; i < Fut2.imtiKiek(); i++)
                {
                        if (pavadinimas == Fut2.Imti(i).ImtiKomanda()
                           && Fut2.Imti(i).ImtiIvarciai() > ivarciai)
                        {
                            ivarciai = Fut2.Imti(i).ImtiIvarciai();
                            indeksas = i;
                        }
                }
                if (Fut3.Imti(indeksas) != Fut2.Imti(indeksas))
                {
                    ivarciai = 0;
                    Fut3.Dėti(Fut2.Imti(indeksas));
                }
            }
        }
    }
}