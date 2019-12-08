using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using KA.StdfSharp;
using KA.StdfSharp.Record;
using KA.StdfSharp.Record.Field;

namespace StdfExecTest
{
    class Program
    {
        private static readonly string filePath = Path.GetFullPath(@"../../../../stdf/stdf_test.std");

        private static readonly Type[] Records;

        static Program()
        {
            IList<Type> records = new List<Type>();
            //records.Add(typeof(FarRecord));
            //records.Add(typeof(AtrRecord));
            //records.Add(typeof(MirRecord)); 
            //records.Add(typeof(MrrRecord));
            //records.Add(typeof(HbrRecord)); 
            //records.Add(typeof(SbrRecord));
            //records.Add(typeof(PcrRecord));
            //records.Add(typeof(PirRecord));
            //records.Add(typeof(PrrRecord)); 
            records.Add(typeof(PtrRecord));
            //records.Add(typeof(TsrRecord));
            //records.Add(typeof(WirRecord));

            Records = new Type[records.Count];
            records.CopyTo(Records, 0);
        }

        static void Main(string[] args)
        {
            Start();
            Finish();
        }

        private static void Start()
        {
            Console.WriteLine("Starting to read");
            DateTime start = DateTime.Now;
            int partCount = 0;
            try
            {
                using (FileStream stream = File.OpenRead(filePath))
                {
                    using (StdfFileReader reader = new StdfFileReader(stream))
                    {
                        foreach (Type record in Records)
                        {
                            //reader.RegisterDelegate(record, PtrReadDelegate);
                            reader.RegisterDelegate(record, delegate (object o, RecordReadEventArgs e)
                                                                {
                                                                    partCount++;
                                                                    //Console.WriteLine(e.Record);
                                                                   //Debug.WriteLine("Read {0} PTR records", partCount);
                                                                });
                        }
                        reader.Read();
                        Finish();
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("An exception occurred: {0}" , e.Message);
                Console.WriteLine(e.StackTrace);
            }
            DateTime end = DateTime.Now;
            Console.WriteLine("Elapsed time: {0}", end - start);
            Console.WriteLine("Counted {0} parts", partCount);
            Console.ReadLine();
        }

        private static void PtrReadDelegate(object sender, RecordReadEventArgs e)
        {
            PtrRecord ptr = e.Record as PtrRecord;
            if (ptr == null)
                return;
            Console.WriteLine(e.Record);
        }

        private static void Finish()
        {
            Console.WriteLine("Reading finished");
            Debug.WriteLine("Reading finished");
        }
    }
}
