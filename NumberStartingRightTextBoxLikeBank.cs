 [System.Runtime.InteropServices.DllImport("user32.dll")]
 static extern bool HideCaret(IntPtr hWnd);   //DESAPARECE EL BLINKING CURSOR (CURSOR TITILANTE DE TEXTO)

public static void SetCoinPlaceHolderTest(TextBox textBox)
{
    textBox.TextAlign = HorizontalAlignment.Right;
    textBox.MaxLength = 2; 
    textBox.ShortcutsEnabled = false; // Deshabilitar pegar
    textBox.Text = "0,00";

   

    textBox.MouseDown += (sender, e) =>  //TEXTBOX para que el Selector se pase a la derecha como si fuera en movil
    {
        HideCaret(textBox.Handle); //DESAPARECE EL BLINKING CURSOR (CURSOR TITILANTE DE TEXTO)
        textBox.SelectionStart = textBox.Text.Length;
    };

    textBox.MouseMove += (sender, e) => 
    { 
        textBox.SelectionLength = 0;
    };

    textBox.KeyPress += (sender, e) =>  //Textbox Entrar
    {
        if (char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
            string currentText = textBox.Text.Replace(".", "").Replace(",", "");
            if (currentText.Length < 100)   // < Límite 
            {
                currentText += e.KeyChar;
                decimal parsedValue = decimal.Parse(currentText) / 100;
                textBox.Text = parsedValue.ToString("N2", CultureInfo.CreateSpecificCulture("es-ES"));
                textBox.SelectionStart = textBox.Text.Length;//Manten el cursor en la derecha
            }
        }
        else if (e.KeyChar == '\b') // Handle Backspace
        {
            e.Handled = true;
            string currentText = textBox.Text.Replace(".", "").Replace(",", "").Replace(" ", "");
            if (currentText.Length > 1)
            {
                currentText = currentText.Substring(0, currentText.Length - 1);
                decimal parsedValue = decimal.Parse(currentText) / 100;
                textBox.Text = parsedValue.ToString("N2", CultureInfo.CreateSpecificCulture("es-ES"));
                textBox.SelectionStart = textBox.Text.Length;//Manten el cursor en la derecha
            }
            else
            {
                textBox.Text = "0,00";
            }
        }
    };
    textBox.Leave += (sender, e) =>   //TextBox Salir
    {
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "0,00";
            textBox.SelectionStart = textBox.Text.Length; //Manten el cursor en la derecha
        }
    };

   
}
