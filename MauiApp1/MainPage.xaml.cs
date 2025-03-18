// Usa el espacio de nombres:
namespace MauiApp1
{

    // Accede a la librería:
    using TicTacToe;

    /// <summary>
    /// Clase principal de la página.
    /// </summary>
    public partial class MainPage : ContentPage
    {

        /// <summary>
        /// Instancia del juego TicTacToe.
        /// </summary>
        private TicTacToe juego;

        /// <summary>
        /// Contador de victorias para el jugador X.
        /// </summary>
        private int victoriasX = 0;

        /// <summary>
        /// Contador de victorias para el jugador O.
        /// </summary>
        private int victoriasO = 0;

        /// <summary>
        /// Constructor que inicializa la página y la instancia del juego.
        /// </summary>
        public MainPage()
        {
            // Inicializar la página y sus componentes
            InitializeComponent();
            // Crear la instancia del juego TicTacToe
            juego = new TicTacToe();
        }

        /// <summary>
        /// Método que maneja el evento de clic en una casilla del tablero.
        /// </summary>
        /// <param name="sender">Objeto que originó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void Casilla_Clicked(object sender, EventArgs e)
        {
            // Obtener el botón que fue clickeado
            var button = sender as Button;
            if (button != null)
            {
                // Obtener la fila y columna a partir de la propiedad del botón
                int row = Grid.GetRow(button);
                int col = Grid.GetColumn(button);

                // Realizar la jugada en las coordenadas indicadas
                int turno = juego.jugada(row, col);

                if (turno == -1)
                {
                    // Si la jugada no es válida, ignorar el clic
                    return;
                }

                // Actualizar el tablero después de la jugada
                ActualizarTablero();

                // Verificar si hay un ganador tras la jugada
                int ganador = juego.Ganador();
                if (ganador != 0)
                {
                    // Si hay un ganador, mostrar un mensaje y actualizar el contador de victorias
                    if (ganador == 1)
                    {
                        victoriasX++;
                        DisplayAlert("¡Ganador!", "¡El jugador X ha ganado!", "OK");
                    }
                    else
                    {
                        victoriasO++;
                        DisplayAlert("¡Ganador!", "¡El jugador O ha ganado!", "OK");
                    }

                    // Reiniciar el tablero para un nuevo juego
                    juego.Reiniciar();
                    ActualizarTablero();
                }
                else if (turno == 9)
                {
                    // Si no hay más jugadas posibles y no hay ganador, declarar un empate
                    DisplayAlert("Empate", "No hay más jugadas disponibles, es un empate", "OK");
                    juego.Reiniciar();
                    ActualizarTablero();
                }

                // Actualizar la información del turno actual
                ActualizarTurno();
            }
        }

        /// <summary>
        /// Método para actualizar el estado visual del tablero en la interfaz.
        /// </summary>
        private void ActualizarTablero()
        {
            // Recorremos las casillas del tablero y actualizamos su imagen según el valor de la casilla
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Acceder a los botones por su nombre
                    var button = (Button)this.FindByName<Button>($"Casilla_{i}{j}");

                    if (button != null)
                    {
                        if (juego.Tablero[i, j] == 1)
                        {
                            button.ImageSource = "img_o.png";  // Mostrar imagen para el jugador O
                        }
                        else if (juego.Tablero[i, j] == 2)
                        {
                            button.ImageSource = "img_x.png";  // Mostrar imagen para el jugador X
                        }
                        else
                        {
                            button.ImageSource = null; // Vaciar la imagen si la casilla está libre
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Actualiza la interfaz para mostrar de quién es el turno y el conteo de victorias.
        /// </summary>
        private void ActualizarTurno()
        {
            if (juego.Turno % 2 == 0)
            {
                turnoLabel.Text = "Turno: O";
            }
            else
            {
                turnoLabel.Text = "Turno: X";
            }

            // Actualizar los contadores de victorias en la interfaz
            victoriasXLabel.Text = "Victorias X: " + victoriasX.ToString();
            victoriasOLabel.Text = "Victorias O: " + victoriasO.ToString();
        }
    }
}