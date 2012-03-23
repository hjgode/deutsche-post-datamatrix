#pragma warning disable 649
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PressePostEtikett
{
    public static class ByteUtil
    {
        #region unsafe
        /// <summary>
        /// read uint from 3 byte array
        /// </summary>
        /// <param name="bytes3"></param>
        /// <returns></returns>
        public static unsafe uint getUint(byte* bytes3)
        {
            uint uRet = 0;
            // 00 F7 DC must become 63452, 0xdcf70000 is wrong, 0000f7dc
            byte[] newBytes = new byte[4];
            newBytes[3] = 0x00;
            for (int i = 0; i < 3; i++)
            {
                newBytes[i] = bytes3[2 - i];
            }
            try
            {
                uint uNew = BitConverter.ToUInt32(newBytes, 0);
                //uNew = BitConverter.ToUInt32(new byte[] { 0xdc, 0xF7, 0x00, 0x00 }, 0); // correct
                uRet = uNew;
            }
            catch (Exception)
            {

            }
            return uRet;
        }
        /// <summary>
        /// read byte from byte*
        /// </summary>
        /// <param name="byte1"></param>
        /// <returns></returns>
        public static unsafe byte getByte1Val(byte* byte1)
        {
            byte bRet = 0;
            byte[] newBytes = new byte[2];
            newBytes[1] = 0x00;
            newBytes[0] = byte1[0];
            try
            {
                uint bNew = (uint)BitConverter.ToInt16(newBytes, 0);
                bRet = (byte)bNew;
            }
            catch (Exception)
            {

            }
            return bRet;
        }
        /// <summary>
        /// read uint from 2 bytes
        /// </summary>
        /// <param name="byte1"></param>
        /// <returns></returns>
        public static unsafe uint getByte2Val(byte* byte1)
        {
            uint bRet = 0;
            byte[] newBytes = new byte[2];
            newBytes[0] = byte1[0];
            newBytes[1] = byte1[1];
            try
            {
                uint bNew;// = (uint)BitConverter.ToInt16(newBytes, 0);
                bNew = (uint)(newBytes[0] * 0x100 + newBytes[1]);
                bRet = (uint)bNew;
            }
            catch (Exception)
            {

            }
            return bRet;
        }
        /// <summary>
        /// read string from byte array
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static unsafe string getString(byte* str, int len)
        {
            string sRet = "";
            try
            {
                byte[] bStr = new byte[len];
                for (int i = 0; i < len; i++)
                {
                    bStr[i] = str[i];
                }
                string s = System.Text.Encoding.UTF8.GetString(bStr, 0, len);
                sRet = s;
            }
            catch (Exception)
            {

            }
            return sRet;
        }
        #endregion
        /// <summary>
        /// read string from byte array
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string getString(byte[] str)
        {
            string sRet = "";
            try
            {

                string s = System.Text.Encoding.UTF8.GetString(str, 0, str.Length);
                sRet = s;
            }
            catch (Exception)
            {

            }
            return sRet;
        }
        /// <summary>
        /// read byte from byte[]
        /// </summary>
        /// <param name="byte1"></param>
        /// <returns></returns>
        public static byte getByte1Val(byte[] byte1)
        {
            byte bRet = 0;
            byte[] newBytes = new byte[2];
            newBytes[1] = 0x00;
            newBytes[0] = byte1[0];
            try
            {
                uint bNew = (uint)BitConverter.ToInt16(newBytes, 0);
                bRet = (byte)bNew;
            }
            catch (Exception)
            {

            }
            return bRet;
        }
        public static long getLongVal(byte[] byte1)
        {
            long uRet = BytetoLong(byte1);
            //byte[] bNew = new byte[byte1.Length];
            ////reverse bytes
            //for (int i = 0; i < byte1.Length; i++)
            //{
            //    bNew[i] = byte1[byte1.Length-1 - i];
            //}
            //uint uRet = 0;
            //for (int i = 1; i < bNew.Length; i++)
            //{
            //    uRet += (uint)(i * 0x100 * bNew[i]);
            //}
            return uRet;
        }
        private static string HexToAscii(byte[] hex)
        {
            string ascii = "";
            for (int i = 0; i < hex.Length; i++)
            {
                string temp = hex[i].ToString("X");
                if (temp.Length == 1)
                    temp = "0" + temp;
                ascii += temp;
            }
            return ascii;
        }

        private static long BytetoLong(byte[] Data)
        {
            string Array = HexToAscii(Data);
            long i = long.Parse(Array, System.Globalization.NumberStyles.HexNumber);
            return i;
        }
        public static decimal ByteToDec(byte[] bIn)
        {
            //the first byte is the euro value, the second byte is the cents
            decimal dRet = 0;
            dRet = bIn[0] * 100m + bIn[1] / 100m;
            return dRet;
        }
        public static DateTime getDate(int iDate)
        {
            int days = (iDate / 100);
            int year = (iDate % 100) + 2000;
            DateTime dt = new DateTime(year, 1, 1).AddDays(-1);
            dt = dt.AddDays(days);
            return dt;
        }
    }//class byteutil

    class Premiumadress
    {
        public string _kennung;
        public string _version;
        public string _preisversion;
        public string _kundennummer;
        public string _entgelt;
        public int _datum;
        public string _productkey;
        public string _sendungsnummer;
        public string _edsnummer;
        public string _datatype;
        public string _premiumID;
        public string _custominfo;

        /// <summary>
        /// if datatype==0x00
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct premiumadress
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]    
            public byte[] kennung;// 44,45,41 D E A
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]    
            public byte[] version;// 0x08 => 1
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]    // 
            public byte[] preisversion; //0x0d
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]    
            public byte[] kundennummer;// 01 30 A5 5D C7 => 51 1111 1111
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]    
            public byte[] entgelt;// 00055 -> 0x00 0x37 => 0,55 Euro
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]    
            public byte[] datum;// 0x50 0x17 => 20503 -> tag 205 im Jahr 2003
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] productkey;// 0x00 0x5A => Infopost Standard dez '90, 0x23 0xE7 => Infopost Prem Standard dez 91
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]    // 
            public byte[] sendungsnummer;   // 0x00, 0x00, 0x65,
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]    // 
            public byte[] edsnummer;        // 0x00, 0x00, 0x00, 0x01,
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]    // 
            public byte[] datatype;     // 00 => datacustom=keine postalischen Inhalte, customInfo 18 bytes
                                        // 01 => datacustom=Premium AdressID, customInfo 16 bytes
                                        // 02 => datacustom=Premium AdressID, customInfo 16 bytes
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]    // 
            //public byte[] premiumadress1;   // premiumID if dataytpe=0x01
            // ODER premiumadress mit 4 bytes
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]    // 
            public byte[] premiumID;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]    // 16 for datatype=0x01, 18 for datatype=0x00
            public byte[] custominfo;
        }

        /// <summary>
        /// if datatype!=0x00
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct premiumadress2
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] kennung;// 44,45,41 D E A
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] version;// 0x08 => 1
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]    // 
            public byte[] preisversion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] kundennummer;// 01 30 A5 5D C7 => 51 1111 1111
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] entgelt;// 00055 -> 0x00 0x37 => 0,55 Euro
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] datum;// 0x50 0x17 => 20503 -> tag 205 im Jahr 2003
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] productkey;// 0x00 0x5A => Infopost Standard dez '90, 0x23 0xE7 => Infopost Prem Standard dez 91
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]    // 
            public byte[] sendungsnummer;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]    // 
            public byte[] edsnummer;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]    // 
            public byte[] datatype;     // 00 => datacustom=keine postalischen Inhalte, customInfo 18 bytes
            // 01 => datacustom=Premium AdressID, customInfo 16 bytes
            // 02 => datacustom=Premium AdressID, customInfo 16 bytes
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]    // 
            //public byte[] premiumadress1;   // premiumID if dataytpe=0x01
            // ODER premiumadress mit 4 bytes
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]    // 
            public byte[] premiumID;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]    // 16 for datatype=0x01, 18 for datatype=0x00
            public byte[] custominfo;
        }
        public override string ToString()
        {
            string s = "";
            s += "Kennung: " + _kennung;
            s += "\r\nVersion: " + _version;
            s += "\r\nPreislistenversion: " + _preisversion;
            s += "\r\nKundennummer: " + _kundennummer;
            s += "\r\nEntgelt: " + _entgelt.ToString();

            s += "\r\nDatum: " + _datum.ToString() + ", " + ByteUtil.getDate(_datum).ToShortDateString();

            s += "\r\nProductkey: " + _productkey;
            s += "\r\nSendungsnummer: " + _sendungsnummer;
            s += "\r\nEDS-Nummer: " + _edsnummer;
            s += "\r\nDatatype: " + _datatype;
            s += "\r\nPremiumID: " + _premiumID;
            s += "\r\nCustomInfo: " + _custominfo;
            return s;
        }

        byte[] _bytes;
        premiumadress _premium;
        public Premiumadress(byte[] bData){
            _bytes=bData;
            if(bData.Length!=42)
                throw new ArgumentOutOfRangeException("Databytes dont match");

            //setup class properties
            _premium = DeSerialize(_bytes);
            _kennung = ByteUtil.getString(_premium.kennung);
            _version = ByteUtil.getByte1Val(_premium.version).ToString();
            _preisversion = ByteUtil.getByte1Val(_premium.preisversion).ToString();
            _kundennummer = ByteUtil.getLongVal(_premium.kundennummer).ToString();
            _entgelt = ByteUtil.ByteToDec(_premium.entgelt).ToString();
            _datum = (int)ByteUtil.getLongVal(_premium.datum);
            _productkey = ByteUtil.getLongVal(_premium.productkey).ToString();
            _sendungsnummer = ByteUtil.getLongVal(_premium.sendungsnummer).ToString();
            _edsnummer = ByteUtil.getLongVal(_premium.edsnummer).ToString();

            _datatype = ByteUtil.getLongVal(_premium.datatype).ToString();
            _premiumID = ByteUtil.getLongVal(_premium.premiumID).ToString();

            _custominfo = ByteUtil.getString(_premium.custominfo);
        } 
        premiumadress2 _premium2;
        public Premiumadress(byte[] bData, bool bAuto){
            _bytes=bData;
            if(bData.Length!=42)
                throw new ArgumentOutOfRangeException("Databytes dont match");
            
            //if (bData[23] != 0x00)

            //setup class properties
            _premium2 = DeSerialize2(_bytes);
            _kennung = ByteUtil.getString(_premium2.kennung);
            _version = ByteUtil.getByte1Val(_premium2.version).ToString();
            _preisversion = ByteUtil.getByte1Val(_premium2.preisversion).ToString();
            _kundennummer = ByteUtil.getLongVal(_premium2.kundennummer).ToString();
            _entgelt = ByteUtil.ByteToDec(_premium2.entgelt).ToString();
            _datum = (int)ByteUtil.getLongVal(_premium2.datum);
            _productkey = ByteUtil.getLongVal(_premium2.productkey).ToString();
            _sendungsnummer = ByteUtil.getLongVal(_premium2.sendungsnummer).ToString();
            _edsnummer = ByteUtil.getLongVal(_premium2.edsnummer).ToString();

            _datatype = ByteUtil.getLongVal(_premium2.datatype).ToString();
            _premiumID = ByteUtil.getLongVal(_premium2.premiumID).ToString();

            _custominfo = ByteUtil.getString(_premium2.custominfo);
        } 

        public premiumadress DeSerialize(byte[] rawdata)
        {
            premiumadress _prem;
            int rawsize = rawdata.Length;
            GCHandle handle = GCHandle.Alloc(rawdata, GCHandleType.Pinned);
            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();
                _prem = (premiumadress)Marshal.PtrToStructure(rawDataPtr, typeof(premiumadress));
            }
            finally            
            {
                handle.Free();    
            }
            
            return _prem;
        }
        public premiumadress2 DeSerialize2(byte[] rawdata)
        {
            premiumadress2 _prem;
            int rawsize = rawdata.Length;
            GCHandle handle = GCHandle.Alloc(rawdata, GCHandleType.Pinned);
            try
            {
                IntPtr rawDataPtr = handle.AddrOfPinnedObject();
                _prem = (premiumadress2)Marshal.PtrToStructure(rawDataPtr, typeof(premiumadress2));
            }
            finally            
            {
                handle.Free();    
            }
            
            return _prem;
        }
        public static byte[] testData = new byte[]{
                0x44, 0x45, 0x41, 
                0x08, 
                0x0d, 
                0x02, 0x54, 0x0b, 0xe3, 0xff, 
                0x00, 0x52, 
                0x23, 0x2d, 
                0x24, 0x2d,
                0x00, 0x00, 0x65, 
                0x00, 0x00, 0x00, 0x01, 
                0x01, 
                0x00, 0x01, 
                0x5a, 0x31, 0x30, 0x31, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
        public static byte[] testData2 = new byte[]{
                0x44,0x45,0x41, 
                0x08, 
                0x0d, 
                0x01, 0x30, 0xa5, 0x5d, 0xc7,
                0x00, 0x37,
                0x50, 0x17,
                0x00, 0x5a,
                0x00, 0x00, 0x65,
                0x00, 0x00, 0x00, 0x01,
                0x01,
                0x00, 0x01,
                0x5A, 0x31, 0x30, 0x31, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
        };//end class

    class PressepostMatrixcode
    {
        unsafe struct pressepostcode
        {
            public fixed byte kennung[3];    // byte 0 to 2
            public fixed byte version[1];           // byte 3
            public fixed byte preisversion[1];      // byte 4
            public fixed byte zeitungskennziffer[3];      // byte 5 to 7
            public fixed byte heftnummer[2];      // byte 8 to 9
            public fixed byte entgelt[2];         // byte 10 to 11 , always 00 00
            public fixed byte datum[2];           // byte 12 to 13
            public fixed byte productkey[2];           // byte 14 to 15
            public fixed byte premiumID[2];           // byte 16 to 17
            public fixed byte abonummer[24];           // byte 18 to 41
            public fixed byte plz[3];           // byte 42 to 44
            public fixed byte ort[24];           // byte 42 to 44
            public fixed byte strasse[22];           // byte 45 to 68
            public fixed byte hausnummer[10];
            public fixed byte name1[30];
            public fixed byte name2[30];
            public fixed byte name3[30];
            public fixed byte kundeninfo[11];
        }
        unsafe pressepostcode get(byte[] src)
        {
            fixed (byte* pb = &src[0])
            {
                return *(pressepostcode*)pb;
            }
        }

        //###########################################################################################
        byte[] b;
        byte[] bKennung = new byte[3] { 44, 45, 41 };// 'D', 'E', 'A' 
        public string _Kennung;
        public uint _Version;
        public uint _Preislistenversion;
        public uint _Kennziffer;
        public uint _Heftnummer;
        public uint _Entgelt;
        public uint _Datum;
        public uint _ProduktKey;
        public uint _PremiumID;
        public string _Abonummer;
        public uint _PLZ;
        public string _Ort;
        public string _Strasse;
        public string _Hausnummer;
        public string _Name1;
        public string _Name2;
        public string _Name3;
        public string _Kundeninfo;

        public override string ToString()
        {
            string s = "";
            s += "Kennung: " + _Kennung;
            s += "\r\nVersion: " + _Version.ToString();
            s += "\r\nPreislistenversion: " + _Preislistenversion.ToString();
            s += "\r\nKennziffer: " + _Kennziffer.ToString();
            s += "\r\nHeftnummer: " + _Heftnummer.ToString();
            s += "\r\nEntgelt: " + _Entgelt.ToString();
            s += "\r\nDatum: " + _Datum.ToString() + ", " + (_Datum % 100).ToString() + "/" + (_Datum / 100).ToString(); ;
            
            s += "\r\nDatum: " + _Datum.ToString() + ", " + ByteUtil.getDate((int)_Datum).ToShortDateString();

            s += "\r\nProduktKey: " + _ProduktKey.ToString();
            s += "\r\nPremiumID: " + _PremiumID.ToString();
            s += "\r\nAbonummer: " + _Abonummer;
            s += "\r\nPLZ: " + _PLZ.ToString() + " Ort: " + _Ort;
            s += "\r\nStrasse: " + _Strasse;
            s += "\r\nHausnummer: " + _Hausnummer;
            s += "\r\nName1: " + _Name1;
            s += "\r\nName2: " + _Name2;
            s += "\r\nName3: " + _Name3;
            s += "\r\nVermerk: " + _Kundeninfo;
            return s;
        }
        public PressepostMatrixcode(byte[] bData)
        {

            if (bData.Length != 202)
            {
                throw new ArgumentOutOfRangeException("Wrong data length");
            }
            b = bData;
            pressepostcode pcode = get(b);
            unsafe
            {                
                string sKennung = ByteUtil.getString(pcode.kennung, 3);
                _Kennung = sKennung;
                uint uVersion = ByteUtil.getByte1Val(pcode.version);
                _Version = uVersion;
                uint uPreisliste = ByteUtil.getByte1Val(pcode.preisversion);
                _Preislistenversion = uPreisliste;
                uint uKennziffer = ByteUtil.getUint(pcode.zeitungskennziffer);
                _Kennziffer = uKennziffer;
                uint uHeftNummer = ByteUtil.getByte1Val(pcode.heftnummer);
                _Heftnummer = uHeftNummer;
                uint uEntgelt = ByteUtil.getUint(pcode.entgelt);
                _Entgelt = uEntgelt;
                uint uDatum = ByteUtil.getByte2Val(pcode.datum);
                _Datum = uDatum;
                uint uProductKey = ByteUtil.getByte2Val(pcode.productkey);
                _ProduktKey = uProductKey;
                uint uPremiumID = ByteUtil.getByte2Val(pcode.premiumID);
                _PremiumID = uPremiumID;
                string sAbonummer = ByteUtil.getString(pcode.abonummer, 24);
                _Abonummer = sAbonummer;
                uint uPLZ = ByteUtil.getUint(pcode.plz);
                _PLZ = uPLZ;
                string sOrt = ByteUtil.getString(pcode.ort, 24);
                _Ort = sOrt;
                string sStrasse = ByteUtil.getString(pcode.strasse, 22);
                _Strasse = sStrasse;
                string sHausnummer = ByteUtil.getString(pcode.hausnummer, 10);
                _Hausnummer = sHausnummer;
                string sName1 = ByteUtil.getString(pcode.name1, 30);
                _Name1 = sName1;
                string sName2 = ByteUtil.getString(pcode.name2, 30);
                _Name2 = sName2;
                string sName3 = ByteUtil.getString(pcode.name3, 30);
                _Name3 = sName3;
                string sKundeninfo = ByteUtil.getString(pcode.kundeninfo, 11);
                _Kundeninfo = sKundeninfo;
            }//unsafe
        }

        public static byte[] testData =
            new byte[] { 
                0x44, 0x45, 0x41, //DEA, 3 Bytes
                0x19,             //Version, 1 Byte
                0x14,              //Preislistenversion, 1 Byte
                0x00, 0xF7, 0xDC,   //Zeitungskennziffer ZKZ 63452, 3 Bytes
                0x00, 0x98,         //Heftnummer, 2 Bytes
                0x00, 0x00,         //Frankierwert, 2Bytes
                0x50, 0x80,         //Datum: 20608 = 206. Tag in 2008, 2 Bytes, 
                0x24, 0x3F,         //ProduktschlÃ¼ssel, 24 F3 = Pressesendung E+0 mit Premiumadress Basis (9279), 2 Bytes
                0x00, 0x00,         //Premium-Adress ID, 2 Bytes

                //Abonnentennummer: ABC1234X, 24 Zeichen
                0x41, 0x42, 0x43, 0x31, 0x32, 0x33, 0x34, 0x58, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                
                0x00, 0xC5, 0xF0,   //plz: 50672, 3 Bytes
                
                //Ortsname: "KÃ¶ln", 24 Zeichen
                0x4b, 0xf6, 0x6c, 0x6e, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                
                //Strasse: "Postfach", 22 Zeichen
                0x50, 0x6f, 0x73, 0x74, 0x66, 0x61, 0x63, 0x68, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                
                //Hausnummer: 12345, 10 Zeichen
                0x31, 0x32, 0x33, 0x34, 0x35, 0x00, 0x00, 0x00, 0x00, 0x00, 

                //Name1: 30 Zeichen
                0x44, 0x65, 0x75, 0x74, 0x73, 0x63, 0x68, 0x65, 0x20, 0x50, 0x6f, 0x73, 0x74, 0x20, 0x41, 0x47, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                //Name2: 30 Zeichen
                0x44, 0x65, 0x75, 0x74, 0x73, 0x63, 0x68, 0x65, 0x20, 0x50, 0x6f, 0x73, 0x74, 0x20, 0x41, 0x47, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                //Name3: 30 Zeichen
                0x44, 0x65, 0x75, 0x74, 0x73, 0x63, 0x68, 0x65, 0x20, 0x50, 0x6f, 0x73, 0x74, 0x20, 0x41, 0x47, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                //KundenInfo: 11 Zeichen
                0x4b, 0x42, 0x30, 0x38, 0x31, 0x35, 0x2d, 0x31, 0x00, 0x00, 0x00
         };
        
    }//class PressepostMatrix
}


