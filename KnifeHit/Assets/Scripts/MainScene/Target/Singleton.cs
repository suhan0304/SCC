using System;

public class Singleton
{
   private static volatile Singleton instance;
   private static object syncRoot = new Object();

   private Singleton() {}

   public static Singleton Instance
   {
      get 
      {
        lock (syncRoot) 
        {
            if (instance == null) 
                instance = new Singleton();
        }
        return instance;
      }
   }
}
 