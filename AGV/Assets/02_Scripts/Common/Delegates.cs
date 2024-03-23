using System.Collections.Generic;

public class Delegates
{
	public delegate void VoidVoid();
	public delegate void VoidInt(in int _value);

	public delegate void VoidPlag(in Plag _plag);
	public delegate void VoidPlagPlag(in Plag _plag1, in Plag _plag2);
	public delegate void VoidPlagList(in List<Plag> _plagList);
	public delegate void VoidAction_VoidPlag(VoidPlag _delegate);
	public delegate void VoidBot(in Bot _bot);

	public delegate void VoidBotCreater(in BotCreater _botCreater);
}
