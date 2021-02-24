using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GrassWrapper;
using GrassWrapper.Parameter;
using GrassWrapper.View;

namespace GrassWrapperTestWithUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public GrassCommand GrassCommand { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            var descriptionFilesDir = @"C:\Users\Administrator\Desktop\description";
            var files = Directory.GetFiles(descriptionFilesDir, "*.txt");
            List<GrassCommand> commands = new List<GrassCommand>();
            foreach (var file in files)
            {
                commands.Add(new GrassCommand(file));
            }

            GrassCommand = commands.First(p=>p.Parameters.Any(t=> t is QgsProcessingParameterEnum &&(t as QgsProcessingParameterEnum).AllowMultiple));
            var groups = commands.GroupBy(t => t.Group);
            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Key}");
                foreach (var c in g)
                {
                    Console.WriteLine($"{g.Key}:{c.Name}\t{c.Description}");
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            GrassCommandView view=new GrassCommandView(GrassCommand);
            view.ShowDialog();
            Console.WriteLine(GrassCommand);
        }
    }
}
