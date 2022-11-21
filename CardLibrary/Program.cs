using CardLibrary;
using MemoryGame;
using System;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Enter card amount");
		string _str = Console.ReadLine();
		int Input = Int32.Parse(_str);
		CardGame game = new CardGame(Input);
		game.Run();
	}
}
