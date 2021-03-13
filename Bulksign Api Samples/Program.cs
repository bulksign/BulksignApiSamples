using System;

namespace Bulksign.ApiSamples
{
	class Program
	{
		static void Main(string[] args)
		{
			new SingleDocumentSingleSigner().SendEnvelope();

			Console.ReadLine();
		}
	}
}