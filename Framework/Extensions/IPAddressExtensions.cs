using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ConnectUO.Framework.Extensions
{
	public static class IPAddressExtensions
	{
		private static Dictionary<IPAddress, IPAddress> _ipAddressTable;

		public static IPAddress Intern( this IPAddress ipAddress )
		{
			if ( _ipAddressTable == null )
			{
				_ipAddressTable = new Dictionary<IPAddress, IPAddress>();
			}

			IPAddress interned;

			if ( !_ipAddressTable.TryGetValue( ipAddress, out interned ) )
			{
				interned = ipAddress;
				_ipAddressTable[ipAddress] = interned;
			}

			return interned;
		}
	}
}
