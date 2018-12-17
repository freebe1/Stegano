using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Stegano
{
    public partial class Form1 : Form
    {
        static int idx;
        static Bitmap pic;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            idx = 0;
            try
            {
                //read image
                Bitmap pic = (Bitmap)Image.FromFile(this.textBox1.Text, true);

                Bitmap pic2 = new Bitmap(pic.Width,pic.Height);

                //load nothing?
                //picBox.Image = Image.FromFile(this.textBox1.Text, true);

                byte[] tempB = new byte[2];

                //streamReader SR = new StreamReader(writer, Encoding.Default);

                int width = pic.Width;
                int height = pic.Height;
                int capa = width * height;
                int x, y;
                //byte[] bytes = File.ReadAllBytes(this.textBox1.Text);

                /*#region binary String (return => string sb)
                StringBuilder sb = new StringBuilder();

                foreach (byte b in this.textBox2.Text)
                {
                    sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                }
                #endregion*/
                byte[] bytes = UnicodeEncoding.Unicode.GetBytes(this.textBox2.Text);
                string sb = string.Empty;

                foreach (byte b in bytes)
                {
                    // byte를 2진수 문자열로 변경
                    string s = Convert.ToString(b, 2);
                    sb += s.PadLeft(8, '0');
                }

                //idx = sb.ToString().Length;

                for (x = 0; x < width-1; x++)
                {
                    for (y = 0; y < height-1; y++)
                    {
                        //get pixel Value
                        Color p = pic.GetPixel(x, y);
                        int vR = p.R;
                        int vG = p.G;
                        int vB = p.B;
                        #region 이미지 주석
                        /*if (x < 1 && y < textBox2.TextLength)
                        {
                            int value = p.B;
                            //char c = Convert.ToChar(value);
                            //string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                            //message = message + letter;

                            pic2.SetPixel(x, y, Color.FromArgb(p.R, p.G, value-1));//BRG?
                        }
                        else
                            pic2.SetPixel(x, y, Color.FromArgb(p.R, p.G, p.B));*/
                        /*pic.SetPixel(x, y, Color.FromArgb(p.R, p.G,
                            Convert.ToInt32(Convert.ToString(p.B, 2).Substring(0, Convert.ToString(value, 2).Length - 1) + sb.ToString().Substring(idx, 1))));*/
                        #endregion

                        if (idx < sb.Length-3)
                        {
                            pic2.SetPixel(x, y,
                                Color.FromArgb(
                                    Convert.ToByte(Convert.ToString(vR, 2).Substring(0, Convert.ToString(vR, 2).Length - 1) + sb.ToString().Substring(idx, 1), 2),
                                    Convert.ToByte(Convert.ToString(vG, 2).Substring(0, Convert.ToString(vG, 2).Length - 1) + sb.ToString().Substring(idx + 1, 1), 2),
                                    Convert.ToByte(Convert.ToString(vB, 2).Substring(0, Convert.ToString(vB, 2).Length - 1) + sb.ToString().Substring(idx + 2, 1), 2))
                            );
                            idx += 3;
                        }
                        else
                        {
                            if (sb.Length - idx == 1)
                            {
                                pic2.SetPixel(x, y,
                                 Color.FromArgb(
                                     Convert.ToByte(Convert.ToString(vR, 2).Substring(0, Convert.ToString(vR, 2).Length - 1) + sb.ToString().Substring(idx, 1), 2),
                                     Convert.ToByte(Convert.ToString(vG, 2).Substring(0, Convert.ToString(vG, 2).Length - 1) + "0", 2),
                                     Convert.ToByte(Convert.ToString(vB, 2).Substring(0, Convert.ToString(vB, 2).Length - 1) + "0", 2))
                                 );
                                idx += 3;
                            }
                            else if (sb.Length - idx == 2)
                            {
                                pic2.SetPixel(x, y,
                                 Color.FromArgb(
                                     Convert.ToByte(Convert.ToString(vR, 2).Substring(0, Convert.ToString(vR, 2).Length - 1) + sb.ToString().Substring(idx, 1), 2),
                                     Convert.ToByte(Convert.ToString(vG, 2).Substring(0, Convert.ToString(vG, 2).Length - 1) + sb.ToString().Substring(idx + 1, 1), 2),
                                     Convert.ToByte(Convert.ToString(vB, 2).Substring(0, Convert.ToString(vB, 2).Length - 1) + "0", 2))
                                 );
                                idx += 3;
                            }
                            /*else if (idx > sb.Length)
                            {
                                pic2.SetPixel(x, y,
                                 Color.FromArgb(
                                     Convert.ToByte(Convert.ToString(vR, 2).Substring(0, Convert.ToString(vR, 2).Length - 1) + "0", 2),
                                     Convert.ToByte(Convert.ToString(vG, 2).Substring(0, Convert.ToString(vG, 2).Length - 1) + "0", 2),
                                     Convert.ToByte(Convert.ToString(vB, 2).Substring(0, Convert.ToString(vB, 2).Length - 1) + "0", 2))
                                 );
                            }*/
                            else {
                                pic2.SetPixel(x, y,
                                    Color.FromArgb(
                                        Convert.ToByte(Convert.ToString(vR, 2).Substring(0, Convert.ToString(vR, 2).Length - 1) + "0", 2),
                                        Convert.ToByte(Convert.ToString(vG, 2).Substring(0, Convert.ToString(vG, 2).Length - 1) + "0", 2),
                                        Convert.ToByte(Convert.ToString(vB, 2).Substring(0, Convert.ToString(vB, 2).Length - 1) + "0", 2))
                                );
                                idx += 3;
                            }
                            //pic2.SetPixel(x,y,Color.FromArgb(p.R, p.G, p.B));
                        }
                            
                    }
                }
                //SaveFileDialog saveFile = new SaveFileDialog();
                //saveFile.Filter = "Image Files";
                //saveFile.InitialDirectory = @"C:\Users\";

                //if(saveFile.ShowDialog() == DialogResult.OK)
                //{
                //textBox2.Text = saveFile.FileName.ToString();
                try
                {
                    pic2.Save(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + (textBox1.Text.Split('\\')[4].Split('.')[0]) + "R." + (textBox1.Text.Split('\\')[4].Split('.')[1]));
                    MessageBox.Show("File Create Success!");
                }
                catch
                {
                    if (pic != null) pic.Dispose();
                    if (pic2 != null) pic2.Dispose();
                    MessageBox.Show("File Create Failed!");
                }
                finally
                {
                    if (pic != null) pic.Dispose();
                    if (pic2 != null) pic2.Dispose();
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("File not Found!");
                //Close();
                //Form1.ActiveForm.Close();                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sb = string.Empty;
            try
            {
                pic = (Bitmap)Image.FromFile(this.textBox3.Text, true);

                int width = pic.Width;
                int height = pic.Height;
                int x, y;
                //byte[] bytes = File.ReadAllBytes(this.textBox1.Text);
                //byte[] bytes = UnicodeEncoding.Unicode.GetBytes(this.textBox2.Text);
                for (x = 0; x < width/4 - 1; x++)
                {
                    for (y = 0; y < height - 1; y++)
                    {
                        //get pixel Value
                        Color p = pic.GetPixel(x, y);
                        int vR = p.R;
                        int vG = p.G;
                        int vB = p.B;
                        sb += Convert.ToString(p.R, 2).Substring(Convert.ToString(p.R, 2).Length-1, 1);
                        sb += Convert.ToString(p.G, 2).Substring(Convert.ToString(p.G, 2).Length-1, 1);
                        sb += Convert.ToString(p.B, 2).Substring(Convert.ToString(p.B, 2).Length-1, 1);
                    }
                }
            }
            catch
            {
                if (pic != null) pic.Dispose();
                Console.WriteLine("sdf");
            }
            finally
            {
                if(pic!=null) pic.Dispose();
                string[] sep = Regex.Split(sb,"00000000");
                //sb.Split(sep);

                int nbytes = sb.Length / 8;
                byte[] outBytes = new byte[nbytes];

                for (int i = 0; i < nbytes; i++)
                {
                    // 8자리 숫자 즉 1바이트 문자열 얻기
                    string binStr = sb.Substring(i * 8, 8);
                    // 2진수 문자열을 숫자로 변경
                    outBytes[i] = (byte)Convert.ToInt32(binStr, 2);
                }

                // Unicode 인코딩으로 바이트를 문자열로
                string result = UnicodeEncoding.Unicode.GetString(outBytes);
                MessageBox.Show(result);

                
            }
            


        }
    }
}
