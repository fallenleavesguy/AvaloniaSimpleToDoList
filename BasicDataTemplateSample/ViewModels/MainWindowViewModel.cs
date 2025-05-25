﻿using System.Collections.Generic;
using BasicDataTemplateSample.Models;

namespace BasicDataTemplateSample.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public List<Person> People { get; } = new List<Person>()
    {
        new Teacher
        {
            FirstName = "Mr.",
            LastName = "X",
            Age = 55,
            Sex = Sex.Diverse,
            Subject = "My Favorite Subject"
        },
        new Student
        {
            FirstName = "Hello",
            LastName = "World",
            Age = 17,
            Sex = Sex.Male,
            Grade = 12
        },
        new Student
        {
            FirstName = "Hello",
            LastName = "Kitty",
            Age = 12,
            Sex = Sex.Female,
            Grade = 6
        }
    };
}