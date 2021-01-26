namespace Blep
{
    public static class BoiCustom
    {
        public static bool BOIC_Bytearr_Compare(byte[] a, byte[] b)
        {
            if (a == null || b == null) return false;
            if (a.Length == 0 || b.Length == 0) return false;
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
    }
}
