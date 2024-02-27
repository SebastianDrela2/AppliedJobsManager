﻿using AppliedJobsManager.JsonProcessing;
using System.Windows;
using System.Windows.Controls;
using System.Drawing.Text;

namespace AppliedJobsManager.UI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly Settings.Settings _settings;
        private readonly DataGrid _dataGrid;
        public SettingsWindow(Settings.Settings settings, JsonSettingsManager jsonSettingsManager, DataGrid dataGrid)
        {
            InitializeComponent();

            _settings = settings;
            _jsonSettingsManager = jsonSettingsManager;
            _dataGrid = dataGrid;

            SetUI();
        }

        private void SetUI()
        {
            _invalidRowsCheckBox.IsChecked = _settings.RemoveInvalidRows;
            _saveColumnsWidthsCheckBox.IsChecked = _settings.SaveColumnWidths;

            PopulateFonts();
            _fontsComboBox.SelectedItem = GetFont();

        }

        private void PopulateFonts()
        {
            using var installedFonts = new InstalledFontCollection();

            foreach(var fontFamily in installedFonts.Families)
            {
                _fontsComboBox.Items.Add(fontFamily.Name);
            }            
        }

        private string GetFont()
        {
            if (!string.IsNullOrEmpty(_settings.Font))
            {
                return _settings.Font;
            }

            return "Arial";
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var settingsToSave = new Settings.Settings
            {
                RemoveInvalidRows = (bool)_invalidRowsCheckBox.IsChecked!,
                SaveColumnWidths = (bool)_saveColumnsWidthsCheckBox.IsChecked!,
                ColumnsWidths = _dataGrid.Columns.Select(x => x.ActualWidth).ToList(),
                Font = _fontsComboBox.SelectedItem.ToString()!               
            };

            _jsonSettingsManager.SaveSettings(settingsToSave);

            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
