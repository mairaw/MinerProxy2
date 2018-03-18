﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinerProxy2.Helpers
{
    public static class Extensions
    {
        public static string GetString(this byte[] data)
        {
            string result = Encoding.ASCII.GetString(data);
            
            return result;
        }

        public static byte[] GetBytes(this string data)
        {
            byte[] result = Encoding.ASCII.GetBytes(data);

            return result;
        }

        public static byte[] CheckForNewLine(this byte[] data)
        {
            byte[] result = JsonHelper.CheckForNewLine(data);

            return result;
        }

        public static string CheckForNewLine(this string data)
        {
            string result = JsonHelper.CheckForNewLine(data);

            return result;
        }

        public static string ToReadableTime(this DateTime value, string append = "")
        {
            TimeSpan ts = DateTime.Now - value;
            //var ts = new TimeSpan(DateTime.Now - value);
            double delta = ts.TotalSeconds;

            if (append.Length > 0)
                append = " " + append;

            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second" + append : ts.Seconds + " seconds" + append;
            }
            if (delta < 120)
            {
                return "a minute" + append;
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes" + append;
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour" + append;
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours" + append;
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days" + append;
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month" + append : months + " months" + append;
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year" + append : years + " years" + append;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> act)
        {
            foreach (var i in array)
                act(i);
            return array;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable arr, Action<T> act)
        {
            return arr.Cast<T>().ForEach<T>(act);
        }

        public static IEnumerable<RT> ForEach<T, RT>(this IEnumerable<T> array, Func<T, RT> func)
        {
            var list = new List<RT>();
            foreach (var i in array)
            {
                var obj = func(i);
                if (obj != null)
                    list.Add(obj);
            }
            return list;
        }
    }
}