﻿namespace ElipseshopMVC.Common
{
    using System;

    public interface IRandomGenerator
    {
        int RandomNumber(int min, int max);
        string RandomString(int minLength = 5, int maxLength = 50);
    }
}
