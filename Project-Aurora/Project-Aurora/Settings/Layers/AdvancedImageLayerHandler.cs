using Aurora.EffectsEngine;
using Aurora.Profiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging; 
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Aurora.Settings.Layers
{
    public class AdvancedImageLayerHandlerProperties : LayerHandlerProperties2Color<AdvancedImageLayerHandlerProperties>
    {
        public string _ImagePath { get; set; }
        public Color _PrimaryColor { get; set; }
        public Color _SecondaryColor { get; set; }

        [JsonIgnore]
        public string ImagePath { get { return Logic._ImagePath ?? _ImagePath; } }

        public AdvancedImageLayerHandlerProperties() : base() { }

        public AdvancedImageLayerHandlerProperties(bool assign_default = false) : base(assign_default) { }

        public override void Default()
        {
            base.Default();
            this._ImagePath = "";
        }
    }

    public class AdvancedImageLayerHandler : LayerHandler<AdvancedImageLayerHandlerProperties>
    {
        private EffectLayer temp_layer;
        private System.Drawing.Image _loaded_image = null;
        private string _loaded_image_path = "";

        public AdvancedImageLayerHandler()
        {
            _ID = "Image";
        }

        protected override UserControl CreateControl()
        {
            return new Control_AdvancedImageLayer(this);
        }

        public override EffectLayer Render(IGameState gamestate)
        {
            EffectLayer image_layer = new EffectLayer();

            if (!String.IsNullOrWhiteSpace(Properties.ImagePath))
            {

                if (!_loaded_image_path.Equals(Properties.ImagePath))
                {
                    //Not loaded, load it!
                    if (!File.Exists(Properties.ImagePath))
                        throw new FileNotFoundException("Could not find file specified for layer: " + Properties.ImagePath);

                    Bitmap loadimg = new Bitmap(Properties.ImagePath);

                    // iterate over frames for animated GIFs, regular images should just do one loop
                    for (int z = 0; z < loadimg.GetFrameCount(FrameDimension.Time); z++)
                    {
                        loadimg.SelectActiveFrame(FrameDimension.Time, z);
                        for (int y = 0; y < loadimg.Height; y++)
                            for (int x = 0; x < loadimg.Width; x++)
                            {
                                Color cur = loadimg.GetPixel(x, y);
                                // if the pixel is not grayscale it will be treated as grayscale by average pixel value
                                int gray = (cur.R + cur.G + cur.B) / 3;
                                Color set = Color.FromArgb(
                                    cur.A,
                                    (Properties._PrimaryColor.R * gray + Properties._SecondaryColor.R * (255 - gray)) / 255,
                                    (Properties._PrimaryColor.G * gray + Properties._SecondaryColor.G * (255 - gray)) / 255,
                                    (Properties._PrimaryColor.B * gray + Properties._SecondaryColor.B * (255 - gray)) / 255);
                                loadimg.SetPixel(x, y, set);
                            }

                    }

                    _loaded_image = loadimg;
                    _loaded_image_path = Properties.ImagePath;

                    

                    if (Properties.ImagePath.EndsWith(".gif") && ImageAnimator.CanAnimate(_loaded_image))
                    {

                        ImageAnimator.Animate(_loaded_image, null);
                    }
                }

                if (Properties.ImagePath.EndsWith(".gif") && ImageAnimator.CanAnimate(_loaded_image))
                    ImageAnimator.UpdateFrames(_loaded_image);

                temp_layer = new EffectLayer("Temp Image Render");
                
                if (Properties.Sequence.type == KeySequenceType.Sequence)
                {
                    using (Graphics g = temp_layer.GetGraphics())
                    {
                        g.DrawImage(_loaded_image, new RectangleF(0, 0, Effects.canvas_width, Effects.canvas_height), new RectangleF(0, 0, _loaded_image.Width, _loaded_image.Height), GraphicsUnit.Pixel);
                    }

                    foreach (var key in Properties.Sequence.keys)
                        image_layer.Set(key, Utils.ColorUtils.AddColors(image_layer.Get(key), temp_layer.Get(key)));
                }
                else
                {
                    float x_pos = (float)Math.Round((Properties.Sequence.freeform.X + Effects.grid_baseline_x) * Effects.editor_to_canvas_width);
                    float y_pos = (float)Math.Round((Properties.Sequence.freeform.Y + Effects.grid_baseline_y) * Effects.editor_to_canvas_height);
                    float width = (float)Math.Round((double)(Properties.Sequence.freeform.Width * Effects.editor_to_canvas_width));
                    float height = (float)Math.Round((double)(Properties.Sequence.freeform.Height * Effects.editor_to_canvas_height));

                    if (width < 3) width = 3;
                    if (height < 3) height = 3;

                    Rectangle rect = new Rectangle((int)x_pos, (int)y_pos, (int)width, (int)height);

                    using (Graphics g = temp_layer.GetGraphics())
                    {
                        g.DrawImage(_loaded_image, rect, new RectangleF(0, 0, _loaded_image.Width, _loaded_image.Height), GraphicsUnit.Pixel);
                    }

                    using (Graphics g = image_layer.GetGraphics())
                    {
                        PointF rotatePoint = new PointF(x_pos + (width / 2.0f), y_pos + (height / 2.0f));

                        Matrix myMatrix = new Matrix();
                        myMatrix.RotateAt(Properties.Sequence.freeform.Angle, rotatePoint, MatrixOrder.Append);

                        g.Transform = myMatrix;
                        g.DrawImage(temp_layer.GetBitmap(), rect, rect, GraphicsUnit.Pixel);
                    }
                }
            }

            return image_layer;
        }
    }
}
