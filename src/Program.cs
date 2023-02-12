using System;

namespace Bulksign.ApiSamples
{
	class Program
	{
		static void Main(string[] args)
		{
			new GetDrafts().RunSample();
			
			//add whichever sample you want to call here , for example : 
			//new  GetUserContacts().RunSample();
			

			Console.ReadLine();
		}
	}
}