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

    public static int AbrirSobre(List<FiguritaUsuario> f)
    {
        int resultado = 0;
        for(int i = 0; i < f.Count; i++){
            string query1 = "INSERT INTO FiguritaUsuario (Cantidad, Pegada, idJugador) VALUES (1, 0, @fidJugador)";
            string query2 = "UPDATE FiguritaUsuario SET Cantidad = Cantidad + 1 WHERE idJugador = idJugador";
            using(SqlConnection connection = new SqlConnection(connectionString)) 
            {
                if(ObtenerFigurita(f[i]) != null)
            resultado = connection.Execute(query, new {fidJugador = f[i].idJugador});
            }
        }
        return resultado;
    }

    public static FiguritaUsuario ObtenerFigurita(FiguritaUsuario revisar)
    {

    }
}
