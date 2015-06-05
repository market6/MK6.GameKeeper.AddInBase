using System;
using System.AddIn;
using System.Timers;
using MK6.GameKeeper.AddIns;

namespace $rootnamespace$
{
	[AddIn(AddInName, Version = "1.0.0.0")]
	public class Service : GameKeeperAddIn
	{
		public const string AddInName = "";

		public Service()
		{
		}

		public virtual void Start()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual AddInStatus Status
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Execute(object sender, ElapsedEventArgs e)
		{
		}
	}
}