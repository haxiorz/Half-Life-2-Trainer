using System;
using System.Diagnostics;
using System.Threading;

namespace HL2Trainer
{
    class Program
    {
        private static int PistolAmmoOffset = 0x4AC;
        private static int HealthOffset = 0xE0;
        private static int ArmorOffset = 0xD30;
        private static int AUXOffset = 0x10CC;
        private static int MachineGunOffset = 0x4AC;

        static void Main(string[] args)
        {
            VAMemory vam = new VAMemory("hl2");
            int serverdllAddress = 0;
            int pointerHealth = 0x633AFC;
            int pointerArmor = 0x633AFC;
            int pointerAUX = 0x633AFC;

            Process[] p = Process.GetProcessesByName("hl2");

            if (p.Length > 0)
            {
                foreach (ProcessModule processModule in p[0].Modules)
                {
                    if (processModule.ModuleName == "server.dll")
                    {
                        serverdllAddress = (int)processModule.BaseAddress;
                    }
                }
            }

            while (true)
            {
                IntPtr firstAddressHealth = IntPtr.Add((IntPtr)serverdllAddress, pointerHealth);
                IntPtr firstAddressHealthValue = (IntPtr)vam.ReadInt32(firstAddressHealth);
                IntPtr healthAddress = IntPtr.Add(firstAddressHealthValue, HealthOffset);

                vam.WriteInt32((IntPtr)healthAddress, 1337);

                IntPtr firstAddressArmor = IntPtr.Add((IntPtr)serverdllAddress, pointerArmor);
                IntPtr firstAddressArmorValue = (IntPtr)vam.ReadInt32(firstAddressArmor);
                IntPtr armorAddress = IntPtr.Add(firstAddressArmorValue, ArmorOffset);

                vam.WriteInt32((IntPtr)armorAddress, 1337);

                IntPtr firstAddressAux = IntPtr.Add((IntPtr)serverdllAddress, pointerAUX);
                IntPtr firstAddressAuxValue = (IntPtr)vam.ReadInt32(firstAddressAux);
                IntPtr auxAddress = IntPtr.Add(firstAddressAuxValue, AUXOffset);

                vam.WriteInt32((IntPtr)auxAddress, 1120403456);

                Thread.Sleep(100);
            }
        }


    }
}