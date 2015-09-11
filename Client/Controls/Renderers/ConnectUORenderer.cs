using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using ConnectUO.Data;
using ConnectUO.Framework;
using ConnectUO.Framework.Extensions;
using ConnectUO.Framework.Data;
using System.Drawing.Imaging;

namespace ConnectUO.Controls
{
    public sealed class DefaultRenderer : ConnectUORenderer
    {

    }

    public class ConnectUORenderer
    {
        public virtual string Name { get { return "Default"; } }
        public virtual int ItemHeight { get { return 90; } }
        public virtual int SelectedItemHeight { get { return (int)(ItemHeight * 1.5f); } }
        public virtual int ArcSize { get { return 10; } }
        public virtual Color BackColor { get { return Color.White; } }
        public virtual Color ForeColor { get { return Color.Black; } }
        public virtual Color BorderPenColor { get { return Color.LightSlateGray; } }
        public virtual Color FillColorTop { get { return Color.WhiteSmoke; } }
        public virtual Color FillColorBottom { get { return Color.Gainsboro; } }
        public virtual Color FillColorTopMouseOver { get { return Color.Gainsboro; } }
        public virtual Color FillColorBottomMouseOver { get { return Color.WhiteSmoke; } }
        public virtual Color FillColorTopSelected { get { return Color.FromArgb(150, 200, 255); } }
        public virtual Color FillColorBottomSelected { get { return Color.FromArgb(80, 110, 150); } }
        public virtual Color FillColorTopMouseOverSelected { get { return Color.FromArgb(170, 220, 255); } }
        public virtual Color FillColorBottomMouseOverSelected { get { return Color.FromArgb(80, 110, 150); } }
        public virtual Color DefaultTextColor { get { return Color.LightSlateGray; } }
        public virtual Color DefaultTextColorMouseOver { get { return Color.LightSlateGray; } }
        public virtual Color DefaultTextColorSelected { get { return Color.Black; } }
        public virtual Color DefaultTextColorMouseOverSelected { get { return Color.Black; } }
        public virtual Color TextNameColor { get { return Color.DarkSlateGray; } }
        public virtual Color TextNameColorMouseOver { get { return Color.DarkSlateGray; } }
        public virtual Color TextNameColorSelected { get { return Color.DarkSlateGray; } }
        public virtual Color TextNameColorMouseOverSelected { get { return Color.DarkSlateGray; } }
        public virtual Color TextStrongColor { get { return Color.Black; } }
        public virtual Color TextStrongColorMouseOver { get { return Color.Black; } }
        public virtual Color TextStrongColorSelected { get { return Color.Black; } }
        public virtual Color TextStrongColorMouseOverSelected { get { return Color.Black; } }
        public virtual Bitmap DefaultShardIcon { get { return Properties.Resources.cuoicon72x72; } }// RunUOGearShadow; } }
        public virtual Bitmap DefaultUpIcon { get { return Properties.Resources.Up; } }
        public virtual Bitmap DefaultDownIcon { get { return Properties.Resources.Down; } }
        public virtual Bitmap DefaultUnknownIcon { get { return Properties.Resources.Unknown; } }
        public virtual float FontSizeName { get { return 14; } }
        public virtual float FontSizeDescription { get { return 12; } }

        public virtual void DrawServer(Graphics g, ServerListItem shardListItem, Rectangle bounds)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;

            DrawBackground(g, bounds, shardListItem);
            DrawIcon(g, bounds, shardListItem);
            DrawText(g, bounds, shardListItem);
            DrawButtons(g, bounds, shardListItem);

            if (shardListItem.Server.Public) //Favorites, Public
            {
                DrawExtendedInformation(g, bounds, shardListItem);
            }
        }

