using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PROYECTO_FINAL
{
    class Program
    {
        static void Main(string[] args)
        {
            Proceso proceso = new Proceso();
            Inputs input = new Inputs();

            proceso.Pago = 1;
            int pagoaumentador = 14;
            int fechaaumentador = 14;
            int CuotaAumentador = 14;
            int InteresAumentador = 14;
            int CapitalAumentador = 14;
            int BDAumentador = 14;
            int IFecha = 31;
            bool Permiso = true;

            while (Permiso == true)
            {
                try
                {
                    Console.WriteLine("Ingrese un monto: ");
                    input.MontoInput = Convert.ToDouble(Console.ReadLine());

                    while (Permiso == true)
                    {
                        Console.WriteLine("Ingrese cantidad de cuotas: ");
                        input.CuotasInput = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Introduzca el interes: ");
                        input.InteresInput = Convert.ToDouble(Console.ReadLine());                        

                        if (input.CuotasInput == 0 || input.InteresInput == 0)
                        {
                            //Console.Clear();
                            Console.WriteLine("No se puede realizar calculos con '0' de cuota o interes");

                            Console.ReadKey();

                            //Permiso = false;
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();

                            proceso.CuotaMensual(input.MontoInput, input.CuotasInput, input.InteresInput);

                            Permiso = false;
                        }
                    }                    
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine("Usted acaba de introducir los datos erronemente");
                }
            }
            
            proceso.display(50, 10, "Tabla de Amortizacion");
            proceso.display(1, 12, "Pago");
            proceso.display(10, 12, "Fechas de Pago");
            proceso.display(30, 12, "Cuota");
            proceso.display(55, 12, "Capital");
            proceso.display(80, 12, "Interes");
            proceso.display(100, 12, "Balance");

            while (input.CuotasInput > proceso.Pago)
            {
                proceso.PagoDisplay(1, pagoaumentador);

                proceso.ObtenerFechadisplay(10, fechaaumentador, IFecha);
                IFecha += 31;

                proceso.CuotaMensual(30, CuotaAumentador);

                proceso.InteresAplicado(input.InteresInput, input.MontoInput, 80, InteresAumentador);

                proceso.Capital(input.CuotasInput, 55, CapitalAumentador);

                proceso.Balance(100, BDAumentador);


                InteresAumentador += 2;
                CapitalAumentador += 2;
                BDAumentador += 2;
                fechaaumentador += 2;
                pagoaumentador += 2;
                proceso.Pago++;
                CuotaAumentador += 2;
            }



            Console.ReadKey();
        }        
    }
    class Inputs
    {
        public double MontoInput { get; set; }
        public int CuotasInput { get; set; }
        public double InteresInput { get; set; }
    }
    class Proceso
    {
        public int Pago { get; set; }
        public double Monto { get; set; }
        public int CantidadCuotas { get; set; }
        public double CuotaMensualObtenida { get; set; }
        public double Interes { get; set; }
        public double Interesnitido { get; set; }
        public double CapitalAPagar { get; set; }
        public DateTime FechaActual { get; set; }

        public void CuotaMensual(double Monto, int CantidadCuotas, double Interes )
        {
            this.Monto = Monto;
            this.CantidadCuotas = CantidadCuotas;
            this.Interes = Interes;

            this.CuotaMensualObtenida = Monto * ((Interes / 100) / 12) / (1 - Math.Pow((1 + (Interes / 100) / 12), -CantidadCuotas));

            Console.WriteLine("Monto del prestamo en RD$: " + Monto.ToString("C", CultureInfo.CurrentCulture));
            Console.WriteLine($"Interes: {Interes}%");
            Console.WriteLine($"Plazo: {CantidadCuotas} Meses");
            Console.WriteLine($"Valor cuota: RD{CuotaMensualObtenida.ToString("C", CultureInfo.CurrentCulture)}");            
        }
        public void CuotaMensual(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"RD{CuotaMensualObtenida.ToString("C", CultureInfo.CurrentCulture)}");
        }
        public void InteresAplicado(double Interes, double Monto)
        {
            this.Interes = Interes;
            this.Monto = Monto;

            double Interespormes = (Interes / 100) / 12;
            this.Interesnitido = Interespormes * Monto;

            Console.WriteLine($"Interes: RD{Interesnitido.ToString("C", CultureInfo.CurrentCulture)}");
        }
        public void InteresAplicado(double Interes, double Monto, int x, int y)
        {
            this.Interes = Interes;
            this.Monto = Monto;

            double Interespormes = (Interes / 100) / 12;
            this.Interesnitido = Interespormes * Monto;

            Console.SetCursorPosition(x, y);
            Console.WriteLine($"RD{Interesnitido.ToString("C", CultureInfo.CurrentCulture)}");
        }
        public void Capital(double Cuota)
        {
            this.CapitalAPagar = CuotaMensualObtenida - Interesnitido;

            Console.WriteLine($"Capital: RD{CapitalAPagar.ToString("C", CultureInfo.CurrentCulture)}");
        }
        public void Capital(double Cuota, int x, int y)
        {
            this.CapitalAPagar = CuotaMensualObtenida - Interesnitido;
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"RD{CapitalAPagar.ToString("C", CultureInfo.CurrentCulture)}");
        }
        
        public void Balance(int x, int y)
        {
            this.Monto = Monto - CapitalAPagar;
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"RD{Monto.ToString("C", CultureInfo.CurrentCulture)}");
        }
        public void ObtenerFecha()
        {
            DateTime FechaActual = DateTime.Today;
            Console.WriteLine(FechaActual.ToString("MM/dd/yyyy"));
        }
        public void ObtenerFechadisplay(int x, int y, int i)
        {
            DateTime FechaActual = DateTime.Today;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(FechaActual.AddDays(i).ToString("MM/dd/yyyy"));
        }
        public void PagoDisplay(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(Pago);
        }
        public void display(int x, int y, string s)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(s);
        }

    }
}