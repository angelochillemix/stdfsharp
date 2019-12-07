///**
// * $Id$
// * 
// * STDFSharp - Reading/writing STDF (Standard Test Data Format) library for .NET
// *
// * File: 
// * Description:
// * 
// * Copyright (C) 2006 Outburst <outburst@users.sourceforge.net>
// *  
// * This library is free software; you can redistribute it and/or 
// * modify it under the terms of the GNU Lesser General Public License 
// * as published by the Free Software Foundation; either version 2.1 
// * of the License, or (at your option) any later version.
// * 
// * This library is distributed in the hope that it will be useful, 
// * but WITHOUT ANY WARRANTY; without even the implied warranty of 
// * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// * GNU Lesser General Public License for more details.
// * 
// * You should have received a copy of the GNU Lesser General Public License
// * along with this library; if not, write to the Free Software Foundation, 
// * Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// */

using System;
using System.IO;
using System.Text;
using System.Diagnostics;

using NUnit.Framework;

using KA.StdfSharp.Compression;

namespace KA.StdfSharp.Tests.Compression
{

	[TestFixture]
	public class TestLZWCompression
	{
		private static readonly string TestDirectory = Path.GetFullPath(@"../../../../../tests/compression/");
		private static readonly string InputFile = TestDirectory + @"Input.txt";
		private static readonly string CompressedFile = TestDirectory + @"Compressed.txt";
		private static readonly string DecompressedFile = TestDirectory + @"Decompressed.txt";
		
		private const string TestText = @"Compression/Decompression Test";
		
		[SetUp]
		public void Init()
		{
			CleanUp();
			CreateInputFile();
		}
		
		private void CreateInputFile()
		{
			FileStream outputStream = new FileStream(InputFile, FileMode.Create);
			TextWriter writer = new StreamWriter(outputStream);
			writer.WriteLine(TestText);
			writer.Close();
			outputStream.Close();
			Assert.IsTrue(File.Exists(InputFile));
		}

		[TearDown]
		public void CleanUp()
		{
			File.Delete(InputFile);
			File.Delete(CompressedFile);
			File.Delete(DecompressedFile);
		}
		
		[Test]
        public void TestCompress()
        {
            FileStream inputStream = new FileStream(InputFile, FileMode.Open);
            StreamReader inputReader = new StreamReader(inputStream);

            FileStream outputStream = new FileStream(CompressedFile, FileMode.Create);
            BinaryWriter outputWriter = new BinaryWriter(outputStream);
			
			LZWCompression compression = new LZWCompression();

			compression.Compress(inputReader, outputWriter);
			
            outputWriter.Close();
            outputStream.Close();

            inputReader.Close();
            inputStream.Close();
			
			Assert.IsTrue(File.Exists(CompressedFile));
			Assert.Less(File.ReadAllBytes(InputFile).Length, File.ReadAllBytes(CompressedFile).Length);
        }

		[Test]
        public void TestDecompress()
        {
			TestCompress();
			
            FileStream inputStream = new FileStream(CompressedFile, FileMode.Open);
            BinaryReader inputReader = new BinaryReader(inputStream);

            FileStream outputStream = new FileStream(DecompressedFile, FileMode.Create);
            StreamWriter outputWriter = new StreamWriter(outputStream, Encoding.ASCII);

			LZWCompression compression = new LZWCompression();
			compression.Decompress(inputReader,outputWriter);
			
            outputWriter.Close();
            outputStream.Close();

            inputReader.Close();
            inputStream.Close();

            TextReader streamReader = new StreamReader(DecompressedFile);
			
			string decompressedText = streamReader.ReadLine();
			
			streamReader.Close();
			
			Assert.AreEqual(TestText, decompressedText);
			
			Assert.IsTrue(File.Exists(DecompressedFile));
        }
	}
}