        protected virtual void DrawExtendedInformation(Graphics g, Rectangle bounds, ServerListItem shardListItem)
        {
            Server shard = shardListItem.Server as Server;

            if (shard != null)
            {
                Color textColor;

                bool mouseDown = shardListItem.IsSelected;
                bool mouseOver = shardListItem.IsMouseOver;

                if (mouseDown && mouseOver)
                    textColor = TextStrongColorMouseOverSelected;
                else if (mouseDown || shardListItem.IsSelected)
                    textColor = TextStrongColorSelected;
                else if (mouseOver)
                    textColor = TextStrongColorMouseOver;
                else
                    textColor = TextStrongColor;

                TextFormatFlags flags = TextFormatFlags.Left |
                                        TextFormatFlags.Top |
                                        TextFormatFlags.Internal |
                                        TextFormatFlags.WordBreak |
                                        TextFormatFlags.EndEllipsis |
                                        TextFormatFlags.WordEllipsis |
                                        TextFormatFlags.GlyphOverhangPadding;

                Rectangle textBounds = new Rectangle(bounds.X + 75, bounds.Y + bounds.Height - 17, bounds.Width - 170, 12);

                Font font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Pixel);
                string extendedInfo = String.Format("Online: {0} | Max: {1} | Avg: {2} | Type: {3} | Language: {4} | Era: {5} | UpTime: {6}% | Client Version: {7}",
                    shard.CurOnline, shard.MaxOnline, shard.AvgOnline, (ShardType)shard.ShardType, (Lang)shard.Lang, (Era)shard.Era,
                    (int)(shard.UpTime * 100), string.IsNullOrEmpty(shard.ServerClientVersion) ? "Unknown" : shard.ServerClientVersion);

                TextRenderer.DrawText(g, extendedInfo, font, textBounds, textColor, flags);

                font.Dispose();
            }
        }
        protected virtual void DrawButtons(Graphics g, Rectangle bounds, ServerListItem shardListItem)
        {
            int startY = 10;

            for(int i = 0; i < shardListItem.Buttons.Count; i++)
            {
                ServerListItemButton button = shardListItem.Buttons[i];
                Rectangle buttonBounds = button.GetBounds(bounds.Location, bounds.Size);

                buttonBounds.Y += startY;

                Color textColor;

                bool mouseDown = button.IsMouseDown;
                bool mouseOver = button.IsMouseOver;

                if (mouseDown && mouseOver)
                    textColor = DefaultTextColorMouseOverSelected;
                else if (mouseDown || shardListItem.IsSelected)
                    textColor = DefaultTextColorSelected;
                else if (mouseOver)
                    textColor = DefaultTextColorMouseOver;
                else
                    textColor = DefaultTextColor;

                g.DrawImage(mouseOver ? Properties.Resources.ArrowSelected : Properties.Resources.Arrow, new Rectangle(
                    new Point(bounds.Width - button.LocationFromRight.X - 5, bounds.Y + startY + 3),
                    new Size(5, 7)));

                Font font = new Font("Verdana", 10, FontStyle.Regular, GraphicsUnit.Pixel);

                TextFormatFlags flags = TextFormatFlags.Left |
                                        TextFormatFlags.VerticalCenter |
                                        TextFormatFlags.Internal |
                                        TextFormatFlags.WordBreak |
                                        TextFormatFlags.WordEllipsis |
                                        TextFormatFlags.GlyphOverhangPadding;

                TextRenderer.DrawText(g, button.Text, font, buttonBounds, textColor, flags);

                startY += 15;
            }
        }
        protected virtual void DrawIcon(Graphics g, Rectangle bounds, ServerListItem shardListItem)
        {
            Image icon = DefaultShardIcon;

            if (shardListItem.Server.Public)
            {
                Server server = shardListItem.Server;
                Image statusIcon = null;

                //if (_storageService.WorkState == WorkState.Online)
                //{
                statusIcon = ((Status)server.Status == Status.Active) ? DefaultUpIcon : DefaultDownIcon;
                //}
                //else
                //{
                //    statusIcon = DefaultUnknownIcon;
                //}

                if (statusIcon != null)
                    g.DrawImage(statusIcon, new Rectangle(bounds.Width - 21, bounds.Y + 5, 16, 16));

                if (server.Data != null)
                {
                    MemoryStream ms = new MemoryStream(server.Data);
                    icon = Bitmap.FromStream(ms);
                    ms.Close();
                    ms.Dispose();
                }
            }

            if (shardListItem.Server.HasPatches)
            {
                g.DrawImage(Properties.Resources.Patch, new Rectangle(bounds.Width - 40, bounds.Y + 5, 16, 16));
            }

            icon = CreateReflection(icon, Properties.Resources.Shadow, 90, true);

            int size = shardListItem.IsSelected ? 64 : shardListItem.IsMouseOver ? 58 : 50;
            int x = (shardListItem.IsSelected ? 72 : shardListItem.IsMouseOver ? 69 : 65) - size;

            int width = (int)((float)icon.Width * ((float)size / (float)Math.Max(icon.Width, icon.Height)));
            int height = (int)((float)icon.Height * ((float)size / (float)Math.Max(icon.Width, icon.Height)));

            if (icon != null)
            {
                g.DrawImage(icon, new Rectangle(x, bounds.Y + bounds.Height / 2 - (size / 2), width, height));
            }
            
        }
        protected virtual void DrawText(Graphics g, Rectangle bounds, ServerListItem shardListItem)
        {
            Color color;
            Color nameColor;

            bool selected = shardListItem.IsSelected;
            bool mouseOver = shardListItem.IsMouseOver;

            float nameFontSize = FontSizeName;
            float descriptionFontSize = FontSizeDescription;

            if (selected && mouseOver)
            {
                color = DefaultTextColorMouseOverSelected;
                nameColor = TextNameColorMouseOverSelected;
            }
            else if (selected)
            {
                color = DefaultTextColorSelected;
                nameColor = TextNameColorMouseOver;
            }
            else if (mouseOver)
            {
                color = DefaultTextColorMouseOver;
                nameColor = TextNameColorSelected;
            }
            else
            {
                color = DefaultTextColor;
                nameColor = TextNameColor;
            }

            TextFormatFlags flags = TextFormatFlags.Left |
                                    TextFormatFlags.Top |
                                    TextFormatFlags.Internal |
                                    TextFormatFlags.WordBreak |
                                    TextFormatFlags.EndEllipsis | 
                                    TextFormatFlags.WordEllipsis |
                                    TextFormatFlags.GlyphOverhangPadding;


            Rectangle nameBounds = new Rectangle(bounds.X + 75, (int)((float)bounds.Height * 0.075f + bounds.Y),
                                                 bounds.Width - 170, (int)(nameFontSize * 2));
            Rectangle descriptionBounds = new Rectangle(bounds.X + 75, 
                                                        (int)((float)bounds.Height * 0.1f + nameFontSize + bounds.Y),
                                                        bounds.Width - 170,
                                                        (int)(descriptionFontSize * 3.85f));
            if (shardListItem.IsSelected)
            {
                SizeF descSize = MeasureDescription(g, shardListItem.Server.Description, bounds.Width);
                descriptionBounds.Height += (int)descSize.Height;
            }

            Font nameFont = new Font("Verdana", nameFontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            Font descriptionFont = new Font("Verdana", descriptionFontSize, FontStyle.Regular, GraphicsUnit.Pixel);

            TextRenderer.DrawText(g, shardListItem.Server.Name, nameFont, nameBounds, nameColor, flags);
            TextRenderer.DrawText(g, shardListItem.Server.Description, descriptionFont, descriptionBounds, color, flags);

            nameFont.Dispose();
            descriptionFont.Dispose();
        }
        protected virtual void DrawBackground(Graphics g, Rectangle bounds, ServerListItem shardListItem)
        {
            Color color1, color2;

            bool selected = shardListItem.IsSelected;
            bool mouseOver = shardListItem.IsMouseOver;

            if (selected && mouseOver)
            {
                color1 = FillColorTopMouseOverSelected;
                color2 = FillColorBottomMouseOverSelected;
            }
            else if (selected)
            {
                color1 = FillColorTopSelected;
                color2 = FillColorBottomSelected;
            }
            else if (mouseOver)
            {
                color1 = FillColorTopMouseOver;
                color2 = FillColorBottomMouseOver;
            }
            else
            {
                color1 = FillColorTop;
                color2 = FillColorBottom;
            }

            using (LinearGradientBrush fillBrush =
                new LinearGradientBrush(new Point(bounds.X, bounds.Y),
                                        new Point(bounds.X, bounds.Y + bounds.Height), color1, color2))
            {
                bounds.Inflate(new Size((int)(bounds.Width * -0.005), (int)(bounds.Height * -0.025)));

                int arcSize = ArcSize;

                GraphicsPath path = new GraphicsPath();

                AddRoundedRectangle(path, bounds, arcSize);

                g.FillPath(fillBrush, path);

                using (Pen pen = new Pen(BorderPenColor))
                {
                    g.DrawPath(pen, path);
                }

                path.Dispose();
            }
        }

        public static void AddRoundedRectangle(GraphicsPath path, Rectangle bounds, int arcSize)
        {
            path.AddArc(new Rectangle(bounds.X, bounds.Y, arcSize, arcSize), 180, 90);
            path.AddArc(new Rectangle(bounds.X + bounds.Width - arcSize, bounds.Y, arcSize, arcSize), 270, 90);
            path.AddArc(new Rectangle(bounds.X + bounds.Width - arcSize, bounds.Y + bounds.Height - arcSize, arcSize, arcSize), 0, 90);
            path.AddArc(new Rectangle(bounds.X, bounds.Y + bounds.Height - arcSize, arcSize, arcSize), 90, 90);

            path.CloseFigure();
        }

        public SizeF MeasureDescription(Graphics graphics, string desc, int width)
        {
            Font font = new Font("Verdana", FontSizeDescription, FontStyle.Regular, GraphicsUnit.Pixel);

            return graphics.MeasureString(desc, font, width - 170);
        }

        public static Image CreateReflection(Image image, Image shadow, int reflectivity, bool drawShadow)
        {
            // Calculate the size of the new image
            int height = (int)(image.Height + (image.Height * ((float)reflectivity / 255)));
            Bitmap newImage = new Bitmap(image.Width, height, PixelFormat.Format32bppArgb);
            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                // Initialize main graphics buffer
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, new Point(0, 0));
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle destinationRectangle = new Rectangle(0, image.Size.Height,
                                                 image.Size.Width, image.Size.Height);

                // Prepare the reflected image
                int reflectionHeight = (image.Height * reflectivity) / 255;
                Image reflectedImage = new Bitmap(image.Width, reflectionHeight);

                // Draw just the reflection on a second graphics buffer
                using (Graphics gReflection = Graphics.FromImage(reflectedImage))
                {
                    gReflection.DrawImage(image,
                       new Rectangle(0, 0, reflectedImage.Width, reflectedImage.Height),
                       0, image.Height - reflectedImage.Height, reflectedImage.Width,
                       reflectedImage.Height, GraphicsUnit.Pixel);
                }

                reflectedImage.RotateFlip(RotateFlipType.RotateNoneFlipY);

                Rectangle imageRectangle =
                    new Rectangle(destinationRectangle.X, destinationRectangle.Y,
                    destinationRectangle.Width,
                    (destinationRectangle.Height * reflectivity) / 255);

                if (drawShadow)
                {
                    graphics.DrawImage(shadow, imageRectangle);
                }

                // Draw the image on the original graphics
                graphics.DrawImage(reflectedImage, imageRectangle);

                BitmapData bmData = newImage.LockBits(imageRectangle, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int scanline = bmData.Stride;
                IntPtr scan0 = bmData.Scan0;

                unsafe
                {
                    byte* cPtr = (byte*)(void*)scan0;

                    for (int y = 0; y < imageRectangle.Height; y++)
                    {
                        for (int x = 0; x < imageRectangle.Width; x++)
                        {
                            cPtr[((x + (y * imageRectangle.Width)) * 4) + 3] = (byte)(cPtr[((x + (y * imageRectangle.Width)) * 4) + 3] * (((float)(imageRectangle.Height - 1) - (float)y) / (float)(imageRectangle.Height - 1)));
                        }
                    }
                }

                newImage.UnlockBits(bmData);
            }

            return newImage;
        }
    }
}
