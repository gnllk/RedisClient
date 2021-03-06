﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnllk.RedisClient
{
    public abstract class Singleton<T> where T : new()
    {
        public static T Instance { get { return Nested.Instance; } }

        private class Nested
        {
            public static T Instance = new T();
            static Nested() { }
        }
    }
}
