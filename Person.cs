using System;
using App.User.LocationInfo.Models;
using App.User.LocationInfo.Services;

namespace TableTest {
    public class Person {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthday { get; set; }
        private BasicUserLocationInfo LocationInfo => TrackingService.GetBasicLocatioInfoAsync ().Result;
        public string Country => LocationInfo.Country;
        public string City => LocationInfo.City;
        public string Language => LocationInfo.Languages[0];
        public string Cordinates => $"Lat: {LocationInfo.Location.Latitude} Long {LocationInfo.Location.Longitude}";

        public Person (string name, string surname, int birthday) {
            this.Name = name;
            this.Surname = surname;
            this.Birthday = (DateTime.Now.Year - birthday).ToString ();

            if (int.Parse (this.Birthday) >= 18) {
                this.Birthday = $"[green]{this.Birthday}[/]";
            } else {
                this.Birthday = $"[red]{this.Birthday}[/]";
            }

        }
    }
}