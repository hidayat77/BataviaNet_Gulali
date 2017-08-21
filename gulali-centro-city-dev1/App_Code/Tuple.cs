using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tuple
/// </summary>
public class Tuple
{  // Double
    public class Double2<T, U>
    {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }
    
        public Double2(T item1, U item2)
        {
            Item1 = item1;
            Item2 = item2;
          
        }
    }

    public static class Double2
    {
        public static Double2<T, U> Create<T, U>(T item1, U item2)
        {
            return new Double2<T, U>(item1, item2);
        }
    }
    /*-------------------------------------------------------------------------------------------------*/
    public class Double2_String2<T, U,V,W>
    {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }
        public V Item3 { get; private set; }
        public W Item4 { get; private set; }

        public Double2_String2(T item1, U item2, V item3, W item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }
    }

    public static class Double2_String2
    {
        public static Double2_String2<T, U, V, W> Create<T, U, V, W>(T item1, U item2, V item3, W item4)
        {
            return new Double2_String2<T, U, V, W>(item1, item2, item3, item4);
        }
    }
    /*-------------------------------------------------------------------------------------------------*/
    public class Double7<T, U, V, W, X, Y, Z>
    {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }
        public V Item3 { get; private set; }
        public W Item4 { get; private set; }
        public X Item5 { get; private set; }
        public Y Item6 { get; private set; }
        public Z Item7 { get; private set; }
        public Double7(T item1, U item2, V item3, W item4, X item5, Y item6, Z item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }
    }

    public static class Double7
    {
        public static Double7<T, U, V, W, X, Y, Z> Create<T, U, V, W, X, Y, Z>(T item1, U item2, V item3, W item4, X item5, Y item6, Z item7)
        {
            return new Double7<T, U, V, W, X, Y, Z>(item1, item2, item3, item4, item5, item6, item7);
        }
    }


    /*-------------------------------------------------------------------------------------------------*/


    public class Double6<T, U, V, W, X, Y>
    {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }
        public V Item3 { get; private set; }
        public W Item4 { get; private set; }
        public X Item5 { get; private set; }
        public Y Item6 { get; private set; }
       
        public Double6(T item1, U item2, V item3, W item4, X item5, Y item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
          
        }
    }

    public static class Double6
    {
        public static Double6<T, U, V, W, X, Y> Create<T, U, V, W, X, Y>(T item1, U item2, V item3, W item4, X item5, Y item6)
        {
            return new Double6<T, U, V, W, X, Y>(item1, item2, item3, item4, item5, item6);
        }
    }

    // Decimal
    public class Decimal3<T, U,V>
    {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }
        public V Item3 { get; private set; }

        public Decimal3(T item1, U item2, V item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
    }

    public static class Decimal3
    {
        public static Decimal3<T, U, V> Create<T, U, V>(T item1, U item2, V item3)
        {
            return new Decimal3<T, U, V>(item1, item2, item3);
        }
    }
    /*-------------------------------------------------------------------------------------------------*/

    public class String1_Decimal2<T, U, V>
    {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }
        public V Item3 { get; private set; }

        public String1_Decimal2(T item1, U item2, V item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
    }

    public static class String1_Decimal2
    {
        public static String1_Decimal2<T, U, V> Create<T, U, V>(T item1, U item2, V item3)
        {
            return new String1_Decimal2<T, U, V>(item1, item2, item3);
        }
    }
    /*-------------------------------------------------------------------------------------------------*/

}