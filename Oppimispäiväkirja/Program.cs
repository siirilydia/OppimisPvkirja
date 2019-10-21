using System;
using System.Collections.Generic;

namespace Oppimispäiväkirja
{
    public class Program
    {

        public static List<Topic> oliot = new List<Topic>();
        public static List<string> aiheet;

        //static Topic topic = new Topic();
        static TiedostonLuku tl = new TiedostonLuku();
        static Käsittely k = new Käsittely();
        static Tallenna t = new Tallenna();

        static void Main(string[] args)
        {
            //luetaan tiedostosta rivit ja sieltä asetetaan List<string>aiheet-sisältö
            aiheet = k.LueAiheet();
            //kirjoitetaan kaikki luetut rivit, tämä lähinnä testimetodi jotta näkee että kaikki meni oikein
            KirjoitaRivit(aiheet);
            //tehdään luetuista riveistä olioita
            oliot = k.TeeOlioita();

            MitäTehdään();

            Console.WriteLine("Mitäs sitten tehdään?");
            Console.WriteLine();
            MitäTehdään();
           //tehdään kaikista olioista stringejä ja kirjoitetaan ne oikeasssa muodossa txt fileen.
           t.TallennaTiedostoon(oliot);
        }


        static void KirjoitaRivit(List<string> aiheet)
        { //testimetodi varmistukseen, kirjoitetaan kaikki rivit aiheet-listasta.
            foreach (var aihe in aiheet)
            {
                Console.WriteLine(aihe);
            }
        }

        static void MitäTehdään()
        {
            Console.WriteLine("Haluatko lisätä tai muokata jotakin?");
            Console.WriteLine("1 = LISÄÄ 2 = MUOKKAA 3 = POISTA");
            int toiminto = int.Parse(Console.ReadLine());

            switch (toiminto)
            {
                case 1:
                    //Käyttäjä vastasi, että halutaan lisätä, jolloin kirjoitetaan halutut tiedot
                    //ja lisätään ne samaan listaan jossa on luetut rivit. Kutsutaan lisäätaimuokkaaolioita-metodi,
                    //joka vertailee, onko nimi sama kuin olemassaolevien - jos on, tapahtuu olion overwrite.
                    //Jos ei, tehdään uusi olio.
                    aiheet = k.LisääAihe();
                    //Koska jotain on nyt lisätty, pitää muutetuista asioista tehdä olioita, tai muokata vanhoja.
                    oliot = k.LisääOlio();
                    HaluatkoLisätä();
                    break;

                case 2:
                    oliot = k.MuokkaaOlioita();
                    break;

                case 3:
                    oliot = k.PoistaOlio();
                    break;
            }
        }

        static void HaluatkoLisätä()
        {
            //tässä kysytään, haluaako lisätä jotain vai ei.
            //Jos kyllä, kutsutaan lisää-metodi uudestaan.
            Console.WriteLine("Haluatko lisätä uuden aiheen?");
            string input = Console.ReadLine();
            if (input == "kyllä")
            {aiheet = k.LisääAihe();}
            else
            {Console.WriteLine("OK!");}
        }
    }
}