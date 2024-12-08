using System;

namespace Final_Project_Web_API.Models
{
    // Ethan Rettinger
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string CollegeProgram { get; set; }
        public string YearInProgram { get; set; }
    }
}