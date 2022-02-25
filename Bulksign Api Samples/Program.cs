using System;

namespace Bulksign.ApiSamples
{
	class Program
	{
		static void Main(string[] args)
		{
			new SingleDocumentApproverAndSigner().SendEnvelope();

			Console.ReadLine();
		}
	}
}