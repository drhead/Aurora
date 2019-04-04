using Aurora.EffectsEngine;
using Aurora.Profiles;
using Newtonsoft.Json;
using System;
using System.Collections;
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
        private Bitmap _loaded_image = null;
        private string _loaded_image_path = "";
        private Color _applied_primary;
        private Color _applied_secondary;

        public AdvancedImageLayerHandler()
        {
            _ID = "AdvancedImage";
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

                if (!_loaded_image_path.Equals(Properties.ImagePath) || 
                    !_applied_primary.Equals(Properties.PrimaryColor) || 
                    !_applied_secondary.Equals(Properties.SecondaryColor))
                {
                    //Not loaded, load it!
                    if (!File.Exists(Properties.ImagePath))
                        throw new FileNotFoundException("Could not find file specified for layer: " + Properties.ImagePath);

                    Bitmap loadimg = new Bitmap(Properties.ImagePath);
                    // expensive function but it should only get run when the image is loaded
                    // iterate over frames for animated GIFs, regular images should just do one loop
                    /*for (int z = 0; z < loadimg.GetFrameCount(FrameDimension.Time); z++)
                    {
                        loadimg.SelectActiveFrame(FrameDimension.Time, z);
                        for (int y = 0; y < loadimg.Height; y++)
                            for (int x = 0; x < loadimg.Width; x++)
                            {
                                Color cur = loadimg.GetPixel(x, y);
                                // if the pixel is not grayscale it will be treated as grayscale by average pixel value
                                int gray = (cur.R + cur.G + cur.B) / 3;
                                Color set = Color.FromArgb(
                                    
                                    (Properties.PrimaryColor.R * gray + Properties.SecondaryColor.R * (255 - gray)) / 255,
                                    (Properties.PrimaryColor.G * gray + Properties.SecondaryColor.G * (255 - gray)) / 255,
                                    (Properties.PrimaryColor.B * gray + Properties.SecondaryColor.B * (255 - gray)) / 255);
                                loadimg.SetPixel(x, y, set);

                            }
                    }*/
                    _loaded_image = new Bitmap(Properties.ImagePath);


                    try
                    {
                        PropertyItem palette = _loaded_image.GetPropertyItem(0x5102);
                        BitmapData bitmapData;
                        bitmapData = _loaded_image.LockBits(
                            new Rectangle(0,0,_loaded_image.Width,_loaded_image.Height),
                            ImageLockMode.ReadWrite,
                            PixelFormat.)
                        for (int i = 0; i < palette.Value.Length; i += 3)
                        {
                            int gray = (palette.Value[i] + palette.Value[i+1] + palette.Value[i+2]) / 3;
                            palette.Value[i] = (byte)((Properties.PrimaryColor.R * gray + (Properties.SecondaryColor.R * (255 - gray))) / 255);
                            palette.Value[i+1] = (byte)((Properties.PrimaryColor.G * gray + (Properties.SecondaryColor.G * (255 - gray))) / 255);
                            palette.Value[i+2] = (byte)((Properties.PrimaryColor.B * gray + (Properties.SecondaryColor.B * (255 - gray))) / 255);
                        }
                        _loaded_image.SetPropertyItem(palette);
                        MemoryStream stream = new MemoryStream();
                        _loaded_image.Save(stream, ImageFormat.Gif);
                        StreamReader.

                        _loaded_image = new Bitmap(stream);

                    } catch(Exception e) {

                    }

                    


                    _loaded_image_path = Properties.ImagePath;
                    _applied_primary = Properties.PrimaryColor;
                    _applied_secondary = Properties.SecondaryColor;

                    if (Properties.ImagePath.EndsWith(".gif") && ImageAnimator.CanAnimate(_loaded_image))
                    {
                        byte[] gifBytes = File.ReadAllBytes(Properties.ImagePath);
                        BitArray bit = new BitArray(new byte[] { gifBytes[10] });
                        for(int i = 13; i < 13 + _loaded_image)
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
