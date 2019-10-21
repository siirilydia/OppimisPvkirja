using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Oppimispäiväkirja

{
    public class TiedostonLuku
    {
        //tallennetaan tiedostosijainti helposti annettavaan muotoon
        //TÄHÄN LISÄTÄÄN MYÖHEMMIN POLKU JOKA TOIMII MUILLAKIN KONEILLA, ELI "." JA TIEDOSTON NIMI
        //KATSO MALLIA SIIHEN POLKUUN KYSYMYKSET-RYHMÄTYÖSTÄ! :)
        public string sijainti = @"C:\Academy\Oppimispäiväkirja\Oppimispäiväkirja\bin\Debug\Aiheet.txt";

        public StreamReader sr;

        public StreamReader TeeStreamReader()
        {
            if (!File.Exists(sijainti))
            {   //jos polku on väärä, tulostetaan tämä.
                //LISÄÄ TÄHÄN KÄYTTÄJÄLLE MAHDOLLISUUS SYÖTTÄÄ POLKU ITSE?
                Console.WriteLine("Virheellinen tiedostosijainti");
                sr = null;
            }

            else
            {
                //tehdään uusi StreamReader-classin olio, jossa käytetään polkuna sijainti-stringiä.
                sr = new StreamReader(sijainti);
            }
            return sr;
        }

        //public void TulostaRivit()
        //{
        //    TeeStreamReader();

        //    string rivi;

        //    while ((rivi = sr.ReadLine()) != null)
        //    {
        //        Console.WriteLine(rivi);
        //    }
        //    sr.Dispose();
        //}
        //LUETAAN TIEDOSTOSTA RIVIT, TEHDÄÄN RIVISTÄ OLIO JA ANNETAAN LUOTU OLIO OLIOLISTA-CLASSIN KÄSITELTÄVÄKSI

        public List<string> LueTiedostonRivit()
        {
            //kutsutaan StreamReader-metodi joka tekee StreamReader-classin olion.
            TeeStreamReader();

            //Tehdään list<string> johon laitetaan seuraavaksi tiedostosta luetut rivit. Nyt se on tyhjä.
            List<string> luetutrivit = new List<string>();
            string luetturivi;

            while ((luetturivi = sr.ReadLine()) != null)
            {
                //Niin pitkään, kun tiedostosta ei lueta tyhjää riviä, asetetaan luetturivi-stringin
                //arvoksi sen rivin sisältö, mitä juuri luettiin. Ja se lisätään luetutrivit-listaan.
                luetutrivit.Add(luetturivi);
            }

            //suljetaan tiedosto, vapautetaan resurssit
            sr.Dispose();

            //palautetaan luetutrivit-lista sinne, mistä tätä metodia on kutsuttu.
            return luetutrivit;
        }
    }
}
