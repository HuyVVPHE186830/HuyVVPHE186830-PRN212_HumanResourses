using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for OvertimeDialog.xaml
    /// </summary>
    public partial class OvertimeDialog : Window
    {
        public int OvertimeHours { get; set; } 

        public OvertimeDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtOvertime.Text, out int overtime))
            {
                OvertimeHours = overtime;
                DialogResult = true;  
            }
            else
            {
                MessageBox.Show("Please enter a valid number of overtime hours.");
            }
        }
    }
}
