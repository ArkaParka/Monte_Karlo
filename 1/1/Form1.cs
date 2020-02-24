using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double a;
        double b;
        static double x;
        int n;
        double y;
        bool ISpaint = true;
        double[] x1;
        double[] y1;
        private void Form1_Load(object sender, EventArgs e)
        {            
            
        }
         
        public void Painting()
        {
            x1 = new double[n];
            y1 = new double[n];

            if(ISpaint == true)
            {
                double h = (b - a) / n;
                x = a;
                for (int i = 0; i < n; i++)
                {
                    y = Math.Pow(x, 3) / (Math.Pow(Math.Sqrt(2.5 + Math.Pow(x, 2)), 3));

                    y1[i] = y;
                    x1[i] = x;
                    chart1.Series[0].Points.AddXY(x, y);
                    x += h;
                }
            }                         
                
        }
                

        private double f(double x)
        {
            y = Math.Pow(x, 3) / (Math.Pow(Math.Sqrt(2.5 + Math.Pow(x, 2)), 3));
            return y;
        }

        double calculateSimpsons(double a, double b, double n)
        {
            double v = 0;
            double p = 0;

            double h = (b - a) / n;
            double s = f(a) + f(b);
            double xi = a + h;

            while (xi < b)
            {
                p += f(xi);
                xi += h * 2;
            }

            xi = a + h * 2;

            while (xi < b - h)
            {
                v += f(xi);
                xi += h * 2;
            }
            return Math.Round(h / 3 * (s + 4 * p + 2 * v), 8);
        }

        double InfSimpson()
        {
            return calculateSimpsons(a, b, n * 2);
        }

        public void Sshow()
        {           
            var res = calculateSimpsons(a,b,n);
            textBox1.Text = Convert.ToString(res);
            
            var resinf = InfSimpson();
            textBox2.Text = Convert.ToString(resinf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = Convert.ToDouble(textBox3.Text);
            b = Convert.ToDouble(textBox4.Text);
            n = Convert.ToInt32(textBox5.Text);
            Painting();
            ISpaint = false;
            Sshow();
            MethodMonteKarlo();
        }

       
        
        private void MethodMonteKarlo()
        {
            
            int i;
            double max, min;


            
            //max = f(b);
            //min = f(a);
            if (f(a) >= f(b))
            {
                max = f(a); // max значение функции
                min = f(b); // min значение функции
            }
            else
            {
                max = f(b);
                min = f(a);
            }
            

            double[] xx = new double[n];
            double[] yy = new double[n];
            int count = 0;
            
            Random rand = new Random();
            for (i = 0; i < n; i++)
            {
                xx[i] = (a + (b - a) * rand.NextDouble());  // random N                
                yy[i] =( min + (max - min) * rand.NextDouble());

                

                chart1.Series["func3"].Points.DataBindXY(xx, yy);  // Точки на граффике


                if ((yy[i]) < f(xx[i]))//>
                {
                    count++;

                }
                
            }

            double integral = (count * (b - a) * (max - min)) / n;

            textBox7.Text = Convert.ToString(integral);
                                   
           
        }


    }


}




