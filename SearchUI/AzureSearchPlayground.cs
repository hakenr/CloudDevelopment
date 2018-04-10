using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchUI
{
    public partial class AzureSearchPlayground : Form
    {
        public AzureSearchPlayground()
        {
            InitializeComponent();
        }

        private void ButtonIndex_Click(object sender, EventArgs e)
        {
            Article article = new Article
            {
                Title = InputTitle.Text,
                Category = InputCategory.Text,
                Text = InputText.Text
            };

            // TODO: index new item
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            // TODO: get results from DB

            ResultGrid.DataSource = new List<Article>
            {
                new Article {Title = "art 1", Category = "ctg 1", Text = "ddd"},
                new Article {Title = "art 2", Category = "ctg 4", Text = "ccc"},
            };
        }
    }
}
