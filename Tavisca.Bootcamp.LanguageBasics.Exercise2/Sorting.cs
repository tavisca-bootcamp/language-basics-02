using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Sorting
    {

        public static void MergeSort(List<string> timeOfPosting, List<string> timeOfShowing, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;
                MergeSort(timeOfPosting, timeOfShowing, l, m);
                MergeSort(timeOfPosting, timeOfShowing, m + 1, r);
                Merge(timeOfPosting, timeOfShowing, l, m, r);
            }
        }

        public static void Merge(List<string> timeOfPosting, List<string> timeOfShowing, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;
            string[] pa = new string[n1];
            string[] sa = new string[n1];
            string[] pb = new string[n2];
            string[] sb = new string[n2];
            for (int i = l; i <= m; i++)
            {
                pa[i - l] = timeOfPosting[i];
                sa[i - l] = timeOfShowing[i];
            }
            for (int i = m + 1; i <= r; i++)
            {
                pb[i - m - 1] = timeOfPosting[i];
                sb[i - m - 1] = timeOfShowing[i];
            }

            int x = 0, y = 0, z = 0;
            while (x < n1 && y < n2)
            {
                if (!ShowLess(sa[x], sb[y]))
                {
                    timeOfPosting[z] = pa[x];
                    timeOfShowing[z] = sa[x];
                    x++;
                }
                else
                {
                    timeOfPosting[z] = pb[y];
                    timeOfShowing[z] = sb[y];
                    y++;
                }
                z++;
            }

            while (x < n1)
            {
                timeOfPosting[z] = pa[x];
                timeOfShowing[z] = sa[x];
                x++;
                z++;
            }
            while (y < n2)
            {
                timeOfPosting[z] = pb[y];
                timeOfShowing[z] = sb[y];
                y++;
                z++;
            }

        }

        public static bool ShowLess(string a, string b)
        {
            if (a.Equals(b) || a.Substring(0, 1).Equals("f"))
                return true;
            if (b.Substring(0, 1).Equals("f"))
                return false;

            int i = a.IndexOf(' ');
            int j = b.IndexOf(' ');

            if (a.Substring(i + 1).Equals(b.Substring(j + 1)))
            {
                int x = Convert.ToInt32(a.Substring(0, i));
                int y = Convert.ToInt32(b.Substring(0, j));
                if (x <= y)
                    return true;
                return false;
            }
            else if (a.Substring(i + 1, 1).Equals("m"))
                return true;

            return false;
        }

    }
}
