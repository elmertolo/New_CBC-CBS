using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;
using System.IO;



//using Rebex.IO.Compression;

namespace New_CBC_CBS.Services
{
    class ZipFileServices
    {
        public void CreateZipFile(string _sourcePath, string _destinationPath)
        {

            ZipFile.CreateFromDirectory(_sourcePath, _destinationPath);
        }
        public void ExtractZipFile(string sourcePath, string destinationPath)
        {

            ZipFile.ExtractToDirectory(sourcePath, destinationPath);
        }

        public void ZipFileS(string _processby, CBS main)
        {
         
            string sPath = Application.StartupPath +"\\"+ Form1.banks+"\\Output\\" + CBS.outputFolder ;
             string dPath = Application.StartupPath + "\\" + Form1.banks + "\\AFT_"+Form1.banks.ToUpper() + "_"+main.batchfile+ "_" + _processby  +"_"+CBS.outputFolder.ToUpper()+ ".zip";
           
            //deleting existing file
            if (File.Exists(dPath))
                File.Delete(dPath);
            //create zip file
                ZipFile.CreateFromDirectory(sPath, dPath);

            Ionic.Zip.ZipFile zips = new Ionic.Zip.ZipFile(dPath);
            //Adding order file to zip file
            zips.AddItem(Application.StartupPath + "\\" + Form1.banks+"\\Head");
          
            zips.Save();
            DeleteSQl();

        }


        public void CopyZipFile(string _processby, CBS main)
        {
            string dPath =@" C:\Users\CaptiveIT1\Documents\Temp" + @"\AFT_" + main.batchfile + "_" + _processby + "_" + CBS.outputFolder.ToUpper() + ".zip";
            string sPath = Application.StartupPath + "\\" + Form1.banks + "\\AFT_" + Form1.banks.ToUpper() + "_" + main.batchfile + "_" + _processby + "_" + CBS.outputFolder.ToUpper() + ".zip";
            File.Copy(sPath, dPath, true);
            //string dPath2 = "\\\\192.168.0.254\\PrinterFiles\\ISLA\\2019\\";
            //string sPath2 = "\\\\192.168.0.254\\captive\\Auto\\IslaBank\\Test\\";

        }
        public void DeleteSQl()
        {

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\" + Form1.banks + @"\Output\" + CBS.outputFolder);
            FileInfo[] files = di.GetFiles("*.SQL")
                     .Where(p => p.Extension == ".SQL").ToArray();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
        }
    }
}
