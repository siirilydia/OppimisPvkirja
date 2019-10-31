using System;
using System.Collections.Generic;
using System.Text;

namespace Oppimispäiväkirja
{

    class Käsittely
    {
        static TiedostonLuku tl = new TiedostonLuku();
        static Tallenna t = new Tallenna();
        public static List<Topic> oliot = new List<Topic>();

        public static List<string> aiheet;

        public List<string> LueAiheet()
        {
            aiheet = tl.LueTiedostonRivit();
            return aiheet;
        }

        public List<Topic> TeeOlioita()
        {
            {
                bool onMuokattava = false;

                foreach (var aihe in aiheet)
                {
                    //luetaan luettujen ja lisättyjen rivien listalta asiat, splitataan ne
                    //aina puolipilkun kohdalla string[]arrayksi.
                    string[] arvot = aihe.ToString().Split(';');

                    Topic uusiaihe = TeeListastaOlioita(0, arvot, onMuokattava);
                    oliot.Add(uusiaihe);
                }

                foreach (var olio in oliot)
                {
                    //kirjoitetaan kaikkien olio-listassa olevien titlet.
                    //tässä on siis vain ne, jotka lopulta kirjoitetaan txt fileen.
                    //sen takia edellisessä kohdassa määriteltiin, löytyykö lisätystä asiasta vastaava olio
                    //vai ei - olio listaan lisättiin vain ne, jotka halutaan lopulta tallennuksessa
                    //kirjoitettavan listaan.

                    //LISÄTÄÄN TOIMINTO, JOSSA TÄMÄ TEHDÄÄN VAIN KÄYTTÄJÄN PYYNNÖSTÄ 
                    Console.WriteLine(olio.Title);
                }
            }
            return oliot;
        }

        public List<string> LisääAihe()
        {
            {
                //Näköjään käyttäjä halusi lisätä tai muokata jotain.
                //Kirjoitettu asia lisätään aiheet-listaan, jossa on myös aiemmin tiedostosta luetut aiheet.

                //TÄHÄN LISÄTÄÄN TOIMINTO, JOSSA INPUTIN VOI ANTAA MYÖS "VIRHEELLISENÄ" JA SOVELLUS KORJAA SEN
                string input = $"{aiheet.Count};";

                Console.WriteLine("Kirjoita aiheelle nimi");
                input += Console.ReadLine();
                Console.WriteLine("Kirjoita uudelle aiheelle kuvaus");
                input += ";" + Console.ReadLine();
                aiheet.Add(input);

                Console.WriteLine("");
                Console.WriteLine("Lisättiin uusi rivi listaan, josta tehdään olioita...");
                Console.WriteLine("");

                SyötäLisätietoja(aiheet);
            }

            void SyötäLisätietoja(List<string> aiheet)
            {
                Console.WriteLine("Tietolähde:");
                aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //aiheen tietolähde

                Console.WriteLine("Onko opiskelua vielä aloitettu?");
                Console.WriteLine("1 = KYLLÄ 2 = EI 3 = KYLLÄ JA ON JO VALMIS");
                int input = int.Parse(Console.ReadLine().Trim());
                switch (input)
                {
                    case 1:
                        aiheet[aiheet.Count - 1] += ";" + "true"; //opiskelu on käynnissä
                        Console.WriteLine("Päivämäärä, jolloin opiskelu alkoi:");
                        aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //päivämäärä koska alkoi
                        aiheet[aiheet.Count - 1] += ";" + "1.1.0001 0.00.00)"; //on valmis-päivämäärä on null
                        Console.WriteLine("Kuinka kauan uskot, että opiskelussa kestää (tunteja)?");
                        aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //arvio opiskelun kestosta tunteina
                        aiheet[aiheet.Count - 1] += ";" + "0"; //kauan kesti todellisuudessa = null
                        break;

                    case 2:
                        aiheet[aiheet.Count - 1] += ";" + "false"; //ei käynnissä
                        aiheet[aiheet.Count - 1] += ";" + "1.1.0001 0.00.00"; //aloituspvm on null
                        aiheet[aiheet.Count - 1] += ";" + "1.1.0001 0.00.00"; //lopetuspvm on null
                        Console.WriteLine("Kuinka kauan uskot, että opiskelussa kestää (tunteja)?");
                        aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //arvio opiskelun kestosta tunteina
                        aiheet[aiheet.Count - 1] += ";" + "0"; //kauan kesti todellisuudessa = null
                        break;

                    case 3:
                        aiheet[aiheet.Count - 1] += ";" + "false"; //ei käynnissä, koska on valmis
                        Console.WriteLine("Päivämäärä, jolloin opiskelu alkoi:");
                        aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //aloituspvm
                        Console.WriteLine("Päivämäärä, jolloin opiskelu loppui:");
                        aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //lopetuspvm
                        Console.WriteLine("Kuinka kauan suunnittelit käyttää opiskeluun (tunteja)?");
                        aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //arvio opiskelun kestosta tunteina
                        Console.WriteLine("Kuinka kauan käytit opiskeluun todellisuudessa?");
                        aiheet[aiheet.Count - 1] += ";" + Console.ReadLine(); //kauan kesti todellisuudessa
                        break;
                }
            }
            return aiheet;
        }
        public List<Topic> MuokkaaOlioita()
        {
            bool onMuokattava = true;

            Console.WriteLine("Tässä on lista olemassaolevista aiheista.");
            Console.WriteLine();

            foreach (var olio in oliot)
            {
                Console.WriteLine(olio.Id + " " + olio.Title);
            }

            Console.WriteLine("Valitse mitä haluat muokata, syöttämällä sen aiheen numero:");
            int muokattava = int.Parse(Console.ReadLine());

            Console.WriteLine($"Halusit muokata aihetta {oliot[muokattava - 1].Id} {oliot[muokattava - 1].Title}");
            Console.WriteLine("muokataan...");
            aiheet = LisääAihe();

            var arvot = aiheet[aiheet.Count - 1].Split(';');

            Topic uusiaihe_m = TeeListastaOlioita(muokattava, arvot, onMuokattava);

            oliot[muokattava - 1] = uusiaihe_m;

            return oliot;
        }

