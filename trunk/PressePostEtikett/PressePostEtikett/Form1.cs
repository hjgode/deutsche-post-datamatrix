//#define EMULATOR
//use the above for testing the app in an emulator or on a device without BarcodeReader
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#if ! EMULATOR
using Intermec.DataCollection;
#endif

namespace PressePostEtikett
{
    public partial class Form1 : Form
    {
#if !EMULATOR
        BarcodeReader bcr;
#endif
        public Form1()
        {
            InitializeComponent();
#if !EMULATOR
            try
            {
                bcr = new BarcodeReader();
                bcr.BarcodeRead += new BarcodeReadEventHandler(bcr_BarcodeRead);
                bcr.symbologyOptions.Preamble = "";
                bcr.symbologyOptions.Postamble = "";
                
                bcr.symbology.Datamatrix.Enable = true;
                bcr.ThreadedRead(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not start BarcodeReader. Are the runtimes installed? Ex=" + ex.Message);
            }
#endif
        }
#if ! EMULATOR

        void bcr_BarcodeRead(object sender, BarcodeReadEventArgs bre)
        {
            byte[] b = bre.DataBuffer;
            int bLen = b.Length;
            if (bLen < 3)
            {
                textBox1.Text = "Content to short";
                return;
            }
            if(!(b[0]=='D' && b[1]=='E' && b[2]=='A'))
            {
                textBox1.Text = "Header DEA not matched";
                return;
            }

            if (bLen == 202)
            {
                PressepostMatrixcode pm = new PressepostMatrixcode(b);
                textBox1.Text = "PressepostMatrixcode\r\n" + pm.ToString();
                return;
            }
            else if (bLen == 42)
            {
                if (b[23] == 0x00)
                {
                    //decode with 18 bytes custominfo
                    Premiumadress pa = new Premiumadress(b, true);
                    textBox1.Text = "Premiumadress 0x00\r\n" + pa.ToString();
                    return;
                }
                else
                {
                    //decode with 16 bytes custominfo
                    Premiumadress pa = new Premiumadress(b);
                    textBox1.Text = "Premiumadress\r\n" + pa.ToString();
                    return;
                }
            }
            textBox1.Text="No Deutsche Post code recognized";
        }
#endif
        private void menuItem1_Click(object sender, EventArgs e)
        {
#if ! EMULATOR
            if (bcr != null)
            {
                //bcr.ThreadedRead(false); // will this disable the HardwareTrigger?
                bcr.BarcodeRead -= bcr_BarcodeRead;
                bcr.Dispose();
                bcr = null;
            }
#endif
            Application.Exit();
        }

        private void mnuPremiumAdress_Click(object sender, EventArgs e)
        {
            Premiumadress pa = new Premiumadress(Premiumadress.testData);
            textBox1.Text = pa.ToString();            
        }

        private void mnuPressepostTest_Click(object sender, EventArgs e)
        {
            PressepostMatrixcode pm = new PressepostMatrixcode(PressepostMatrixcode.testData);
            textBox1.Text = pm.ToString();
        }

        private void mnuPremiumAdress0_Click(object sender, EventArgs e)
        {
            string s = "";
            for (int i = 0; i < Premiumadress.testData2.Length; i++)
            {
                s += Premiumadress.testData2[i].ToString("^000");
            }
            System.Diagnostics.Debug.WriteLine(s);
            Premiumadress pa = new Premiumadress(Premiumadress.testData, true);
            textBox1.Text = pa.ToString();
        }
    }
}