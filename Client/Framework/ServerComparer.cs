using System.Collections.Generic;
using ConnectUO.Data;

namespace ConnectUO.Framework
{

    public class ServerComparer : IComparer<Server>
    {
        private ServerListOrderBy _orderBy = ServerListOrderBy.Id;
        private bool _reverse = false;

        public bool Reverse
        {
            get { return _reverse; }
            set { _reverse = value; }
        }

        public ServerListOrderBy OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }

        public int Compare(Server x, Server y)
        {
            if (Reverse)
            {
                Server z = x;

                x = y;
                y = z;
            }

            switch (OrderBy)
            {
                default:
                    {
                        //Local server id's go backward.
                        if (!x.Public)
                        {
                            return y.Id.CompareTo(x.Id);
                        }

                        return x.Id.CompareTo(y.Id);
                    }
                case ServerListOrderBy.Description:
                    return x.Description.CompareTo(y.Description);
                case ServerListOrderBy.Name:
                    return x.Name.CompareTo(y.Name);
                case ServerListOrderBy.AvgOnline:
                    return y.AvgOnline.CompareTo(x.AvgOnline);
                case ServerListOrderBy.CurOnline:
                    return y.CurOnline.CompareTo(x.CurOnline);
                case ServerListOrderBy.Era:
                    return x.Era.CompareTo(y.Era);
                case ServerListOrderBy.Lang:
                    return x.Lang.CompareTo(y.Lang);
                case ServerListOrderBy.MaxOnline:
                    return y.MaxOnline.CompareTo(x.MaxOnline);
                case ServerListOrderBy.Status:
                    return x.Status.CompareTo(y.Status);
                case ServerListOrderBy.ShardType:
                    return x.ShardType.CompareTo(y.ShardType);
                case ServerListOrderBy.ClientVersion:
                    return x.ServerClientVersion.CompareTo(y.ServerClientVersion);
            }
        }
    }
}
