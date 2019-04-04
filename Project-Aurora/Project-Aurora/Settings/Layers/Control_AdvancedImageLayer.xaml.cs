﻿using System;
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

namespace Aurora.Settings.Layers
{
    /// <summary>
    /// Interaction logic for Control_DefaultLayer.xaml
    /// </summary>
    public partial class Control_AdvancedImageLayer : UserControl
    {
        private bool settingsset = false;

        public Control_AdvancedImageLayer()
        {
            InitializeComponent();
        }

        public Control_AdvancedImageLayer(AdvancedImageLayerHandler datacontext)
        {
            InitializeComponent();

            this.DataContext = datacontext;
        }

        public void SetSettings()
        {
            if(this.DataContext is AdvancedImageLayerHandler && !settingsset)
            {
                UpdateTextBlock((this.DataContext as AdvancedImageLayerHandler).Properties._ImagePath);
                this.advanced_image_primary_color_colorpicker.SelectedColor = Utils.ColorUtils.DrawingColorToMediaColor((this.DataContext as AdvancedImageLayerHandler).Properties._PrimaryColor ?? System.Drawing.Color.Empty);
                this.advanced_image_secondary_color_colorpicker.SelectedColor = Utils.ColorUtils.DrawingColorToMediaColor((this.DataContext as AdvancedImageLayerHandler).Properties._SecondaryColor ?? System.Drawing.Color.Empty);
                this.KeySequence_keys.Sequence = (this.DataContext as AdvancedImageLayerHandler).Properties._Sequence;

                settingsset = true;
            }
        }

        private void UpdateTextBlock(string path)
        {
            if(String.IsNullOrWhiteSpace(path))
                this.txtBlk_seletedImage.Text = "Selected Image: None";
            else
                this.txtBlk_seletedImage.Text = $"Selected Image: {System.IO.Path.GetFileName(path)}";
        }

        private void btn_SelectImage_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.png, *.gif, *.bmp, *.tiff, *.tif) | *.jpg; *.jpeg; *.jpe; *.png; *.gif; *.bmp; *.tiff; *.tif";
            dialog.Title = "Please select an image.";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if(File.Exists(dialog.FileName))
                {
                    if (IsLoaded && settingsset && this.DataContext is AdvancedImageLayerHandler)
                    {
                        (this.DataContext as AdvancedImageLayerHandler).Properties._ImagePath = dialog.FileName;
                        UpdateTextBlock((this.DataContext as AdvancedImageLayerHandler).Properties._ImagePath);
                    }
                }
            }
        }

        private void ColorPicker_primaryColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (IsLoaded && settingsset && this.DataContext is AdvancedImageLayerHandler && sender is Xceed.Wpf.Toolkit.ColorPicker && (sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.HasValue)
                (this.DataContext as AdvancedImageLayerHandler).Properties._PrimaryColor = Utils.ColorUtils.MediaColorToDrawingColor((sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.Value);
        }

        private void KeySequence_keys_SequenceUpdated(object sender, EventArgs e)
        {
            if (IsLoaded && settingsset && this.DataContext is AdvancedImageLayerHandler && sender is Aurora.Controls.KeySequence)
                (this.DataContext as AdvancedImageLayerHandler).Properties._Sequence = (sender as Aurora.Controls.KeySequence).Sequence;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetSettings();

            this.Loaded -= UserControl_Loaded;
        }

        private void advanced_image_primary_color_colorpicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (IsLoaded && settingsset && this.DataContext is AdvancedImageLayerHandler && sender is Xceed.Wpf.Toolkit.ColorPicker && (sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.HasValue)
            {
                (this.DataContext as AdvancedImageLayerHandler).Properties._PrimaryColor = Utils.ColorUtils.MediaColorToDrawingColor((sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.Value);
            }
        }

        private void advanced_image_secondary_color_colorpicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (IsLoaded && settingsset && this.DataContext is AdvancedImageLayerHandler && sender is Xceed.Wpf.Toolkit.ColorPicker && (sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.HasValue)
            {
                (this.DataContext as AdvancedImageLayerHandler).Properties._SecondaryColor = Utils.ColorUtils.MediaColorToDrawingColor((sender as Xceed.Wpf.Toolkit.ColorPicker).SelectedColor.Value);
            }
        }

    }
}
