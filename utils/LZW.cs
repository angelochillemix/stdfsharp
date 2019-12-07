#undef debug
#define debugdisplay
#undef debugdictionary
using System;
using System.Collections;

namespace LZW
{
 public class cLZW
 {
  #region Constrcut
  public cLZW()
  {
  }
  #endregion
  
  #region Coding
  public string InCharStream
  {
   set { _InCharStream = value; }
   get {return _InCharStream; }
  }
  public ArrayList CodingCodeStream
  {
   get {return _CodingCodeStream;}
  }
  public ArrayList CodingDictionary
  {
   get {return _CodingDictionary;}
  }
  private void InitCodingDictionary()
  {
   _CodingDictionary.Clear();
#if debug
   _CodingDictionary.Add("A");
   _CodingDictionary.Add("B");
   _CodingDictionary.Add("C");
#else
   for(int i = 0; i < 256; i++)
   {
    _CodingDictionary.Add((char)i);
   }
#endif
  }
  private void AddCodingDictionary(object str)
  {
   _CodingDictionary.Add(str);
  }
  private void AddCodingCodeStream(object str)
  {
   _CodingCodeStream.Add(str);
  }
  private bool ISInCodingDictionary(string Prefix)
  {
   bool result = false;
   int  count = _CodingDictionary.Count;
   for(int i = 0; i < count; i++)
   {
    string temp = _CodingDictionary[i].ToString();
    if (temp.IndexOf(Prefix) >= 0)
    {
     result = true;
     break;
    }
   }
   return result;
  }
  private string  GetIndexCodingDictionary(string Prefix)
  {
   string result ="0";
   int  count = _CodingDictionary.Count;
   for(int i = 0; i < count; i++)
   {
    string temp = _CodingDictionary[i].ToString();
    if (temp.IndexOf(Prefix) >= 0)
    {
     result = Convert.ToString(i + 1);
     break;
    }
   }
   return result;
  }
  private void DisplayCodingCodeStream()
  {
   System.Console.WriteLine("*********_CodingCodeStream************");
   for(int i = 0; i < _CodingCodeStream.Count; i++)
   {
    System.Console.WriteLine(_CodingCodeStream[i].ToString());
   }
  }
  private void DisplayCodingDictionary()
  {
   System.Console.WriteLine("*********_CodingDictionary************");
   for(int i = 0; i < _CodingDictionary.Count; i++)
   {
    System.Console.WriteLine(_CodingDictionary[i].ToString());
   }
  }
  private void DisplayInCharStream()
  {
   System.Console.WriteLine("*********_InCharStream************");
   System.Console.WriteLine(_InCharStream);
  }
  private void InitCodingCodeStream()
  {
   _CodingCodeStream.Clear();
  }
  private ArrayList _CodingDictionary = new ArrayList();
  private string _InCharStream = "";
  private ArrayList _CodingCodeStream = new ArrayList();
  public void Coding()
  {
   string Prefix ="" ;
   string c ="";
   string PrefixIndex= "0";
   int  count = _InCharStream.Length;
   if (count == 0) return ;
   InitCodingDictionary();
   InitCodingCodeStream();
   Prefix = _InCharStream[0].ToString();
   for(int i = 1; i < count; i++)
   {
    c = _InCharStream[i].ToString();
    if (ISInCodingDictionary( Prefix + c))
    {
     Prefix += c;
    }
    else
    {
     PrefixIndex = GetIndexCodingDictionary(Prefix);
     AddCodingCodeStream(PrefixIndex);
     AddCodingDictionary( Prefix + c);
     Prefix = c;
    }
   }
   PrefixIndex = GetIndexCodingDictionary(Prefix);
   AddCodingCodeStream(PrefixIndex);
#if debugdisplay
   DisplayInCharStream();
   DisplayCodingCodeStream();
#if debugdictionary
   DisplayCodingDictionary();
#endif
#endif
  }
  
  #endregion
  
