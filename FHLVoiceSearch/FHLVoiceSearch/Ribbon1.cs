using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FHLVoiceSearch
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            //MessageBox.Show("Hi there! :)");
            //Globals.ThisAddIn.Application.ActiveExplorer().Search("from:Raj", Microsoft.Office.Interop.Outlook.OlSearchScope.olSearchScopeAllFolders);


            /*MailItem mailItem = (MailItem)Globals.ThisAddIn.Application.CreateItem(OlItemType.olMailItem);
            
            mailItem.Display(true);*/

            VoiceSearch form = new VoiceSearch();
            form.Show();
        }
    }
}
