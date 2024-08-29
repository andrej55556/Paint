using PluginInterface;
using System.Reflection;
using System.Windows.Forms;

namespace MainApp
{
    public partial class PictureForm : Form
    {
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        public PictureForm()
        {
            InitializeComponent();
            FindPlugins();
            CreatePluginsMenu();
        }

        void FindPlugins()
        {
            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;

            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform((Bitmap)pictureBox.Image);
            pictureBox.Refresh();
        }

        void CreatePluginsMenu()
        {
            foreach (IPlugin p in plugins.Values)
            {
                var menuItem = new ToolStripMenuItem(p.Name);
                menuItem.Click += OnPluginClick;

                плагиныToolStripMenuItem.DropDownItems.Add(menuItem);

                menuItem.DropDownItems.Add(p.Author);

                VersionAttribute versionAttribute = (VersionAttribute)Attribute.GetCustomAttribute(p.GetType(), typeof(VersionAttribute));
                menuItem.DropDownItems.Add(versionAttribute.Major.ToString() + "." + versionAttribute.Minor.ToString());
            }
        }
    }
}
