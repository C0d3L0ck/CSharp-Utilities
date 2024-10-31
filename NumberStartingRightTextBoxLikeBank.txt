 public static void SetupTextBox(TextBox textBox)
 {
     textBox.TextAlign = HorizontalAlignment.Right;
     textBox.MaxLength = 6; // Para incluir el punto decimal y dos decimales
     textBox.ShortcutsEnabled = false; // Deshabilitar pegar
     textBox.Text = "0.00";

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
                 textBox.Text = parsedValue.ToString("N2");
             }
         }
         else if (e.KeyChar == '\b') // Handle Backspace
         {
             e.Handled = true;
             string currentText = textBox.Text.Replace(".", "").Replace(",", "");
             if (currentText.Length > 1)
             {
                 currentText = currentText.Substring(0, currentText.Length - 1);
                 decimal parsedValue = decimal.Parse(currentText) / 100;
                 textBox.Text = parsedValue.ToString("N2");
             }
             else
             {
                 textBox.Text = "0.00";
             }
         }
     };
     textBox.Leave += (sender, e) =>   //TextBox Salir
     {
         if (string.IsNullOrWhiteSpace(textBox.Text))
         {
             textBox.Text = "0.00";
         }
     };
 }