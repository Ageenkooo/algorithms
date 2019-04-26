using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace package
{
    public class Package
    {
        public List<double> packages = new List<double>();
        private Dictionary<int, List<double>> packagesList = new Dictionary<int, List<double>>();

        public Package()
        {
            packages = new List<double>();
            packagesList = new Dictionary<int, List<double>>();
        }

        private void AddItemToPackageList(int packageNumber, double packageItem)
        {
            if (packagesList.Count == packageNumber)
            {
                packagesList.Add(packageNumber, new List<double>() { packageItem });
            }
            else
            {
                packagesList[packageNumber].Add(packageItem);
            }
        }

        public void AddItem(double item)
        {
            if (item > 1 || item < 0)
            {
                Console.WriteLine("Not from 0 to 1");
                return;
            }

            bool flag = false;
            for (int i = 0; i < packages.Count; i++)
            {
                if (packages[i] - item >= 0)
                {
                    packages[i] = packages[i] - item;
                    flag = true;
                    AddItemToPackageList(i, item);
                    break;
                }
            }

            if (!flag)
            {
                packages.Add(1 - item);
                AddItemToPackageList(packagesList.Count, item);
            }
        }

        public void ShowPacksLoad()
        {
            foreach (var el in packagesList)
            {
                Console.Write("Package: " + el.Key + "; package items: ");
                foreach (var itemValues in el.Value)
                {
                    Console.Write(itemValues + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
