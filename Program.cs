using System;
using System.Management;

namespace CheckGPU
{
    class Program
    {
        static void Main(string[] args)
        {
            // Query the Win32_VideoController WMI class to get information about the graphics card
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_VideoController");

            // Get the first matching object (there may be multiple graphics cards in the system)
            ManagementObject obj = searcher.Get().Cast<ManagementObject>().FirstOrDefault();

            // Check if the object exists
            if (obj != null)
            {
                // Get the value of the AdapterCompatibility property
                string adapterCompatibility = obj["AdapterCompatibility"].ToString();
                Console.WriteLine(adapterCompatibility);
                // Check if the AdapterCompatibility property contains "Microsoft" or "Intel"
                // These are common indicators that the device does not have a physical GPU
                if (adapterCompatibility.Contains("Microsoft") || adapterCompatibility.Contains("Intel"))
                {
                    Console.WriteLine("Your device does not have a physical GPU.");
                }
                else
                {
                    Console.WriteLine("Your device has a physical GPU.");
                }
            }
            else
            {
                Console.WriteLine("Unable to retrieve graphics card information.");
            }
        }
    }
}
