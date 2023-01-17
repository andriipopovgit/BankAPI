﻿namespace BankAPI
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public override string ToString()
        {
            return $"{Id} {Firstname} {Lastname}";
        }
    }
}
