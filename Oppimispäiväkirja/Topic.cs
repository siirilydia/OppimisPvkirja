using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Oppimispäiväkirja
{
    public class Topic
    {
        public int Id;
        public string Title;
        public string Description;
        public string Source;
        public bool InProgress;
        public DateTime StartLearningDate = new DateTime();
        public DateTime CompletionDate = new DateTime();
        public double EstimatedTimeToMaster;
        public double TimeSpent;

        public Topic()
        {
            //tyhjä konstruktori, jotta eri classeissa voisi luoda olioita,
            //joiden avulla voisi sitten kutsua tässä classissa mahdollisesti olevia metodeita.
        }


        public Topic(int id, string title, string descr)
        {
            //konstruktori, jolla annetaan vain id, title ja kuvaus.
            this.Id = id;
            this.Title = title;
            this.Description = descr;
            //this.EstimatedTimeToMaster = 0;
            //this.Source = null;
        }

        //public Topic(int id, string title, string descr, double estTime, string source)
        //{
        //    //konstruktori, jolla annetaan id, title, kuvaus, aika-arvio sekä lähde.
        //    this.Id = id;
        //    this.Title = title;
        //    this.Description = descr;
        //    this.EstimatedTimeToMaster = estTime;
        //    this.Source = source;
        //}

        public override string ToString()
        {
            //juuri nyt ihan hyödytön toiminto, mutta periaatteessa jos haluaisin kirjoittaa jotain
            //tyyliin Console.WriteLine(uusiaihe);, niin se ei kirjoittaisikaan jotain.topic vaan sen,
            //mitä tässä on annettu eli uusiaihe-olion id;title;description
            string aihe = ($"{Id};{Title};{Description}");
            return aihe;
        }

    }
}
