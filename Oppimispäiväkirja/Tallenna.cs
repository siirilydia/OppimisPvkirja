using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Oppimispäiväkirja
{
    class Tallenna
    {
        //tallennetaan tiedostosijainti helposti annettavaan muotoon
        //TÄHÄN LISÄTÄÄN MYÖHEMMIN POLKU JOKA TOIMII MUILLAKIN KONEILLA, ELI "." JA TIEDOSTON NIMI
        //KATSO MALLIA SIIHEN POLKUUN KYSYMYKSET-RYHMÄTYÖSTÄ! :)
        string sijainti = @"C:\Academy\Oppimispäiväkirja\Oppimispäiväkirja\bin\Debug\Aiheet.txt";

        StreamWriter sw;

        public void TallennaTiedostoon(List<Topic> oliot)
        {
            //Tehdään uusi olio streamwriter-classista, käytetään sijainti-stringiä sijainnin
            //antamiseen. False tarkoittaa sitä, että tämä tekee aina overwriten. True tarkoittaa,
            //että kirjoitetaan vain olemassaolevan tekstin perään.

            sw = new StreamWriter(sijainti, false);

            foreach (var olio in oliot)
            {
                //apumuuttuja on se minimimäärä arvoja, jotka oliolle on pakko antaa.
                string tiedot = $"{olio.Id};{olio.Title};{olio.Description};{olio.Source};{olio.InProgress};" +
                    $"{olio.StartLearningDate};{olio.CompletionDate};{olio.EstimatedTimeToMaster};{olio.TimeSpent}";

                //kirjoitetaan apumuuttujan sisältämä teksti txt fileen.
                //tämä on foreach-looppi, eli tämä kaikki tehdään jokaikisen oliot-listan olion kohdalla
                //jolloin txt fileen kirjoitetaan string jokaisesta oliosta
                //sitten ensi kerralla, kun sovellus käynnistetään, luetaan ne taas txt filesta,
                //jossa ne ovat oikeassa muodossa eli ; kaiken välissä, sovelluksen käyttöön. :)
                sw.WriteLine(tiedot);
            }

            sw.Dispose();
        }

        //public string MuutaOlioTekstiksi(List<Topic> oliot)
        //{
        //    foreach (var olio in oliot)
        //    {
        //        //string tekstiksi = olio.GetType().GetProperties().ToString();
        //        //Console.WriteLine(tekstiksi);
        //        //string ApuMuuttuja = $"{olio.Id};{olio.Title};{olio.Description}";

        //        //if (Kotiosoite != null && Ikä != 0)
        //        //{
        //        //    ApuMuuttuja = $"{_id} {Etunimi}  { Sukunimi}, Ikä: {Ikä}, { Kotiosoite}";
        //        //}

        //        //else if (Kotiosoite != null)
        //        //{
        //        //    ApuMuuttuja = $"{_id} {Etunimi}  { Sukunimi}, { Kotiosoite}";
        //        //}
        //    }

        //    //return ApuMuuttuja;
        //}

    }
}
