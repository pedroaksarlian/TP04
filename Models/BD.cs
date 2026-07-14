using Microsoft.Data.SqlClient;
using Dapper;
namespace TP04.Models;
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
            FiguritaUsuario repetida = BuscarRepetida(f[i].ID);
            using(SqlConnection connection = new SqlConnection(connectionString)) 
            {
                if(repetida == null)
                {
                    resultado = connection.Execute(query1, new {fidJugador = f[i].ID});
                }
                else
                {
                    resultado = connection.Execute(query2, new {fidJugador = f[i].ID});
                }
            }
        }
        return resultado;
    }

    public static FiguritaUsuario BuscarRepetida(int idJugador)
    {
        FiguritaUsuario nueva = null;
        string query = "SELECT * FROM FiguritaUsuario WHERE @fidJugador = idJugador";
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            nueva = connection.QueryFirstOrDefault<FiguritaUsuario>(query, new {fidJugador = idJugador});
        }
        return nueva;
    }

    public static void PegarFigurita(int idJugador)
    {
        string query = "UPDATE FiguritaUsuario SET Pegada = 1 WHERE idJugador = @idJugador";
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(query, new {idJugador});
        }
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
        List<Jugadores> jugadores = ObtenerJugadores();
        List<Jugadores> sobre = new List<Jugadores>();
        Random random = new Random();
        while(sobre.Count < 5)
        {
            int posicion = random.Next(jugadores.Count);
            sobre.Add(jugadores[posicion]);
            jugadores.RemoveAt(posicion);
        }
        return sobre;
}
}