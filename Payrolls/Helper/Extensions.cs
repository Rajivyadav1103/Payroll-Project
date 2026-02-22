namespace Payrolls.Helper
{
    public static class Extensions
    {
        public static int ToNumber(this object o)  // o = 123
        {
            try
            {
                return Convert.ToInt32(o);
            }
            catch
            {
                return 0;
            }
        }

        public static string ToText(this object o)
        {
            try
            {
                string s = Convert.ToString(o);
                if (string.IsNullOrEmpty(s))
                {
                    return "";
                }
                else return s;
            }
            catch
            {
                return "";
            }
        }


    }
}
