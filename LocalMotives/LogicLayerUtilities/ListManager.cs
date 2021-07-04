using DataAccessUtilities;
using DataObject;
using System;
using System.Collections.Generic;

namespace LogicLayerUtilities
{
	public static class ListManager
    {
        public static List<string> GetTimes()
        {
			try
			{
				return ListAccessor.RetrieveHours();
			}
			catch (Exception ex)
			{

				throw new ApplicationException("Times Not Found.",ex);
			}
        }

        //Pass an array
		public static int[] Sort(int[] lines)
        {
            quickSort(lines,0,lines.Length-1);
            return lines;
		}

        private static void quickSort(int[] list, int low, int high)
        {
			if(low < high)
            {
                // pivot index
				int pivot = Split(list, low, high);

                if (pivot > 1)
                {
					quickSort(list, low, pivot - 1);
                }
				if(pivot+1 < high)
                {
					quickSort(list, pivot + 1, high);
                }
            }
        }

        private static int Split(int[] list, int low, int high)
        {
            // Item at pivot
            int pivot = list[low];
            while (true)
            {
                while(list[low] < pivot)
                {
                    low++;
                }
                while(list[high] > pivot)
                {
                    high--;
                }
                if (low < high)
                {
                    if (list[low] == list[high])
                    {
                        return high;
                    }

                    //simple swap
                    int temp = list[low];
                    list[low] = list[high];
                    list[high] = temp;
                }
                else
                {
                    return high;
                }

            }
        }
    }
}
