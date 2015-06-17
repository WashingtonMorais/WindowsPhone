using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MapaBrasil
{
    public partial class Mapa : PhoneApplicationPage
    {
        public Mapa()
        {
            InitializeComponent();
        }

        private void OnPathTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Cor padrão dos elementos do mapa
            Color cor = ConvertStringToColor("#007acc");

            // Recupera todos os elementos "Path" do mapa
            var listaPaths = FindControls<Path>(gridMapa);

            // Altera a cor de todos os elementos novamente para a cor padrão
            if (listaPaths != null)
            {
                foreach (Path item in listaPaths)
                {
                    item.Fill = new SolidColorBrush(cor);
                }
            }

            // Recupera o elemento Path selecionado
            Path path = (Path)sender;
            if (path != null)
            {
                // Altera a cor do elemento
                path.Fill = new SolidColorBrush(Colors.Blue);
            }
        }

        /// <summary>
        /// Busca uma lista de controles dentro de outro controle.
        /// </summary>
        /// <typeparam name="T">Tipo a ser buscado.</typeparam>
        /// <param name="parentElement">Controle pai.</param>
        /// <returns>Controle.</returns>
        public List<T> FindControls<T>(DependencyObject targetElement) where T : DependencyObject
        {
            var result = new List<T>();

            var count = VisualTreeHelper.GetChildrenCount(targetElement);
            if (count == 0)
                return new List<T>();

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(targetElement, i);
                if (child is T)
                {
                    result.Add(child as T);
                }
                else
                {
                    result = FindControls<T>(child);
                }
            }

            return result;
        }

        /// <summary>
        /// Conmverte uma cor em Hexadecimal para System.Windows.Media.Color.
        /// Referência: http://stackoverflow.com/a/11739523/1509954
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public Color ConvertStringToColor(string hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }   
    }
}
