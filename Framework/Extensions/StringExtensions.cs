namespace ConnectUO.Framework.Extensions
{
    public static class StringExtensions
    {
        public static string Remove(this string str, string phrase)
        {
            int index = -1;
            while ((index = str.IndexOf(phrase)) != -1)
            {
                str = str.Remove(index, phrase.Length);
            }

            return str;
        }
    }
}
