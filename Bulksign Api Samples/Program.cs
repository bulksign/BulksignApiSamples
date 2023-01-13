using System;

namespace Bulksign.ApiSamples
{
	class Program
	{
		static void Main(string[] args)
		{
			new PrepareSendEnvelopeWithTags().SendEnvelope();
			
			//add whichever sample you want to call here , for example : 
			//new  GetUserContacts().GetContacts();
			

			Console.ReadLine();
		}
	}
}