        public Topic TeeListastaOlioita(int kohteennro, string[] arvot, bool onMuokkaus)
        {
            Topic uusiaihe = new Topic();

            if (onMuokkaus)
                uusiaihe.Id = oliot[kohteennro - 1].Id;
            else
                uusiaihe.Id = Convert.ToInt32(arvot[0]);

            uusiaihe.Title = arvot[1];
            uusiaihe.Description = arvot[2];
            uusiaihe.Source = arvot[3];
            uusiaihe.InProgress = Convert.ToBoolean(arvot[4]);

            DateTime result;
            DateTime.TryParse(arvot[5], out result);
            uusiaihe.StartLearningDate = result;

            DateTime.TryParse(arvot[6], out result);
            uusiaihe.CompletionDate = result;

            uusiaihe.EstimatedTimeToMaster = Convert.ToDouble(arvot[7]);
            uusiaihe.TimeSpent = Convert.ToDouble(arvot[8]);

            return uusiaihe;
        }

        public List<Topic> LisääOlio()
        {
            {
                int täsmäävä = 0;
                bool korvaa = false;

                //luetaan luettujen ja lisättyjen rivien listalta asiat, splitataan ne
                //aina puolipilkun kohdalla string[]arrayksi.
                var arvot = aiheet[aiheet.Count - 1].Split(';');

                //Koska jotain oli lisätty, tarkistetaan, että löytyikö sellainen olio jo.
                //Eli määritellän, pitääkö tehdä overwrite vai ei.
                //Tämä on siksi, että aina kun tehdään muutos, niin halutaan lisätä oliot-listaan
                //ainoastaan uudet asiat, ei kaikkia rivejä. Muuten tulisi duplikaatteja.
                täsmäävä = LöytyyköListasta(arvot);

                if (täsmäävä != -1)
                {
                    //näköjään löytyi oliot-listasta joku käyttäjän antamaa arvoa vastaava olio.
                    //koska luku on defaulttina -1, niin tiedetään, että jos se ei ole -1,
                    //on jotain löytynyt. Täsmäävä ei voi olla defaulttina 0, koska oliot-listassa on
                    //jotain myös sijalla 0. Siksi se on -1.

                    Console.WriteLine("Antamallasi järjestysluvulla ja/tai nimellä on jo luotu aihe.");
                    Console.WriteLine("Haluatko muuttaa olemassaolevan aiheen antamasi tietoja vastaavaksi,");
                    Console.WriteLine("vai luoda uuden aiheen eri nimellä?");
                    Console.WriteLine("");
                    Console.WriteLine("1 = TEE UUSI AIHE  2 = MUUTA OLEMASSAOLEVAA AIHETTA");

                    int input = int.Parse(Console.ReadLine().Trim());

                    if (input == 1)
                    {
                        //korvaa = PoistaOlio(oliot);
                        korvaa = false;
                        Console.WriteLine($"Kirjoita aiheelle jokin muu nimi, kuin {arvot[1]}");
                        arvot[1] = Console.ReadLine().Trim();
                    }
                    else if (input == 2)
                    {
                        korvaa = true;
                    }
                }

                Topic uusiaihe = TeeListastaOlioita(0, arvot, false);

                //jos uutta lisätessä ei ollut vastaavaa, lisätään tämä vain uutena oliot-listaan.
                if (korvaa == false)
                {
                    uusiaihe.Id += 1;
                    oliot.Add(uusiaihe);
                    //aiheet.Remove(aihe);
                }
                //jos uutta lisätessä vastaava olio löytyi, korvataan se arvoilla jotka käyttäjä antoi.
                else
                {
                    uusiaihe.Id = täsmäävä + 1;
                    oliot[täsmäävä] = uusiaihe;
                }

                Console.WriteLine("Muutetaan teksti olioksi ja lisätään olio-listaan...");

                foreach (var olio in oliot)
                {
                    //katotaan että nyt kun lisättiin tai muokattiin, että menikö kaikki oikein...
                    Console.WriteLine(olio.Title);
                }
            }
            return oliot;
        }

        public int LöytyyköListasta(string[] arvot)
        {
            //tutkitaan, löytyikö käyttäjän antama asia jo valmiiksi olevista olioista oliot-listassa.
            //Jos löytyi, niin nnetaan int arvo, jonka avulla tiedetään, monesko olio vastasi
            //käyttäjän antamaa tietoa, eli mikä olioista overwritetaan.

            //LINQ  arvot.find(

            int löytyi = -1;
            for (int o = 0; o < oliot.Count; o++)
            {
                if (arvot[1] == oliot[o].Title)
                {
                    löytyi = o;
                }
            }
            return löytyi;
        }
        public List<Topic> PoistaOlio()
        {
            Console.WriteLine("Tässä on lista olemassaolevista aiheista.");
            Console.WriteLine();

            foreach (var olio in oliot)
            {
                Console.WriteLine(olio.Id + " " + olio.Title);
            }
            Console.WriteLine();
            Console.WriteLine("Minkä aiheen haluat poistaa?");

            int poistettava = int.Parse(Console.ReadLine());
            oliot.Remove(oliot[poistettava - 1]);
            Console.WriteLine("poistettu, kiitos");

            foreach (var olio in oliot)
            {
                if (olio.Id >= poistettava)
                    olio.Id = (olio.Id - 1);
            }
            return oliot;
        }
    }
}
