using Microsoft.Data.SqlClient;
using Dapper;

public class BD{
    private static string connectionString = @"Server=localhost;DataBase=Album;Integrated Security=true;TrustServerCertificate=true";

    public static List<FiguritaUsuario> ObtenerFiguritas()
    {
        List<FiguritaUsuario> FiguritaUsuario = new List <FiguritaUsuario>();

        using(SqlConnection connection = new SqlConnection(connectionString)) {
            FiguritaUsuario = connection.Query<FiguritaUsuario>("SELECT * FROM FiguritaUsuario").ToList();
        }
        return FiguritaUsuario;
    }

    public static List<FiguritaUsuario> ObtenerFiguritasPegadas()
    {
        List<FiguritaUsuario> FiguritaUsuario = new List <FiguritaUsuario>();

        using(SqlConnection connection = new SqlConnection(connectionString)) {
            FiguritaUsuario = connection.Query<FiguritaUsuario>("SELECT * FROM FiguritaUsuario where Pegada = 1").ToList();
        }
        return FiguritaUsuario;
    }

    public static int AbrirSobre(List<Jugadores> f)
    {
        int resultado = 0;
        for(int i = 0; i < f.Count; i++){
            string query1 = "INSERT INTO FiguritaUsuario (Cantidad, Pegada, idJugador) VALUES (1, 0, @fidJugador)";
            string query2 = "UPDATE FiguritaUsuario SET Cantidad = Cantidad + 1 WHERE @fidJugador = idJugador";
            using(SqlConnection connection = new SqlConnection(connectionString)) 
            {
                if(BuscarRepetida(f[i]) != null)
                {
                    resultado = connection.Execute(query1, new {fidJugador = f[i].idJugador})
                }else{
                    resultado = connection.Execute(query2, new {fidJugador = f[i].idJugador});
                }
            }
        }
        return resultado;
    }

    public static FiguritaUsuario BuscarRepetida(FiguritaUsuario revisar)
    {
        FiguritaUsuario nueva = null;
        string query = "SELECT * FROM FiguritaUsuario WHERE @fidJugador = idJugador";
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            nueva = connection.Query<FiguritaUsuario>(query, new {fidJugador = revisar.idJugador}).ToList();
        }
        return nueva;
    }

    public static int PegarFigurita(FiguritaUsuario FiguritaUsuario){
        int resultado = 0;
        string query = "UPDATE FiguritaUsuario SET Pegada = 1 WHERE @fidJugador = idJugador";
        if(FiguritaUsuario.Pegada == 0){
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                resultado = connection.Execute(query, new {fidJugador = FiguritaUsuario.idJugador});
            }
        }
        return resultado;
    }

    public static List<Jugadores> ObtenerJugadores()
    {
        List<Jugadores> Jugadores = new List <Jugadores>();
        using(SqlConnection connection = new SqlConnection(connectionString)) 
        {
            Jugadores = connection.Query<Jugadores>("SELECT * FROM Jugadores").ToList();
        }
        return Jugadores;
    }

    public static List<Jugadores> GenerarSobre()
    {
        List<Jugadores> Jugadores = ObtenerJugadores();
        List<Jugadores> sobre = new List<Jugadores>();
        Random random = new Random();
        for(int i = 0; i < 5; i++)
        {
            Jugadores jugador = null;
            int NumRandom = random.Next(0, Jugadores.Count);
            jugador = Jugadores[NumRandom];
            sobre.Add(jugador);
            Jugadores.Remove(jugador);
        }
        return sobre;
    }
}
