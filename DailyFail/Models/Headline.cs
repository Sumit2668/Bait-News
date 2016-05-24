﻿using System;
using BaitNews.Helpers;

namespace BaitNews.Models
{
    public class Headline : EntityData
    {
        public string Text { get; set;}
        public string Source { get; set;}
        public string Url { get; set;}
        public bool IsTrue { get; set;}
    }
}

