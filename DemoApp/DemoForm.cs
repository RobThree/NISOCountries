using NISOCountries.Core;
using NISOCountries.GeoNames;
using NISOCountries.Ripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
            typeSelection.SelectedIndex = 0;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            IEnumerable<IISOCountry> countries = null;
            switch (typeSelection.SelectedIndex)
            {
                case 0:
                    countries = new RipeISOCountryReader().GetDefault();
                    break;
                case 1:
                    countries = new GeonamesISOCountryReader().GetDefault();
                    break;
                case 2:
                    countries = new NISOCountries.Wikipedia.HAP.WikipediaISOCountryReader().GetDefault();
                    break;
                case 3:
                    countries = new NISOCountries.Datahub.DatahubISOCountryReader().GetDefault();
                    break;
            }
            dataGridView.DataSource = countries.OrderBy(c => c.Alpha2).ToArray();
            dataGridView.AutoResizeColumns();
        }
    }
}