  #region Decode
  private ArrayList _DeCodeDictionary = new ArrayList();
  private ArrayList _OutCharStream = new ArrayList();
  private int[] _DeCodeCodeStream ;
  public void SetDeCodeSCodetream(int[] obj)
  {
   int count = obj.Length;
   _DeCodeCodeStream = new int[count];
   for(int i =0; i < count ; i++)
   {
    _DeCodeCodeStream[i] = obj[i];
   }
  }
  public void SetDeCodeSCodetream(ArrayList obj)
  {
   int count = obj.Count;
   _DeCodeCodeStream = new int[count];
   for(int i =0; i < count ; i++)
   {
    _DeCodeCodeStream[i] = System.Convert.ToInt32(obj[i]);
   }
  
  }
  public int[] GetDeCodeCodeStream()
  {
   return _DeCodeCodeStream;
  }
  public string OutCharStream
  {
   get
   {
    string result = "";
    for(int i = 0,count = _OutCharStream.Count; i < count; i++)
    {
     result += _OutCharStream[i].ToString();
    }
    return result;
   }
  }
  public ArrayList DeCodeDictionary
  {
   get
   {
    return _DeCodeDictionary;
   }
  }
  private void InitDeCodeDictionary()
  {
   _DeCodeDictionary.Clear();
#if debug
   _DeCodeDictionary.Add("A");
   _DeCodeDictionary.Add("B");
   _DeCodeDictionary.Add("C");
#else
   for(int i = 0; i < 256; i++)
   {
    _DeCodeDictionary.Add((char)i);
   }
#endif
  }
  private void InitOutCharStream()
  {
   _OutCharStream.Clear();
  }
  private void DisplayOutCharStream()
  {
   System.Console.WriteLine("*********_OutCharStream************");
   string temp = "";
   for(int i = 0; i < _OutCharStream.Count; i++)
   {
    temp = temp + (_OutCharStream[i].ToString());
   }
 
   System.Console.WriteLine(temp);
  }
  private void DisplayDeCodeDictionary()
  {
   System.Console.WriteLine("*********_DeCodeDictionary************");
   for(int i = 0; i < _DeCodeDictionary.Count; i++)
   {
    System.Console.WriteLine(_DeCodeDictionary[i].ToString());
   }
   
  }


  private void DisplayDeCodeCodeStream()
  {
   System.Console.WriteLine("*********_DeCodeCodeStream************");
   int count = _DeCodeCodeStream.Length;
   for(int i = 0; i < count; i++)
   {
    System.Console.WriteLine("{0}",_DeCodeCodeStream[i]);
   }
  }
  private void AddOutCharStream(object str)
  {
   _OutCharStream.Add(str);
  }
  private void AddDeCodeDictionary(object str)
  {
   _DeCodeDictionary.Add(str);
  }
  private bool ISInDeCodeDictionary(int cw)
  {
   bool result = false;
   int  count = _DeCodeDictionary.Count;
   if (cw <= count - 1)
   {
    result = true;
   }
   return result;
  }
  public void Decode()
  {
   InitDeCodeDictionary();
   InitOutCharStream();
   int cw = 0;
   int pw = 0;
   string Prefix = "";
   string c="";
   cw = _DeCodeCodeStream[0] - 1;
   this.AddOutCharStream(this._DeCodeDictionary[cw]);
   pw = cw;
   int count = _DeCodeCodeStream.Length;
   if (count == 0) return;
   for(int i = 1; i < count; i++)
   {
    cw = _DeCodeCodeStream[i] - 1;
    if (ISInDeCodeDictionary(cw))
    {
     this.AddOutCharStream(this._DeCodeDictionary[cw]);
     Prefix = this._DeCodeDictionary[pw].ToString();
     c = (this._DeCodeDictionary[cw].ToString())[0].ToString();
     this.AddDeCodeDictionary(Prefix + c);
    }
    else
    {
     Prefix = this._DeCodeDictionary[pw].ToString();
     c = Prefix[0].ToString();
     this.AddOutCharStream(Prefix + c);
     this.AddDeCodeDictionary(Prefix + c);
    }
    pw = cw;
   }
#if debugdisplay
   DisplayOutCharStream();
   DisplayDeCodeCodeStream();
#if debugdictionary
   DisplayDeCodeDictionary();
#endif
#endif
  }
  #endregion
 }
}

 

#undef debug
using System;

namespace LZW
{
 class Class1
 {
  [STAThread]
  static void Main(string[] args)
  {
   cLZW lzw = new cLZW();
   #if debug
   lzw.InCharStream = "ABBABABACCBBAAA";
   #else
   System.Console.WriteLine("Enter the Tests CharArray [a-zA-Z0-9]:");
   lzw.InCharStream = System.Console.ReadLine();
   #endif
   System.Console.WriteLine("The Coding ... ...");
   lzw.Coding();
   System.Console.WriteLine("The DeCode ... ...");
   lzw.SetDeCodeSCodetream(lzw.CodingCodeStream);
   lzw.Decode();
   System.Console.ReadLine();
  }
 }
}

